namespace CNPN_NHOM13_GymManagement
{
    partial class DangKyForm
    {
        private System.ComponentModel.IContainer components = null;

        // Add missing control field declarations used by InitializeComponent and other partials
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxHoTen;
        private System.Windows.Forms.TextBox txtBoxMatKhau;
        private System.Windows.Forms.TextBox txtBoxSĐT;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comboBGioiTinh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnHuyDangKy;
        private System.Windows.Forms.Button btnDangky;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Drawing.Font modernFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxHoTen = new System.Windows.Forms.TextBox();
            this.txtBoxMatKhau = new System.Windows.Forms.TextBox();
            this.txtBoxSĐT = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.comboBGioiTinh = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnHuyDangKy = new System.Windows.Forms.Button();
            this.btnDangky = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(931, 80);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(931, 80);
            this.label1.TabIndex = 0;
            this.label1.Text = "ĐĂNG KÝ HỘI VIÊN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtBoxHoTen, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtBoxMatKhau, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtBoxSĐT, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.dateTimePicker1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboBGioiTinh, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 80);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(931, 378);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = modernFont;
            this.label2.Location = new System.Drawing.Point(33, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 60);
            this.label2.TabIndex = 0;
            this.label2.Text = "Họ và tên:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBoxHoTen
            // 
            this.txtBoxHoTen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxHoTen.Font = modernFont;
            this.txtBoxHoTen.Location = new System.Drawing.Point(233, 33);
            this.txtBoxHoTen.Name = "txtBoxHoTen";
            this.txtBoxHoTen.Size = new System.Drawing.Size(444, 34);
            this.txtBoxHoTen.TabIndex = 1;
            // 
            // txtBoxMatKhau
            // 
            this.txtBoxMatKhau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxMatKhau.Font = modernFont;
            this.txtBoxMatKhau.Location = new System.Drawing.Point(233, 93);
            this.txtBoxMatKhau.Name = "txtBoxMatKhau";
            this.txtBoxMatKhau.Size = new System.Drawing.Size(444, 34);
            this.txtBoxMatKhau.TabIndex = 2;
            this.txtBoxMatKhau.UseSystemPasswordChar = true;
            // 
            // txtBoxSĐT
            // 
            this.txtBoxSĐT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxSĐT.Font = modernFont;
            this.txtBoxSĐT.Location = new System.Drawing.Point(233, 153);
            this.txtBoxSĐT.Name = "txtBoxSĐT";
            this.txtBoxSĐT.Size = new System.Drawing.Size(444, 34);
            this.txtBoxSĐT.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Font = modernFont;
            this.dateTimePicker1.Location = new System.Drawing.Point(233, 213);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(444, 34);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // comboBGioiTinh
            // 
            this.comboBGioiTinh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBGioiTinh.Font = modernFont;
            this.comboBGioiTinh.FormattingEnabled = true;
            this.comboBGioiTinh.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Khác"});
            this.comboBGioiTinh.Location = new System.Drawing.Point(233, 273);
            this.comboBGioiTinh.Name = "comboBGioiTinh";
            this.comboBGioiTinh.Size = new System.Drawing.Size(444, 36);
            this.comboBGioiTinh.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnHuyDangKy);
            this.panel2.Controls.Add(this.btnDangky);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 458);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(931, 73);
            this.panel2.TabIndex = 2;
            // 
            // btnHuyDangKy
            // 
            this.btnHuyDangKy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnHuyDangKy.Font = modernFont;
            this.btnHuyDangKy.Location = new System.Drawing.Point(704, 17);
            this.btnHuyDangKy.Name = "btnHuyDangKy";
            this.btnHuyDangKy.Size = new System.Drawing.Size(200, 40);
            this.btnHuyDangKy.TabIndex = 1;
            this.btnHuyDangKy.Text = "Hủy đăng ký";
            this.btnHuyDangKy.UseVisualStyleBackColor = true;
            // 
            // btnDangky
            // 
            this.btnDangky.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnDangky.Font = modernFont;
            this.btnDangky.Location = new System.Drawing.Point(33, 17);
            this.btnDangky.Name = "btnDangky";
            this.btnDangky.Size = new System.Drawing.Size(200, 40);
            this.btnDangky.TabIndex = 0;
            this.btnDangky.Text = "Đăng ký";
            this.btnDangky.UseVisualStyleBackColor = true;
            // 
            // DangKyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 39F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 531);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = modernFont;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "DangKyForm";
            this.Text = "DangKyForm";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
    }
}