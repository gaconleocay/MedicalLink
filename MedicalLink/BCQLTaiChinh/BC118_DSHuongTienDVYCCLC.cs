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
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon.BCQLTaiChinh;

namespace MedicalLink.BCQLTaiChinh
{
    public partial class BC118_DSHuongTienDVYCCLC : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<BacSiDTO> lstBacSi { get; set; }
        private List<BC118DSHuongTienDVYCCLCDTO> lstResultBC = new List<BC118DSHuongTienDVYCCLCDTO>();
        private string DanhMucDichVu_String { get; set; }

        #endregion

        public BC118_DSHuongTienDVYCCLC()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC118_DSHuongTienDVYCCLC_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                //load danh muc dich vu
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_118_DV").ToList();
                if (lstOtherList != null && lstOtherList.Count > 0)
                {
                    for (int i = 0; i < lstOtherList.Count - 1; i++)
                    {
                        this.DanhMucDichVu_String += lstOtherList[i].tools_otherlistvalue + ",";
                    }
                    this.DanhMucDichVu_String += lstOtherList[lstOtherList.Count - 1].tools_otherlistvalue;
                }
                LoadDanhSachBacSi();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachBacSi()
        {
            try
            {
                string _sqlbacsi = "SELECT userhisid,usercode,username,departmentgroupname FROM ml_nhanvien ORDER BY username;";
                DataTable _dataBacSi = condb.GetDataTable_MeL(_sqlbacsi);
                if (_dataBacSi.Rows.Count > 0)
                {
                    this.lstBacSi = Utilities.DataTables.DataTableToList<BacSiDTO>(_dataBacSi);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                this.lstResultBC = new List<BC118DSHuongTienDVYCCLCDTO>();

                string tieuchi_ser = " and servicepricedate >= '2017-01-01 00:00:00' ";
                string tieuchi_vp = " and vienphidate >= '2017-01-01 00:00:00' ";
                string tieuchi_pttt = " and phauthuatthuthuatdate>'2017-01-01 00:00:00' ";
                string lstdichvu_ser = " and servicepricecode in (" + this.DanhMucDichVu_String + ") ";
                string trangthai_vp = "";
                string sql_timkiem = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    tieuchi_pttt = " and phauthuatthuthuatdate>'" + datetungay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
                    tieuchi_ser = " and servicepricedate >= '" + datetungay + "' ";
                    tieuchi_pttt = " and phauthuatthuthuatdate>'" + datetungay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày ra viện")
                {
                    tieuchi_vp = " and vienphidate_ravien between '" + datetungay + "' and '" + datedenngay + "' ";                
                }
                else if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_vp = " and duyet_ngayduyet_vp between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                //trang thai
                if (cboTrangThai.Text == "Đang điều trị")
                {
                    trangthai_vp = " and vienphistatus=0 ";
                }
                else if (cboTrangThai.Text == "Ra viện chưa thanh toán")
                {
                    trangthai_vp = " and vienphistatus<>0 and COALESCE(vienphistatus_vp,0)=0 ";
                }
                else if (cboTrangThai.Text == "Đã thanh toán")
                {
                    trangthai_vp = " and vienphistatus<>0 and vienphistatus_vp=1 ";
                }

                sql_timkiem = $@"SELECT
	vp.vienphiid,
	pttt.phauthuatvien,
	pttt.phumo1,
	pttt.phumo2,
	ser.soluong,
	ser.dongia,
	(case when (pttt.phumo1>0 and pttt.phumo2>0) then (ser.soluong/2)
			else ser.soluong end) as soluong_phu
FROM 
	(select vienphiid,servicepriceid,phauthuatvien,phumo1,phumo2,bacsigayme,phume,dungcuvien from phauthuatthuthuat where 1=1 {tieuchi_pttt}) pttt
	inner join (select vienphiid,servicepriceid,soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where 1=1 {lstdichvu_ser} {tieuchi_ser}) ser on ser.servicepriceid=pttt.servicepriceid
	inner join (select vienphiid from vienphi where 1=1 {tieuchi_vp} {trangthai_vp}) vp on vp.vienphiid=ser.vienphiid;";

            DataTable dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (dataBaoCao != null && dataBaoCao.Rows.Count > 0)
                {
                    XuLyVaHienThiBaoCao_TongHop(dataBaoCao);
                }
                else
                {
                    gridControlDataBC.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }


        #endregion

        #region In va xuat file
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TONGTIEN_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTien(), 0).ToString());
                thongTinThem.Add(_item_tien_string);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                string fileTemplatePath = "BC_118_DSHuongTienDVYCCLC.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TONGTIEN_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTien(), 0).ToString());
                thongTinThem.Add(_item_tien_string);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                string fileTemplatePath = "BC_118_DSHuongTienDVYCCLC.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Custom
        private void bandedGridViewDataBC_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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

        #region Process
        private decimal TinhTongTien()
        {
            decimal _result = 0;
            try
            {
                _result = this.lstResultBC.Sum(s => s.thuclinh);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            return _result;
        }

        private void XuLyVaHienThiBaoCao_TongHop(DataTable _dataBC)
        {
            try
            {
                List<BC118ThucHienPTTTDTO> _lstThucHienPTTT = Utilities.DataTables.DataTableToList<BC118ThucHienPTTTDTO>(_dataBC);

                if (this.lstBacSi != null)
                {
                    foreach (var _itemBS in this.lstBacSi)
                    {
                        BC118DSHuongTienDVYCCLCDTO _bacsiBC = new BC118DSHuongTienDVYCCLCDTO();
                        _bacsiBC.stt = this.lstResultBC.Count + 1;
                        _bacsiBC.usercode = _itemBS.usercode;
                        _bacsiBC.username = _itemBS.username;
                        _bacsiBC.departmentgroupname = _itemBS.departmentgroupname;

                        //phauthuatvien - 2000000
                        var _lstphauthuatvien2 = _lstThucHienPTTT.Where(o => o.phauthuatvien == _itemBS.userhisid && o.dongia== 2000000).ToList();
                        if (_lstphauthuatvien2 != null && _lstphauthuatvien2.Count > 0)
                        {
                            _bacsiBC.mochinh_sl2 = _lstphauthuatvien2.Sum(s => s.soluong);
                        }
                        //phauthuatvien - 3000000
                        var _lstphauthuatvien3 = _lstThucHienPTTT.Where(o => o.phauthuatvien == _itemBS.userhisid && o.dongia == 3000000).ToList();
                        if (_lstphauthuatvien3 != null && _lstphauthuatvien3.Count > 0)
                        {
                            _bacsiBC.mochinh_sl3 = _lstphauthuatvien3.Sum(s => s.soluong);
                        }
                        //phauthuatvien - 5000000
                        var _lstphauthuatvien5 = _lstThucHienPTTT.Where(o => o.phauthuatvien == _itemBS.userhisid && o.dongia == 5000000).ToList();
                        if (_lstphauthuatvien5 != null && _lstphauthuatvien5.Count > 0)
                        {
                            _bacsiBC.mochinh_sl5 = _lstphauthuatvien5.Sum(s => s.soluong);
                        }
                        //phu - 2000000
                        var _lstphu2 = _lstThucHienPTTT.Where(o => (o.phumo1 == _itemBS.userhisid || o.phumo2 == _itemBS.userhisid) && o.dongia == 2000000).ToList();
                        if (_lstphu2 != null && _lstphu2.Count > 0)
                        {
                            _bacsiBC.phu_sl2 = _lstphu2.Sum(s => s.soluong_phu);
                        }
                        //phu - 3000000
                        var _lstphu3 = _lstThucHienPTTT.Where(o => (o.phumo1 == _itemBS.userhisid || o.phumo2 == _itemBS.userhisid) && o.dongia == 3000000).ToList();
                        if (_lstphu3 != null && _lstphu3.Count > 0)
                        {
                            _bacsiBC.phu_sl3 = _lstphu3.Sum(s => s.soluong_phu);
                        }
                        //phu - 5000000
                        var _lstphu5 = _lstThucHienPTTT.Where(o => (o.phumo1 == _itemBS.userhisid || o.phumo2 == _itemBS.userhisid) && o.dongia == 5000000).ToList();
                        if (_lstphu5 != null && _lstphu5.Count > 0)
                        {
                            _bacsiBC.phu_sl5 = _lstphu5.Sum(s => s.soluong_phu);
                        }
                        //------
                        _bacsiBC.mochinh_tien = (_bacsiBC.mochinh_sl2 * 2000000) + (_bacsiBC.mochinh_sl3 * 3000000) + (_bacsiBC.mochinh_sl5 * 5000000);
                        _bacsiBC.mochinh_thue2 = _bacsiBC.mochinh_tien * (decimal)0.02;
                        _bacsiBC.mochinh_sauthue= (_bacsiBC.mochinh_tien - _bacsiBC.mochinh_thue2)* (decimal)0.25;
                        _bacsiBC.phu_tien = (_bacsiBC.phu_sl2 * 2000000) + (_bacsiBC.phu_sl3 * 3000000) + (_bacsiBC.phu_sl5 * 5000000);
                        _bacsiBC.phu_thue2 = _bacsiBC.phu_tien * (decimal)0.02;
                        _bacsiBC.phu_sauthue = (_bacsiBC.phu_tien - _bacsiBC.phu_thue2) * (decimal)0.05;
                        _bacsiBC.thuclinh = _bacsiBC.mochinh_sauthue + _bacsiBC.phu_sauthue;

                        if (_bacsiBC.thuclinh > 0)
                        {
                            this.lstResultBC.Add(_bacsiBC);
                        }
                    }
                }
                gridControlDataBC.DataSource = this.lstResultBC;
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        #endregion


    }
}
