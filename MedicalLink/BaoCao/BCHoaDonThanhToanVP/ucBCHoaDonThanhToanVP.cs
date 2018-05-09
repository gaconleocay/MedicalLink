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

namespace MedicalLink.BaoCao
{
    public partial class ucBCHoaDonThanhToanVP : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        //private DataTable dataDanhSachSo { get; set; }
        private List<BCHoaDonThanhToanVPDTO> lstDataHoaDonThanhToan = new List<BCHoaDonThanhToanVPDTO>();
        private decimal tongTienBaoCao = 0;
        #endregion

        #region Load
        public ucBCHoaDonThanhToanVP()
        {
            InitializeComponent();
        }

        private void ucBCHoaDonThanhToanVP_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                LoadDanhSachNguoiThu();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachNguoiThu()
        {
            try
            {
                string sql_ngthu = "select userhisid,usercode,username from nhompersonnel where usergnhom='2';";
                DataTable _dataThuNgan = condb.GetDataTable_HIS(sql_ngthu);
                chkcomboListNguoiThu.Properties.DataSource = _dataThuNgan;
                chkcomboListNguoiThu.Properties.DisplayMember = "username";
                chkcomboListNguoiThu.Properties.ValueMember = "userhisid";
                chkcomboListNguoiThu.CheckAll();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                gridControlDSHoaDon.DataSource = null;
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                string _listuserid = "";
                int loaiphieuthuid = 0; //thu tien

                if (cboLoaiSo.Text == "Tạm ứng")
                {
                    loaiphieuthuid = 2;
                }
                else if (cboLoaiSo.Text == "Hoàn ứng")
                {
                    loaiphieuthuid = 1;
                }

                List<Object> lstNhanVienCheck = chkcomboListNguoiThu.Properties.Items.GetCheckedValues();
                if (lstNhanVienCheck.Count > 0 || chkTatCa.Checked)
                {
                    if (chkTatCa.Checked == false)
                    {
                        _listuserid = " and userid in (";
                        for (int i = 0; i < lstNhanVienCheck.Count - 1; i++)
                        {
                            _listuserid += "'" + lstNhanVienCheck[i] + "', ";
                        }
                        _listuserid += "'" + lstNhanVienCheck[lstNhanVienCheck.Count - 1] + "') ";
                    }
                    string sql_getdata = "SELECT row_number () over (order by b.billgroupcode,cast(b.billcode as numeric)) as stt, b.billgroupcode, b.billcode, '' as billgrouptu_den, b.billdate, (b.datra-b.miengiam) as sotien, b.miengiam as miengiam, b.patientid, b.vienphiid, b.patientname, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname else '' end)) as diachi, hsba.bhytcode, b.userid, ngthu.username as nguoithu FROM (select hosobenhanid,patientid,vienphiid,patientname,billgroupcode,dahuyphieu,datra,(case when miengiam<>'' then cast(replace(miengiam,',','') as numeric) else 0 end) as miengiam,billcode,billdate,userid from bill where loaiphieuthuid='" + loaiphieuthuid + "' and dahuyphieu=0 and billdate between '" + tungay + "' and '" + denngay + "' " + _listuserid + ") b INNER JOIN (select vienphiid,hosobenhanid from vienphi) vp on vp.vienphiid=b.vienphiid INNER JOIN (select hosobenhanid,patientcode,patientname,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba on hsba.hosobenhanid=vp.hosobenhanid LEFT JOIN nhompersonnel ngthu ON ngthu.userhisid=b.userid; ";

                    DataTable dataDanhSachSo = condb.GetDataTable_HIS(sql_getdata);
                    if (dataDanhSachSo != null && dataDanhSachSo.Rows.Count > 0)
                    {
                        HienThiDuLieuLenLuoi(dataDanhSachSo);
                        //gridControlDSHoaDon.DataSource = this.dataDanhSachSo;
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void HienThiDuLieuLenLuoi(DataTable _dataBaoCao)
        {
            try
            {
                this.tongTienBaoCao = 0;
                this.lstDataHoaDonThanhToan = new List<BCHoaDonThanhToanVPDTO>();
                List<BCHoaDonThanhToanVPDTO> _lstHoaDonThanhToan = new List<BCHoaDonThanhToanVPDTO>();
                for (int i = 0; i < _dataBaoCao.Rows.Count; i++)
                {
                    BCHoaDonThanhToanVPDTO _dataBC = new BCHoaDonThanhToanVPDTO();
                    _dataBC.stt = _dataBaoCao.Rows[i]["stt"].ToString();
                    _dataBC.billgroupcode = _dataBaoCao.Rows[i]["billgroupcode"].ToString();
                    _dataBC.billcode = _dataBaoCao.Rows[i]["billcode"].ToString();
                    _dataBC.billdate = _dataBaoCao.Rows[i]["billdate"];
                    _dataBC.sotien = Utilities.Util_TypeConvertParse.ToDecimal(_dataBaoCao.Rows[i]["sotien"].ToString());
                    _dataBC.miengiam = Utilities.Util_TypeConvertParse.ToDecimal(_dataBaoCao.Rows[i]["miengiam"].ToString());
                    _dataBC.patientid = _dataBaoCao.Rows[i]["patientid"].ToString();
                    _dataBC.vienphiid = _dataBaoCao.Rows[i]["vienphiid"].ToString();
                    _dataBC.patientname = _dataBaoCao.Rows[i]["patientname"].ToString();
                    _dataBC.diachi = _dataBaoCao.Rows[i]["diachi"].ToString();
                    _dataBC.bhytcode = _dataBaoCao.Rows[i]["bhytcode"].ToString();
                    _dataBC.nguoithu = _dataBaoCao.Rows[i]["nguoithu"].ToString();
                    _lstHoaDonThanhToan.Add(_dataBC);
                }

                // 
                List<BCHoaDonThanhToanVPDTO> _lstQuyenSo = _lstHoaDonThanhToan.GroupBy(o => o.billgroupcode).Select(n => n.First()).ToList();
                foreach (var _item_qs in _lstQuyenSo)
                {
                    List<BCHoaDonThanhToanVPDTO> _lstQuyenSo_Data = _lstHoaDonThanhToan.Where(o => o.billgroupcode == _item_qs.billgroupcode).ToList();

                    decimal _quyenSo_tongtien = 0;
                    decimal _quyenSo_miengiam = 0;
                    foreach (var item_dt in _lstQuyenSo_Data)
                    {
                        _quyenSo_tongtien += item_dt.sotien;
                        _quyenSo_miengiam += item_dt.miengiam;
                        item_dt.billgrouptu_den = _item_qs.billgroupcode + " (" + _lstQuyenSo_Data[0].billcode + " - " + _lstQuyenSo_Data[_lstQuyenSo_Data.Count - 1].billcode + ")";
                    }
                    this.tongTienBaoCao += _quyenSo_tongtien;

                    BCHoaDonThanhToanVPDTO _dataItem_Gr = new BCHoaDonThanhToanVPDTO();
                    _dataItem_Gr.billgroupcode= _item_qs.billgroupcode + " (" + _lstQuyenSo_Data[0].billcode + " - " + _lstQuyenSo_Data[_lstQuyenSo_Data.Count - 1].billcode + ")";
                    _dataItem_Gr.billgrouptu_den = _item_qs.billgroupcode + "(" + _lstQuyenSo_Data[0].billcode + " - " + _lstQuyenSo_Data[_lstQuyenSo_Data.Count - 1].billcode + ")";
                    _dataItem_Gr.sotien = _quyenSo_tongtien;
                    _dataItem_Gr.miengiam = _quyenSo_miengiam;
                    _dataItem_Gr.isgroup = 1;

                    this.lstDataHoaDonThanhToan.Add(_dataItem_Gr);
                    this.lstDataHoaDonThanhToan.AddRange(_lstQuyenSo_Data);
                }

                gridControlDSHoaDon.DataSource = this.lstDataHoaDonThanhToan.Where(o => o.isgroup != 1).ToList();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #region Xuat bao cao and print
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDSHoaDon.RowCount > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;
                    thongTinThem.Add(reportitem);
                    ClassCommon.reportExcelDTO reportitem_tientong = new ClassCommon.reportExcelDTO();
                    reportitem_tientong.name = "SOTIEN_TONG_STRING";
                    reportitem_tientong.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.Util_NumberConvert.NumberToNumberRoundAuto(this.tongTienBaoCao,0).ToString());
                    thongTinThem.Add(reportitem_tientong);

                    string fileTemplatePath = "BC_HoaDonThanhToanVienPhi.xlsx";
                    DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, data_XuatBaoCao);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO reportitem_tientong = new ClassCommon.reportExcelDTO();
                reportitem_tientong.name = "SOTIEN_TONG_STRING";
                reportitem_tientong.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.Util_NumberConvert.NumberToNumberRoundAuto(this.tongTienBaoCao, 0).ToString());
                thongTinThem.Add(reportitem_tientong);

                string fileTemplatePath = "BC_HoaDonThanhToanVienPhi.xlsx";
                DataTable data_XuatBaoCao = ExportExcel_GroupColume();
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, data_XuatBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        //Xuat excel co group nhom
        private DataTable ExportExcel_GroupColume()
        {
            DataTable result = new DataTable();
            try
            {
                result = Utilities.Util_DataTable.ListToDataTable(this.lstDataHoaDonThanhToan);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void chkTatCa_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTatCa.Checked)
                {
                    chkcomboListNguoiThu.Enabled = false;
                }
                else
                { chkcomboListNguoiThu.Enabled = true; }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion


    }
}
