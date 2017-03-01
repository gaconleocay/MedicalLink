namespace MedicalLink.FormCommon
{
    partial class ucTrangChu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTrangChu));
            this.xtraTabControlHome = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabTTCoBan = new DevExpress.XtraTab.XtraTabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblThongBao = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboGiaoDien = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabelThoiHan = new System.Windows.Forms.Label();
            this.linkLabelTenDatabase = new System.Windows.Forms.Label();
            this.linkLabelTenNguoiDung = new System.Windows.Forms.LinkLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureLogo = new DevExpress.XtraEditors.PictureEdit();
            this.xtraTabCaiDat = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainerControlCaiDat = new DevExpress.XtraEditors.SplitContainerControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemLicense = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemConnectDB = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemListNguoiDung = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemListNhanVien = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemListOption = new DevExpress.XtraNavBar.NavBarItem();
            this.panelCaiDatChiTiet = new DevExpress.XtraEditors.PanelControl();
            this.timerThongBao = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlHome)).BeginInit();
            this.xtraTabControlHome.SuspendLayout();
            this.xtraTabTTCoBan.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGiaoDien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo.Properties)).BeginInit();
            this.xtraTabCaiDat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlCaiDat)).BeginInit();
            this.splitContainerControlCaiDat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCaiDatChiTiet)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControlHome
            // 
            this.xtraTabControlHome.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.xtraTabControlHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlHome.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControlHome.Margin = new System.Windows.Forms.Padding(0);
            this.xtraTabControlHome.Name = "xtraTabControlHome";
            this.xtraTabControlHome.SelectedTabPage = this.xtraTabTTCoBan;
            this.xtraTabControlHome.Size = new System.Drawing.Size(1096, 613);
            this.xtraTabControlHome.TabIndex = 1;
            this.xtraTabControlHome.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabTTCoBan,
            this.xtraTabCaiDat});
            this.xtraTabControlHome.TabPageWidth = 160;
            this.xtraTabControlHome.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlHome_SelectedPageChanged);
            this.xtraTabControlHome.CloseButtonClick += new System.EventHandler(this.xtraTabControlHome_CloseButtonClick);
            // 
            // xtraTabTTCoBan
            // 
            this.xtraTabTTCoBan.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.xtraTabTTCoBan.Appearance.Header.ForeColor = System.Drawing.Color.Navy;
            this.xtraTabTTCoBan.Appearance.Header.Options.UseFont = true;
            this.xtraTabTTCoBan.Appearance.Header.Options.UseForeColor = true;
            this.xtraTabTTCoBan.Appearance.PageClient.BackColor = System.Drawing.Color.DarkRed;
            this.xtraTabTTCoBan.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabTTCoBan.Controls.Add(this.panel3);
            this.xtraTabTTCoBan.Controls.Add(this.panel2);
            this.xtraTabTTCoBan.Image = ((System.Drawing.Image)(resources.GetObject("xtraTabTTCoBan.Image")));
            this.xtraTabTTCoBan.Name = "xtraTabTTCoBan";
            this.xtraTabTTCoBan.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabTTCoBan.Size = new System.Drawing.Size(1090, 582);
            this.xtraTabTTCoBan.Text = "Thông tin về phần mềm";
            this.xtraTabTTCoBan.Tooltip = "Thông tin về phần mềm";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblThongBao);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.cboGiaoDien);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.linkLabelThoiHan);
            this.panel2.Controls.Add(this.linkLabelTenDatabase);
            this.panel2.Controls.Add(this.linkLabelTenNguoiDung);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.lblVersion);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.MaximumSize = new System.Drawing.Size(420, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(420, 582);
            this.panel2.TabIndex = 26;
            // 
            // lblThongBao
            // 
            this.lblThongBao.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblThongBao.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblThongBao.Location = new System.Drawing.Point(148, 288);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(101, 23);
            this.lblThongBao.TabIndex = 46;
            this.lblThongBao.Text = "Thong bao";
            this.lblThongBao.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 482);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 100);
            this.panel1.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(100, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Copiright 2016, Hồng Minh Nhất";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(88, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(219, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "E-mail: hongminhnhat15@gmail.com\r\nPhone: 0868-915-456\r\n\r\n\r\n\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboGiaoDien
            // 
            this.cboGiaoDien.EditValue = "";
            this.cboGiaoDien.Location = new System.Drawing.Point(148, 208);
            this.cboGiaoDien.Name = "cboGiaoDien";
            this.cboGiaoDien.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGiaoDien.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cboGiaoDien.Properties.Appearance.Options.UseFont = true;
            this.cboGiaoDien.Properties.Appearance.Options.UseForeColor = true;
            this.cboGiaoDien.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGiaoDien.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboGiaoDien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGiaoDien.Properties.DropDownRows = 10;
            this.cboGiaoDien.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboGiaoDien.Size = new System.Drawing.Size(255, 26);
            this.cboGiaoDien.TabIndex = 45;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 19);
            this.label5.TabIndex = 44;
            this.label5.Text = "Giao diện:";
            // 
            // linkLabelThoiHan
            // 
            this.linkLabelThoiHan.AutoSize = true;
            this.linkLabelThoiHan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelThoiHan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.linkLabelThoiHan.Location = new System.Drawing.Point(144, 77);
            this.linkLabelThoiHan.Name = "linkLabelThoiHan";
            this.linkLabelThoiHan.Size = new System.Drawing.Size(124, 19);
            this.linkLabelThoiHan.TabIndex = 43;
            this.linkLabelThoiHan.TabStop = true;
            this.linkLabelThoiHan.Text = "Thời hạn license";
            // 
            // linkLabelTenDatabase
            // 
            this.linkLabelTenDatabase.AutoSize = true;
            this.linkLabelTenDatabase.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelTenDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.linkLabelTenDatabase.Location = new System.Drawing.Point(144, 120);
            this.linkLabelTenDatabase.Name = "linkLabelTenDatabase";
            this.linkLabelTenDatabase.Size = new System.Drawing.Size(103, 19);
            this.linkLabelTenDatabase.TabIndex = 42;
            this.linkLabelTenDatabase.TabStop = true;
            this.linkLabelTenDatabase.Text = "Tên database";
            // 
            // linkLabelTenNguoiDung
            // 
            this.linkLabelTenNguoiDung.AutoSize = true;
            this.linkLabelTenNguoiDung.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelTenNguoiDung.LinkColor = System.Drawing.Color.Red;
            this.linkLabelTenNguoiDung.Location = new System.Drawing.Point(144, 168);
            this.linkLabelTenNguoiDung.Name = "linkLabelTenNguoiDung";
            this.linkLabelTenNguoiDung.Size = new System.Drawing.Size(145, 19);
            this.linkLabelTenNguoiDung.TabIndex = 41;
            this.linkLabelTenNguoiDung.TabStop = true;
            this.linkLabelTenNguoiDung.Tag = "Click vào đây để thay đổi mật khẩu";
            this.linkLabelTenNguoiDung.Text = "Tên người sử dụng";
            this.linkLabelTenNguoiDung.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTenNguoiDung_LinkClicked);
            this.linkLabelTenNguoiDung.MouseHover += new System.EventHandler(this.linkLabelTenNguoiDung_MouseHover);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(17, 168);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 19);
            this.label11.TabIndex = 40;
            this.label11.Text = "Người sử dụng:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 19);
            this.label9.TabIndex = 39;
            this.label9.Text = "Database:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 19);
            this.label7.TabIndex = 38;
            this.label7.Text = "Thời hạn:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblVersion.Location = new System.Drawing.Point(144, 33);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(60, 19);
            this.lblVersion.TabIndex = 37;
            this.lblVersion.Text = "1.0.0.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 19);
            this.label6.TabIndex = 36;
            this.label6.Text = "Phiên bản:";
            // 
            // pictureLogo
            // 
            this.pictureLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureLogo.Size = new System.Drawing.Size(670, 582);
            this.pictureLogo.TabIndex = 0;
            // 
            // xtraTabCaiDat
            // 
            this.xtraTabCaiDat.Controls.Add(this.splitContainerControlCaiDat);
            this.xtraTabCaiDat.Name = "xtraTabCaiDat";
            this.xtraTabCaiDat.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabCaiDat.Size = new System.Drawing.Size(1090, 582);
            this.xtraTabCaiDat.Text = "Cài đặt";
            this.xtraTabCaiDat.Tooltip = "Cài đặt";
            // 
            // splitContainerControlCaiDat
            // 
            this.splitContainerControlCaiDat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControlCaiDat.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControlCaiDat.Name = "splitContainerControlCaiDat";
            this.splitContainerControlCaiDat.Panel1.Controls.Add(this.navBarControl1);
            this.splitContainerControlCaiDat.Panel1.Text = "Panel1";
            this.splitContainerControlCaiDat.Panel2.Controls.Add(this.panelCaiDatChiTiet);
            this.splitContainerControlCaiDat.Panel2.Text = "Panel2";
            this.splitContainerControlCaiDat.Size = new System.Drawing.Size(1090, 582);
            this.splitContainerControlCaiDat.SplitterPosition = 206;
            this.splitContainerControlCaiDat.TabIndex = 0;
            this.splitContainerControlCaiDat.Text = "splitContainerControl1";
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItemLicense,
            this.navBarItemConnectDB,
            this.navBarItemListNguoiDung,
            this.navBarItemListNhanVien,
            this.navBarItemListOption});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 206;
            this.navBarControl1.Size = new System.Drawing.Size(206, 582);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarGroup1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.navBarGroup1.Appearance.Options.UseFont = true;
            this.navBarGroup1.Appearance.Options.UseForeColor = true;
            this.navBarGroup1.Caption = "THÔNG TIN CHUNG";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemLicense),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemConnectDB)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarItemLicense
            // 
            this.navBarItemLicense.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemLicense.Appearance.Options.UseFont = true;
            this.navBarItemLicense.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemLicense.AppearanceDisabled.Options.UseFont = true;
            this.navBarItemLicense.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemLicense.AppearanceHotTracked.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.navBarItemLicense.AppearanceHotTracked.Options.UseFont = true;
            this.navBarItemLicense.AppearanceHotTracked.Options.UseForeColor = true;
            this.navBarItemLicense.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemLicense.AppearancePressed.Options.UseFont = true;
            this.navBarItemLicense.Caption = "Đăng ký bản quyền";
            this.navBarItemLicense.Name = "navBarItemLicense";
            this.navBarItemLicense.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemLicense_LinkClicked);
            // 
            // navBarItemConnectDB
            // 
            this.navBarItemConnectDB.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemConnectDB.Appearance.Options.UseFont = true;
            this.navBarItemConnectDB.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemConnectDB.AppearanceDisabled.Options.UseFont = true;
            this.navBarItemConnectDB.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemConnectDB.AppearanceHotTracked.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.navBarItemConnectDB.AppearanceHotTracked.Options.UseFont = true;
            this.navBarItemConnectDB.AppearanceHotTracked.Options.UseForeColor = true;
            this.navBarItemConnectDB.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemConnectDB.AppearancePressed.Options.UseFont = true;
            this.navBarItemConnectDB.Caption = "Kết nối cơ sở dữ liệu";
            this.navBarItemConnectDB.Name = "navBarItemConnectDB";
            this.navBarItemConnectDB.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemConnectDB_LinkClicked);
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarGroup2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.navBarGroup2.Appearance.Options.UseFont = true;
            this.navBarGroup2.Appearance.Options.UseForeColor = true;
            this.navBarGroup2.Caption = "CÀI ĐẶT";
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemListNguoiDung),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemListNhanVien),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemListOption)});
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // navBarItemListNguoiDung
            // 
            this.navBarItemListNguoiDung.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListNguoiDung.Appearance.Options.UseFont = true;
            this.navBarItemListNguoiDung.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListNguoiDung.AppearanceDisabled.Options.UseFont = true;
            this.navBarItemListNguoiDung.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListNguoiDung.AppearanceHotTracked.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.navBarItemListNguoiDung.AppearanceHotTracked.Options.UseFont = true;
            this.navBarItemListNguoiDung.AppearanceHotTracked.Options.UseForeColor = true;
            this.navBarItemListNguoiDung.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListNguoiDung.AppearancePressed.Options.UseFont = true;
            this.navBarItemListNguoiDung.Caption = "Danh sách người dùng";
            this.navBarItemListNguoiDung.Name = "navBarItemListNguoiDung";
            this.navBarItemListNguoiDung.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemListNguoiDung_LinkClicked);
            // 
            // navBarItemListNhanVien
            // 
            this.navBarItemListNhanVien.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListNhanVien.Appearance.Options.UseFont = true;
            this.navBarItemListNhanVien.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListNhanVien.AppearanceDisabled.Options.UseFont = true;
            this.navBarItemListNhanVien.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListNhanVien.AppearanceHotTracked.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.navBarItemListNhanVien.AppearanceHotTracked.Options.UseFont = true;
            this.navBarItemListNhanVien.AppearanceHotTracked.Options.UseForeColor = true;
            this.navBarItemListNhanVien.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListNhanVien.AppearancePressed.Options.UseFont = true;
            this.navBarItemListNhanVien.Caption = "DS người YC mở BA";
            this.navBarItemListNhanVien.Name = "navBarItemListNhanVien";
            this.navBarItemListNhanVien.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemListNhanVien_LinkClicked);
            // 
            // navBarItemListOption
            // 
            this.navBarItemListOption.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListOption.Appearance.Options.UseFont = true;
            this.navBarItemListOption.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListOption.AppearanceDisabled.Options.UseFont = true;
            this.navBarItemListOption.AppearanceHotTracked.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListOption.AppearanceHotTracked.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.navBarItemListOption.AppearanceHotTracked.Options.UseFont = true;
            this.navBarItemListOption.AppearanceHotTracked.Options.UseForeColor = true;
            this.navBarItemListOption.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarItemListOption.AppearancePressed.Options.UseFont = true;
            this.navBarItemListOption.Caption = "Danh sách tùy chọn";
            this.navBarItemListOption.Name = "navBarItemListOption";
            this.navBarItemListOption.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemListOption_LinkClicked);
            // 
            // panelCaiDatChiTiet
            // 
            this.panelCaiDatChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCaiDatChiTiet.Location = new System.Drawing.Point(0, 0);
            this.panelCaiDatChiTiet.Name = "panelCaiDatChiTiet";
            this.panelCaiDatChiTiet.Size = new System.Drawing.Size(879, 582);
            this.panelCaiDatChiTiet.TabIndex = 0;
            // 
            // timerThongBao
            // 
            this.timerThongBao.Interval = 2000;
            this.timerThongBao.Tick += new System.EventHandler(this.timerThongBao_Tick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureLogo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(420, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(670, 582);
            this.panel3.TabIndex = 27;
            // 
            // ucTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControlHome);
            this.Name = "ucTrangChu";
            this.Size = new System.Drawing.Size(1096, 613);
            this.Load += new System.EventHandler(this.ucTrangChu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlHome)).EndInit();
            this.xtraTabControlHome.ResumeLayout(false);
            this.xtraTabTTCoBan.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboGiaoDien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo.Properties)).EndInit();
            this.xtraTabCaiDat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControlCaiDat)).EndInit();
            this.splitContainerControlCaiDat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelCaiDatChiTiet)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControlHome;
        private DevExpress.XtraTab.XtraTabPage xtraTabTTCoBan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timerThongBao;
        private DevExpress.XtraTab.XtraTabPage xtraTabCaiDat;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControlCaiDat;
        private DevExpress.XtraEditors.PanelControl panelCaiDatChiTiet;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem navBarItemLicense;
        private DevExpress.XtraNavBar.NavBarItem navBarItemConnectDB;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarItem navBarItemListNguoiDung;
        private DevExpress.XtraNavBar.NavBarItem navBarItemListNhanVien;
        private DevExpress.XtraNavBar.NavBarItem navBarItemListOption;
        private DevExpress.XtraEditors.PictureEdit pictureLogo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.LabelControl lblThongBao;
        private DevExpress.XtraEditors.ComboBoxEdit cboGiaoDien;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label linkLabelThoiHan;
        private System.Windows.Forms.Label linkLabelTenDatabase;
        private System.Windows.Forms.LinkLabel linkLabelTenNguoiDung;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
    }
}
