namespace MedicalLinkUpdate
{
    partial class FormUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpdate));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.btnCommit = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.textFilePath = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeViewSelect = new System.Windows.Forms.TreeView();
            this.folderBrowserDialogSelect = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Controls.Add(this.btnCommit);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.textFilePath);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 85);
            this.panel1.TabIndex = 0;
            // 
            // lblVersion
            // 
            this.lblVersion.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblVersion.Location = new System.Drawing.Point(543, 40);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(67, 19);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "Version:";
            // 
            // btnCommit
            // 
            this.btnCommit.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCommit.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnCommit.Appearance.Options.UseFont = true;
            this.btnCommit.Appearance.Options.UseForeColor = true;
            this.btnCommit.Image = ((System.Drawing.Image)(resources.GetObject("btnCommit.Image")));
            this.btnCommit.Location = new System.Drawing.Point(845, 38);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(100, 23);
            this.btnCommit.TabIndex = 8;
            this.btnCommit.Text = "Commit";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(12, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(120, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Thư mục chứa phần mềm";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnBrowse.Appearance.Options.UseFont = true;
            this.btnBrowse.Appearance.Options.UseForeColor = true;
            this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
            this.btnBrowse.Location = new System.Drawing.Point(388, 38);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse…";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // textFilePath
            // 
            this.textFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFilePath.Location = new System.Drawing.Point(12, 38);
            this.textFilePath.Name = "textFilePath";
            this.textFilePath.Size = new System.Drawing.Size(360, 24);
            this.textFilePath.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeViewSelect);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 85);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 477);
            this.panel2.TabIndex = 1;
            // 
            // treeViewSelect
            // 
            this.treeViewSelect.CheckBoxes = true;
            this.treeViewSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSelect.Location = new System.Drawing.Point(0, 0);
            this.treeViewSelect.Name = "treeViewSelect";
            this.treeViewSelect.Size = new System.Drawing.Size(984, 477);
            this.treeViewSelect.TabIndex = 0;
            this.treeViewSelect.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewSelect_BeforeExpand);
            this.treeViewSelect.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewSelect_BeforeSelect);
            // 
            // FormUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update version";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private System.Windows.Forms.TextBox textFilePath;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCommit;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogSelect;
        private System.Windows.Forms.TreeView treeViewSelect;

    }
}

