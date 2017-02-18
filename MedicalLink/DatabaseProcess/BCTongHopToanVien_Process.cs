using MedicalLink.ClassCommon;
using MedicalLink.DatabaseProcess.FilterDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.DatabaseProcess
{
    public class BCTongHopToanVien_Process
    {
        private static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        internal static List<BCDashboardTongHopToanVien> BCTongHopToanVien_ChayMoi(BCDashboardTongHopToanVienFilter filter)
        {
            List<BCDashboardTongHopToanVien> lstdataBCTHToanVien = new List<BCDashboardTongHopToanVien>();
            try
            {
                string sqlLayBaoCao = "SELECT (case vpm.loaivienphiid when 1 then 'Ngoại trú' when 0 then 'Nội trú' else '' end) as loaivienphi, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, sum(vpm.money_khambenh_bh+vpm.money_xetnghiem_bh+vpm.money_cdha_bh+vpm.money_tdcn_bh+vpm.money_pttt_bh+vpm.money_dvktc_bh+vpm.money_giuong_bh+vpm.money_phuthu_bh+vpm.money_vanchuyen_bh+vpm.money_khac_bh+vpm.money_mau_bh+vpm.money_thuoc_bh+vpm.money_vattu_bh +vpm.money_khambenh_vp+vpm.money_xetnghiem_vp+vpm.money_cdha_vp+vpm.money_tdcn_vp+vpm.money_pttt_vp+vpm.money_dvktc_vp+vpm.money_giuong_vp+vpm.money_phuthu_vp+vpm.money_vanchuyen_vp+vpm.money_khac_vp+vpm.money_mau_vp+vpm.money_thuoc_vp+vpm.money_vattu_vp) as tien_tong, sum(vpm.tam_ung) as tam_ung FROM vienphi_money vpm WHERE vpm.vienphistatus_vp=1 and vpm.duyet_ngayduyet_vp>='" + filter.dateTu + "' and vpm.duyet_ngayduyet_vp<='" + filter.dateDen + "' and vpm.vienphidate_ravien<='" + filter.dateDen + "' GROUP BY vpm.loaivienphiid;";
                DataView BCTHToanVien = new DataView(condb.getDataTable(sqlLayBaoCao));
                if (BCTHToanVien != null && BCTHToanVien.Count > 0)
                {
                    long slbn_bh_tong = 0;
                    long slbn_vp_tong = 0;
                    decimal tien_tong_tong = 0;
                    decimal tam_ung_tong = 0;
                    for (int i = 0; i < BCTHToanVien.Count; i++)
                    {
                        BCDashboardTongHopToanVien bcBNTHToanVien = new BCDashboardTongHopToanVien();
                        bcBNTHToanVien.stt = i + 1;
                        bcBNTHToanVien.loaivienphi = BCTHToanVien[i]["loaivienphi"].ToString();
                        bcBNTHToanVien.slbn_bh = Utilities.Util_TypeConvertParse.ToInt64(BCTHToanVien[i]["slbn_bh"].ToString());
                        bcBNTHToanVien.slbn_vp = Utilities.Util_TypeConvertParse.ToInt64(BCTHToanVien[i]["slbn_vp"].ToString());
                        bcBNTHToanVien.slbn_tong = bcBNTHToanVien.slbn_bh + bcBNTHToanVien.slbn_vp;
                        bcBNTHToanVien.tien_tong = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["tien_tong"].ToString());
                        bcBNTHToanVien.tam_ung = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["tam_ung"].ToString());
                        lstdataBCTHToanVien.Add(bcBNTHToanVien);

                        slbn_bh_tong += bcBNTHToanVien.slbn_bh;
                        slbn_vp_tong += bcBNTHToanVien.slbn_vp;
                        tien_tong_tong += bcBNTHToanVien.tien_tong;
                        tam_ung_tong += bcBNTHToanVien.tam_ung;
                    }
                    BCDashboardTongHopToanVien bcBNTHToanVien_Tong = new BCDashboardTongHopToanVien();
                    bcBNTHToanVien_Tong.stt = 3;
                    bcBNTHToanVien_Tong.loaivienphi = "Tổng";
                    bcBNTHToanVien_Tong.slbn_bh = slbn_bh_tong;
                    bcBNTHToanVien_Tong.slbn_vp = slbn_vp_tong;
                    bcBNTHToanVien_Tong.slbn_tong = slbn_bh_tong + slbn_vp_tong;
                    bcBNTHToanVien_Tong.tien_tong = tien_tong_tong;
                    bcBNTHToanVien_Tong.tam_ung = tam_ung_tong;
                    lstdataBCTHToanVien.Add(bcBNTHToanVien_Tong);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            return lstdataBCTHToanVien;
        }

        //internal static void BCTongHopToanVien_TuDongChay(BCDashboardTongHopToanVienFilter filter)
        //{
        //    try
        //    {
        //        String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //        string departmentgroupid = "";
        //        string loaivienphiid = "";

        //        if (filter.departmentgroupid != 0)
        //        {
        //            departmentgroupid = " and vpm.departmentgroupid='" + filter.departmentgroupid + "' ";
        //        }

        //        string updateTmp = "";

        //        string sqlxoadulieuTmp = "DELETE FROM tools_dangdt_tmp vpm WHERE dangdt_date < '" + datetimeNow + "' and loaibaocao='" + filter.loaiBaoCao + "' and khoangdl_tu='" + filter.dateKhoangDLTu + "' " + departmentgroupid + " and chaytudong=" + filter.chayTuDong + " ;";

        //        condb.ExecuteNonQuery(updateTmp);
        //        condb.ExecuteNonQuery(sqlxoadulieuTmp);
        //    }
        //    catch (Exception ex)
        //    {
        //        MedicalLink.Base.Logging.Error(ex);
        //    }
        //}






    }
}
