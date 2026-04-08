using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace Hardware.DAL
{
   public class DataAccessor
    {
        private string strCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\THE HAI\Documents\Hardwawe Shop.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";
        public DataTable GetDataTable(string sql, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(strCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                if (parameters != null)
                {
                    da.SelectCommand.Parameters.AddRange(parameters);
                }
                da.Fill(dt);
            }
            return dt;
        }

        
        public bool ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0; 
                }
            }
            catch (Exception ex)
            {
           
                System.Diagnostics.Debug.WriteLine("Lỗi SQL: " + ex.Message);
                return false;
            }
        }
    }
}

