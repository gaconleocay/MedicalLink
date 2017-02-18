namespace MedicalLink.FormCommon.TabTrangChu
{
    partial class ucSettingDatabase
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
            this.components = new System.ComponentModel.Container();
            this.groupBoxDatabase = new System.Windows.Forms.GroupBox();
            this.lblThongBao = new DevExpress.XtraEditors.LabelControl();
            this.btnDBUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.txtDBPort = new DevExpress.XtraEditors.TextEdit();
            this.txtDBName = new DevExpress.XtraEditors.TextEdit();
            this.txtDBPass = new DevExpress.XtraEditors.TextEdit();
            this.txtDBUser = new DevExpress.XtraEditors.TextEdit();
            this.txtDBHost = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnDBLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnDBKiemTra = new DevExpress.XtraEditors.SimpleButton();
            this.timerThongBao = new System.Windows.Forms.Timer(this.components);
            this.groupBoxDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBHost.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxDatabase
            // 
            this.groupBoxDatabase.Controls.Add(this.lblThongBao);
            this.groupBoxDatabase.Controls.Add(this.btnDBUpdate);
            this.groupBoxDatabase.Controls.Add(this.txtDBPort);
            this.groupBoxDatabase.Controls.Add(this.txtDBName);
            this.groupBoxDatabase.Controls.Add(this.txtDBPass);
            this.groupBoxDatabase.Controls.Add(this.txtDBUser);
            this.groupBoxDatabase.Controls.Add(this.txtDBHost);
            this.groupBoxDatabase.Controls.Add(this.labelControl5);
            this.groupBoxDatabase.Controls.Add(this.labelControl4);
            this.groupBoxDatabase.Controls.Add(this.labelControl3);
            this.groupBoxDatabase.Controls.Add(this.labelControl2);
            this.groupBoxDatabase.Controls.Add(this.labelControl1);
            this.groupBoxDatabase.Controls.Add(this.btnDBLuu);
            this.groupBoxDatabase.Controls.Add(this.btnDBKiemTra);
            this.groupBoxDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDatabase.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDatabase.Name = "groupBoxDatabase";
            this.groupBoxDatabase.Size = new System.Drawing.Size(771, 582);
            this.groupBoxDatabase.TabIndex = 16;
            this.groupBoxDatabase.TabStop = false;
            this.groupBoxDatabase.Text = "Chi tiết database";
            // 
            // lblThongBao
            // 
            this.lblThongBao.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblThongBao.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblThongBao.Location = new System.Drawing.Point(264, 232);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(101, 23);
            this.lblThongBao.TabIndex = 31;
            this.lblThongBao.Text = "Thong bao";
            this.lblThongBao.Visible = false;
            // 
            // btnDBUpdate
            // 
            this.btnDBUpdate.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBUpdate.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDBUpdate.Appearance.Options.UseFont = true;
            this.btnDBUpdate.Appearance.Options.UseForeColor = true;
            this.btnDBUpdate.Location = new System.Drawing.Point(419, 146);
            this.btnDBUpdate.Name = "btnDBUpdate";
            this.btnDBUpdate.Size = new System.Drawing.Size(80, 23);
            this.btnDBUpdate.TabIndex = 30;
            this.btnDBUpdate.Text = "Update DB";
            this.btnDBUpdate.Click += new System.EventHandler(this.btnDBUpdate_Click);
            // 
            // txtDBPort
            // 
            this.txtDBPort.EditValue = "";
            this.txtDBPort.Location = new System.Drawing.Point(404, 26);
            this.txtDBPort.Name = "txtDBPort";
            this.txtDBPort.Properties.AllowFocused = false;
            this.txtDBPort.Properties.AllowMouseWheel = false;
            this.txtDBPort.Size = new System.Drawing.Size(200, 20);
            this.txtDBPort.TabIndex = 29;
            // 
            // txtDBName
            // 
            this.txtDBName.EditValue = "";
            this.txtDBName.Location = new System.Drawing.Point(102, 101);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(200, 20);
            this.txtDBName.TabIndex = 26;
            // 
            // txtDBPass
            // 
            this.txtDBPass.EditValue = "";
            this.txtDBPass.Location = new System.Drawing.Point(404, 64);
            this.txtDBPass.Name = "txtDBPass";
            this.txtDBPass.Properties.PasswordChar = '*';
            this.txtDBPass.Size = new System.Drawing.Size(200, 20);
            this.txtDBPass.TabIndex = 25;
            // 
            // txtDBUser
            // 
            this.txtDBUser.EditValue = "";
            this.txtDBUser.Location = new System.Drawing.Point(102, 62);
            this.txtDBUser.Name = "txtDBUser";
            this.txtDBUser.Size = new System.Drawing.Size(200, 20);
            this.txtDBUser.TabIndex = 24;
            // 
            // txtDBHost
            // 
            this.txtDBHost.EditValue = "";
            this.txtDBHost.Location = new System.Drawing.Point(102, 24);
            this.txtDBHost.Name = "txtDBHost";
            this.txtDBHost.Size = new System.Drawing.Size(200, 20);
            this.txtDBHost.TabIndex = 23;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl5.Location = new System.Drawing.Point(21, 104);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(75, 13);
            this.labelControl5.TabIndex = 22;
            this.labelControl5.Text = "Database name";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl4.Location = new System.Drawing.Point(352, 67);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(46, 13);
            this.labelControl4.TabIndex = 21;
            this.labelControl4.Text = "Password";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl3.Location = new System.Drawing.Point(74, 65);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(22, 13);
            this.labelControl3.TabIndex = 20;
            this.labelControl3.Text = "User";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl2.Location = new System.Drawing.Point(74, 27);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(22, 13);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "Host";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(378, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(20, 13);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "Port";
            // 
            // btnDBLuu
            // 
            this.btnDBLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBLuu.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDBLuu.Appearance.Options.UseFont = true;
            this.btnDBLuu.Appearance.Options.UseForeColor = true;
            this.btnDBLuu.Location = new System.Drawing.Point(308, 146);
            this.btnDBLuu.Name = "btnDBLuu";
            this.btnDBLuu.Size = new System.Drawing.Size(80, 23);
            this.btnDBLuu.TabIndex = 17;
            this.btnDBLuu.Text = "Lưu";
            this.btnDBLuu.Click += new System.EventHandler(this.btnDBLuu_Click);
            // 
            // btnDBKiemTra
            // 
            this.btnDBKiemTra.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBKiemTra.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDBKiemTra.Appearance.Options.UseFont = true;
            this.btnDBKiemTra.Appearance.Options.UseForeColor = true;
            this.btnDBKiemTra.Location = new System.Drawing.Point(186, 146);
            this.btnDBKiemTra.Name = "btnDBKiemTra";
            this.btnDBKiemTra.Size = new System.Drawing.Size(80, 23);
            this.btnDBKiemTra.TabIndex = 16;
            this.btnDBKiemTra.Text = "Kiểm tra";
            this.btnDBKiemTra.Click += new System.EventHandler(this.btnDBKiemTra_Click);
            // 
            // timerThongBao
            // 
            this.timerThongBao.Interval = 2000;
            this.timerThongBao.Tick += new System.EventHandler(this.timerThongBao_Tick);
            // 
            // ucSettingDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxDatabase);
            this.Name = "ucSettingDatabase";
            this.Size = new System.Drawing.Size(771, 582);
            this.Load += new System.EventHandler(this.ucSettingDatabase_Load);
            this.groupBoxDatabase.ResumeLayout(false);
            this.groupBoxDatabase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBHost.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDatabase;
        private DevExpress.XtraEditors.SimpleButton btnDBUpdate;
        private DevExpress.XtraEditors.TextEdit txtDBPort;
        private DevExpress.XtraEditors.TextEdit txtDBName;
        private DevExpress.XtraEditors.TextEdit txtDBPass;
        private DevExpress.XtraEditors.TextEdit txtDBUser;
        private DevExpress.XtraEditors.TextEdit txtDBHost;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnDBLuu;
        private DevExpress.XtraEditors.SimpleButton btnDBKiemTra;
        private DevExpress.XtraEditors.LabelControl lblThongBao;
        private System.Windows.Forms.Timer timerThongBao;
    }
}
