namespace O2S_MLReportRIS
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gridControlSoCDHA = new DevExpress.XtraGrid.GridControl();
            this.bandedGridViewSoCDHA = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.stt = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandTuoi = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn16 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBandCoPhim = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridColumn9 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBandThuocVTDiKem = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.cboPhongThucHien = new DevExpress.XtraEditors.LookUpEdit();
            this.cboTieuChi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dateTuNgay = new System.Windows.Forms.DateTimePicker();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.tbnExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSoCDHA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridViewSoCDHA)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPhongThucHien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTieuChi.Properties)).BeginInit();
            this.groupBoxAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(1084, 612);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gridControlSoCDHA);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(5, 105);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1074, 502);
            this.panel3.TabIndex = 1;
            // 
            // gridControlSoCDHA
            // 
            this.gridControlSoCDHA.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.gridControlSoCDHA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSoCDHA.EmbeddedNavigator.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.gridControlSoCDHA.Location = new System.Drawing.Point(0, 0);
            this.gridControlSoCDHA.MainView = this.bandedGridViewSoCDHA;
            this.gridControlSoCDHA.Name = "gridControlSoCDHA";
            this.gridControlSoCDHA.Size = new System.Drawing.Size(1074, 502);
            this.gridControlSoCDHA.TabIndex = 5;
            this.gridControlSoCDHA.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridViewSoCDHA});
            // 
            // bandedGridViewSoCDHA
            // 
            this.bandedGridViewSoCDHA.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bandedGridViewSoCDHA.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridViewSoCDHA.Appearance.HeaderPanel.Options.UseFont = true;
            this.bandedGridViewSoCDHA.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.bandedGridViewSoCDHA.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.bandedGridViewSoCDHA.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.bandTuoi,
            this.gridBand3,
            this.gridBandCoPhim,
            this.gridBandThuocVTDiKem});
            this.bandedGridViewSoCDHA.ColumnPanelRowHeight = 25;
            this.bandedGridViewSoCDHA.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.stt,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn16,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn15,
            this.bandedGridColumn2,
            this.bandedGridColumn3,
            this.bandedGridColumn6,
            this.bandedGridColumn8,
            this.bandedGridColumn7,
            this.gridColumn8,
            this.bandedGridColumn1,
            this.gridColumn9,
            this.gridColumn10,
            this.bandedGridColumn4,
            this.bandedGridColumn5});
            this.bandedGridViewSoCDHA.GridControl = this.gridControlSoCDHA;
            this.bandedGridViewSoCDHA.IndicatorWidth = 50;
            this.bandedGridViewSoCDHA.Name = "bandedGridViewSoCDHA";
            this.bandedGridViewSoCDHA.OptionsView.ColumnAutoWidth = false;
            this.bandedGridViewSoCDHA.OptionsView.EnableAppearanceEvenRow = true;
            this.bandedGridViewSoCDHA.OptionsView.ShowGroupPanel = false;
            this.bandedGridViewSoCDHA.OptionsView.ShowIndicator = false;
            this.bandedGridViewSoCDHA.RowHeight = 25;
            this.bandedGridViewSoCDHA.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.bandedGridViewSoCDHA_RowCellStyle);
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Columns.Add(this.stt);
            this.gridBand1.Columns.Add(this.gridColumn1);
            this.gridBand1.Columns.Add(this.gridColumn2);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.OptionsBand.ShowCaption = false;
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 300;
            // 
            // stt
            // 
            this.stt.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.stt.AppearanceCell.Options.UseFont = true;
            this.stt.AppearanceCell.Options.UseTextOptions = true;
            this.stt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.stt.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.stt.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.stt.AppearanceHeader.Options.UseFont = true;
            this.stt.AppearanceHeader.Options.UseForeColor = true;
            this.stt.AppearanceHeader.Options.UseTextOptions = true;
            this.stt.Caption = "STT";
            this.stt.FieldName = "stt";
            this.stt.Name = "stt";
            this.stt.OptionsColumn.AllowEdit = false;
            this.stt.Visible = true;
            this.stt.Width = 60;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn1.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.Caption = "Mã BN";
            this.gridColumn1.FieldName = "patientcode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.Width = 94;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn2.AppearanceCell.Options.UseFont = true;
            this.gridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn2.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn2.AppearanceHeader.Options.UseFont = true;
            this.gridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.Caption = "Họ tên người bệnh";
            this.gridColumn2.FieldName = "patientname";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.Width = 146;
            // 
            // bandTuoi
            // 
            this.bandTuoi.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.bandTuoi.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandTuoi.AppearanceHeader.Options.UseFont = true;
            this.bandTuoi.AppearanceHeader.Options.UseForeColor = true;
            this.bandTuoi.AppearanceHeader.Options.UseTextOptions = true;
            this.bandTuoi.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandTuoi.Caption = "Tuổi";
            this.bandTuoi.Columns.Add(this.gridColumn16);
            this.bandTuoi.Columns.Add(this.gridColumn3);
            this.bandTuoi.Name = "bandTuoi";
            this.bandTuoi.VisibleIndex = 1;
            this.bandTuoi.Width = 90;
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn16.AppearanceCell.Options.UseFont = true;
            this.gridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn16.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn16.AppearanceHeader.Options.UseFont = true;
            this.gridColumn16.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn16.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn16.Caption = "Nam";
            this.gridColumn16.FieldName = "year_nam";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.Width = 45;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn3.AppearanceCell.Options.UseFont = true;
            this.gridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn3.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn3.AppearanceHeader.Options.UseFont = true;
            this.gridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.Caption = "Nữ";
            this.gridColumn3.FieldName = "year_nu";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.Width = 45;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "gridBand3";
            this.gridBand3.Columns.Add(this.gridColumn4);
            this.gridBand3.Columns.Add(this.gridColumn5);
            this.gridBand3.Columns.Add(this.gridColumn6);
            this.gridBand3.Columns.Add(this.gridColumn7);
            this.gridBand3.Columns.Add(this.gridColumn15);
            this.gridBand3.Columns.Add(this.bandedGridColumn2);
            this.gridBand3.Columns.Add(this.bandedGridColumn3);
            this.gridBand3.Columns.Add(this.bandedGridColumn6);
            this.gridBand3.Columns.Add(this.bandedGridColumn8);
            this.gridBand3.Columns.Add(this.bandedGridColumn7);
            this.gridBand3.Columns.Add(this.gridColumn8);
            this.gridBand3.Columns.Add(this.bandedGridColumn1);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.OptionsBand.ShowCaption = false;
            this.gridBand3.VisibleIndex = 2;
            this.gridBand3.Width = 2528;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn4.AppearanceCell.Options.UseFont = true;
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn4.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn4.AppearanceHeader.Options.UseFont = true;
            this.gridColumn4.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.Caption = "Địa chỉ";
            this.gridColumn4.FieldName = "diachi";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.Width = 268;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn5.AppearanceCell.Options.UseFont = true;
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn5.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn5.AppearanceHeader.Options.UseFont = true;
            this.gridColumn5.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.Caption = "Có BHYT";
            this.gridColumn5.FieldName = "cobhyt";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.Width = 60;
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
            this.gridColumn6.Caption = "Chẩn đoán";
            this.gridColumn6.FieldName = "chandoan";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.Width = 232;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn7.AppearanceCell.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn7.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn7.AppearanceHeader.Options.UseFont = true;
            this.gridColumn7.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.Caption = "Nơi gửi";
            this.gridColumn7.FieldName = "noigui";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.Width = 258;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn15.AppearanceCell.Options.UseFont = true;
            this.gridColumn15.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn15.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn15.AppearanceHeader.Options.UseFont = true;
            this.gridColumn15.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn15.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn15.Caption = "Yêu cầu";
            this.gridColumn15.FieldName = "yeucau";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.Width = 291;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.bandedGridColumn2.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.bandedGridColumn2.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridColumn2.AppearanceHeader.Options.UseFont = true;
            this.bandedGridColumn2.AppearanceHeader.Options.UseForeColor = true;
            this.bandedGridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn2.Caption = "Ngày chỉ định";
            this.bandedGridColumn2.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.bandedGridColumn2.FieldName = "maubenhphamdate";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 120;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandedGridColumn3.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn3.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandedGridColumn3.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridColumn3.AppearanceHeader.Options.UseFont = true;
            this.bandedGridColumn3.AppearanceHeader.Options.UseForeColor = true;
            this.bandedGridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn3.Caption = "Ngày trả kết quả";
            this.bandedGridColumn3.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.bandedGridColumn3.FieldName = "maubenhphamfinishdate";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn3.Visible = true;
            this.bandedGridColumn3.Width = 120;
            // 
            // bandedGridColumn6
            // 
            this.bandedGridColumn6.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandedGridColumn6.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn6.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandedGridColumn6.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridColumn6.AppearanceHeader.Options.UseFont = true;
            this.bandedGridColumn6.AppearanceHeader.Options.UseForeColor = true;
            this.bandedGridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn6.Caption = "Ngày trả kết quả từng phần";
            this.bandedGridColumn6.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.bandedGridColumn6.FieldName = "servicetimetrakq";
            this.bandedGridColumn6.Name = "bandedGridColumn6";
            this.bandedGridColumn6.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn6.Visible = true;
            this.bandedGridColumn6.Width = 120;
            // 
            // bandedGridColumn8
            // 
            this.bandedGridColumn8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.bandedGridColumn8.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandedGridColumn8.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridColumn8.AppearanceHeader.Options.UseFont = true;
            this.bandedGridColumn8.AppearanceHeader.Options.UseForeColor = true;
            this.bandedGridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn8.Caption = "Ngày ra viện";
            this.bandedGridColumn8.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.bandedGridColumn8.FieldName = "vienphidate_ravien";
            this.bandedGridColumn8.Name = "bandedGridColumn8";
            this.bandedGridColumn8.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn8.Visible = true;
            this.bandedGridColumn8.Width = 120;
            // 
            // bandedGridColumn7
            // 
            this.bandedGridColumn7.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.bandedGridColumn7.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn7.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandedGridColumn7.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridColumn7.AppearanceHeader.Options.UseFont = true;
            this.bandedGridColumn7.AppearanceHeader.Options.UseForeColor = true;
            this.bandedGridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn7.Caption = "Ngày duyệt VP";
            this.bandedGridColumn7.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.bandedGridColumn7.FieldName = "duyet_ngayduyet_vp";
            this.bandedGridColumn7.Name = "bandedGridColumn7";
            this.bandedGridColumn7.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn7.Visible = true;
            this.bandedGridColumn7.Width = 120;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn8.AppearanceCell.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn8.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn8.AppearanceHeader.Options.UseFont = true;
            this.gridColumn8.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.Caption = "Kết quả";
            this.gridColumn8.FieldName = "ketqua";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.Width = 600;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.bandedGridColumn1.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.bandedGridColumn1.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.bandedGridColumn1.AppearanceHeader.Options.UseFont = true;
            this.bandedGridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.bandedGridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn1.Caption = "Người đọc";
            this.bandedGridColumn1.FieldName = "nguoidoc";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 219;
            // 
            // gridBandCoPhim
            // 
            this.gridBandCoPhim.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridBandCoPhim.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridBandCoPhim.AppearanceHeader.Options.UseFont = true;
            this.gridBandCoPhim.AppearanceHeader.Options.UseForeColor = true;
            this.gridBandCoPhim.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBandCoPhim.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBandCoPhim.Caption = "Cỡ phim";
            this.gridBandCoPhim.Columns.Add(this.gridColumn9);
            this.gridBandCoPhim.Columns.Add(this.gridColumn10);
            this.gridBandCoPhim.Name = "gridBandCoPhim";
            this.gridBandCoPhim.VisibleIndex = 3;
            this.gridBandCoPhim.Width = 180;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn9.AppearanceCell.Options.UseFont = true;
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn9.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn9.AppearanceHeader.Options.UseFont = true;
            this.gridColumn9.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.Caption = "20x25  (Xquang)";
            this.gridColumn9.FieldName = "phim_20x25";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.Width = 90;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn10.AppearanceCell.Options.UseFont = true;
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gridColumn10.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn10.AppearanceHeader.Options.UseFont = true;
            this.gridColumn10.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.Caption = "35x43 (CT+MRI)";
            this.gridColumn10.FieldName = "phim_35x43";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.Width = 90;
            // 
            // gridBandThuocVTDiKem
            // 
            this.gridBandThuocVTDiKem.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridBandThuocVTDiKem.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridBandThuocVTDiKem.AppearanceHeader.Options.UseFont = true;
            this.gridBandThuocVTDiKem.AppearanceHeader.Options.UseForeColor = true;
            this.gridBandThuocVTDiKem.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBandThuocVTDiKem.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBandThuocVTDiKem.Caption = "Thuốc/vật tư đi kèm";
            this.gridBandThuocVTDiKem.Columns.Add(this.bandedGridColumn4);
            this.gridBandThuocVTDiKem.Columns.Add(this.bandedGridColumn5);
            this.gridBandThuocVTDiKem.Name = "gridBandThuocVTDiKem";
            this.gridBandThuocVTDiKem.VisibleIndex = 4;
            this.gridBandThuocVTDiKem.Width = 150;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandedGridColumn4.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandedGridColumn4.AppearanceHeader.Options.UseFont = true;
            this.bandedGridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn4.Caption = "Có thuốc đi kèm";
            this.bandedGridColumn4.FieldName = "isthuocdikem";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn4.Visible = true;
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandedGridColumn5.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumn5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandedGridColumn5.AppearanceHeader.Options.UseFont = true;
            this.bandedGridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumn5.Caption = "Có vật tư đi kèm";
            this.bandedGridColumn5.FieldName = "isvattudikem";
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn5.Visible = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBoxFile);
            this.panel2.Controls.Add(this.groupBoxAction);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1074, 100);
            this.panel2.TabIndex = 0;
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Controls.Add(this.cboPhongThucHien);
            this.groupBoxFile.Controls.Add(this.cboTieuChi);
            this.groupBoxFile.Controls.Add(this.button1);
            this.groupBoxFile.Controls.Add(this.label3);
            this.groupBoxFile.Controls.Add(this.label4);
            this.groupBoxFile.Controls.Add(this.label2);
            this.groupBoxFile.Controls.Add(this.label1);
            this.groupBoxFile.Controls.Add(this.dateDenNgay);
            this.groupBoxFile.Controls.Add(this.dateTuNgay);
            this.groupBoxFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxFile.Location = new System.Drawing.Point(0, 0);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(944, 100);
            this.groupBoxFile.TabIndex = 11;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "Tìm kiếm";
            // 
            // cboPhongThucHien
            // 
            this.cboPhongThucHien.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.True;
            this.cboPhongThucHien.Location = new System.Drawing.Point(358, 59);
            this.cboPhongThucHien.Name = "cboPhongThucHien";
            this.cboPhongThucHien.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPhongThucHien.Properties.Appearance.Options.UseFont = true;
            this.cboPhongThucHien.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPhongThucHien.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboPhongThucHien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPhongThucHien.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("departmentid", 10, "ID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("departmentname", 50, "Tên phòng")});
            this.cboPhongThucHien.Properties.DropDownRows = 10;
            this.cboPhongThucHien.Properties.NullText = "";
            this.cboPhongThucHien.Properties.PopupSizeable = false;
            this.cboPhongThucHien.Size = new System.Drawing.Size(314, 22);
            this.cboPhongThucHien.TabIndex = 77;
            this.cboPhongThucHien.EditValueChanged += new System.EventHandler(this.cboPhongThucHien_EditValueChanged);
            // 
            // cboTieuChi
            // 
            this.cboTieuChi.EditValue = "Theo ngày chỉ định";
            this.cboTieuChi.Location = new System.Drawing.Point(358, 22);
            this.cboTieuChi.Name = "cboTieuChi";
            this.cboTieuChi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTieuChi.Properties.Appearance.Options.UseFont = true;
            this.cboTieuChi.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTieuChi.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboTieuChi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTieuChi.Properties.Items.AddRange(new object[] {
            "Theo ngày chỉ định",
            "Theo ngày trả kết quả",
            "Theo ngày ra viện",
            "Theo ngày duyệt VP"});
            this.cboTieuChi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboTieuChi.Size = new System.Drawing.Size(181, 22);
            this.cboTieuChi.TabIndex = 76;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(708, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 40);
            this.button1.TabIndex = 26;
            this.button1.Text = "Tìm kiếm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(252, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "Phòng thực hiện";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(299, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "Tiêu chí";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "Đến";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "Từ";
            // 
            // dateDenNgay
            // 
            this.dateDenNgay.CalendarFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateDenNgay.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            this.dateDenNgay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateDenNgay.Location = new System.Drawing.Point(56, 57);
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
            this.dateTuNgay.Location = new System.Drawing.Point(56, 20);
            this.dateTuNgay.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dateTuNgay.Name = "dateTuNgay";
            this.dateTuNgay.Size = new System.Drawing.Size(159, 23);
            this.dateTuNgay.TabIndex = 18;
            this.dateTuNgay.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.tbnExport);
            this.groupBoxAction.Controls.Add(this.btnPrint);
            this.groupBoxAction.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBoxAction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxAction.Location = new System.Drawing.Point(944, 0);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(130, 100);
            this.groupBoxAction.TabIndex = 10;
            this.groupBoxAction.TabStop = false;
            // 
            // tbnExport
            // 
            this.tbnExport.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbnExport.Image = ((System.Drawing.Image)(resources.GetObject("tbnExport.Image")));
            this.tbnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbnExport.Location = new System.Drawing.Point(18, 54);
            this.tbnExport.Name = "tbnExport";
            this.tbnExport.Size = new System.Drawing.Size(100, 30);
            this.tbnExport.TabIndex = 28;
            this.tbnExport.Text = "Xuất file";
            this.tbnExport.UseVisualStyleBackColor = true;
            this.tbnExport.Click += new System.EventHandler(this.tbnExport_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(18, 17);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 27;
            this.btnPrint.Text = "In...";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 612);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sổ chuẩn đoán hình ảnh";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSoCDHA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridViewSoCDHA)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPhongThucHien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTieuChi.Properties)).EndInit();
            this.groupBoxAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.DateTimePicker dateDenNgay;
        private System.Windows.Forms.DateTimePicker dateTuNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button tbnExport;
        private DevExpress.XtraGrid.GridControl gridControlSoCDHA;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridViewSoCDHA;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn stt;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn16;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn15;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn10;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn5;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandTuoi;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandCoPhim;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandThuocVTDiKem;
        private DevExpress.XtraEditors.LookUpEdit cboPhongThucHien;
        private DevExpress.XtraEditors.ComboBoxEdit cboTieuChi;
    }
}

