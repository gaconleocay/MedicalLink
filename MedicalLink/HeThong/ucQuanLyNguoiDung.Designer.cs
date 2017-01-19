namespace MedicalLink.HeThong
{
    partial class ucQuanLyNguoiDung
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucQuanLyNguoiDung));
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            this.checker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelControlThongTinUS = new DevExpress.XtraEditors.PanelControl();
            this.groupBoxChucNang = new System.Windows.Forms.GroupBox();
            this.btnUserThem = new DevExpress.XtraEditors.SimpleButton();
            this.groupBoxThongTin = new System.Windows.Forms.GroupBox();
            this.cbbUserNhom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnUserOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.txtUserID = new DevExpress.XtraEditors.TextEdit();
            this.txtUserPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblUserId = new DevExpress.XtraEditors.LabelControl();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.lblUsername = new DevExpress.XtraEditors.LabelControl();
            this.panelControlPhanQuyen = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControlDSUser = new DevExpress.XtraGrid.GridControl();
            this.gridViewDSUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.usercode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.username = new DevExpress.XtraGrid.Columns.GridColumn();
            this.usergnhom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblThongBao = new DevExpress.XtraEditors.LabelControl();
            this.gridControlChucNang = new DevExpress.XtraGrid.GridControl();
            this.gridViewChucNang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.permissioncheck2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chucnangid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tenchucnang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.timerThongBao = new System.Windows.Forms.Timer(this.components);
            this.imMenu = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinUS)).BeginInit();
            this.panelControlThongTinUS.SuspendLayout();
            this.groupBoxChucNang.SuspendLayout();
            this.groupBoxThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbbUserNhom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlPhanQuyen)).BeginInit();
            this.panelControlPhanQuyen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDSUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlChucNang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChucNang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // checker
            // 
            this.checker.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checker.AppearanceHeader.Options.UseForeColor = true;
            this.checker.AppearanceHeader.Options.UseTextOptions = true;
            this.checker.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.checker.Caption = "#";
            this.checker.ColumnEdit = this.repositoryItemCheckEdit1;
            this.checker.FieldName = "permissioncheck";
            this.checker.Image = ((System.Drawing.Image)(resources.GetObject("checker.Image")));
            this.checker.MaxWidth = 20;
            this.checker.Name = "checker";
            this.checker.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.checker.Visible = true;
            this.checker.VisibleIndex = 0;
            this.checker.Width = 20;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.PictureChecked = ((System.Drawing.Image)(resources.GetObject("repositoryItemCheckEdit1.PictureChecked")));
            // 
            // panelControlThongTinUS
            // 
            this.panelControlThongTinUS.Controls.Add(this.groupBoxChucNang);
            this.panelControlThongTinUS.Controls.Add(this.groupBoxThongTin);
            this.panelControlThongTinUS.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControlThongTinUS.Location = new System.Drawing.Point(0, 0);
            this.panelControlThongTinUS.Name = "panelControlThongTinUS";
            this.panelControlThongTinUS.Size = new System.Drawing.Size(1096, 79);
            this.panelControlThongTinUS.TabIndex = 0;
            // 
            // groupBoxChucNang
            // 
            this.groupBoxChucNang.Controls.Add(this.btnUserThem);
            this.groupBoxChucNang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxChucNang.Location = new System.Drawing.Point(2, 3);
            this.groupBoxChucNang.Name = "groupBoxChucNang";
            this.groupBoxChucNang.Size = new System.Drawing.Size(117, 73);
            this.groupBoxChucNang.TabIndex = 8;
            this.groupBoxChucNang.TabStop = false;
            this.groupBoxChucNang.Text = "Chức năng";
            // 
            // btnUserThem
            // 
            this.btnUserThem.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserThem.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnUserThem.Appearance.Options.UseFont = true;
            this.btnUserThem.Appearance.Options.UseForeColor = true;
            this.btnUserThem.Image = ((System.Drawing.Image)(resources.GetObject("btnUserThem.Image")));
            this.btnUserThem.Location = new System.Drawing.Point(9, 22);
            this.btnUserThem.Name = "btnUserThem";
            this.btnUserThem.Size = new System.Drawing.Size(100, 40);
            this.btnUserThem.TabIndex = 5;
            this.btnUserThem.Text = "Thêm";
            this.btnUserThem.Click += new System.EventHandler(this.btnUserThem_Click);
            // 
            // groupBoxThongTin
            // 
            this.groupBoxThongTin.Controls.Add(this.cbbUserNhom);
            this.groupBoxThongTin.Controls.Add(this.labelControl3);
            this.groupBoxThongTin.Controls.Add(this.btnUserOK);
            this.groupBoxThongTin.Controls.Add(this.txtUsername);
            this.groupBoxThongTin.Controls.Add(this.txtUserID);
            this.groupBoxThongTin.Controls.Add(this.txtUserPassword);
            this.groupBoxThongTin.Controls.Add(this.lblUserId);
            this.groupBoxThongTin.Controls.Add(this.lblPassword);
            this.groupBoxThongTin.Controls.Add(this.lblUsername);
            this.groupBoxThongTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBoxThongTin.Location = new System.Drawing.Point(125, 3);
            this.groupBoxThongTin.Name = "groupBoxThongTin";
            this.groupBoxThongTin.Size = new System.Drawing.Size(682, 73);
            this.groupBoxThongTin.TabIndex = 7;
            this.groupBoxThongTin.TabStop = false;
            this.groupBoxThongTin.Text = "Thông tin về tài khoản";
            // 
            // cbbUserNhom
            // 
            this.cbbUserNhom.Location = new System.Drawing.Point(70, 47);
            this.cbbUserNhom.Name = "cbbUserNhom";
            this.cbbUserNhom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbbUserNhom.Properties.Items.AddRange(new object[] {
            "Quản trị hệ thống",
            "Nhân viên"});
            this.cbbUserNhom.Size = new System.Drawing.Size(167, 20);
            this.cbbUserNhom.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl3.Location = new System.Drawing.Point(36, 51);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(27, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Nhóm";
            // 
            // btnUserOK
            // 
            this.btnUserOK.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserOK.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnUserOK.Appearance.Options.UseFont = true;
            this.btnUserOK.Appearance.Options.UseForeColor = true;
            this.btnUserOK.Image = ((System.Drawing.Image)(resources.GetObject("btnUserOK.Image")));
            this.btnUserOK.Location = new System.Drawing.Point(503, 22);
            this.btnUserOK.Name = "btnUserOK";
            this.btnUserOK.Size = new System.Drawing.Size(100, 40);
            this.btnUserOK.TabIndex = 8;
            this.btnUserOK.Text = "OK";
            this.btnUserOK.Click += new System.EventHandler(this.btnUserOK_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(323, 20);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(167, 20);
            this.txtUsername.TabIndex = 6;
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsername_KeyDown);
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(70, 20);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(167, 20);
            this.txtUserID.TabIndex = 4;
            this.txtUserID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserID_KeyDown);
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Location = new System.Drawing.Point(323, 48);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Properties.PasswordChar = '*';
            this.txtUserPassword.Size = new System.Drawing.Size(167, 20);
            this.txtUserPassword.TabIndex = 7;
            this.txtUserPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserPassword_KeyDown);
            // 
            // lblUserId
            // 
            this.lblUserId.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblUserId.Location = new System.Drawing.Point(27, 23);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(36, 13);
            this.lblUserId.TabIndex = 1;
            this.lblUserId.Text = "User ID";
            // 
            // lblPassword
            // 
            this.lblPassword.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblPassword.Location = new System.Drawing.Point(271, 51);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(46, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            this.lblUsername.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblUsername.Location = new System.Drawing.Point(269, 23);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(48, 13);
            this.lblUsername.TabIndex = 3;
            this.lblUsername.Text = "Username";
            // 
            // panelControlPhanQuyen
            // 
            this.panelControlPhanQuyen.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControlPhanQuyen.Controls.Add(this.splitContainerControl1);
            this.panelControlPhanQuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControlPhanQuyen.Location = new System.Drawing.Point(0, 79);
            this.panelControlPhanQuyen.Name = "panelControlPhanQuyen";
            this.panelControlPhanQuyen.Size = new System.Drawing.Size(1096, 534);
            this.panelControlPhanQuyen.TabIndex = 1;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControlDSUser);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.lblThongBao);
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControlChucNang);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1096, 534);
            this.splitContainerControl1.SplitterPosition = 465;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControlDSUser
            // 
            this.gridControlDSUser.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.gridControlDSUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDSUser.Location = new System.Drawing.Point(0, 22);
            this.gridControlDSUser.MainView = this.gridViewDSUser;
            this.gridControlDSUser.Name = "gridControlDSUser";
            this.gridControlDSUser.Size = new System.Drawing.Size(465, 508);
            this.gridControlDSUser.TabIndex = 0;
            this.gridControlDSUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDSUser});
            this.gridControlDSUser.Click += new System.EventHandler(this.gridControlDSUser_Click);
            // 
            // gridViewDSUser
            // 
            this.gridViewDSUser.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.usercode,
            this.username,
            this.usergnhom});
            this.gridViewDSUser.GridControl = this.gridControlDSUser;
            this.gridViewDSUser.Name = "gridViewDSUser";
            this.gridViewDSUser.OptionsView.ShowGroupPanel = false;
            this.gridViewDSUser.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewDSUser_RowCellStyle);
            this.gridViewDSUser.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridViewDSUser_PopupMenuShowing);
            // 
            // usercode
            // 
            this.usercode.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.usercode.AppearanceHeader.Options.UseForeColor = true;
            this.usercode.AppearanceHeader.Options.UseTextOptions = true;
            this.usercode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.usercode.Caption = "User Code";
            this.usercode.FieldName = "usercode";
            this.usercode.Name = "usercode";
            this.usercode.OptionsColumn.AllowEdit = false;
            this.usercode.Visible = true;
            this.usercode.VisibleIndex = 0;
            this.usercode.Width = 119;
            // 
            // username
            // 
            this.username.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.username.AppearanceHeader.Options.UseForeColor = true;
            this.username.AppearanceHeader.Options.UseTextOptions = true;
            this.username.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.username.Caption = "Username";
            this.username.FieldName = "username";
            this.username.Name = "username";
            this.username.OptionsColumn.AllowEdit = false;
            this.username.Visible = true;
            this.username.VisibleIndex = 1;
            this.username.Width = 202;
            // 
            // usergnhom
            // 
            this.usergnhom.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.usergnhom.AppearanceHeader.Options.UseForeColor = true;
            this.usergnhom.AppearanceHeader.Options.UseTextOptions = true;
            this.usergnhom.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.usergnhom.Caption = "Nhóm";
            this.usergnhom.FieldName = "usergnhom";
            this.usergnhom.Name = "usergnhom";
            this.usergnhom.OptionsColumn.AllowEdit = false;
            this.usergnhom.Visible = true;
            this.usergnhom.VisibleIndex = 2;
            this.usergnhom.Width = 60;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(465, 22);
            this.panelControl1.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl1.Location = new System.Drawing.Point(68, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(141, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Danh sách người sử dụng";
            // 
            // lblThongBao
            // 
            this.lblThongBao.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblThongBao.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblThongBao.Location = new System.Drawing.Point(20, 149);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(149, 23);
            this.lblThongBao.TabIndex = 20;
            this.lblThongBao.Text = "Sửa thành công";
            this.lblThongBao.Visible = false;
            // 
            // gridControlChucNang
            // 
            this.gridControlChucNang.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.gridControlChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlChucNang.Location = new System.Drawing.Point(0, 22);
            this.gridControlChucNang.MainView = this.gridViewChucNang;
            this.gridControlChucNang.Name = "gridControlChucNang";
            this.gridControlChucNang.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.gridControlChucNang.Size = new System.Drawing.Size(622, 508);
            this.gridControlChucNang.TabIndex = 1;
            this.gridControlChucNang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewChucNang});
            // 
            // gridViewChucNang
            // 
            this.gridViewChucNang.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.permissioncheck2,
            this.checker,
            this.chucnangid,
            this.tenchucnang});
            gridFormatRule1.Column = this.checker;
            gridFormatRule1.ColumnApplyTo = this.checker;
            gridFormatRule1.Name = "Format0";
            gridFormatRule1.Rule = null;
            this.gridViewChucNang.FormatRules.Add(gridFormatRule1);
            this.gridViewChucNang.GridControl = this.gridControlChucNang;
            this.gridViewChucNang.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.None, null, this.chucnangid, "")});
            this.gridViewChucNang.Name = "gridViewChucNang";
            this.gridViewChucNang.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            this.gridViewChucNang.OptionsView.ShowGroupPanel = false;
            this.gridViewChucNang.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewChucNang_RowCellStyle);
            // 
            // permissioncheck2
            // 
            this.permissioncheck2.Caption = "Check";
            this.permissioncheck2.FieldName = "permissioncheck2";
            this.permissioncheck2.Name = "permissioncheck2";
            // 
            // chucnangid
            // 
            this.chucnangid.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.chucnangid.AppearanceHeader.Options.UseForeColor = true;
            this.chucnangid.AppearanceHeader.Options.UseTextOptions = true;
            this.chucnangid.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.chucnangid.Caption = "Mã chức năng";
            this.chucnangid.FieldName = "permissioncode";
            this.chucnangid.Name = "chucnangid";
            this.chucnangid.OptionsColumn.AllowEdit = false;
            this.chucnangid.Visible = true;
            this.chucnangid.VisibleIndex = 1;
            this.chucnangid.Width = 82;
            // 
            // tenchucnang
            // 
            this.tenchucnang.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.tenchucnang.AppearanceHeader.Options.UseForeColor = true;
            this.tenchucnang.AppearanceHeader.Options.UseTextOptions = true;
            this.tenchucnang.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tenchucnang.Caption = "Tên chức năng";
            this.tenchucnang.FieldName = "permissionname";
            this.tenchucnang.Name = "tenchucnang";
            this.tenchucnang.OptionsColumn.AllowEdit = false;
            this.tenchucnang.Visible = true;
            this.tenchucnang.VisibleIndex = 2;
            this.tenchucnang.Width = 268;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(622, 22);
            this.panelControl2.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl2.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            this.labelControl2.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.Horizontal;
            this.labelControl2.Location = new System.Drawing.Point(119, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(127, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Phân quyền chức năng";
            // 
            // timerThongBao
            // 
            this.timerThongBao.Interval = 2000;
            this.timerThongBao.Tick += new System.EventHandler(this.timerThongBao_Tick);
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
            // ucQuanLyNguoiDung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControlPhanQuyen);
            this.Controls.Add(this.panelControlThongTinUS);
            this.Name = "ucQuanLyNguoiDung";
            this.Size = new System.Drawing.Size(1096, 613);
            this.Load += new System.EventHandler(this.ucQuanLyNguoiDung_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlThongTinUS)).EndInit();
            this.panelControlThongTinUS.ResumeLayout(false);
            this.groupBoxChucNang.ResumeLayout(false);
            this.groupBoxThongTin.ResumeLayout(false);
            this.groupBoxThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbbUserNhom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControlPhanQuyen)).EndInit();
            this.panelControlPhanQuyen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDSUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDSUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlChucNang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChucNang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControlThongTinUS;
        private DevExpress.XtraEditors.PanelControl panelControlPhanQuyen;
        private DevExpress.XtraEditors.LabelControl lblUsername;
        private DevExpress.XtraEditors.LabelControl lblPassword;
        private DevExpress.XtraEditors.LabelControl lblUserId;
        private System.Windows.Forms.GroupBox groupBoxChucNang;
        private System.Windows.Forms.GroupBox groupBoxThongTin;
        private DevExpress.XtraEditors.SimpleButton btnUserThem;
        private DevExpress.XtraEditors.TextEdit txtUserID;
        private DevExpress.XtraEditors.ComboBoxEdit cbbUserNhom;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.TextEdit txtUserPassword;
        private DevExpress.XtraEditors.SimpleButton btnUserOK;
        private DevExpress.XtraGrid.GridControl gridControlChucNang;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewChucNang;
        private DevExpress.XtraGrid.Columns.GridColumn chucnangid;
        private DevExpress.XtraGrid.Columns.GridColumn tenchucnang;
        private DevExpress.XtraGrid.GridControl gridControlDSUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDSUser;
        private DevExpress.XtraGrid.Columns.GridColumn usercode;
        private DevExpress.XtraGrid.Columns.GridColumn username;
        private DevExpress.XtraGrid.Columns.GridColumn usergnhom;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Timer timerThongBao;
        private DevExpress.XtraEditors.LabelControl lblThongBao;
        private DevExpress.Utils.ImageCollection imMenu;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn checker;
        private DevExpress.XtraGrid.Columns.GridColumn permissioncheck2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
    }
}
