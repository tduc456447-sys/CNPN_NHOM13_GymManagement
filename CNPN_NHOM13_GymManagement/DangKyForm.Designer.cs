namespace CNPN_NHOM13_GymManagement
{
    partial class DangKyForm
    {
        private System.ComponentModel.IContainer components = null;

        // Left panel
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblGymIcon;
        private System.Windows.Forms.Label lblGymName;
        private System.Windows.Forms.Label lblSlogan;

        // Right panel
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlRightTop;
        private System.Windows.Forms.Panel pnlRightBottom;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;

        // Table layout
        private System.Windows.Forms.TableLayoutPanel tlpForm;

        // Cell wrapper panels
        private System.Windows.Forms.Panel pnlCellHoTen;
        private System.Windows.Forms.Panel pnlCellUsername;
        private System.Windows.Forms.Panel pnlCellMatKhau;
        private System.Windows.Forms.Panel pnlCellXacNhan;
        private System.Windows.Forms.Panel pnlCellEmail;
        private System.Windows.Forms.Panel pnlCellSDT;
        private System.Windows.Forms.Panel pnlCellNgaySinh;
        private System.Windows.Forms.Panel pnlCellGioiTinh;

        // Field labels
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.Label lblXacNhan;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.Label lblGioiTinh;

        // Inputs
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.TextBox txtXacNhan;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;

        // Bottom bar
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Button btnHuy;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Colours ──────────────────────────────────────────────
            System.Drawing.Color cDark = System.Drawing.Color.FromArgb(15, 23, 42);
            System.Drawing.Color cBlue = System.Drawing.Color.FromArgb(59, 130, 246);
            System.Drawing.Color cRed = System.Drawing.Color.FromArgb(239, 68, 68);
            System.Drawing.Color cLightBg = System.Drawing.Color.FromArgb(248, 250, 252);
            System.Drawing.Color cWhite = System.Drawing.Color.White;
            System.Drawing.Color cSlate = System.Drawing.Color.FromArgb(148, 163, 184);
            System.Drawing.Color cMuted = System.Drawing.Color.FromArgb(100, 116, 139);

            // ── Fonts ─────────────────────────────────────────────────
            System.Drawing.Font fInput = new System.Drawing.Font("Segoe UI", 11F);
            System.Drawing.Font fLabel = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            System.Drawing.Font fBtn = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

            // ── Instantiate ───────────────────────────────────────────
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblGymIcon = new System.Windows.Forms.Label();
            this.lblGymName = new System.Windows.Forms.Label();
            this.lblSlogan = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlRightTop = new System.Windows.Forms.Panel();
            this.pnlRightBottom = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.tlpForm = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCellHoTen = new System.Windows.Forms.Panel();
            this.pnlCellUsername = new System.Windows.Forms.Panel();
            this.pnlCellMatKhau = new System.Windows.Forms.Panel();
            this.pnlCellXacNhan = new System.Windows.Forms.Panel();
            this.pnlCellEmail = new System.Windows.Forms.Panel();
            this.pnlCellSDT = new System.Windows.Forms.Panel();
            this.pnlCellNgaySinh = new System.Windows.Forms.Panel();
            this.pnlCellGioiTinh = new System.Windows.Forms.Panel();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.lblXacNhan = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.txtXacNhan = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();

            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlRightTop.SuspendLayout();
            this.pnlRightBottom.SuspendLayout();
            this.tlpForm.SuspendLayout();
            this.pnlCellHoTen.SuspendLayout();
            this.pnlCellUsername.SuspendLayout();
            this.pnlCellMatKhau.SuspendLayout();
            this.pnlCellXacNhan.SuspendLayout();
            this.pnlCellEmail.SuspendLayout();
            this.pnlCellSDT.SuspendLayout();
            this.pnlCellNgaySinh.SuspendLayout();
            this.pnlCellGioiTinh.SuspendLayout();
            this.SuspendLayout();

            // ═════════════════ LEFT PANEL ════════════════════════════
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Width = 300;
            this.pnlLeft.BackColor = cDark;
            this.pnlLeft.Controls.Add(this.lblSlogan);
            this.pnlLeft.Controls.Add(this.lblGymName);
            this.pnlLeft.Controls.Add(this.lblGymIcon);

            this.lblGymIcon.Text = "💪";
            this.lblGymIcon.Font = new System.Drawing.Font("Segoe UI", 48F);
            this.lblGymIcon.ForeColor = cWhite;
            this.lblGymIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGymIcon.Location = new System.Drawing.Point(0, 160);
            this.lblGymIcon.Size = new System.Drawing.Size(300, 80);

            this.lblGymName.Text = "GYM MANAGER";
            this.lblGymName.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblGymName.ForeColor = cWhite;
            this.lblGymName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGymName.Location = new System.Drawing.Point(0, 248);
            this.lblGymName.Size = new System.Drawing.Size(300, 46);

            this.lblSlogan.Text = "Hệ thống quản lý\nphòng tập chuyên nghiệp";
            this.lblSlogan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSlogan.ForeColor = cSlate;
            this.lblSlogan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSlogan.Location = new System.Drawing.Point(20, 302);
            this.lblSlogan.Size = new System.Drawing.Size(260, 60);

            // ═════════════════ RIGHT TOP ══════════════════════════════
            this.pnlRightTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRightTop.Height = 90;
            this.pnlRightTop.BackColor = cWhite;
            this.pnlRightTop.Padding = new System.Windows.Forms.Padding(35, 16, 20, 8);
            this.pnlRightTop.Controls.Add(this.lblSubTitle);
            this.pnlRightTop.Controls.Add(this.lblTitle);

            this.lblTitle.Text = "Tạo tài khoản nhân viên";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = cDark;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Height = 42;

            this.lblSubTitle.Text = "Tài khoản sẽ chờ Admin phê duyệt trước khi được sử dụng.";
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubTitle.ForeColor = cMuted;
            this.lblSubTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSubTitle.Height = 22;

            // ═════════════════ RIGHT BOTTOM ══════════════════════════
            this.pnlRightBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRightBottom.Height = 70;
            this.pnlRightBottom.BackColor = cWhite;
            this.pnlRightBottom.Padding = new System.Windows.Forms.Padding(35, 15, 35, 15);
            this.pnlRightBottom.Controls.Add(this.lblError);
            this.pnlRightBottom.Controls.Add(this.btnHuy);
            this.pnlRightBottom.Controls.Add(this.btnDangKy);

            this.btnDangKy.Text = "✔  ĐĂNG KÝ";
            this.btnDangKy.Font = fBtn;
            this.btnDangKy.BackColor = cBlue;
            this.btnDangKy.ForeColor = cWhite;
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.FlatAppearance.BorderSize = 0;
            this.btnDangKy.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDangKy.Width = 155;
            this.btnDangKy.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnHuy.Text = "✖  HỦY";
            this.btnHuy.Font = fBtn;
            this.btnHuy.BackColor = cRed;
            this.btnHuy.ForeColor = cWhite;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnHuy.Width = 115;
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;

            this.lblError.Text = "";
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblError.ForeColor = cRed;
            this.lblError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ═════════════════ TABLE LAYOUT ══════════════════════════
            this.tlpForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpForm.BackColor = cLightBg;
            this.tlpForm.Padding = new System.Windows.Forms.Padding(35, 18, 35, 8);
            this.tlpForm.ColumnCount = 2;
            this.tlpForm.RowCount = 4;
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            // Unrolled — 4 row styles, NO for loop
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));

            // ═════════════════ LABELS (unrolled) ════════════════════
            this.lblHoTen.Text = "HỌ VÀ TÊN *";
            this.lblHoTen.Font = fLabel;
            this.lblHoTen.ForeColor = cMuted;
            this.lblHoTen.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHoTen.Height = 24;
            this.lblHoTen.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            this.lblTenDangNhap.Text = "TÊN ĐĂNG NHẬP *";
            this.lblTenDangNhap.Font = fLabel;
            this.lblTenDangNhap.ForeColor = cMuted;
            this.lblTenDangNhap.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTenDangNhap.Height = 24;
            this.lblTenDangNhap.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            this.lblMatKhau.Text = "MẬT KHẨU *";
            this.lblMatKhau.Font = fLabel;
            this.lblMatKhau.ForeColor = cMuted;
            this.lblMatKhau.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMatKhau.Height = 24;
            this.lblMatKhau.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            this.lblXacNhan.Text = "XÁC NHẬN MẬT KHẨU *";
            this.lblXacNhan.Font = fLabel;
            this.lblXacNhan.ForeColor = cMuted;
            this.lblXacNhan.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblXacNhan.Height = 24;
            this.lblXacNhan.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            this.lblEmail.Text = "EMAIL";
            this.lblEmail.Font = fLabel;
            this.lblEmail.ForeColor = cMuted;
            this.lblEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEmail.Height = 24;
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            this.lblSDT.Text = "SỐ ĐIỆN THOẠI *";
            this.lblSDT.Font = fLabel;
            this.lblSDT.ForeColor = cMuted;
            this.lblSDT.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSDT.Height = 24;
            this.lblSDT.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            this.lblNgaySinh.Text = "NGÀY SINH";
            this.lblNgaySinh.Font = fLabel;
            this.lblNgaySinh.ForeColor = cMuted;
            this.lblNgaySinh.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNgaySinh.Height = 24;
            this.lblNgaySinh.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            this.lblGioiTinh.Text = "GIỚI TÍNH";
            this.lblGioiTinh.Font = fLabel;
            this.lblGioiTinh.ForeColor = cMuted;
            this.lblGioiTinh.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGioiTinh.Height = 24;
            this.lblGioiTinh.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            // ═════════════════ INPUTS (unrolled) ════════════════════
            this.txtHoTen.Font = fInput;
            this.txtHoTen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoTen.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtHoTen.Height = 38;
            this.txtHoTen.BackColor = cWhite;

            this.txtTenDangNhap.Font = fInput;
            this.txtTenDangNhap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenDangNhap.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTenDangNhap.Height = 38;
            this.txtTenDangNhap.BackColor = cWhite;

            this.txtMatKhau.Font = fInput;
            this.txtMatKhau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMatKhau.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMatKhau.Height = 38;
            this.txtMatKhau.BackColor = cWhite;
            this.txtMatKhau.UseSystemPasswordChar = true;

            this.txtXacNhan.Font = fInput;
            this.txtXacNhan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtXacNhan.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtXacNhan.Height = 38;
            this.txtXacNhan.BackColor = cWhite;
            this.txtXacNhan.UseSystemPasswordChar = true;

            this.txtEmail.Font = fInput;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtEmail.Height = 38;
            this.txtEmail.BackColor = cWhite;

            this.txtSDT.Font = fInput;
            this.txtSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSDT.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSDT.Height = 38;
            this.txtSDT.BackColor = cWhite;

            this.dtpNgaySinh.Font = fInput;
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtpNgaySinh.Height = 38;
            this.dtpNgaySinh.Value = new System.DateTime(2000, 1, 1);

            this.cboGioiTinh.Font = fInput;
            this.cboGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioiTinh.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboGioiTinh.Height = 38;
            this.cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            this.cboGioiTinh.SelectedIndex = 0;

            // ═════════════════ CELL PANELS (unrolled) ════════════════
            this.pnlCellHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCellHoTen.BackColor = cLightBg;
            this.pnlCellHoTen.Padding = new System.Windows.Forms.Padding(0, 6, 14, 6);
            this.pnlCellHoTen.Controls.Add(this.txtHoTen);
            this.pnlCellHoTen.Controls.Add(this.lblHoTen);

            this.pnlCellUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCellUsername.BackColor = cLightBg;
            this.pnlCellUsername.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.pnlCellUsername.Controls.Add(this.txtTenDangNhap);
            this.pnlCellUsername.Controls.Add(this.lblTenDangNhap);

            this.pnlCellMatKhau.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCellMatKhau.BackColor = cLightBg;
            this.pnlCellMatKhau.Padding = new System.Windows.Forms.Padding(0, 6, 14, 6);
            this.pnlCellMatKhau.Controls.Add(this.txtMatKhau);
            this.pnlCellMatKhau.Controls.Add(this.lblMatKhau);

            this.pnlCellXacNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCellXacNhan.BackColor = cLightBg;
            this.pnlCellXacNhan.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.pnlCellXacNhan.Controls.Add(this.txtXacNhan);
            this.pnlCellXacNhan.Controls.Add(this.lblXacNhan);

            this.pnlCellEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCellEmail.BackColor = cLightBg;
            this.pnlCellEmail.Padding = new System.Windows.Forms.Padding(0, 6, 14, 6);
            this.pnlCellEmail.Controls.Add(this.txtEmail);
            this.pnlCellEmail.Controls.Add(this.lblEmail);

            this.pnlCellSDT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCellSDT.BackColor = cLightBg;
            this.pnlCellSDT.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.pnlCellSDT.Controls.Add(this.txtSDT);
            this.pnlCellSDT.Controls.Add(this.lblSDT);

            this.pnlCellNgaySinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCellNgaySinh.BackColor = cLightBg;
            this.pnlCellNgaySinh.Padding = new System.Windows.Forms.Padding(0, 6, 14, 6);
            this.pnlCellNgaySinh.Controls.Add(this.dtpNgaySinh);
            this.pnlCellNgaySinh.Controls.Add(this.lblNgaySinh);

            this.pnlCellGioiTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCellGioiTinh.BackColor = cLightBg;
            this.pnlCellGioiTinh.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.pnlCellGioiTinh.Controls.Add(this.cboGioiTinh);
            this.pnlCellGioiTinh.Controls.Add(this.lblGioiTinh);

            // ── Add cells to table ────────────────────────────────────
            this.tlpForm.Controls.Add(this.pnlCellHoTen, 0, 0);
            this.tlpForm.Controls.Add(this.pnlCellUsername, 1, 0);
            this.tlpForm.Controls.Add(this.pnlCellMatKhau, 0, 1);
            this.tlpForm.Controls.Add(this.pnlCellXacNhan, 1, 1);
            this.tlpForm.Controls.Add(this.pnlCellEmail, 0, 2);
            this.tlpForm.Controls.Add(this.pnlCellSDT, 1, 2);
            this.tlpForm.Controls.Add(this.pnlCellNgaySinh, 0, 3);
            this.tlpForm.Controls.Add(this.pnlCellGioiTinh, 1, 3);

            // ── Assemble right panel ──────────────────────────────────
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.BackColor = cLightBg;
            this.pnlRight.Controls.Add(this.tlpForm);
            this.pnlRight.Controls.Add(this.pnlRightBottom);
            this.pnlRight.Controls.Add(this.pnlRightTop);

            // ── Form ─────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = cDark;
            this.ClientSize = new System.Drawing.Size(1020, 640);
            this.MinimumSize = new System.Drawing.Size(840, 580);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.Name = "DangKyForm";
            this.Text = "Đăng ký tài khoản nhân viên";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.pnlCellHoTen.ResumeLayout(false); this.pnlCellHoTen.PerformLayout();
            this.pnlCellUsername.ResumeLayout(false); this.pnlCellUsername.PerformLayout();
            this.pnlCellMatKhau.ResumeLayout(false); this.pnlCellMatKhau.PerformLayout();
            this.pnlCellXacNhan.ResumeLayout(false); this.pnlCellXacNhan.PerformLayout();
            this.pnlCellEmail.ResumeLayout(false); this.pnlCellEmail.PerformLayout();
            this.pnlCellSDT.ResumeLayout(false); this.pnlCellSDT.PerformLayout();
            this.pnlCellNgaySinh.ResumeLayout(false); this.pnlCellNgaySinh.PerformLayout();
            this.pnlCellGioiTinh.ResumeLayout(false); this.pnlCellGioiTinh.PerformLayout();
            this.tlpForm.ResumeLayout(false);
            this.pnlRightTop.ResumeLayout(false);
            this.pnlRightBottom.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}