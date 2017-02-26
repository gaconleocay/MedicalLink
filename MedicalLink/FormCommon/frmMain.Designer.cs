namespace MedicalLink.FormCommon
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.lblStatusTenBC = new DevExpress.XtraBars.BarStaticItem();
            this.StatusDBName = new DevExpress.XtraBars.BarStaticItem();
            this.StatusUsername = new DevExpress.XtraBars.BarStaticItem();
            this.StatusClock = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.tabPaneMenu = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabMenuTrangChu = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabMenuDashboard = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabMenuChucNang = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabMenuRestart = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.timerKiemTraLicense = new System.Windows.Forms.Timer(this.components);
            this.timerTblBNDangDT = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPaneMenu)).BeginInit();
            this.tabPaneMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1,
            this.lblStatusTenBC,
            this.StatusClock,
            this.StatusUsername,
            this.StatusDBName});
            this.barManager1.MaxItemId = 5;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.lblStatusTenBC),
            new DevExpress.XtraBars.LinkPersistInfo(this.StatusDBName),
            new DevExpress.XtraBars.LinkPersistInfo(this.StatusUsername),
            new DevExpress.XtraBars.LinkPersistInfo(this.StatusClock)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Chức năng:";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lblStatusTenBC
            // 
            this.lblStatusTenBC.Caption = "Home";
            this.lblStatusTenBC.Id = 1;
            this.lblStatusTenBC.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Red;
            this.lblStatusTenBC.ItemAppearance.Normal.Options.UseForeColor = true;
            this.lblStatusTenBC.Name = "lblStatusTenBC";
            this.lblStatusTenBC.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // StatusDBName
            // 
            this.StatusDBName.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.StatusDBName.Caption = "StatusDBName";
            this.StatusDBName.Id = 4;
            this.StatusDBName.Name = "StatusDBName";
            this.StatusDBName.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // StatusUsername
            // 
            this.StatusUsername.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.StatusUsername.Caption = "StatusUsername";
            this.StatusUsername.Id = 3;
            this.StatusUsername.Name = "StatusUsername";
            this.StatusUsername.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // StatusClock
            // 
            this.StatusClock.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.StatusClock.Caption = "StatusClock";
            this.StatusClock.Id = 2;
            this.StatusClock.Name = "StatusClock";
            this.StatusClock.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1184, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 617);
            this.barDockControlBottom.Size = new System.Drawing.Size(1184, 25);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 617);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1184, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 617);
            // 
            // tabPaneMenu
            // 
            this.tabPaneMenu.Controls.Add(this.tabMenuTrangChu);
            this.tabPaneMenu.Controls.Add(this.tabMenuDashboard);
            this.tabPaneMenu.Controls.Add(this.tabMenuChucNang);
            this.tabPaneMenu.Controls.Add(this.tabMenuRestart);
            this.tabPaneMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPaneMenu.Location = new System.Drawing.Point(0, 0);
            this.tabPaneMenu.Margin = new System.Windows.Forms.Padding(0);
            this.tabPaneMenu.Name = "tabPaneMenu";
            this.tabPaneMenu.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabMenuRestart,
            this.tabMenuDashboard,
            this.tabMenuChucNang,
            this.tabMenuTrangChu});
            this.tabPaneMenu.RegularSize = new System.Drawing.Size(1184, 617);
            this.tabPaneMenu.SelectedPage = this.tabMenuTrangChu;
            this.tabPaneMenu.Size = new System.Drawing.Size(1184, 617);
            this.tabPaneMenu.TabIndex = 5;
            this.tabPaneMenu.Text = "MENU";
            this.tabPaneMenu.SelectedPageChanged += new DevExpress.XtraBars.Navigation.SelectedPageChangedEventHandler(this.tabPaneMenu_SelectedPageChanged);
            // 
            // tabMenuTrangChu
            // 
            this.tabMenuTrangChu.Caption = "TRANG CHỦ";
            this.tabMenuTrangChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabMenuTrangChu.Image = ((System.Drawing.Image)(resources.GetObject("tabMenuTrangChu.Image")));
            this.tabMenuTrangChu.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuTrangChu.Name = "tabMenuTrangChu";
            this.tabMenuTrangChu.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuTrangChu.Size = new System.Drawing.Size(1166, 569);
            // 
            // tabMenuDashboard
            // 
            this.tabMenuDashboard.Caption = "DASHBOARD";
            this.tabMenuDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMenuDashboard.Image = ((System.Drawing.Image)(resources.GetObject("tabMenuDashboard.Image")));
            this.tabMenuDashboard.ImageUri.Uri = "AlignJustify";
            this.tabMenuDashboard.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuDashboard.Margin = new System.Windows.Forms.Padding(0);
            this.tabMenuDashboard.Name = "tabMenuDashboard";
            this.tabMenuDashboard.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuDashboard.Size = new System.Drawing.Size(1166, 569);
            // 
            // tabMenuChucNang
            // 
            this.tabMenuChucNang.Caption = "CHỨC NĂNG";
            this.tabMenuChucNang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabMenuChucNang.Image = ((System.Drawing.Image)(resources.GetObject("tabMenuChucNang.Image")));
            this.tabMenuChucNang.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuChucNang.Margin = new System.Windows.Forms.Padding(0);
            this.tabMenuChucNang.Name = "tabMenuChucNang";
            this.tabMenuChucNang.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuChucNang.Size = new System.Drawing.Size(1116, 569);
            // 
            // tabMenuRestart
            // 
            this.tabMenuRestart.Caption = "tabMenuRestart";
            this.tabMenuRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabMenuRestart.Image = ((System.Drawing.Image)(resources.GetObject("tabMenuRestart.Image")));
            this.tabMenuRestart.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.Image;
            this.tabMenuRestart.Name = "tabMenuRestart";
            this.tabMenuRestart.PageText = "Khởi động lại";
            this.tabMenuRestart.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.Image;
            this.tabMenuRestart.Size = new System.Drawing.Size(1116, 569);
            this.tabMenuRestart.Tag = "Khởi động lại";
            // 
            // timerClock
            // 
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // timerKiemTraLicense
            // 
            this.timerKiemTraLicense.Interval = 360000;
            this.timerKiemTraLicense.Tick += new System.EventHandler(this.timerKiemTraLicense_Tick);
            // 
            // timerTblBNDangDT
            // 
            this.timerTblBNDangDT.Tick += new System.EventHandler(this.timerTblBCNoiTru_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 642);
            this.Controls.Add(this.tabPaneMenu);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Công cụ sửa trong DB Alibobo HIS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPaneMenu)).EndInit();
            this.tabPaneMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarStaticItem StatusDBName;
        private DevExpress.XtraBars.BarStaticItem StatusUsername;
        private DevExpress.XtraBars.BarStaticItem StatusClock;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Navigation.TabPane tabPaneMenu;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabMenuChucNang;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabMenuDashboard;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabMenuTrangChu;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Timer timerKiemTraLicense;
        internal DevExpress.XtraBars.BarStaticItem lblStatusTenBC;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabMenuRestart;
        private System.Windows.Forms.Timer timerTblBNDangDT;
    }
}