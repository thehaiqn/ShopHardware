using HardwareBLL;
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
    public partial class Account : Form
    {
        public Account()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            LoginBLL bll = new LoginBLL();

        
            string result = bll.RegisterAccount(txtUser.Text, txtPass.Text, txtConfirm.Text);

            MessageBox.Show(result);

            if (result == "Đăng ký thành công!")
            {
                this.Hide(); 
                Login login = new Login();
                login.ShowDialog(); 

                this.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Login login =new Login();
            login.Show();
            this.Hide();
        }

        private void text1_Click(object sender, EventArgs e)
        {

        }
    }
}
