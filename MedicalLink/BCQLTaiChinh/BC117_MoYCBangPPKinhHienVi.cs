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
    public partial class BC117_MoYCBangPPKinhHienVi : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        //private DataTable dataBaoCao { get; set; }
        private List<BacSiDTO> lstBacSi { get; set; }
        private List<BC117MoYCBangPPKinhHienViDTO> lstResultBC = new List<BC117MoYCBangPPKinhHienViDTO>();
        #endregion

        public BC117_MoYCBangPPKinhHienVi()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC117_MoYCBangPPKinhHienVi_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDuLieuMacDinh();
                LoadDanhSachBacSi();
                //LoadDanhSachDichVu();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadDuLieuMacDinh()
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                gridControlDataBC.Visible = true;
                gridControlDataBC.Dock = DockStyle.Fill;
                gridControlBNDetail.Visible = false;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
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
        //private void LoadDanhSachDichVu()
        //{
        //    try
        //    {
        //        //load danh muc dich vu
        //        //List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_115_DV").ToList();
        //        //if (lstOtherList != null && lstOtherList.Count > 0)
        //        //{
        //        //    for (int i = 0; i < lstOtherList.Count - 1; i++)
        //        //    {
        //        //        this.DanhMucDichVu_String += lstOtherList[i].tools_otherlistvalue + ",";
        //        //    }
        //        //    this.DanhMucDichVu_String += lstOtherList[lstOtherList.Count - 1].tools_otherlistvalue;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        MedicalLink.Base.Logging.Error(ex);
        //    }
        //}
        #endregion

        #region Tim kiem
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                this.lstResultBC = new List<BC117MoYCBangPPKinhHienViDTO>();
                string tieuchi_ser = " and servicepricedate >= '2017-01-01 00:00:00' ";
                string tieuchi_vp = " and vienphidate >= '2017-01-01 00:00:00' ";
                string tieuchi_pttt = " and phauthuatthuthuatdate>'2017-01-01 00:00:00' ";
                string trangthai_vp = "";

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

                if (radioXemTongHop.Checked)//tong hop
                {
                    string sql_timkiem = $@"SELECT
	vp.vienphiid,
	pttt.phauthuatvien,
	pttt.phumo1,
	pttt.phumo2,
	pttt.bacsigayme,
	pttt.phume,
	pttt.dungcuvien,
	ser.soluong,
	(case when (pttt.phumo1>0 and pttt.phumo2>0) then (ser.soluong/2)
			else ser.soluong end) as soluong_phu
FROM 
	(select vienphiid,servicepriceid,phauthuatvien,phumo1,phumo2,bacsigayme,phume,dungcuvien from phauthuatthuthuat where 1=1 {tieuchi_pttt}) pttt
	inner join (select vienphiid,servicepriceid,soluong from serviceprice where servicepricecode='U11970-3701' {tieuchi_ser}) ser on ser.servicepriceid=pttt.servicepriceid
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
                else//chi tiet = chua lam
                {
                    string _sql_timkiemCT = "";

                    DataTable _dataBCChiTiet = condb.GetDataTable_HIS(_sql_timkiemCT);
                    if (_dataBCChiTiet != null && _dataBCChiTiet.Rows.Count > 0)
                    {
                        gridControlBNDetail.DataSource = _dataBCChiTiet;
                    }
                    else
                    {
                        gridControlBNDetail.DataSource = null;
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                        frmthongbao.Show();
                    }
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
                if (radioXemTongHop.Checked)
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

                    string fileTemplatePath = "BC_117_MoYCBangPPKinhHienVi.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaoCao);
                }
                else
                {
                    DataTable _dataBaoCao = Utilities.GridControl.Util_GridcontrolConvert.ConvertGridControlToDataTable(gridViewBNDetail);
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelNotTemplate("DANH SÁCH BỆNH NHÂN CHỈ ĐỊNH DỊCH VỤ", _dataBaoCao);
                }
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

                string fileTemplatePath = "BC_117_MoYCBangPPKinhHienVi.xlsx";
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaoCao);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Events
        private void radioXemTongHop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXemTongHop.Checked)
                {
                    radioXemChiTiet.Checked = false;
                    gridControlDataBC.Visible = true;
                    gridControlDataBC.DataSource = null;
                    gridControlDataBC.Dock = DockStyle.Fill;
                    gridControlBNDetail.Visible = false;
                    btnPrint.Enabled = true;
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
                    gridControlBNDetail.Visible = true;
                    gridControlBNDetail.DataSource = null;
                    gridControlBNDetail.Dock = DockStyle.Fill;
                    gridControlDataBC.Visible = false;
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
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
                List<ThucHienPTTTDTO> _lstThucHienPTTT = Utilities.DataTables.DataTableToList<ThucHienPTTTDTO>(_dataBC);

                if (this.lstBacSi != null)
                {
                    foreach (var _itemBS in this.lstBacSi)
                    {
                        BC117MoYCBangPPKinhHienViDTO _bacsiBC = new BC117MoYCBangPPKinhHienViDTO();
                        _bacsiBC.stt = this.lstResultBC.Count + 1;
                        _bacsiBC.usercode = _itemBS.usercode;
                        _bacsiBC.username = _itemBS.username;
                        _bacsiBC.departmentgroupname = _itemBS.departmentgroupname;

                        //phauthuatvien
                        var _lstphauthuatvien = _lstThucHienPTTT.Where(o => o.phauthuatvien == _itemBS.userhisid).ToList();
                        if (_lstphauthuatvien != null && _lstphauthuatvien.Count > 0)
                        {
                            _bacsiBC.sl_mochinh = _lstphauthuatvien.Sum(s => s.soluong);
                            _bacsiBC.tien_mochinh = _bacsiBC.sl_mochinh * 800000;
                        }
                        //phu
                        var _lstphu = _lstThucHienPTTT.Where(o => o.phumo1 == _itemBS.userhisid || o.phumo2 == _itemBS.userhisid).ToList();
                        if (_lstphu != null && _lstphu.Count > 0)
                        {
                            _bacsiBC.sl_phu = _lstphu.Sum(s => s.soluong_phu);
                            _bacsiBC.tien_phu = _bacsiBC.sl_phu * 500000;
                        }
                        //bacsigayme
                        var _lstbacsigayme = _lstThucHienPTTT.Where(o => o.bacsigayme == _itemBS.userhisid).ToList();
                        if (_lstbacsigayme != null && _lstbacsigayme.Count > 0)
                        {
                            _bacsiBC.sl_bacsigayme = _lstbacsigayme.Sum(s => s.soluong);
                            _bacsiBC.tien_bacsigayme = _bacsiBC.sl_bacsigayme * 350000;
                        }
                        //ktvphume
                        var _lstktvphume = _lstThucHienPTTT.Where(o => o.phume == _itemBS.userhisid).ToList();
                        if (_lstktvphume != null && _lstktvphume.Count > 0)
                        {
                            _bacsiBC.sl_ktvphume = _lstktvphume.Sum(s => s.soluong);
                            _bacsiBC.tien_ktvphume = _bacsiBC.sl_ktvphume * 175000;
                        }
                        //dungcuvien
                        var _lstdungcuvien = _lstThucHienPTTT.Where(o => o.dungcuvien == _itemBS.userhisid).ToList();
                        if (_lstdungcuvien != null && _lstdungcuvien.Count > 0)
                        {
                            _bacsiBC.sl_dungcuvien = _lstdungcuvien.Sum(s => s.soluong);
                            _bacsiBC.tien_dungcuvien = _bacsiBC.sl_dungcuvien * 175000;
                        }
                        _bacsiBC.thuclinh = _bacsiBC.tien_mochinh + _bacsiBC.tien_phu + _bacsiBC.tien_bacsigayme + _bacsiBC.tien_ktvphume + _bacsiBC.tien_dungcuvien;

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
