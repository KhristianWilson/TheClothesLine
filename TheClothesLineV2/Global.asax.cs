using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TheClothesLineV2
{
    public class Global : System.Web.HttpApplication
    {
        private static string strConn = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

        protected void Application_Start(object sender, EventArgs e)
        {
            
        }

        void Application_Error(object sender, EventArgs e)
        {
            if (HttpContext.Current == null) return;
            HttpContext context = HttpContext.Current;
            Exception exception = context.Server.GetLastError();
            string location = context.Request.Url.ToString();
            string source = exception.Source;
            string msg = exception.Message;
            string details = exception.StackTrace;
            logErr(location, source, msg, details);
            context.Response.Write("<h3>Sorry, it appears there is a problem. Please try again.</h3> If this same message displays, kindly contact the <a href='mailto:admin@theclothesline.com'>web admin</a>");
            context.Server.ClearError();

        }

        private void logErr(string location, string source, string msg, string details)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    SqlCommand cmd = new SqlCommand("logerr", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@errloc", location + " " + source);
                    cmd.Parameters.AddWithValue("@errdetails", details);
                    cmd.Parameters.AddWithValue("@errmsg", msg);

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.GetType());
            }
        }
    }
}