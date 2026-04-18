using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using CNPN_NHOM13_GymManagement.Database;
using System.Runtime.InteropServices;

namespace CNPN_NHOM13_GymManagement
{
    public partial class QuanLyPTForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;
        private static readonly Color CGreen = Color.FromArgb(34, 197, 94);
        private static readonly Color CAmber = Color.FromArgb(245, 158, 11);
        private static readonly Color CRed = Color.FromArgb(239, 68, 68);
        private static readonly Color CPurple = Color.FromArgb(139, 92, 246);
        private static readonly Color CBlue = Color.FromArgb(59, 130, 246);
        private static readonly Color CDark = Color.FromArgb(15, 23, 42);

        private readonly DbHelper _db = new DbHelper();

        public QuanLyPTForm()
        {
            InitializeComponent();
            SendMessage(txtSearch.Handle, EM_SETCUEBANNER, 0, "🔍 Tìm tên, chuyên môn...");
            AttachEvents();
            LoadData();
        }

        // ── Events ───────────────────────────────────────────────────
        private void AttachEvents()
        {
            btnSearch.Click += (s, e) => LoadData(txtSearch.Text);
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) LoadData(txtSearch.Text); };
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadData(); };
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnToggleStatus.Click += BtnToggle_Click;

            dgvPT.CellFormatting += (s, e) =>
            {
                if (dgvPT.Columns[e.ColumnIndex].HeaderText == "Trạng thái" && e.Value != null)
                {
                    if (e.Value.ToString() == "Active")
                    { e.CellStyle.ForeColor = CGreen; e.CellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); }
                    else
                        e.CellStyle.ForeColor = CRed;
                }
                if (dgvPT.Columns[e.ColumnIndex].HeaderText == "Level" && e.Value != null)
                {
                    switch (e.Value.ToString())
                    {
                        case "Elite": e.CellStyle.ForeColor = CPurple; e.CellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); break;
                        case "Pro": e.CellStyle.ForeColor = CBlue; break;
                        case "Basic": e.CellStyle.ForeColor = CAmber; break;
                    }
                }
            };
        }

        // ── Load ─────────────────────────────────────────────────────
        private void LoadData(string search = "")
        {
            try
            {
                string sql = @"
                    SELECT t.TrainerId     AS [ID],
                           t.Name          AS [Họ và tên],
                           t.Phone         AS [Số ĐT],
                           t.Email         AS [Email],
                           t.Specialty     AS [Chuyên môn],
                           t.Experience    AS [KN (năm)],
                           tl.Name         AS [Level],
                           t.SalaryPercent AS [Hoa hồng %],
                           t.Status        AS [Trạng thái],
                           (SELECT COUNT(*) FROM Memberships m
                            WHERE m.TrainerId = t.TrainerId
                              AND m.Status='Active' AND m.EndDate > GETDATE()) AS [HV hiện tại]
                    FROM Trainers t
                    LEFT JOIN TrainerLevels tl ON t.LevelId = tl.LevelId";

                SqlParameter[] p = null;
                if (!string.IsNullOrWhiteSpace(search))
                {
                    sql += " WHERE t.Name LIKE @s OR t.Specialty LIKE @s OR t.Email LIKE @s OR t.Phone LIKE @s";
                    p = new[] { new SqlParameter("@s", "%" + search + "%") };
                }
                sql += " ORDER BY t.TrainerId DESC";

                dgvPT.DataSource = _db.ExecuteQuery(sql, p);
                SetStatus("Hiển thị " + dgvPT.Rows.Count + " huấn luyện viên.");
                UpdateStats();
            }
            catch (Exception ex) { SetStatus("Lỗi: " + ex.Message); }
        }

        private void UpdateStats()
        {
            try
            {
                lblStatTotalVal.Text = _db.ExecuteScalar("SELECT COUNT(*) FROM Trainers")?.ToString() ?? "0";
                lblStatActiveVal.Text = _db.ExecuteScalar("SELECT COUNT(*) FROM Trainers WHERE Status='Active'")?.ToString() ?? "0";
                lblStatEliteVal.Text = _db.ExecuteScalar(
                    "SELECT COUNT(*) FROM Trainers t JOIN TrainerLevels tl ON t.LevelId=tl.LevelId WHERE tl.Name='Elite'")?.ToString() ?? "0";
                lblStatClientsVal.Text = _db.ExecuteScalar(
                    "SELECT COUNT(*) FROM Memberships WHERE TrainerId IS NOT NULL AND Status='Active' AND EndDate>GETDATE()")?.ToString() ?? "0";
            }
            catch { }
        }

        // ── CRUD ─────────────────────────────────────────────────────
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new PTEditDialog("Thêm huấn luyện viên", null, _db))
                if (dlg.ShowDialog() == DialogResult.OK) { LoadData(); SetStatus("Đã thêm PT mới."); }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();
            if (id < 0) return;
            var dt = _db.ExecuteQuery("SELECT * FROM Trainers WHERE TrainerId=@id", new[] { new SqlParameter("@id", id) });
            if (dt.Rows.Count == 0) return;
            using (var dlg = new PTEditDialog("Sửa huấn luyện viên", dt.Rows[0], _db))
                if (dlg.ShowDialog() == DialogResult.OK) { LoadData(); SetStatus("Đã cập nhật."); }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();
            if (id < 0) return;

            int active = Convert.ToInt32(_db.ExecuteScalar(
                "SELECT COUNT(*) FROM Memberships WHERE TrainerId=@id AND Status='Active' AND EndDate>GETDATE()",
                new[] { new SqlParameter("@id", id) }));
            if (active > 0)
            {
                MessageBox.Show("PT đang có " + active + " học viên hoạt động. Hãy đổi trạng thái thay vì xóa.", "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Xác nhận xóa?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                _db.ExecuteNonQuery("DELETE FROM Trainers WHERE TrainerId=@id", new[] { new SqlParameter("@id", id) });
                LoadData(); SetStatus("Đã xóa.");
            }
            catch (Exception ex) { SetStatus("Lỗi: " + ex.Message); }
        }

        private void BtnToggle_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();
            if (id < 0) return;
            var dt = _db.ExecuteQuery("SELECT Status,Name FROM Trainers WHERE TrainerId=@id", new[] { new SqlParameter("@id", id) });
            if (dt.Rows.Count == 0) return;
            string cur = dt.Rows[0]["Status"].ToString();
            string next = cur == "Active" ? "Inactive" : "Active";
            string name = dt.Rows[0]["Name"].ToString();
            if (MessageBox.Show("Đổi «" + name + "» → " + next + "?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                _db.ExecuteNonQuery("UPDATE Trainers SET Status=@s WHERE TrainerId=@id",
                    new SqlParameter[] { new SqlParameter("@s", next), new SqlParameter("@id", id) });
                LoadData(); SetStatus("Đã đổi trạng thái.");
            }
            catch (Exception ex) { SetStatus("Lỗi: " + ex.Message); }
        }

        // ── Helpers ───────────────────────────────────────────────────
        private int GetSelectedId()
        {
            if (dgvPT.SelectedRows.Count == 0)
            { MessageBox.Show("Vui lòng chọn một PT.", "Chưa chọn"); return -1; }
            return Convert.ToInt32(dgvPT.SelectedRows[0].Cells["ID"].Value);
        }

        private void SetStatus(string msg) =>
            lblStatus.Text = "  " + DateTime.Now.ToString("HH:mm:ss") + "  —  " + msg;
    }

    // ════════════════════════════════════════════════════════════════
    //  PT ADD / EDIT DIALOG  (standalone, no Designer needed)
    // ════════════════════════════════════════════════════════════════
    internal class PTEditDialog : Form
    {
        private readonly DbHelper _db;
        private readonly DataRow _row;
        private int _ptId = -1;

        private TextBox txtName, txtPhone, txtEmail, txtSpecialty;
        private NumericUpDown nudExp, nudSal;
        private ComboBox cboLevel, cboStatus;
        private Label lblError;

        private static readonly Color CBlue = Color.FromArgb(59, 130, 246);
        private static readonly Color CRed = Color.FromArgb(239, 68, 68);
        private static readonly Color CDark = Color.FromArgb(15, 23, 42);
        private static readonly Color CLightBg = Color.FromArgb(248, 250, 252);
        private static readonly Color CMuted = Color.FromArgb(100, 116, 139);

        public PTEditDialog(string title, DataRow row, DbHelper db)
        {
            _db = db;
            _row = row;
            BuildUI(title);
            if (row != null) PopulateFields();
        }

        private void BuildUI(string title)
        {
            this.Text = title;
            this.Size = new Size(530, 560);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = CLightBg;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var fInput = new Font("Segoe UI", 10.5F);
            var fLbl = new Font("Segoe UI", 8.5F, FontStyle.Bold);

            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 56, BackColor = CDark, Padding = new Padding(20, 10, 10, 10) };
            pnlTop.Controls.Add(new Label { Text = title, Font = new Font("Segoe UI", 16F, FontStyle.Bold), ForeColor = Color.White, Dock = DockStyle.Fill });

            var pnlContent = new Panel { Dock = DockStyle.Fill, Padding = new Padding(25, 12, 25, 8), BackColor = CLightBg, AutoScroll = true };

            int y = 8;
            AddField(pnlContent, fLbl, fInput, "HỌ VÀ TÊN *", ref y, out txtName);
            AddField(pnlContent, fLbl, fInput, "SỐ ĐIỆN THOẠI", ref y, out txtPhone);
            AddField(pnlContent, fLbl, fInput, "EMAIL", ref y, out txtEmail);
            AddField(pnlContent, fLbl, fInput, "CHUYÊN MÔN (VD: Giảm cân)", ref y, out txtSpecialty);

            pnlContent.Controls.Add(MakeLabel("KINH NGHIỆM (NĂM)", fLbl, y));
            nudExp = new NumericUpDown { Font = fInput, Minimum = 0, Maximum = 50, Location = new Point(0, y + 22), Size = new Size(480, 34) };
            pnlContent.Controls.Add(nudExp);
            y += 62;

            pnlContent.Controls.Add(MakeLabel("HOA HỒNG (%)", fLbl, y));
            nudSal = new NumericUpDown { Font = fInput, Minimum = 0, Maximum = 100, Location = new Point(0, y + 22), Size = new Size(480, 34) };
            pnlContent.Controls.Add(nudSal);
            y += 62;

            pnlContent.Controls.Add(MakeLabel("TRẠNG THÁI", fLbl, y));
            cboStatus = new ComboBox { Font = fInput, DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(0, y + 22), Size = new Size(480, 34) };
            cboStatus.Items.AddRange(new object[] { "Active", "Inactive" });
            cboStatus.SelectedIndex = 0;
            pnlContent.Controls.Add(cboStatus);
            y += 62;

            pnlContent.Controls.Add(MakeLabel("CẤP ĐỘ (Level)", fLbl, y));
            cboLevel = new ComboBox { Font = fInput, DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(0, y + 22), Size = new Size(480, 34) };
            try
            {
                var dt = _db.ExecuteQuery("SELECT LevelId,Name FROM TrainerLevels ORDER BY LevelId");
                cboLevel.DataSource = dt; cboLevel.DisplayMember = "Name"; cboLevel.ValueMember = "LevelId";
            }
            catch { cboLevel.Items.Add("(lỗi)"); }
            pnlContent.Controls.Add(cboLevel);
            y += 62;

            lblError = new Label { Location = new Point(0, y), Size = new Size(480, 20), Font = new Font("Segoe UI", 9F), ForeColor = CRed };
            pnlContent.Controls.Add(lblError);

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
            parent.Controls.Add(MakeLabel(labelText, fLbl, y));
            txt = new TextBox { Font = fInput, BorderStyle = BorderStyle.FixedSingle, Location = new Point(0, y + 22), Size = new Size(480, 34) };
            parent.Controls.Add(txt);
            y += 62;
        }

        private static Label MakeLabel(string text, Font font, int y) =>
            new Label { Text = text, Font = font, ForeColor = Color.FromArgb(100, 116, 139), Location = new Point(0, y), Size = new Size(480, 20) };

        private void PopulateFields()
        {
            _ptId = Convert.ToInt32(_row["TrainerId"]);
            txtName.Text = _row["Name"].ToString();
            txtPhone.Text = _row["Phone"]?.ToString() ?? "";
            txtEmail.Text = _row["Email"]?.ToString() ?? "";
            txtSpecialty.Text = _row["Specialty"]?.ToString() ?? "";
            nudExp.Value = _row["Experience"] != DBNull.Value ? Convert.ToDecimal(_row["Experience"]) : 0;
            nudSal.Value = _row["SalaryPercent"] != DBNull.Value ? Convert.ToDecimal(_row["SalaryPercent"]) : 0;
            cboStatus.SelectedItem = _row["Status"]?.ToString() ?? "Active";
            if (_row["LevelId"] != DBNull.Value)
            {
                cboLevel.SelectedValue = _row["LevelId"];
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (string.IsNullOrWhiteSpace(txtName.Text)) { lblError.Text = "⚠  Nhập tên huấn luyện viên!"; return; }
            try
            {
                int? lvId = cboLevel.SelectedValue is int lv ? (int?)lv : null;
                var p = new SqlParameter[] {
                    new SqlParameter("@n",  txtName.Text.Trim()),
                    new SqlParameter("@ph", Nul(txtPhone.Text)),
                    new SqlParameter("@em", Nul(txtEmail.Text)),
                    new SqlParameter("@sp", Nul(txtSpecialty.Text)),
                    new SqlParameter("@ex", (int)nudExp.Value),
                    new SqlParameter("@sl", (int)nudSal.Value),
                    new SqlParameter("@st", cboStatus.SelectedItem?.ToString() ?? "Active"),
                    new SqlParameter("@lv", (object)lvId ?? DBNull.Value),
                };
                if (_row == null)
                    _db.ExecuteNonQuery("INSERT INTO Trainers (Name,Phone,Email,Specialty,Experience,SalaryPercent,Status,LevelId) VALUES (@n,@ph,@em,@sp,@ex,@sl,@st,@lv)", p);
                else
                {
                    var pl = new System.Collections.Generic.List<SqlParameter>(p);
                    pl.Add(new SqlParameter("@id", _ptId));
                    _db.ExecuteNonQuery("UPDATE Trainers SET Name=@n,Phone=@ph,Email=@em,Specialty=@sp,Experience=@ex,SalaryPercent=@sl,Status=@st,LevelId=@lv WHERE TrainerId=@id", pl.ToArray());
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { lblError.Text = "⚠  " + ex.Message; }
        }

        private static object Nul(string s) => string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s.Trim();
    }
}