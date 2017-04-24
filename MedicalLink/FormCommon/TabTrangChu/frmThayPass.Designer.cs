namespace MedicalLink.FormCommon.TabTrangChu
{
    partial class frmThayPass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThayPass));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtPasswordOld = new DevExpress.XtraEditors.TextEdit();
            this.txtPasswordNew2 = new DevExpress.XtraEditors.TextEdit();
            this.txtPasswordNew1 = new DevExpress.XtraEditors.TextEdit();
            this.btnThayDoi = new DevExpress.XtraEditors.SimpleButton();
            this.checkEditHienThi = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblTenUserDN = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordOld.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordNew2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordNew1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditHienThi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(63, 65);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(69, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mật khẩu cũ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl2.Location = new System.Drawing.Point(55, 99);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(77, 16);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Mật khẩu mới";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl3.Location = new System.Drawing.Point(29, 136);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(103, 16);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Nhập lại mật khẩu";
            // 
            // txtPasswordOld
            // 
            this.txtPasswordOld.Location = new System.Drawing.Point(146, 59);
            this.txtPasswordOld.Name = "txtPasswordOld";
            this.txtPasswordOld.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswordOld.Properties.Appearance.Options.UseFont = true;
            this.txtPasswordOld.Properties.PasswordChar = '*';
            this.txtPasswordOld.Size = new System.Drawing.Size(230, 26);
            this.txtPasswordOld.TabIndex = 3;
            this.txtPasswordOld.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPasswordOld_KeyDown);
            // 
            // txtPasswordNew2
            // 
            this.txtPasswordNew2.Location = new System.Drawing.Point(146, 130);
            this.txtPasswordNew2.Name = "txtPasswordNew2";
            this.txtPasswordNew2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswordNew2.Properties.Appearance.Options.UseFont = true;
            this.txtPasswordNew2.Properties.PasswordChar = '*';
            this.txtPasswordNew2.Size = new System.Drawing.Size(230, 26);
            this.txtPasswordNew2.TabIndex = 4;
            this.txtPasswordNew2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPasswordNew2_KeyDown);
            // 
            // txtPasswordNew1
            // 
            this.txtPasswordNew1.Location = new System.Drawing.Point(146, 93);
            this.txtPasswordNew1.Name = "txtPasswordNew1";
            this.txtPasswordNew1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPasswordNew1.Properties.Appearance.Options.UseFont = true;
            this.txtPasswordNew1.Properties.PasswordChar = '*';
            this.txtPasswordNew1.Size = new System.Drawing.Size(230, 26);
            this.txtPasswordNew1.TabIndex = 5;
            this.txtPasswordNew1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPasswordNew1_KeyDown);
            // 
            // btnThayDoi
            // 
            this.btnThayDoi.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThayDoi.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnThayDoi.Appearance.Options.UseFont = true;
            this.btnThayDoi.Appearance.Options.UseForeColor = true;
            this.btnThayDoi.Image = ((System.Drawing.Image)(resources.GetObject("btnThayDoi.Image")));
            this.btnThayDoi.Location = new System.Drawing.Point(161, 210);
            this.btnThayDoi.Name = "btnThayDoi";
            this.btnThayDoi.Size = new System.Drawing.Size(100, 40);
            this.btnThayDoi.TabIndex = 6;
            this.btnThayDoi.Text = "Lưu";
            this.btnThayDoi.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // checkEditHienThi
            // 
            this.checkEditHienThi.Location = new System.Drawing.Point(146, 172);
            this.checkEditHienThi.Name = "checkEditHienThi";
            this.checkEditHienThi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkEditHienThi.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkEditHienThi.Properties.Appearance.Options.UseFont = true;
            this.checkEditHienThi.Properties.Appearance.Options.UseForeColor = true;
            this.checkEditHienThi.Properties.Caption = "Hiển thị mật khẩu";
            this.checkEditHienThi.Size = new System.Drawing.Size(208, 20);
            this.checkEditHienThi.TabIndex = 7;
            this.checkEditHienThi.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl4.Location = new System.Drawing.Point(76, 14);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(56, 16);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Tài khoản";
            // 
            // lblTenUserDN
            // 
            this.lblTenUserDN.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenUserDN.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTenUserDN.Location = new System.Drawing.Point(146, 14);
            this.lblTenUserDN.Name = "lblTenUserDN";
            this.lblTenUserDN.Size = new System.Drawing.Size(55, 16);
            this.lblTenUserDN.TabIndex = 9;
            this.lblTenUserDN.Text = "Not user";
            // 
            // frmThayPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 262);
            this.Controls.Add(this.lblTenUserDN);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.checkEditHienThi);
            this.Controls.Add(this.btnThayDoi);
            this.Controls.Add(this.txtPasswordNew1);
            this.Controls.Add(this.txtPasswordNew2);
            this.Controls.Add(this.txtPasswordOld);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(450, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 300);
            this.Name = "frmThayPass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thay đổi mật khẩu";
            this.Load += new System.EventHandler(this.frmThayPass_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordOld.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordNew2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPasswordNew1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditHienThi.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtPasswordOld;
        private DevExpress.XtraEditors.TextEdit txtPasswordNew2;
        private DevExpress.XtraEditors.TextEdit txtPasswordNew1;
        private DevExpress.XtraEditors.SimpleButton btnThayDoi;
        private DevExpress.XtraEditors.CheckEdit checkEditHienThi;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblTenUserDN;
    }
}