namespace MedicalLink.FormCommon.DangKyBanQuyen
{
    partial class frmDangKyLicense
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangKyLicense));
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnDBLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnDBKiemTra = new DevExpress.XtraEditors.SimpleButton();
            this.txtMaMay = new DevExpress.XtraEditors.MemoEdit();
            this.txtKeyKichHoat = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblThoiGianSuDung = new DevExpress.XtraEditors.LabelControl();
            this.linkLaykeyKichHoat = new System.Windows.Forms.LinkLabel();
            this.timerThongBao = new System.Windows.Forms.Timer(this.components);
            this.lblThongBao = new DevExpress.XtraEditors.LabelControl();
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.linkTroGiup = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaMay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyKichHoat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(33, 32);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(41, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Mã máy:";
            // 
            // btnDBLuu
            // 
            this.btnDBLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBLuu.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDBLuu.Appearance.Options.UseFont = true;
            this.btnDBLuu.Appearance.Options.UseForeColor = true;
            this.btnDBLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnDBLuu.Image")));
            this.btnDBLuu.Location = new System.Drawing.Point(301, 263);
            this.btnDBLuu.Name = "btnDBLuu";
            this.btnDBLuu.Size = new System.Drawing.Size(115, 40);
            this.btnDBLuu.TabIndex = 10;
            this.btnDBLuu.Text = "Lưu";
            this.btnDBLuu.Click += new System.EventHandler(this.tbnDBLuu_Click);
            // 
            // btnDBKiemTra
            // 
            this.btnDBKiemTra.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBKiemTra.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDBKiemTra.Appearance.Options.UseFont = true;
            this.btnDBKiemTra.Appearance.Options.UseForeColor = true;
            this.btnDBKiemTra.Image = ((System.Drawing.Image)(resources.GetObject("btnDBKiemTra.Image")));
            this.btnDBKiemTra.Location = new System.Drawing.Point(132, 263);
            this.btnDBKiemTra.Name = "btnDBKiemTra";
            this.btnDBKiemTra.Size = new System.Drawing.Size(115, 40);
            this.btnDBKiemTra.TabIndex = 11;
            this.btnDBKiemTra.Text = "Kiểm Tra Key";
            this.btnDBKiemTra.Click += new System.EventHandler(this.btnDBKiemTra_Click);
            // 
            // txtMaMay
            // 
            this.txtMaMay.Location = new System.Drawing.Point(80, 12);
            this.txtMaMay.Name = "txtMaMay";
            this.txtMaMay.Size = new System.Drawing.Size(394, 55);
            this.txtMaMay.TabIndex = 12;
            // 
            // txtKeyKichHoat
            // 
            this.txtKeyKichHoat.Location = new System.Drawing.Point(80, 76);
            this.txtKeyKichHoat.Name = "txtKeyKichHoat";
            this.txtKeyKichHoat.Size = new System.Drawing.Size(394, 120);
            this.txtKeyKichHoat.TabIndex = 14;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 127);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Mã kích hoạt:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(6, 207);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(89, 13);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "Thời gian sử dụng:";
            // 
            // lblThoiGianSuDung
            // 
            this.lblThoiGianSuDung.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThoiGianSuDung.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblThoiGianSuDung.Location = new System.Drawing.Point(105, 205);
            this.lblThoiGianSuDung.Name = "lblThoiGianSuDung";
            this.lblThoiGianSuDung.Size = new System.Drawing.Size(103, 16);
            this.lblThoiGianSuDung.TabIndex = 16;
            this.lblThoiGianSuDung.Text = "Thời gian sử dụng";
            // 
            // linkLaykeyKichHoat
            // 
            this.linkLaykeyKichHoat.AutoSize = true;
            this.linkLaykeyKichHoat.Location = new System.Drawing.Point(7, 276);
            this.linkLaykeyKichHoat.Name = "linkLaykeyKichHoat";
            this.linkLaykeyKichHoat.Size = new System.Drawing.Size(64, 13);
            this.linkLaykeyKichHoat.TabIndex = 17;
            this.linkLaykeyKichHoat.TabStop = true;
            this.linkLaykeyKichHoat.Text = "Lấy License";
            this.linkLaykeyKichHoat.Visible = false;
            this.linkLaykeyKichHoat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLaykeyKichHoat_LinkClicked);
            // 
            // timerThongBao
            // 
            this.timerThongBao.Interval = 2000;
            this.timerThongBao.Tick += new System.EventHandler(this.timerThongBao_Tick);
            // 
            // lblThongBao
            // 
            this.lblThongBao.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblThongBao.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblThongBao.Location = new System.Drawing.Point(127, 117);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(149, 23);
            this.lblThongBao.TabIndex = 19;
            this.lblThongBao.Text = "Sửa thành công";
            this.lblThongBao.Visible = false;
            // 
            // btnCopy
            // 
            this.btnCopy.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnCopy.Appearance.Options.UseForeColor = true;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnCopy.Location = new System.Drawing.Point(479, 12);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(40, 55);
            this.btnCopy.TabIndex = 25;
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // linkTroGiup
            // 
            this.linkTroGiup.AutoSize = true;
            this.linkTroGiup.Location = new System.Drawing.Point(468, 276);
            this.linkTroGiup.Name = "linkTroGiup";
            this.linkTroGiup.Size = new System.Drawing.Size(46, 13);
            this.linkTroGiup.TabIndex = 26;
            this.linkTroGiup.TabStop = true;
            this.linkTroGiup.Text = "Trợ giúp";
            this.linkTroGiup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTroGiup_LinkClicked);
            // 
            // frmDangKyLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 312);
            this.Controls.Add(this.linkTroGiup);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.lblThongBao);
            this.Controls.Add(this.linkLaykeyKichHoat);
            this.Controls.Add(this.lblThoiGianSuDung);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtKeyKichHoat);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtMaMay);
            this.Controls.Add(this.btnDBKiemTra);
            this.Controls.Add(this.btnDBLuu);
            this.Controls.Add(this.labelControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(540, 350);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(540, 350);
            this.Name = "frmDangKyLicense";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng ký bản quyền";
            this.Load += new System.EventHandler(this.frmConnectDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMaMay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyKichHoat.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnDBLuu;
        private DevExpress.XtraEditors.SimpleButton btnDBKiemTra;
        private DevExpress.XtraEditors.MemoEdit txtMaMay;
        private DevExpress.XtraEditors.MemoEdit txtKeyKichHoat;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblThoiGianSuDung;
        private System.Windows.Forms.LinkLabel linkLaykeyKichHoat;
        private System.Windows.Forms.Timer timerThongBao;
        private DevExpress.XtraEditors.LabelControl lblThongBao;
        private DevExpress.XtraEditors.SimpleButton btnCopy;
        private System.Windows.Forms.LinkLabel linkTroGiup;
    }
}