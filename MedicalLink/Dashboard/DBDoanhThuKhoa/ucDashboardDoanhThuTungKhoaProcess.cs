using DevExpress.XtraCharts;
using DevExpress.XtraSplashScreen;
using MedicalLink.ClassCommon;
using MedicalLink.DatabaseProcess.FilterDTO;
using MedicalLink.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard
{
    public partial class ucDashboardDoanhThuTungKhoa : UserControl
    {
        private DangDTRaVienChuaDaTTFilterDTO filter { get; set; }
        private void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                filter = new DangDTRaVienChuaDaTTFilterDTO();
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                filter.loaiBaoCao = "REPORT_08";
                filter.dateTu = this.thoiGianTu;
                filter.dateDen = this.thoiGianDen;
                filter.dateKhoangDLTu = this.KhoangThoiGianLayDuLieu;
                filter.departmentgroupid = Convert.ToInt16(cboKhoa.EditValue);
                filter.loaivienphiid = 0;
                filter.chayTuDong = 0;

                //t4.Start();
                //DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_DangDT_Tmp(filter);
                //DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienChuaTT_Tmp(filter);
                DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienDaTT_Tmp(filter);
                SQLLayDuLieuBaoCao();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }


        private void SQLLayDuLieuBaoCao()
        {
            try
            {
                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

              //  string sqlBaoCao_DangDT = "(SELECT departmentgroupid, bn_chuyendi, bn_chuyenden, ravien_slbn, dangdt_slbn_bh, dangdt_slbn_vp, dangdt_slbn, dangdt_tienkb, dangdt_tienxn, dangdt_tiencdhatdcn, dangdt_tienpttt, dangdt_tiendvktc, dangdt_tiengiuong, dangdt_tienkhac, dangdt_tienvattu, dangdt_tienmau, dangdt_tienthuoc_bhyt, dangdt_tienthuoc_vp, dangdt_tienthuoc, dangdt_tongtien_bhyt, dangdt_tongtien_vp, dangdt_tongtien, dangdt_tamung FROM tools_dangdt_tmp WHERE departmentgroupid = '" + Convert.ToInt32(cboKhoa.EditValue) + "' and loaibaocao='REPORT_08' and khoangdl_tu='" + KhoangThoiGianLayDuLieu + "' and chaytudong=0 ORDER BY dangdt_date DESC LIMIT 1) Union (select 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 from tools_dangdt_tmp  limit 1) order by departmentgroupid desc; ";
               // string sqlBaoCao_RaVienChuaTT = "(SELECT departmentgroupid,ravienchuatt_slbn_bh,ravienchuatt_slbn_vp,ravienchuatt_slbn,ravienchuatt_tienkb,ravienchuatt_tienxn,ravienchuatt_tiencdhatdcn,ravienchuatt_tienpttt,ravienchuatt_tiendvktc,ravienchuatt_tiengiuong,ravienchuatt_tienkhac,ravienchuatt_tienvattu,ravienchuatt_tienmau,ravienchuatt_tienthuoc_bhyt,ravienchuatt_tienthuoc_vp,ravienchuatt_tienthuoc,ravienchuatt_tongtien_bhyt,ravienchuatt_tongtien_vp,ravienchuatt_tongtien,ravienchuatt_tamung FROM tools_ravienchuatt_tmp WHERE departmentgroupid = '" + Convert.ToInt32(cboKhoa.EditValue) + "' and loaibaocao='REPORT_08' and khoangdl_tu='" + KhoangThoiGianLayDuLieu + "' and chaytudong=0 ORDER BY ravienchuatt_date DESC LIMIT 1) Union (select 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 from tools_ravienchuatt_tmp  limit 1) order by departmentgroupid desc;";
                string sqlBaoCao_RaVienDaTT = "(SELECT departmentgroupid, raviendatt_slbn_bh, raviendatt_slbn_vp, raviendatt_slbn, raviendatt_tienkb, raviendatt_tienxn, raviendatt_tiencdhatdcn, raviendatt_tienpttt, raviendatt_tiendvktc, raviendatt_tiengiuong, raviendatt_tienkhac, raviendatt_tienvattu, raviendatt_tienmau, raviendatt_tienthuoc_bhyt, raviendatt_tienthuoc_vp, raviendatt_tienthuoc, raviendatt_tongtien_bhyt, raviendatt_tongtien_vp, raviendatt_tongtien, raviendatt_tamung FROM tools_raviendatt_tmp raviendatt WHERE raviendatt.departmentgroupid = '" + Convert.ToInt32(cboKhoa.EditValue) + "' and loaibaocao='REPORT_08' and chaytudong=0 ORDER BY raviendatt_date DESC LIMIT 1) Union (select 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 from tools_raviendatt_tmp  limit 1) order by departmentgroupid desc; ";

                //string sqlBNRaVienChuyenDiChuyenDen = "(SELECT 'BN ra vien' as name, count(vp.vienphiid) as ravien_slbn FROM vienphi vp WHERE vp.vienphistatus=1 and vp.vienphidate_ravien>='" + thoiGianTu + "' and vp.vienphidate_ravien<='" + thoiGianDen + "' and vp.departmentgroupid='" + (cboKhoa.EditValue) + "') Union (SELECT 'BN chuyen di' as name, count(A1.vienphiid) as bn_chuyendi FROM( SELECT DISTINCT (mrd.vienphiid) FROM medicalrecord mrd  WHERE mrd.departmentgroupid='" + (cboKhoa.EditValue) + "' and mrd.hinhthucravienid=8 and mrd.thoigianravien>='" + thoiGianTu + "' and mrd.thoigianravien<='" + thoiGianDen + "') A1 ) Union (SELECT 'BN chuyen den' as name, count(A2.vienphiid) as bn_chuyenden FROM( SELECT DISTINCT (mrd.vienphiid) FROM medicalrecord mrd  WHERE mrd.departmentgroupid='" + (cboKhoa.EditValue) + "' and mrd.hinhthucvaovienid=3 and mrd.thoigianravien>='" + thoiGianTu + "' and mrd.thoigianravien<='" + thoiGianDen + "') A2);";

                //string sqlDoanhThu = "(SELECT serde.departmentgroupid,vp.vienphiid, sum(case when vp.doituongbenhnhanid=1 then 1 else 0 end) as doanhthu_slbn_bh, sum(case when vp.doituongbenhnhanid<>1 then 1 else 0 end) as doanhthu_slbn_vp, count(vp.*) as doanhthu_slbn, round(cast(sum(serde.money_khambenh_bh + serde.money_khambenh_vp) as numeric),0) as doanhthu_tienkb, round(cast(sum(serde.money_xetnghiem_bh + serde.money_xetnghiem_vp) as numeric),0) as doanhthu_tienxn, round(cast(sum(serde.money_cdha_bh + serde.money_cdha_vp + serde.money_tdcn_bh + serde.money_tdcn_vp) as numeric),0) as doanhthu_tiencdhatdcn, round(cast(sum(serde.money_pttt_bh + serde.money_pttt_vp) as numeric),0) as doanhthu_tienpttt, round(cast(sum(serde.money_dvktc_bh + serde.money_dvktc_vp) as numeric),0) as doanhthu_tiendvktc, round(cast(sum(serde.money_giuong_bh + serde.money_giuong_vp) as numeric),0) as doanhthu_tiengiuong, round(cast(sum(serde.money_khac_bh + serde.money_khac_vp + serde.money_phuthu_bh + serde.money_phuthu_vp + serde.money_vanchuyen_bh + serde.money_vanchuyen_vp) as numeric),0) as doanhthu_tienkhac, round(cast(sum(serde.money_vattu_bh + serde.money_vattu_vp) as numeric),0) as doanhthu_tienvattu, round(cast(sum(serde.money_mau_bh + serde.money_mau_vp) as numeric),0) as doanhthu_tienmau, round(cast(sum(serde.money_thuoc_bh) as numeric),0) as doanhthu_tienthuoc_bhyt,round(cast(sum(serde.money_thuoc_vp) as numeric),0) as doanhthu_tienthuoc_vp,round(cast(sum(serde.money_thuoc_bh + serde.money_thuoc_vp) as numeric),0) as doanhthu_tienthuoc,round(cast(sum(serde.money_khambenh_bh + serde.money_xetnghiem_bh + serde.money_cdha_bh + serde.money_tdcn_bh + serde.money_pttt_bh + serde.money_dvktc_bh + serde.money_giuong_bh + serde.money_khac_bh + serde.money_phuthu_bh + serde.money_vanchuyen_bh + serde.money_thuoc_bh + serde.money_mau_bh + serde.money_vattu_bh) as numeric),0) as doanhthu_tongtien_bhyt,round(cast(sum(serde.money_khambenh_vp + serde.money_xetnghiem_vp + serde.money_cdha_vp + serde.money_tdcn_vp + serde.money_pttt_vp + serde.money_dvktc_vp + serde.money_giuong_vp + serde.money_khac_vp + serde.money_phuthu_vp + serde.money_vanchuyen_vp + serde.money_thuoc_vp + serde.money_mau_vp + serde.money_vattu_vp) as numeric),0) as doanhthu_tongtien_vp, round(cast(sum(serde.money_khambenh_bh + serde.money_xetnghiem_bh + serde.money_cdha_bh + serde.money_tdcn_bh + serde.money_pttt_bh + serde.money_dvktc_bh + serde.money_giuong_bh + serde.money_khac_bh + serde.money_phuthu_bh + serde.money_vanchuyen_bh + serde.money_thuoc_bh + serde.money_mau_bh + serde.money_vattu_bh + serde.money_khambenh_vp + serde.money_xetnghiem_vp + serde.money_cdha_vp + serde.money_tdcn_vp + serde.money_pttt_vp + serde.money_dvktc_vp + serde.money_giuong_vp + serde.money_khac_vp + serde.money_phuthu_vp + serde.money_vanchuyen_vp + serde.money_thuoc_vp + serde.money_mau_vp + serde.money_vattu_vp) as numeric),0) as doanhthu_tongtien FROM vienphi vp inner join serviceprice_department serde on vp.vienphiid=serde.vienphiid WHERE COALESCE(vp.vienphistatus_vp,0)=1 and vp.loaivienphiid=0 and vp.duyet_ngayduyet_vp >= '" + thoiGianTu + "' and vp.duyet_ngayduyet_vp <= '" + thoiGianDen + "' and serde.departmentgroupid='" + Convert.ToInt32(cboKhoa.EditValue) + "' GROUP BY vp.vienphiid,serde.departmentgroupid) UNION (select 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 from tools_raviendatt_tmp ) ORDER BY departmentgroupid desc;";
               // string sqlDoanhThu = "SELECT 0 as departmentgroupid,0 as vienphiid, 0 as doanhthu_slbn_bh, 0 as doanhthu_slbn_vp, 0 as doanhthu_slbn, 0 as doanhthu_tienkb, 0 as doanhthu_tienxn, 0 as doanhthu_tiencdhatdcn, 0 as doanhthu_tienpttt, 0 as doanhthu_tiendvktc, 0 as doanhthu_tiengiuong, 0 as doanhthu_tienkhac, 0 as doanhthu_tienvattu, 0 as doanhthu_tienmau, 0 as doanhthu_tienthuoc_bhyt,0 as doanhthu_tienthuoc_vp,0 as doanhthu_tienthuoc,0 as doanhthu_tongtien_bhyt,0 as doanhthu_tongtien_vp, 0 as doanhthu_tongtien from tools_raviendatt_tmp limit 1;";

                //DataView dataBCTongTheKhoa_DangDT = new DataView(condb.getDataTable(sqlBaoCao_DangDT));
               // DataView dataBCTongTheKhoa_RaVienChuaTT = new DataView(condb.getDataTable(sqlBaoCao_RaVienChuaTT));
                DataView dataBCTongTheKhoa_RaVienDaTT = new DataView(condb.getDataTable(sqlBaoCao_RaVienDaTT));
               // DataView dataBNRaVienChuyenDiChuyenDen = new DataView(condb.getDataTable(sqlBNRaVienChuyenDiChuyenDen));
              //  DataView dataDoanhThu = new DataView(condb.getDataTable(sqlDoanhThu));

                chartControlDTKhoa.Series[0].Points.Clear();
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Khám bệnh", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienkb"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Xét nghiệm", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienxn"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("CĐHA-TDCN", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tiencdhatdcn"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("PTTT", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienpttt"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("DC KTC", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tiendvktc"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Ngày giường", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tiengiuong"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Thuốc", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienthuoc"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Vật tư", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienvattu"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Máu", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienmau"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Khác", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienkhac"].ToString())));
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

    }
}
