
using Hardwawe.DAL;
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
using System.Xml.Linq;

namespace Hardware_Shop
{
    public partial class Customers : Form
    {
        DataCustomer dal = new DataCustomer();
        public Customers()
        {
            InitializeComponent();
            DisplayCustomers();
        }
        private void DisplayCustomers()
        {
            string sql = "SELECT * FROM Customers";
            dataGridView1.DataSource = dal.GetDataTable(sql);
        }


        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetFields();
        }
        private void ResetFields()
        {
            txtCustom.Clear();
            txtEmail.Clear();
            txtPhone.Clear();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận xóa", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {

                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);


                    string sql = "DELETE FROM Customers WHERE CustomerID=@CustomerID";


                    SqlParameter[] parameters = {
                new SqlParameter("@CustomerID", id)
            };


                    if (dal.ExecuteNonQuery(sql, parameters))
                    {
                        MessageBox.Show("Đã xóa sản phẩm!");
                        DisplayCustomers();
                        ResetFields();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO Customers (CName, Phone, Email) VALUES (@Name, @Phone, @Email)";
            SqlParameter[] parameters = {
            new SqlParameter("@Name", txtCustom.Text),
            new SqlParameter("@Phone", txtPhone.Text),
            new SqlParameter("@Email", txtEmail.Text),

        };

            if (dal.ExecuteNonQuery(sql, parameters))
            {
                MessageBox.Show("Thêm thành công!");
                DisplayCustomers();
                ResetFields();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string sql = "UPDATE Customers SET CName=@Name, Phone=@Phone, Email=@Email WHERE CustomerID=@CustomerID";


                SqlParameter[] parameters = {
            new SqlParameter("@Name", txtCustom.Text),
            new SqlParameter("@Phone", txtPhone.Text),
           new SqlParameter("@Email", txtEmail.Text),

            new SqlParameter("@CustomerID", id)
        };


                if (dal.ExecuteNonQuery(sql, parameters))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    DisplayCustomers();
                    ResetFields();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Hide();

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Hide();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();   
            this.Hide();    
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Customers_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click_1(object sender, EventArgs e)
        {
            Sales sale = new Sales();
            sale.Show();
            this.Hide();    
        }

        private void label2_Click_2(object sender, EventArgs e)
        {
            Sales sale = new Sales();
            sale.Show();
            this.Hide();
        }

        private void label12_Click_1(object sender, EventArgs e)
        {
            Login login1 = new Login();
            login1.Show();
            this.Hide();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Login login1 = new Login();
            login1.Show();
            this.Hide();
        }

        private void label1_Click_2(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
           Product product = new Product();
            product.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
    }


