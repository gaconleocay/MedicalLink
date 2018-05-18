namespace MedicalLink.BCQLTaiChinh
{
    partial class frmBC106_NhapTruThue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBC106_NhapTruThue));
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txttongchi = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txttongchi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl3.Location = new System.Drawing.Point(23, 40);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(104, 16);
            this.labelControl3.TabIndex = 21;
            this.labelControl3.Text = "Nhập tiền trừ thuế";
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Appearance.Options.UseForeColor = true;
            this.btnOK.Image = global::MedicalLink.Properties.Resources.check_mark_16;
            this.btnOK.Location = new System.Drawing.Point(115, 147);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 30);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txttongchi
            // 
            this.txttongchi.EditValue = "";
            this.txttongchi.Location = new System.Drawing.Point(72, 85);
            this.txttongchi.Name = "txttongchi";
            this.txttongchi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttongchi.Properties.Appearance.Options.UseFont = true;
            this.txttongchi.Properties.Appearance.Options.UseTextOptions = true;
            this.txttongchi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txttongchi.Properties.DisplayFormat.FormatString = "#,##0";
            this.txttongchi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.txttongchi.Properties.Mask.EditMask = "n0";
            this.txttongchi.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txttongchi.Size = new System.Drawing.Size(204, 30);
            this.txttongchi.TabIndex = 1;
            // 
            // frmBC106_NhapTruThue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 212);
            this.Controls.Add(this.txttongchi);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelControl3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBC106_NhapTruThue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập tiền trừ thuế";
            ((System.ComponentModel.ISupportInitialize)(this.txttongchi.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.TextEdit txttongchi;
    }
}