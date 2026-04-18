using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CNPN_NHOM13_GymManagement.Database;
using System.Runtime.InteropServices;

namespace CNPN_NHOM13_GymManagement
{
    public partial class QuanLyTaiKhoanForm : Form
    {
        private static readonly Color CGreen = Color.FromArgb(34, 197, 94);
        private static readonly Color CAmber = Color.FromArgb(245, 158, 11);
        private static readonly Color CRed = Color.FromArgb(239, 68, 68);
        private static readonly Color CDark = Color.FromArgb(15, 23, 42);
        private static readonly Color CPanel = Color.FromArgb(30, 41, 59);
        private static readonly Color CWhite = Color.White;
        private static readonly Color CLightBg = Color.FromArgb(248, 250, 252);
        private static readonly Color CBorder = Color.FromArgb(226, 232, 240);

        private readonly DbHelper _db = new DbHelper();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;
        public QuanLyTaiKhoanForm()
        {
            InitializeComponent();
            SendMessage(txtSearch.Handle, EM_SETCUEBANNER, 0, "🔍 Tìm theo tên, username...");
            AttachEvents();
            LoadAll();
        }
        // ── Events ───────────────────────────────────────────────────
        private void AttachEvents()
        {
            btnSearch.Click += (s, e) => SearchCurrent();
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) SearchCurrent(); };
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadAll(); };
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnApprove.Click += BtnApprove_Click;

            tabMain.SelectedIndexChanged += (s, e) =>
            {
                bool onPending = tabMain.SelectedTab == tabPending;
                btnApprove.Enabled = onPending;
                btnApprove.BackColor = onPending
                    ? Color.FromArgb(34, 197, 94)
                    : Color.FromArgb(134, 239, 172);
            };

            dgvUsers.CellFormatting += (s, e) =>
            {
                if (dgvUsers.Columns[e.ColumnIndex].HeaderText == "Trạng thái" && e.Value != null)
                {
                    if (e.Value.ToString() == "Active")
                    {
                        e.CellStyle.ForeColor = CGreen;
                        e.CellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    }
                    else if (e.Value.ToString() == "Inactive")
                        e.CellStyle.ForeColor = CRed;
                }
            };
        }

        // ── Load ─────────────────────────────────────────────────────
        private void LoadAll()
        {
            LoadUsers();
            LoadPending();
            UpdateStats();
        }

        private void LoadUsers(string search = "")
        {
            try
            {
                string sql = @"
                    SELECT u.UserId      AS [ID],
                           u.Username    AS [Tên đăng nhập],
                           u.FullName    AS [Họ và tên],
                           u.Phone       AS [Số ĐT],
                           u.Email       AS [Email],
                           u.Gender      AS [Giới tính],
                           u.Role        AS [Vai trò],
                           sl.Name       AS [Cấp độ],
                           u.Experience  AS [KN (năm)],
                           u.Status      AS [Trạng thái],
                           CONVERT(NVARCHAR,u.CreatedAt,103) AS [Ngày tạo]
                    FROM   Users u
                    LEFT JOIN StaffLevels sl ON u.LevelId = sl.LevelId";

                SqlParameter[] p = null;
                if (!string.IsNullOrWhiteSpace(search))
                {
                    sql += " WHERE u.FullName LIKE @s OR u.Username LIKE @s OR u.Phone LIKE @s";
                    p = new[] { new SqlParameter("@s", "%" + search + "%") };
                }
                sql += " ORDER BY u.UserId DESC";

                dgvUsers.DataSource = _db.ExecuteQuery(sql, p);
                SetStatus("Hiển thị " + dgvUsers.Rows.Count + " tài khoản.");
            }
            catch (Exception ex) { SetStatus("Lỗi: " + ex.Message); }
        }

        private void LoadPending(string search = "")
        {
            try
            {
                string sql = @"
                    SELECT Id         AS [ID],
                           Username   AS [Tên đăng nhập],
                           FullName   AS [Họ và tên],
                           Phone      AS [Số ĐT],
                           Email      AS [Email],
                           Gender     AS [Giới tính],
                           Role       AS [Vai trò],
                           Status     AS [Trạng thái]
                    FROM   PendingUsers";

                SqlParameter[] p = null;
                if (!string.IsNullOrWhiteSpace(search))
                {
                    sql += " WHERE FullName LIKE @s OR Username LIKE @s";
                    p = new[] { new SqlParameter("@s", "%" + search + "%") };
                }
                sql += " ORDER BY Id DESC";

                dgvPending.DataSource = _db.ExecuteQuery(sql, p);
            }
            catch (Exception ex) { SetStatus("Lỗi pending: " + ex.Message); }
        }

        private void UpdateStats()
        {
            try
            {
                lblStatTotalVal.Text = _db.ExecuteScalar("SELECT COUNT(*) FROM Users")?.ToString() ?? "0";
                lblStatActiveVal.Text = _db.ExecuteScalar("SELECT COUNT(*) FROM Users WHERE Status='Active'")?.ToString() ?? "0";
                lblStatPendingVal.Text = _db.ExecuteScalar("SELECT COUNT(*) FROM PendingUsers WHERE Status='Pending'")?.ToString() ?? "0";
            }
            catch { }
        }

        private void SearchCurrent()
        {
            if (tabMain.SelectedTab == tabAll) LoadUsers(txtSearch.Text);
            else LoadPending(txtSearch.Text);
        }

        // ── CRUD ─────────────────────────────────────────────────────
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new UserEditDialog("Thêm tài khoản", null, _db))
                if (dlg.ShowDialog() == DialogResult.OK) { LoadAll(); SetStatus("Đã thêm tài khoản."); }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab != tabAll) { MessageBox.Show("Chọn tab 'Tất cả tài khoản' để sửa."); return; }
            int id = GetSelectedId(dgvUsers);
            if (id < 0) return;

            var dt = _db.ExecuteQuery("SELECT * FROM Users WHERE UserId=@id",
                new[] { new SqlParameter("@id", id) });
            if (dt.Rows.Count == 0) return;

            using (var dlg = new UserEditDialog("Sửa tài khoản", dt.Rows[0], _db))
                if (dlg.ShowDialog() == DialogResult.OK) { LoadAll(); SetStatus("Đã cập nhật."); }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            bool isPending = tabMain.SelectedTab == tabPending;
            var dgv = isPending ? dgvPending : dgvUsers;
            int id = GetSelectedId(dgv);
            if (id < 0) return;

            if (MessageBox.Show("Xác nhận xóa?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                if (isPending)
                    _db.ExecuteNonQuery("DELETE FROM PendingUsers WHERE Id=@id", new[] { new SqlParameter("@id", id) });
                else
                    _db.ExecuteNonQuery("DELETE FROM Users WHERE UserId=@id", new[] { new SqlParameter("@id", id) });
                LoadAll(); SetStatus("Đã xóa.");
            }
            catch (Exception ex) { SetStatus("Lỗi: " + ex.Message); }
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId(dgvPending);
            if (id < 0) return;
            if (MessageBox.Show("Phê duyệt tài khoản này?", "Duyệt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                _db.ExecuteNonQuery(
                    @"INSERT INTO Users (Username,Password,FullName,Phone,Email,Role,Gender,Status,Experience,LevelId)
                      SELECT Username,Password,FullName,Phone,Email,
                             ISNULL(Role,'Staff'),ISNULL(Gender,'Nam'),'Active',
                             ISNULL(Experience,0),ISNULL(LevelId,1)
                      FROM PendingUsers WHERE Id=@id",
                    new[] { new SqlParameter("@id", id) });
                _db.ExecuteNonQuery("DELETE FROM PendingUsers WHERE Id=@id", new[] { new SqlParameter("@id", id) });
                LoadAll(); SetStatus("Đã phê duyệt.");
            }
            catch (Exception ex) { SetStatus("Lỗi duyệt: " + ex.Message); }
        }

        // ── Helpers ───────────────────────────────────────────────────
        private int GetSelectedId(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            { MessageBox.Show("Vui lòng chọn một dòng.", "Chưa chọn"); return -1; }
            return Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
        }

        private void SetStatus(string msg) =>
            lblStatus.Text = "  " + DateTime.Now.ToString("HH:mm:ss") + "  —  " + msg;
    }

    // ════════════════════════════════════════════════════════════════
    //  ADD / EDIT DIALOG  (standalone Form, không cần Designer)
    // ════════════════════════════════════════════════════════════════
    internal class UserEditDialog : Form
    {
        private readonly DbHelper _db;
        private readonly DataRow _row;
        private int _userId = -1;

        private TextBox txtUsername, txtFullName, txtPhone, txtEmail, txtPassword;
        private ComboBox cboRole, cboGender, cboLevel, cboStatus;
        private NumericUpDown nudExp;
        private Label lblError;

        private static readonly Color CBlue = Color.FromArgb(59, 130, 246);
        private static readonly Color CRed = Color.FromArgb(239, 68, 68);
        private static readonly Color CDark = Color.FromArgb(15, 23, 42);
        private static readonly Color CLightBg = Color.FromArgb(248, 250, 252);
        private static readonly Color CMuted = Color.FromArgb(100, 116, 139);

        public UserEditDialog(string title, DataRow row, DbHelper db)
        {
            _db = db;
            _row = row;
            BuildUI(title);
            if (row != null) PopulateFields();
        }

        private void BuildUI(string title)
        {
            this.Text = title;
            this.Size = new Size(540, 570);
            this.MinimumSize = new Size(480, 530);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = CLightBg;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var fInput = new Font("Segoe UI", 10.5F);
            var fLbl = new Font("Segoe UI", 8.5F, FontStyle.Bold);

            // Header strip
            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 56, BackColor = CDark, Padding = new Padding(20, 10, 10, 10) };
            pnlTop.Controls.Add(new Label { Text = title, Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = Color.White, Dock = DockStyle.Fill });

            // Scrollable content area
            var pnlContent = new Panel { Dock = DockStyle.Fill, Padding = new Padding(25, 12, 25, 8), BackColor = CLightBg, AutoScroll = true };

            int y = 8;
            AddField(pnlContent, fLbl, fInput, "TÊN ĐĂNG NHẬP *", ref y, out txtUsername);
            AddField(pnlContent, fLbl, fInput, "HỌ VÀ TÊN *", ref y, out txtFullName);
            AddField(pnlContent, fLbl, fInput, "MẬT KHẨU" + (_row == null ? " *" : " (để trống = giữ nguyên)"), ref y, out txtPassword);
            txtPassword.UseSystemPasswordChar = true;
            AddField(pnlContent, fLbl, fInput, "SỐ ĐIỆN THOẠI", ref y, out txtPhone);
            AddField(pnlContent, fLbl, fInput, "EMAIL", ref y, out txtEmail);

            // Giới tính
            pnlContent.Controls.Add(MakeLabel("GIỚI TÍNH", fLbl, CMuted, y));
            cboGender = new ComboBox { Font = fInput, DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(0, y + 22), Size = new Size(490, 34) };
            cboGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGender.SelectedIndex = 0;
            pnlContent.Controls.Add(cboGender);
            y += 62;

            // Vai trò
            pnlContent.Controls.Add(MakeLabel("VAI TRÒ *", fLbl, CMuted, y));
            cboRole = new ComboBox { Font = fInput, DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(0, y + 22), Size = new Size(490, 34) };
            cboRole.Items.AddRange(new object[] { "Admin", "Staff" });
            cboRole.SelectedIndex = 1;
            pnlContent.Controls.Add(cboRole);
            y += 62;

            // Trạng thái
            pnlContent.Controls.Add(MakeLabel("TRẠNG THÁI", fLbl, CMuted, y));
            cboStatus = new ComboBox { Font = fInput, DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(0, y + 22), Size = new Size(490, 34) };
            cboStatus.Items.AddRange(new object[] { "Active", "Inactive" });
            cboStatus.SelectedIndex = 0;
            pnlContent.Controls.Add(cboStatus);
            y += 62;

            // Kinh nghiệm
            pnlContent.Controls.Add(MakeLabel("KINH NGHIỆM (NĂM)", fLbl, CMuted, y));
            nudExp = new NumericUpDown { Font = fInput, Minimum = 0, Maximum = 50, Location = new Point(0, y + 22), Size = new Size(490, 34) };
            pnlContent.Controls.Add(nudExp);
            y += 62;

            // Level
            pnlContent.Controls.Add(MakeLabel("CẤP ĐỘ (Level)", fLbl, CMuted, y));
            cboLevel = new ComboBox { Font = fInput, DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(0, y + 22), Size = new Size(490, 34) };
            try
            {
                var dt = _db.ExecuteQuery("SELECT LevelId, Name FROM StaffLevels ORDER BY LevelId");
                cboLevel.DataSource = dt;
                cboLevel.DisplayMember = "Name";
                cboLevel.ValueMember = "LevelId";
            }
            catch { cboLevel.Items.Add("(lỗi)"); }
            pnlContent.Controls.Add(cboLevel);
            y += 62;

            lblError = new Label { Location = new Point(0, y), Size = new Size(490, 20), Font = new Font("Segoe UI", 9F), ForeColor = CRed };
            pnlContent.Controls.Add(lblError);

            // Buttons
            var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.White, Padding = new Padding(20, 12, 20, 12) };
            var btnCancel = new Button { Text = "Hủy", Font = new Font("Segoe UI", 10F, FontStyle.Bold), BackColor = CRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Dock = DockStyle.Right, Width = 100, Cursor = Cursors.Hand };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            var btnSave = new Button { Text = "💾  Lưu", Font = new Font("Segoe UI", 10F, FontStyle.Bold), BackColor = CBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Dock = DockStyle.Right, Width = 120, Cursor = Cursors.Hand };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Controls.Add(btnSave);

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlTop);
        }

        private void AddField(Panel parent, Font fLbl, Font fInput, string labelText, ref int y, out TextBox txt)
        {
            parent.Controls.Add(MakeLabel(labelText, fLbl, Color.FromArgb(100, 116, 139), y));
            txt = new TextBox { Font = fInput, BorderStyle = BorderStyle.FixedSingle, Location = new Point(0, y + 22), Size = new Size(490, 34) };
            parent.Controls.Add(txt);
            y += 62;
        }

        private static Label MakeLabel(string text, Font font, Color fore, int y)
        {
            return new Label { Text = text, Font = font, ForeColor = fore, Location = new Point(0, y), Size = new Size(490, 20) };
        }

        private void PopulateFields()
        {
            _userId = Convert.ToInt32(_row["UserId"]);
            txtUsername.Text = _row["Username"].ToString();
            txtUsername.ReadOnly = true;
            txtFullName.Text = _row["FullName"].ToString();
            txtPhone.Text = _row["Phone"]?.ToString() ?? "";
            txtEmail.Text = _row["Email"]?.ToString() ?? "";
            cboGender.SelectedItem = _row["Gender"]?.ToString() ?? "Nam";
            cboRole.SelectedItem = _row["Role"]?.ToString() ?? "Staff";
            cboStatus.SelectedItem = _row["Status"]?.ToString() ?? "Active";
            nudExp.Value = _row["Experience"] != DBNull.Value ? Convert.ToDecimal(_row["Experience"]) : 0;
            if (_row["LevelId"] != DBNull.Value)
            {
                cboLevel.SelectedValue = Convert.ToInt32(_row["LevelId"]);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(txtFullName.Text)) { lblError.Text = "⚠  Nhập họ và tên!"; return; }
            if (_row == null && string.IsNullOrWhiteSpace(txtUsername.Text)) { lblError.Text = "⚠  Nhập tên đăng nhập!"; return; }
            if (_row == null && string.IsNullOrWhiteSpace(txtPassword.Text)) { lblError.Text = "⚠  Nhập mật khẩu!"; return; }
            try
            {
                int? lvId = cboLevel.SelectedValue is int lv ? (int?)lv : null;
                if (_row == null)
                {
                    _db.ExecuteNonQuery(
                        "INSERT INTO Users (Username,Password,FullName,Phone,Email,Gender,Role,Status,Experience,LevelId) VALUES (@u,@pw,@fn,@ph,@em,@g,@r,@st,@exp,@lv)",
                        new SqlParameter[] {
                            new SqlParameter("@u",  txtUsername.Text.Trim()),
                            new SqlParameter("@pw", Hash(txtPassword.Text)),
                            new SqlParameter("@fn", txtFullName.Text.Trim()),
                            new SqlParameter("@ph", Nul(txtPhone.Text)),
                            new SqlParameter("@em", Nul(txtEmail.Text)),
                            new SqlParameter("@g",  cboGender.SelectedItem),
                            new SqlParameter("@r",  cboRole.SelectedItem),
                            new SqlParameter("@st", cboStatus.SelectedItem),
                            new SqlParameter("@exp",(int)nudExp.Value),
                            new SqlParameter("@lv", (object)lvId ?? DBNull.Value),
                        });
                }
                else
                {
                    string pwPart = "";
                    var pl = new System.Collections.Generic.List<SqlParameter> {
                        new SqlParameter("@fn", txtFullName.Text.Trim()),
                        new SqlParameter("@ph", Nul(txtPhone.Text)),
                        new SqlParameter("@em", Nul(txtEmail.Text)),
                        new SqlParameter("@g",  cboGender.SelectedItem),
                        new SqlParameter("@r",  cboRole.SelectedItem),
                        new SqlParameter("@st", cboStatus.SelectedItem),
                        new SqlParameter("@exp",(int)nudExp.Value),
                        new SqlParameter("@lv", (object)lvId ?? DBNull.Value),
                        new SqlParameter("@id", _userId),
                    };
                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    { pwPart = ",Password=@pw"; pl.Insert(0, new SqlParameter("@pw", Hash(txtPassword.Text))); }
                    _db.ExecuteNonQuery(
                        "UPDATE Users SET FullName=@fn,Phone=@ph,Email=@em,Gender=@g,Role=@r,Status=@st,Experience=@exp,LevelId=@lv" + pwPart + " WHERE UserId=@id",
                        pl.ToArray());
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { lblError.Text = "⚠  " + ex.Message; }
        }

        private static object Nul(string s) => string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s.Trim();
        private static string Hash(string s)
        {
            using (var sha = SHA256.Create())
            {
                var b = sha.ComputeHash(Encoding.UTF8.GetBytes(s));
                var sb = new StringBuilder();
                foreach (var x in b) sb.Append(x.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
