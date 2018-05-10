using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace MedicalLink.BaoCao
{
    public partial class ucBCSoXetNghiem : UserControl
    {
        private void LayDuLieuSo_ViSinh(string tieuchi_mbp, long _tools_otherlistid)
        {
            try
            {
                string sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode, A.maubenhphamid) as stt, hsba.patientcode, hsba.patientname, A.maubenhphamid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when hsba.bhytcode <>'' then 'x' else '' end) as COBHYT, A.CHANDOAN, (KYC.DEPARTMENTGROUPNAME || ' - ' || PYC.DEPARTMENTNAME) AS NOIGUI, A.YEUCAU, A.KETQUA, A.MAUBENHPHAMDATE, (case when A.maubenhphamfinishdate<>'0001-01-01 00:00:00' then A.maubenhphamfinishdate end) as maubenhphamfinishdate, nth.username AS nguoithuchien, nd.username as nguoiduyet FROM (SELECT mbp.maubenhphamid, mbp.hosobenhanid, mbp.departmentgroupid, mbp.departmentid, mbp.departmentid_des, mbp.chandoan, mbp.usertrakq, mbp.userthuchien, mbp.maubenhphamdate, mbp.maubenhphamfinishdate, se.servicename as yeucau, se.servicevalue as ketqua, se.servicedoer, se.servicecomment FROM maubenhpham mbp LEFT JOIN service se ON se.maubenhphamid=mbp.maubenhphamid WHERE se.servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) and mbp.maubenhphamgrouptype=0 and mbp.departmentid_des='" + 253 + "' " + tieuchi_mbp + " ) A INNER JOIN hosobenhan hsba ON hsba.hosobenhanid=A.hosobenhanid LEFT JOIN departmentgroup kyc ON kyc.departmentgroupid=A.departmentgroupid LEFT JOIN department pyc ON pyc.departmentid=A.departmentid and pyc.departmenttype in (2,3,9) LEFT JOIN nhompersonnel nth ON nth.usercode=A.servicedoer LEFT JOIN nhompersonnel nd ON 'UP_OK:' || nd.usercode=A.servicecomment;  ";
               this.dataBaoCao_VS = condb.GetDataTable_HIS(sql_timkiem);
                if (this.dataBaoCao_VS != null && this.dataBaoCao_VS.Rows.Count > 0)
                {
                    gridControlSoViSinh.DataSource = this.dataBaoCao_VS;
                }
                else
                {
                    gridControlSoViSinh.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LayDuLieuSo_SinhHoaThuongQuy(string tieuchi_mbp, long _tools_otherlistid)
        {
            try
            {
                string sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode,A.maubenhphamid) as stt, hsba.patientcode, hsba.patientname, A.maubenhphamid, A.vienphiid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when hsba.bhytcode <>'' then 'x' else '' end) as cobhyt, A.chandoan, (KYC.departmentgroupname || ' - ' || PYC.departmentname) AS noigui, A.yeucau, A.ketqua_na1, A.ketqua_cl1, A.ketqua_k1, A.ketqua_na2, A.ketqua_cl2, A.ketqua_k2, A.ketqua, A.maubenhphamdate, A.maubenhphamfinishdate, ngd.username AS nguoidoc, ngg.username as nguoigui FROM ( select mbp.maubenhphamid, mbp.hosobenhanid, mbp.vienphiid, mbp.departmentgroupid, mbp.departmentid, mbp.departmentid_des, mbp.chandoan, mbp.maubenhphamdate, (case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate, tsef.servicepricename as yeucau, se.ketqua_na1, se.ketqua_cl1, se.ketqua_k1, se.ketqua_na2, se.ketqua_cl2, se.ketqua_k2, se.ketqua, mbp.userthuchien as nguoidoc, mbp.userid as nguoigui from (select maubenhphamid,hosobenhanid,vienphiid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq,userthuchien,maubenhphamdate,maubenhphamfinishdate,userid from maubenhpham where maubenhphamgrouptype=0 " + tieuchi_mbp + ") mbp inner join (select maubenhphamid,servicepricecode, string_agg((case when servicecode='" + this.maChiSo_SHTQ.KETQUA_NA1 + "' then servicevalue end),'') as ketqua_na1, string_agg((case when servicecode='" + this.maChiSo_SHTQ.KETQUA_CL1 + "' then servicevalue end),'') as ketqua_cl1, string_agg((case when servicecode='" + this.maChiSo_SHTQ.KETQUA_K1 + "' then servicevalue end),'') as ketqua_k1, string_agg((case when servicecode='" + this.maChiSo_SHTQ.KETQUA_NA2 + "' then servicevalue end),'') as ketqua_na2, string_agg((case when servicecode='" + this.maChiSo_SHTQ.KETQUA_CL2 + "' then servicevalue end),'') as ketqua_cl2, string_agg((case when servicecode='" + this.maChiSo_SHTQ.KETQUA_K2 + "' then servicevalue end),'') as ketqua_k2, string_agg((case when servicecode not in ('" + this.maChiSo_SHTQ.KETQUA_NA1 + "','" + this.maChiSo_SHTQ.KETQUA_CL1 + "','" + this.maChiSo_SHTQ.KETQUA_K1 + "','" + this.maChiSo_SHTQ.KETQUA_NA2 + "','" + this.maChiSo_SHTQ.KETQUA_CL2 + "','" + this.maChiSo_SHTQ.KETQUA_K2 + "') then servicevalue end),'') as ketqua from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) group by maubenhphamid,servicepricecode) se on se.maubenhphamid=mbp.maubenhphamid inner join (SELECT tools_serviceref.servicepricecode,tools_serviceref.servicepricename FROM dblink('myconn_mel','select servicepricecode,servicepricename from tools_serviceref where tools_otherlistid=" + _tools_otherlistid + "') AS tools_serviceref(servicepricecode text, servicepricename text)) tsef on tsef.servicepricecode=se.servicepricecode ) A INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba ON hsba.hosobenhanid=A.hosobenhanid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=A.departmentgroupid LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pyc ON pyc.departmentid=A.departmentid LEFT JOIN (select userhisid,username from nhompersonnel) ngd ON ngd.userhisid=A.nguoidoc LEFT JOIN (select userhisid,username from nhompersonnel) ngg ON ngg.userhisid=A.nguoigui; ";
                this.dataBaoCao_SHTQ = condb.GetDataTable_HISToMeL(sql_timkiem);
                if (this.dataBaoCao_SHTQ != null && this.dataBaoCao_SHTQ.Rows.Count > 0)
                {
                    gridControlSoSHTQ.DataSource = this.dataBaoCao_SHTQ;
                }
                else
                {
                    gridControlSoSHTQ.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LayDuLieuSo_NuocTieuVaDichKhac(string tieuchi_mbp, long _tools_otherlistid)
        {
            try
            {
                string sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode,mbp.maubenhphamid) as stt, hsba.patientcode, hsba.patientname, mbp.maubenhphamid, mbp.vienphiid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when hsba.bhytcode <>'' then 'x' else '' end) as cobhyt, mbp.chandoan, (KYC.departmentgroupname || ' - ' || PYC.departmentname) AS noigui, tsef.servicepricename as yeucau, se.ketqua_ubg, se.ketqua_bil, se.ketqua_ket, se.ketqua_pro, se.ketqua_nit, se.ketqua_leu, se.ketqua_glu, se.ketqua_sg, se.ketqua_ph, se.ketqua_blo, se.ketqua, mbp.maubenhphamdate, (case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate, ngd.username AS nguoidoc, ngg.username as nguoigui FROM (select maubenhphamid,hosobenhanid,vienphiid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq,userthuchien,maubenhphamdate,maubenhphamfinishdate,userid from maubenhpham where maubenhphamgrouptype=0 " + tieuchi_mbp + ") mbp inner join (select maubenhphamid,servicepricecode, string_agg((case when servicecode='" + this.maChiSo_NTVD.KETQUA_UBG + "' then servicevalue end),'') as ketqua_ubg, string_agg((case when servicecode='" + this.maChiSo_NTVD.KETQUA_BIL + "' then servicevalue end),'') as ketqua_bil, string_agg((case when servicecode='" + this.maChiSo_NTVD.KETQUA_KET + "' then servicevalue end),'') as ketqua_ket, string_agg((case when servicecode='" + this.maChiSo_NTVD.KETQUA_PRO + "' then servicevalue end),'') as ketqua_pro, string_agg((case when servicecode='" + this.maChiSo_NTVD.KETQUA_NIT + "' then servicevalue end),'') as ketqua_nit, string_agg((case when servicecode='" + this.maChiSo_NTVD.KETQUA_LEU + "' then servicevalue end),'') as ketqua_leu, string_agg((case when servicecode='" + this.maChiSo_NTVD.KETQUA_GLU + "' then servicevalue end),'') as ketqua_glu, string_agg((case when servicecode='" + this.maChiSo_NTVD.KETQUA_SG + "' then servicevalue end),'') as ketqua_sg, string_agg((case when servicecode='" + this.maChiSo_NTVD.KETQUA_PH + "' then servicevalue end),'') as ketqua_ph, string_agg((case when servicecode='" + this.maChiSo_NTVD.KETQUA_BLO + "' then servicevalue end),'') as ketqua_blo, string_agg((case when servicecode not in ('" + this.maChiSo_NTVD.KETQUA_UBG + "','" + this.maChiSo_NTVD.KETQUA_BIL + "','" + this.maChiSo_NTVD.KETQUA_KET + "','" + this.maChiSo_NTVD.KETQUA_PRO + "','" + this.maChiSo_NTVD.KETQUA_NIT + "','" + this.maChiSo_NTVD.KETQUA_LEU + "','" + this.maChiSo_NTVD.KETQUA_GLU + "','" + this.maChiSo_NTVD.KETQUA_SG + "','" + this.maChiSo_NTVD.KETQUA_PH + "','" + this.maChiSo_NTVD.KETQUA_BLO + "') then servicevalue end),'') as ketqua from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) group by maubenhphamid,servicepricecode) se on se.maubenhphamid=mbp.maubenhphamid inner join (SELECT tools_serviceref.servicepricecode,tools_serviceref.servicepricename FROM dblink('myconn_mel','select servicepricecode,servicepricename from tools_serviceref where tools_otherlistid=" + _tools_otherlistid + "') AS tools_serviceref(servicepricecode text, servicepricename text)) tsef on tsef.servicepricecode=se.servicepricecode INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba ON hsba.hosobenhanid=mbp.hosobenhanid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=mbp.departmentgroupid LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pyc ON pyc.departmentid=mbp.departmentid LEFT JOIN (select userhisid,username from nhompersonnel) ngd ON ngd.userhisid=mbp.userthuchien LEFT JOIN (select userhisid,username from nhompersonnel) ngg ON ngg.userhisid=mbp.userid; ";
                this.dataBaoCao_NTVD = condb.GetDataTable_HISToMeL(sql_timkiem);
                if (this.dataBaoCao_NTVD != null && this.dataBaoCao_NTVD.Rows.Count > 0)
                {
                    gridControlSoNTVD.DataSource = this.dataBaoCao_NTVD;
                }
                else
                {
                    gridControlSoNTVD.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LayDuLieuSo_MienDich(string tieuchi_mbp, long _tools_otherlistid)
        {
            try
            {
                string sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode,mbp.maubenhphamid) as stt, hsba.patientcode, hsba.patientname, mbp.maubenhphamid, mbp.vienphiid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when hsba.bhytcode <>'' then 'x' else '' end) as cobhyt, mbp.chandoan, (KYC.departmentgroupname || ' - ' || PYC.departmentname) AS noigui, tsef.servicepricename as yeucau, se.ketqua, mbp.maubenhphamdate, (case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate, ngd.username AS nguoidoc, ngg.username as nguoigui FROM (select maubenhphamid,hosobenhanid,vienphiid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq,userthuchien,maubenhphamdate,maubenhphamfinishdate,userid from maubenhpham where maubenhphamgrouptype=0 " + tieuchi_mbp + ") mbp inner join (select maubenhphamid,servicepricecode, servicevalue as ketqua from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) ) se on se.maubenhphamid=mbp.maubenhphamid inner join (SELECT tools_serviceref.servicepricecode,tools_serviceref.servicepricename FROM dblink('myconn_mel','select servicepricecode,servicepricename from tools_serviceref where tools_otherlistid=" + _tools_otherlistid + "') AS tools_serviceref(servicepricecode text, servicepricename text)) tsef on tsef.servicepricecode=se.servicepricecode INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba ON hsba.hosobenhanid=mbp.hosobenhanid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=mbp.departmentgroupid LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pyc ON pyc.departmentid=mbp.departmentid LEFT JOIN (select userhisid,username from nhompersonnel) ngd ON ngd.userhisid=mbp.userthuchien LEFT JOIN (select userhisid,username from nhompersonnel) ngg ON ngg.userhisid=mbp.userid; ";
                this.dataBaoCao_MD = condb.GetDataTable_HISToMeL(sql_timkiem);
                if (this.dataBaoCao_MD != null && this.dataBaoCao_MD.Rows.Count > 0)
                {
                    gridControlSoMienDich.DataSource = this.dataBaoCao_MD;
                }
                else
                {
                    gridControlSoMienDich.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LayDuLieuSo_KhiMau(string tieuchi_mbp, long _tools_otherlistid)
        {
            try
            {
                string sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode,mbp.maubenhphamid) as stt, hsba.patientcode, hsba.patientname, mbp.maubenhphamid, mbp.vienphiid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when hsba.bhytcode <>'' then 'x' else '' end) as cobhyt, mbp.chandoan, (KYC.departmentgroupname || ' - ' || PYC.departmentname) AS noigui, tsef.servicepricename as yeucau, se.ketqua_fio2, se.ketqua_ptem, se.ketqua_ag, se.ketqua_ao2, se.ketqua_hco, se.ketqua_ctc_b, se.ketqua_cto2, se.ketqua_bb, se.ketqua_bee, se.ketqua_be, se.ketqua_ctc_p, se.ketqua_chc, se.ketqua_baro, se.ketqua_bili, se.ketqua_hhb, se.ketqua_met, se.ketqua_coh, se.ketqua_o2h, se.ketqua_so2, se.ketqua_thb, se.ketqua_cl, se.ketqua_ca2, se.ketqua_k, se.ketqua_na, se.ketqua_hct, se.ketqua_pco, se.ketqua_po2, se.ketqua_ph, mbp.maubenhphamdate, (case when mbp.maubenhphamfinishdate<>'0001-01-01 00:00:00' then mbp.maubenhphamfinishdate end) as maubenhphamfinishdate, ngd.username AS nguoidoc, ngg.username as nguoigui FROM (select maubenhphamid,hosobenhanid,vienphiid,departmentgroupid,departmentid,departmentid_des,chandoan,usertrakq,userthuchien,maubenhphamdate,maubenhphamfinishdate,userid from maubenhpham where maubenhphamgrouptype=0 " + tieuchi_mbp + ") mbp inner join (select maubenhphamid,servicepricecode, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_FIO2 + "' then servicevalue end),'') as ketqua_fio2, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_PTEM + "' then servicevalue end),'') as ketqua_ptem, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_AG + "' then servicevalue end),'') as ketqua_ag, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_AO2 + "' then servicevalue end),'') as ketqua_ao2, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_HCO + "' then servicevalue end),'') as ketqua_hco, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_CTC_B + "' then servicevalue end),'') as ketqua_ctc_b, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_CTO2 + "' then servicevalue end),'') as ketqua_cto2, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_BB + "' then servicevalue end),'') as ketqua_bb, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_BEE + "' then servicevalue end),'') as ketqua_bee, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_BE + "' then servicevalue end),'') as ketqua_be, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_CTC_P + "' then servicevalue end),'') as ketqua_ctc_p, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_CHC + "' then servicevalue end),'') as ketqua_chc, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_BARO + "' then servicevalue end),'') as ketqua_baro, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_BILI + "' then servicevalue end),'') as ketqua_bili, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_HHB + "' then servicevalue end),'') as ketqua_hhb, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_MET + "' then servicevalue end),'') as ketqua_met, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_COH + "' then servicevalue end),'') as ketqua_coh, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_O2H + "' then servicevalue end),'') as ketqua_o2h, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_SO2 + "' then servicevalue end),'') as ketqua_so2, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_THB + "' then servicevalue end),'') as ketqua_thb, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_CL + "' then servicevalue end),'') as ketqua_cl, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_CA2 + "' then servicevalue end),'') as ketqua_ca2, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_K + "' then servicevalue end),'') as ketqua_k, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_NA + "' then servicevalue end),'') as ketqua_na, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_HCT + "' then servicevalue end),'') as ketqua_hct, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_PCO + "' then servicevalue end),'') as ketqua_pco, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_PO2 + "' then servicevalue end),'') as ketqua_po2, string_agg((case when servicecode='" + this.maChiSo_KM.KETQUA_PH + "' then servicevalue end),'') as ketqua_ph from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) group by maubenhphamid,servicepricecode) se on se.maubenhphamid=mbp.maubenhphamid inner join (SELECT tools_serviceref.servicepricecode,tools_serviceref.servicepricename FROM dblink('myconn_mel','select servicepricecode,servicepricename from tools_serviceref where tools_otherlistid=" + _tools_otherlistid + "') AS tools_serviceref(servicepricecode text, servicepricename text)) tsef on tsef.servicepricecode=se.servicepricecode INNER JOIN (select hosobenhanid,patientcode,patientname,gioitinhcode,hosobenhandate,birthday,hc_sonha,hc_thon,hc_xacode,hc_xaname,hc_huyencode,hc_huyenname,hc_tinhcode,hc_tinhname,hc_quocgianame,bhytcode from hosobenhan) hsba ON hsba.hosobenhanid=mbp.hosobenhanid LEFT JOIN (select departmentgroupid,departmentgroupname from departmentgroup) kyc ON kyc.departmentgroupid=mbp.departmentgroupid LEFT JOIN (select departmentid,departmentname from department where departmenttype in (2,3,9)) pyc ON pyc.departmentid=mbp.departmentid LEFT JOIN (select userhisid,username from nhompersonnel) ngd ON ngd.userhisid=mbp.userthuchien LEFT JOIN (select userhisid,username from nhompersonnel) ngg ON ngg.userhisid=mbp.userid;  ";
                this.dataBaoCao_KM = condb.GetDataTable_HISToMeL(sql_timkiem);
                if (this.dataBaoCao_KM != null && this.dataBaoCao_KM.Rows.Count > 0)
                {
                    gridControlSoKhiMau.DataSource = this.dataBaoCao_KM;
                }
                else
                {
                    gridControlSoKhiMau.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }



    }
}
