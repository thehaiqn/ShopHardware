using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardwawe.DAL
{
    public class DataCustomer
    {
        private string strCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\THE HAI\Documents\Hardwawe Shop.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";
        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(strCon))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            return dt;
        }
        //

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
                    int check = cmd.ExecuteNonQuery();
                    return check > 0;
                }
            }
            catch { return false; }
        }
        public string GetCustomerNameByID(string customerID)
        {
            string name = "";

            using (SqlConnection con = new SqlConnection(strCon))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT CName FROM Customers WHERE CustomerId = @ID";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@ID", customerID);


                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        name = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi DAL (GetCustomerNameByID): " + ex.Message);
                }
            }
            return name;
        }
    }
}
    