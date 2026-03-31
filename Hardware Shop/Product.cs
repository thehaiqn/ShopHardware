using HardwareBLL;
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


namespace Hardware_Shop
{
    public partial class Product : Form
    {
        private ProductBLL bll = new ProductBLL();
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
            dataGridView1.DataSource = bll.GetProducts();
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
            try
            {
                ProductDTO product = new ProductDTO
                {
                    ProductName = txtName.Text,
                    Category = cbLoai.Text,
                    Quantity = int.Parse(txtSoLuong.Text),
                    Price = decimal.Parse(txtGia.Text)
                };

                if (bll.AddProduct(product))
                {
                    MessageBox.Show("Thêm thành công!");
                    DisplayProducts();
                    ResetFields();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại. Vui lòng kiểm tra lại thông tin!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nhập liệu: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    ProductDTO product = new ProductDTO
                    {
                        ProductID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value),
                        ProductName = txtName.Text,
                        Category = cbLoai.Text,
                        Quantity = int.Parse(txtSoLuong.Text),
                        Price = decimal.Parse(txtGia.Text)
                    };

                    if (bll.UpdateProduct(product))
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
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nhập liệu: " + ex.Message);
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
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xoá sản phẩm này không?", "Xác nhận", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                    if (bll.DeleteProduct(id))
                    {
                        MessageBox.Show("Đã xoá sản phẩm!");
                        DisplayProducts();
                        ResetFields();
                    }
                    else
                    {
                        MessageBox.Show("Xoá thất bại!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xoá!");
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
           
        }
    } }