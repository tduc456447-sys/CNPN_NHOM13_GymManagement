using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
namespace CNPN_NHOM13_GymManagement
{
    public partial class Form1 : Form
    {
        // 🔗 Connection string (sửa nếu cần)
        string connectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=GymDB;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(30, 30, 30); // nền tối
            this.ForeColor = Color.White;

            // TextBox
            txtUsername.BackColor = Color.FromArgb(50, 50, 50);
            txtUsername.ForeColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;

            txtPassword.BackColor = Color.FromArgb(50, 50, 50);
            txtPassword.ForeColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;

            // Button Login
            btnLogin.BackColor = Color.FromArgb(0, 122, 204); // xanh đẹp
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;

            // Button Exit
            Exit.BackColor = Color.FromArgb(200, 50, 50); // đỏ nhẹ
            Exit.ForeColor = Color.White;
            Exit.FlatStyle = FlatStyle.Flat;
            Exit.FlatAppearance.BorderSize = 0;

            // Checkbox
            //chkRemember.ForeColor = Color.White;

            // Ẩn mật khẩu
            txtPassword.UseSystemPasswordChar = true;

            // Nhấn Enter = login
            this.AcceptButton = btnLogin;
            //
            txtUsername.Text = Properties.Settings.Default.Username;
            txtPassword.Text = Properties.Settings.Default.Password;


        }

        // 🔐 Nút đăng nhập
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // validate
            if (username == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT * FROM Users WHERE Username=@u AND Password=@p";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        MessageBox.Show("Đăng nhập thành công!");

                        // 👉 mở form chính
                        MainForm main = new MainForm();
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối DB: " + ex.Message);
                }
            }

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
        
    

    


