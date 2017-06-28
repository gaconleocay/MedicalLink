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
                string lstdepartmentid = " and spt.departmentid in (" + this.lstPhongChonLayBC + ")";
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

                string sqlBaoCao_RaVienDaTT = "SELECT COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienkb,0)) as numeric),0),0) as raviendatt_tienkb,  COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienxn,0)) as numeric),0),0) as raviendatt_tienxn,  COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiencdhatdcn,0)) as numeric),0),0) as raviendatt_tiencdhatdcn,  COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienpttt,0)) as numeric),0),0) as raviendatt_tienpttt,  COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiendvktc,0)) as numeric),0),0) as raviendatt_tiendvktc,  COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiengiuongthuong,0)) as numeric),0),0) as raviendatt_tiengiuongthuong,  COALESCE(round(cast(sum(COALESCE(A.raviendatt_tiengiuongyeucau,0)) as numeric),0),0) as raviendatt_tiengiuongyeucau,  COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienkhac,0)) as numeric),0),0) as raviendatt_tienkhac,  COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienvattu,0)) as numeric),0),0) as raviendatt_tienvattu,  COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienmau,0)) as numeric),0),0) as raviendatt_tienmau,  COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienthuoc,0)) as numeric),0),0) as raviendatt_tienthuoc,  COALESCE(round(cast(sum(COALESCE(A.raviendatt_tienkb,0) + COALESCE(A.raviendatt_tienxn,0) + COALESCE(A.raviendatt_tiencdhatdcn,0) + COALESCE(A.raviendatt_tienpttt,0) + COALESCE(A.raviendatt_tiendvktc,0) + COALESCE(A.raviendatt_tiengiuongthuong,0) + COALESCE(A.raviendatt_tiengiuongyeucau,0) + COALESCE(A.raviendatt_tienkhac,0) + COALESCE(A.raviendatt_tienvattu,0) + COALESCE(A.raviendatt_tienmau,0) + COALESCE(A.raviendatt_tienthuoc,0)) as numeric),0),0) as raviendatt_tongtien   FROM (select spt.vienphiid,  sum(spt.money_khambenh_bh + spt.money_khambenh_vp) as raviendatt_tienkb,  sum(spt.money_xetnghiem_bh + spt.money_xetnghiem_vp) as raviendatt_tienxn,  sum(spt.money_cdha_bh + spt.money_cdha_vp + spt.money_tdcn_bh + spt.money_tdcn_vp) as raviendatt_tiencdhatdcn,  sum(spt.money_pttt_bh + spt.money_pttt_vp) as raviendatt_tienpttt,  sum(spt.money_dvktc_bh + spt.money_dvktc_vp) as raviendatt_tiendvktc,  sum(spt.money_giuongthuong_bh + spt.money_giuongthuong_vp) as raviendatt_tiengiuongthuong,  sum(spt.money_giuongyeucau_bh + spt.money_giuongyeucau_vp) as raviendatt_tiengiuongyeucau,  sum(spt.money_nuocsoi_bh + spt.money_nuocsoi_vp + spt.money_xuatan_bh + spt.money_xuatan_vp + spt.money_diennuoc_bh + spt.money_diennuoc_vp + spt.money_vanchuyen_bh + spt.money_vanchuyen_vp + spt.money_khac_bh + spt.money_khac_vp + spt.money_phuthu_bh + spt.money_phuthu_vp) as raviendatt_tienkhac,  sum(spt.money_vattu_bh + spt.money_vattu_vp + spt.money_vtthaythe_bh + spt.money_vtthaythe_vp) as raviendatt_tienvattu,  sum(spt.money_mau_bh + spt.money_mau_vp) as raviendatt_tienmau,  sum(spt.money_thuoc_bh + spt.money_thuoc_vp + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_bh + spt.money_dkpttt_vattu_vp) as raviendatt_tienthuoc from tools_serviceprice_pttt spt where spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' " + lstdepartmentid + " group by spt.vienphiid) A;";


                DataView dataBCTongTheKhoa_RaVienDaTT = new DataView(condb.GetDataTable_HIS(sqlBaoCao_RaVienDaTT));

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
