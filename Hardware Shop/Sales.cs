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
using Excel = Microsoft.Office.Interop.Excel;
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
            cbCustomID.SelectedIndex = -1;
            txtCustom.Clear();

            cbProductID.SelectedIndex = -1;
            txtProduct.Clear();

            txtQuantity.Clear();
            txtPrice.Clear();

            dateTimePicker1.Value = DateTime.Now;

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
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                SqlCommand sqlCommand = new SqlCommand(
                    "SELECT CNAME FROM Customers WHERE CustomerID = @ID", con);

                sqlCommand.Parameters.AddWithValue("@ID", cbCustomID.Text);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    txtCustom.Text = reader["CNAME"].ToString();
                }

                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                textBox1.Text = reader["Price"].ToString();
            }
            con.Close();
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
                    SqlCommand sqlCommand = new SqlCommand("SELECT Quantity, Price FROM Products Where ProductID = @PID", con);
                    sqlCommand.Parameters.AddWithValue("@PID", cbProductID.Text);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (!reader.Read())
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm");
                        reader.Close();
                        con.Close();
                        return;
                    }
                    int availableQty = Convert.ToInt32(reader["Quantity"]);
                    decimal unitPrice = Convert.ToInt32(reader["Price"]);
                    reader.Close();
                    if (quantitySold > unitPrice)
                    {
                        MessageBox.Show("Hàng không có sẵn");
                        return;
                    }
                    decimal totalAmount = quantitySold * unitPrice;


                SqlCommand insert = new SqlCommand(
            "INSERT INTO Sales(CustomerID, CustomerName, ProductID, ProductName, QuantitySold, TotalAmount, SaleDate) " +
            "VALUES (@CusID, @CusName, @ProID, @ProName, @Qty, @Total, @Date)", con);


                insert.Parameters.AddWithValue("@CusID", cbCustomID.Text);
                insert.Parameters.AddWithValue("@CusName", txtCustom.Text);
                insert.Parameters.AddWithValue("@ProID", cbProductID.Text);
                insert.Parameters.AddWithValue("@ProName", txtProduct.Text);
                insert.Parameters.AddWithValue("@Qty", quantitySold);

                insert.Parameters.AddWithValue("@Total", textBox1.Text);

                insert.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);

                insert.ExecuteNonQuery();
                SqlCommand update = new SqlCommand("UPDATE Products SET Quantity = Quantity - @Qty WHERE ProductID = @PID", con);

                update.Parameters.AddWithValue("@Qty", quantitySold);
                update.Parameters.AddWithValue("@PID", cbProductID.Text);

                update.ExecuteNonQuery();
                MessageBox.Show("Thêm hóa đơn thành công!");
                DisplaySales();
                ResetFields();
            }

            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

            if (decimal.TryParse(textBox1.Text, out decimal price) && int.TryParse(txtQuantity.Text, out int qty))
            {
                decimal total = price * qty;
                textBox1.Text = total.ToString("0.00");
            }
            else
            {
                textBox1.Text ="";
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy dữ liệu từ dòng đang được chọn trong DataGridView
                int saleId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SaleID"].Value);
                int oldQty = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["QuantitySold"].Value);
                string productId = dataGridView1.SelectedRows[0].Cells["ProductID"].Value.ToString();

                try
                {
                    if (con.State == ConnectionState.Open) {
                        con.Close();
                    }
                        con.Open();


                    SqlCommand cmd = new SqlCommand("UPDATE Products SET Quantity = Quantity * @oldQty WHERE ProductID = @PID", con);
                    cmd.Parameters.AddWithValue("@oldQty", oldQty);
                    cmd.Parameters.AddWithValue("@PID", productId);
                    cmd.ExecuteNonQuery();

                    // Truy vấn lấy số lượng mới sau khi cập nhật (đang viết dở trong hình)
                    SqlCommand sqlCommand = new SqlCommand("SELECT Quantity from Products WHERE ProductID = @PID", con);
                    sqlCommand.Parameters.AddWithValue("@PID",cbProductID.Text);
                    int currentStock = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    int newQty = Convert.ToInt32(txtQuantity.Text);
                    if (newQty > currentStock)
                    {
                        MessageBox.Show("Số lượng trong kho không đủ để cập nhật");
                        return;
                    }
                    decimal unitPrice = Convert.ToDecimal(textBox1.Text);
                    decimal total = unitPrice * newQty;
                    SqlCommand sql = new SqlCommand(@"UPDATE Sales SET CustomerID=@CusID, CustomerName=@CusName, ProductID=@ProID, ProductName=@ProName, QuantitySold=@Qty, TotalAmount=@Total, SaleDate=@Date WHERE SaleID=@SID ", con);
                    sql.Parameters.AddWithValue("@CusID", cbCustomID.Text);
                    sql.Parameters.AddWithValue("@CusName", txtCustom.Text);
                    sql.Parameters.AddWithValue("@ProID", cbProductID.Text);
                    sql.Parameters.AddWithValue("@ProName", txtProduct.Text);
                    sql.Parameters.AddWithValue("@Qty", newQty);

                    sql.Parameters.AddWithValue("@Total", total);

                    sql.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);
                    sql.Parameters.AddWithValue("@SID", saleId);
                    sql.ExecuteNonQuery();
                    
                    SqlCommand sqlCommand1 = new SqlCommand("UPDATE Products SET Quantity = Quantity - @newQty WHERE ProductID = @PID", con);
                    sqlCommand1.Parameters.AddWithValue("@newQty", newQty);
                    sqlCommand1.Parameters.AddWithValue("@PID", cbProductID.Text);
                    sqlCommand1.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật hóa đơn thành công");
                    
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
        }
        private void label1_Click_1(object sender, EventArgs e)
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

        private void label3_Click_1(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
            this.Hide();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cbProductID.Text) && int.TryParse(txtQuantity.Text, out int qty))
            {
                try
                {
                    con.Open();

                    SqlCommand s = new SqlCommand("SELECT Price FROM Products WHERE ProductID = @ID", con);
                    s.Parameters.AddWithValue("@ID", cbProductID.Text);

                    object result = s.ExecuteScalar();

                    if (result != null && decimal.TryParse(result.ToString(), out decimal unitPrice))
                    {

                        decimal total = unitPrice * qty;
                        textBox1.Text = total.ToString("0.00");
                    }
                    else
                    {
                        textBox1.Text = "0.00";
                    }
                }
                catch
                {
                    con.Close();
                    textBox1.Text = "0.00";
                }
            }
            else
            {

                textBox1.Text = "0.00";
            }
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {
         
               cbCustomID.Text = dataGridView1.CurrentRow.Cells["CustomerID"].Value.ToString();
                txtCustom.Text = dataGridView1.CurrentRow.Cells["CustomerName"].Value.ToString();
                cbProductID.Text = dataGridView1.CurrentRow.Cells["ProductID"].Value.ToString();
                txtProduct.Text = dataGridView1.CurrentRow.Cells["ProductName"].Value.ToString();
                txtQuantity.Text = dataGridView1.CurrentRow.Cells["QuantitySold"].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells["TotalAmount"].Value.ToString();
                dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["SaleDate"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa");
                return;
            }

            int saleID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SaleID"].Value);
            int productID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ProductID"].Value);
            int quantitySold = Convert.ToInt32(dataGridView1.CurrentRow.Cells["QuantitySold"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;

            try
            {
                // 🔥 FIX QUAN TRỌNG
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                // 1. Xóa hóa đơn
                SqlCommand delete = new SqlCommand(
                    "DELETE FROM Sales WHERE SaleID = @ID", con);

                delete.Parameters.AddWithValue("@ID", saleID);
                delete.ExecuteNonQuery();

                // 2. Cộng lại số lượng sản phẩm
                SqlCommand update = new SqlCommand(
                    "UPDATE Products SET Quantity = Quantity + @Qty WHERE ProductID = @PID", con);

                update.Parameters.AddWithValue("@Qty", quantitySold);
                update.Parameters.AddWithValue("@PID", productID);
                update.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công!");

                con.Close(); // 🔥 luôn đóng lại
                DisplaySales();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application app = new Excel.Application();
                app.Workbooks.Add(Type.Missing);

                // Tên sheet
                Excel._Worksheet sheet = app.ActiveSheet;
                sheet.Name = "HoaDon";

                // 1. Header
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    sheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }

                // 2. Data
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        sheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                // 3. Autofit
                sheet.Columns.AutoFit();
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Excel File|*.xlsx";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    sheet.SaveAs(save.FileName);
                    MessageBox.Show("Lưu file thành công!");
                }
                // 4. Hiển thị Excel
                app.Visible = true;

                MessageBox.Show("Xuất Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    }

