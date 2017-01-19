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

namespace MedicalLink.FormCommon
{
    public partial class ucChucNang : UserControl
    {
        #region Declaration
        public string CurrentTabPage { get; set; }
        public int SelectedTabPageIndex { get; set; }
        internal frmMain frmMain;
        #endregion
        public ucChucNang()
        {
            InitializeComponent();
        }

        #region Load
        private void ucChucNang_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataDSChucNang();
                LoadDataDSBaoCao();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void LoadDataDSChucNang()
        {
            try
            {
                List<ClassCommon.classPermission> lstDSChucNang = new List<ClassCommon.classPermission>();
                lstDSChucNang = MedicalLink.Base.listChucNang.getDanhSachChucNang().Where(o => o.permissiontype == 2).ToList();
                gridControlDSChucNang.DataSource = lstDSChucNang;
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
                List<ClassCommon.classPermission> lstDSBaoCao = new List<ClassCommon.classPermission>();
                lstDSBaoCao = MedicalLink.Base.listChucNang.getDanhSachChucNang().Where(o => o.permissiontype == 3).ToList();
                gridControlDSBaoCao.DataSource = lstDSBaoCao;
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
        private void xtraTabControlChucNang_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
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

        #region Grid DS Chuc Nang
        private void gridViewDSChucNang_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridViewDSChucNang_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void gridControlDSChucNang_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDSChucNang.FocusedRowHandle;
                txtThongTinChiTiet.Text = gridViewDSChucNang.GetRowCellValue(rowHandle, "permissionname").ToString();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void gridViewDSChucNang_DoubleClick(object sender, EventArgs e)
        {
            UserControl ucControlActive = new UserControl();
            try
            {
                var rowHandle = gridViewDSChucNang.FocusedRowHandle;
                string code = gridViewDSChucNang.GetRowCellValue(rowHandle, "permissioncode").ToString();
                string name = gridViewDSChucNang.GetRowCellValue(rowHandle, "permissionname").ToString();
                bool permission = Convert.ToBoolean(gridViewDSChucNang.GetRowCellValue(rowHandle, "permissioncheck"));

                if (permission ==false) //xemlai...
                {
                    //Chon ucControl
                   ucControlActive= TabControlProcess.SelectUCControlActive(code);
                   MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, code, name, ucControlActive);
                    ucControlActive.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        #region Gird DS Bao Cao
        private void gridViewDSBaoCao_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
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

        private void gridViewDSBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDSBaoCao.FocusedRowHandle;
                txtThongTinChiTiet.Text = gridViewDSBaoCao.GetRowCellValue(rowHandle, "permissionname").ToString();
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
                bool permission = Convert.ToBoolean(gridViewDSBaoCao.GetRowCellValue(rowHandle, "permissioncheck"));

                if (permission == false) //xemlai...
                {
                    //Chon ucControl
                    ucControlActive = TabControlProcess.SelectUCControlActive(code);
                    MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlChucNang, code, name, ucControlActive);
                    ucControlActive.Show();
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
