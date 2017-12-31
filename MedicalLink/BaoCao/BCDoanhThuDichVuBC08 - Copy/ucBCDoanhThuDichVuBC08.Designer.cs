namespace MedicalLink.BaoCao
{
    partial class ucBCDoanhThuDichVuBC08
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBCDoanhThuDichVuBC08));
            this.panelControlThongTinDV = new DevExpress.XtraEditors.PanelControl();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cboTieuChi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkcomboNhomDichVu = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.radioXemChiTiet = new System.Windows.Forms.RadioButton();
            this.radioXemTongHop = new System.Windows.Forms.RadioButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboTrangThaiVienPhi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dateDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dateTuNgay = new System.Windows.Forms.DateTimePicker();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.tbnExport = new DevExpress.XtraEditors.SimpleButton();
            this.treeListDSDichVu = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.servicepricecode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.servicepricename = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.servicepriceunit = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.servicepricefeebhyt = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.servicepricefeenhandan = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.mrd_templatename = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinDV)).BeginInit();
            this.panelControlThongTinDV.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTieuChi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcomboNhomDichVu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrangThaiVienPhi.Properties)).BeginInit();
            this.groupBoxAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListDSDichVu)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlThongTinDV
            // 
            this.panelControlThongTinDV.Controls.Add(this.groupBoxFile);
            this.panelControlThongTinDV.Controls.Add(this.groupBoxAction);
            this.panelControlThongTinDV.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlThongTinDV.Location = new System.Drawing.Point(0, 0);
            this.panelControlThongTinDV.Name = "panelControlThongTinDV";
            this.panelControlThongTinDV.Size = new System.Drawing.Size(1200, 94);
            this.panelControlThongTinDV.TabIndex = 3;
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Controls.Add(this.labelControl5);
            this.groupBoxFile.Controls.Add(this.cboTieuChi);
            this.groupBoxFile.Controls.Add(this.chkcomboNhomDichVu);
            this.groupBoxFile.Controls.Add(this.radioXemChiTiet);
            this.groupBoxFile.Controls.Add(this.radioXemTongHop);
            this.groupBoxFile.Controls.Add(this.labelControl4);
            this.groupBoxFile.Controls.Add(this.labelControl2);
            this.groupBoxFile.Controls.Add(this.cboTrangThaiVienPhi);
            this.groupBoxFile.Controls.Add(this.labelControl1);
            this.groupBoxFile.Controls.Add(this.labelControl3);
            this.groupBoxFile.Controls.Add(this.dateDenNgay);
            this.groupBoxFile.Controls.Add(this.dateTuNgay);
            this.groupBoxFile.Controls.Add(this.btnTimKiem);
            this.groupBoxFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxFile.Location = new System.Drawing.Point(2, 2);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(1071, 90);
            this.groupBoxFile.TabIndex = 10;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "Tìm kiếm";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(471, 25);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(46, 16);
            this.labelControl5.TabIndex = 89;
            this.labelControl5.Text = "Tiêu chí";
            // 
            // cboTieuChi
            // 
            this.cboTieuChi.EditValue = "Theo ngày thanh toán";
            this.cboTieuChi.Location = new System.Drawing.Point(523, 22);
            this.cboTieuChi.Name = "cboTieuChi";
            this.cboTieuChi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTieuChi.Properties.Appearance.Options.UseFont = true;
            this.cboTieuChi.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTieuChi.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboTieuChi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTieuChi.Properties.Items.AddRange(new object[] {
            "Theo ngày chỉ định",
            "Theo ngày vào viện",
            "Theo ngày thanh toán"});
            this.cboTieuChi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboTieuChi.Size = new System.Drawing.Size(160, 22);
            this.cboTieuChi.TabIndex = 88;
            // 
            // chkcomboNhomDichVu
            // 
            this.chkcomboNhomDichVu.EditValue = "";
            this.chkcomboNhomDichVu.Location = new System.Drawing.Point(288, 58);
            this.chkcomboNhomDichVu.Name = "chkcomboNhomDichVu";
            this.chkcomboNhomDichVu.Properties.AllowMultiSelect = true;
            this.chkcomboNhomDichVu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcomboNhomDichVu.Properties.Appearance.Options.UseFont = true;
            this.chkcomboNhomDichVu.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcomboNhomDichVu.Properties.AppearanceDropDown.Options.UseFont = true;
            this.chkcomboNhomDichVu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkcomboNhomDichVu.Properties.DropDownRows = 15;
            this.chkcomboNhomDichVu.Size = new System.Drawing.Size(395, 22);
            this.chkcomboNhomDichVu.TabIndex = 87;
            // 
            // radioXemChiTiet
            // 
            this.radioXemChiTiet.AutoSize = true;
            this.radioXemChiTiet.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioXemChiTiet.Location = new System.Drawing.Point(779, 60);
            this.radioXemChiTiet.Name = "radioXemChiTiet";
            this.radioXemChiTiet.Size = new System.Drawing.Size(113, 20);
            this.radioXemChiTiet.TabIndex = 85;
            this.radioXemChiTiet.Text = "Xem chi tiết BN";
            this.radioXemChiTiet.UseVisualStyleBackColor = true;
            this.radioXemChiTiet.CheckedChanged += new System.EventHandler(this.radioXemChiTiet_CheckedChanged);
            // 
            // radioXemTongHop
            // 
            this.radioXemTongHop.AutoSize = true;
            this.radioXemTongHop.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioXemTongHop.Location = new System.Drawing.Point(779, 23);
            this.radioXemTongHop.Name = "radioXemTongHop";
            this.radioXemTongHop.Size = new System.Drawing.Size(106, 20);
            this.radioXemTongHop.TabIndex = 84;
            this.radioXemTongHop.Text = "Xem tổng hợp";
            this.radioXemTongHop.UseVisualStyleBackColor = true;
            this.radioXemTongHop.CheckedChanged += new System.EventHandler(this.radioXemTongHop_CheckedChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(220, 25);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(59, 16);
            this.labelControl4.TabIndex = 24;
            this.labelControl4.Text = "Trạng thái";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(226, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 16);
            this.labelControl2.TabIndex = 23;
            this.labelControl2.Text = "Nhóm DV";
            // 
            // cboTrangThaiVienPhi
            // 
            this.cboTrangThaiVienPhi.EditValue = "Đã thanh toán";
            this.cboTrangThaiVienPhi.Location = new System.Drawing.Point(288, 22);
            this.cboTrangThaiVienPhi.Name = "cboTrangThaiVienPhi";
            this.cboTrangThaiVienPhi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTrangThaiVienPhi.Properties.Appearance.Options.UseFont = true;
            this.cboTrangThaiVienPhi.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTrangThaiVienPhi.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboTrangThaiVienPhi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTrangThaiVienPhi.Properties.Items.AddRange(new object[] {
            "Đang điều trị",
            "Ra viện chưa thanh toán",
            "Đã thanh toán"});
            this.cboTrangThaiVienPhi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboTrangThaiVienPhi.Size = new System.Drawing.Size(160, 22);
            this.cboTrangThaiVienPhi.TabIndex = 22;
            this.cboTrangThaiVienPhi.EditValueChanged += new System.EventHandler(this.cboTrangThaiVienPhi_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(6, 62);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 16);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "Đến";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(13, 25);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(16, 16);
            this.labelControl3.TabIndex = 20;
            this.labelControl3.Text = "Từ";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateDenNgay.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateDenNgay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateDenNgay.Location = new System.Drawing.Point(44, 57);
            this.dateDenNgay.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Size = new System.Drawing.Size(159, 23);
            this.dateDenNgay.TabIndex = 19;
            this.dateDenNgay.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTuNgay.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateTuNgay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTuNgay.Location = new System.Drawing.Point(44, 20);
            this.dateTuNgay.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Size = new System.Drawing.Size(159, 23);
            this.dateTuNgay.TabIndex = 18;
            this.dateTuNgay.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnTimKiem.Appearance.Options.UseFont = true;
            this.btnTimKiem.Appearance.Options.UseForeColor = true;
            this.btnTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Image")));
            this.btnTimKiem.Location = new System.Drawing.Point(923, 27);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 40);
            this.btnTimKiem.TabIndex = 5;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.btnPrint);
            this.groupBoxAction.Controls.Add(this.tbnExport);
            this.groupBoxAction.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBoxAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxAction.Location = new System.Drawing.Point(1073, 2);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(125, 90);
            this.groupBoxAction.TabIndex = 9;
            this.groupBoxAction.TabStop = false;
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
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "In...";
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
            this.tbnExport.TabIndex = 8;
            this.tbnExport.Text = "Xuất file";
            this.tbnExport.Click += new System.EventHandler(this.tbnExport_Click);
            // 
            // treeListDSDichVu
            // 
            this.treeListDSDichVu.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListDSDichVu.Appearance.Row.Options.UseFont = true;
            this.treeListDSDichVu.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.treeListDSDichVu.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListDSDichVu.Appearance.SelectedRow.Options.UseBackColor = true;
            this.treeListDSDichVu.Appearance.SelectedRow.Options.UseFont = true;
            this.treeListDSDichVu.CaptionHeight = 25;
            this.treeListDSDichVu.ColumnPanelRowHeight = 25;
            this.treeListDSDichVu.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn3,
            this.treeListColumn2,
            this.servicepricecode,
            this.servicepricename,
            this.servicepriceunit,
            this.servicepricefeebhyt,
            this.servicepricefeenhandan,
            this.mrd_templatename});
            this.treeListDSDichVu.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListDSDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListDSDichVu.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListDSDichVu.Location = new System.Drawing.Point(0, 94);
            this.treeListDSDichVu.Name = "treeListDSDichVu";
            this.treeListDSDichVu.OptionsBehavior.PopulateServiceColumns = true;
            this.treeListDSDichVu.OptionsFind.AllowFindPanel = true;
            this.treeListDSDichVu.OptionsFind.FindNullPrompt = "Từ khóa tìm kiếm...";
            this.treeListDSDichVu.OptionsFind.ShowClearButton = false;
            this.treeListDSDichVu.OptionsSelection.InvertSelection = true;
            this.treeListDSDichVu.OptionsSelection.MultiSelect = true;
            this.treeListDSDichVu.OptionsSelection.SelectNodesOnRightClick = true;
            this.treeListDSDichVu.OptionsView.AutoWidth = false;
            this.treeListDSDichVu.OptionsView.ShowIndicator = false;
            this.treeListDSDichVu.RowHeight = 25;
            this.treeListDSDichVu.Size = new System.Drawing.Size(1200, 500);
            this.treeListDSDichVu.TabIndex = 4;
            this.treeListDSDichVu.TreeLevelWidth = 25;
            this.treeListDSDichVu.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeListDSDichVu_NodeCellStyle);
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn3.AppearanceCell.Options.UseFont = true;
            this.treeListColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn3.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.treeListColumn3.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.treeListColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn3.Caption = "Mã nhóm";
            this.treeListColumn3.FieldName = "servicepricegroupcode";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.OptionsColumn.AllowEdit = false;
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 0;
            this.treeListColumn3.Width = 179;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn2.AppearanceCell.Options.UseFont = true;
            this.treeListColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListColumn2.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.treeListColumn2.AppearanceHeader.Options.UseFont = true;
            this.treeListColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.treeListColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn2.Caption = "Loại dịch vụ";
            this.treeListColumn2.FieldName = "servicegrouptype_name";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.AllowEdit = false;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            // 
            // servicepricecode
            // 
            this.servicepricecode.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicepricecode.AppearanceCell.Options.UseFont = true;
            this.servicepricecode.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicepricecode.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.servicepricecode.AppearanceHeader.Options.UseFont = true;
            this.servicepricecode.AppearanceHeader.Options.UseForeColor = true;
            this.servicepricecode.AppearanceHeader.Options.UseTextOptions = true;
            this.servicepricecode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.servicepricecode.Caption = "Mã dịch vụ";
            this.servicepricecode.FieldName = "servicepricecode";
            this.servicepricecode.Name = "servicepricecode";
            this.servicepricecode.OptionsColumn.ReadOnly = true;
            this.servicepricecode.Visible = true;
            this.servicepricecode.VisibleIndex = 2;
            this.servicepricecode.Width = 158;
            // 
            // servicepricename
            // 
            this.servicepricename.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicepricename.AppearanceCell.Options.UseFont = true;
            this.servicepricename.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicepricename.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.servicepricename.AppearanceHeader.Options.UseFont = true;
            this.servicepricename.AppearanceHeader.Options.UseForeColor = true;
            this.servicepricename.AppearanceHeader.Options.UseTextOptions = true;
            this.servicepricename.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.servicepricename.Caption = "Tên dịch vụ";
            this.servicepricename.FieldName = "servicepricename";
            this.servicepricename.Name = "servicepricename";
            this.servicepricename.OptionsColumn.AllowEdit = false;
            this.servicepricename.Visible = true;
            this.servicepricename.VisibleIndex = 3;
            this.servicepricename.Width = 303;
            // 
            // servicepriceunit
            // 
            this.servicepriceunit.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicepriceunit.AppearanceCell.Options.UseFont = true;
            this.servicepriceunit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicepriceunit.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.servicepriceunit.AppearanceHeader.Options.UseFont = true;
            this.servicepriceunit.AppearanceHeader.Options.UseForeColor = true;
            this.servicepriceunit.AppearanceHeader.Options.UseTextOptions = true;
            this.servicepriceunit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.servicepriceunit.Caption = "Loại hình TT";
            this.servicepriceunit.FieldName = "loaidoituong_name";
            this.servicepriceunit.Name = "servicepriceunit";
            this.servicepriceunit.OptionsColumn.AllowEdit = false;
            this.servicepriceunit.Visible = true;
            this.servicepriceunit.VisibleIndex = 4;
            this.servicepriceunit.Width = 97;
            // 
            // servicepricefeebhyt
            // 
            this.servicepricefeebhyt.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicepricefeebhyt.AppearanceCell.Options.UseFont = true;
            this.servicepricefeebhyt.AppearanceCell.Options.UseTextOptions = true;
            this.servicepricefeebhyt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.servicepricefeebhyt.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicepricefeebhyt.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.servicepricefeebhyt.AppearanceHeader.Options.UseFont = true;
            this.servicepricefeebhyt.AppearanceHeader.Options.UseForeColor = true;
            this.servicepricefeebhyt.AppearanceHeader.Options.UseTextOptions = true;
            this.servicepricefeebhyt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.servicepricefeebhyt.Caption = "Số lượng";
            this.servicepricefeebhyt.FieldName = "soluong";
            this.servicepricefeebhyt.Format.FormatString = "#,##0";
            this.servicepricefeebhyt.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            this.servicepricefeebhyt.Name = "servicepricefeebhyt";
            this.servicepricefeebhyt.OptionsColumn.AllowEdit = false;
            this.servicepricefeebhyt.Visible = true;
            this.servicepricefeebhyt.VisibleIndex = 5;
            this.servicepricefeebhyt.Width = 120;
            // 
            // servicepricefeenhandan
            // 
            this.servicepricefeenhandan.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicepricefeenhandan.AppearanceCell.Options.UseFont = true;
            this.servicepricefeenhandan.AppearanceCell.Options.UseTextOptions = true;
            this.servicepricefeenhandan.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.servicepricefeenhandan.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicepricefeenhandan.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.servicepricefeenhandan.AppearanceHeader.Options.UseFont = true;
            this.servicepricefeenhandan.AppearanceHeader.Options.UseForeColor = true;
            this.servicepricefeenhandan.AppearanceHeader.Options.UseTextOptions = true;
            this.servicepricefeenhandan.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.servicepricefeenhandan.Caption = "Đơn giá";
            this.servicepricefeenhandan.FieldName = "servicepricemoney";
            this.servicepricefeenhandan.Format.FormatString = "#,##0";
            this.servicepricefeenhandan.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            this.servicepricefeenhandan.Name = "servicepricefeenhandan";
            this.servicepricefeenhandan.OptionsColumn.AllowEdit = false;
            this.servicepricefeenhandan.Visible = true;
            this.servicepricefeenhandan.VisibleIndex = 6;
            this.servicepricefeenhandan.Width = 120;
            // 
            // mrd_templatename
            // 
            this.mrd_templatename.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrd_templatename.AppearanceCell.Options.UseFont = true;
            this.mrd_templatename.AppearanceCell.Options.UseTextOptions = true;
            this.mrd_templatename.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.mrd_templatename.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrd_templatename.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.mrd_templatename.AppearanceHeader.Options.UseFont = true;
            this.mrd_templatename.AppearanceHeader.Options.UseForeColor = true;
            this.mrd_templatename.AppearanceHeader.Options.UseTextOptions = true;
            this.mrd_templatename.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.mrd_templatename.Caption = "Thành tiền";
            this.mrd_templatename.FieldName = "thanhtien";
            this.mrd_templatename.Format.FormatString = "#,##0";
            this.mrd_templatename.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            this.mrd_templatename.Name = "mrd_templatename";
            this.mrd_templatename.OptionsColumn.AllowEdit = false;
            this.mrd_templatename.Visible = true;
            this.mrd_templatename.VisibleIndex = 7;
            this.mrd_templatename.Width = 236;
            // 
            // ucBCDoanhThuDichVuBC08
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListDSDichVu);
            this.Controls.Add(this.panelControlThongTinDV);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucBCDoanhThuDichVuBC08";
            this.Size = new System.Drawing.Size(1200, 594);
            this.Load += new System.EventHandler(this.ucBCDoanhThuDichVuBC08_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinDV)).EndInit();
            this.panelControlThongTinDV.ResumeLayout(false);
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTieuChi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkcomboNhomDichVu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrangThaiVienPhi.Properties)).EndInit();
            this.groupBoxAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListDSDichVu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlThongTinDV;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.DateTimePicker dateDenNgay;
        private System.Windows.Forms.DateTimePicker dateTuNgay;
        private DevExpress.XtraEditors.SimpleButton tbnExport;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cboTrangThaiVienPhi;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private System.Windows.Forms.RadioButton radioXemChiTiet;
        private System.Windows.Forms.RadioButton radioXemTongHop;
        private DevExpress.XtraTreeList.TreeList treeListDSDichVu;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn servicepricecode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn servicepricename;
        private DevExpress.XtraTreeList.Columns.TreeListColumn servicepriceunit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn servicepricefeebhyt;
        private DevExpress.XtraTreeList.Columns.TreeListColumn servicepricefeenhandan;
        private DevExpress.XtraTreeList.Columns.TreeListColumn mrd_templatename;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkcomboNhomDichVu;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit cboTieuChi;
    }
}
