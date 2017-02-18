using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.DatabaseProcess
{
    public class TimerServiceProcess
    {
        private static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        //kieulaydulieu = 0: bao cao QL Khoa - lay full du lieu
        //kieulaydulieu = 1: bao cao Noi tru - lay tuy chon du lieu
        //departmentgroupid = 0 : lay toan vien
        //departmentgroupid !=0 : lay theo khoa
        //chaymoidulieu==0: khong chay moi
        //chaymoidulieu==1: chay moi

        #region DangDT_Tmp
        public static void SQLKiemTraVaUpdate_DangDT_Tmp(int kieulaydulieu, string dateTu, string dateDen, string dateKhoangDLTu, long departmentgroupid, int chaymoidulieu)
        {
            try
            {
                if (chaymoidulieu == 0)
                {
                    string sqlkiemtraDL = "";
                    if (departmentgroupid == 0) //toan vien
                    {
                        sqlkiemtraDL = "SELECT dangdt_date FROM tools_dangdt_tmp WHERE kieulaydulieu='" + kieulaydulieu + "' and khoangdl_tu='" + dateKhoangDLTu + "' ORDER BY dangdt_date DESC LIMIT 1";
                    }
                    else
                    {
                        sqlkiemtraDL = "SELECT dangdt_date FROM tools_dangdt_tmp WHERE kieulaydulieu='" + kieulaydulieu + "' and khoangdl_tu='" + dateKhoangDLTu + "' and departmentgroupid='" + departmentgroupid + "' ORDER BY dangdt_date DESC LIMIT 1";
                    }
                    DataView dataKiemTra = new DataView(condb.getDataTable(sqlkiemtraDL));
                    if (dataKiemTra != null && dataKiemTra.Count > 0)
                    {
                        long datetime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm"));
                        long thoigianUpdataTabl = Utilities.Util_TypeConvertParse.ToInt64(Convert.ToDateTime(dataKiemTra[0]["dangdt_date"].ToString()).ToString("yyyyMMddHHmm"));
                        if ((datetime - thoigianUpdataTabl) > GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp)
                        {
                            SQLChay_DangDT_Tmp(kieulaydulieu, dateTu, dateDen, dateKhoangDLTu, departmentgroupid);
                        }
                    }
                    else
                    {
                        SQLChay_DangDT_Tmp(kieulaydulieu, dateTu, dateDen, dateKhoangDLTu, departmentgroupid);
                    }
                }
                else
                {
                    SQLChay_DangDT_Tmp(kieulaydulieu, dateTu, dateDen, dateKhoangDLTu, departmentgroupid);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private static void SQLChay_DangDT_Tmp(int kieulaydulieu, string dateTu, string dateDen, string dateKhoangDLTu, long departmentgroupid)
        {
            try
            {
                String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string updateTmp = "";
                if (departmentgroupid == 0) //toan vien
                {
                    if (kieulaydulieu == 0) //lay full du lieu
                    {
                        updateTmp = "INSERT INTO tools_dangdt_tmp(departmentgroupid, bn_chuyendi, bn_chuyenden, ravien_slbn, dangdt_slbn_bh, dangdt_slbn_vp, dangdt_slbn, dangdt_tienkb, dangdt_tienxn, dangdt_tiencdhatdcn, dangdt_tienpttt, dangdt_tiendvktc, dangdt_tiengiuong, dangdt_tienkhac, dangdt_tienvattu, dangdt_tienmau, dangdt_tienthuoc_bhyt, dangdt_tienthuoc_vp, dangdt_tienthuoc, dangdt_tongtien_bhyt, dangdt_tongtien_vp, dangdt_tongtien, dangdt_tamung, dangdt_date, kieulaydulieu, khoangdl_tu) SELECT vpm.departmentgroupid, sum(case when vpm.vienphistatus=0 then (select count(mrd.*) from medicalrecord mrd where mrd.vienphiid=vpm.vienphiid and mrd.departmentgroupid=vpm.departmentgroupid and mrd.hinhthucravienid=8 and mrd.thoigianravien>='" + dateTu + "' and mrd.thoigianravien<='" + dateDen + "') else 0 end) as bn_chuyendi, sum(case when vpm.vienphistatus=0 then (select count(mrd.*) from medicalrecord mrd where mrd.vienphiid=vpm.vienphiid and mrd.departmentgroupid=vpm.departmentgroupid and mrd.hinhthucvaovienid=3 and mrd.thoigianvaovien>='" + dateTu + "' and mrd.thoigianvaovien<='" + dateDen + "') else 0 end) as bn_chuyenden, sum(case when vpm.vienphidate_ravien>='" + dateTu + "' and vpm.vienphidate_ravien<='" + dateDen + "' then 1 else 0 end) as ravien_slbn, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as dangdt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as dangdt_slbn_vp, count(vpm.*) as dangdt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as dangdt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as dangdt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as dangdt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as dangdt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as dangdt_tiendvktc, round(cast(sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as dangdt_tiengiuong, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as dangdt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as dangdt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as dangdt_tienmau,round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as dangdt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as dangdt_tienthuoc_vp, round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as dangdt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as dangdt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as dangdt_tongtien_vp,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as dangdt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as dangdt_tamung, '" + datetimeNow + "' as dangdt_date, '" + kieulaydulieu + "', '" + dateKhoangDLTu + "' FROM vienphi_money vpm WHERE vpm.loaivienphiid=0 and vpm.vienphistatus=0 and vpm.vienphidate>='" + dateKhoangDLTu + "' GROUP BY vpm.departmentgroupid;";
                    }
                    else
                    {
                        updateTmp = "INSERT INTO tools_dangdt_tmp(departmentgroupid, ravien_slbn, dangdt_slbn_bh, dangdt_slbn_vp, dangdt_slbn, dangdt_tienkb, dangdt_tienxn, dangdt_tiencdhatdcn, dangdt_tienpttt, dangdt_tiendvktc, dangdt_tiengiuong, dangdt_tienkhac, dangdt_tienvattu, dangdt_tienmau, dangdt_tienthuoc_bhyt, dangdt_tienthuoc_vp, dangdt_tienthuoc, dangdt_tongtien_bhyt, dangdt_tongtien_vp, dangdt_tongtien, dangdt_tamung, dangdt_date, kieulaydulieu, khoangdl_tu) SELECT vpm.departmentgroupid, sum(case when vpm.vienphidate_ravien>='" + dateTu + "' and vpm.vienphidate_ravien<='" + dateDen + "' then 1 else 0 end) as ravien_slbn, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as dangdt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as dangdt_slbn_vp, count(vpm.*) as dangdt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as dangdt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as dangdt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as dangdt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as dangdt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as dangdt_tiendvktc, round(cast(sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as dangdt_tiengiuong, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as dangdt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as dangdt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as dangdt_tienmau,round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as dangdt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as dangdt_tienthuoc_vp, round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as dangdt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as dangdt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as dangdt_tongtien_vp,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as dangdt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as dangdt_tamung, '" + datetimeNow + "' as dangdt_date, '" + kieulaydulieu + "', '" + dateKhoangDLTu + "' FROM vienphi_money vpm WHERE vpm.loaivienphiid=0 and vpm.vienphistatus=0  and vpm.vienphidate>='" + dateKhoangDLTu + "' GROUP BY vpm.departmentgroupid;";
                    }
                }
                else // lay theo khoa
                {
                    updateTmp = "INSERT INTO tools_dangdt_tmp(departmentgroupid, bn_chuyendi, bn_chuyenden, ravien_slbn, dangdt_slbn_bh, dangdt_slbn_vp, dangdt_slbn, dangdt_tienkb, dangdt_tienxn, dangdt_tiencdhatdcn, dangdt_tienpttt, dangdt_tiendvktc, dangdt_tiengiuong, dangdt_tienkhac, dangdt_tienvattu, dangdt_tienmau, dangdt_tienthuoc_bhyt, dangdt_tienthuoc_vp, dangdt_tienthuoc, dangdt_tongtien_bhyt, dangdt_tongtien_vp, dangdt_tongtien, dangdt_tamung, dangdt_date, kieulaydulieu, khoangdl_tu) SELECT vpm.departmentgroupid, sum(case when vpm.vienphistatus=0 then (select count(mrd.*) from medicalrecord mrd where mrd.vienphiid=vpm.vienphiid and mrd.departmentgroupid=vpm.departmentgroupid and mrd.hinhthucravienid=8 and mrd.thoigianravien>='" + dateTu + "' and mrd.thoigianravien<='" + dateDen + "') else 0 end) as bn_chuyendi, sum(case when vpm.vienphistatus=0 then (select count(mrd.*) from medicalrecord mrd where mrd.vienphiid=vpm.vienphiid and mrd.departmentgroupid=vpm.departmentgroupid and mrd.hinhthucvaovienid=3 and mrd.thoigianvaovien>='" + dateTu + "' and mrd.thoigianvaovien<='" + dateDen + "') else 0 end) as bn_chuyenden, sum(case when vpm.vienphidate_ravien>='" + dateTu + "' and vpm.vienphidate_ravien<='" + dateDen + "' then 1 else 0 end) as ravien_slbn, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as dangdt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as dangdt_slbn_vp, count(vpm.*) as dangdt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as dangdt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as dangdt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as dangdt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as dangdt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as dangdt_tiendvktc, round(cast(sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as dangdt_tiengiuong, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as dangdt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as dangdt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as dangdt_tienmau,round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as dangdt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as dangdt_tienthuoc_vp, round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as dangdt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as dangdt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as dangdt_tongtien_vp,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as dangdt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as dangdt_tamung, '" + datetimeNow + "' as dangdt_date, '" + kieulaydulieu + "', '" + dateKhoangDLTu + "' FROM vienphi_money vpm WHERE vpm.loaivienphiid=0 and vpm.vienphistatus=0 and vpm.departmentgroupid='" + departmentgroupid + "' and vpm.vienphidate>='" + dateKhoangDLTu + "' GROUP BY vpm.departmentgroupid;";
                }

                string sqlxoadulieuTmp = "DELETE FROM tools_dangdt_tmp WHERE dangdt_date < '" + datetimeNow + "' and kieulaydulieu='" + kieulaydulieu + "' and khoangdl_tu='" + dateKhoangDLTu + "' ;";

                condb.ExecuteNonQuery(updateTmp);
                condb.ExecuteNonQuery(sqlxoadulieuTmp);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion

        #region RaVienChuaTT_Tmp
        public static void SQLKiemTraVaUpdate_RaVienChuaTT_Tmp(int kieulaydulieu, string dateTu, string dateDen, string dateKhoangDLTu, long departmentgroupid, int chaymoidulieu)
        {
            try
            {
                if (chaymoidulieu == 0)
                {
                    string sqlkiemtraDL = "";
                    if (departmentgroupid == 0) //toan vien
                    {
                        sqlkiemtraDL = "SELECT ravienchuatt_date FROM tools_ravienchuatt_tmp WHERE kieulaydulieu='" + kieulaydulieu + "' and khoangdl_tu='" + dateKhoangDLTu + "' ORDER BY ravienchuatt_date DESC LIMIT 1";
                    }
                    else
                    {
                        sqlkiemtraDL = "SELECT ravienchuatt_date FROM tools_ravienchuatt_tmp WHERE kieulaydulieu='" + kieulaydulieu + "' and khoangdl_tu='" + dateKhoangDLTu + "' and departmentgroupid='" + departmentgroupid + "' ORDER BY ravienchuatt_date DESC LIMIT 1";
                    }
                    DataView dataKiemTra = new DataView(condb.getDataTable(sqlkiemtraDL));
                    if (dataKiemTra != null && dataKiemTra.Count > 0)
                    {
                        long datetime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm"));
                        long thoigianUpdataTabl = Utilities.Util_TypeConvertParse.ToInt64(Convert.ToDateTime(dataKiemTra[0]["ravienchuatt_date"].ToString()).ToString("yyyyMMddHHmm"));
                        if ((datetime - thoigianUpdataTabl) > GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp)
                        {
                            SQLChay_RaVienChuaTT_Tmp(kieulaydulieu,dateTu, dateDen, dateKhoangDLTu, departmentgroupid);
                        }
                    }
                    else
                    {
                        SQLChay_RaVienChuaTT_Tmp(kieulaydulieu, dateTu, dateDen, dateKhoangDLTu, departmentgroupid);
                    }
                }
                else
                {
                    SQLChay_RaVienChuaTT_Tmp(kieulaydulieu, dateTu, dateDen, dateKhoangDLTu, departmentgroupid);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private static void SQLChay_RaVienChuaTT_Tmp(int kieulaydulieu, string dateTu, string dateDen, string dateKhoangDLTu, long departmentgroupid)
        {
            try
            {
                String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string updateTmp = "";
                if (departmentgroupid == 0) //toan vien
                {
                    updateTmp = "INSERT INTO tools_ravienchuatt_tmp(departmentgroupid, ravienchuatt_slbn_bh, ravienchuatt_slbn_vp, ravienchuatt_slbn, ravienchuatt_tienkb, ravienchuatt_tienxn, ravienchuatt_tiencdhatdcn, ravienchuatt_tienpttt, ravienchuatt_tiendvktc, ravienchuatt_tiengiuong, ravienchuatt_tienkhac, ravienchuatt_tienvattu, ravienchuatt_tienmau, ravienchuatt_tienthuoc_bhyt, ravienchuatt_tienthuoc_vp, ravienchuatt_tienthuoc, ravienchuatt_tongtien_bhyt, ravienchuatt_tongtien_vp, ravienchuatt_tongtien, ravienchuatt_tamung, ravienchuatt_date, kieulaydulieu, khoangdl_tu) SELECT vpm.departmentgroupid, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as ravienchuatt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as ravienchuatt_slbn_vp, count(vpm.*) as ravienchuatt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as ravienchuatt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as ravienchuatt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as ravienchuatt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as ravienchuatt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as ravienchuatt_tiendvktc, round(cast(sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as ravienchuatt_tiengiuong, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as ravienchuatt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as ravienchuatt_tienmau,round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as ravienchuatt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as ravienchuatt_tienthuoc_vp, round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as ravienchuatt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as ravienchuatt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tongtien_vp,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as ravienchuatt_tamung, '" + datetimeNow + "' as ravienchuatt_date, '" + kieulaydulieu + "', '" + dateKhoangDLTu + "' FROM vienphi_money vpm WHERE vpm.loaivienphiid=0 and COALESCE(vpm.vienphistatus_vp,0)=0 and vpm.vienphistatus<>0 and vpm.vienphidate>='" + dateKhoangDLTu + "' GROUP BY vpm.departmentgroupid;";
                }
                else // lay theo khoa
                {
                    updateTmp = "INSERT INTO tools_ravienchuatt_tmp(departmentgroupid, ravienchuatt_slbn_bh, ravienchuatt_slbn_vp, ravienchuatt_slbn, ravienchuatt_tienkb, ravienchuatt_tienxn, ravienchuatt_tiencdhatdcn, ravienchuatt_tienpttt, ravienchuatt_tiendvktc, ravienchuatt_tiengiuong, ravienchuatt_tienkhac, ravienchuatt_tienvattu, ravienchuatt_tienmau, ravienchuatt_tienthuoc_bhyt, ravienchuatt_tienthuoc_vp, ravienchuatt_tienthuoc, ravienchuatt_tongtien_bhyt, ravienchuatt_tongtien_vp, ravienchuatt_tongtien, ravienchuatt_tamung, ravienchuatt_date, kieulaydulieu, khoangdl_tu) SELECT vpm.departmentgroupid, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as ravienchuatt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as ravienchuatt_slbn_vp, count(vpm.*) as ravienchuatt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as ravienchuatt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as ravienchuatt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as ravienchuatt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as ravienchuatt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as ravienchuatt_tiendvktc, round(cast(sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as ravienchuatt_tiengiuong, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as ravienchuatt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as ravienchuatt_tienmau,round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as ravienchuatt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as ravienchuatt_tienthuoc_vp, round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as ravienchuatt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as ravienchuatt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tongtien_vp,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as ravienchuatt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as ravienchuatt_tamung, '" + datetimeNow + "' as ravienchuatt_date, '" + kieulaydulieu + "', '" + dateKhoangDLTu + "' FROM vienphi_money vpm WHERE vpm.loaivienphiid=0 and COALESCE(vpm.vienphistatus_vp,0)=0 and vpm.vienphistatus<>0 and vpm.departmentgroupid='" + departmentgroupid + "'  and vpm.vienphidate>='" + dateKhoangDLTu + "' GROUP BY vpm.departmentgroupid;";
                }

                string sqlxoadulieuTmp = "DELETE FROM tools_ravienchuatt_tmp WHERE ravienchuatt_date < '" + datetimeNow + "' and khoangdl_tu='" + dateKhoangDLTu + "' and kieulaydulieu='" + kieulaydulieu + "' ;";

                condb.ExecuteNonQuery(updateTmp);
                condb.ExecuteNonQuery(sqlxoadulieuTmp);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region RaVienDaTT_Tmp
        public static void SQLKiemTraVaUpdate_RaVienDaTT_Tmp(int kieulaydulieu, string dateTu, string dateDen, string dateKhoangDLTu, long departmentgroupid)
        {
            try
            {
                SQLChay_RaVienDaTT_Tmp(dateTu, dateDen, departmentgroupid);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private static void SQLChay_RaVienDaTT_Tmp(string dateTu, string dateDen, long departmentgroupid)
        {
            try
            {
                string updateTmp = "";
                String datetimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (departmentgroupid == 0)
                {
                    updateTmp = "INSERT INTO tools_raviendatt_tmp(departmentgroupid, raviendatt_slbn_bh, raviendatt_slbn_vp, raviendatt_slbn, raviendatt_tienkb, raviendatt_tienxn, raviendatt_tiencdhatdcn, raviendatt_tienpttt, raviendatt_tiendvktc, raviendatt_tiengiuong, raviendatt_tienkhac, raviendatt_tienvattu, raviendatt_tienmau, raviendatt_tienthuoc_bhyt, raviendatt_tienthuoc_vp, raviendatt_tienthuoc, raviendatt_tongtien_bhyt, raviendatt_tongtien_vp, raviendatt_tongtien, raviendatt_tamung, raviendatt_date) SELECT vpm.departmentgroupid, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as raviendatt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as raviendatt_slbn_vp, count(vpm.*) as raviendatt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as raviendatt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as raviendatt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as raviendatt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as raviendatt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as raviendatt_tiendvktc, round(cast(sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as raviendatt_tiengiuong, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as raviendatt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as raviendatt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as raviendatt_tienmau, round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as raviendatt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as raviendatt_tienthuoc_vp,round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as raviendatt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as raviendatt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as raviendatt_tongtien_vp, round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as raviendatt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as raviendatt_tamung, '" + datetimeNow + "' as raviendatt_date FROM vienphi_money vpm WHERE vpm.loaivienphiid=0 and COALESCE(vpm.vienphistatus_vp,0)=1 and vpm.duyet_ngayduyet_vp >= '" + dateTu + "' and vpm.duyet_ngayduyet_vp <= '" + dateDen + "' GROUP BY vpm.departmentgroupid;";
                }
                else
                {
                    updateTmp = "INSERT INTO tools_raviendatt_tmp(departmentgroupid, raviendatt_slbn_bh, raviendatt_slbn_vp, raviendatt_slbn, raviendatt_tienkb, raviendatt_tienxn, raviendatt_tiencdhatdcn, raviendatt_tienpttt, raviendatt_tiendvktc, raviendatt_tiengiuong, raviendatt_tienkhac, raviendatt_tienvattu, raviendatt_tienmau, raviendatt_tienthuoc_bhyt, raviendatt_tienthuoc_vp, raviendatt_tienthuoc, raviendatt_tongtien_bhyt, raviendatt_tongtien_vp, raviendatt_tongtien, raviendatt_tamung, raviendatt_date) SELECT vpm.departmentgroupid, sum(case when vpm.doituongbenhnhanid=1 then 1 else 0 end) as raviendatt_slbn_bh, sum(case when vpm.doituongbenhnhanid<>1 then 1 else 0 end) as raviendatt_slbn_vp, count(vpm.*) as raviendatt_slbn, round(cast(sum(vpm.money_khambenh_bh + vpm.money_khambenh_vp) as numeric),0) as raviendatt_tienkb, round(cast(sum(vpm.money_xetnghiem_bh + vpm.money_xetnghiem_vp) as numeric),0) as raviendatt_tienxn, round(cast(sum(vpm.money_cdha_bh + vpm.money_cdha_vp + vpm.money_tdcn_bh + vpm.money_tdcn_vp) as numeric),0) as raviendatt_tiencdhatdcn, round(cast(sum(vpm.money_pttt_bh + vpm.money_pttt_vp) as numeric),0) as raviendatt_tienpttt, round(cast(sum(vpm.money_dvktc_bh + vpm.money_dvktc_vp) as numeric),0) as raviendatt_tiendvktc, round(cast(sum(vpm.money_giuong_bh + vpm.money_giuong_vp) as numeric),0) as raviendatt_tiengiuong, round(cast(sum(vpm.money_khac_bh + vpm.money_khac_vp + vpm.money_phuthu_bh + vpm.money_phuthu_vp + vpm.money_vanchuyen_bh + vpm.money_vanchuyen_vp) as numeric),0) as raviendatt_tienkhac, round(cast(sum(vpm.money_vattu_bh + vpm.money_vattu_vp) as numeric),0) as raviendatt_tienvattu, round(cast(sum(vpm.money_mau_bh + vpm.money_mau_vp) as numeric),0) as raviendatt_tienmau, round(cast(sum(vpm.money_thuoc_bh) as numeric),0) as raviendatt_tienthuoc_bhyt,round(cast(sum(vpm.money_thuoc_vp) as numeric),0) as raviendatt_tienthuoc_vp,round(cast(sum(vpm.money_thuoc_bh + vpm.money_thuoc_vp) as numeric),0) as raviendatt_tienthuoc,round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh) as numeric),0) as raviendatt_tongtien_bhyt,round(cast(sum(vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as raviendatt_tongtien_vp, round(cast(sum(vpm.money_khambenh_bh + vpm.money_xetnghiem_bh + vpm.money_cdha_bh + vpm.money_tdcn_bh + vpm.money_pttt_bh + vpm.money_dvktc_bh + vpm.money_giuong_bh + vpm.money_khac_bh + vpm.money_phuthu_bh + vpm.money_vanchuyen_bh + vpm.money_thuoc_bh + vpm.money_mau_bh + vpm.money_vattu_bh + vpm.money_khambenh_vp + vpm.money_xetnghiem_vp + vpm.money_cdha_vp + vpm.money_tdcn_vp + vpm.money_pttt_vp + vpm.money_dvktc_vp + vpm.money_giuong_vp + vpm.money_khac_vp + vpm.money_phuthu_vp + vpm.money_vanchuyen_vp + vpm.money_thuoc_vp + vpm.money_mau_vp + vpm.money_vattu_vp) as numeric),0) as raviendatt_tongtien,round(cast(sum(vpm.tam_ung) as numeric),0) as raviendatt_tamung, '" + datetimeNow + "' as raviendatt_date FROM vienphi_money vpm WHERE vpm.loaivienphiid=0 and COALESCE(vpm.vienphistatus_vp,0)=1 and vpm.duyet_ngayduyet_vp >= '" + dateTu + "' and vpm.duyet_ngayduyet_vp <= '" + dateDen + "' and vpm.departmentgroupid='" + departmentgroupid + "' GROUP BY vpm.departmentgroupid;";
                }

                string sqlxoadulieuTmp = "DELETE FROM tools_raviendatt_tmp WHERE raviendatt_date < '" + datetimeNow + "';";

                condb.ExecuteNonQuery(updateTmp);
                condb.ExecuteNonQuery(sqlxoadulieuTmp);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        #endregion
    }
}
