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
                string lstdepartmentid = " and vpm.departmentid in (" + this.lstPhongChonLayBC + ")";
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                SQLLayDuLieuBaoCao(lstdepartmentid);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }


        private void SQLLayDuLieuBaoCao(string lstdepartmentid)
        {
            try
            {
                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                string sqlBaoCao_RaVienDaTT = "SELECT count(vpm.*) as raviendatt_slbn, COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0),0) as raviendatt_tienkb, COALESCE(round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0),0) as raviendatt_tienxn, COALESCE(round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0),0) as raviendatt_tiencdhatdcn, COALESCE(round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0),0) as raviendatt_tienpttt, COALESCE(round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0),0) as raviendatt_tiendvktc, COALESCE(round(cast(sum(vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as numeric),0),0) as raviendatt_tiengiuongthuong, COALESCE(round(cast(sum(vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as numeric),0),0) as raviendatt_tiengiuongyeucau, COALESCE(round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0),0) as raviendatt_tienkhac, COALESCE(round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0),0) as raviendatt_tienvattu, COALESCE(round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0),0) as raviendatt_tienmau, COALESCE(round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0),0) as raviendatt_tienthuoc, COALESCE(round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0),0) as raviendatt_tongtien, COALESCE(round(cast(sum(vpm.tam_ung) as numeric),0),0) as raviendatt_tamung FROM vienphi_money vpm WHERE vpm.vienphistatus_vp=1 " + lstdepartmentid + " and vpm.duyet_ngayduyet_vp >= '" + this.thoiGianTu + "' and vpm.duyet_ngayduyet_vp <= '" + this.thoiGianDen + "';";

                DataView dataBCTongTheKhoa_RaVienDaTT = new DataView(condb.getDataTable(sqlBaoCao_RaVienDaTT));

                chartControlDTKhoa.Series[0].Points.Clear();
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Khám bệnh", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienkb"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Xét nghiệm", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienxn"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("CĐHA-TDCN", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tiencdhatdcn"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("PTTT", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienpttt"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("DC KTC", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tiendvktc"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Giường thường", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tiengiuongthuong"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Giường yêu cầu", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tiengiuongyeucau"].ToString())));
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
