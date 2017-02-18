namespace MedicalLink.FormCommon
{
    partial class ucDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDashboard));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarBCTongHopToanVien = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarBCBenhNhanNgoaiTru = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem6 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup4 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarBCQLTongTheKhoa = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarBCBenhNhanNoiTru = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup5 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem9 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem10 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarBCXNTTuTruc = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem4 = new DevExpress.XtraNavBar.NavBarItem();
            this.xtraTabControlChucNang = new DevExpress.XtraTab.XtraTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlChucNang)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.navBarControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtraTabControlChucNang);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1170, 613);
            this.splitContainerControl1.SplitterPosition = 180;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup3,
            this.navBarGroup4,
            this.navBarGroup5,
            this.navBarGroup2});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarBCTongHopToanVien,
            this.navBarItem2,
            this.navBarBCXNTTuTruc,
            this.navBarItem4,
            this.navBarBCBenhNhanNgoaiTru,
            this.navBarItem6,
            this.navBarBCQLTongTheKhoa,
            this.navBarBCBenhNhanNoiTru,
            this.navBarItem9,
            this.navBarItem10});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 180;
            this.navBarControl1.Size = new System.Drawing.Size(180, 613);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarGroup1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.navBarGroup1.Appearance.Options.UseFont = true;
            this.navBarGroup1.Appearance.Options.UseForeColor = true;
            this.navBarGroup1.Caption = "TỔNG HỢP";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarBCTongHopToanVien),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem2)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarBCTongHopToanVien
            // 
            this.navBarBCTongHopToanVien.Caption = "Tổng hợp toàn viện";
            this.navBarBCTongHopToanVien.Name = "navBarBCTongHopToanVien";
            this.navBarBCTongHopToanVien.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarBCTongHopToanVien.SmallImage")));
            this.navBarBCTongHopToanVien.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarBCTongHopToanVien_LinkClicked);
            // 
            // navBarItem2
            // 
            this.navBarItem2.Caption = "Tạm ứng";
            this.navBarItem2.Name = "navBarItem2";
            this.navBarItem2.SmallImage = global::MedicalLink.Properties.Resources.arrow_32_16;
            this.navBarItem2.Visible = false;
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarGroup3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.navBarGroup3.Appearance.Options.UseFont = true;
            this.navBarGroup3.Appearance.Options.UseForeColor = true;
            this.navBarGroup3.Caption = "NGOẠI TRÚ";
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarBCBenhNhanNgoaiTru),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem6)});
            this.navBarGroup3.Name = "navBarGroup3";
            // 
            // navBarBCBenhNhanNgoaiTru
            // 
            this.navBarBCBenhNhanNgoaiTru.Caption = "BC bệnh nhân ngoại trú";
            this.navBarBCBenhNhanNgoaiTru.Name = "navBarBCBenhNhanNgoaiTru";
            this.navBarBCBenhNhanNgoaiTru.SmallImage = global::MedicalLink.Properties.Resources.arrow_32_16;
            this.navBarBCBenhNhanNgoaiTru.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarBCBenhNhanNgoaiTru_LinkClicked);
            // 
            // navBarItem6
            // 
            this.navBarItem6.Caption = "navBarItem6";
            this.navBarItem6.Name = "navBarItem6";
            this.navBarItem6.SmallImage = global::MedicalLink.Properties.Resources.arrow_32_16;
            this.navBarItem6.Visible = false;
            // 
            // navBarGroup4
            // 
            this.navBarGroup4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarGroup4.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.navBarGroup4.Appearance.Options.UseFont = true;
            this.navBarGroup4.Appearance.Options.UseForeColor = true;
            this.navBarGroup4.Caption = "NỘI TRÚ";
            this.navBarGroup4.Expanded = true;
            this.navBarGroup4.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarBCQLTongTheKhoa),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarBCBenhNhanNoiTru)});
            this.navBarGroup4.Name = "navBarGroup4";
            // 
            // navBarBCQLTongTheKhoa
            // 
            this.navBarBCQLTongTheKhoa.Caption = "BC QL tổng thể khoa";
            this.navBarBCQLTongTheKhoa.Name = "navBarBCQLTongTheKhoa";
            this.navBarBCQLTongTheKhoa.SmallImage = global::MedicalLink.Properties.Resources.arrow_32_16;
            this.navBarBCQLTongTheKhoa.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarBCQLTongTheKhoa_LinkClicked);
            // 
            // navBarBCBenhNhanNoiTru
            // 
            this.navBarBCBenhNhanNoiTru.Caption = "BC bệnh nhân nội trú";
            this.navBarBCBenhNhanNoiTru.Name = "navBarBCBenhNhanNoiTru";
            this.navBarBCBenhNhanNoiTru.SmallImage = global::MedicalLink.Properties.Resources.arrow_32_16;
            this.navBarBCBenhNhanNoiTru.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarBCBenhNhanNoiTru_LinkClicked);
            // 
            // navBarGroup5
            // 
            this.navBarGroup5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarGroup5.Appearance.ForeColor = System.Drawing.Color.Red;
            this.navBarGroup5.Appearance.Options.UseFont = true;
            this.navBarGroup5.Appearance.Options.UseForeColor = true;
            this.navBarGroup5.Caption = "CẬN LÂM SÀNG";
            this.navBarGroup5.Expanded = true;
            this.navBarGroup5.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem9),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem10)});
            this.navBarGroup5.Name = "navBarGroup5";
            // 
            // navBarItem9
            // 
            this.navBarItem9.Caption = "navBarItem9";
            this.navBarItem9.Name = "navBarItem9";
            this.navBarItem9.SmallImage = global::MedicalLink.Properties.Resources.arrow_32_16;
            this.navBarItem9.Visible = false;
            // 
            // navBarItem10
            // 
            this.navBarItem10.Caption = "navBarItem10";
            this.navBarItem10.Name = "navBarItem10";
            this.navBarItem10.SmallImage = global::MedicalLink.Properties.Resources.arrow_32_16;
            this.navBarItem10.Visible = false;
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarGroup2.Appearance.ForeColor = System.Drawing.Color.Fuchsia;
            this.navBarGroup2.Appearance.Options.UseFont = true;
            this.navBarGroup2.Appearance.Options.UseForeColor = true;
            this.navBarGroup2.Caption = "DƯỢC";
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarBCXNTTuTruc),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem4)});
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // navBarBCXNTTuTruc
            // 
            this.navBarBCXNTTuTruc.Caption = "BC xuất nhập tồn tủ trực";
            this.navBarBCXNTTuTruc.Name = "navBarBCXNTTuTruc";
            this.navBarBCXNTTuTruc.SmallImage = global::MedicalLink.Properties.Resources.arrow_32_16;
            this.navBarBCXNTTuTruc.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarBCXNTTuTruc_LinkClicked);
            // 
            // navBarItem4
            // 
            this.navBarItem4.Caption = "navBarItem4";
            this.navBarItem4.Name = "navBarItem4";
            this.navBarItem4.SmallImage = global::MedicalLink.Properties.Resources.arrow_32_16;
            this.navBarItem4.Visible = false;
            // 
            // xtraTabControlChucNang
            // 
            this.xtraTabControlChucNang.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.xtraTabControlChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControlChucNang.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControlChucNang.Margin = new System.Windows.Forms.Padding(0);
            this.xtraTabControlChucNang.Name = "xtraTabControlChucNang";
            this.xtraTabControlChucNang.Size = new System.Drawing.Size(985, 613);
            this.xtraTabControlChucNang.TabIndex = 1;
            this.xtraTabControlChucNang.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlDashboard_SelectedPageChanged);
            this.xtraTabControlChucNang.CloseButtonClick += new System.EventHandler(this.xtraTabControlDashboard_CloseButtonClick);
            // 
            // ucDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucDashboard";
            this.Size = new System.Drawing.Size(1170, 613);
            this.Load += new System.EventHandler(this.ucDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlChucNang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControlChucNang;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem navBarBCTongHopToanVien;
        private DevExpress.XtraNavBar.NavBarItem navBarItem2;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarItem navBarBCXNTTuTruc;
        private DevExpress.XtraNavBar.NavBarItem navBarItem4;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarItem navBarBCBenhNhanNgoaiTru;
        private DevExpress.XtraNavBar.NavBarItem navBarItem6;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup4;
        private DevExpress.XtraNavBar.NavBarItem navBarBCQLTongTheKhoa;
        private DevExpress.XtraNavBar.NavBarItem navBarBCBenhNhanNoiTru;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup5;
        private DevExpress.XtraNavBar.NavBarItem navBarItem9;
        private DevExpress.XtraNavBar.NavBarItem navBarItem10;
    }
}
