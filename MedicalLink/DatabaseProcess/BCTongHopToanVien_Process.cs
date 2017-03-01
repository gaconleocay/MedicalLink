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
                string sqlLayBaoCao = "";
                //=0: theo khoa ra vien; =1: theo khoa chi dinh
                //=0: xem tong hop; =1: xem chi tiet theo khoa
                if (filter.tieuChi == 0 && filter.kieuXem == 0)//theo khoa ra vien + xem tong hop
                {
                    sqlLayBaoCao = "SELECT (case vpm.loaivienphiid when 1 then 'Ngoại trú' when 0 then 'Nội trú' else '' end) as loaivienphi, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as money_khambenh, sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as money_xetnghiem, sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as money_cdhatdcn, sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as money_pttt, sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as money_dvktc, sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as money_giuong, sum(vpm.money_mau_bh + vpm.money_mau_vp) as money_mau, sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as money_thuoc, sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as money_vattu, sum(vpm.money_phuthu_bh + vpm.money_phuthu_vp) as money_phuthu, sum(vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as money_vanchuyen, sum(vpm.money_khac_bh + vpm.money_khac_vp) as money_khac, sum(vpm.tam_ung) as tam_ung FROM vienphi_money vpm WHERE vpm.vienphistatus_vp=1 and vpm.duyet_ngayduyet_vp>='" + filter.dateTu + "' and vpm.duyet_ngayduyet_vp<='" + filter.dateDen + "' GROUP BY vpm.loaivienphiid;";
                }
                else if (filter.tieuChi == 0 && filter.kieuXem == 1) //Theo khoa ra vien + xem chi tiet tung khoa
                {
                    sqlLayBaoCao = "SELECT depg.departmentgroupname as loaivienphi, A.*  FROM departmentgroup depg  LEFT JOIN (SELECT vpm.departmentgroupid as departmentgroupid, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp, sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as money_khambenh, sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as money_xetnghiem, sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as money_cdhatdcn, sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as money_pttt, sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as money_dvktc, sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as money_giuong, sum(vpm.money_mau_bh + vpm.money_mau_vp) as money_mau, sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as money_thuoc, sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as money_vattu, sum(vpm.money_phuthu_bh + vpm.money_phuthu_vp) as money_phuthu, sum(vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as money_vanchuyen, sum(vpm.money_khac_bh + vpm.money_khac_vp) as money_khac, sum(vpm.tam_ung) as tam_ung FROM vienphi_money vpm WHERE vpm.vienphistatus_vp=1 and vpm.duyet_ngayduyet_vp>='" + filter.dateTu + "' and vpm.duyet_ngayduyet_vp<='" + filter.dateDen + "'  GROUP BY vpm.departmentgroupid) A ON depg.departmentgroupid=A.departmentgroupid  WHERE depg.departmentgrouptype in (1,4,11,10)  ORDER BY depg.departmentgroupname;";
                }
                else if (filter.tieuChi == 1 && filter.kieuXem == 0)//theo khoa chi dinh + xem tong hop
                {
                }
                else if (filter.tieuChi == 1 && filter.kieuXem == 1)//theo khoa chi dinh + xem chi tiet tung khoa
                {
                }

                //Convert sang DTO
                DataView BCTHToanVien = new DataView(condb.getDataTable(sqlLayBaoCao));
                if (BCTHToanVien != null && BCTHToanVien.Count > 0)
                {
                    long slbn_bh_tong = 0;
                    long slbn_vp_tong = 0;
                    decimal tien_tong_tong = 0;
                    decimal tam_ung_tong = 0;
                    decimal money_khambenh_tong = 0;
                    decimal money_xetnghiem_tong = 0;
                    decimal money_cdhatdcn_tong = 0;
                    decimal money_pttt_tong = 0;
                    decimal money_dvktc_tong = 0;
                    decimal money_giuong_tong = 0;
                    decimal money_mau_tong = 0;
                    decimal money_thuoc_tong = 0;
                    decimal money_vattu_tong = 0;
                    decimal money_phuthu_tong = 0;
                    decimal money_vanchuyen_tong = 0;
                    decimal money_khac_tong = 0;
                    for (int i = 0; i < BCTHToanVien.Count; i++)
                    {
                        BCDashboardTongHopToanVien bcBNTHToanVien = new BCDashboardTongHopToanVien();
                        bcBNTHToanVien.stt = i + 1;
                        bcBNTHToanVien.loaivienphi = BCTHToanVien[i]["loaivienphi"].ToString();
                        bcBNTHToanVien.slbn_bh = Utilities.Util_TypeConvertParse.ToInt64(BCTHToanVien[i]["slbn_bh"].ToString());
                        bcBNTHToanVien.slbn_vp = Utilities.Util_TypeConvertParse.ToInt64(BCTHToanVien[i]["slbn_vp"].ToString());
                        bcBNTHToanVien.slbn_tong = bcBNTHToanVien.slbn_bh + bcBNTHToanVien.slbn_vp;
                        bcBNTHToanVien.tam_ung = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["tam_ung"].ToString());
                        bcBNTHToanVien.money_khambenh = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_khambenh"].ToString());
                        bcBNTHToanVien.money_xetnghiem = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_xetnghiem"].ToString());
                        bcBNTHToanVien.money_cdhatdcn = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_cdhatdcn"].ToString());
                        bcBNTHToanVien.money_pttt = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_pttt"].ToString());
                        bcBNTHToanVien.money_dvktc = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_dvktc"].ToString());
                        bcBNTHToanVien.money_giuong = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_giuong"].ToString());
                        bcBNTHToanVien.money_mau = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_mau"].ToString());
                        bcBNTHToanVien.money_thuoc = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_thuoc"].ToString());
                        bcBNTHToanVien.money_vattu = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_vattu"].ToString());
                        bcBNTHToanVien.money_phuthu = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_phuthu"].ToString());
                        bcBNTHToanVien.money_vanchuyen = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_vanchuyen"].ToString());
                        bcBNTHToanVien.money_khac = Utilities.Util_TypeConvertParse.ToDecimal(BCTHToanVien[i]["money_khac"].ToString());
                        bcBNTHToanVien.tien_tong = bcBNTHToanVien.money_khambenh + bcBNTHToanVien.money_xetnghiem + bcBNTHToanVien.money_cdhatdcn + bcBNTHToanVien.money_pttt + bcBNTHToanVien.money_dvktc + bcBNTHToanVien.money_giuong + bcBNTHToanVien.money_mau + bcBNTHToanVien.money_thuoc + bcBNTHToanVien.money_vattu + bcBNTHToanVien.money_phuthu + bcBNTHToanVien.money_vanchuyen + bcBNTHToanVien.money_khac;
                        lstdataBCTHToanVien.Add(bcBNTHToanVien);

                        slbn_bh_tong += bcBNTHToanVien.slbn_bh;
                        slbn_vp_tong += bcBNTHToanVien.slbn_vp;
                        tien_tong_tong += bcBNTHToanVien.tien_tong;
                        tam_ung_tong += bcBNTHToanVien.tam_ung;
                        money_khambenh_tong += bcBNTHToanVien.money_khambenh;
                        money_xetnghiem_tong += bcBNTHToanVien.money_xetnghiem;
                        money_cdhatdcn_tong += bcBNTHToanVien.money_cdhatdcn;
                        money_pttt_tong += bcBNTHToanVien.money_pttt;
                        money_dvktc_tong += bcBNTHToanVien.money_dvktc;
                        money_giuong_tong += bcBNTHToanVien.money_giuong;
                        money_mau_tong += bcBNTHToanVien.money_mau;
                        money_thuoc_tong += bcBNTHToanVien.money_thuoc;
                        money_vattu_tong += bcBNTHToanVien.money_vattu;
                        money_phuthu_tong += bcBNTHToanVien.money_phuthu;
                        money_vanchuyen_tong += bcBNTHToanVien.money_vanchuyen;
                        money_khac_tong += bcBNTHToanVien.money_khac;
                    }
                    BCDashboardTongHopToanVien bcBNTHToanVien_Tong = new BCDashboardTongHopToanVien();
                    bcBNTHToanVien_Tong.stt = BCTHToanVien.Count + 1;
                    bcBNTHToanVien_Tong.loaivienphi = "Tổng";
                    bcBNTHToanVien_Tong.slbn_bh = slbn_bh_tong;
                    bcBNTHToanVien_Tong.slbn_vp = slbn_vp_tong;
                    bcBNTHToanVien_Tong.slbn_tong = slbn_bh_tong + slbn_vp_tong;
                    bcBNTHToanVien_Tong.tien_tong = tien_tong_tong;
                    bcBNTHToanVien_Tong.tam_ung = tam_ung_tong;
                    bcBNTHToanVien_Tong.money_khambenh = money_khambenh_tong;
                    bcBNTHToanVien_Tong.money_xetnghiem = money_xetnghiem_tong;
                    bcBNTHToanVien_Tong.money_cdhatdcn = money_cdhatdcn_tong;
                    bcBNTHToanVien_Tong.money_pttt = money_pttt_tong;
                    bcBNTHToanVien_Tong.money_dvktc = money_dvktc_tong;
                    bcBNTHToanVien_Tong.money_giuong = money_giuong_tong;
                    bcBNTHToanVien_Tong.money_mau = money_mau_tong;
                    bcBNTHToanVien_Tong.money_thuoc = money_thuoc_tong;
                    bcBNTHToanVien_Tong.money_vattu = money_vattu_tong;
                    bcBNTHToanVien_Tong.money_phuthu = money_phuthu_tong;
                    bcBNTHToanVien_Tong.money_vanchuyen = money_vanchuyen_tong;
                    bcBNTHToanVien_Tong.money_khac = money_khac_tong;
                    lstdataBCTHToanVien.Add(bcBNTHToanVien_Tong);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            return lstdataBCTHToanVien;
        }






    }
}
