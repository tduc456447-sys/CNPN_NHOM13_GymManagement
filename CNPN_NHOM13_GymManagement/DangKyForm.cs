using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CNPN_NHOM13_GymManagement.Database;

namespace CNPN_NHOM13_GymManagement
{
    public partial class DangKyForm : Form
    {
        private readonly DbHelper _db = new DbHelper();

        private static readonly Color CBlue = Color.FromArgb(59, 130, 246);
        private static readonly Color CBlueDark = Color.FromArgb(37, 99, 235);
        private static readonly Color CRed = Color.FromArgb(239, 68, 68);
        private static readonly Color CRedDark = Color.FromArgb(220, 38, 38);

        public DangKyForm()
        {
            InitializeComponent();
            AttachEvents();
        }

        private void AttachEvents()
        {
            btnDangKy.Click += BtnDangKy_Click;
            btnHuy.Click += (s, e) => this.Close();
            btnDangKy.MouseEnter += (s, e) => btnDangKy.BackColor = CBlueDark;
            btnDangKy.MouseLeave += (s, e) => btnDangKy.BackColor = CBlue;
            btnHuy.MouseEnter += (s, e) => btnHuy.BackColor = CRedDark;
            btnHuy.MouseLeave += (s, e) => btnHuy.BackColor = CRed;

            txtHoTen.TextChanged += (s, e) => lblError.Text = "";
            txtTenDangNhap.TextChanged += (s, e) => lblError.Text = "";
            txtMatKhau.TextChanged += (s, e) => lblError.Text = "";
            txtXacNhan.TextChanged += (s, e) => lblError.Text = "";
            txtSDT.TextChanged += (s, e) => lblError.Text = "";
            txtEmail.TextChanged += (s, e) => lblError.Text = "";
        }

        // ── Đăng ký ──────────────────────────────────────────────────
        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (!Validate()) return;

            try
            {
                // Kiểm tra username trùng
                int uCount = Convert.ToInt32(_db.ExecuteScalar(
                    "SELECT COUNT(*) FROM Users WHERE Username=@u",
                    new[] { new SqlParameter("@u", txtTenDangNhap.Text.Trim()) }));
                int pCount = Convert.ToInt32(_db.ExecuteScalar(
                    "SELECT COUNT(*) FROM PendingUsers WHERE Username=@u",
                    new[] { new SqlParameter("@u", txtTenDangNhap.Text.Trim()) }));
                if (uCount + pCount > 0) { SetError("Tên đăng nhập đã tồn tại!"); txtTenDangNhap.Focus(); return; }

                // Kiểm tra SĐT trùng
                if (!string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    int ph = Convert.ToInt32(_db.ExecuteScalar(
                        "SELECT COUNT(*) FROM Users WHERE Phone=@p",
                        new[] { new SqlParameter("@p", txtSDT.Text.Trim()) }));
                    if (ph > 0) { SetError("Số điện thoại đã được đăng ký!"); txtSDT.Focus(); return; }
                }

                _db.ExecuteNonQuery(
                    @"INSERT INTO PendingUsers (Username,Password,FullName,Phone,Email,Role,Gender,Status)
                      VALUES (@u,@pw,@fn,@ph,@em,'Staff',@g,'Pending')",
                    new SqlParameter[]
                    {
                        new SqlParameter("@u",  txtTenDangNhap.Text.Trim()),
                        new SqlParameter("@pw", HashSHA256(txtMatKhau.Text)),
                        new SqlParameter("@fn", txtHoTen.Text.Trim()),
                        new SqlParameter("@ph", Nul(txtSDT.Text)),
                        new SqlParameter("@em", Nul(txtEmail.Text)),
                        new SqlParameter("@g",  cboGioiTinh.SelectedItem?.ToString() ?? "Nam"),
                    });

                MessageBox.Show(
                    "Đăng ký thành công!\n\nTài khoản «" + txtTenDangNhap.Text.Trim() + "» đang chờ Admin phê duyệt.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (SqlException ex) { SetError("Lỗi CSDL: " + ex.Message); }
            catch (Exception ex) { SetError("Lỗi: " + ex.Message); }
        }

        // ── Validation ───────────────────────────────────────────────
        private new bool Validate()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) return SetError("Vui lòng nhập họ và tên!");
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text)) return SetError("Vui lòng nhập tên đăng nhập!");
            if (txtTenDangNhap.Text.Trim().Length < 3) return SetError("Tên đăng nhập tối thiểu 3 ký tự!");
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text)) return SetError("Vui lòng nhập mật khẩu!");
            if (txtMatKhau.Text.Length < 6) return SetError("Mật khẩu tối thiểu 6 ký tự!");
            if (txtMatKhau.Text != txtXacNhan.Text) return SetError("Mật khẩu xác nhận không khớp!");
            if (!string.IsNullOrWhiteSpace(txtSDT.Text) && txtSDT.Text.Trim().Length < 9)
                return SetError("Số điện thoại không hợp lệ!");
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !txtEmail.Text.Contains("@"))
                return SetError("Email không hợp lệ!");
            return true;
        }

        private bool SetError(string msg) { lblError.Text = "⚠  " + msg; return false; }

        private void ClearForm()
        {
            txtHoTen.Clear(); txtTenDangNhap.Clear();
            txtMatKhau.Clear(); txtXacNhan.Clear();
            txtEmail.Clear(); txtSDT.Clear();
            dtpNgaySinh.Value = new DateTime(2000, 1, 1);
            cboGioiTinh.SelectedIndex = 0;
            lblError.Text = "";
            txtHoTen.Focus();
        }

        private static object Nul(string s) =>
            string.IsNullOrWhiteSpace(s) ? (object)System.DBNull.Value : s.Trim();

        private static string HashSHA256(string input)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder();
                foreach (var b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}