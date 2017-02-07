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
                    //frmMain.StatusTenBC.Caption = e.Page.Tooltip;
                    frmMain.HienThiTenChucNang();
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
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, "REPORT_08", "Dashboard BC quản lý tổng thể khoa", ucControlActive);
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
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, "REPORT_09", "Dashboard BC bệnh nhân nội trú", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


    }
}
