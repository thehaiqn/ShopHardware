
using HardwareBLL;
using Hardwawe.DAL;
using Hardwawe.DTO;
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
        private CustomerBLL customerBLL = new CustomerBLL();
        public Customers()
        {
            InitializeComponent();
            DisplayCustomers();
        }
        private void DisplayCustomers()
        {
            dataGridView1.DataSource = customerBLL.GetCustomers();
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
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                    if (customerBLL.DeleteCustomer(id))
                    {
                        MessageBox.Show("Đã xóa khách hàng!");
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
            CustomerDTO c = new CustomerDTO
            {
                CName = txtCustom.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text
            };

            if (customerBLL.AddCustomer(c))
            {
                MessageBox.Show("Thêm thành công!");
                DisplayCustomers();
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

          
                CustomerDTO c = new CustomerDTO
                {
                    CustomerID = id,
                    CName = txtCustom.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text
                };

                if (customerBLL.UpdateCustomer(c))
                {
                    MessageBox.Show("Cập nhật khách hàng thành công!");
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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {

                txtCustom.Text = dataGridView1.CurrentRow.Cells["CName"].Value.ToString();
                txtPhone.Text = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();
                txtEmail.Text = dataGridView1.CurrentRow.Cells["Email"].Value.ToString();
             
             


            }
        }
    }
    }


