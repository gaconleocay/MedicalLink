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
using System.Globalization;
using System.IO;
using MedicalLink.Base;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using DevExpress.Utils.Menu;
using MedicalLink.Utilities.GridControl;
using MedicalLink.DatabaseProcess;
using DevExpress.XtraPrinting;
using MedicalLink.Utilities;

namespace MedicalLink.BaoCao
{
    public partial class ucBCHoaDonChungTuThuocSD : UserControl
    {
        #region Declaration
        DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private List<BCHoaDonThanhToanVPDTO> lstDataHoaDonThuoc = new List<BCHoaDonThanhToanVPDTO>();
        private DataTable dataBaoCao { get; set; }
        #endregion

        #region Load
        public ucBCHoaDonChungTuThuocSD()
        {
            InitializeComponent();
        }

        private void ucBCHoaDonChungTuThuocSD_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDanhSachKho();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDanhSachKho()
        {
            try
            {
                string sql_ngthu = "";
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_35_KHO").ToList();
                if (lstOtherList != null && lstOtherList.Count > 0)
                {
                    string _lstDSKhoaID = lstOtherList[0].tools_otherlistvalue;
                    sql_ngthu = "SELECT medicinestoreid, medicinestorename FROM medicine_store WHERE medicinestoreid in (" + _lstDSKhoaID + ") ORDER BY medicinestorename";
                }
                else
                {
                    sql_ngthu = "SELECT medicinestoreid, medicinestorename FROM medicine_store WHERE medicinestoretype in (1,3,7) ORDER BY medicinestorename;";
                }
                System.Data.DataTable _dataKho = condb.GetDataTable_HIS(sql_ngthu);
                chklstKho.Properties.DataSource = _dataKho;
                chklstKho.Properties.DisplayMember = "medicinestorename";
                chklstKho.Properties.ValueMember = "medicinestoreid";
                chklstKho.CheckAll();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        #endregion

        #region Tim kiem va hien thi
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string _tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string _denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string _listKhoThuoc = " and medicinestoreid in (";

                List<Object> lstKhoCheck = chklstKho.Properties.Items.GetCheckedValues();
                if (lstKhoCheck.Count > 0)
                {
                    for (int i = 0; i < lstKhoCheck.Count - 1; i++)
                    {
                        _listKhoThuoc += "'" + lstKhoCheck[i] + "', ";
                    }
                    _listKhoThuoc += "'" + lstKhoCheck[lstKhoCheck.Count - 1] + "') ";

                    string sql_getdata = "SELECT (row_number() over (partition by kho.medicinestoreremark order by msb.medicinestorebilldate)) as stt, msb.medicinestorebillcode, msb.medicinestorebilldate, msb.sochungtu, msb.medicinestorebilltotalmoney as tongtien, (select count(*) from medicine where medicinestorebillid=msb.medicinestorebillid) as soluong, ncc.nhacungcapname as nhacungcap, msb.customername as nguoigiaohang, (case when msb.ngaychungtu<>'0001-01-01 00:00:00' then msb.ngaychungtu end) as ngaynhanduhang, '' as nguoinhanhang, msb.medicinestoreid, kho.medicinestorename, kho.medicinestoreremark as khonhanhang, msb.medicinestorebillremark as ghichu, '0' as isgroup FROM (select medicinestorebillid,medicinestorebillcode,medicinestorebilldate,sochungtu,medicinestorebilltotalmoney,partnerid,customername,ngaychungtu,medicinestoreid,medicinestorebillremark from medicine_store_bill where medicinestorebilltype=1 and medicinestorebillstatus=2 and medicinestorebilldate between '" + _tungay + "' and '" + _denngay + "' " + _listKhoThuoc + " ) msb LEFT JOIN (select nhacungcapid,nhacungcapname from nhacungcap_medicine) ncc on ncc.nhacungcapid=msb.partnerid INNER JOIN (select medicinestoreid,medicinestorename,medicinestoreremark from medicine_store) kho on kho.medicinestoreid=msb.medicinestoreid;";

                    this.dataBaoCao = condb.GetDataTable_HIS(sql_getdata);
                    if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                    {
                        gridControlDatBaoCao.DataSource = this.dataBaoCao;
                    }
                    else
                    {
                        gridControlDatBaoCao.DataSource = null;
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        #region Xuat bao cao and print
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDatBaoCao.RowCount > 0)
                {
                    string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;
                    thongTinThem.Add(reportitem);
                    ClassCommon.reportExcelDTO reportitem_kho = new ClassCommon.reportExcelDTO();
                    reportitem_kho.name = Base.BienTrongBaoCao.MEDICINESTORENAME;
                    reportitem_kho.value = chklstKho.Text;
                    thongTinThem.Add(reportitem_kho);

                    string fileTemplatePath = "BC_35_HoaDonChungTuCuaThuocVTYTSuDung.xlsx";
                    System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
                string tungay = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_kho = new ClassCommon.reportExcelDTO();
                reportitem_kho.name = Base.BienTrongBaoCao.MEDICINESTORENAME;
                reportitem_kho.value = chklstKho.Text;
                thongTinThem.Add(reportitem_kho);

                string fileTemplatePath = "BC_35_HoaDonChungTuCuaThuocVTYTSuDung.xlsx";
                System.Data.DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume()
        {
            System.Data.DataTable result = new System.Data.DataTable();
            try
            {
                List<ClassCommon.BCHoaDonChungTuThuocSDDTO> lstData_XuatBaoCao = new List<ClassCommon.BCHoaDonChungTuThuocSDDTO>();
                List<ClassCommon.BCHoaDonChungTuThuocSDDTO> lstDataDoanhThu = new List<ClassCommon.BCHoaDonChungTuThuocSDDTO>();

                lstDataDoanhThu = DataTables.DataTableToList<ClassCommon.BCHoaDonChungTuThuocSDDTO>(this.dataBaoCao);

                List<ClassCommon.BCHoaDonChungTuThuocSDDTO> lstData_Group = lstDataDoanhThu.GroupBy(o => o.medicinestoreid).Select(n => n.First()).ToList();
                foreach (var item_group in lstData_Group)
                {
                    ClassCommon.BCHoaDonChungTuThuocSDDTO data_groupname = new ClassCommon.BCHoaDonChungTuThuocSDDTO();

                    List<ClassCommon.BCHoaDonChungTuThuocSDDTO> lstData_doanhthu = lstDataDoanhThu.Where(o => o.medicinestoreid == item_group.medicinestoreid).ToList();

                    decimal sum_tongtien = 0;
                    foreach (var item_tinhsum in lstData_doanhthu)
                    {
                        sum_tongtien += item_tinhsum.tongtien;
                    }

                    //data_groupname.medicinestoreid = item_group.medicinestoreid;
                    data_groupname.stt = item_group.khonhanhang;
                    data_groupname.tongtien = sum_tongtien;
                    data_groupname.isgroup = 1;

                    lstData_XuatBaoCao.Add(data_groupname);
                    lstData_XuatBaoCao.AddRange(lstData_doanhthu);
                }
                result = Utilities.DataTables.ListToDataTable(lstData_XuatBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }


        #endregion

        #region Custom

        private void gridViewDSHoaDon_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
        #endregion
    }
}
