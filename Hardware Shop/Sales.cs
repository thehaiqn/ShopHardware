using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hardwawe.DAL;

namespace Hardware_Shop

{

    public partial class Sales : Form
    {

        public Sales()
        {
            InitializeComponent();
            LoadCustomerIDs();
            LoadProductIDs();
            DisplaySales();


        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\THE HAI\Documents\Hardwawe Shop.mdf"";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;");
        private void DisplaySales()
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(" SELECT * FROM Sales", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void ResetFields()
        {
            cbCustomID.Text = "";
            txtCustom.Text = "";
            cbProductID.Text = "";
            txtProduct.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
            dateTimePicker1.Text = "";

        }
        private void LoadCustomerIDs()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT CustomerID FROM Customers", con);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    cbCustomID.Items.Add(sqlDataReader["CustomerID"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void LoadProductIDs()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ProductID FROM Products", con);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    cbProductID.Items.Add(sqlDataReader["ProductID"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetFields();
        }

        private void cbCustomID_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT CNAME From Customers WHERE CustomerID=@ID", con);
            sqlCommand.Parameters.AddWithValue("@ID", cbCustomID.Text);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                txtCustom.Text = reader["CName"].ToString();
            }
            con.Close();
        }

        private void cbProductID_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cbProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT ProductName, Price From Products WHERE ProductID=@ID", con);
            sqlCommand.Parameters.AddWithValue("@ID", cbProductID.Text);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                txtProduct.Text = reader["ProductName"].ToString();
                txtPrice.Text = reader["Price"].ToString();
            }
            con.Close();
        }
        private void ProaIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbCustomID.Text == "" || cbProductID.Text == "" || txtQuantity.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                return;
            }
            int quantitySold;
            if (!int.TryParse(txtQuantity.Text, out quantitySold) || quantitySold <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ");
                return;
            }
            try
            {
                con.Open();


                string sql = "INSERT INTO Sales(CustomerID, CustomerName, ProductID, ProductName, QuantitySold, TotalAmount, SaleDate) " +
                             "VALUES (@CusID, @CusName, @ProID, @ProName, @Qty, @Total, @Date)";

                SqlCommand cmd = new SqlCommand(sql, con);


                cmd.Parameters.AddWithValue("@CusID", cbCustomID.Text);
                cmd.Parameters.AddWithValue("@CusName", txtCustom.Text);
                cmd.Parameters.AddWithValue("@ProID", cbProductID.Text);
                cmd.Parameters.AddWithValue("@ProName", txtProduct.Text);
                cmd.Parameters.AddWithValue("@Qty", txtQuantity.Text);


                int soLuong = Convert.ToInt32(txtQuantity.Text);
                decimal donGia = Convert.ToDecimal(txtPrice.Text);
                decimal tongTien = soLuong * donGia;
          
                cmd.Parameters.AddWithValue("@Total", tongTien);

                cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm hóa đơn thành công");
                con.Close();


                DisplaySales();
                ResetFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }


        }


        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
     
                if (string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrEmpty(txtPrice.Text))
                {
                    txtPrice.Text = "";
                    return;
                }

  
                int soLuong = Convert.ToInt32(txtQuantity.Text);
                int donGia = Convert.ToInt32(txtPrice.Text);

             
                int tongTien = soLuong * donGia;

 
                txtPrice.Text = tongTien.ToString();
            }
            catch
            {
          
               txtPrice.Text = "0";
            }
        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        } }}
