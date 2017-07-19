namespace MedicalLink.ChucNang.XyLyMauBenhPham
{
    partial class frmSuaThoiGianChiDinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSuaThoiGianChiDinh));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateTGChiDinh = new System.Windows.Forms.DateTimePicker();
            this.dateTGSuDung = new System.Windows.Forms.DateTimePicker();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnSuaThoiGian = new DevExpress.XtraEditors.SimpleButton();
            this.lblMaPhieuChiDinh = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(34, 66);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(101, 16);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "Thời gian chỉ định";
            // 
            // dateTGChiDinh
            // 
            this.dateTGChiDinh.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTGChiDinh.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateTGChiDinh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTGChiDinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTGChiDinh.Location = new System.Drawing.Point(148, 61);
            this.dateTGChiDinh.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTGChiDinh.Name = "dateTGChiDinh";
            this.dateTGChiDinh.Size = new System.Drawing.Size(203, 27);
            this.dateTGChiDinh.TabIndex = 1;
            this.dateTGChiDinh.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTGChiDinh.ValueChanged += new System.EventHandler(this.dateTGChiDinh_ValueChanged);
            // 
            // dateTGSuDung
            // 
            this.dateTGSuDung.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTGSuDung.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateTGSuDung.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTGSuDung.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTGSuDung.Location = new System.Drawing.Point(148, 108);
            this.dateTGSuDung.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTGSuDung.Name = "dateTGSuDung";
            this.dateTGSuDung.Size = new System.Drawing.Size(203, 27);
            this.dateTGSuDung.TabIndex = 2;
            this.dateTGSuDung.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTGSuDung.ValueChanged += new System.EventHandler(this.dateTGSuDung_ValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl2.Location = new System.Drawing.Point(31, 113);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(103, 16);
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "Thời gian sử dụng";
            // 
            // btnSuaThoiGian
            // 
            this.btnSuaThoiGian.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaThoiGian.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSuaThoiGian.Appearance.Options.UseFont = true;
            this.btnSuaThoiGian.Appearance.Options.UseForeColor = true;
            this.btnSuaThoiGian.Enabled = false;
            this.btnSuaThoiGian.Image = global::MedicalLink.Properties.Resources.check_mark_24;
            this.btnSuaThoiGian.Location = new System.Drawing.Point(148, 158);
            this.btnSuaThoiGian.Name = "btnSuaThoiGian";
            this.btnSuaThoiGian.Size = new System.Drawing.Size(112, 40);
            this.btnSuaThoiGian.TabIndex = 2;
            this.btnSuaThoiGian.Text = "OK";
            this.btnSuaThoiGian.Click += new System.EventHandler(this.btnSuaThoiGian_Click);
            // 
            // lblMaPhieuChiDinh
            // 
            this.lblMaPhieuChiDinh.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaPhieuChiDinh.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblMaPhieuChiDinh.Location = new System.Drawing.Point(148, 10);
            this.lblMaPhieuChiDinh.MinimumSize = new System.Drawing.Size(154, 20);
            this.lblMaPhieuChiDinh.Name = "lblMaPhieuChiDinh";
            this.lblMaPhieuChiDinh.Size = new System.Drawing.Size(154, 20);
            this.lblMaPhieuChiDinh.TabIndex = 34;
            this.lblMaPhieuChiDinh.Text = "lblMaPhieuChiDinh";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl5.Location = new System.Drawing.Point(82, 12);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(52, 16);
            this.labelControl5.TabIndex = 35;
            this.labelControl5.Text = "Mã phiếu";
            // 
            // frmSuaThoiGianChiDinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 212);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.lblMaPhieuChiDinh);
            this.Controls.Add(this.btnSuaThoiGian);
            this.Controls.Add(this.dateTGSuDung);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.dateTGChiDinh);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(450, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 250);
            this.Name = "frmSuaThoiGianChiDinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa thời gian chỉ định";
            this.Load += new System.EventHandler(this.frmSuaThoiGianChiDinh_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal System.Windows.Forms.DateTimePicker dateTGChiDinh;
        internal System.Windows.Forms.DateTimePicker dateTGSuDung;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSuaThoiGian;
        private DevExpress.XtraEditors.LabelControl lblMaPhieuChiDinh;
        private DevExpress.XtraEditors.LabelControl labelControl5;

    }
}