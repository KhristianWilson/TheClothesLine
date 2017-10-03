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
    public partial class ProductDisplay : System.Web.UI.Page
    {
        #region "startup"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master myMaster = (Master)this.Master;
                myMaster.masterMsg = "";

                string catId = Request.QueryString["cID"];
                if (!String.IsNullOrEmpty(catId))
                {
                    GetCatDetails(catId);
                }

                string pID = Request.QueryString["pID"];
                if (!String.IsNullOrEmpty(pID))
                {
                    getProdDetails(pID);
                }

                string strSearch = Request.QueryString["s"];
                string strSearchType = Request.QueryString["w"];

                if (!string.IsNullOrEmpty(strSearch))
                {
                    getSearchResults(strSearch, strSearchType);
                }

                if (String.IsNullOrEmpty(catId) && String.IsNullOrEmpty(pID) && String.IsNullOrEmpty(strSearch) && String.IsNullOrEmpty(strSearchType))
                {
                    getDisplay();
                }
            }
        }
        #endregion

        #region "Searching"
        private void getSearchResults(string strSearch, string SearchType)
        {
            if (!string.IsNullOrEmpty(strSearch) && strSearch.Length > 2)
            {
                string mystring = strSearch.Trim();
                String[] keywords = new string[4];
                mystring = mystring.Replace(" ", ",");
                mystring = mystring.Replace(";", ",");
                keywords = mystring.Split(Convert.ToChar(","));

                doSearch(keywords, SearchType);
            }
            else
            {
                (this.Master as Master).masterMsg = "Please enter a vaild search string";
            }
        }

        private void doSearch(string[] keywords, string SearchType)
        {
            try
            {
                List<DataAccess.ParmStruct> parmList = new List<DataAccess.ParmStruct>();

                int i;
                string strwords = null;
                for (i = 0; i <= keywords.Length - 1; i++)
                {
                    if (!String.IsNullOrEmpty(keywords.GetValue(i).ToString()))
                    {
                        if (i == 0 || i == 4)
                        {
                            strwords = strwords + " " + keywords.GetValue(i).ToString();
                            strwords = strwords.Trim();
                        }
                        else if (i < 4)
                        {
                            if (SearchType == "0")
                            {
                                strwords = strwords + " or " + keywords.GetValue(i).ToString();
                                strwords = strwords.Trim();
                            }
                            if (SearchType == "1")
                            {
                                strwords = strwords + " and " + keywords.GetValue(i).ToString();
                                strwords = strwords.Trim();
                            }

                        }

                    }
                }

                parmList.Add(new DataAccess.ParmStruct("@AllWords", SearchType));
                parmList.Add(new DataAccess.ParmStruct("@Words", strwords));
                SqlDataReader dr = DataAccess.getReader("SearchCatalog", parmList);

                if (dr.HasRows)
                {
                    rptFeatured.DataSource = dr;
                    rptFeatured.DataBind();
                }
                else
                {
                    (this.Master as Master).masterMsg = "No results found try again";
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                (this.Master as Master).masterMsg = ex.Message;
            }
        }
        #endregion

        #region "Retrieve Data"
        private void getDisplay()
        {
            SqlDataReader dr = DataAccess.getReader("spgetFeatured");

            try
            {
                if (dr.HasRows)
                {
                    rptFeatured.DataSource = dr;
                    rptFeatured.DataBind();
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

        private void GetCatDetails(string catId)
        {
            List<DataAccess.ParmStruct> parmList = new List<DataAccess.ParmStruct>();
            parmList.Add(new DataAccess.ParmStruct("categoryid", catId));
            SqlDataReader dr = DataAccess.getReader("spgetProductByCategory", parmList);

            try
            {
                if (dr.HasRows)
                {
                    rptFeatured.DataSource = dr;
                    rptFeatured.DataBind();
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

        private void getProdDetails(string pId)
        {
            List<DataAccess.ParmStruct> parmList = new List<DataAccess.ParmStruct>();
            parmList.Add(new DataAccess.ParmStruct("@productID ", pId));
            SqlDataReader dr = DataAccess.getReader("spgetProductsFull", parmList);

            try
            {
                if (dr.HasRows)
                {
                    frmDetails.DataSource = dr;
                    frmDetails.DataBind();
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

        #region "Edit Info"

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string productId = (frmDetails.FindControl("prodId") as Label).Text;
            Response.Redirect("ManageProductInfo.aspx?pId=" + productId);
        }

        #endregion
    }
}