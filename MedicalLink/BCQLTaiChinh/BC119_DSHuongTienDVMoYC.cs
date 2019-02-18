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
    public partial class BC119_DSHuongTienDVMoYC : UserControl
    {
        #region Khai bao
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        //private DataTable dataBaoCao { get; set; }
        private List<BacSiDTO> lstBacSi { get; set; }
        private List<BC119DSHuongTienDVMoYCDTO> lstResultBC = new List<BC119DSHuongTienDVMoYCDTO>();
        private string DanhMucDichVu_String { get; set; }

        #endregion

        public BC119_DSHuongTienDVMoYC()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC119_DSHuongTienDVMoYC_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                //load danh muc dich vu
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_119_DV").ToList();
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
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                this.lstResultBC = new List<BC119DSHuongTienDVMoYCDTO>();
                string tieuchi_ser = " and servicepricedate >= '2017-01-01 00:00:00' ";
                string tieuchi_vp = " and vienphidate >= '2017-01-01 00:00:00' ";
                //string tieuchi_mrd = "";
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
	ser.servicepricecode,
	pttt.phauthuatvien as mochinh,
	pttt.phumo1,
	pttt.phumo2,
	pttt.bacsigayme,
	pttt.phume as ktvphume,
	pttt.dungcuvien,
	pttt.phume2 as ddhoitinh,
	pttt.phumo3 as ktvhoitinh,
	pttt.dieuduong as ddhanhchinh,
	pttt.phumo4 as holy,
	ser.soluong,
	ser.dongia,
	(case when (pttt.phumo1>0 and pttt.phumo2>0) then (ser.soluong/2)
			else ser.soluong end) as soluong_phu
FROM 
	(select vienphiid,servicepriceid,phauthuatvien,phumo1,phumo2,phumo3,phumo4,bacsigayme,phume,phume2,dungcuvien,dieuduong from phauthuatthuthuat where 1=1 {tieuchi_pttt}) pttt
	inner join (select vienphiid,servicepriceid,servicepricecode,soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where 1=1 {lstdichvu_ser} {tieuchi_ser}) ser on ser.servicepriceid=pttt.servicepriceid
	inner join (select vienphiid from vienphi where 1=1 {tieuchi_vp} {trangthai_vp}) vp on vp.vienphiid=ser.vienphiid;";

                DataTable dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (dataBaoCao != null && dataBaoCao.Rows.Count > 0)
                {
                    XuLyVaHienThiBaoCao_TongHop(dataBaoCao);
                }
                else
                {
                    gridControlDataBC.DataSource = null;
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TONGTIEN_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTien(), 0).ToString());
                thongTinThem.Add(_item_tien_string);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                string fileTemplatePath = "BC_119_DSHuongTienDVMoYC.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_Common.Utilities.ThongBao.WaitForm_Wait));
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.BienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;
                thongTinThem.Add(reportitem);
                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TONGTIEN_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(TinhTongTien(), 0).ToString());
                thongTinThem.Add(_item_tien_string);

                DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewDataBC);

                string fileTemplatePath = "BC_119_DSHuongTienDVMoYC.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Warn(ex);
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
                O2S_Common.Logging.LogSystem.Warn(ex);
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
                 O2S_Common.Logging.LogSystem.Warn(ex);
            }
            return _result;
        }
        private void XuLyVaHienThiBaoCao_TongHop(DataTable _dataBC)
        {
            try
            {
                List<BC119ThucHienPTTTDTO> _lstThucHienPTTT = Utilities.DataTables.DataTableToList<BC119ThucHienPTTTDTO>(_dataBC);

                if (this.lstBacSi != null)
                {
                    foreach (var _itemBS in this.lstBacSi)
                    {
                        BC119DSHuongTienDVMoYCDTO _bacsiBC = new BC119DSHuongTienDVMoYCDTO();
                        _bacsiBC.stt = this.lstResultBC.Count + 1;
                        _bacsiBC.usercode = _itemBS.usercode;
                        _bacsiBC.username = _itemBS.username;
                        _bacsiBC.departmentgroupname = _itemBS.departmentgroupname;

                        //mochinh
                        _bacsiBC.mochinh_sltt = _lstThucHienPTTT.Where(o => o.mochinh == _itemBS.userhisid && o.servicepricecode == "U11620-4506").ToList().Sum(s => s.soluong);
                        _bacsiBC.mochinh_sll1 = _lstThucHienPTTT.Where(o => o.mochinh == _itemBS.userhisid && o.servicepricecode == "U11621-4524").ToList().Sum(s => s.soluong);
                        _bacsiBC.mochinh_sll2 = _lstThucHienPTTT.Where(o => o.mochinh == _itemBS.userhisid && o.servicepricecode == "U11622-4536").ToList().Sum(s => s.soluong);
                        _bacsiBC.mochinh_sll3 = _lstThucHienPTTT.Where(o => o.mochinh == _itemBS.userhisid && o.servicepricecode == "U11623-4610").ToList().Sum(s => s.soluong);
                        //phu
                        _bacsiBC.phu_sltt = _lstThucHienPTTT.Where(o => (o.phumo1 == _itemBS.userhisid || o.phumo2 == _itemBS.userhisid) && o.servicepricecode == "U11620-4506").ToList().Sum(s => s.soluong_phu);
                        _bacsiBC.phu_sll1 = _lstThucHienPTTT.Where(o => (o.phumo1 == _itemBS.userhisid || o.phumo2 == _itemBS.userhisid) && o.servicepricecode == "U11621-4524").ToList().Sum(s => s.soluong_phu);
                        _bacsiBC.phu_sll2 = _lstThucHienPTTT.Where(o => (o.phumo1 == _itemBS.userhisid || o.phumo2 == _itemBS.userhisid) && o.servicepricecode == "U11622-4536").ToList().Sum(s => s.soluong_phu);
                        _bacsiBC.phu_sll3 = _lstThucHienPTTT.Where(o => (o.phumo1 == _itemBS.userhisid || o.phumo2 == _itemBS.userhisid) && o.servicepricecode == "U11623-4610").ToList().Sum(s => s.soluong_phu);
                        //bacsigayme
                        _bacsiBC.bacsigayme_sltt = _lstThucHienPTTT.Where(o => o.bacsigayme == _itemBS.userhisid && o.servicepricecode == "U11620-4506").ToList().Sum(s => s.soluong);
                        _bacsiBC.bacsigayme_sll1 = _lstThucHienPTTT.Where(o => o.bacsigayme == _itemBS.userhisid && o.servicepricecode == "U11621-4524").ToList().Sum(s => s.soluong);
                        _bacsiBC.bacsigayme_sll2 = _lstThucHienPTTT.Where(o => o.bacsigayme == _itemBS.userhisid && o.servicepricecode == "U11622-4536").ToList().Sum(s => s.soluong);
                        _bacsiBC.bacsigayme_sll3 = _lstThucHienPTTT.Where(o => o.bacsigayme == _itemBS.userhisid && o.servicepricecode == "U11623-4610").ToList().Sum(s => s.soluong);
                        //ktvphume
                        _bacsiBC.ktvphume_sltt = _lstThucHienPTTT.Where(o => o.ktvphume == _itemBS.userhisid && o.servicepricecode == "U11620-4506").ToList().Sum(s => s.soluong);
                        _bacsiBC.ktvphume_sll1 = _lstThucHienPTTT.Where(o => o.ktvphume == _itemBS.userhisid && o.servicepricecode == "U11621-4524").ToList().Sum(s => s.soluong);
                        _bacsiBC.ktvphume_sll2 = _lstThucHienPTTT.Where(o => o.ktvphume == _itemBS.userhisid && o.servicepricecode == "U11622-4536").ToList().Sum(s => s.soluong);
                        _bacsiBC.ktvphume_sll3 = _lstThucHienPTTT.Where(o => o.ktvphume == _itemBS.userhisid && o.servicepricecode == "U11623-4610").ToList().Sum(s => s.soluong);
                        //dungcuvien
                        _bacsiBC.dungcuvien_sltt = _lstThucHienPTTT.Where(o => o.dungcuvien == _itemBS.userhisid && o.servicepricecode == "U11620-4506").ToList().Sum(s => s.soluong);
                        _bacsiBC.dungcuvien_sll1 = _lstThucHienPTTT.Where(o => o.dungcuvien == _itemBS.userhisid && o.servicepricecode == "U11621-4524").ToList().Sum(s => s.soluong);
                        _bacsiBC.dungcuvien_sll2 = _lstThucHienPTTT.Where(o => o.dungcuvien == _itemBS.userhisid && o.servicepricecode == "U11622-4536").ToList().Sum(s => s.soluong);
                        _bacsiBC.dungcuvien_sll3 = _lstThucHienPTTT.Where(o => o.dungcuvien == _itemBS.userhisid && o.servicepricecode == "U11623-4610").ToList().Sum(s => s.soluong);
                        //ddhoitinh
                        _bacsiBC.ddhoitinh_sltt = _lstThucHienPTTT.Where(o => o.ddhoitinh == _itemBS.userhisid && o.servicepricecode == "U11620-4506").ToList().Sum(s => s.soluong);
                        _bacsiBC.ddhoitinh_sll1 = _lstThucHienPTTT.Where(o => o.ddhoitinh == _itemBS.userhisid && o.servicepricecode == "U11621-4524").ToList().Sum(s => s.soluong);
                        _bacsiBC.ddhoitinh_sll2 = _lstThucHienPTTT.Where(o => o.ddhoitinh == _itemBS.userhisid && o.servicepricecode == "U11622-4536").ToList().Sum(s => s.soluong);
                        _bacsiBC.ddhoitinh_sll3 = _lstThucHienPTTT.Where(o => o.ddhoitinh == _itemBS.userhisid && o.servicepricecode == "U11623-4610").ToList().Sum(s => s.soluong);
                        //ktvhoitinh
                        _bacsiBC.ktvhoitinh_sltt = _lstThucHienPTTT.Where(o => o.ktvhoitinh == _itemBS.userhisid && o.servicepricecode == "U11620-4506").ToList().Sum(s => s.soluong);
                        _bacsiBC.ktvhoitinh_sll1 = _lstThucHienPTTT.Where(o => o.ktvhoitinh == _itemBS.userhisid && o.servicepricecode == "U11621-4524").ToList().Sum(s => s.soluong);
                        _bacsiBC.ktvhoitinh_sll2 = _lstThucHienPTTT.Where(o => o.ktvhoitinh == _itemBS.userhisid && o.servicepricecode == "U11622-4536").ToList().Sum(s => s.soluong);
                        _bacsiBC.ktvhoitinh_sll3 = _lstThucHienPTTT.Where(o => o.ktvhoitinh == _itemBS.userhisid && o.servicepricecode == "U11623-4610").ToList().Sum(s => s.soluong);
                        //ddhanhchinh
                        _bacsiBC.ddhanhchinh_sltt = _lstThucHienPTTT.Where(o => o.ddhanhchinh == _itemBS.userhisid && o.servicepricecode == "U11620-4506").ToList().Sum(s => s.soluong);
                        _bacsiBC.ddhanhchinh_sll1 = _lstThucHienPTTT.Where(o => o.ddhanhchinh == _itemBS.userhisid && o.servicepricecode == "U11621-4524").ToList().Sum(s => s.soluong);
                        _bacsiBC.ddhanhchinh_sll2 = _lstThucHienPTTT.Where(o => o.ddhanhchinh == _itemBS.userhisid && o.servicepricecode == "U11622-4536").ToList().Sum(s => s.soluong);
                        _bacsiBC.ddhanhchinh_sll3 = _lstThucHienPTTT.Where(o => o.ddhanhchinh == _itemBS.userhisid && o.servicepricecode == "U11623-4610").ToList().Sum(s => s.soluong);
                        //holy
                        _bacsiBC.holy_sltt = _lstThucHienPTTT.Where(o => o.holy == _itemBS.userhisid && o.servicepricecode == "U11620-4506").ToList().Sum(s => s.soluong);
                        _bacsiBC.holy_sll1 = _lstThucHienPTTT.Where(o => o.holy == _itemBS.userhisid && o.servicepricecode == "U11621-4524").ToList().Sum(s => s.soluong);
                        _bacsiBC.holy_sll2 = _lstThucHienPTTT.Where(o => o.holy == _itemBS.userhisid && o.servicepricecode == "U11622-4536").ToList().Sum(s => s.soluong);
                        _bacsiBC.holy_sll3 = _lstThucHienPTTT.Where(o => o.holy == _itemBS.userhisid && o.servicepricecode == "U11623-4610").ToList().Sum(s => s.soluong);
                        //thuclinh
                        _bacsiBC.thuclinh = (_bacsiBC.mochinh_sltt * 5000000) + (_bacsiBC.mochinh_sll1 * 1500000) + (_bacsiBC.mochinh_sll2 * 1300000) + (_bacsiBC.mochinh_sll3 * 1200000) + (_bacsiBC.phu_sltt * 1300000) + (_bacsiBC.phu_sll1 * 750000) + (_bacsiBC.phu_sll2 * 600000) + (_bacsiBC.phu_sll3 * 500000) + (_bacsiBC.bacsigayme_sltt * 500000) + (_bacsiBC.bacsigayme_sll1 * 400000) + (_bacsiBC.bacsigayme_sll2 * 350000) + (_bacsiBC.bacsigayme_sll3 * 325000) + (_bacsiBC.ktvphume_sltt * 200000) + (_bacsiBC.ktvphume_sll1 * 160000) + (_bacsiBC.ktvphume_sll2 * 140000) + (_bacsiBC.ktvphume_sll3 * 130000) + (_bacsiBC.dungcuvien_sltt * 200000) + (_bacsiBC.dungcuvien_sll1 * 160000) + (_bacsiBC.dungcuvien_sll2 * 140000) + (_bacsiBC.dungcuvien_sll3 * 130000) + (_bacsiBC.ddhoitinh_sltt * 30000) + (_bacsiBC.ddhoitinh_sll1 * 24000) + (_bacsiBC.ddhoitinh_sll2 * 21000) + (_bacsiBC.ddhoitinh_sll3 * 19500) + (_bacsiBC.ktvhoitinh_sltt * 30000) + (_bacsiBC.ktvhoitinh_sll1 * 24000) + (_bacsiBC.ktvhoitinh_sll2 * 21000) + (_bacsiBC.ktvhoitinh_sll3 * 19500) + (_bacsiBC.ddhanhchinh_sltt * 30000) + (_bacsiBC.ddhanhchinh_sll1 * 24000) + (_bacsiBC.ddhanhchinh_sll2 * 21000) + (_bacsiBC.ddhanhchinh_sll3 * 19500) + (_bacsiBC.holy_sltt * 10000) + (_bacsiBC.holy_sll1 * 8000) + (_bacsiBC.holy_sll2 * 7000) + (_bacsiBC.holy_sll3 * 6500);

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
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #endregion


    }
}
