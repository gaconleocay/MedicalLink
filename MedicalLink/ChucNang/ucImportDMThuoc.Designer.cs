namespace MedicalLink.ChucNang
{
    partial class ucImportDMThuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucImportDMThuoc));
            this.panelControlThongTinNV = new DevExpress.XtraEditors.PanelControl();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtFilePath = new DevExpress.XtraEditors.TextEdit();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.cbbChonKieu = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnNVOK = new DevExpress.XtraEditors.SimpleButton();
            this.openFileDialogSelect = new System.Windows.Forms.OpenFileDialog();
            this.timerThongBao = new System.Windows.Forms.Timer();
            this.gridControlThuoc = new DevExpress.XtraGrid.GridControl();
            this.gridViewThuoc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblThongBao = new DevExpress.XtraEditors.LabelControl();
            this.panelControlDT = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinNV)).BeginInit();
            this.panelControlThongTinNV.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            this.groupBoxAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbbChonKieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlThuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlDT)).BeginInit();
            this.panelControlDT.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControlThongTinNV
            // 
            this.panelControlThongTinNV.Controls.Add(this.groupBoxFile);
            this.panelControlThongTinNV.Controls.Add(this.groupBoxAction);
            this.panelControlThongTinNV.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlThongTinNV.Location = new System.Drawing.Point(0, 0);
            this.panelControlThongTinNV.Name = "panelControlThongTinNV";
            this.panelControlThongTinNV.Size = new System.Drawing.Size(812, 83);
            this.panelControlThongTinNV.TabIndex = 2;
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Controls.Add(this.labelControl1);
            this.groupBoxFile.Controls.Add(this.txtFilePath);
            this.groupBoxFile.Controls.Add(this.btnSelect);
            this.groupBoxFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxFile.Location = new System.Drawing.Point(2, 3);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(430, 72);
            this.groupBoxFile.TabIndex = 8;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "Import from Excel";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(9, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "File path";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(9, 43);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(288, 20);
            this.txtFilePath.TabIndex = 7;
            // 
            // btnSelect
            // 
            this.btnSelect.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSelect.Appearance.Options.UseFont = true;
            this.btnSelect.Appearance.Options.UseForeColor = true;
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(313, 23);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(100, 40);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "Browse…";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.cbbChonKieu);
            this.groupBoxAction.Controls.Add(this.labelControl2);
            this.groupBoxAction.Controls.Add(this.btnNVOK);
            this.groupBoxAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxAction.Location = new System.Drawing.Point(435, 2);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(370, 73);
            this.groupBoxAction.TabIndex = 7;
            this.groupBoxAction.TabStop = false;
            this.groupBoxAction.Text = "Action";
            // 
            // cbbChonKieu
            // 
            this.cbbChonKieu.Location = new System.Drawing.Point(11, 43);
            this.cbbChonKieu.Name = "cbbChonKieu";
            this.cbbChonKieu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbChonKieu.Properties.Items.AddRange(new object[] {
            "Tên thuốc",
            "Mã DM BYT (mã User)",
            "Mã STT thầu BHYT",
            "Năm thầu",
            "Đánh STT ngày SD",
            "Đường dùng",
            "Đóng gói",
            "Số đăng ký",
            "Số lô"});
            this.cbbChonKieu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbbChonKieu.Size = new System.Drawing.Size(225, 20);
            this.cbbChonKieu.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(11, 24);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Kiểu cập nhật";
            // 
            // btnNVOK
            // 
            this.btnNVOK.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNVOK.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnNVOK.Appearance.Options.UseFont = true;
            this.btnNVOK.Appearance.Options.UseForeColor = true;
            this.btnNVOK.Image = ((System.Drawing.Image)(resources.GetObject("btnNVOK.Image")));
            this.btnNVOK.Location = new System.Drawing.Point(259, 23);
            this.btnNVOK.Name = "btnNVOK";
            this.btnNVOK.Size = new System.Drawing.Size(100, 40);
            this.btnNVOK.TabIndex = 7;
            this.btnNVOK.Text = "OK";
            this.btnNVOK.Click += new System.EventHandler(this.btnNVOK_Click);
            // 
            // openFileDialogSelect
            // 
            this.openFileDialogSelect.Filter = "Excel 2003|*.xls|Excel 2007-2013|*.xlsx";
            this.openFileDialogSelect.Title = "Mở file Excel";
            // 
            // timerThongBao
            // 
            this.timerThongBao.Interval = 2000;
            this.timerThongBao.Tick += new System.EventHandler(this.timerThongBao_Tick);
            // 
            // gridControlThuoc
            // 
            this.gridControlThuoc.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.gridControlThuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlThuoc.Location = new System.Drawing.Point(0, 0);
            this.gridControlThuoc.MainView = this.gridViewThuoc;
            this.gridControlThuoc.Name = "gridControlThuoc";
            this.gridControlThuoc.Size = new System.Drawing.Size(812, 511);
            this.gridControlThuoc.TabIndex = 0;
            this.gridControlThuoc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewThuoc});
            // 
            // gridViewThuoc
            // 
            this.gridViewThuoc.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewThuoc.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridViewThuoc.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewThuoc.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridViewThuoc.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewThuoc.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewThuoc.AppearancePrint.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridViewThuoc.AppearancePrint.HeaderPanel.Options.UseForeColor = true;
            this.gridViewThuoc.GridControl = this.gridControlThuoc;
            this.gridViewThuoc.IndicatorWidth = 50;
            this.gridViewThuoc.Name = "gridViewThuoc";
            this.gridViewThuoc.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewThuoc.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewThuoc.OptionsBehavior.Editable = false;
            this.gridViewThuoc.OptionsView.ShowGroupPanel = false;
            this.gridViewThuoc.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewThuoc_CustomDrawRowIndicator);
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // lblThongBao
            // 
            this.lblThongBao.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblThongBao.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblThongBao.Location = new System.Drawing.Point(188, 98);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(149, 23);
            this.lblThongBao.TabIndex = 19;
            this.lblThongBao.Text = "Sửa thành công";
            this.lblThongBao.Visible = false;
            // 
            // panelControlDT
            // 
            this.panelControlDT.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlDT.Controls.Add(this.lblThongBao);
            this.panelControlDT.Controls.Add(this.gridControlThuoc);
            this.panelControlDT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlDT.Location = new System.Drawing.Point(0, 83);
            this.panelControlDT.Margin = new System.Windows.Forms.Padding(0);
            this.panelControlDT.Name = "panelControlDT";
            this.panelControlDT.Size = new System.Drawing.Size(812, 511);
            this.panelControlDT.TabIndex = 3;
            // 
            // ucImportDMThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlDT);
            this.Controls.Add(this.panelControlThongTinNV);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucImportDMThuoc";
            this.Size = new System.Drawing.Size(812, 594);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinNV)).EndInit();
            this.panelControlThongTinNV.ResumeLayout(false);
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            this.groupBoxAction.ResumeLayout(false);
            this.groupBoxAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbbChonKieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlThuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlDT)).EndInit();
            this.panelControlDT.ResumeLayout(false);
            this.panelControlDT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlThongTinNV;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private DevExpress.XtraEditors.SimpleButton btnNVOK;
        private System.Windows.Forms.OpenFileDialog openFileDialogSelect;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cbbChonKieu;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtFilePath;
        private System.Windows.Forms.Timer timerThongBao;
        private DevExpress.XtraGrid.GridControl gridControlThuoc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewThuoc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl lblThongBao;
        private DevExpress.XtraEditors.PanelControl panelControlDT;
    }
}
