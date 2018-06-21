namespace MedicalLink.ChucNang.ImportDMThuocVatTu
{
    partial class frmChonNhomThuocVatTu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChonNhomThuocVatTu));
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.chkThuoc = new DevExpress.XtraEditors.CheckEdit();
            this.chkVatTu = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.chkThuoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVatTu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Appearance.Options.UseForeColor = true;
            this.btnOK.Image = global::MedicalLink.Properties.Resources.check_mark_24;
            this.btnOK.Location = new System.Drawing.Point(122, 158);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 40);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkThuoc
            // 
            this.chkThuoc.Location = new System.Drawing.Point(12, 39);
            this.chkThuoc.Name = "chkThuoc";
            this.chkThuoc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThuoc.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.chkThuoc.Properties.Appearance.Options.UseFont = true;
            this.chkThuoc.Properties.Appearance.Options.UseForeColor = true;
            this.chkThuoc.Properties.Caption = "Thuốc";
            this.chkThuoc.Size = new System.Drawing.Size(147, 23);
            this.chkThuoc.TabIndex = 4;
            // 
            // chkVatTu
            // 
            this.chkVatTu.Location = new System.Drawing.Point(165, 39);
            this.chkVatTu.Name = "chkVatTu";
            this.chkVatTu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVatTu.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.chkVatTu.Properties.Appearance.Options.UseFont = true;
            this.chkVatTu.Properties.Appearance.Options.UseForeColor = true;
            this.chkVatTu.Properties.Caption = "Vật tư";
            this.chkVatTu.Size = new System.Drawing.Size(207, 23);
            this.chkVatTu.TabIndex = 6;
            // 
            // frmChonNhomThuocVatTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 212);
            this.Controls.Add(this.chkVatTu);
            this.Controls.Add(this.chkThuoc);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "frmChonNhomThuocVatTu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xuất danh mục dịch vụ";
            ((System.ComponentModel.ISupportInitialize)(this.chkThuoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVatTu.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.CheckEdit chkThuoc;
        private DevExpress.XtraEditors.CheckEdit chkVatTu;
    }
}