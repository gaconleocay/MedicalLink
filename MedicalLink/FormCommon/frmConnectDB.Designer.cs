namespace MedicalLink.FormCommon
{
    partial class frmConnectDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnectDB));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtDBHost = new DevExpress.XtraEditors.TextEdit();
            this.txtDBUser = new DevExpress.XtraEditors.TextEdit();
            this.txtDBPass = new DevExpress.XtraEditors.TextEdit();
            this.txtDBName = new DevExpress.XtraEditors.TextEdit();
            this.btnDBLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnDBKiemTra = new DevExpress.XtraEditors.SimpleButton();
            this.txtDBPort = new DevExpress.XtraEditors.TextEdit();
            this.btnDBUpdate = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBHost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPort.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(70, 54);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(20, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Port";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(68, 24);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(22, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Host";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(68, 84);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(22, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "User";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(44, 116);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(46, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Password";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(15, 147);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(75, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Database name";
            // 
            // txtDBHost
            // 
            this.txtDBHost.EditValue = "";
            this.txtDBHost.Location = new System.Drawing.Point(106, 20);
            this.txtDBHost.Name = "txtDBHost";
            this.txtDBHost.Size = new System.Drawing.Size(248, 20);
            this.txtDBHost.TabIndex = 6;
            // 
            // txtDBUser
            // 
            this.txtDBUser.EditValue = "";
            this.txtDBUser.Location = new System.Drawing.Point(106, 80);
            this.txtDBUser.Name = "txtDBUser";
            this.txtDBUser.Size = new System.Drawing.Size(248, 20);
            this.txtDBUser.TabIndex = 7;
            // 
            // txtDBPass
            // 
            this.txtDBPass.EditValue = "";
            this.txtDBPass.Location = new System.Drawing.Point(106, 112);
            this.txtDBPass.Name = "txtDBPass";
            this.txtDBPass.Properties.PasswordChar = '*';
            this.txtDBPass.Size = new System.Drawing.Size(248, 20);
            this.txtDBPass.TabIndex = 8;
            // 
            // txtDBName
            // 
            this.txtDBName.EditValue = "";
            this.txtDBName.Location = new System.Drawing.Point(106, 144);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(248, 20);
            this.txtDBName.TabIndex = 9;
            // 
            // btnDBLuu
            // 
            this.btnDBLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBLuu.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDBLuu.Appearance.Options.UseFont = true;
            this.btnDBLuu.Appearance.Options.UseForeColor = true;
            this.btnDBLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnDBLuu.Image")));
            this.btnDBLuu.Location = new System.Drawing.Point(138, 191);
            this.btnDBLuu.Name = "btnDBLuu";
            this.btnDBLuu.Size = new System.Drawing.Size(100, 40);
            this.btnDBLuu.TabIndex = 10;
            this.btnDBLuu.Text = "Lưu";
            this.btnDBLuu.Click += new System.EventHandler(this.tbnDBLuu_Click);
            // 
            // btnDBKiemTra
            // 
            this.btnDBKiemTra.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBKiemTra.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDBKiemTra.Appearance.Options.UseFont = true;
            this.btnDBKiemTra.Appearance.Options.UseForeColor = true;
            this.btnDBKiemTra.Image = ((System.Drawing.Image)(resources.GetObject("btnDBKiemTra.Image")));
            this.btnDBKiemTra.Location = new System.Drawing.Point(21, 191);
            this.btnDBKiemTra.Name = "btnDBKiemTra";
            this.btnDBKiemTra.Size = new System.Drawing.Size(100, 40);
            this.btnDBKiemTra.TabIndex = 11;
            this.btnDBKiemTra.Text = "Kiểm Tra";
            this.btnDBKiemTra.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // txtDBPort
            // 
            this.txtDBPort.EditValue = "";
            this.txtDBPort.Location = new System.Drawing.Point(106, 50);
            this.txtDBPort.Name = "txtDBPort";
            this.txtDBPort.Properties.AllowFocused = false;
            this.txtDBPort.Properties.AllowMouseWheel = false;
            this.txtDBPort.Size = new System.Drawing.Size(248, 20);
            this.txtDBPort.TabIndex = 12;
            // 
            // btnDBUpdate
            // 
            this.btnDBUpdate.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBUpdate.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDBUpdate.Appearance.Options.UseFont = true;
            this.btnDBUpdate.Appearance.Options.UseForeColor = true;
            this.btnDBUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnDBUpdate.Image")));
            this.btnDBUpdate.Location = new System.Drawing.Point(255, 191);
            this.btnDBUpdate.Name = "btnDBUpdate";
            this.btnDBUpdate.Size = new System.Drawing.Size(100, 40);
            this.btnDBUpdate.TabIndex = 13;
            this.btnDBUpdate.Text = "Update DB";
            this.btnDBUpdate.Click += new System.EventHandler(this.btnDBUpdate_Click);
            // 
            // frmConnectDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 252);
            this.Controls.Add(this.btnDBUpdate);
            this.Controls.Add(this.txtDBPort);
            this.Controls.Add(this.btnDBKiemTra);
            this.Controls.Add(this.btnDBLuu);
            this.Controls.Add(this.txtDBName);
            this.Controls.Add(this.txtDBPass);
            this.Controls.Add(this.txtDBUser);
            this.Controls.Add(this.txtDBHost);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(390, 290);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(390, 290);
            this.Name = "frmConnectDB";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu Hình Cơ Sở Dữ Liệu";
            this.Load += new System.EventHandler(this.frmConnectDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDBHost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPort.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtDBHost;
        private DevExpress.XtraEditors.TextEdit txtDBUser;
        private DevExpress.XtraEditors.TextEdit txtDBPass;
        private DevExpress.XtraEditors.TextEdit txtDBName;
        private DevExpress.XtraEditors.SimpleButton btnDBLuu;
        private DevExpress.XtraEditors.SimpleButton btnDBKiemTra;
        private DevExpress.XtraEditors.TextEdit txtDBPort;
        private DevExpress.XtraEditors.SimpleButton btnDBUpdate;
    }
}