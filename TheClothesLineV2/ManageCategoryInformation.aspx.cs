using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheClothesLineV2
{
    public partial class ManageCategoryInformation : System.Web.UI.Page
    {
        #region "Start-up"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string catId = Request.QueryString["cID"];
                if (!String.IsNullOrEmpty(catId))
                {
                    GetCategory(catId);
                    Master myMaster = (Master)this.Master;
                    myMaster.masterMsg = "";
                }
            }
        }

        private void GetCategory(string catId)
        {

            List<DataAccess.ParmStruct> parmList = new List<DataAccess.ParmStruct>();
            parmList.Add(new DataAccess.ParmStruct("@categoryid", catId));
            SqlDataReader dr = DataAccess.getReader("spgetCategoriesFull", parmList);

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
                Master myMaster = (Master)this.Master;
                myMaster.masterMsg = ex.Message;
                DataAccess.LogErr(Request.Url.ToString(), ex.Source, ex.Message, ex.StackTrace);
            }
            finally
            {
                dr.Close();
            }
        }
        #endregion

        #region "Actions"
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    List<DataAccess.ParmStruct> parmList = new List<DataAccess.ParmStruct>();
                    string catname = (frmCatDetails.Row.FindControl("txtCatName") as TextBox).Text;
                    string catDesc = (frmCatDetails.Row.FindControl("txtAreaDesc") as TextBox).Text;
                    parmList.Add(new DataAccess.ParmStruct("@categoryName", catname));
                    parmList.Add(new DataAccess.ParmStruct("@categoryDesc", catDesc));
                    DataAccess.sendReader("spinsertCategory", parmList);
                    btnClear_Click(null, null);
                    (this.Master as Master).getCategories();
                    (this.Master as Master).masterMsg = "Add Sucessful";
                }
                catch (Exception ex)
                {
                    (this.Master as Master).masterMsg = ex.Message;
                    DataAccess.LogErr(Request.Url.ToString(), ex.Source, ex.Message, ex.StackTrace);
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    List<DataAccess.ParmStruct> parmList = new List<DataAccess.ParmStruct>();
                    string catname = (frmCatDetails.Row.FindControl("txtCatName") as TextBox).Text;
                    string catDesc = (frmCatDetails.Row.FindControl("txtAreaDesc") as TextBox).Text;
                    string catid = (frmCatDetails.Row.FindControl("catId") as Label).Text;
                    parmList.Add(new DataAccess.ParmStruct("@categoryId", catid));
                    parmList.Add(new DataAccess.ParmStruct("@categoryName", catname));
                    parmList.Add(new DataAccess.ParmStruct("@categoryDesc", catDesc));
                    DataAccess.sendReader("spupdateCategory", parmList);
                    (this.Master as Master).getCategories();
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
                string catid = (frmCatDetails.Row.FindControl("catId") as Label).Text;
                parmList.Add(new DataAccess.ParmStruct("@categoryid", catid));
                DataAccess.sendReader("spdeleteCategory", parmList);
                btnClear_Click(null, null);
                (this.Master as Master).getCategories();
                (this.Master as Master).masterMsg = "Delete Sucessful";
            }
            catch (Exception ex)
            {
                (this.Master as Master).masterMsg = ex.Message;
                DataAccess.LogErr(Request.Url.ToString(), ex.Source, ex.Message, ex.StackTrace);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            (frmCatDetails.Row.FindControl("catId") as Label).Text = "";
            (frmCatDetails.Row.FindControl("txtCatName") as TextBox).Text = "";
            (frmCatDetails.Row.FindControl("txtAreaDesc") as TextBox).Text = "";
        }
        #endregion
    }
}
