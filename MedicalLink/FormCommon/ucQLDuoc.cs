using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.Base;
using MedicalLink.QLDuoc;

namespace MedicalLink.FormCommon
{
    public partial class ucQLDuoc : UserControl
    {
        #region Declaration
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        internal frmMain frmMain;

        // khai báo 1 hàm delegate
        internal delegate void GetString(string thoigian);
        // khai báo 1 kiểu hàm delegate
        internal GetString MyGetData;

        #endregion

        public ucQLDuoc()
        {
            InitializeComponent();
        }

        #region Load
        private void ucQLDuoc_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataDSChucNang();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadDataDSChucNang()
        {
            try
            {
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Tabcontrol function
        private void xtraTabControlChucNang_CloseButtonClick(object sender, EventArgs e)
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
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void xtraTabControlChucNang_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                frmMain = new frmMain();
                //this.CurrentTabPage = e.Page.Name;
                XtraTabControl xtab = new XtraTabControl();
                xtab = (XtraTabControl)sender;
                if (xtab != null)
                {
                    //this.SelectedTabPageIndex = xtab.SelectedTabPageIndex;
                    //delegate - thong tin chuc nang
                    // tại đây gọi nó
                    MyGetData(xtab.TabPages[xtab.SelectedTabPageIndex].Text);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        #endregion

        #region Tab DANH MUC Events
        private void navBarDM_Kho_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                panelDM_NoiDung.Controls.Clear();
                DM_KhoThuocVT _frm = new DM_KhoThuocVT();
                _frm.Dock = System.Windows.Forms.DockStyle.Fill;
                panelDM_NoiDung.Controls.Add(_frm);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void navBarDM_Thuoc_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                panelDM_NoiDung.Controls.Clear();
                DM_Thuoc _frm = new DM_Thuoc();
                _frm.Dock = System.Windows.Forms.DockStyle.Fill;
                panelDM_NoiDung.Controls.Add(_frm);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void navBarDM_VatTu_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void navBarDM_NhaCungCap_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void navBarDM_NuocSanXuat_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void navBarDM_HangSanXuat_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void navBarDM_HoatChat_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void navBarDM_BietDuoc_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void navBarDM_DonViTinh_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void navBarDM_DuongDung_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }



        #endregion



    }
}
