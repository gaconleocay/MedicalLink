namespace MedicalLink.BaoCao
{
    partial class ucBangKeTongHopHoaDon
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
            this.panelControlThongTinDV = new DevExpress.XtraEditors.PanelControl();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cboLoaiSo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dateDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dateTuNgay = new System.Windows.Forms.DateTimePicker();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.tbnExport = new DevExpress.XtraEditors.SimpleButton();
            this.panelControlData = new DevExpress.XtraEditors.PanelControl();
            this.gridControlDSHoaDon = new DevExpress.XtraGrid.GridControl();
            this.gridViewDSHoaDon = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinDV)).BeginInit();
            this.panelControlThongTinDV.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboLoaiSo.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlData)).BeginInit();
            this.panelControlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDSHoaDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlThongTinDV
            // 
            this.panelControlThongTinDV.Controls.Add(this.groupBoxFile);
            this.panelControlThongTinDV.Controls.Add(this.groupBox1);
            this.panelControlThongTinDV.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlThongTinDV.Location = new System.Drawing.Point(0, 0);
            this.panelControlThongTinDV.Name = "panelControlThongTinDV";
            this.panelControlThongTinDV.Size = new System.Drawing.Size(1100, 94);
            this.panelControlThongTinDV.TabIndex = 3;
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Controls.Add(this.labelControl4);
            this.groupBoxFile.Controls.Add(this.cboLoaiSo);
            this.groupBoxFile.Controls.Add(this.labelControl1);
            this.groupBoxFile.Controls.Add(this.labelControl3);
            this.groupBoxFile.Controls.Add(this.dateDenNgay);
            this.groupBoxFile.Controls.Add(this.dateTuNgay);
            this.groupBoxFile.Controls.Add(this.btnTimKiem);
            this.groupBoxFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxFile.Location = new System.Drawing.Point(2, 2);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(971, 90);
            this.groupBoxFile.TabIndex = 27;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "Tìm kiếm";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(276, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(40, 16);
            this.labelControl4.TabIndex = 74;
            this.labelControl4.Text = "Loại sổ";
            // 
            // cboLoaiSo
            // 
            this.cboLoaiSo.EditValue = "Thu tiền";
            this.cboLoaiSo.Location = new System.Drawing.Point(334, 20);
            this.cboLoaiSo.Name = "cboLoaiSo";
            this.cboLoaiSo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiSo.Properties.Appearance.Options.UseFont = true;
            this.cboLoaiSo.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiSo.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboLoaiSo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLoaiSo.Properties.Items.AddRange(new object[] {
            "Tổng hợp",
            "Tạm ứng",
            "Thu tiền"});
            this.cboLoaiSo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboLoaiSo.Size = new System.Drawing.Size(222, 22);
            this.cboLoaiSo.TabIndex = 72;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(37, 56);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 16);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "Đến";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(43, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(16, 16);
            this.labelControl3.TabIndex = 20;
            this.labelControl3.Text = "Từ";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateDenNgay.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateDenNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateDenNgay.Location = new System.Drawing.Point(70, 52);
            this.dateDenNgay.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Size = new System.Drawing.Size(159, 22);
            this.dateDenNgay.TabIndex = 19;
            this.dateDenNgay.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTuNgay.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateTuNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTuNgay.Location = new System.Drawing.Point(70, 19);
            this.dateTuNgay.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Size = new System.Drawing.Size(159, 22);
            this.dateTuNgay.TabIndex = 18;
            this.dateTuNgay.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnTimKiem.Appearance.Options.UseFont = true;
            this.btnTimKiem.Appearance.Options.UseForeColor = true;
            this.btnTimKiem.Image = global::MedicalLink.Properties.Resources.search_3_24;
            this.btnTimKiem.Location = new System.Drawing.Point(692, 34);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 40);
            this.btnTimKiem.TabIndex = 5;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.tbnExport);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Location = new System.Drawing.Point(973, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(125, 90);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Appearance.Options.UseForeColor = true;
            this.btnPrint.Image = global::MedicalLink.Properties.Resources.printer_16;
            this.btnPrint.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(12, 15);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 11;
            this.btnPrint.Text = "In ấn...";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tbnExport
            // 
            this.tbnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbnExport.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tbnExport.Appearance.Options.UseFont = true;
            this.tbnExport.Appearance.Options.UseForeColor = true;
            this.tbnExport.Image = global::MedicalLink.Properties.Resources.excel_3_16;
            this.tbnExport.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.tbnExport.Location = new System.Drawing.Point(12, 53);
            this.tbnExport.Name = "tbnExport";
            this.tbnExport.Size = new System.Drawing.Size(100, 30);
            this.tbnExport.TabIndex = 10;
            this.tbnExport.Text = "Xuất file";
            this.tbnExport.Click += new System.EventHandler(this.tbnExport_Click);
            // 
            // panelControlData
            // 
            this.panelControlData.Controls.Add(this.gridControlDSHoaDon);
            this.panelControlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlData.Location = new System.Drawing.Point(0, 94);
            this.panelControlData.Name = "panelControlData";
            this.panelControlData.Size = new System.Drawing.Size(1100, 519);
            this.panelControlData.TabIndex = 4;
            // 
            // gridControlDSHoaDon
            // 
            this.gridControlDSHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDSHoaDon.Location = new System.Drawing.Point(2, 2);
            this.gridControlDSHoaDon.MainView = this.gridViewDSHoaDon;
            this.gridControlDSHoaDon.Name = "gridControlDSHoaDon";
            this.gridControlDSHoaDon.Size = new System.Drawing.Size(1096, 515);
            this.gridControlDSHoaDon.TabIndex = 0;
            this.gridControlDSHoaDon.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDSHoaDon});
            // 
            // gridViewDSHoaDon
            // 
            this.gridViewDSHoaDon.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewDSHoaDon.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridViewDSHoaDon.Appearance.FooterPanel.Options.UseFont = true;
            this.gridViewDSHoaDon.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridViewDSHoaDon.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.gridViewDSHoaDon.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridViewDSHoaDon.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewDSHoaDon.Appearance.GroupFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridViewDSHoaDon.Appearance.GroupFooter.Options.UseFont = true;
            this.gridViewDSHoaDon.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gridViewDSHoaDon.Appearance.GroupFooter.Options.UseTextOptions = true;
            this.gridViewDSHoaDon.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridViewDSHoaDon.ColumnPanelRowHeight = 25;
            this.gridViewDSHoaDon.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gridViewDSHoaDon.GridControl = this.gridControlDSHoaDon;
            this.gridViewDSHoaDon.Name = "gridViewDSHoaDon";
            this.gridViewDSHoaDon.OptionsView.ColumnAutoWidth = false;
            this.gridViewDSHoaDon.OptionsView.RowAutoHeight = true;
            this.gridViewDSHoaDon.OptionsView.ShowFooter = true;
            this.gridViewDSHoaDon.OptionsView.ShowGroupPanel = false;
            this.gridViewDSHoaDon.OptionsView.ShowIndicator = false;
            this.gridViewDSHoaDon.RowHeight = 25;
            this.gridViewDSHoaDon.ViewCaptionHeight = 25;
            this.gridViewDSHoaDon.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewDSHoaDon_RowCellStyle);
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
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn2.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Sổ quyển";
            this.gridColumn2.FieldName = "billgroupcode";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 125;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn3.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Thời gian tạo sổ";
            this.gridColumn3.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn3.FieldName = "billgroupdate";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 125;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn4.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Từ số - đến số";
            this.gridColumn4.FieldName = "sophieutu_den";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 122;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn5.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.Caption = "Số phiếu sử dụng đến";
            this.gridColumn5.FieldName = "sophieusudung";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 151;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn6.AppearanceCell.Options.UseFont = true;
            this.gridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn6.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn6.AppearanceHeader.Options.UseFont = true;
            this.gridColumn6.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn6.Caption = "Danh sách phiếu hủy";
            this.gridColumn6.FieldName = "billcode_huy";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 550;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn7.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Tổng tiền";
            this.gridColumn7.DisplayFormat.FormatString = "#,##0";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn7.FieldName = "tongtien_thu";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "tongtien_thu", "{0:#,##0}")});
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 161;
            // 
            // ucBangKeTongHopHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlData);
            this.Controls.Add(this.panelControlThongTinDV);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucBangKeTongHopHoaDon";
            this.Size = new System.Drawing.Size(1100, 613);
            this.Load += new System.EventHandler(this.ucBangKeTongHopHoaDon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinDV)).EndInit();
            this.panelControlThongTinDV.ResumeLayout(false);
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboLoaiSo.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlData)).EndInit();
            this.panelControlData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDSHoaDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSHoaDon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlThongTinDV;
        private DevExpress.XtraEditors.PanelControl panelControlData;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit cboLoaiSo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.DateTimePicker dateDenNgay;
        private System.Windows.Forms.DateTimePicker dateTuNgay;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraGrid.GridControl gridControlDSHoaDon;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDSHoaDon;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton tbnExport;
    }
}
