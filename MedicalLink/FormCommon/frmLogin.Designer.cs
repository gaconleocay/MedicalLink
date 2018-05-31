namespace MedicalLink.FormCommon
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.checkEditNhoPass = new DevExpress.XtraEditors.CheckEdit();
            this.linkTroGiup = new System.Windows.Forms.LinkLabel();
            this.picture_Logobenhvien = new DevExpress.XtraEditors.PictureEdit();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditNhoPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Logobenhvien.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.EditValue = "";
            this.txtUsername.Location = new System.Drawing.Point(254, 16);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Properties.Appearance.Options.UseFont = true;
            this.txtUsername.Properties.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsername_Properties_KeyDown);
            this.txtUsername.Size = new System.Drawing.Size(156, 24);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.EditValueChanged += new System.EventHandler(this.txtUsername_EditValueChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new System.Drawing.Point(254, 54);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Properties.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_Properties_KeyDown);
            this.txtPassword.Size = new System.Drawing.Size(156, 24);
            this.txtPassword.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(155, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(90, 18);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Tên tài khoản";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl2.Location = new System.Drawing.Point(181, 56);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(61, 18);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Mật khẩu";
            // 
            // checkEditNhoPass
            // 
            this.checkEditNhoPass.Location = new System.Drawing.Point(254, 86);
            this.checkEditNhoPass.Name = "checkEditNhoPass";
            this.checkEditNhoPass.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkEditNhoPass.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkEditNhoPass.Properties.Appearance.Options.UseFont = true;
            this.checkEditNhoPass.Properties.Appearance.Options.UseForeColor = true;
            this.checkEditNhoPass.Properties.Caption = "Ghi nhớ";
            this.checkEditNhoPass.Size = new System.Drawing.Size(109, 22);
            this.checkEditNhoPass.TabIndex = 5;
            // 
            // linkTroGiup
            // 
            this.linkTroGiup.AutoSize = true;
            this.linkTroGiup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkTroGiup.Location = new System.Drawing.Point(21, 157);
            this.linkTroGiup.Name = "linkTroGiup";
            this.linkTroGiup.Size = new System.Drawing.Size(58, 16);
            this.linkTroGiup.TabIndex = 27;
            this.linkTroGiup.TabStop = true;
            this.linkTroGiup.Text = "Trợ giúp";
            this.linkTroGiup.Click += new System.EventHandler(this.linkTroGiup_Click);
            // 
            // picture_Logobenhvien
            // 
            this.picture_Logobenhvien.CausesValidation = false;
            this.picture_Logobenhvien.Cursor = System.Windows.Forms.Cursors.Default;
            this.picture_Logobenhvien.Location = new System.Drawing.Point(8, 9);
            this.picture_Logobenhvien.Name = "picture_Logobenhvien";
            this.picture_Logobenhvien.Properties.AllowFocused = false;
            this.picture_Logobenhvien.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picture_Logobenhvien.Properties.Appearance.Options.UseBackColor = true;
            this.picture_Logobenhvien.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picture_Logobenhvien.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.picture_Logobenhvien.Properties.ShowMenu = false;
            this.picture_Logobenhvien.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picture_Logobenhvien.Properties.ZoomPercent = 12D;
            this.picture_Logobenhvien.Size = new System.Drawing.Size(125, 125);
            this.picture_Logobenhvien.TabIndex = 9;
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Appearance.Options.UseForeColor = true;
            this.btnLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Image")));
            this.btnLogin.Location = new System.Drawing.Point(171, 130);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(110, 40);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 182);
            this.Controls.Add(this.linkTroGiup);
            this.Controls.Add(this.picture_Logobenhvien);
            this.Controls.Add(this.checkEditNhoPass);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(465, 220);
            this.MinimumSize = new System.Drawing.Size(465, 220);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLogin_FormClosed);
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditNhoPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Logobenhvien.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.CheckEdit checkEditNhoPass;
        private DevExpress.XtraEditors.PictureEdit picture_Logobenhvien;
        private System.Windows.Forms.LinkLabel linkTroGiup;
    }
}