namespace MedicalLink.FormCommon.TabCaiDat
{
    partial class ucDanhSachNhanVien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDanhSachNhanVien));
            this.panelControlThongTinNV = new DevExpress.XtraEditors.PanelControl();
            this.groupBoxChucNang = new System.Windows.Forms.GroupBox();
            this.btnNVThem = new DevExpress.XtraEditors.SimpleButton();
            this.groupBoxThongTin = new System.Windows.Forms.GroupBox();
            this.txtIDHIS = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnThemTuExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnNVOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtNVName = new DevExpress.XtraEditors.TextEdit();
            this.txtNVID = new DevExpress.XtraEditors.TextEdit();
            this.lblUserId = new DevExpress.XtraEditors.LabelControl();
            this.lblUsername = new DevExpress.XtraEditors.LabelControl();
            this.panelControlNV_DT = new DevExpress.XtraEditors.PanelControl();
            this.gridControlDSNV = new DevExpress.XtraGrid.GridControl();
            this.gridViewDSNV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.stt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.manv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tennv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.openFileDialogSelect = new System.Windows.Forms.OpenFileDialog();
            this.imMenu = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinNV)).BeginInit();
            this.panelControlThongTinNV.SuspendLayout();
            this.groupBoxChucNang.SuspendLayout();
            this.groupBoxThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIDHIS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNVName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNVID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlNV_DT)).BeginInit();
            this.panelControlNV_DT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDSNV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSNV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControlThongTinNV
            // 
            this.panelControlThongTinNV.Controls.Add(this.groupBoxChucNang);
            this.panelControlThongTinNV.Controls.Add(this.groupBoxThongTin);
            this.panelControlThongTinNV.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlThongTinNV.Location = new System.Drawing.Point(0, 0);
            this.panelControlThongTinNV.Name = "panelControlThongTinNV";
            this.panelControlThongTinNV.Size = new System.Drawing.Size(1096, 85);
            this.panelControlThongTinNV.TabIndex = 1;
            // 
            // groupBoxChucNang
            // 
            this.groupBoxChucNang.Controls.Add(this.btnNVThem);
            this.groupBoxChucNang.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxChucNang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxChucNang.Location = new System.Drawing.Point(2, 2);
            this.groupBoxChucNang.Name = "groupBoxChucNang";
            this.groupBoxChucNang.Size = new System.Drawing.Size(146, 81);
            this.groupBoxChucNang.TabIndex = 8;
            this.groupBoxChucNang.TabStop = false;
            this.groupBoxChucNang.Text = "Chức năng";
            // 
            // btnNVThem
            // 
            this.btnNVThem.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNVThem.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnNVThem.Appearance.Options.UseFont = true;
            this.btnNVThem.Appearance.Options.UseForeColor = true;
            this.btnNVThem.Image = ((System.Drawing.Image)(resources.GetObject("btnNVThem.Image")));
            this.btnNVThem.Location = new System.Drawing.Point(15, 22);
            this.btnNVThem.Name = "btnNVThem";
            this.btnNVThem.Size = new System.Drawing.Size(100, 40);
            this.btnNVThem.TabIndex = 5;
            this.btnNVThem.Text = "Thêm";
            this.btnNVThem.Click += new System.EventHandler(this.btnNVThem_Click);
            // 
            // groupBoxThongTin
            // 
            this.groupBoxThongTin.Controls.Add(this.txtIDHIS);
            this.groupBoxThongTin.Controls.Add(this.labelControl1);
            this.groupBoxThongTin.Controls.Add(this.btnThemTuExcel);
            this.groupBoxThongTin.Controls.Add(this.btnNVOK);
            this.groupBoxThongTin.Controls.Add(this.txtNVName);
            this.groupBoxThongTin.Controls.Add(this.txtNVID);
            this.groupBoxThongTin.Controls.Add(this.lblUserId);
            this.groupBoxThongTin.Controls.Add(this.lblUsername);
            this.groupBoxThongTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxThongTin.Location = new System.Drawing.Point(154, 2);
            this.groupBoxThongTin.Name = "groupBoxThongTin";
            this.groupBoxThongTin.Size = new System.Drawing.Size(846, 80);
            this.groupBoxThongTin.TabIndex = 7;
            this.groupBoxThongTin.TabStop = false;
            this.groupBoxThongTin.Text = "Thông tin về tài khoản";
            // 
            // txtIDHIS
            // 
            this.txtIDHIS.Location = new System.Drawing.Point(285, 19);
            this.txtIDHIS.Name = "txtIDHIS";
            this.txtIDHIS.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDHIS.Properties.Appearance.Options.UseFont = true;
            this.txtIDHIS.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIDHIS.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtIDHIS.Size = new System.Drawing.Size(145, 22);
            this.txtIDHIS.TabIndex = 10;
            this.txtIDHIS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIDHIS_KeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(242, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 16);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "ID HIS";
            // 
            // btnThemTuExcel
            // 
            this.btnThemTuExcel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemTuExcel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnThemTuExcel.Appearance.Options.UseFont = true;
            this.btnThemTuExcel.Appearance.Options.UseForeColor = true;
            this.btnThemTuExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnThemTuExcel.Image")));
            this.btnThemTuExcel.Location = new System.Drawing.Point(639, 22);
            this.btnThemTuExcel.Name = "btnThemTuExcel";
            this.btnThemTuExcel.Size = new System.Drawing.Size(130, 40);
            this.btnThemTuExcel.TabIndex = 8;
            this.btnThemTuExcel.Text = "Thêm từ Excel";
            this.btnThemTuExcel.Click += new System.EventHandler(this.btnThemTuExcel_Click);
            // 
            // btnNVOK
            // 
            this.btnNVOK.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNVOK.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnNVOK.Appearance.Options.UseFont = true;
            this.btnNVOK.Appearance.Options.UseForeColor = true;
            this.btnNVOK.Image = ((System.Drawing.Image)(resources.GetObject("btnNVOK.Image")));
            this.btnNVOK.Location = new System.Drawing.Point(500, 22);
            this.btnNVOK.Name = "btnNVOK";
            this.btnNVOK.Size = new System.Drawing.Size(100, 40);
            this.btnNVOK.TabIndex = 7;
            this.btnNVOK.Text = "OK";
            this.btnNVOK.Click += new System.EventHandler(this.btnNVOK_Click_1);
            // 
            // txtNVName
            // 
            this.txtNVName.Location = new System.Drawing.Point(78, 54);
            this.txtNVName.Name = "txtNVName";
            this.txtNVName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNVName.Properties.Appearance.Options.UseFont = true;
            this.txtNVName.Size = new System.Drawing.Size(352, 22);
            this.txtNVName.TabIndex = 6;
            // 
            // txtNVID
            // 
            this.txtNVID.Location = new System.Drawing.Point(78, 20);
            this.txtNVID.Name = "txtNVID";
            this.txtNVID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNVID.Properties.Appearance.Options.UseFont = true;
            this.txtNVID.Size = new System.Drawing.Size(145, 22);
            this.txtNVID.TabIndex = 4;
            // 
            // lblUserId
            // 
            this.lblUserId.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserId.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblUserId.Location = new System.Drawing.Point(20, 23);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(42, 16);
            this.lblUserId.TabIndex = 1;
            this.lblUserId.Text = "User ID";
            // 
            // lblUsername
            // 
            this.lblUsername.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblUsername.Location = new System.Drawing.Point(4, 57);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 16);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Username";
            // 
            // panelControlNV_DT
            // 
            this.panelControlNV_DT.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlNV_DT.Controls.Add(this.gridControlDSNV);
            this.panelControlNV_DT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlNV_DT.Location = new System.Drawing.Point(0, 85);
            this.panelControlNV_DT.Name = "panelControlNV_DT";
            this.panelControlNV_DT.Size = new System.Drawing.Size(1096, 528);
            this.panelControlNV_DT.TabIndex = 2;
            // 
            // gridControlDSNV
            // 
            this.gridControlDSNV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDSNV.Location = new System.Drawing.Point(0, 0);
            this.gridControlDSNV.MainView = this.gridViewDSNV;
            this.gridControlDSNV.Name = "gridControlDSNV";
            this.gridControlDSNV.Size = new System.Drawing.Size(1096, 528);
            this.gridControlDSNV.TabIndex = 0;
            this.gridControlDSNV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDSNV});
            // 
            // gridViewDSNV
            // 
            this.gridViewDSNV.ColumnPanelRowHeight = 25;
            this.gridViewDSNV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.stt,
            this.manv,
            this.tennv,
            this.gridColumn1});
            this.gridViewDSNV.GridControl = this.gridControlDSNV;
            this.gridViewDSNV.Name = "gridViewDSNV";
            this.gridViewDSNV.OptionsFind.AlwaysVisible = true;
            this.gridViewDSNV.OptionsFind.FindNullPrompt = "Từ khóa tìm kiếm...";
            this.gridViewDSNV.OptionsFind.ShowClearButton = false;
            this.gridViewDSNV.OptionsFind.ShowCloseButton = false;
            this.gridViewDSNV.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewDSNV.OptionsView.ShowGroupPanel = false;
            this.gridViewDSNV.OptionsView.ShowIndicator = false;
            this.gridViewDSNV.RowHeight = 25;
            this.gridViewDSNV.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewDSNV_CustomDrawCell);
            this.gridViewDSNV.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewDSNV_RowCellStyle);
            this.gridViewDSNV.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridViewDSNV_PopupMenuShowing);
            this.gridViewDSNV.Click += new System.EventHandler(this.gridViewDSNV_Click);
            // 
            // stt
            // 
            this.stt.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stt.AppearanceCell.Options.UseFont = true;
            this.stt.AppearanceCell.Options.UseTextOptions = true;
            this.stt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.stt.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stt.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.stt.AppearanceHeader.Options.UseFont = true;
            this.stt.AppearanceHeader.Options.UseForeColor = true;
            this.stt.Caption = "STT";
            this.stt.FieldName = "stt";
            this.stt.Name = "stt";
            this.stt.OptionsColumn.AllowEdit = false;
            this.stt.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.stt.Visible = true;
            this.stt.VisibleIndex = 0;
            this.stt.Width = 70;
            // 
            // manv
            // 
            this.manv.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manv.AppearanceCell.Options.UseFont = true;
            this.manv.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manv.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.manv.AppearanceHeader.Options.UseFont = true;
            this.manv.AppearanceHeader.Options.UseForeColor = true;
            this.manv.AppearanceHeader.Options.UseTextOptions = true;
            this.manv.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.manv.Caption = "Mã Nhân Viên";
            this.manv.FieldName = "manv";
            this.manv.Name = "manv";
            this.manv.OptionsColumn.AllowEdit = false;
            this.manv.Visible = true;
            this.manv.VisibleIndex = 1;
            this.manv.Width = 293;
            // 
            // tennv
            // 
            this.tennv.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tennv.AppearanceCell.Options.UseFont = true;
            this.tennv.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tennv.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tennv.AppearanceHeader.Options.UseFont = true;
            this.tennv.AppearanceHeader.Options.UseForeColor = true;
            this.tennv.AppearanceHeader.Options.UseTextOptions = true;
            this.tennv.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tennv.Caption = "Tên Nhân Viên";
            this.tennv.FieldName = "tennv";
            this.tennv.Name = "tennv";
            this.tennv.OptionsColumn.AllowEdit = false;
            this.tennv.Visible = true;
            this.tennv.VisibleIndex = 2;
            this.tennv.Width = 533;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceCell.Options.UseFont = true;
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridColumn1.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gridColumn1.AppearanceHeader.Options.UseFont = true;
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "ID HIS";
            this.gridColumn1.FieldName = "userhisid";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 154;
            // 
            // openFileDialogSelect
            // 
            this.openFileDialogSelect.Filter = "Excel 2003|*.xls|Excel 2007-2013|*.xlsx";
            this.openFileDialogSelect.Title = "Mở file Excel";
            // 
            // imMenu
            // 
            this.imMenu.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imMenu.ImageStream")));
            this.imMenu.InsertGalleryImage("HanhChinh.png", "images/actions/hide_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/hide_16x16.png"), 0);
            this.imMenu.Images.SetKeyName(0, "HanhChinh.png");
            this.imMenu.InsertGalleryImage("XoaDT.png", "images/actions/clear_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/clear_16x16.png"), 1);
            this.imMenu.Images.SetKeyName(1, "XoaDT.png");
            this.imMenu.InsertGalleryImage("XoaDTHC.png", "images/actions/remove_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/remove_16x16.png"), 2);
            this.imMenu.Images.SetKeyName(2, "XoaDTHC.png");
            this.imMenu.InsertGalleryImage("Xoa.png", "images/actions/cancel_16x16.png", DevExpress.Images.ImageResourceCache.Default.GetImage("images/actions/cancel_16x16.png"), 3);
            this.imMenu.Images.SetKeyName(3, "Xoa.png");
            // 
            // ucDanhSachNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlNV_DT);
            this.Controls.Add(this.panelControlThongTinNV);
            this.Name = "ucDanhSachNhanVien";
            this.Size = new System.Drawing.Size(1096, 613);
            this.Load += new System.EventHandler(this.ucDanhSachNhanVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinNV)).EndInit();
            this.panelControlThongTinNV.ResumeLayout(false);
            this.groupBoxChucNang.ResumeLayout(false);
            this.groupBoxThongTin.ResumeLayout(false);
            this.groupBoxThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIDHIS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNVName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNVID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlNV_DT)).EndInit();
            this.panelControlNV_DT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDSNV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSNV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlThongTinNV;
        private System.Windows.Forms.GroupBox groupBoxChucNang;
        private DevExpress.XtraEditors.SimpleButton btnNVThem;
        private System.Windows.Forms.GroupBox groupBoxThongTin;
        private DevExpress.XtraEditors.TextEdit txtNVName;
        private DevExpress.XtraEditors.TextEdit txtNVID;
        private DevExpress.XtraEditors.LabelControl lblUserId;
        private DevExpress.XtraEditors.LabelControl lblUsername;
        private DevExpress.XtraEditors.PanelControl panelControlNV_DT;
        private DevExpress.XtraGrid.GridControl gridControlDSNV;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDSNV;
        private DevExpress.XtraGrid.Columns.GridColumn manv;
        private DevExpress.XtraGrid.Columns.GridColumn tennv;
        protected internal DevExpress.XtraGrid.Columns.GridColumn stt;
        private DevExpress.XtraEditors.SimpleButton btnNVOK;
        private DevExpress.XtraEditors.SimpleButton btnThemTuExcel;
        internal System.Windows.Forms.OpenFileDialog openFileDialogSelect;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.TextEdit txtIDHIS;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.ImageCollection imMenu;

    }
}
