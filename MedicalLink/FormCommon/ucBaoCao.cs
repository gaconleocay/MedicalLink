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

namespace MedicalLink.FormCommon
{
    public partial class ucBaoCao : UserControl
    {
        #region Declaration
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        // public string CurrentTabPage { get; set; }
        //public int SelectedTabPageIndex { get; set; }
        //internal frmMain frmMain;

        // khai báo 1 hàm delegate
        internal delegate void GetString(string thoigian);
        // khai báo 1 kiểu hàm delegate
        internal GetString MyGetData;

        #endregion
        public ucBaoCao()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBaoCao_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataBC_Khoa();
                LoadDataBC_DoanhThu();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadDataBC_Khoa()
        {
            try
            {
                gridControlBC_Khoa.DataSource = Base.SessionLogin.LstPhanQuyen_BaoCaoKhoa;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void LoadDataBC_DoanhThu()
        {
            try
            {
                gridControlBC_DoanhThu.DataSource = Base.SessionLogin.LstPhanQuyen_BaoCaoDoanhThu;
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Tabcontrol function
        //Dong tab
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
                //frmMain = new frmMain();
                // this.CurrentTabPage = e.Page.Name;
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

        #region Custom Bao cao
        private void gridViewBC_Khoa_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void gridViewBC_Khoa_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == gridColumnstt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void gridControlBC_Khoa_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewBC_Khoa.FocusedRowHandle;
                txtThongTinChiTiet.Text = gridViewBC_Khoa.GetRowCellValue(rowHandle, "permissionnote").ToString();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void gridViewBC_Khoa_DoubleClick(object sender, EventArgs e)
        {
            UserControl ucControlActive = new UserControl();
            try
            {
                var rowHandle = gridViewBC_Khoa.FocusedRowHandle;
                string code = gridViewBC_Khoa.GetRowCellValue(rowHandle, "permissioncode").ToString();
                string name = gridViewBC_Khoa.GetRowCellValue(rowHandle, "permissionname").ToString();
                string note = gridViewBC_Khoa.GetRowCellValue(rowHandle, "permissionnote").ToString();
                if (Convert.ToBoolean(gridViewBC_Khoa.GetRowCellValue(rowHandle, "permissioncheck")))
                {
                    //Chon ucControl
                    ucControlActive = TabControlProcess.SelectUCControlActive(code);
                    MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, code, name, note, ucControlActive);
                    ucControlActive.Show();
                }
                else
                {
                    MessageBox.Show("Bạn không được phân quyền sử dụng chức năng này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #endregion

        #region Custom Bao cao doanh thu
        private void gridViewBC_DoanhThu_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }
        private void gridViewBC_DoanhThu_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column == gridDSBCColumeStt)
                {
                    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void gridViewBC_DoanhThu_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewBC_DoanhThu.FocusedRowHandle;
                txtThongTinChiTiet.Text = gridViewBC_DoanhThu.GetRowCellValue(rowHandle, "permissionnote").ToString();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void gridViewBC_DoanhThu_DoubleClick(object sender, EventArgs e)
        {
            UserControl ucControlActive = new UserControl();
            try
            {
                var rowHandle = gridViewBC_DoanhThu.FocusedRowHandle;
                string code = gridViewBC_DoanhThu.GetRowCellValue(rowHandle, "permissioncode").ToString();
                string name = gridViewBC_DoanhThu.GetRowCellValue(rowHandle, "permissionname").ToString();
                string note = gridViewBC_DoanhThu.GetRowCellValue(rowHandle, "permissionnote").ToString();
                if (Convert.ToBoolean(gridViewBC_DoanhThu.GetRowCellValue(rowHandle, "permissioncheck"))) //xemlai...
                {
                    //Chon ucControl
                    ucControlActive = TabControlProcess.SelectUCControlActive(code);
                    MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, code, name, note, ucControlActive);
                    ucControlActive.Show();
                }
                else
                {
                    MessageBox.Show("Bạn không được phân quyền sử dụng chức năng này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }





        #endregion

    }
}
