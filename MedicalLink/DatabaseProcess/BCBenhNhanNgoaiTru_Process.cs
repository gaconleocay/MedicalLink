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
    public class BCBenhNhanNgoaiTru_Process
    {
        private static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        internal static List<BCDashboardBenhNhanNgoaiTru> BCBenhNhanNgoaiTru_ChayMoi(BCDashboardBenhNhanNgoaiTruFilter filter)
        {
            List<BCDashboardBenhNhanNgoaiTru> lstBCBNNgoaiTru = new List<BCDashboardBenhNhanNgoaiTru>();
            try
            {
                string departmentgroupid = "";

                if (filter.departmentgroupid != 0)
                {
                    departmentgroupid = " and vpm.departmentgroupid='" + filter.departmentgroupid + "' ";
                }

                string sqlLayBaoCao = "SELECT A.departmentid, sum(A.slbn_bh) as slbn_bh, sum(A.slbn_vp) as slbn_vp, sum(A.slbn_bh+slbn_vp) as slbn, sum(A.slbn_nhapvien) as slbn_nhapvien, round(cast(sum(A.tien_bh)as numeric),0) as tien_bh, round(cast(sum(A.tien_vp)as numeric),0) as tien_vp, round(cast(sum(A.tien_bh+A.tien_vp)as numeric),0) as tien_tong  FROM(  SELECT mrd.departmentid,mrd.vienphiid,  sum(case when mrd.doituongbenhnhanid=1 then 1 else 0 end) as slbn_bh,  sum(case when mrd.doituongbenhnhanid<>1 then 1 else 0 end) as slbn_vp,  sum(case when mrd.xutrikhambenhid=4 then 1 else 0 end) as slbn_nhapvien,  (select sum(serdep.money_khambenh_bh+serdep.money_xetnghiem_bh+serdep.money_cdha_bh+serdep.money_tdcn_bh+serdep.money_pttt_bh+serdep.money_dvktc_bh+serdep.money_giuong_bh+serdep.money_phuthu_bh+serdep.money_vanchuyen_bh+serdep.money_khac_bh+serdep.money_mau_bh+serdep.money_thuoc_bh+serdep.money_vattu_bh) from serviceprice_department serdep where serdep.departmentid=mrd.departmentid and serdep.vienphiid=mrd.vienphiid) as tien_bh,  (select sum(serdep.money_khambenh_vp+serdep.money_xetnghiem_vp+serdep.money_cdha_vp+serdep.money_tdcn_vp+serdep.money_pttt_vp+serdep.money_dvktc_vp+serdep.money_giuong_vp+serdep.money_phuthu_vp+serdep.money_vanchuyen_vp+serdep.money_khac_vp+serdep.money_mau_vp+serdep.money_thuoc_vp+serdep.money_vattu_vp) from serviceprice_department serdep where serdep.departmentid=mrd.departmentid and serdep.vienphiid=mrd.vienphiid) as tien_vp  FROM medicalrecord mrd  WHERE mrd.loaibenhanid in (24,20) and mrd.medicalrecordstatus<>0 and mrd.thoigianvaovien >='" + filter.dateTu + "' and mrd.thoigianvaovien <='" + filter.dateDen + departmentgroupid + "'  GROUP BY mrd.departmentid,mrd.vienphiid) A  GROUP BY A.departmentid;";
                DataView dataBCTongTheKhoa = new DataView(condb.getDataTable(sqlLayBaoCao));
                if (dataBCTongTheKhoa != null && dataBCTongTheKhoa.Count > 0)
                {
                    for (int i = 0; i < dataBCTongTheKhoa.Count; i++)
                    {
                        BCDashboardBenhNhanNgoaiTru bcBNngoaiTru = new BCDashboardBenhNhanNgoaiTru();
                        bcBNngoaiTru.stt = i + 1;
                        bcBNngoaiTru.departmentid = Utilities.Util_TypeConvertParse.ToInt64(dataBCTongTheKhoa[i]["departmentid"].ToString());
                        bcBNngoaiTru.slbn_bh = Utilities.Util_TypeConvertParse.ToInt64(dataBCTongTheKhoa[i]["slbn_bh"].ToString());
                        bcBNngoaiTru.slbn_vp = Utilities.Util_TypeConvertParse.ToInt64(dataBCTongTheKhoa[i]["slbn_vp"].ToString());
                        bcBNngoaiTru.slbn = Utilities.Util_TypeConvertParse.ToInt64(dataBCTongTheKhoa[i]["slbn"].ToString());
                        bcBNngoaiTru.slbn_nhapvien = Utilities.Util_TypeConvertParse.ToInt64(dataBCTongTheKhoa[i]["slbn_nhapvien"].ToString());
                        bcBNngoaiTru.tien_bh = Utilities.Util_TypeConvertParse.ToDecimal(dataBCTongTheKhoa[i]["tien_bh"].ToString());
                        bcBNngoaiTru.tien_vp = Utilities.Util_TypeConvertParse.ToDecimal(dataBCTongTheKhoa[i]["tien_vp"].ToString());
                        bcBNngoaiTru.tien_tong = Utilities.Util_TypeConvertParse.ToDecimal(dataBCTongTheKhoa[i]["tien_tong"].ToString());
                        lstBCBNNgoaiTru.Add(bcBNngoaiTru);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            return lstBCBNNgoaiTru;
        }

        internal static void BCBenhNhanNgoaiTru_TuDongChay(BCDashboardBenhNhanNgoaiTruFilter filter)
        {
            try
            {
                String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string departmentgroupid = "";
                string loaivienphiid = "";

                if (filter.departmentgroupid != 0)
                {
                    departmentgroupid = " and vpm.departmentgroupid='" + filter.departmentgroupid + "' ";
                }

                string updateTmp = "";

                string sqlxoadulieuTmp = "DELETE FROM tools_dangdt_tmp vpm WHERE dangdt_date < '" + datetimeNow + "' and loaibaocao='" + filter.loaiBaoCao + "' and khoangdl_tu='" + filter.dateKhoangDLTu + "' " + departmentgroupid + " and chaytudong=" + filter.chayTuDong + " ;";

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
