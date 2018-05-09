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
    public partial class ucQLTaiChinh : UserControl
    {
        #region Declaration
        private ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        //private string CurrentTabPage { get; set; }
        //private int SelectedTabPageIndex { get; set; }
        // internal frmMain frmMain;

        // khai báo 1 hàm delegate
        internal delegate void GetString(string thoigian);
        // khai báo 1 kiểu hàm delegate
        internal GetString MyGetData;

        #endregion
        public ucQLTaiChinh()
        {
            InitializeComponent();
        }

        #region Load
        private void ucQLTaiChinh_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataDSBaoCao();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void LoadDataDSBaoCao()
        {
            try
            {
                gridControlDSBaoCao.DataSource = Base.SessionLogin.LstPhanQuyen_QLTaiChinh;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        internal void xtraTabControlChucNang_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                //frmMain = new frmMain();
                //this.CurrentTabPage = e.Page.Name;
                XtraTabControl xtab = new XtraTabControl();
                xtab = (XtraTabControl)sender;
                if (xtab != null)
                {
                    //this.SelectedTabPageIndex = xtab.SelectedTabPageIndex;
                    //delegate - thong tin chuc nang
                    // tại đây gọi nó
                    MyGetData(xtab.TabPages[xtab.SelectedTabPageIndex].Tooltip);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Custom
        private void gridViewDSBaoCao_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridViewDSBaoCao_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void gridViewDSBaoCao_DoubleClick(object sender, EventArgs e)
        {
            UserControl ucControlActive = new UserControl();
            try
            {
                var rowHandle = gridViewDSBaoCao.FocusedRowHandle;
                string code = gridViewDSBaoCao.GetRowCellValue(rowHandle, "permissioncode").ToString();
                string name = gridViewDSBaoCao.GetRowCellValue(rowHandle, "permissionname").ToString();
                string note = gridViewDSBaoCao.GetRowCellValue(rowHandle, "permissionnote").ToString();
                if (Convert.ToBoolean(gridViewDSBaoCao.GetRowCellValue(rowHandle, "permissioncheck"))) //xemlai...
                {
                    //Chon ucControl
                    ucControlActive = TabControlProcess.SelectUCControlActive(code);
                    MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlBaoCao, code, name, note, ucControlActive);
                    ucControlActive.Show();
                }
                else
                {
                    MessageBox.Show("Bạn không được phân quyền sử dụng chức năng này !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

    }
}
