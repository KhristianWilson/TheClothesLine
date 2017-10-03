using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheClothesLineV2
{
    public partial class ManageProductInfo : System.Web.UI.Page
    {
        #region "Start-up"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master myMaster = (Master)this.Master;
                myMaster.masterMsg = "";

                string proId = Request.QueryString["pID"];
                if (!String.IsNullOrEmpty(proId))
                {
                    GetProduct(proId);
                }
            }
        }

        private void GetProduct(string proId)
        {
            List<DataAccess.ParmStruct> parmList = new List<DataAccess.ParmStruct>();
            parmList.Add(new DataAccess.ParmStruct("@productid", proId));
            SqlDataReader dr = DataAccess.getReader("spgetProductsFull", parmList);

            try
            {
                if (dr.HasRows)
                {
                    frmCatDetails.DataSource = dr;
                    frmCatDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                (this.Master as Master).masterMsg = ex.Message;
                DataAccess.LogErr(Request.Url.ToString(), ex.Source, ex.Message, ex.StackTrace);
            }
            finally
            {
                dr.Close();
            }
        }
        #endregion

        #region "Actions"

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    List<DataAccess.ParmStruct> parmList = new List<DataAccess.ParmStruct>();

                    parmList.Add(new DataAccess.ParmStruct("@productId", (frmCatDetails.FindControl("prodId") as Label).Text));
                    parmList.Add(new DataAccess.ParmStruct("@name", (frmCatDetails.FindControl("txtProdName") as TextBox).Text));
                    parmList.Add(new DataAccess.ParmStruct("@bDesc", (frmCatDetails.FindControl("txtProdBDesc") as TextBox).Text));
                    parmList.Add(new DataAccess.ParmStruct("@fDesc", (frmCatDetails.FindControl("txtProdFDesc") as TextBox).Text));
                    parmList.Add(new DataAccess.ParmStruct("@price", (frmCatDetails.FindControl("txtProdPrice") as TextBox).Text));

                    Boolean featured = (frmCatDetails.FindControl("chkFeatured") as CheckBox).Checked;
                    if (featured)
                    {
                        parmList.Add(new DataAccess.ParmStruct("@Featured", 1));
                    }
                    else if (featured == false)
                    {
                        parmList.Add(new DataAccess.ParmStruct("@Featured", 0));
                    }

                    parmList.Add(new DataAccess.ParmStruct("@status", (frmCatDetails.FindControl("ddlProdStatus") as DropDownList).SelectedValue));
                    parmList.Add(new DataAccess.ParmStruct("@categoryId", (frmCatDetails.FindControl("ddlcatId") as DropDownList).SelectedValue));

                    DataAccess.sendReader("spupdateProduct", parmList);
                    (this.Master as Master).masterMsg = "Update Sucessful";

                }
                catch (Exception ex)
                {
                    (this.Master as Master).masterMsg = ex.Message;
                    DataAccess.LogErr(Request.Url.ToString(), ex.Source, ex.Message, ex.StackTrace);
                }
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                List<DataAccess.ParmStruct> parmList = new List<DataAccess.ParmStruct>();
                string proid = (frmCatDetails.Row.FindControl("prodId") as Label).Text;
                parmList.Add(new DataAccess.ParmStruct("@productid", proid));
                DataAccess.sendReader("spdeleteProduct", parmList);
                btnClear_Click(null, null);
                (this.Master as Master).masterMsg = "Delete Sucessful";
            }
            catch (Exception ex)
            {
                (this.Master as Master).masterMsg = ex.Message;
                DataAccess.LogErr(Request.Url.ToString(), ex.Source, ex.Message, ex.StackTrace);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    List<DataAccess.ParmStruct> parmList = new List<DataAccess.ParmStruct>();

                    parmList.Add(new DataAccess.ParmStruct("@name", (frmCatDetails.FindControl("txtProdName") as TextBox).Text));
                    parmList.Add(new DataAccess.ParmStruct("@bDesc", (frmCatDetails.FindControl("txtProdBDesc") as TextBox).Text));
                    parmList.Add(new DataAccess.ParmStruct("@fDesc", (frmCatDetails.FindControl("txtProdFDesc") as TextBox).Text));
                    parmList.Add(new DataAccess.ParmStruct("@price", (frmCatDetails.FindControl("txtProdPrice") as TextBox).Text));

                    Boolean featured = (frmCatDetails.FindControl("chkFeatured") as CheckBox).Checked;
                    if (featured)
                    {
                        parmList.Add(new DataAccess.ParmStruct("@Featured", 1));
                    }
                    else if (featured == false)
                    {
                        parmList.Add(new DataAccess.ParmStruct("@Featured", 0));
                    }

                    parmList.Add(new DataAccess.ParmStruct("@status", (frmCatDetails.FindControl("ddlProdStatus") as DropDownList).SelectedValue));
                    parmList.Add(new DataAccess.ParmStruct("@categoryId", (frmCatDetails.FindControl("ddlcatId") as DropDownList).SelectedValue));

                    DataAccess.sendReader("spaddProduct", parmList);
                    btnClear_Click(null, null);
                    (this.Master as Master).masterMsg = "Add Sucessful";

                }
                catch (Exception ex)
                {
                    (this.Master as Master).masterMsg = ex.Message;
                    DataAccess.LogErr(Request.Url.ToString(), ex.Source, ex.Message, ex.StackTrace);
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            (frmCatDetails.FindControl("prodId") as Label).Text = "";
            (frmCatDetails.FindControl("txtProdName") as TextBox).Text = "";
            (frmCatDetails.FindControl("txtProdBDesc") as TextBox).Text = "";
            (frmCatDetails.FindControl("txtProdFDesc") as TextBox).Text = "";
            (frmCatDetails.FindControl("txtProdPrice") as TextBox).Text = "";
            (frmCatDetails.FindControl("chkFeatured") as CheckBox).Checked = false;
            (frmCatDetails.FindControl("ddlProdStatus") as DropDownList).SelectedIndex = -1;
            (frmCatDetails.FindControl("ddlcatId") as DropDownList).SelectedIndex = -1;
        }
        #endregion
    }
}