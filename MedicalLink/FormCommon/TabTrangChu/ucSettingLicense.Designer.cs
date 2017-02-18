namespace MedicalLink.FormCommon.TabTrangChu
{
    partial class ucSettingLicense
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSettingLicense));
            this.groupBoxLicense = new System.Windows.Forms.GroupBox();
            this.lblThongBao = new DevExpress.XtraEditors.LabelControl();
            this.lblThoiGianSuDung = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.btnLicenseCopy = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaMay = new DevExpress.XtraEditors.MemoEdit();
            this.btnLicenseLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnLicenseKiemTra = new DevExpress.XtraEditors.SimpleButton();
            this.txtKeyKichHoat = new DevExpress.XtraEditors.MemoEdit();
            this.groupBoxTaoLicense = new System.Windows.Forms.GroupBox();
            this.btnTaoLicenseTao = new DevExpress.XtraEditors.SimpleButton();
            this.btnTaoLicenseCopy = new DevExpress.XtraEditors.SimpleButton();
            this.txtTaoLicenseMaKichHoat = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.dtTaoLicenseKeyTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtTaoLicenseKeyDenNgay = new System.Windows.Forms.DateTimePicker();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.txtTaoLicensePassword = new DevExpress.XtraEditors.TextEdit();
            this.txtTaoLicenseMaMay = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.timerThongBao = new System.Windows.Forms.Timer(this.components);
            this.groupBoxLicense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaMay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyKichHoat.Properties)).BeginInit();
            this.groupBoxTaoLicense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaoLicenseMaKichHoat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaoLicensePassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaoLicenseMaMay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxLicense
            // 
            this.groupBoxLicense.Controls.Add(this.lblThongBao);
            this.groupBoxLicense.Controls.Add(this.lblThoiGianSuDung);
            this.groupBoxLicense.Controls.Add(this.labelControl3);
            this.groupBoxLicense.Controls.Add(this.labelControl10);
            this.groupBoxLicense.Controls.Add(this.btnLicenseCopy);
            this.groupBoxLicense.Controls.Add(this.labelControl9);
            this.groupBoxLicense.Controls.Add(this.txtMaMay);
            this.groupBoxLicense.Controls.Add(this.btnLicenseLuu);
            this.groupBoxLicense.Controls.Add(this.btnLicenseKiemTra);
            this.groupBoxLicense.Controls.Add(this.txtKeyKichHoat);
            this.groupBoxLicense.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxLicense.Location = new System.Drawing.Point(0, 0);
            this.groupBoxLicense.Name = "groupBoxLicense";
            this.groupBoxLicense.Size = new System.Drawing.Size(771, 284);
            this.groupBoxLicense.TabIndex = 29;
            this.groupBoxLicense.TabStop = false;
            this.groupBoxLicense.Text = "Nhập license";
            // 
            // lblThongBao
            // 
            this.lblThongBao.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblThongBao.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblThongBao.Location = new System.Drawing.Point(253, 122);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(101, 23);
            this.lblThongBao.TabIndex = 30;
            this.lblThongBao.Text = "Thong bao";
            this.lblThongBao.Visible = false;
            // 
            // lblThoiGianSuDung
            // 
            this.lblThoiGianSuDung.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThoiGianSuDung.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblThoiGianSuDung.Location = new System.Drawing.Point(204, 242);
            this.lblThoiGianSuDung.Name = "lblThoiGianSuDung";
            this.lblThoiGianSuDung.Size = new System.Drawing.Size(103, 16);
            this.lblThoiGianSuDung.TabIndex = 29;
            this.lblThoiGianSuDung.Text = "Thời gian sử dụng";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(105, 244);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(89, 13);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "Thời gian sử dụng:";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl10.Location = new System.Drawing.Point(35, 122);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(64, 13);
            this.labelControl10.TabIndex = 27;
            this.labelControl10.Text = "Mã kích hoạt:";
            // 
            // btnLicenseCopy
            // 
            this.btnLicenseCopy.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnLicenseCopy.Appearance.Options.UseForeColor = true;
            this.btnLicenseCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnLicenseCopy.Image")));
            this.btnLicenseCopy.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnLicenseCopy.Location = new System.Drawing.Point(592, 19);
            this.btnLicenseCopy.Name = "btnLicenseCopy";
            this.btnLicenseCopy.Size = new System.Drawing.Size(40, 55);
            this.btnLicenseCopy.TabIndex = 26;
            this.btnLicenseCopy.Text = "Copy";
            this.btnLicenseCopy.Click += new System.EventHandler(this.btnLicenseCopy_Click);
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl9.Location = new System.Drawing.Point(58, 40);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(41, 13);
            this.labelControl9.TabIndex = 19;
            this.labelControl9.Text = "Mã máy:";
            // 
            // txtMaMay
            // 
            this.txtMaMay.Location = new System.Drawing.Point(105, 19);
            this.txtMaMay.Name = "txtMaMay";
            this.txtMaMay.Size = new System.Drawing.Size(481, 56);
            this.txtMaMay.TabIndex = 18;
            // 
            // btnLicenseLuu
            // 
            this.btnLicenseLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLicenseLuu.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnLicenseLuu.Appearance.Options.UseFont = true;
            this.btnLicenseLuu.Appearance.Options.UseForeColor = true;
            this.btnLicenseLuu.Location = new System.Drawing.Point(365, 191);
            this.btnLicenseLuu.Name = "btnLicenseLuu";
            this.btnLicenseLuu.Size = new System.Drawing.Size(80, 23);
            this.btnLicenseLuu.TabIndex = 17;
            this.btnLicenseLuu.Text = "Lưu";
            this.btnLicenseLuu.Click += new System.EventHandler(this.btnLicenseLuu_Click);
            // 
            // btnLicenseKiemTra
            // 
            this.btnLicenseKiemTra.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLicenseKiemTra.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnLicenseKiemTra.Appearance.Options.UseFont = true;
            this.btnLicenseKiemTra.Appearance.Options.UseForeColor = true;
            this.btnLicenseKiemTra.Location = new System.Drawing.Point(200, 191);
            this.btnLicenseKiemTra.Name = "btnLicenseKiemTra";
            this.btnLicenseKiemTra.Size = new System.Drawing.Size(80, 23);
            this.btnLicenseKiemTra.TabIndex = 16;
            this.btnLicenseKiemTra.Text = "Kiểm tra";
            this.btnLicenseKiemTra.Click += new System.EventHandler(this.btnLicenseKiemTra_Click);
            // 
            // txtKeyKichHoat
            // 
            this.txtKeyKichHoat.Location = new System.Drawing.Point(105, 81);
            this.txtKeyKichHoat.Name = "txtKeyKichHoat";
            this.txtKeyKichHoat.Size = new System.Drawing.Size(481, 93);
            this.txtKeyKichHoat.TabIndex = 15;
            // 
            // groupBoxTaoLicense
            // 
            this.groupBoxTaoLicense.Controls.Add(this.btnTaoLicenseTao);
            this.groupBoxTaoLicense.Controls.Add(this.btnTaoLicenseCopy);
            this.groupBoxTaoLicense.Controls.Add(this.txtTaoLicenseMaKichHoat);
            this.groupBoxTaoLicense.Controls.Add(this.labelControl15);
            this.groupBoxTaoLicense.Controls.Add(this.labelControl11);
            this.groupBoxTaoLicense.Controls.Add(this.labelControl12);
            this.groupBoxTaoLicense.Controls.Add(this.dtTaoLicenseKeyTuNgay);
            this.groupBoxTaoLicense.Controls.Add(this.dtTaoLicenseKeyDenNgay);
            this.groupBoxTaoLicense.Controls.Add(this.labelControl13);
            this.groupBoxTaoLicense.Controls.Add(this.txtTaoLicensePassword);
            this.groupBoxTaoLicense.Controls.Add(this.txtTaoLicenseMaMay);
            this.groupBoxTaoLicense.Controls.Add(this.labelControl14);
            this.groupBoxTaoLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTaoLicense.Location = new System.Drawing.Point(0, 284);
            this.groupBoxTaoLicense.Name = "groupBoxTaoLicense";
            this.groupBoxTaoLicense.Size = new System.Drawing.Size(771, 298);
            this.groupBoxTaoLicense.TabIndex = 30;
            this.groupBoxTaoLicense.TabStop = false;
            this.groupBoxTaoLicense.Text = "Tạo license";
            this.groupBoxTaoLicense.Visible = false;
            // 
            // btnTaoLicenseTao
            // 
            this.btnTaoLicenseTao.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoLicenseTao.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnTaoLicenseTao.Appearance.Options.UseFont = true;
            this.btnTaoLicenseTao.Appearance.Options.UseForeColor = true;
            this.btnTaoLicenseTao.Image = global::MedicalLink.Properties.Resources.arrow_32_16;
            this.btnTaoLicenseTao.Location = new System.Drawing.Point(275, 140);
            this.btnTaoLicenseTao.Name = "btnTaoLicenseTao";
            this.btnTaoLicenseTao.Size = new System.Drawing.Size(100, 23);
            this.btnTaoLicenseTao.TabIndex = 35;
            this.btnTaoLicenseTao.Text = "Tạo license";
            this.btnTaoLicenseTao.Click += new System.EventHandler(this.btnTaoLicenseTao_Click);
            // 
            // btnTaoLicenseCopy
            // 
            this.btnTaoLicenseCopy.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnTaoLicenseCopy.Appearance.Options.UseForeColor = true;
            this.btnTaoLicenseCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnTaoLicenseCopy.Image")));
            this.btnTaoLicenseCopy.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnTaoLicenseCopy.Location = new System.Drawing.Point(560, 184);
            this.btnTaoLicenseCopy.Name = "btnTaoLicenseCopy";
            this.btnTaoLicenseCopy.Size = new System.Drawing.Size(40, 55);
            this.btnTaoLicenseCopy.TabIndex = 34;
            this.btnTaoLicenseCopy.Text = "Copy";
            this.btnTaoLicenseCopy.Click += new System.EventHandler(this.btnTaoLicenseCopy_Click);
            // 
            // txtTaoLicenseMaKichHoat
            // 
            this.txtTaoLicenseMaKichHoat.Location = new System.Drawing.Point(105, 172);
            this.txtTaoLicenseMaKichHoat.Name = "txtTaoLicenseMaKichHoat";
            this.txtTaoLicenseMaKichHoat.Size = new System.Drawing.Size(449, 84);
            this.txtTaoLicenseMaKichHoat.TabIndex = 32;
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl15.Location = new System.Drawing.Point(31, 208);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(68, 13);
            this.labelControl15.TabIndex = 33;
            this.labelControl15.Text = "Key kích hoạt:";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl11.Location = new System.Drawing.Point(346, 105);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(68, 13);
            this.labelControl11.TabIndex = 31;
            this.labelControl11.Text = "Thời gian đến:";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl12.Location = new System.Drawing.Point(38, 105);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(61, 13);
            this.labelControl12.TabIndex = 30;
            this.labelControl12.Text = "Thời gian từ:";
            // 
            // dtTaoLicenseKeyTuNgay
            // 
            this.dtTaoLicenseKeyTuNgay.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTaoLicenseKeyTuNgay.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dtTaoLicenseKeyTuNgay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTaoLicenseKeyTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTaoLicenseKeyTuNgay.Location = new System.Drawing.Point(105, 98);
            this.dtTaoLicenseKeyTuNgay.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dtTaoLicenseKeyTuNgay.Name = "dtTaoLicenseKeyTuNgay";
            this.dtTaoLicenseKeyTuNgay.Size = new System.Drawing.Size(175, 23);
            this.dtTaoLicenseKeyTuNgay.TabIndex = 27;
            this.dtTaoLicenseKeyTuNgay.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // dtTaoLicenseKeyDenNgay
            // 
            this.dtTaoLicenseKeyDenNgay.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTaoLicenseKeyDenNgay.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dtTaoLicenseKeyDenNgay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTaoLicenseKeyDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTaoLicenseKeyDenNgay.Location = new System.Drawing.Point(425, 98);
            this.dtTaoLicenseKeyDenNgay.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dtTaoLicenseKeyDenNgay.Name = "dtTaoLicenseKeyDenNgay";
            this.dtTaoLicenseKeyDenNgay.Size = new System.Drawing.Size(175, 23);
            this.dtTaoLicenseKeyDenNgay.TabIndex = 28;
            this.dtTaoLicenseKeyDenNgay.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl13.Location = new System.Drawing.Point(49, 22);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(50, 13);
            this.labelControl13.TabIndex = 29;
            this.labelControl13.Text = "Password:";
            // 
            // txtTaoLicensePassword
            // 
            this.txtTaoLicensePassword.Location = new System.Drawing.Point(105, 19);
            this.txtTaoLicensePassword.Name = "txtTaoLicensePassword";
            this.txtTaoLicensePassword.Properties.PasswordChar = '*';
            this.txtTaoLicensePassword.Size = new System.Drawing.Size(495, 20);
            this.txtTaoLicensePassword.TabIndex = 24;
            this.txtTaoLicensePassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaoLicensePassword_KeyDown);
            // 
            // txtTaoLicenseMaMay
            // 
            this.txtTaoLicenseMaMay.Location = new System.Drawing.Point(105, 46);
            this.txtTaoLicenseMaMay.Name = "txtTaoLicenseMaMay";
            this.txtTaoLicenseMaMay.Size = new System.Drawing.Size(495, 45);
            this.txtTaoLicenseMaMay.TabIndex = 26;
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl14.Location = new System.Drawing.Point(58, 62);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(41, 13);
            this.labelControl14.TabIndex = 25;
            this.labelControl14.Text = "Mã máy:";
            // 
            // timerThongBao
            // 
            this.timerThongBao.Interval = 2000;
            this.timerThongBao.Tick += new System.EventHandler(this.timerThongBao_Tick);
            // 
            // ucSettingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxTaoLicense);
            this.Controls.Add(this.groupBoxLicense);
            this.Name = "ucSettingLicense";
            this.Size = new System.Drawing.Size(771, 582);
            this.Load += new System.EventHandler(this.ucSettingLicense_Load);
            this.groupBoxLicense.ResumeLayout(false);
            this.groupBoxLicense.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaMay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyKichHoat.Properties)).EndInit();
            this.groupBoxTaoLicense.ResumeLayout(false);
            this.groupBoxTaoLicense.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaoLicenseMaKichHoat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaoLicensePassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaoLicenseMaMay.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLicense;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.SimpleButton btnLicenseCopy;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.MemoEdit txtMaMay;
        private DevExpress.XtraEditors.SimpleButton btnLicenseLuu;
        private DevExpress.XtraEditors.SimpleButton btnLicenseKiemTra;
        private DevExpress.XtraEditors.MemoEdit txtKeyKichHoat;
        private System.Windows.Forms.GroupBox groupBoxTaoLicense;
        private DevExpress.XtraEditors.SimpleButton btnTaoLicenseTao;
        private DevExpress.XtraEditors.SimpleButton btnTaoLicenseCopy;
        private DevExpress.XtraEditors.MemoEdit txtTaoLicenseMaKichHoat;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private System.Windows.Forms.DateTimePicker dtTaoLicenseKeyTuNgay;
        private System.Windows.Forms.DateTimePicker dtTaoLicenseKeyDenNgay;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.TextEdit txtTaoLicensePassword;
        private DevExpress.XtraEditors.MemoEdit txtTaoLicenseMaMay;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl lblThoiGianSuDung;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblThongBao;
        private System.Windows.Forms.Timer timerThongBao;

    }
}
