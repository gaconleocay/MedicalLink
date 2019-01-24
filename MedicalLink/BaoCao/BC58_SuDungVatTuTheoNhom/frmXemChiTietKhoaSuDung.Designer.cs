namespace MedicalLink.BaoCao.BC58_SuDungVatTuTheoNhom
{
    partial class frmXemChiTietKhoaSuDung
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXemChiTietKhoaSuDung));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbnExport = new DevExpress.XtraEditors.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridControlBaoCao = new DevExpress.XtraGrid.GridControl();
            this.gridViewBaoCao = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_noitru_sl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_noitru_thanhtien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_tutruc_thanhtien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_ton_thanhtien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButton_XemCT = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButton_XemCT)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbnExport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 50);
            this.panel1.TabIndex = 0;
            // 
            // tbnExport
            // 
            this.tbnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbnExport.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tbnExport.Appearance.Options.UseFont = true;
            this.tbnExport.Appearance.Options.UseForeColor = true;
            this.tbnExport.Image = global::MedicalLink.Properties.Resources.excel_3_16;
            this.tbnExport.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.tbnExport.Location = new System.Drawing.Point(12, 10);
            this.tbnExport.Name = "tbnExport";
            this.tbnExport.Size = new System.Drawing.Size(100, 30);
            this.tbnExport.TabIndex = 11;
            this.tbnExport.Text = "Xuất file";
            this.tbnExport.Click += new System.EventHandler(this.tbnExport_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gridControlBaoCao);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 512);
            this.panel2.TabIndex = 1;
            // 
            // gridControlBaoCao
            // 
            this.gridControlBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlBaoCao.Location = new System.Drawing.Point(0, 0);
            this.gridControlBaoCao.MainView = this.gridViewBaoCao;
            this.gridControlBaoCao.Name = "gridControlBaoCao";
            this.gridControlBaoCao.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButton_XemCT});
            this.gridControlBaoCao.Size = new System.Drawing.Size(1008, 512);
            this.gridControlBaoCao.TabIndex = 1;
            this.gridControlBaoCao.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBaoCao});
            // 
            // gridViewBaoCao
            // 
            this.gridViewBaoCao.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewBaoCao.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridViewBaoCao.Appearance.FooterPanel.Options.UseFont = true;
            this.gridViewBaoCao.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridViewBaoCao.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gridViewBaoCao.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridViewBaoCao.Appearance.GroupButton.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridViewBaoCao.Appearance.GroupButton.Options.UseFont = true;
            this.gridViewBaoCao.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewBaoCao.Appearance.GroupFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridViewBaoCao.Appearance.GroupFooter.Options.UseFont = true;
            this.gridViewBaoCao.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gridViewBaoCao.Appearance.GroupFooter.Options.UseTextOptions = true;
            this.gridViewBaoCao.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridViewBaoCao.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gridViewBaoCao.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewBaoCao.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridViewBaoCao.Appearance.GroupRow.Options.UseBackColor = true;
            this.gridViewBaoCao.Appearance.GroupRow.Options.UseFont = true;
            this.gridViewBaoCao.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridViewBaoCao.ColumnPanelRowHeight = 25;
            this.gridViewBaoCao.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn4,
            this.gridColumn_noitru_sl,
            this.gridColumn_noitru_thanhtien,
            this.gridColumn8,
            this.gridColumn_tutruc_thanhtien,
            this.gridColumn10,
            this.gridColumn_ton_thanhtien});
            this.gridViewBaoCao.GridControl = this.gridControlBaoCao;
            this.gridViewBaoCao.GroupRowHeight = 25;
            this.gridViewBaoCao.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "noitru_thanhtien", this.gridColumn_noitru_thanhtien, "{0:#,##0 }"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "tutruc_thanhtien", this.gridColumn_tutruc_thanhtien, "{0:#,##0 }"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ton_thanhtien", this.gridColumn_ton_thanhtien, "{0:#,##0 }")});
            this.gridViewBaoCao.Name = "gridViewBaoCao";
            this.gridViewBaoCao.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewBaoCao.OptionsFind.AlwaysVisible = true;
            this.gridViewBaoCao.OptionsFind.FindNullPrompt = "Từ khóa tìm kiếm...";
            this.gridViewBaoCao.OptionsView.ColumnAutoWidth = false;
            this.gridViewBaoCao.OptionsView.RowAutoHeight = true;
            this.gridViewBaoCao.OptionsView.ShowFooter = true;
            this.gridViewBaoCao.OptionsView.ShowGroupPanel = false;
            this.gridViewBaoCao.OptionsView.ShowIndicator = false;
            this.gridViewBaoCao.RowHeight = 25;
            this.gridViewBaoCao.ViewCaptionHeight = 25;
            this.gridViewBaoCao.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewBaoCao_RowCellStyle);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn1.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "STT";
            this.gridColumn1.FieldName = "stt";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 40;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn4.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Tên khoa";
            this.gridColumn4.FieldName = "departmentgroupname";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 255;
            // 
            // gridColumn_noitru_sl
            // 
            this.gridColumn_noitru_sl.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn_noitru_sl.AppearanceCell.Options.UseFont = true;
            this.gridColumn_noitru_sl.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn_noitru_sl.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn_noitru_sl.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn_noitru_sl.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn_noitru_sl.AppearanceHeader.Options.UseFont = true;
            this.gridColumn_noitru_sl.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn_noitru_sl.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_noitru_sl.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_noitru_sl.Caption = "SL nội trú";
            this.gridColumn_noitru_sl.DisplayFormat.FormatString = "#,##0.0";
            this.gridColumn_noitru_sl.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn_noitru_sl.FieldName = "noitru_sl";
            this.gridColumn_noitru_sl.Name = "gridColumn_noitru_sl";
            this.gridColumn_noitru_sl.OptionsColumn.AllowEdit = false;
            this.gridColumn_noitru_sl.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "noitru_sl", "{0:#,##0.0}")});
            this.gridColumn_noitru_sl.Visible = true;
            this.gridColumn_noitru_sl.VisibleIndex = 2;
            this.gridColumn_noitru_sl.Width = 120;
            // 
            // gridColumn_noitru_thanhtien
            // 
            this.gridColumn_noitru_thanhtien.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn_noitru_thanhtien.AppearanceCell.Options.UseFont = true;
            this.gridColumn_noitru_thanhtien.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn_noitru_thanhtien.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn_noitru_thanhtien.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn_noitru_thanhtien.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn_noitru_thanhtien.AppearanceHeader.Options.UseFont = true;
            this.gridColumn_noitru_thanhtien.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn_noitru_thanhtien.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_noitru_thanhtien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_noitru_thanhtien.Caption = "Thành tiền nội trú";
            this.gridColumn_noitru_thanhtien.DisplayFormat.FormatString = "#,##0";
            this.gridColumn_noitru_thanhtien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn_noitru_thanhtien.FieldName = "noitru_thanhtien";
            this.gridColumn_noitru_thanhtien.Name = "gridColumn_noitru_thanhtien";
            this.gridColumn_noitru_thanhtien.OptionsColumn.AllowEdit = false;
            this.gridColumn_noitru_thanhtien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "noitru_thanhtien", "{0:#,##0}")});
            this.gridColumn_noitru_thanhtien.Visible = true;
            this.gridColumn_noitru_thanhtien.VisibleIndex = 3;
            this.gridColumn_noitru_thanhtien.Width = 140;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn8.AppearanceCell.Options.UseFont = true;
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn8.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn8.Caption = "Sl tủ trực";
            this.gridColumn8.DisplayFormat.FormatString = "#,##0.0";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn8.FieldName = "tutruc_sl";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "tutruc_sl", "{0:#,##0.0}")});
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 120;
            // 
            // gridColumn_tutruc_thanhtien
            // 
            this.gridColumn_tutruc_thanhtien.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn_tutruc_thanhtien.AppearanceCell.Options.UseFont = true;
            this.gridColumn_tutruc_thanhtien.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn_tutruc_thanhtien.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn_tutruc_thanhtien.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn_tutruc_thanhtien.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn_tutruc_thanhtien.AppearanceHeader.Options.UseFont = true;
            this.gridColumn_tutruc_thanhtien.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn_tutruc_thanhtien.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_tutruc_thanhtien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_tutruc_thanhtien.Caption = "Thành tiền tủ trực";
            this.gridColumn_tutruc_thanhtien.DisplayFormat.FormatString = "#,##0";
            this.gridColumn_tutruc_thanhtien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn_tutruc_thanhtien.FieldName = "tutruc_thanhtien";
            this.gridColumn_tutruc_thanhtien.Name = "gridColumn_tutruc_thanhtien";
            this.gridColumn_tutruc_thanhtien.OptionsColumn.AllowEdit = false;
            this.gridColumn_tutruc_thanhtien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "tutruc_thanhtien", "{0:#,##0}")});
            this.gridColumn_tutruc_thanhtien.Visible = true;
            this.gridColumn_tutruc_thanhtien.VisibleIndex = 5;
            this.gridColumn_tutruc_thanhtien.Width = 140;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn10.AppearanceCell.Options.UseFont = true;
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn10.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "Tổng số lượng";
            this.gridColumn10.DisplayFormat.FormatString = "#,##0.0";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn10.FieldName = "tong_sl";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "tong_sl", "{0:#,##0.0}")});
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 6;
            this.gridColumn10.Width = 120;
            // 
            // gridColumn_ton_thanhtien
            // 
            this.gridColumn_ton_thanhtien.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn_ton_thanhtien.AppearanceCell.Options.UseFont = true;
            this.gridColumn_ton_thanhtien.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn_ton_thanhtien.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn_ton_thanhtien.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn_ton_thanhtien.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn_ton_thanhtien.AppearanceHeader.Options.UseFont = true;
            this.gridColumn_ton_thanhtien.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn_ton_thanhtien.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn_ton_thanhtien.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn_ton_thanhtien.Caption = "Tổng thành tiền";
            this.gridColumn_ton_thanhtien.DisplayFormat.FormatString = "#,##0";
            this.gridColumn_ton_thanhtien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn_ton_thanhtien.FieldName = "tong_thanhtien";
            this.gridColumn_ton_thanhtien.Name = "gridColumn_ton_thanhtien";
            this.gridColumn_ton_thanhtien.OptionsColumn.AllowEdit = false;
            this.gridColumn_ton_thanhtien.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "tong_thanhtien", "{0:#,##0}")});
            this.gridColumn_ton_thanhtien.Visible = true;
            this.gridColumn_ton_thanhtien.VisibleIndex = 7;
            this.gridColumn_ton_thanhtien.Width = 140;
            // 
            // repositoryItemButton_XemCT
            // 
            this.repositoryItemButton_XemCT.AutoHeight = false;
            this.repositoryItemButton_XemCT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::MedicalLink.Properties.Resources.arrow_32_16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Xem chi tiết", null, null, true)});
            this.repositoryItemButton_XemCT.Name = "repositoryItemButton_XemCT";
            this.repositoryItemButton_XemCT.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // frmXemChiTietKhoaSuDung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 562);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmXemChiTietKhoaSuDung";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem chi tiết các khoa sử dụng";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBaoCao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButton_XemCT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton tbnExport;
        private DevExpress.XtraGrid.GridControl gridControlBaoCao;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBaoCao;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_noitru_sl;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_noitru_thanhtien;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_tutruc_thanhtien;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_ton_thanhtien;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButton_XemCT;
    }
}