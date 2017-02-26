namespace MedicalLink.Dashboard.BCBenhNhanNoiTru
{
    partial class BCBenhNhanNoiTruTuyChonNangCao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BCBenhNhanNoiTruTuyChonNangCao));
            this.btnSettingAdvand = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.dtTGLayDLTu = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btnSettingAdvand
            // 
            this.btnSettingAdvand.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettingAdvand.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSettingAdvand.Appearance.Options.UseFont = true;
            this.btnSettingAdvand.Appearance.Options.UseForeColor = true;
            this.btnSettingAdvand.Image = global::MedicalLink.Properties.Resources.ok_16;
            this.btnSettingAdvand.Location = new System.Drawing.Point(206, 199);
            this.btnSettingAdvand.Name = "btnSettingAdvand";
            this.btnSettingAdvand.Size = new System.Drawing.Size(100, 25);
            this.btnSettingAdvand.TabIndex = 81;
            this.btnSettingAdvand.Text = "OK";
            this.btnSettingAdvand.Click += new System.EventHandler(this.btnSettingAdvand_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(153, 119);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 14);
            this.labelControl1.TabIndex = 85;
            this.labelControl1.Text = "Đến hiện tại";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl3.Location = new System.Drawing.Point(153, 77);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(16, 14);
            this.labelControl3.TabIndex = 84;
            this.labelControl3.Text = "Từ";
            // 
            // labelControl23
            // 
            this.labelControl23.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl23.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl23.Location = new System.Drawing.Point(153, 29);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(173, 14);
            this.labelControl23.TabIndex = 83;
            this.labelControl23.Text = "Khoảng thời gian lấy dữ liệu";
            // 
            // dtTGLayDLTu
            // 
            this.dtTGLayDLTu.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTGLayDLTu.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dtTGLayDLTu.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTGLayDLTu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTGLayDLTu.Location = new System.Drawing.Point(190, 71);
            this.dtTGLayDLTu.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dtTGLayDLTu.Name = "dtTGLayDLTu";
            this.dtTGLayDLTu.Size = new System.Drawing.Size(185, 23);
            this.dtTGLayDLTu.TabIndex = 82;
            this.dtTGLayDLTu.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // BCBenhNhanNoiTruTuyChonNangCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 236);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl23);
            this.Controls.Add(this.dtTGLayDLTu);
            this.Controls.Add(this.btnSettingAdvand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BCBenhNhanNoiTruTuyChonNangCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình nâng cao";
            this.Load += new System.EventHandler(this.BCBenhNhanNoiTruTuyChonNangCao_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSettingAdvand;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private System.Windows.Forms.DateTimePicker dtTGLayDLTu;
    }
}