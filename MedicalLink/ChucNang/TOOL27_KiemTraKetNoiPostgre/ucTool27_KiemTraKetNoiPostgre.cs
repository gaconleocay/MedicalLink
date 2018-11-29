using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using MedicalLink.ChucNang.TOOL27_KiemTraKetNoiPostgre;

namespace MedicalLink.ChucNang
{
    public partial class ucTool27_KiemTraKetNoiPostgre : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        #endregion

        public ucTool27_KiemTraKetNoiPostgre()
        {
            InitializeComponent();
        }

        #region Load
        private void ucTool27_KiemTraKetNoiPostgre_Load(object sender, EventArgs e)
        {
            LoadDanhSachDatabase();

            gridControlTongHop.Visible = true;
            gridControlTongHop.DataSource = null;
            gridControlTongHop.Dock = DockStyle.Fill;
            gridControlChiTiet.Visible = false;
        }
        private void LoadDanhSachDatabase()
        {
            try
            {
                string _sqldb = "SELECT datname FROM pg_database WHERE datistemplate = false order by datname;";
                DataTable _dataDB = condb.GetDataTable_HIS(_sqldb);

                chkListDSCSDL.Properties.DataSource = _dataDB;
                chkListDSCSDL.Properties.DisplayMember = "datname";
                chkListDSCSDL.Properties.ValueMember = "datname";
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Events
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string _lstdatname = " and datname in ('0'";
                string _state = " and state='" + cboTrangThai.Text + "' ";
                //chon DB
                List<Object> lstKhoaCheck = chkListDSCSDL.Properties.Items.GetCheckedValues();
                if (lstKhoaCheck.Count > 0)
                {
                    for (int i = 0; i < lstKhoaCheck.Count; i++)
                    {
                        _lstdatname += ",'" + lstKhoaCheck[i] + "'";
                    }
                    _lstdatname += ")";
                }
                else
                {
                    SplashScreenManager.CloseForm();
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                    return;
                }
                //state
                if (cboTrangThai.Text == "all")
                {
                    _state = "";
                }

                if (radioXemChiTiet.Checked)
                {
                    string _sqldata = $@"select row_number () over (order by datname,client_addr,usename,state_change) as stt, TO_CHAR((current_timestamp-state_change),'HH24:MI:ss') as time_change, * from pg_stat_activity
where 1=1 {_lstdatname} {_state} ;";
                    DataTable _dataData = condb.GetDataTable_HIS(_sqldata);
                    if (_dataData != null && _dataData.Rows.Count > 0)
                    {
                        gridControlChiTiet.DataSource = _dataData;
                    }
                    else
                    {
                        gridControlChiTiet.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
                else//tong hop
                {
                    string _sqldata = $@"select row_number () over (order by datname,client_addr) as stt,
	datname,usesysid,usename,client_addr,count(*) as soluong
from pg_stat_activity
where 1=1 {_lstdatname} {_state}
group by datname,usesysid,usename,client_addr;";
                    DataTable _dataData = condb.GetDataTable_HIS(_sqldata);
                    if (_dataData != null && _dataData.Rows.Count > 0)
                    {
                        gridControlTongHop.DataSource = _dataData;
                    }
                    else
                    {
                        gridControlTongHop.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void radioXemTongHop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    radioXemChiTiet.Checked = false;
                    gridControlTongHop.Visible = true;
                    gridControlTongHop.DataSource = null;
                    gridControlTongHop.Dock = DockStyle.Fill;
                    gridControlChiTiet.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void radioXemChiTiet_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemChiTiet.Checked)
                {
                    radioXemTongHop.Checked = false;
                    gridControlChiTiet.Visible = true;
                    gridControlChiTiet.DataSource = null;
                    gridControlChiTiet.Dock = DockStyle.Fill;
                    gridControlTongHop.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridViewChiTiet_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DXMenuItem itemmenu1 = new DXMenuItem("Tắt các kết nối đã chọn");
                    itemmenu1.Image = imMenu_HSBA.Images[0];
                    itemmenu1.Click += new EventHandler(TatCacKetNoiDaChon_Click);
                    e.Menu.Items.Add(itemmenu1);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void TatCacKetNoiDaChon_Click(object sender, EventArgs e)
        {
            try
            {
                string _sqlkill = "";
                if (gridViewChiTiet.GetSelectedRows().Count() > 0)
                {
                    foreach (var item_index in gridViewChiTiet.GetSelectedRows())
                    {
                        string _state = gridViewChiTiet.GetRowCellValue(item_index, "state").ToString();
                        if (_state != "active")
                        {
                            string _pid = gridViewChiTiet.GetRowCellValue(item_index, "pid").ToString();
                            string _datname = gridViewChiTiet.GetRowCellValue(item_index, "datname").ToString();

                            _sqlkill += "SELECT pg_terminate_backend(" + _pid + ") FROM pg_stat_activity WHERE datname='" + _datname + "' AND pid <> pg_backend_pid() and pid=" + _pid + " and state<>'active' and query in ('unlisten *','DISCARD ALL'); ";
                        }
                    }
                    if (_sqlkill != "" && condb.ExecuteNonQuery_HIS(_sqlkill))
                    {
                        btnRefresh.PerformClick();
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.THAO_TAC_THANH_CONG);
                        frmthongbao.Show();
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Thao tác thất bại!");
                        frmthongbao.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnCauHinh_Click(object sender, EventArgs e)
        {
            frmCauHinhNgatKetNoiTuDong _frm = new frmCauHinhNgatKetNoiTuDong();
            _frm.ShowDialog();
        }
        #endregion

        #region Xuat excel
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dataBaoCao = new DataTable();
                if (radioXemChiTiet.Checked)
                {
                    _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewChiTiet);
                }
                else
                {
                    _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewTongHop);
                }
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelNotTemplate("DANH SÁCH KẾT NỐI", _dataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Custom
        private void gridViewChiTiet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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


        #endregion

        #region Chay kiem tra tu dong
        private void timerTGKiemTra_Tick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion


    }
}
