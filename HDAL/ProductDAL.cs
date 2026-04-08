using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hardwawe.DTO;

namespace Hardwawe.DAL
{
    public class ProductDAL
    { 
        private string strCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\THE HAI\Documents\Hardwawe Shop.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";
        public DataTable GetAllProducts()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(strCon))
            {
                string sql = "SELECT * FROM Products";
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
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                // In lỗi ra màn hình Output của Visual Studio để kiểm tra nếu code không chạy
                System.Diagnostics.Debug.WriteLine("Lỗi SQL: " + ex.Message);
                return false;
            }
        }

   
        public bool Insert(ProductDTO p)
        {
            string sql = "INSERT INTO Products (ProductName, Category, Quantity, Price) VALUES (@name, @cat, @qty, @price)";
            SqlParameter[] pars = {
                new SqlParameter("@name", p.ProductName),
                new SqlParameter("@cat", p.Category),
                new SqlParameter("@qty", p.Quantity),
                new SqlParameter("@price", p.Price)
            };
            return Execute(sql, pars);
        }

        // 4. Hàm Sửa sản phẩm
        public bool Update(ProductDTO p)
        {
            string sql = "UPDATE Products SET ProductName=@name, Category=@cat, Quantity=@qty, Price=@price WHERE ProductID=@id";
            SqlParameter[] pars = {
                new SqlParameter("@name", p.ProductName),
                new SqlParameter("@cat", p.Category),
                new SqlParameter("@qty", p.Quantity),
                new SqlParameter("@price", p.Price),
                new SqlParameter("@id", p.ProductID)
            };
            return Execute(sql, pars);
        }

        // 5. Hàm Xóa sản phẩm
        public bool Delete(int id)
        {
            string sql = "DELETE FROM Products WHERE ProductID=@id";
            SqlParameter[] pars = {
                new SqlParameter("@id", id)
            };
            return Execute(sql, pars);
        }
        public bool UpdateStock(int productId, int qtySold)
        {
            // Câu lệnh trừ số lượng tồn kho dựa trên ID sản phẩm
            string sql = "UPDATE Products SET Quantity = Quantity - @qty WHERE ProductID = @id";
            SqlParameter[] pars = {
        new SqlParameter("@qty", qtySold),
        new SqlParameter("@id", productId)
    };
            return Execute(sql, pars); // Dùng lại hàm Execute chung đã viết hôm qua
        }
    }
}

