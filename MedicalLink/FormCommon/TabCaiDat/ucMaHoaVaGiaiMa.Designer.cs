namespace MedicalLink.FormCommon.TabCaiDat
{
    partial class ucMaHoaVaGiaiMa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMaHoaVaGiaiMa));
            this.txtDauVao = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.txtDauRa = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.btnGiaiMa = new DevExpress.XtraEditors.SimpleButton();
            this.btnMaHoa = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtDauVao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDauRa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDauVao
            // 
            this.txtDauVao.Location = new System.Drawing.Point(85, 26);
            this.txtDauVao.Name = "txtDauVao";
            this.txtDauVao.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDauVao.Properties.Appearance.Options.UseFont = true;
            this.txtDauVao.Size = new System.Drawing.Size(550, 100);
            this.txtDauVao.TabIndex = 28;
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl14.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl14.Location = new System.Drawing.Point(22, 61);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(52, 16);
            this.labelControl14.TabIndex = 27;
            this.labelControl14.Text = "Đầu vào:";
            // 
            // txtDauRa
            // 
            this.txtDauRa.Location = new System.Drawing.Point(85, 206);
            this.txtDauRa.Name = "txtDauRa";
            this.txtDauRa.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDauRa.Properties.Appearance.Options.UseFont = true;
            this.txtDauRa.Size = new System.Drawing.Size(550, 100);
            this.txtDauRa.TabIndex = 35;
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl15.Location = new System.Drawing.Point(30, 236);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(44, 16);
            this.labelControl15.TabIndex = 36;
            this.labelControl15.Text = "Đầu ra:";
            // 
            // btnCopy
            // 
            this.btnCopy.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnCopy.Appearance.Options.UseFont = true;
            this.btnCopy.Appearance.Options.UseForeColor = true;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnCopy.Location = new System.Drawing.Point(641, 225);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(40, 55);
            this.btnCopy.TabIndex = 37;
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnGiaiMa
            // 
            this.btnGiaiMa.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGiaiMa.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnGiaiMa.Appearance.Options.UseFont = true;
            this.btnGiaiMa.Appearance.Options.UseForeColor = true;
            this.btnGiaiMa.Location = new System.Drawing.Point(372, 157);
            this.btnGiaiMa.Name = "btnGiaiMa";
            this.btnGiaiMa.Size = new System.Drawing.Size(100, 23);
            this.btnGiaiMa.TabIndex = 39;
            this.btnGiaiMa.Text = "Giải mã";
            this.btnGiaiMa.Click += new System.EventHandler(this.btnGiaiMa_Click);
            // 
            // btnMaHoa
            // 
            this.btnMaHoa.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaHoa.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnMaHoa.Appearance.Options.UseFont = true;
            this.btnMaHoa.Appearance.Options.UseForeColor = true;
            this.btnMaHoa.Location = new System.Drawing.Point(189, 157);
            this.btnMaHoa.Name = "btnMaHoa";
            this.btnMaHoa.Size = new System.Drawing.Size(100, 23);
            this.btnMaHoa.TabIndex = 38;
            this.btnMaHoa.Text = "Mã hóa";
            this.btnMaHoa.Click += new System.EventHandler(this.btnMaHoa_Click);
            // 
            // ucMaHoaVaGiaiMa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGiaiMa);
            this.Controls.Add(this.btnMaHoa);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtDauRa);
            this.Controls.Add(this.labelControl15);
            this.Controls.Add(this.txtDauVao);
            this.Controls.Add(this.labelControl14);
            this.Name = "ucMaHoaVaGiaiMa";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.txtDauVao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDauRa.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtDauVao;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.SimpleButton btnCopy;
        private DevExpress.XtraEditors.MemoEdit txtDauRa;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.SimpleButton btnGiaiMa;
        private DevExpress.XtraEditors.SimpleButton btnMaHoa;
    }
}
