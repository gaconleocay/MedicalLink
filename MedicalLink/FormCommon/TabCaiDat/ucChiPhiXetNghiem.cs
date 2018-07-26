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
using Aspose.Cells;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Menu;
using MedicalLink.Base;

namespace MedicalLink.FormCommon.TabCaiDat
{
    public partial class ucChiPhiXetNghiem : UserControl
    {
        #region Khai bao
        private ConnectDatabase condb = new ConnectDatabase();
        private List<ClassCommon.MayXetNghiemKhuVucDTO> lstMayXNKhuVuc { get; set; }
        private List<ClassCommon.DVXetNghiemChiPhiDTO> lstDVXetNghiemChiPhi { get; set; }
        private List<ClassCommon.MayXetNghiemNhomBCDTO> lstMayXNNhomBC { get; set; }

        #endregion

        public ucChiPhiXetNghiem()
        {
            InitializeComponent();
        }

        #region Load
        private void ucChiPhiXetNghiem_Load(object sender, EventArgs e)
        {
            try
            {
                Load_MayXNKhuVuc();
                Load_DVXNChiPhi();
                Load_NhomBaoCao();
                EnableAndDisableControl();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void Load_MayXNKhuVuc()
        {
            try
            {
                gridControlXNMay.DataSource = null;
                string _getDMXNMay = "SELECT row_number () over (order by mayxn_ten) as stt, * FROM ml_mayxnkhuvuc;";
                DataTable dataDMXNMay = condb.GetDataTable_MeL(_getDMXNMay);
                if (dataDMXNMay != null && dataDMXNMay.Rows.Count > 0)
                {
                    this.lstMayXNKhuVuc = Utilities.DataTables.DataTableToList<ClassCommon.MayXetNghiemKhuVucDTO>(dataDMXNMay);
                }
                gridControlXNMay.DataSource = this.lstMayXNKhuVuc;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void Load_DVXNChiPhi()
        {
            try
            {
                gridControlDVXNChiPhi.DataSource = null;
                string _getXNChiPhi = "SELECT row_number () over (order by mayxn_ten,servicepricename) as stt, * FROM ml_mayxnchiphi;";
                DataTable dataXNChiPhi = condb.GetDataTable_MeL(_getXNChiPhi);
                if (dataXNChiPhi != null && dataXNChiPhi.Rows.Count > 0)
                {
                    this.lstDVXetNghiemChiPhi = Utilities.DataTables.DataTableToList<ClassCommon.DVXetNghiemChiPhiDTO>(dataXNChiPhi);
                }
                gridControlDVXNChiPhi.DataSource = this.lstDVXetNghiemChiPhi;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void Load_NhomBaoCao()
        {
            try
            {
                gridControlNhomBC.DataSource = null;
                string _getXNChiPhi = "SELECT row_number () over (order by nhombc_ten) as stt, * FROM ml_mayxnnhombc;";
                DataTable dataXNChiPhi = condb.GetDataTable_MeL(_getXNChiPhi);
                if (dataXNChiPhi != null && dataXNChiPhi.Rows.Count > 0)
                {
                    this.lstMayXNNhomBC = Utilities.DataTables.DataTableToList<ClassCommon.MayXetNghiemNhomBCDTO>(dataXNChiPhi);
                }
                gridControlNhomBC.DataSource = this.lstMayXNNhomBC;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void EnableAndDisableControl()
        {
            try
            {
                btnXNKV_Luu.Enabled = false;
                btnDVXNCP_Luu.Enabled = false;
                btnNhomBC_Luu.Enabled = false;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Tab May XN- khu vuc
        private void btnNhapTuExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    this.lstMayXNKhuVuc = new List<ClassCommon.MayXetNghiemKhuVucDTO>();
                    gridControlXNMay.DataSource = null;
                    Workbook workbook = new Workbook(openFileDialogSelect.FileName);
                    Worksheet worksheet = workbook.Worksheets["May_KhuVuc"];
                    DataTable data_Excel = worksheet.Cells.ExportDataTable(3, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, true);
                    data_Excel.TableName = "DATA";
                    if (data_Excel != null)
                    {
                        this.lstMayXNKhuVuc = Utilities.DataTables.DataTableToList<ClassCommon.MayXetNghiemKhuVucDTO>(data_Excel);
                        gridControlXNMay.DataSource = this.lstMayXNKhuVuc;
                        btnXNKV_Luu.Enabled = true;
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                        frmthongbao.Show();
                        btnXNKV_Luu.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                frmthongbao.Show();
                btnXNKV_Luu.Enabled = false;
            }
        }
        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(ThongBao.WaitForm1));
            try
            {
                int insert_count = 0;
                int _update_count = 0;
                foreach (var item_dv in this.lstMayXNKhuVuc)
                {
                    if (item_dv.STT != null)
                    {
                        string sql_insert = "";
                        try
                        {
                            string _sqlkiemtratrung = "SELECT * FROM ml_mayxnkhuvuc WHERE mayxn_ma='" + item_dv.MAYXN_MA + "'; ";
                            DataTable _dataKiemTra = condb.GetDataTable_MeL(_sqlkiemtratrung);
                            if (_dataKiemTra != null && _dataKiemTra.Rows.Count > 0)
                            {
                                //Xoa
                                string _sql_delete = "DELETE FROM ml_mayxnkhuvuc WHERE mayxn_ma='" + item_dv.MAYXN_MA + "'; ";
                                if (condb.ExecuteNonQuery_MeL(_sql_delete))
                                {
                                    _update_count += _dataKiemTra.Rows.Count;
                                }
                            }

                            sql_insert = "INSERT INTO ml_mayxnkhuvuc(mayxn_ma, mayxn_ten, khuvuc_ma, khuvuc_ten, lastuserupdated, lasttimeupdated) VALUES ('" + item_dv.MAYXN_MA + "', '" + item_dv.MAYXN_TEN + "', '" + item_dv.KHUVUC_MA + "', '" + item_dv.KHUVUC_TEN + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                            if (condb.ExecuteNonQuery_MeL(sql_insert))
                            {
                                insert_count += 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            MedicalLink.Base.Logging.Error("Loi insert ml_mayxnkhuvuc " + sql_insert);
                            continue;
                        }
                    }
                }
                MessageBox.Show("Thêm mới [" + (insert_count - _update_count) + "/" + this.lstMayXNKhuVuc.Where(o => o.STT != null).ToList().Count() + "]; cập nhật [" + _update_count + "] máy xét nghiệm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
            ucChiPhiXetNghiem_Load(null, null);
        }
        private void gridViewDichVu_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (!btnXNKV_Luu.Enabled)
                {
                    if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                    {
                        //GridView view = sender as GridView;
                        e.Menu.Items.Clear();
                        DXMenuItem itemKiemTraDaChon = new DXMenuItem("Xóa máy xét nghiệm đã chọn");
                        itemKiemTraDaChon.Image = imageCollectionDSBN.Images[0];
                        itemKiemTraDaChon.Click += new EventHandler(XoaMayXetNghiem_Click);
                        e.Menu.Items.Add(itemKiemTraDaChon);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void XoaMayXetNghiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewXNMay.RowCount > 0)
                {
                    string sql_deleteDV = "";
                    foreach (var item_index in gridViewXNMay.GetSelectedRows())
                    {
                        string mayxnkhuvucid = gridViewXNMay.GetRowCellValue(item_index, "MAYXNKHUVUCID").ToString();
                        sql_deleteDV += "DELETE FROM ml_mayxnkhuvuc where mayxnkhuvucid='" + mayxnkhuvucid + "'; ";
                    }
                    condb.ExecuteNonQuery_MeL(sql_deleteDV);
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(Base.ThongBaoLable.XOA_THANH_CONG);
                    frmthongbao.Show();
                    ucChiPhiXetNghiem_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnXNKV_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lstMayXNKhuVuc != null)
                {
                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    thongTinThem.Add(reportitem);
                    string fileTemplatePath = "0_ToolsMayXetNghiemKhuVuc_Export.xlsx";
                    DataTable _dataBaoCao = Utilities.DataTables.ListToDataTable(this.lstMayXNKhuVuc);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Tab DV Xet nghiem-Chi phi
        private void btnDVXNCP_Import_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    this.lstDVXetNghiemChiPhi = new List<ClassCommon.DVXetNghiemChiPhiDTO>();
                    gridControlDVXNChiPhi.DataSource = null;
                    Workbook workbook = new Workbook(openFileDialogSelect.FileName);
                    Worksheet worksheet = workbook.Worksheets["DV_HC_May"];
                    DataTable data_Excel = worksheet.Cells.ExportDataTable(3, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, true);
                    data_Excel.TableName = "DATA";
                    if (data_Excel != null)
                    {
                        this.lstDVXetNghiemChiPhi = Utilities.DataTables.DataTableToList<ClassCommon.DVXetNghiemChiPhiDTO>(data_Excel);
                        gridControlDVXNChiPhi.DataSource = this.lstDVXetNghiemChiPhi;
                        btnDVXNCP_Luu.Enabled = true;
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                        frmthongbao.Show();
                        btnDVXNCP_Luu.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                frmthongbao.Show();
                btnXNKV_Luu.Enabled = false;
            }
        }
        private void btnDVXNCP_Luu_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(ThongBao.WaitForm1));
            try
            {
                int insert_count = 0;
                int _update_count = 0;
                foreach (var item_dv in this.lstDVXetNghiemChiPhi)
                {
                    if (item_dv.STT != null)
                    {
                        string sql_insert = "";
                        try
                        {
                            string _mayxn_ma = item_dv.MAYXN_MA ?? "0";
                            if (_mayxn_ma == "")
                            {
                                _mayxn_ma = "0";
                            }

                            string _sqlkiemtratrung = "SELECT * FROM ml_mayxnchiphi WHERE mayxn_ma='" + _mayxn_ma + "' and servicepricecode='" + item_dv.SERVICEPRICECODE + "'; ";
                            DataTable _dataKiemTra = condb.GetDataTable_MeL(_sqlkiemtratrung);
                            if (_dataKiemTra != null && _dataKiemTra.Rows.Count > 0)
                            {
                                //Xoa
                                string _sql_delete = "DELETE FROM ml_mayxnchiphi WHERE mayxn_ma='" + _mayxn_ma + "' and servicepricecode='" + item_dv.SERVICEPRICECODE + "'; ";
                                if (condb.ExecuteNonQuery_MeL(_sql_delete))
                                {
                                    _update_count += _dataKiemTra.Rows.Count;
                                }
                            }

                            string _SERVICEPRICENAME = item_dv.SERVICEPRICENAME != null ? item_dv.SERVICEPRICENAME.Replace("'", "''") : "";
                            string _cp_hoachat = item_dv.CP_HOACHAT != null ? item_dv.CP_HOACHAT.ToString().Replace(",", ".") : "0";
                            string _cp_haophixn = item_dv.CP_HAOPHIXN != null ? item_dv.CP_HAOPHIXN.ToString().Replace(",", ".") : "0";
                            string _cp_luong = item_dv.CP_LUONG != null ? item_dv.CP_LUONG.ToString().Replace(",", ".") : "0";
                            string _cp_diennuoc = item_dv.CP_DIENNUOC != null ? item_dv.CP_DIENNUOC.ToString().Replace(",", ".") : "0";
                            string _cp_khmaymoc = item_dv.CP_KHMAYMOC != null ? item_dv.CP_KHMAYMOC.ToString().Replace(",", ".") : "0";
                            string _cp_khxaydung = item_dv.CP_KHXAYDUNG != null ? item_dv.CP_KHXAYDUNG.ToString().Replace(",", ".") : "0";


                            sql_insert = "INSERT INTO ml_mayxnchiphi(mayxn_ma, mayxn_ten, servicepricecode, servicepricename, servicepriceunit, cp_hoachat, cp_haophixn, cp_luong, cp_diennuoc, cp_khmaymoc, cp_khxaydung, nhombc_ma, lastuserupdated, lasttimeupdated) VALUES ('" + _mayxn_ma + "', '" + item_dv.MAYXN_TEN + "', '" + item_dv.SERVICEPRICECODE + "', '" + _SERVICEPRICENAME + "', '" + item_dv.SERVICEPRICEUNIT + "', '" + _cp_hoachat + "', '" + _cp_haophixn + "', '" + _cp_luong + "', '" + _cp_diennuoc + "', '" + _cp_khmaymoc + "', '" + _cp_khxaydung + "', '" + item_dv.NHOMBC_MA + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                            if (condb.ExecuteNonQuery_MeL(sql_insert))
                            {
                                insert_count += 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            MedicalLink.Base.Logging.Error("Loi insert ml_mayxnchiphi " + sql_insert);
                            continue;
                        }
                    }
                }
                MessageBox.Show("Thêm mới [" + (insert_count - _update_count) + "/" + this.lstDVXetNghiemChiPhi.Where(o => o.STT != null).ToList().Count() + "]; cập nhật [" + _update_count + "] chi phí xét nghiệm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
            ucChiPhiXetNghiem_Load(null, null);
        }
        private void gridViewDVXNChiPhi_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (!btnXNKV_Luu.Enabled)
                {
                    if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                    {
                        //GridView view = sender as GridView;
                        e.Menu.Items.Clear();
                        DXMenuItem itemKiemTraDaChon = new DXMenuItem("Xóa chi phí xét nghiệm đã chọn");
                        itemKiemTraDaChon.Image = imageCollectionDSBN.Images[0];
                        itemKiemTraDaChon.Click += new EventHandler(XoaChiPhiXetNghiem_Click);
                        e.Menu.Items.Add(itemKiemTraDaChon);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void XoaChiPhiXetNghiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDVXNChiPhi.RowCount > 0)
                {
                    string sql_deleteDV = "";
                    foreach (var item_index in gridViewDVXNChiPhi.GetSelectedRows())
                    {
                        string mayxndmxncpid = gridViewDVXNChiPhi.GetRowCellValue(item_index, "MAYXNDMXNCPID").ToString();
                        sql_deleteDV += "DELETE FROM ml_mayxnchiphi where mayxndmxncpid='" + mayxndmxncpid + "'; ";
                    }
                    condb.ExecuteNonQuery_MeL(sql_deleteDV);
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(Base.ThongBaoLable.XOA_THANH_CONG);
                    frmthongbao.Show();
                    ucChiPhiXetNghiem_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnDVXNCP_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lstDVXetNghiemChiPhi != null)
                {
                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    thongTinThem.Add(reportitem);
                    string fileTemplatePath = "0_ToolsDVKTChiPhi_Export.xlsx";
                    DataTable _dataBaoCao = Utilities.DataTables.ListToDataTable(this.lstDVXetNghiemChiPhi);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


        #endregion

        #region Tab Nhom bao cao
        private void btnNhomBC_Import_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    this.lstMayXNNhomBC = new List<ClassCommon.MayXetNghiemNhomBCDTO>();
                    gridControlNhomBC.DataSource = null;
                    Workbook workbook = new Workbook(openFileDialogSelect.FileName);
                    Worksheet worksheet = workbook.Worksheets["NhomBC"];
                    DataTable data_Excel = worksheet.Cells.ExportDataTable(3, 0, worksheet.Cells.MaxDataRow + 1, worksheet.Cells.MaxDataColumn + 1, true);
                    data_Excel.TableName = "DATA";
                    if (data_Excel != null)
                    {
                        this.lstMayXNNhomBC = Utilities.DataTables.DataTableToList<ClassCommon.MayXetNghiemNhomBCDTO>(data_Excel);
                        gridControlNhomBC.DataSource = this.lstMayXNNhomBC;
                        btnNhomBC_Luu.Enabled = true;
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                        frmthongbao.Show();
                        btnNhomBC_Luu.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("File excel sai định dạng cấu trúc!");
                frmthongbao.Show();
                btnXNKV_Luu.Enabled = false;
            }
        }
        private void btnNhomBC_Luu_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(ThongBao.WaitForm1));
            try
            {
                int insert_count = 0;
                int _update_count = 0;
                foreach (var item_dv in this.lstMayXNNhomBC)
                {
                    if (item_dv.STT != null)
                    {
                        string sql_insert = "";
                        try
                        {
                            string _sqlkiemtratrung = "SELECT * FROM ml_mayxnnhombc WHERE nhombc_ma='" + item_dv.NHOMBC_MA + "'; ";
                            DataTable _dataKiemTra = condb.GetDataTable_MeL(_sqlkiemtratrung);
                            if (_dataKiemTra != null && _dataKiemTra.Rows.Count > 0)
                            {
                                //Xoa
                                string _sql_delete = "DELETE FROM ml_mayxnnhombc WHERE nhombc_ma='" + item_dv.NHOMBC_MA + "'; ";
                                if (condb.ExecuteNonQuery_MeL(_sql_delete))
                                {
                                    _update_count += _dataKiemTra.Rows.Count;
                                }
                            }

                            sql_insert = "INSERT INTO ml_mayxnnhombc(nhombc_ma, nhombc_ten, istrakq, ghichu, lastuserupdated, lasttimeupdated) VALUES ('" + item_dv.NHOMBC_MA + "', '" + item_dv.NHOMBC_TEN + "', '" + item_dv.ISTRAKQ + "', '" + item_dv.GHICHU + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                            if (condb.ExecuteNonQuery_MeL(sql_insert))
                            {
                                insert_count += 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            MedicalLink.Base.Logging.Error("Loi insert ml_mayxnnhombc " + sql_insert);
                            continue;
                        }
                    }
                }
                MessageBox.Show("Thêm mới [" + (insert_count - _update_count) + "/" + this.lstMayXNNhomBC.Where(o => o.STT != null).ToList().Count() + "]; cập nhật [" + _update_count + "] nhóm báo cáo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
            ucChiPhiXetNghiem_Load(null, null);
        }
        private void gridViewNhomBC_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (!btnXNKV_Luu.Enabled)
                {
                    if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                    {
                        //GridView view = sender as GridView;
                        e.Menu.Items.Clear();
                        DXMenuItem itemKiemTraDaChon = new DXMenuItem("Xóa nhóm báo cáo đã chọn");
                        itemKiemTraDaChon.Image = imageCollectionDSBN.Images[0];
                        itemKiemTraDaChon.Click += new EventHandler(XoaNhomBaoCao_Click);
                        e.Menu.Items.Add(itemKiemTraDaChon);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void XoaNhomBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewNhomBC.RowCount > 0)
                {
                    string sql_deleteDV = "";
                    foreach (var item_index in gridViewNhomBC.GetSelectedRows())
                    {
                        string mayxnnhombcid = gridViewNhomBC.GetRowCellValue(item_index, "MAYXNNHOMBCID").ToString();
                        sql_deleteDV += "DELETE FROM ml_mayxnnhombc where mayxnnhombcid='" + mayxnnhombcid + "'; ";
                    }
                    condb.ExecuteNonQuery_MeL(sql_deleteDV);
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(Base.ThongBaoLable.XOA_THANH_CONG);
                    frmthongbao.Show();
                    ucChiPhiXetNghiem_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnNhomBC_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lstMayXNNhomBC != null)
                {
                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    thongTinThem.Add(reportitem);
                    string fileTemplatePath = "0_ToolsMayXetNghiemNhomBC_Export.xlsx";
                    DataTable _dataBaoCao = Utilities.DataTables.ListToDataTable(this.lstMayXNNhomBC);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Custom
        private void gridViewDichVu_RowCellStyle(object sender, RowCellStyleEventArgs e)
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




    }
}
