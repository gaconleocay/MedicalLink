namespace MedicalLinkUpdate
{
    partial class Update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Update));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtVersionMecicalLink = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialogShelect = new System.Windows.Forms.OpenFileDialog();
            this.txtUpdateLink = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpdateLink = new System.Windows.Forms.Button();
            this.btnUpdateMedicalLink = new System.Windows.Forms.Button();
            this.btnUpdateLauncher = new System.Windows.Forms.Button();
            this.txtVersionLauncher = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtKeyDangNhap = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(25, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thư mục chứa phần mềm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(25, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Version MedicalLink";
            this.label2.Visible = false;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Location = new System.Drawing.Point(28, 95);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(625, 26);
            this.txtFilePath.TabIndex = 2;
            // 
            // txtVersionMecicalLink
            // 
            this.txtVersionMecicalLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVersionMecicalLink.Location = new System.Drawing.Point(28, 238);
            this.txtVersionMecicalLink.Name = "txtVersionMecicalLink";
            this.txtVersionMecicalLink.Size = new System.Drawing.Size(243, 26);
            this.txtVersionMecicalLink.TabIndex = 3;
            this.txtVersionMecicalLink.Visible = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnBrowse.Location = new System.Drawing.Point(673, 95);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 26);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse…";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtUpdateLink
            // 
            this.txtUpdateLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpdateLink.Location = new System.Drawing.Point(28, 165);
            this.txtUpdateLink.Name = "txtUpdateLink";
            this.txtUpdateLink.Size = new System.Drawing.Size(625, 26);
            this.txtUpdateLink.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(25, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Update link";
            // 
            // btnUpdateLink
            // 
            this.btnUpdateLink.Enabled = false;
            this.btnUpdateLink.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnUpdateLink.Location = new System.Drawing.Point(673, 165);
            this.btnUpdateLink.Name = "btnUpdateLink";
            this.btnUpdateLink.Size = new System.Drawing.Size(100, 26);
            this.btnUpdateLink.TabIndex = 8;
            this.btnUpdateLink.Text = "Update";
            this.btnUpdateLink.UseVisualStyleBackColor = true;
            this.btnUpdateLink.Click += new System.EventHandler(this.btnUpdateLink_Click);
            // 
            // btnUpdateMedicalLink
            // 
            this.btnUpdateMedicalLink.Enabled = false;
            this.btnUpdateMedicalLink.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateMedicalLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnUpdateMedicalLink.Location = new System.Drawing.Point(303, 238);
            this.btnUpdateMedicalLink.Name = "btnUpdateMedicalLink";
            this.btnUpdateMedicalLink.Size = new System.Drawing.Size(100, 26);
            this.btnUpdateMedicalLink.TabIndex = 9;
            this.btnUpdateMedicalLink.Text = "Update";
            this.btnUpdateMedicalLink.UseVisualStyleBackColor = true;
            this.btnUpdateMedicalLink.Visible = false;
            this.btnUpdateMedicalLink.Click += new System.EventHandler(this.btnUpdateMedicalLink_Click);
            // 
            // btnUpdateLauncher
            // 
            this.btnUpdateLauncher.Enabled = false;
            this.btnUpdateLauncher.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateLauncher.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnUpdateLauncher.Location = new System.Drawing.Point(303, 311);
            this.btnUpdateLauncher.Name = "btnUpdateLauncher";
            this.btnUpdateLauncher.Size = new System.Drawing.Size(100, 26);
            this.btnUpdateLauncher.TabIndex = 12;
            this.btnUpdateLauncher.Text = "Update";
            this.btnUpdateLauncher.UseVisualStyleBackColor = true;
            this.btnUpdateLauncher.Visible = false;
            this.btnUpdateLauncher.Click += new System.EventHandler(this.btnUpdateLauncher_Click);
            // 
            // txtVersionLauncher
            // 
            this.txtVersionLauncher.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVersionLauncher.Location = new System.Drawing.Point(28, 311);
            this.txtVersionLauncher.Name = "txtVersionLauncher";
            this.txtVersionLauncher.Size = new System.Drawing.Size(243, 26);
            this.txtVersionLauncher.TabIndex = 11;
            this.txtVersionLauncher.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(25, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Version Launcher";
            this.label4.Visible = false;
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnLogin.Location = new System.Drawing.Point(495, 13);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(100, 26);
            this.btnLogin.TabIndex = 18;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtKeyDangNhap
            // 
            this.txtKeyDangNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeyDangNhap.Location = new System.Drawing.Point(148, 12);
            this.txtKeyDangNhap.Name = "txtKeyDangNhap";
            this.txtKeyDangNhap.PasswordChar = '*';
            this.txtKeyDangNhap.Size = new System.Drawing.Size(334, 26);
            this.txtKeyDangNhap.TabIndex = 17;
            this.txtKeyDangNhap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKeyDangNhap_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(31, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Key đăng nhập";
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 396);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtKeyDangNhap);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnUpdateLauncher);
            this.Controls.Add(this.txtVersionLauncher);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnUpdateMedicalLink);
            this.Controls.Add(this.btnUpdateLink);
            this.Controls.Add(this.txtUpdateLink);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtVersionMecicalLink);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Update";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MedicalLinkUpdateMain";
            this.Load += new System.EventHandler(this.Update_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TextBox txtVersionMecicalLink;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialogShelect;
        private System.Windows.Forms.TextBox txtUpdateLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpdateLink;
        private System.Windows.Forms.Button btnUpdateMedicalLink;
        private System.Windows.Forms.Button btnUpdateLauncher;
        private System.Windows.Forms.TextBox txtVersionLauncher;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtKeyDangNhap;
        private System.Windows.Forms.Label label5;
    }
}