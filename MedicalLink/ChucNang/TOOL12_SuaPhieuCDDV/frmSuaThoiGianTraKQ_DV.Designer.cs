namespace MedicalLink.ChucNang.XyLyMauBenhPham
{
    partial class frmSuaThoiGianTraKQ_DV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSuaThoiGianTraKQ_DV));
            this.btnSuaThoiGian = new DevExpress.XtraEditors.SimpleButton();
            this.lblServicepriceID = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.dateTGPTTT = new System.Windows.Forms.DateTimePicker();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.dateTGTraKetQua = new System.Windows.Forms.DateTimePicker();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblServicepricename = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // btnSuaThoiGian
            // 
            this.btnSuaThoiGian.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaThoiGian.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSuaThoiGian.Appearance.Options.UseFont = true;
            this.btnSuaThoiGian.Appearance.Options.UseForeColor = true;
            this.btnSuaThoiGian.Enabled = false;
            this.btnSuaThoiGian.Image = global::MedicalLink.Properties.Resources.check_mark_24;
            this.btnSuaThoiGian.Location = new System.Drawing.Point(201, 279);
            this.btnSuaThoiGian.Name = "btnSuaThoiGian";
            this.btnSuaThoiGian.Size = new System.Drawing.Size(112, 40);
            this.btnSuaThoiGian.TabIndex = 99;
            this.btnSuaThoiGian.Text = "OK";
            this.btnSuaThoiGian.Click += new System.EventHandler(this.btnSuaThoiGian_Click);
            // 
            // lblServicepriceID
            // 
            this.lblServicepriceID.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServicepriceID.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblServicepriceID.Location = new System.Drawing.Point(180, 12);
            this.lblServicepriceID.MinimumSize = new System.Drawing.Size(154, 20);
            this.lblServicepriceID.Name = "lblServicepriceID";
            this.lblServicepriceID.Size = new System.Drawing.Size(154, 20);
            this.lblServicepriceID.TabIndex = 34;
            this.lblServicepriceID.Text = "lblMaPhieuChiDinh";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl5.Location = new System.Drawing.Point(108, 14);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(56, 16);
            this.labelControl5.TabIndex = 35;
            this.labelControl5.Text = "ID dịch vụ";
            // 
            // dateTGPTTT
            // 
            this.dateTGPTTT.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTGPTTT.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateTGPTTT.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTGPTTT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTGPTTT.Location = new System.Drawing.Point(180, 169);
            this.dateTGPTTT.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTGPTTT.Name = "dateTGPTTT";
            this.dateTGPTTT.Size = new System.Drawing.Size(203, 27);
            this.dateTGPTTT.TabIndex = 3;
            this.dateTGPTTT.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTGPTTT.ValueChanged += new System.EventHandler(this.dateTGPTTT_ValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl6.Location = new System.Drawing.Point(28, 178);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(136, 16);
            this.labelControl6.TabIndex = 84;
            this.labelControl6.Text = "TG phẫu thuật thủ thuật";
            // 
            // dateTGTraKetQua
            // 
            this.dateTGTraKetQua.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTGTraKetQua.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateTGTraKetQua.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTGTraKetQua.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTGTraKetQua.Location = new System.Drawing.Point(180, 202);
            this.dateTGTraKetQua.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTGTraKetQua.Name = "dateTGTraKetQua";
            this.dateTGTraKetQua.Size = new System.Drawing.Size(203, 27);
            this.dateTGTraKetQua.TabIndex = 4;
            this.dateTGTraKetQua.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTGTraKetQua.ValueChanged += new System.EventHandler(this.dateTGTraKetQua_ValueChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl7.Location = new System.Drawing.Point(12, 211);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(152, 16);
            this.labelControl7.TabIndex = 86;
            this.labelControl7.Text = "TG trả kết quả/TG kết thúc";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(108, 60);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 16);
            this.labelControl1.TabIndex = 101;
            this.labelControl1.Text = "Tên dịch vụ";
            // 
            // lblServicepricename
            // 
            this.lblServicepricename.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServicepricename.Appearance.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblServicepricename.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblServicepricename.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblServicepricename.Location = new System.Drawing.Point(180, 38);
            this.lblServicepricename.MinimumSize = new System.Drawing.Size(154, 20);
            this.lblServicepricename.Name = "lblServicepricename";
            this.lblServicepricename.Size = new System.Drawing.Size(417, 67);
            this.lblServicepricename.TabIndex = 100;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl2.Location = new System.Drawing.Point(389, 178);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 16);
            this.labelControl2.TabIndex = 102;
            this.labelControl2.Text = "(đối với PTTT)";
            // 
            // frmSuaThoiGianTraKQ_DV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 352);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lblServicepricename);
            this.Controls.Add(this.dateTGTraKetQua);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.dateTGPTTT);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.lblServicepriceID);
            this.Controls.Add(this.btnSuaThoiGian);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(630, 390);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(630, 390);
            this.Name = "frmSuaThoiGianTraKQ_DV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa thời gian trả kết quả";
            this.Load += new System.EventHandler(this.frmSuaThoiGianTraKQ_DV_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnSuaThoiGian;
        private DevExpress.XtraEditors.LabelControl lblServicepriceID;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        internal System.Windows.Forms.DateTimePicker dateTGPTTT;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        internal System.Windows.Forms.DateTimePicker dateTGTraKetQua;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblServicepricename;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}