namespace MedicalLink.ChucNang
{
    partial class SuaPhoiThanhToan_SoLuong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuaPhoiThanhToan_SoLuong));
            this.btnSL_OK = new DevExpress.XtraEditors.SimpleButton();
            this.spinSoLuong = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuong.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSL_OK
            // 
            this.btnSL_OK.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSL_OK.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSL_OK.Appearance.Options.UseFont = true;
            this.btnSL_OK.Appearance.Options.UseForeColor = true;
            this.btnSL_OK.Image = global::MedicalLink.Properties.Resources.check_mark_24;
            this.btnSL_OK.Location = new System.Drawing.Point(79, 70);
            this.btnSL_OK.Name = "btnSL_OK";
            this.btnSL_OK.Size = new System.Drawing.Size(100, 40);
            this.btnSL_OK.TabIndex = 3;
            this.btnSL_OK.Text = "OK";
            this.btnSL_OK.Click += new System.EventHandler(this.btnSL_OK_Click);
            // 
            // spinSoLuong
            // 
            this.spinSoLuong.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinSoLuong.Location = new System.Drawing.Point(48, 24);
            this.spinSoLuong.Name = "spinSoLuong";
            this.spinSoLuong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinSoLuong.Properties.Appearance.Options.UseFont = true;
            this.spinSoLuong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinSoLuong.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSoLuong.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSoLuong.Size = new System.Drawing.Size(149, 26);
            this.spinSoLuong.TabIndex = 4;
            this.spinSoLuong.EditValueChanged += new System.EventHandler(this.spinSoLuong_EditValueChanged);
            // 
            // SuaPhoiThanhToan_SoLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 122);
            this.Controls.Add(this.spinSoLuong);
            this.Controls.Add(this.btnSL_OK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(280, 160);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(280, 160);
            this.Name = "SuaPhoiThanhToan_SoLuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhập số lượng";
            this.Load += new System.EventHandler(this.SuaPhoiThanhToan_SoLuong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuong.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSL_OK;
        private DevExpress.XtraEditors.SpinEdit spinSoLuong;
    }
}