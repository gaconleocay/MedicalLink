namespace MedicalLink.FormCommon
{
    partial class ucQLTaiChinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucQLTaiChinh));
            this.xtraTabControlBaoCao = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabDSBaoCao = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControlDSBaoCao = new DevExpress.XtraGrid.GridControl();
            this.gridViewDSBaoCao = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridDSBCColumeStt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.permissioncheck1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlBaoCao)).BeginInit();
            this.xtraTabControlBaoCao.SuspendLayout();
            this.xtraTabDSBaoCao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDSBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControlBaoCao
            // 
            this.xtraTabControlBaoCao.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.xtraTabControlBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlBaoCao.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControlBaoCao.Margin = new System.Windows.Forms.Padding(0);
            this.xtraTabControlBaoCao.Name = "xtraTabControlBaoCao";
            this.xtraTabControlBaoCao.SelectedTabPage = this.xtraTabDSBaoCao;
            this.xtraTabControlBaoCao.Size = new System.Drawing.Size(1096, 613);
            this.xtraTabControlBaoCao.TabIndex = 0;
            this.xtraTabControlBaoCao.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabDSBaoCao});
            this.xtraTabControlBaoCao.TabPageWidth = 150;
            this.xtraTabControlBaoCao.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlChucNang_SelectedPageChanged);
            this.xtraTabControlBaoCao.CloseButtonClick += new System.EventHandler(this.xtraTabControlChucNang_CloseButtonClick);
            // 
            // xtraTabDSBaoCao
            // 
            this.xtraTabDSBaoCao.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xtraTabDSBaoCao.Appearance.Header.ForeColor = System.Drawing.Color.Navy;
            this.xtraTabDSBaoCao.Appearance.Header.Options.UseFont = true;
            this.xtraTabDSBaoCao.Appearance.Header.Options.UseForeColor = true;
            this.xtraTabDSBaoCao.Appearance.PageClient.BackColor = System.Drawing.Color.DarkRed;
            this.xtraTabDSBaoCao.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabDSBaoCao.Controls.Add(this.layoutControl1);
            this.xtraTabDSBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabDSBaoCao.Image")));
            this.xtraTabDSBaoCao.Name = "xtraTabDSBaoCao";
            this.xtraTabDSBaoCao.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabDSBaoCao.Size = new System.Drawing.Size(1090, 582);
            this.xtraTabDSBaoCao.Text = "DS Báo cáo";
            this.xtraTabDSBaoCao.Tooltip = "Danh sách báo cáo";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.layoutControl3);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1090, 582);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.gridControlDSBaoCao);
            this.layoutControl3.Location = new System.Drawing.Point(2, 2);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup2;
            this.layoutControl3.Size = new System.Drawing.Size(1086, 578);
            this.layoutControl3.TabIndex = 5;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // gridControlDSBaoCao
            // 
            this.gridControlDSBaoCao.Location = new System.Drawing.Point(2, 27);
            this.gridControlDSBaoCao.MainView = this.gridViewDSBaoCao;
            this.gridControlDSBaoCao.Name = "gridControlDSBaoCao";
            this.gridControlDSBaoCao.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.gridControlDSBaoCao.Size = new System.Drawing.Size(1082, 549);
            this.gridControlDSBaoCao.TabIndex = 4;
            this.gridControlDSBaoCao.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDSBaoCao});
            // 
            // gridViewDSBaoCao
            // 
            this.gridViewDSBaoCao.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewDSBaoCao.Appearance.Row.Options.UseFont = true;
            this.gridViewDSBaoCao.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewDSBaoCao.Appearance.SelectedRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridViewDSBaoCao.Appearance.SelectedRow.Options.UseFont = true;
            this.gridViewDSBaoCao.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridViewDSBaoCao.ColumnPanelRowHeight = 25;
            this.gridViewDSBaoCao.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridDSBCColumeStt,
            this.gridColumn7,
            this.gridColumn8,
            this.permissioncheck1,
            this.gridColumn4});
            this.gridViewDSBaoCao.GridControl = this.gridControlDSBaoCao;
            this.gridViewDSBaoCao.Name = "gridViewDSBaoCao";
            this.gridViewDSBaoCao.OptionsFind.AlwaysVisible = true;
            this.gridViewDSBaoCao.OptionsFind.FindNullPrompt = "Từ khóa tìm kiếm...";
            this.gridViewDSBaoCao.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewDSBaoCao.OptionsView.ShowGroupPanel = false;
            this.gridViewDSBaoCao.RowHeight = 30;
            this.gridViewDSBaoCao.ViewCaptionHeight = 25;
            this.gridViewDSBaoCao.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewDSBaoCao_CustomDrawCell);
            this.gridViewDSBaoCao.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewDSBaoCao_RowCellStyle);
            this.gridViewDSBaoCao.DoubleClick += new System.EventHandler(this.gridViewDSBaoCao_DoubleClick);
            // 
            // gridDSBCColumeStt
            // 
            this.gridDSBCColumeStt.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridDSBCColumeStt.AppearanceCell.Options.UseFont = true;
            this.gridDSBCColumeStt.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridDSBCColumeStt.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridDSBCColumeStt.AppearanceHeader.Options.UseFont = true;
            this.gridDSBCColumeStt.AppearanceHeader.Options.UseForeColor = true;
            this.gridDSBCColumeStt.AppearanceHeader.Options.UseTextOptions = true;
            this.gridDSBCColumeStt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridDSBCColumeStt.Caption = "STT";
            this.gridDSBCColumeStt.FieldName = "stt";
            this.gridDSBCColumeStt.Name = "gridDSBCColumeStt";
            this.gridDSBCColumeStt.OptionsColumn.AllowEdit = false;
            this.gridDSBCColumeStt.OptionsColumn.FixedWidth = true;
            this.gridDSBCColumeStt.Visible = true;
            this.gridDSBCColumeStt.VisibleIndex = 0;
            this.gridDSBCColumeStt.Width = 45;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn7.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Mã";
            this.gridColumn7.FieldName = "permissioncode";
            this.gridColumn7.MinWidth = 100;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.FixedWidth = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 100;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn8.AppearanceCell.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn8.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "Tên chương trình";
            this.gridColumn8.FieldName = "permissionname";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 500;
            // 
            // permissioncheck1
            // 
            this.permissioncheck1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.permissioncheck1.AppearanceCell.Options.UseFont = true;
            this.permissioncheck1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.permissioncheck1.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.permissioncheck1.AppearanceHeader.Options.UseFont = true;
            this.permissioncheck1.AppearanceHeader.Options.UseForeColor = true;
            this.permissioncheck1.AppearanceHeader.Options.UseTextOptions = true;
            this.permissioncheck1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.permissioncheck1.Caption = "Phân quyền";
            this.permissioncheck1.ColumnEdit = this.repositoryItemCheckEdit2;
            this.permissioncheck1.FieldName = "permissioncheck";
            this.permissioncheck1.Name = "permissioncheck1";
            this.permissioncheck1.OptionsColumn.AllowEdit = false;
            this.permissioncheck1.OptionsColumn.FixedWidth = true;
            this.permissioncheck1.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.permissioncheck1.Width = 70;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn4.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Ghi chú";
            this.gridColumn4.FieldName = "permissionnote";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 419;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1086, 578);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlItem5.AppearanceItemCaption.ForeColor = System.Drawing.Color.Blue;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseForeColor = true;
            this.layoutControlItem5.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem5.Control = this.gridControlDSBaoCao;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1086, 578);
            this.layoutControlItem5.Text = "DANH SÁCH BÁO CÁO";
            this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(49, 20);
            this.layoutControlItem5.TextToControlDistance = 5;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1090, 582);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.layoutControl3;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1090, 582);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ucQLTaiChinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControlBaoCao);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucQLTaiChinh";
            this.Size = new System.Drawing.Size(1096, 613);
            this.Load += new System.EventHandler(this.ucQLTaiChinh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlBaoCao)).EndInit();
            this.xtraTabControlBaoCao.ResumeLayout(false);
            this.xtraTabDSBaoCao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDSBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControlBaoCao;
        private DevExpress.XtraTab.XtraTabPage xtraTabDSBaoCao;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gridControlDSBaoCao;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDSBaoCao;
        private DevExpress.XtraGrid.Columns.GridColumn gridDSBCColumeStt;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn permissioncheck1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;


    }
}
