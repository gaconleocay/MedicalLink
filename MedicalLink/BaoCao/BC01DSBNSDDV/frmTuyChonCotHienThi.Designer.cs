namespace MedicalLink.BaoCao.BC01DSBNSDDV
{
    partial class frmTuyChonCotHienThi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTuyChonCotHienThi));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkListCotBoSung = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.btnLuuLai = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkListCotBoSung.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(21, 40);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(99, 16);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "Chọn cột bổ sung";
            // 
            // chkListCotBoSung
            // 
            this.chkListCotBoSung.EditValue = "";
            this.chkListCotBoSung.Location = new System.Drawing.Point(21, 62);
            this.chkListCotBoSung.Name = "chkListCotBoSung";
            this.chkListCotBoSung.Properties.AllowMultiSelect = true;
            this.chkListCotBoSung.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListCotBoSung.Properties.Appearance.Options.UseFont = true;
            this.chkListCotBoSung.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkListCotBoSung.Properties.AppearanceDropDown.Options.UseFont = true;
            this.chkListCotBoSung.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkListCotBoSung.Properties.DropDownRows = 15;
            this.chkListCotBoSung.Size = new System.Drawing.Size(542, 22);
            this.chkListCotBoSung.TabIndex = 104;
            // 
            // btnLuuLai
            // 
            this.btnLuuLai.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuLai.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnLuuLai.Appearance.Options.UseFont = true;
            this.btnLuuLai.Appearance.Options.UseForeColor = true;
            this.btnLuuLai.Image = global::MedicalLink.Properties.Resources.check_mark_24;
            this.btnLuuLai.Location = new System.Drawing.Point(228, 161);
            this.btnLuuLai.Name = "btnLuuLai";
            this.btnLuuLai.Size = new System.Drawing.Size(120, 40);
            this.btnLuuLai.TabIndex = 105;
            this.btnLuuLai.Text = "OK";
            this.btnLuuLai.Click += new System.EventHandler(this.btnLuuLai_Click);
            // 
            // frmTuyChonCotHienThi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 215);
            this.Controls.Add(this.btnLuuLai);
            this.Controls.Add(this.chkListCotBoSung);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTuyChonCotHienThi";
            this.Text = "Tùy chọn bổ sung thêm cột lấy dữ liệu";
            this.Load += new System.EventHandler(this.frmTuyChonCotHienThi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkListCotBoSung.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkListCotBoSung;
        private DevExpress.XtraEditors.SimpleButton btnLuuLai;
    }
}