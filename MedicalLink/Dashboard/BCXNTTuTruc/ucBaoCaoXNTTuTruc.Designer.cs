namespace MedicalLink.Dashboard
{
    partial class ucBaoCaoXNTTuTruc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBaoCaoXNTTuTruc));
            this.panelControlMenu = new DevExpress.XtraEditors.PanelControl();
            this.tbnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.bbbb = new DevExpress.XtraEditors.LabelControl();
            this.lblTenKhoaLayBaoCao = new System.Windows.Forms.Label();
            this.lblThoiGianLayBaoCao = new DevExpress.XtraEditors.LabelControl();
            this.cboTuTruc = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControlData = new DevExpress.XtraEditors.PanelControl();
            this.lblThongBao = new DevExpress.XtraEditors.LabelControl();
            this.gridControlThuocTuTruc = new DevExpress.XtraGrid.GridControl();
            this.gridViewThuocTuTruc = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.timerThongBao = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMenu)).BeginInit();
            this.panelControlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTuTruc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlData)).BeginInit();
            this.panelControlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlThuocTuTruc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThuocTuTruc)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlMenu
            // 
            this.panelControlMenu.Controls.Add(this.tbnExport);
            this.panelControlMenu.Controls.Add(this.btnTimKiem);
            this.panelControlMenu.Controls.Add(this.bbbb);
            this.panelControlMenu.Controls.Add(this.lblTenKhoaLayBaoCao);
            this.panelControlMenu.Controls.Add(this.lblThoiGianLayBaoCao);
            this.panelControlMenu.Controls.Add(this.cboTuTruc);
            this.panelControlMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlMenu.Location = new System.Drawing.Point(0, 0);
            this.panelControlMenu.Name = "panelControlMenu";
            this.panelControlMenu.Size = new System.Drawing.Size(1000, 73);
            this.panelControlMenu.TabIndex = 0;
            // 
            // tbnExport
            // 
            this.tbnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbnExport.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tbnExport.Appearance.Options.UseFont = true;
            this.tbnExport.Appearance.Options.UseForeColor = true;
            this.tbnExport.Image = ((System.Drawing.Image)(resources.GetObject("tbnExport.Image")));
            this.tbnExport.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.tbnExport.Location = new System.Drawing.Point(871, 18);
            this.tbnExport.Name = "tbnExport";
            this.tbnExport.Size = new System.Drawing.Size(100, 40);
            this.tbnExport.TabIndex = 79;
            this.tbnExport.Text = "Export...";
            this.tbnExport.Click += new System.EventHandler(this.tbnExport_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnTimKiem.Appearance.Options.UseFont = true;
            this.btnTimKiem.Appearance.Options.UseForeColor = true;
            this.btnTimKiem.Image = global::MedicalLink.Properties.Resources.recurring_appointment_16;
            this.btnTimKiem.Location = new System.Drawing.Point(716, 18);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 40);
            this.btnTimKiem.TabIndex = 78;
            this.btnTimKiem.Text = "Refresh";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // bbbb
            // 
            this.bbbb.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bbbb.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bbbb.Location = new System.Drawing.Point(9, 15);
            this.bbbb.Name = "bbbb";
            this.bbbb.Size = new System.Drawing.Size(179, 16);
            this.bbbb.TabIndex = 75;
            this.bbbb.Text = "Thời gian lấy dữ liệu cuối cùng:";
            // 
            // lblTenKhoaLayBaoCao
            // 
            this.lblTenKhoaLayBaoCao.AutoSize = true;
            this.lblTenKhoaLayBaoCao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTenKhoaLayBaoCao.Location = new System.Drawing.Point(219, 12);
            this.lblTenKhoaLayBaoCao.Name = "lblTenKhoaLayBaoCao";
            this.lblTenKhoaLayBaoCao.Size = new System.Drawing.Size(61, 13);
            this.lblTenKhoaLayBaoCao.TabIndex = 76;
            this.lblTenKhoaLayBaoCao.Text = "Tên tủ trực";
            // 
            // lblThoiGianLayBaoCao
            // 
            this.lblThoiGianLayBaoCao.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThoiGianLayBaoCao.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblThoiGianLayBaoCao.Location = new System.Drawing.Point(34, 42);
            this.lblThoiGianLayBaoCao.Name = "lblThoiGianLayBaoCao";
            this.lblThoiGianLayBaoCao.Size = new System.Drawing.Size(122, 16);
            this.lblThoiGianLayBaoCao.TabIndex = 77;
            this.lblThoiGianLayBaoCao.Text = "00:00:00 01/01/0001";
            // 
            // cboTuTruc
            // 
            this.cboTuTruc.Location = new System.Drawing.Point(222, 28);
            this.cboTuTruc.Name = "cboTuTruc";
            this.cboTuTruc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTuTruc.Properties.Appearance.Options.UseFont = true;
            this.cboTuTruc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTuTruc.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("medicinestoreid", 10, "ID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("medicinestorename", 50, "Tên tủ trực")});
            this.cboTuTruc.Properties.DropDownRows = 10;
            this.cboTuTruc.Properties.NullText = "";
            this.cboTuTruc.Properties.PopupSizeable = false;
            this.cboTuTruc.Size = new System.Drawing.Size(465, 30);
            this.cboTuTruc.TabIndex = 74;
            // 
            // panelControlData
            // 
            this.panelControlData.Controls.Add(this.lblThongBao);
            this.panelControlData.Controls.Add(this.gridControlThuocTuTruc);
            this.panelControlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlData.Location = new System.Drawing.Point(0, 73);
            this.panelControlData.Name = "panelControlData";
            this.panelControlData.Size = new System.Drawing.Size(1000, 540);
            this.panelControlData.TabIndex = 1;
            // 
            // lblThongBao
            // 
            this.lblThongBao.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblThongBao.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblThongBao.Location = new System.Drawing.Point(418, 188);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(149, 23);
            this.lblThongBao.TabIndex = 30;
            this.lblThongBao.Text = "Sửa thành công";
            this.lblThongBao.Visible = false;
            // 
            // gridControlThuocTuTruc
            // 
            this.gridControlThuocTuTruc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlThuocTuTruc.Location = new System.Drawing.Point(2, 2);
            this.gridControlThuocTuTruc.MainView = this.gridViewThuocTuTruc;
            this.gridControlThuocTuTruc.Name = "gridControlThuocTuTruc";
            this.gridControlThuocTuTruc.Size = new System.Drawing.Size(996, 536);
            this.gridControlThuocTuTruc.TabIndex = 0;
            this.gridControlThuocTuTruc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewThuocTuTruc});
            // 
            // gridViewThuocTuTruc
            // 
            this.gridViewThuocTuTruc.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.gridViewThuocTuTruc.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridViewThuocTuTruc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gridViewThuocTuTruc.GridControl = this.gridControlThuocTuTruc;
            this.gridViewThuocTuTruc.GroupCount = 1;
            this.gridViewThuocTuTruc.Name = "gridViewThuocTuTruc";
            this.gridViewThuocTuTruc.OptionsBehavior.AutoExpandAllGroups = true;
            this.gridViewThuocTuTruc.OptionsFind.AlwaysVisible = true;
            this.gridViewThuocTuTruc.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.gridViewThuocTuTruc.OptionsFind.FindNullPrompt = "Từ khóa tìm kiếm...";
            this.gridViewThuocTuTruc.OptionsFind.ShowClearButton = false;
            this.gridViewThuocTuTruc.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewThuocTuTruc.OptionsView.ShowGroupPanel = false;
            this.gridViewThuocTuTruc.OptionsView.ShowIndicator = false;
            this.gridViewThuocTuTruc.RowHeight = 20;
            this.gridViewThuocTuTruc.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn8, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewThuocTuTruc.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewThuocTuTruc_RowCellStyle);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "STT";
            this.gridColumn1.FieldName = "stt";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 59;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Mã thuốc/vật tư";
            this.gridColumn2.FieldName = "medicinecode";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 140;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Tên thuốc/vật tư";
            this.gridColumn3.FieldName = "medicinename";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 338;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn4.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Đơn vị tính";
            this.gridColumn4.FieldName = "donvitinh";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 79;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn5.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn5.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "SL cơ sổ tủ trực";
            this.gridColumn5.DisplayFormat.FormatString = "#,##0.00";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn5.FieldName = "soluongtutruc";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 113;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn6.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn6.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "SL tồn kho";
            this.gridColumn6.DisplayFormat.FormatString = "#,##0.00";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn6.FieldName = "soluongtonkho";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 123;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn7.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn7.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "SL khả dụng";
            this.gridColumn7.DisplayFormat.FormatString = "#,##0.00";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn7.FieldName = "soluongkhadung";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 126;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Nhóm";
            this.gridColumn8.FieldName = "medicinegroupcode";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // timerThongBao
            // 
            this.timerThongBao.Interval = 2000;
            this.timerThongBao.Tick += new System.EventHandler(this.timerThongBao_Tick);
            // 
            // ucBaoCaoXNTTuTruc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlData);
            this.Controls.Add(this.panelControlMenu);
            this.Name = "ucBaoCaoXNTTuTruc";
            this.Size = new System.Drawing.Size(1000, 613);
            this.Load += new System.EventHandler(this.ucBaoCaoXNTTuTruc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlMenu)).EndInit();
            this.panelControlMenu.ResumeLayout(false);
            this.panelControlMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTuTruc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlData)).EndInit();
            this.panelControlData.ResumeLayout(false);
            this.panelControlData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlThuocTuTruc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThuocTuTruc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlMenu;
        private DevExpress.XtraEditors.PanelControl panelControlData;
        private DevExpress.XtraEditors.LabelControl bbbb;
        private System.Windows.Forms.Label lblTenKhoaLayBaoCao;
        private DevExpress.XtraEditors.LabelControl lblThoiGianLayBaoCao;
        private DevExpress.XtraEditors.LookUpEdit cboTuTruc;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraGrid.GridControl gridControlThuocTuTruc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewThuocTuTruc;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.SimpleButton tbnExport;
        private DevExpress.XtraEditors.LabelControl lblThongBao;
        private System.Windows.Forms.Timer timerThongBao;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    }
}
