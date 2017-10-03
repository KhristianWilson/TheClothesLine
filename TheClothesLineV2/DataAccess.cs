using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TheClothesLineV2
{
    public static class DataAccess
    {

        private static string strConn = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

        public struct ParmStruct
        {
            public String name;
            public object value;

            public ParmStruct(string oname, object ovalue)
            {
                this.name = oname;
                this.value = ovalue;
            }
        }

        public static string getConnection()
        {
            return strConn;
        }

        public static SqlDataReader getReader(string SQLStatement, List<ParmStruct> parmlist = null)
        {
            SqlConnection conn = new SqlConnection(strConn);
            using (SqlCommand cmd = new SqlCommand(SQLStatement, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parmlist != null)
                {
                    foreach (var parm in parmlist)
                    {
                        cmd.Parameters.AddWithValue(parm.name, parm.value);
                    }
                }

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader
                (CommandBehavior.CloseConnection);

                return dr;
            }
        }
        public static void sendReader(string SQLStatement, List<ParmStruct> parmlist = null)
        {
            SqlConnection conn = new SqlConnection(strConn);
            using (SqlCommand cmd = new SqlCommand(SQLStatement, conn))
            {

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var parm in parmlist)
                {
                    cmd.Parameters.AddWithValue(parm.name, parm.value);
                }

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void LogErr(string location, string source, string msg, string details)
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