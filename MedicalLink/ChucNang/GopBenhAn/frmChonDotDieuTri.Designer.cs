namespace MedicalLink.ChucNang.GopBenhAn
{
    partial class frmChonDotDieuTri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChonDotDieuTri));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLuuLai = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cboMaDieuTri = new DevExpress.XtraEditors.LookUpEdit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboMaDieuTri.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(12, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 16);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Mã điều trị:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLuuLai);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 42);
            this.panel1.TabIndex = 7;
            // 
            // btnLuuLai
            // 
            this.btnLuuLai.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuLai.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnLuuLai.Appearance.Options.UseFont = true;
            this.btnLuuLai.Appearance.Options.UseForeColor = true;
            this.btnLuuLai.Image = global::MedicalLink.Properties.Resources.save_16;
            this.btnLuuLai.Location = new System.Drawing.Point(216, 6);
            this.btnLuuLai.Name = "btnLuuLai";
            this.btnLuuLai.Size = new System.Drawing.Size(100, 30);
            this.btnLuuLai.TabIndex = 8;
            this.btnLuuLai.Text = "Lưu";
            this.btnLuuLai.Click += new System.EventHandler(this.btnLuuLai_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cboMaDieuTri);
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 120);
            this.panel2.TabIndex = 8;
            // 
            // cboMaDieuTri
            // 
            this.cboMaDieuTri.Location = new System.Drawing.Point(84, 29);
            this.cboMaDieuTri.Name = "cboMaDieuTri";
            this.cboMaDieuTri.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaDieuTri.Properties.Appearance.Options.UseFont = true;
            this.cboMaDieuTri.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cboMaDieuTri.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboMaDieuTri.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cboMaDieuTri.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.cboMaDieuTri.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMaDieuTri.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("medicalrecordid", 50, "Mã điều trị"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("departmentgroupname", 130, "Tên khoa"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("departmentname", 130, "Tên phòng")});
            this.cboMaDieuTri.Properties.NullText = "";
            this.cboMaDieuTri.Size = new System.Drawing.Size(465, 26);
            this.cboMaDieuTri.TabIndex = 7;
            // 
            // frmChonDotDieuTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 162);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 200);
            this.Name = "frmChonDotDieuTri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn đợt điều trị";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboMaDieuTri.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnLuuLai;
        private DevExpress.XtraEditors.LookUpEdit cboMaDieuTri;
    }
}