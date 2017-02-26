using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalLink.Dashboard;
using DevExpress.XtraTab;

namespace MedicalLink.FormCommon
{
    public partial class ucDashboard : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        public string CurrentTabPage { get; set; }
        public int SelectedTabPageIndex { get; set; }
        internal frmMain frmMain;

        // khai báo 1 hàm delegate
        public delegate void GetString(string thoigian);
        // khai báo 1 kiểu hàm delegate
        public GetString MyGetData;

        #endregion

        public ucDashboard()
        {
            InitializeComponent();
           // splitContainerControl1.SplitterPosition = 180;
        }

        private void ucDashboard_Load(object sender, EventArgs e)
        {
            try
            {
               // splitContainerControl1.SplitterPosition = 180;
                EnabledAndDisableControl();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void EnabledAndDisableControl()
        {
            try
            {
                navBarBCQLTongTheKhoa.Visible = MedicalLink.Base.CheckPermission.ChkPerModule("REPORT_08");
                navBarBCBenhNhanNoiTru.Visible = MedicalLink.Base.CheckPermission.ChkPerModule("REPORT_09");
                navBarBCBenhNhanNgoaiTru.Visible = MedicalLink.Base.CheckPermission.ChkPerModule("REPORT_10");
                navBarBCTongHopToanVien.Visible = MedicalLink.Base.CheckPermission.ChkPerModule("REPORT_11");
                navBarBCDTCLS.Visible = MedicalLink.Base.CheckPermission.ChkPerModule("REPORT_12");
                navBarDBDTTungKhoa.Visible = MedicalLink.Base.CheckPermission.ChkPerModule("DASHBOARD_01");
                navBarDBBenhNhanNoiTru.Visible = MedicalLink.Base.CheckPermission.ChkPerModule("DASHBOARD_02");


            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #region Tabcontrol function
        //Dong tab
        private void xtraTabControlDashboard_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                XtraTabControl xtab = (XtraTabControl)sender;
                int i = xtab.SelectedTabPageIndex;
                DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs arg = e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs;
                xtab.TabPages.Remove((arg.Page as XtraTabPage));
                xtab.SelectedTabPageIndex = i - 1;
                //(arg.Page as XtraTabPage).PageVisible = false;
                System.GC.Collect();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void xtraTabControlDashboard_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                frmMain = new frmMain();
                this.CurrentTabPage = e.Page.Name;
                XtraTabControl xtab = new XtraTabControl();
                xtab = (XtraTabControl)sender;
                if (xtab != null)
                {
                    this.SelectedTabPageIndex = xtab.SelectedTabPageIndex;
                    //delegate - thong tin chuc nang
                    if (MyGetData != null)
                    {// tại đây gọi nó
                        MyGetData(xtab.TabPages[xtab.SelectedTabPageIndex].Tooltip);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion
        private void navBarBCQLTongTheKhoa_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                UserControl ucControlActive = new UserControl();
                ucControlActive = TabControlProcess.SelectUCControlActive("REPORT_08");
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, "REPORT_08", "BC quản lý tổng thể khoa", "BC quản lý tổng thể khoa. Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void navBarBCBenhNhanNoiTru_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                UserControl ucControlActive = new UserControl();
                ucControlActive = TabControlProcess.SelectUCControlActive("REPORT_09");
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, "REPORT_09", "BC bệnh nhân nội trú", "BC bệnh nhân nội trú. Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void navBarBCBenhNhanNgoaiTru_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                UserControl ucControlActive = new UserControl();
                ucControlActive = TabControlProcess.SelectUCControlActive("REPORT_10");
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, "REPORT_10", "BC bệnh nhân ngoại trú", "BC bệnh nhân ngoại trú. Lấy theo tiêu chí thời gian bệnh nhân đến khám; doanh thu chia theo khoa/phòng chỉ định", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void navBarBCTongHopToanVien_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                UserControl ucControlActive = new UserControl();
                ucControlActive = TabControlProcess.SelectUCControlActive("REPORT_11");
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, "REPORT_11", "BC tổng hợp toàn viện", "BC tổng hợp toàn viện. Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void navBarBCXNTTuTruc_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                UserControl ucControlActive = new UserControl();
                ucControlActive = TabControlProcess.SelectUCControlActive("REPORT_14");
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, "REPORT_14", "BC xuất nhập tồn tủ trực", "Dashboard BC xuất nhập tồn tủ trực", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void navBarBCDTCLS_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                UserControl ucControlActive = new UserControl();
                ucControlActive = TabControlProcess.SelectUCControlActive("REPORT_12");
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, "REPORT_12", "BC doanh thu cận lâm sàng", "BC doanh thu cận lâm sàng. Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng chỉ định", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void navBarDBDTTungKhoa_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                UserControl ucControlActive = new UserControl();
                ucControlActive = TabControlProcess.SelectUCControlActive("DASHBOARD_01");
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, "DASHBOARD_01", "Biểu đồ doanh thu khoa", "Biểu đồ doanh thu khoa", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void navBarDBBenhNhanNoiTru_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                UserControl ucControlActive = new UserControl();
                ucControlActive = TabControlProcess.SelectUCControlActive("DASHBOARD_02");
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, "DASHBOARD_02", "Biểu đồ doanh thu các khoa nội trú", "Biểu đồ doanh thu các khoa nội trú", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


    }
}
