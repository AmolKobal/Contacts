using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactDemo.Models
{
    public class Common
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = GetConnectionString();
            sqlcon.Open();
            return sqlcon;
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ContactConnection"].ConnectionString;
        }

        public static void LogError(ModuleType module, Exception ex)
        {
            try
            {
                using (SqlConnection sqlCon = GetConnection())
                {
                    SqlCommand sqlCmd = sqlCon.CreateCommand();
                    sqlCmd.CommandText = "INSERT INTO ErrorLog(LogDateTime, Module, Description)" +
                        "VALUES(GETDATE(), " + module.ToString() + ", " + ex.Message + ")";
                    sqlCmd.CommandType = System.Data.CommandType.Text;

                    sqlCmd.ExecuteNonQuery();
                    sqlCmd.Dispose();
                }
            }
            catch
            {
            }
        }
    }
    public enum ModuleType
    {
        Create = 1,
        Edit = 2,
        List = 3,
        Delete = 4
    }

}