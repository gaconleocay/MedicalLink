﻿using System;
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
    public partial class BC102_TrichThuongChuyenGiaYC : UserControl
    {
        #region Khai bao
        private Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private List<TrichThuongChuyenGiaYCDTO> lstBaoCao { get; set; }
        private string DanhMucDichVu_String { get; set; }
        private decimal TongTienChi = 0;
        #endregion

        public BC102_TrichThuongChuyenGiaYC()
        {
            InitializeComponent();
        }

        #region Load
        private void ucBC102_TrichThuongChuyenGiaYC_Load(object sender, EventArgs e)
        {
            try
            {
                dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
                //load danh muc dich vu
                List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_102_DV").ToList();
                if (lstOtherList != null && lstOtherList.Count > 0)
                {
                    for (int i = 0; i < lstOtherList.Count - 1; i++)
                    {
                        this.DanhMucDichVu_String += lstOtherList[i].tools_otherlistvalue + ",";
                    }
                    this.DanhMucDichVu_String += lstOtherList[lstOtherList.Count - 1].tools_otherlistvalue;
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
                string tieuchi_ser = "";
                string tieuchi_vp = "";
                string lstdichvu_ser = " and servicepricecode in (" + this.DanhMucDichVu_String + ") ";
                string _khoaChiDinh = " and departmentgroupid=46 ";
                string trangthai_vp = "";
                string sql_timkiem = "";
                string tieuchi_mbp = "";

                string datetungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string datedenngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                if (cboTieuChi.Text == "Theo ngày chỉ định")
                {
                    tieuchi_ser = " and servicepricedate between '" + datetungay + "' and '" + datedenngay + "' ";
                    tieuchi_mbp = " and maubenhphamdate between '" + datetungay + "' and '" + datedenngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày vào viện")
                {
                    tieuchi_vp = " and vienphidate between '" + datetungay + "' and '" + datedenngay + "' ";
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

                sql_timkiem = @"SELECT (row_number() OVER (PARTITION BY ncd.nhom_bcid ORDER BY ncd.username)) as stt, mbp.userthuchien as userhisid, ncd.usercode, ncd.username, coalesce(ncd.nhom_bcid,0) as nhom_bcid, ncd.nhom_bcten, sum(ser.soluong) as soluong, sum(ser.soluong*ser.dongia) as thanhtien, 50 as tylehuong, sum(ser.soluong*ser.dongia*0.5) as tongtien, 0 as tienthue, 0 as thuclinh, '' as kynhan, 0 as isgroup FROM (select maubenhphamid,userid,maubenhphamstatus,(select pk.userid from sothutuphongkham pk where pk.medicalrecordid=m.medicalrecordid order by sothutuid limit 1) as userthuchien from maubenhpham m where maubenhphamgrouptype=2 " + tieuchi_mbp + _khoaChiDinh + ") mbp inner join (select maubenhphamid,vienphiid,soluong,(case when doituongbenhnhanid=4 then servicepricemoney_nuocngoai else (case when loaidoituong=0 then servicepricemoney_bhyt when loaidoituong=1 then servicepricemoney_nhandan else servicepricemoney end) end) as dongia from serviceprice where bhyt_groupcode='01KB' and (case when loaidoituong>0 then billid_thutien>0 or billid_clbh_thutien>0 end) " + lstdichvu_ser + tieuchi_ser + _khoaChiDinh + ") ser on ser.maubenhphamid=mbp.maubenhphamid inner join (select vienphiid,vienphistatus from vienphi where 1=1 " + tieuchi_vp + trangthai_vp + ") vp on vp.vienphiid=ser.vienphiid inner join (select userhisid,usercode,username,nhom_bcid,nhom_bcten from nhompersonnel group by userhisid,usercode,username,nhom_bcid,nhom_bcten) ncd on ncd.userhisid=mbp.userthuchien WHERE mbp.maubenhphamstatus=16 or vp.vienphistatus<>0 GROUP BY mbp.userthuchien,ncd.usercode,ncd.username,ncd.nhom_bcid,ncd.nhom_bcten;";

                DataTable _dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (_dataBaoCao != null && _dataBaoCao.Rows.Count > 0)
                {
                    XuLyDuLieuVaHienThi(_dataBaoCao);
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

        private void XuLyDuLieuVaHienThi(DataTable _dataBaocao)
        {
            try
            {
                this.lstBaoCao = new List<TrichThuongChuyenGiaYCDTO>();
                List<TrichThuongChuyenGiaYCDTO> _lstBaoCao = Utilities.DataTables.DataTableToList<TrichThuongChuyenGiaYCDTO>(_dataBaocao);

                //NV hop dong
                List<TrichThuongChuyenGiaYCDTO> _lst_HopDong = _lstBaoCao.Where(o => o.nhom_bcid == 1).ToList();
                TrichThuongChuyenGiaYCDTO _item_HopDong = new TrichThuongChuyenGiaYCDTO();
                int _sum_soluong_hd = 0;
                decimal _sum_thanhtien_hd = 0;
                decimal _sum_tongtien_hd = 0;
                decimal _sum_tienthue_hd = 0;
                decimal _sum_thuclinh_hd = 0;
                foreach (var item_hd in _lst_HopDong)
                {
                    _sum_soluong_hd += item_hd.soluong ?? 0;
                    _sum_thanhtien_hd += item_hd.thanhtien ?? 0;
                    _sum_tongtien_hd += item_hd.tongtien ?? 0;
                    if (item_hd.tongtien >= 2000000)
                    {//Nếu tổng tiền của nhóm NV HĐ >=2.000.000đ sẽ tính 10% thuế
                        item_hd.tienthue = (item_hd.tongtien ?? 0) * (decimal)0.1;
                    }
                    item_hd.thuclinh = item_hd.tongtien - item_hd.tienthue;

                    _sum_tienthue_hd += item_hd.tienthue ?? 0;
                    _sum_thuclinh_hd += item_hd.thuclinh ?? 0;
                }
                _item_HopDong.stt = "I";
                _item_HopDong.username = "Nhân viên hợp đồng";
                _item_HopDong.soluong = _sum_soluong_hd;
                _item_HopDong.thanhtien = _sum_thanhtien_hd;
                _item_HopDong.tongtien = _sum_tongtien_hd;
                _item_HopDong.tienthue = _sum_tienthue_hd;
                _item_HopDong.thuclinh = _sum_thuclinh_hd;
                _item_HopDong.isgroup = 1;
                this.lstBaoCao.Add(_item_HopDong);
                this.lstBaoCao.AddRange(_lst_HopDong);
                //nhan vien benh vien
                List<TrichThuongChuyenGiaYCDTO> _lst_BenhVien = _lstBaoCao.Where(o => o.nhom_bcid != 1).ToList();
                TrichThuongChuyenGiaYCDTO _item_BenhVien = new TrichThuongChuyenGiaYCDTO();
                int _sum_soluong_bv = 0;
                decimal _sum_thanhtien_bv = 0;
                decimal _sum_tongtien_bv = 0;
                decimal _sum_tienthue_bv = 0;
                decimal _sum_thuclinh_bv = 0;
                foreach (var item_bv in _lst_BenhVien)
                {
                    _sum_soluong_bv += item_bv.soluong ?? 0;
                    _sum_thanhtien_bv += item_bv.thanhtien ?? 0;
                    _sum_tongtien_bv += item_bv.tongtien ?? 0;
                    item_bv.tienthue = 0;
                    item_bv.thuclinh = item_bv.tongtien ?? 0;

                    _sum_thuclinh_bv += item_bv.thuclinh ?? 0;
                }
                _item_BenhVien.stt = "II";
                _item_BenhVien.username = "Nhân viên bệnh viện";
                _item_BenhVien.soluong = _sum_soluong_bv;
                _item_BenhVien.thanhtien = _sum_thanhtien_bv;
                _item_BenhVien.tongtien = _sum_tongtien_bv;
                _item_BenhVien.tienthue = _sum_tienthue_bv;
                _item_BenhVien.thuclinh = _sum_thuclinh_bv;
                _item_BenhVien.isgroup = 1;
                this.lstBaoCao.Add(_item_BenhVien);
                this.lstBaoCao.AddRange(_lst_BenhVien);
                //tong cong
                TrichThuongChuyenGiaYCDTO _item_TongCong = new TrichThuongChuyenGiaYCDTO();
                _item_TongCong.username = "TỔNG CỘNG";
                _item_TongCong.soluong = _sum_soluong_hd + _sum_soluong_bv;
                _item_TongCong.thanhtien = _sum_thanhtien_hd + _sum_thanhtien_bv;
                _item_TongCong.tongtien = _sum_tongtien_hd + _sum_tongtien_bv;
                _item_TongCong.tienthue = _sum_tienthue_hd + _sum_tienthue_bv;
                _item_TongCong.thuclinh = _sum_thuclinh_hd + _sum_thuclinh_bv;
                _item_TongCong.isgroup = 1;
                this.lstBaoCao.Add(_item_TongCong);
                //Khoa khám bệnh theo yêu cầu
                TrichThuongChuyenGiaYCDTO _item_KhoaYC = new TrichThuongChuyenGiaYCDTO();
                _item_KhoaYC.username = "Khoa khám bệnh theo yêu cầu";
                _item_KhoaYC.tylehuong = 5;
                //_item_KhoaYC.thanhtien = _sum_thanhtien_hd + _sum_thanhtien_bv;
                _item_KhoaYC.tongtien = (_sum_thanhtien_hd + _sum_thanhtien_bv) * (decimal)0.05;
                // _item_KhoaYC.tienthue = _sum_tienthue_hd + _sum_tienthue_bv;
                _item_KhoaYC.thuclinh = _item_KhoaYC.tongtien;
                _item_KhoaYC.isgroup = 0;
                this.lstBaoCao.Add(_item_KhoaYC);
                //Tổng số tiền chi
                TrichThuongChuyenGiaYCDTO _item_TongTienChi = new TrichThuongChuyenGiaYCDTO();
                _item_TongTienChi.username = "Tổng số tiền chi";
                //_item_TongTienChi.tylehuong = 5;
                //_item_TongTienChi.thanhtien = _sum_thanhtien_hd + _sum_thanhtien_bv;
                //_item_TongTienChi.tongtien = (_sum_thanhtien_hd + _sum_thanhtien_bv) * (decimal)0.05;
                // _item_TongTienChi.tienthue = _sum_tienthue_hd + _sum_tienthue_bv;
                _item_TongTienChi.thuclinh = _item_TongCong.thuclinh + _item_KhoaYC.thuclinh;
                _item_TongTienChi.isgroup = 1;
                this.lstBaoCao.Add(_item_TongTienChi);

                this.TongTienChi = _item_TongTienChi.thuclinh ?? 0;
                gridControlDataBC.DataSource = this.lstBaoCao;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
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
                ClassCommon.reportExcelDTO item_phong = new ClassCommon.reportExcelDTO();
                item_phong.name = Base.bienTrongBaoCao.LST_DICHVU;
                item_phong.value = this.DanhMucDichVu_String;
                thongTinThem.Add(item_phong);
                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TONGTHUCLINH_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(this.TongTienChi, 0).ToString());
                thongTinThem.Add(_item_tien_string);

                string fileTemplatePath = "BC_102_TrichThuongChuyenGiaYeuCau.xlsx";
                DataTable _dataBaocao = Utilities.DataTables.ListToDataTable(this.lstBaoCao);
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, _dataBaocao);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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
                ClassCommon.reportExcelDTO item_phong = new ClassCommon.reportExcelDTO();
                item_phong.name = Base.bienTrongBaoCao.LST_DICHVU;
                item_phong.value = this.DanhMucDichVu_String;
                thongTinThem.Add(item_phong);
                ClassCommon.reportExcelDTO _item_tien_string = new ClassCommon.reportExcelDTO();
                _item_tien_string.name = "TONGTHUCLINH_STRING";
                _item_tien_string.value = Utilities.Common.String.Convert.CurrencyToVneseString(Utilities.NumberConvert.NumberToNumberRoundAuto(this.TongTienChi, 0).ToString());
                thongTinThem.Add(_item_tien_string);

                string fileTemplatePath = "BC_102_TrichThuongChuyenGiaYeuCau.xlsx";
                DataTable _dataBaocao = Utilities.DataTables.ListToDataTable(this.lstBaoCao);
                Utilities.PrintPreview.PrintPreview_ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileTemplatePath, thongTinThem, _dataBaocao);
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

                int _isgroup = (int)view.GetRowCellValue(e.RowHandle, "isgroup");
                if (_isgroup == 1)
                {
                    e.Appearance.BackColor = Color.BlanchedAlmond;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                }

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
