﻿namespace MedicalLink.ChucNang
{
    partial class ucKTHSBALoiTrangThai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucKTHSBALoiTrangThai));
            this.panelControlThongTinDV = new DevExpress.XtraEditors.PanelControl();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTuNgay = new System.Windows.Forms.DateTimePicker();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.btnSuaTTHSBA = new DevExpress.XtraEditors.SimpleButton();
            this.tbnExport = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlHSBA = new DevExpress.XtraGrid.GridControl();
            this.gridViewHSBA = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.stt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.openFileDialogSelect = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinDV)).BeginInit();
            this.panelControlThongTinDV.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            this.groupBoxAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHSBA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHSBA)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlThongTinDV
            // 
            this.panelControlThongTinDV.Controls.Add(this.groupBoxFile);
            this.panelControlThongTinDV.Controls.Add(this.groupBoxAction);
            this.panelControlThongTinDV.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlThongTinDV.Location = new System.Drawing.Point(0, 0);
            this.panelControlThongTinDV.Name = "panelControlThongTinDV";
            this.panelControlThongTinDV.Size = new System.Drawing.Size(1000, 102);
            this.panelControlThongTinDV.TabIndex = 3;
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Controls.Add(this.labelControl1);
            this.groupBoxFile.Controls.Add(this.labelControl3);
            this.groupBoxFile.Controls.Add(this.dateTimePicker2);
            this.groupBoxFile.Controls.Add(this.dateDenNgay);
            this.groupBoxFile.Controls.Add(this.dateTimePicker1);
            this.groupBoxFile.Controls.Add(this.dateTuNgay);
            this.groupBoxFile.Controls.Add(this.btnTimKiem);
            this.groupBoxFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxFile.Location = new System.Drawing.Point(2, 2);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(641, 98);
            this.groupBoxFile.TabIndex = 10;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "Tìm kiếm theo thời gian duyệt VP";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(19, 67);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 16);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "Đến ngày";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(26, 29);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(47, 16);
            this.labelControl3.TabIndex = 20;
            this.labelControl3.Text = "Từ ngày";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateTimePicker2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(312, 144);
            this.dateTimePicker2.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(190, 27);
            this.dateTimePicker2.TabIndex = 19;
            this.dateTimePicker2.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateDenNgay.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateDenNgay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateDenNgay.Location = new System.Drawing.Point(90, 62);
            this.dateDenNgay.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateDenNgay.Name = "dateDenNgay";
            this.dateDenNgay.Size = new System.Drawing.Size(190, 23);
            this.dateDenNgay.TabIndex = 19;
            this.dateDenNgay.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(312, 106);
            this.dateTimePicker1.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(190, 27);
            this.dateTimePicker1.TabIndex = 18;
            this.dateTimePicker1.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // dateTuNgay
            // 
            this.dateTuNgay.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTuNgay.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateTuNgay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTuNgay.Location = new System.Drawing.Point(90, 24);
            this.dateTuNgay.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Size = new System.Drawing.Size(190, 23);
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
            this.btnTimKiem.Location = new System.Drawing.Point(446, 33);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 40);
            this.btnTimKiem.TabIndex = 5;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.btnSuaTTHSBA);
            this.groupBoxAction.Controls.Add(this.tbnExport);
            this.groupBoxAction.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBoxAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxAction.Location = new System.Drawing.Point(643, 2);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(355, 98);
            this.groupBoxAction.TabIndex = 9;
            this.groupBoxAction.TabStop = false;
            this.groupBoxAction.Text = "Action";
            // 
            // btnSuaTTHSBA
            // 
            this.btnSuaTTHSBA.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaTTHSBA.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSuaTTHSBA.Appearance.Options.UseFont = true;
            this.btnSuaTTHSBA.Appearance.Options.UseForeColor = true;
            this.btnSuaTTHSBA.Image = ((System.Drawing.Image)(resources.GetObject("btnSuaTTHSBA.Image")));
            this.btnSuaTTHSBA.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSuaTTHSBA.Location = new System.Drawing.Point(25, 33);
            this.btnSuaTTHSBA.Name = "btnSuaTTHSBA";
            this.btnSuaTTHSBA.Size = new System.Drawing.Size(100, 40);
            this.btnSuaTTHSBA.TabIndex = 9;
            this.btnSuaTTHSBA.Text = "Sửa HSBA";
            this.btnSuaTTHSBA.Click += new System.EventHandler(this.btnSuaTTHSBA_Click);
            // 
            // tbnExport
            // 
            this.tbnExport.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbnExport.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tbnExport.Appearance.Options.UseFont = true;
            this.tbnExport.Appearance.Options.UseForeColor = true;
            this.tbnExport.Image = ((System.Drawing.Image)(resources.GetObject("tbnExport.Image")));
            this.tbnExport.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.tbnExport.Location = new System.Drawing.Point(199, 33);
            this.tbnExport.Name = "tbnExport";
            this.tbnExport.Size = new System.Drawing.Size(100, 40);
            this.tbnExport.TabIndex = 8;
            this.tbnExport.Text = "Export...";
            this.tbnExport.Click += new System.EventHandler(this.tbnExport_Click);
            // 
            // gridControlHSBA
            // 
            this.gridControlHSBA.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.gridControlHSBA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlHSBA.EmbeddedNavigator.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.gridControlHSBA.Location = new System.Drawing.Point(0, 102);
            this.gridControlHSBA.MainView = this.gridViewHSBA;
            this.gridControlHSBA.Name = "gridControlHSBA";
            this.gridControlHSBA.Size = new System.Drawing.Size(1000, 492);
            this.gridControlHSBA.TabIndex = 4;
            this.gridControlHSBA.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewHSBA});
            // 
            // gridViewHSBA
            // 
            this.gridViewHSBA.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gridViewHSBA.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridViewHSBA.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewHSBA.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridViewHSBA.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridViewHSBA.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridViewHSBA.ColumnPanelRowHeight = 25;
            this.gridViewHSBA.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.stt,
            this.gridColumn2,
            this.gridColumn16,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn14,
            this.gridColumn15});
            this.gridViewHSBA.GridControl = this.gridControlHSBA;
            this.gridViewHSBA.IndicatorWidth = 50;
            this.gridViewHSBA.Name = "gridViewHSBA";
            this.gridViewHSBA.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewHSBA.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewHSBA.OptionsBehavior.Editable = false;
            this.gridViewHSBA.OptionsView.ColumnAutoWidth = false;
            this.gridViewHSBA.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewHSBA.OptionsView.ShowGroupPanel = false;
            this.gridViewHSBA.OptionsView.ShowIndicator = false;
            this.gridViewHSBA.RowHeight = 25;
            this.gridViewHSBA.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewDichVu_CustomDrawCell);
            // 
            // stt
            // 
            this.stt.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stt.AppearanceCell.Options.UseFont = true;
            this.stt.AppearanceCell.Options.UseTextOptions = true;
            this.stt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.stt.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.stt.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.stt.AppearanceHeader.Options.UseFont = true;
            this.stt.AppearanceHeader.Options.UseForeColor = true;
            this.stt.AppearanceHeader.Options.UseTextOptions = true;
            this.stt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.stt.Caption = "STT";
            this.stt.FieldName = "STT";
            this.stt.Name = "stt";
            this.stt.OptionsColumn.AllowEdit = false;
            this.stt.Visible = true;
            this.stt.VisibleIndex = 0;
            this.stt.Width = 57;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn2.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn2.Caption = "Mã điều trị";
            this.gridColumn2.FieldName = "medicalrecordid";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 86;
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn16.AppearanceCell.Options.UseFont = true;
            this.gridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn16.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn16.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn16.AppearanceHeader.Options.UseFont = true;
            this.gridColumn16.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn16.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn16.Caption = "Mã BN";
            this.gridColumn16.FieldName = "patientid";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 2;
            this.gridColumn16.Width = 83;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn3.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn3.Caption = "Mã VP";
            this.gridColumn3.FieldName = "vienphiid";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 69;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn4.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn4.Caption = "Mã HSBA";
            this.gridColumn4.FieldName = "hosobenhanid";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 99;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn7.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn7.Caption = "Tên bệnh nhân";
            this.gridColumn7.FieldName = "patientname";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 200;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn1.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Số thẻ BHYT";
            this.gridColumn1.FieldName = "bhytcode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            this.gridColumn1.Width = 130;
            // 
            // gridColumn17
            // 
            this.gridColumn17.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn17.AppearanceCell.Options.UseFont = true;
            this.gridColumn17.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn17.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn17.AppearanceHeader.Options.UseFont = true;
            this.gridColumn17.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn17.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn17.Caption = "Khoa";
            this.gridColumn17.FieldName = "departmentgroupname";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 7;
            this.gridColumn17.Width = 186;
            // 
            // gridColumn18
            // 
            this.gridColumn18.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn18.AppearanceCell.Options.UseFont = true;
            this.gridColumn18.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn18.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn18.AppearanceHeader.Options.UseFont = true;
            this.gridColumn18.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn18.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn18.Caption = "Phòng";
            this.gridColumn18.FieldName = "departmentname";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.AllowEdit = false;
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 8;
            this.gridColumn18.Width = 145;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn14.AppearanceCell.Options.UseFont = true;
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn14.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn14.AppearanceHeader.Options.UseFont = true;
            this.gridColumn14.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn14.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn14.Caption = "Thời gian duyệt VP";
            this.gridColumn14.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn14.FieldName = "vienphidate";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 9;
            this.gridColumn14.Width = 134;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn15.AppearanceCell.Options.UseFont = true;
            this.gridColumn15.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn15.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn15.AppearanceHeader.Options.UseFont = true;
            this.gridColumn15.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn15.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn15.Caption = "Trạng thái";
            this.gridColumn15.FieldName = "medicalrecordstatus";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 10;
            this.gridColumn15.Width = 200;
            // 
            // openFileDialogSelect
            // 
            this.openFileDialogSelect.Filter = "Excel 2003|*.xls|Excel 2007-2013|*.xlsx";
            this.openFileDialogSelect.Title = "Mở file Excel";
            // 
            // ucKTHSBALoiTrangThai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlHSBA);
            this.Controls.Add(this.panelControlThongTinDV);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucKTHSBALoiTrangThai";
            this.Size = new System.Drawing.Size(1000, 594);
            this.Load += new System.EventHandler(this.ucUpdateDataSerPrice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinDV)).EndInit();
            this.panelControlThongTinDV.ResumeLayout(false);
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            this.groupBoxAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlHSBA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHSBA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlThongTinDV;
        private DevExpress.XtraGrid.GridControl gridControlHSBA;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewHSBA;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.OpenFileDialog openFileDialogSelect;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.DateTimePicker dateDenNgay;
        private System.Windows.Forms.DateTimePicker dateTuNgay;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.SimpleButton tbnExport;
        private DevExpress.XtraGrid.Columns.GridColumn stt;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.SimpleButton btnSuaTTHSBA;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}
