﻿namespace MedicalLink.FormCommon
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
            this.btnResert = new DevExpress.XtraEditors.SimpleButton();
            this.tabMenuTrangChu = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabMenuDashboard = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabMenuQLTaiChinh = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabMenuBaoCao = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabMenuChucNang = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.tabMenuQLDuoc = new DevExpress.XtraBars.Navigation.TabNavigationPage();
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
            this.barStaticItem1.ItemAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barStaticItem1.ItemAppearance.Disabled.Options.UseFont = true;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lblStatusTenBC
            // 
            this.lblStatusTenBC.Caption = "Home";
            this.lblStatusTenBC.Id = 1;
            this.lblStatusTenBC.ItemAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusTenBC.ItemAppearance.Disabled.Options.UseFont = true;
            this.lblStatusTenBC.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusTenBC.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Red;
            this.lblStatusTenBC.ItemAppearance.Normal.Options.UseFont = true;
            this.lblStatusTenBC.ItemAppearance.Normal.Options.UseForeColor = true;
            this.lblStatusTenBC.Name = "lblStatusTenBC";
            this.lblStatusTenBC.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // StatusDBName
            // 
            this.StatusDBName.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.StatusDBName.Caption = "StatusDBName";
            this.StatusDBName.Id = 4;
            this.StatusDBName.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusDBName.ItemAppearance.Normal.Options.UseFont = true;
            this.StatusDBName.Name = "StatusDBName";
            this.StatusDBName.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // StatusUsername
            // 
            this.StatusUsername.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.StatusUsername.Caption = "StatusUsername";
            this.StatusUsername.Id = 3;
            this.StatusUsername.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusUsername.ItemAppearance.Normal.Options.UseFont = true;
            this.StatusUsername.Name = "StatusUsername";
            this.StatusUsername.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // StatusClock
            // 
            this.StatusClock.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.StatusClock.Caption = "StatusClock";
            this.StatusClock.Id = 2;
            this.StatusClock.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusClock.ItemAppearance.Normal.Options.UseFont = true;
            this.StatusClock.Name = "StatusClock";
            this.StatusClock.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1084, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 636);
            this.barDockControlBottom.Size = new System.Drawing.Size(1084, 26);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 636);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1084, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 636);
            // 
            // tabPaneMenu
            // 
            this.tabPaneMenu.Controls.Add(this.btnResert);
            this.tabPaneMenu.Controls.Add(this.tabMenuTrangChu);
            this.tabPaneMenu.Controls.Add(this.tabMenuDashboard);
            this.tabPaneMenu.Controls.Add(this.tabMenuQLTaiChinh);
            this.tabPaneMenu.Controls.Add(this.tabMenuBaoCao);
            this.tabPaneMenu.Controls.Add(this.tabMenuChucNang);
            this.tabPaneMenu.Controls.Add(this.tabMenuQLDuoc);
            this.tabPaneMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPaneMenu.Location = new System.Drawing.Point(0, 0);
            this.tabPaneMenu.Margin = new System.Windows.Forms.Padding(0);
            this.tabPaneMenu.Name = "tabPaneMenu";
            this.tabPaneMenu.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabMenuQLDuoc,
            this.tabMenuDashboard,
            this.tabMenuQLTaiChinh,
            this.tabMenuBaoCao,
            this.tabMenuChucNang,
            this.tabMenuTrangChu});
            this.tabPaneMenu.RegularSize = new System.Drawing.Size(1084, 636);
            this.tabPaneMenu.SelectedPage = this.tabMenuTrangChu;
            this.tabPaneMenu.Size = new System.Drawing.Size(1084, 636);
            this.tabPaneMenu.TabIndex = 5;
            this.tabPaneMenu.Text = "MENU";
            // 
            // btnResert
            // 
            this.btnResert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResert.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnResert.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.btnResert.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnResert.Appearance.Options.UseBackColor = true;
            this.btnResert.Appearance.Options.UseFont = true;
            this.btnResert.Appearance.Options.UseForeColor = true;
            this.btnResert.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnResert.Image = global::MedicalLink.Properties.Resources.recurring_appointment_16;
            this.btnResert.Location = new System.Drawing.Point(961, 3);
            this.btnResert.Name = "btnResert";
            this.btnResert.Size = new System.Drawing.Size(110, 26);
            this.btnResert.TabIndex = 11;
            this.btnResert.Text = "Khởi động lại";
            this.btnResert.Click += new System.EventHandler(this.btnResert_Click);
            // 
            // tabMenuTrangChu
            // 
            this.tabMenuTrangChu.Caption = "TRANG CHỦ";
            this.tabMenuTrangChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabMenuTrangChu.Image = ((System.Drawing.Image)(resources.GetObject("tabMenuTrangChu.Image")));
            this.tabMenuTrangChu.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuTrangChu.Name = "tabMenuTrangChu";
            this.tabMenuTrangChu.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuTrangChu.Size = new System.Drawing.Size(1066, 588);
            // 
            // tabMenuDashboard
            // 
            this.tabMenuDashboard.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.tabMenuDashboard.Appearance.Options.UseBackColor = true;
            this.tabMenuDashboard.Caption = "DASHBOARD";
            this.tabMenuDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMenuDashboard.Image = ((System.Drawing.Image)(resources.GetObject("tabMenuDashboard.Image")));
            this.tabMenuDashboard.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuDashboard.Margin = new System.Windows.Forms.Padding(0);
            this.tabMenuDashboard.Name = "tabMenuDashboard";
            this.tabMenuDashboard.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuDashboard.Size = new System.Drawing.Size(1066, 588);
            // 
            // tabMenuQLTaiChinh
            // 
            this.tabMenuQLTaiChinh.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.tabMenuQLTaiChinh.Appearance.Options.UseBackColor = true;
            this.tabMenuQLTaiChinh.Caption = "QL TÀI CHÍNH";
            this.tabMenuQLTaiChinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabMenuQLTaiChinh.Image = ((System.Drawing.Image)(resources.GetObject("tabMenuQLTaiChinh.Image")));
            this.tabMenuQLTaiChinh.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuQLTaiChinh.Margin = new System.Windows.Forms.Padding(0);
            this.tabMenuQLTaiChinh.Name = "tabMenuQLTaiChinh";
            this.tabMenuQLTaiChinh.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuQLTaiChinh.Size = new System.Drawing.Size(1066, 588);
            // 
            // tabMenuBaoCao
            // 
            this.tabMenuBaoCao.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.tabMenuBaoCao.Appearance.Options.UseBackColor = true;
            this.tabMenuBaoCao.Caption = "BÁO CÁO";
            this.tabMenuBaoCao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabMenuBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("tabMenuBaoCao.Image")));
            this.tabMenuBaoCao.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuBaoCao.Margin = new System.Windows.Forms.Padding(0);
            this.tabMenuBaoCao.Name = "tabMenuBaoCao";
            this.tabMenuBaoCao.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuBaoCao.Size = new System.Drawing.Size(1066, 588);
            // 
            // tabMenuChucNang
            // 
            this.tabMenuChucNang.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.tabMenuChucNang.Appearance.Options.UseBackColor = true;
            this.tabMenuChucNang.Caption = "CHỨC NĂNG";
            this.tabMenuChucNang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabMenuChucNang.Image = ((System.Drawing.Image)(resources.GetObject("tabMenuChucNang.Image")));
            this.tabMenuChucNang.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuChucNang.Margin = new System.Windows.Forms.Padding(0);
            this.tabMenuChucNang.Name = "tabMenuChucNang";
            this.tabMenuChucNang.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuChucNang.Size = new System.Drawing.Size(1066, 588);
            // 
            // tabMenuQLDuoc
            // 
            this.tabMenuQLDuoc.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.tabMenuQLDuoc.Appearance.Options.UseBackColor = true;
            this.tabMenuQLDuoc.Caption = "QL DƯỢC";
            this.tabMenuQLDuoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabMenuQLDuoc.Image = ((System.Drawing.Image)(resources.GetObject("tabMenuQLDuoc.Image")));
            this.tabMenuQLDuoc.ItemShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuQLDuoc.Margin = new System.Windows.Forms.Padding(0);
            this.tabMenuQLDuoc.Name = "tabMenuQLDuoc";
            this.tabMenuQLDuoc.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.ImageAndText;
            this.tabMenuQLDuoc.Size = new System.Drawing.Size(1066, 588);
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
            this.ClientSize = new System.Drawing.Size(1084, 662);
            this.Controls.Add(this.tabPaneMenu);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Công cụ sửa trong DB Alibobo HIS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
        private System.Windows.Forms.Timer timerTblBNDangDT;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabMenuQLTaiChinh;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabMenuBaoCao;
        private DevExpress.XtraEditors.SimpleButton btnResert;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabMenuQLDuoc;
    }
}