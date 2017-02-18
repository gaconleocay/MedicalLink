using MedicalLink.DatabaseProcess.FilterDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.DatabaseProcess
{
    public class RaVienChuaTT_Tmp_Process
    {
        private static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();


        private static void SQLChay_RaVienChuaTT_Tmp(RaVienChuaTT_TmpFilterDTO RaVienChuaTTFilter)
        {
            try
            {
                String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string departmentgroupid = "";
                string loaivienphiid = "";

                if (RaVienChuaTTFilter.departmentgroupid != 0)
                {
                    departmentgroupid = " and vpm.departmentgroupid='" + RaVienChuaTTFilter.departmentgroupid + "' ";
                }
                if (RaVienChuaTTFilter.loaivienphiid > -1)
                {
                    loaivienphiid = " and vpm.loaivienphiid='" + RaVienChuaTTFilter.loaivienphiid + "' ";
                }

                string updateTmp = "INSERT INTO tools_ravienchuatt_tmp(departmentgroupid, ravienchuatt_slbn_bh, ravienchuatt_slbn_vp, ravienchuatt_slbn, ravienchuatt_tienkb, ravienchuatt_tienxn, ravienchuatt_tiencdhatdcn, ravienchuatt_tienpttt, ravienchuatt_tiendvktc, ravienchuatt_tiengiuong, ravienchuatt_tienkhac, ravienchuatt_tienvattu, ravienchuatt_tienmau, ravienchuatt_tienthuoc_bhyt, ravienchuatt_tienthuoc_vp, ravienchuatt_tienthuoc, ravienchuatt_tongtien_bhyt, ravienchuatt_tongtien_vp, ravienchuatt_tongtien, ravienchuatt_tamung, ravienchuatt_date, loaibaocao, khoangdl_tu) SELECT vpm.departmentgroupid, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as ravienchuatt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as ravienchuatt_slbn_vp, count(vpm.*) as ravienchuatt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as ravienchuatt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as ravienchuatt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as ravienchuatt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as ravienchuatt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as ravienchuatt_tiendvktc, round(cast(sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as ravienchuatt_tiengiuong, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as ravienchuatt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as ravienchuatt_tienmau,round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as ravienchuatt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as ravienchuatt_tienthuoc_vp, round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as ravienchuatt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as ravienchuatt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tongtien_vp,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as ravienchuatt_tamung, '" + datetimeNow + "' as ravienchuatt_date, '" + RaVienChuaTTFilter.loaiBaoCao + "', '" + RaVienChuaTTFilter.dateKhoangDLTu + "' FROM vienphi_money vpm WHERE COALESCE(vpm.vienphistatus_vp,0)=0 " + loaivienphiid + departmentgroupid + " and vpm.vienphistatus<>0 and vpm.vienphidate>='" + RaVienChuaTTFilter.dateKhoangDLTu + "' GROUP BY vpm.departmentgroupid;";

                string sqlxoadulieuTmp = "DELETE FROM tools_ravienchuatt_tmp WHERE ravienchuatt_date < '" + datetimeNow + "' and khoangdl_tu='" + RaVienChuaTTFilter.dateKhoangDLTu + "' and loaibaocao='" + RaVienChuaTTFilter.loaiBaoCao + "' ;";
                condb.ExecuteNonQuery(updateTmp);
                condb.ExecuteNonQuery(sqlxoadulieuTmp);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
    }
}
