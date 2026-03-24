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
    public partial class Product : Form
    {
        DataAccessor dal = new DataAccessor();
        public Product()
        {
            InitializeComponent();
            cbLoai.Items.Clear();
            cbLoai.Items.Add("CPU");
            cbLoai.Items.Add("RAM");
            cbLoai.Items.Add("Mouse");
            cbLoai.Items.Add("Keyboard");

            cbLoai.SelectedIndex = 0;
            DisplayProducts();
           
            
        }
        
        private void DisplayProducts()
        {
            string sql = "SELECT * FROM Products";
            dataGridView1.DataSource = dal.GetDataTable(sql);
        }

      

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

      

      

        private void label3_Click(object sender, EventArgs e)
        {
            Customers cs = new Customers();
            cs.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Sales sale = new Sales();
            sale.Show();
            this.Hide();
        }

  

       
        
        private void ResetFields()
        {
            txtName.Clear();
            txtGia.Clear();
            txtSoLuong.Clear();
            cbLoai.SelectedIndex = -1;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetFields();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO Products (ProductName, Category, Quantity, Price) VALUES (@ProductName, @PCategory, @Quantity, @Price)";
            SqlParameter[] parameters = {
            new SqlParameter("@ProductName", txtName.Text),
            new SqlParameter("@PCategory", cbLoai.Text),
            new SqlParameter("@Quantity", int.Parse(txtSoLuong.Text)),
            new SqlParameter("@Price", decimal.Parse(txtGia.Text))
        };

            if (dal.ExecuteNonQuery(sql, parameters))
            {
                MessageBox.Show("Thêm thành công!");
                DisplayProducts();
                ResetFields();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string sql = "UPDATE Products SET ProductName=@ProductName, Category=@PCategory, Quantity=@Quantity, Price=@Price WHERE ProductID=@ProductID";

             
                SqlParameter[] parameters = {
            new SqlParameter("@ProductName", txtName.Text),
            new SqlParameter("@PCategory", cbLoai.Text),
            new SqlParameter("@Quantity", decimal.Parse(txtSoLuong.Text)),
            new SqlParameter("@Price", decimal.Parse(txtGia.Text)),
            new SqlParameter("@ProductID", id)
        };

      
                if (dal.ExecuteNonQuery(sql, parameters))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    DisplayProducts();
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


        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
      
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận xóa", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
       
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

         
                    string sql = "DELETE FROM Products WHERE ProductID=@ProductID";

      
                    SqlParameter[] parameters = {
                new SqlParameter("@ProductID", id)
            };


                    if (dal.ExecuteNonQuery(sql, parameters))
                    {
                        MessageBox.Show("Đã xóa sản phẩm!");
                        DisplayProducts(); 
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {

                txtName.Text = dataGridView1.CurrentRow.Cells["ProductName"].Value.ToString();
                cbLoai.Text = dataGridView1.CurrentRow.Cells["Category"].Value.ToString();
                txtSoLuong.Text = dataGridView1.CurrentRow.Cells["Quantity"].Value.ToString();
                txtGia.Text = dataGridView1.CurrentRow.Cells["Price"].Value.ToString();


            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
              
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + 4);
            }
        }

        private void cbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Products_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbLoai_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
            this.Hide();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Hide();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    } }