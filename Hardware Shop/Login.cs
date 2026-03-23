using Hardwawe.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hardware_Shop
{
    public partial class Login : Form
    {
        DataAccessor dal = new DataAccessor();
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void text1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            string pass = txtPass.Text;


            if (user == "admin" && pass == "123")
            {
               
                try
                {
                    dal.GetDataTable("SELECT TOP 1 * FROM Products");

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo");

           
                    Product p = new Product();
                    p.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối Database: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Clear();
                txtUser.Focus();
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUser.Text = "";
            txtPass.Text = "";
        }
    }
}
