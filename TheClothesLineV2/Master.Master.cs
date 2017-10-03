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
    public partial class Master : System.Web.UI.MasterPage
    {

        #region "Startup/HouseKeeping"
        private string strConn = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

        public string masterMsg
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCategories();
            }
        }

        public string getPage(String page)
        {
            if (page == "SingleProduct.aspx" || page == "ManageProductInfo.aspx")
            {
                return "ProductDisplay.aspx";
            }
            else
            {
                return page;
            }
        }
        #endregion

        #region "Load Side Bar"
        public void getCategories()
        {
            SqlDataReader dr = DataAccess.getReader("spgetCategories");
            try
            {
                if (dr.HasRows)
                {
                    rptCategories.DataSource = dr;
                    rptCategories.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dr.Close();
            }
        }
        #endregion

        #region "Searching"
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strSearch = txtSearch.Text.Trim();
            int x;
            if ((Int32.TryParse(strSearch, out x)))
            {
                Response.Redirect("~/ProductDisplay.aspx?pID=" + Server.UrlEncode(x.ToString()));
            }
            else
            {

                int bitValue = chkAll.Checked ? 1 : 0;

                if (strSearch.Length > 2)
                {
                    Response.Redirect("~/ProductDisplay.aspx?s=" + Server.UrlEncode(strSearch) + "&w=" + bitValue);
                }
                else
                {
                    lblMessage.Text = "Invalid search string...please enter more";
                }
            }
        }
        #endregion
    }
}