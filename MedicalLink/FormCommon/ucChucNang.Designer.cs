namespace MedicalLink.FormCommon
{
    partial class ucChucNang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucChucNang));
            this.xtraTabControlChucNang = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabDSChucNang = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlDSChucNang = new DevExpress.XtraGrid.GridControl();
            this.gridViewDSChucNang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnstt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.permissioncheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlChucNang)).BeginInit();
            this.xtraTabControlChucNang.SuspendLayout();
            this.xtraTabDSChucNang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDSChucNang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSChucNang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControlChucNang
            // 
            this.xtraTabControlChucNang.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.xtraTabControlChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlChucNang.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControlChucNang.Margin = new System.Windows.Forms.Padding(0);
            this.xtraTabControlChucNang.Name = "xtraTabControlChucNang";
            this.xtraTabControlChucNang.SelectedTabPage = this.xtraTabDSChucNang;
            this.xtraTabControlChucNang.Size = new System.Drawing.Size(1096, 613);
            this.xtraTabControlChucNang.TabIndex = 0;
            this.xtraTabControlChucNang.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabDSChucNang});
            this.xtraTabControlChucNang.TabPageWidth = 150;
            this.xtraTabControlChucNang.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlChucNang_SelectedPageChanged);
            this.xtraTabControlChucNang.CloseButtonClick += new System.EventHandler(this.xtraTabControlChucNang_CloseButtonClick);
            // 
            // xtraTabDSChucNang
            // 
            this.xtraTabDSChucNang.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xtraTabDSChucNang.Appearance.Header.ForeColor = System.Drawing.Color.Navy;
            this.xtraTabDSChucNang.Appearance.Header.Options.UseFont = true;
            this.xtraTabDSChucNang.Appearance.Header.Options.UseForeColor = true;
            this.xtraTabDSChucNang.Appearance.PageClient.BackColor = System.Drawing.Color.DarkRed;
            this.xtraTabDSChucNang.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabDSChucNang.Controls.Add(this.layoutControl1);
            this.xtraTabDSChucNang.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabDSChucNang.Image")));
            this.xtraTabDSChucNang.Name = "xtraTabDSChucNang";
            this.xtraTabDSChucNang.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabDSChucNang.Size = new System.Drawing.Size(1090, 582);
            this.xtraTabDSChucNang.Text = "DS Chức năng";
            this.xtraTabDSChucNang.Tooltip = "Danh sách chức năng";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1090, 582);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gridControlDSChucNang);
            this.layoutControl2.Location = new System.Drawing.Point(2, 2);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.Root;
            this.layoutControl2.Size = new System.Drawing.Size(1086, 578);
            this.layoutControl2.TabIndex = 4;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gridControlDSChucNang
            // 
            this.gridControlDSChucNang.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridControlDSChucNang.Location = new System.Drawing.Point(2, 27);
            this.gridControlDSChucNang.MainView = this.gridViewDSChucNang;
            this.gridControlDSChucNang.Name = "gridControlDSChucNang";
            this.gridControlDSChucNang.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridControlDSChucNang.Size = new System.Drawing.Size(1082, 549);
            this.gridControlDSChucNang.TabIndex = 0;
            this.gridControlDSChucNang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDSChucNang});
            // 
            // gridViewDSChucNang
            // 
            this.gridViewDSChucNang.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewDSChucNang.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewDSChucNang.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gridViewDSChucNang.Appearance.Row.Options.UseFont = true;
            this.gridViewDSChucNang.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.gridViewDSChucNang.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridViewDSChucNang.Appearance.SelectedRow.Options.UseFont = true;
            this.gridViewDSChucNang.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridViewDSChucNang.ColumnPanelRowHeight = 25;
            this.gridViewDSChucNang.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnstt,
            this.gridColumn2,
            this.gridColumn3,
            this.permissioncheck,
            this.gridColumn1});
            this.gridViewDSChucNang.GridControl = this.gridControlDSChucNang;
            this.gridViewDSChucNang.Name = "gridViewDSChucNang";
            this.gridViewDSChucNang.OptionsFind.AlwaysVisible = true;
            this.gridViewDSChucNang.OptionsFind.FindNullPrompt = "Từ khóa tìm kiếm...";
            this.gridViewDSChucNang.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewDSChucNang.OptionsView.ShowGroupPanel = false;
            this.gridViewDSChucNang.RowHeight = 30;
            this.gridViewDSChucNang.ViewCaptionHeight = 25;
            this.gridViewDSChucNang.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewDSChucNang_CustomDrawCell);
            this.gridViewDSChucNang.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewDSChucNang_RowCellStyle);
            this.gridViewDSChucNang.DoubleClick += new System.EventHandler(this.gridViewDSChucNang_DoubleClick);
            // 
            // gridColumnstt
            // 
            this.gridColumnstt.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumnstt.AppearanceCell.Options.UseFont = true;
            this.gridColumnstt.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumnstt.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumnstt.AppearanceHeader.Options.UseFont = true;
            this.gridColumnstt.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumnstt.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnstt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnstt.Caption = "STT";
            this.gridColumnstt.FieldName = "stt";
            this.gridColumnstt.Name = "gridColumnstt";
            this.gridColumnstt.OptionsColumn.AllowEdit = false;
            this.gridColumnstt.OptionsColumn.FixedWidth = true;
            this.gridColumnstt.Visible = true;
            this.gridColumnstt.VisibleIndex = 0;
            this.gridColumnstt.Width = 45;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Mã";
            this.gridColumn2.FieldName = "permissioncode";
            this.gridColumn2.MinWidth = 100;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 100;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn3.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Tên chương trình";
            this.gridColumn3.FieldName = "permissionname";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 500;
            // 
            // permissioncheck
            // 
            this.permissioncheck.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.permissioncheck.AppearanceCell.Options.UseFont = true;
            this.permissioncheck.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.permissioncheck.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.permissioncheck.AppearanceHeader.Options.UseFont = true;
            this.permissioncheck.AppearanceHeader.Options.UseForeColor = true;
            this.permissioncheck.AppearanceHeader.Options.UseTextOptions = true;
            this.permissioncheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.permissioncheck.Caption = "Phân quyền";
            this.permissioncheck.ColumnEdit = this.repositoryItemCheckEdit1;
            this.permissioncheck.FieldName = "permissioncheck";
            this.permissioncheck.Name = "permissioncheck";
            this.permissioncheck.OptionsColumn.AllowEdit = false;
            this.permissioncheck.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.permissioncheck.Width = 74;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Ghi chú";
            this.gridColumn1.FieldName = "permissionnote";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 419;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.Root.Location = new System.Drawing.Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1086, 578);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem4.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem4.Control = this.gridControlDSChucNang;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1086, 578);
            this.layoutControlItem4.Text = "DANH SÁCH CHỨC NĂNG";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(131, 20);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1090, 582);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.layoutControl2;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1090, 582);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ucChucNang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControlChucNang);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucChucNang";
            this.Size = new System.Drawing.Size(1096, 613);
            this.Load += new System.EventHandler(this.ucChucNang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlChucNang)).EndInit();
            this.xtraTabControlChucNang.ResumeLayout(false);
            this.xtraTabDSChucNang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDSChucNang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSChucNang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControlChucNang;
        private DevExpress.XtraTab.XtraTabPage xtraTabDSChucNang;
        private DevExpress.XtraGrid.GridControl gridControlDSChucNang;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDSChucNang;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnstt;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn permissioncheck;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;


    }
}
