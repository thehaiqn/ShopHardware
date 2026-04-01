using Hardwawe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardwawe.DAL
{
    public class CustomerDAL
    {
        private string strCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\THE HAI\Documents\Hardwawe Shop.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";
        public DataTable GetAllCustomers()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(strCon))
            {
                string sql = "SELECT * FROM Customers";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
            }
            return dt;
        }


        private bool Execute(string sql, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strCon))
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch { return false; }
        }

        public bool Insert(CustomerDTO c)
        {
            string sql = "INSERT INTO Customers (CName, Phone, Email) VALUES (@name, @phone, @email)";
            SqlParameter[] pars = {
                new SqlParameter("@name", c.CName),
                new SqlParameter("@phone", c.Phone),
                new SqlParameter("@email", c.Email)
            };
            return Execute(sql, pars);
        }

        public bool Update(CustomerDTO c)
        {
            string sql = "UPDATE Customers SET CName=@name, Phone=@phone, Email=@email WHERE CustomerID=@id";
            SqlParameter[] pars = {
                new SqlParameter("@name", c.CName),
                new SqlParameter("@phone", c.Phone),
                new SqlParameter("@email", c.Email),
                new SqlParameter("@id", c.CustomerID)
            };
            return Execute(sql, pars);
        }

        public bool Delete(int id)
        {
            string sql = "DELETE FROM Customers WHERE CustomerID=@id";
            SqlParameter[] pars = { new SqlParameter("@id", id) };
            return Execute(sql, pars);
        }
      
        }
    }


    