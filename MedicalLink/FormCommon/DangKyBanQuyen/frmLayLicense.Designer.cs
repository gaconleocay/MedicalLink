namespace MedicalLink.FormCommon.DangKyBanQuyen
{
    partial class frmLayLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayLicense));
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnTaoLicense = new DevExpress.XtraEditors.SimpleButton();
            this.txtMaMay = new DevExpress.XtraEditors.MemoEdit();
            this.txtKeyKichHoat = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtPasswordMoKhoa = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.dtKeyTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtKeyDenNgay = new System.Windows.Forms.DateTimePicker();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaMay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyKichHoat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordMoKhoa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(33, 65);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(41, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Mã máy:";
            // 
            // btnTaoLicense
            // 
            this.btnTaoLicense.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaoLicense.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnTaoLicense.Appearance.Options.UseFont = true;
            this.btnTaoLicense.Appearance.Options.UseForeColor = true;
            this.btnTaoLicense.Enabled = false;
            this.btnTaoLicense.Image = ((System.Drawing.Image)(resources.GetObject("btnTaoLicense.Image")));
            this.btnTaoLicense.Location = new System.Drawing.Point(233, 157);
            this.btnTaoLicense.Name = "btnTaoLicense";
            this.btnTaoLicense.Size = new System.Drawing.Size(115, 40);
            this.btnTaoLicense.TabIndex = 6;
            this.btnTaoLicense.Text = "Tạo License";
            this.btnTaoLicense.Click += new System.EventHandler(this.btnTaoLicense_Click);
            // 
            // txtMaMay
            // 
            this.txtMaMay.Location = new System.Drawing.Point(80, 49);
            this.txtMaMay.Name = "txtMaMay";
            this.txtMaMay.Size = new System.Drawing.Size(436, 45);
            this.txtMaMay.TabIndex = 2;
            // 
            // txtKeyKichHoat
            // 
            this.txtKeyKichHoat.Location = new System.Drawing.Point(80, 203);
            this.txtKeyKichHoat.Name = "txtKeyKichHoat";
            this.txtKeyKichHoat.Size = new System.Drawing.Size(436, 84);
            this.txtKeyKichHoat.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 239);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Key kích hoạt:";
            // 
            // txtPasswordMoKhoa
            // 
            this.txtPasswordMoKhoa.Location = new System.Drawing.Point(80, 22);
            this.txtPasswordMoKhoa.Name = "txtPasswordMoKhoa";
            this.txtPasswordMoKhoa.Properties.PasswordChar = '*';
            this.txtPasswordMoKhoa.Size = new System.Drawing.Size(436, 20);
            this.txtPasswordMoKhoa.TabIndex = 1;
            this.txtPasswordMoKhoa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPasswordMoKhoa_KeyDown);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(24, 25);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(50, 13);
            this.labelControl4.TabIndex = 17;
            this.labelControl4.Text = "Password:";
            // 
            // dtKeyTuNgay
            // 
            this.dtKeyTuNgay.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtKeyTuNgay.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dtKeyTuNgay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtKeyTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtKeyTuNgay.Location = new System.Drawing.Point(80, 101);
            this.dtKeyTuNgay.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dtKeyTuNgay.Name = "dtKeyTuNgay";
            this.dtKeyTuNgay.Size = new System.Drawing.Size(160, 23);
            this.dtKeyTuNgay.TabIndex = 4;
            this.dtKeyTuNgay.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // dtKeyDenNgay
            // 
            this.dtKeyDenNgay.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtKeyDenNgay.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dtKeyDenNgay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtKeyDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtKeyDenNgay.Location = new System.Drawing.Point(356, 101);
            this.dtKeyDenNgay.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dtKeyDenNgay.Name = "dtKeyDenNgay";
            this.dtKeyDenNgay.Size = new System.Drawing.Size(160, 23);
            this.dtKeyDenNgay.TabIndex = 5;
            this.dtKeyDenNgay.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(14, 108);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(61, 13);
            this.labelControl5.TabIndex = 22;
            this.labelControl5.Text = "Thời gian từ:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(280, 108);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(68, 13);
            this.labelControl6.TabIndex = 23;
            this.labelControl6.Text = "Thời gian đến:";
            // 
            // btnCopy
            // 
            this.btnCopy.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnCopy.Appearance.Options.UseForeColor = true;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnCopy.Location = new System.Drawing.Point(522, 217);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(40, 55);
            this.btnCopy.TabIndex = 24;
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // frmLayLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 312);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.dtKeyTuNgay);
            this.Controls.Add(this.dtKeyDenNgay);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtPasswordMoKhoa);
            this.Controls.Add(this.txtKeyKichHoat);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtMaMay);
            this.Controls.Add(this.btnTaoLicense);
            this.Controls.Add(this.labelControl2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 350);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "frmLayLicense";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo License";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLayLicense_FormClosing);
            this.Load += new System.EventHandler(this.frmLayLicense_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtMaMay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyKichHoat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordMoKhoa.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnTaoLicense;
        private DevExpress.XtraEditors.MemoEdit txtMaMay;
        private DevExpress.XtraEditors.MemoEdit txtKeyKichHoat;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtPasswordMoKhoa;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.DateTimePicker dtKeyTuNgay;
        private System.Windows.Forms.DateTimePicker dtKeyDenNgay;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnCopy;
    }
}