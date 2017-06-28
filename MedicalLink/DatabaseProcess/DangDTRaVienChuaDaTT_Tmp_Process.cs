using MedicalLink.DatabaseProcess.FilterDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.DatabaseProcess
{
    public class DangDTRaVienChuaDaTT_Tmp_Process
    {
        private static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        internal static void SQLChay_RaVienDaTT_Tmp(DangDTRaVienChuaDaTTFilterDTO RaVienDaTTFilter)
        {
            try
            {
                String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string departmentgroupid = "";
                string loaivienphiid = "";
                string lstdepartmentid = "";
                string updateTmp = "";

                if (RaVienDaTTFilter.departmentgroupid != 0)
                {
                    departmentgroupid = " and vpm.departmentgroupid='" + RaVienDaTTFilter.departmentgroupid + "' ";
                }
                if (RaVienDaTTFilter.loaivienphiid > -1)
                {
                    loaivienphiid = " and vpm.loaivienphiid='" + RaVienDaTTFilter.loaivienphiid + "' ";
                }
                if (RaVienDaTTFilter.lstdepartmentid != null && RaVienDaTTFilter.lstdepartmentid != "")
                {
                    lstdepartmentid = " and vpm.departmentid in (" + RaVienDaTTFilter.lstdepartmentid + ") ";
                }

                if (lstdepartmentid != "") //Group theo phong
                {
                    updateTmp = "INSERT INTO tools_raviendatt_tmp(departmentgroupid, departmentid, raviendatt_slbn_bh, raviendatt_slbn_vp, raviendatt_slbn, raviendatt_tienkb, raviendatt_tienxn, raviendatt_tiencdhatdcn, raviendatt_tienpttt, raviendatt_tiendvktc, raviendatt_tiengiuongthuong,raviendatt_tiengiuongyeucau, raviendatt_tienkhac, raviendatt_tienvattu, raviendatt_tienmau, raviendatt_tienthuoc_bhyt, raviendatt_tienthuoc_vp, raviendatt_tienthuoc, raviendatt_tongtien_bhyt, raviendatt_tongtien_vp, raviendatt_tongtien, raviendatt_tamung, loaibaocao, raviendatt_date, chaytudong) SELECT vpm.departmentgroupid, vpm.departmentid, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as raviendatt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as raviendatt_slbn_vp, count(vpm.*) as raviendatt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as raviendatt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as raviendatt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as raviendatt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as raviendatt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as raviendatt_tiendvktc, round(cast(sum(vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as numeric),0) as raviendatt_tiengiuongthuong, round(cast(sum(vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as numeric),0) as raviendatt_tiengiuongyeucau, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as raviendatt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as raviendatt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as raviendatt_tienmau, round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as raviendatt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as raviendatt_tienthuoc_vp,round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as raviendatt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as raviendatt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as raviendatt_tongtien_vp, round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as raviendatt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as raviendatt_tamung, '" + RaVienDaTTFilter.loaiBaoCao + "', '" + datetimeNow + "' as raviendatt_date, " + RaVienDaTTFilter.chayTuDong + " FROM vienphi_money vpm WHERE COALESCE(vpm.vienphistatus_vp,0)=1 " + loaivienphiid + departmentgroupid + lstdepartmentid + " and vpm.duyet_ngayduyet_vp >= '" + RaVienDaTTFilter.dateTu + "' and vpm.duyet_ngayduyet_vp <= '" + RaVienDaTTFilter.dateDen + "' GROUP BY vpm.departmentgroupid, vpm.departmentid;";
                }
                else//Group theo khoa
                {
                    updateTmp = "INSERT INTO tools_raviendatt_tmp(departmentgroupid, raviendatt_slbn_bh, raviendatt_slbn_vp, raviendatt_slbn, raviendatt_tienkb, raviendatt_tienxn, raviendatt_tiencdhatdcn, raviendatt_tienpttt, raviendatt_tiendvktc, raviendatt_tiengiuongthuong,raviendatt_tiengiuongyeucau, raviendatt_tienkhac, raviendatt_tienvattu, raviendatt_tienmau, raviendatt_tienthuoc_bhyt, raviendatt_tienthuoc_vp, raviendatt_tienthuoc, raviendatt_tongtien_bhyt, raviendatt_tongtien_vp, raviendatt_tongtien, raviendatt_tamung, loaibaocao, raviendatt_date, chaytudong) SELECT vpm.departmentgroupid, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as raviendatt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as raviendatt_slbn_vp, count(vpm.*) as raviendatt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as raviendatt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as raviendatt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as raviendatt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as raviendatt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as raviendatt_tiendvktc, round(cast(sum(vpm.money_giuongthuong_bh + vpm.money_giuongthuong_vp) as numeric),0) as raviendatt_tiengiuongthuong, round(cast(sum(vpm.money_giuongyeucau_bh + vpm.money_giuongyeucau_vp) as numeric),0) as raviendatt_tiengiuongyeucau, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as raviendatt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as raviendatt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as raviendatt_tienmau, round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as raviendatt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as raviendatt_tienthuoc_vp,round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as raviendatt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as raviendatt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as raviendatt_tongtien_vp, round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuongthuong_bh + vpm.money_giuongyeucau_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuongthuong_vp + vpm.money_giuongyeucau_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as raviendatt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as raviendatt_tamung, '" + RaVienDaTTFilter.loaiBaoCao + "', '" + datetimeNow + "' as raviendatt_date, " + RaVienDaTTFilter.chayTuDong + " FROM vienphi_money vpm WHERE COALESCE(vpm.vienphistatus_vp,0)=1 " + loaivienphiid + departmentgroupid + " and vpm.duyet_ngayduyet_vp >= '" + RaVienDaTTFilter.dateTu + "' and vpm.duyet_ngayduyet_vp <= '" + RaVienDaTTFilter.dateDen + "' GROUP BY vpm.departmentgroupid;";
                }
                string sqlxoadulieuTmp = "DELETE FROM tools_RaVienDaTT_Tmp WHERE raviendatt_date < '" + datetimeNow + "' and loaibaocao='" + RaVienDaTTFilter.loaiBaoCao + "' and chaytudong=" + RaVienDaTTFilter.chayTuDong + " ;";

                condb.ExecuteNonQuery_HIS(updateTmp);
                condb.ExecuteNonQuery_HIS(sqlxoadulieuTmp);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }



    }
}
