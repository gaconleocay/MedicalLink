using DevExpress.Utils.Menu;
using DevExpress.XtraSplashScreen;
using MedicalLink.Base;
using MedicalLink.ClassCommon;
using MedicalLink.DatabaseProcess;
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

namespace MedicalLink.BaoCao
{
    public partial class ucBCThucHienCLS : UserControl
    {
        #region Tim kiem

        internal void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                EnableAndDisableNutIn();
                this.helper.ClearSelection();

                this.kiemtrasuadulieu = false;
                string sql_laydulieu = "";

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string tieuchi_date_ser = "";
                string tieuchi_date_vp = "";
                string tieuchi_date_thuchien = "";
                string tieuchi_date_tiepnhan = "";
                string mbp_departmentid = "";
                string serf_pttt_loaiid = " and pttt_loaiid>0 ";
                string lstdepartmentgroupid = "";
                string serf_nhomdichvu = "";
                string _sapxeptheo = "A.maubenhphamfinishdate";
                string _trangthaipttt = "";
                string _join_thuchiencls = " left join ";

                //Loai bo dich vu Xa tri - bv Viet tiep
                string _servicepricecodeXaTri = "";
                List<ClassCommon.ToolsOtherListDTO> _lstOtherListXaTri = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_29_DV").ToList();
                if (_lstOtherListXaTri != null && _lstOtherListXaTri.Count > 0)
                {
                    _servicepricecodeXaTri = " and servicepricecode !='" + _lstOtherListXaTri[0].tools_otherlistvalue + "' ";
                }




                string chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GAYME_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.PHU2_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100))-(A.GIUPVIEC2_TIEN * (A.TYLE/100)) ";

                if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_date_vp = " where vienphistatus_vp=1 and duyet_ngayduyet_vp between '" + tungay + "' and '" + denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    tieuchi_date_thuchien = " and thuchienclsdate between '" + tungay + "' and '" + denngay + "' ";
                    _join_thuchiencls = " inner join ";
                }
                else if (cboTieuChi.Text == "Theo ngày tiếp nhận")
                {
                    tieuchi_date_tiepnhan = " and maubenhphamdate_thuchien between '" + tungay + "' and '" + denngay + "' ";
                }
                else
                {
                    tieuchi_date_ser = " and servicepricedate between '" + tungay + "' and '" + denngay + "' ";
                    _sapxeptheo = "A.NGAY_CHIDINH";
                }

                if (cboTrangThai.Text == "Chưa gửi YC")
                {
                    _trangthaipttt = " and coalesce(duyetpttt_stt,0)=0 ";
                }
                else if (cboTrangThai.Text == "Đã gửi YC")
                {
                    _trangthaipttt = " and duyetpttt_stt=1 ";
                }
                else if (cboTrangThai.Text == "Đã tiếp nhận YC")
                {
                    _trangthaipttt = " and duyetpttt_stt=2 ";
                }
                else if (cboTrangThai.Text == "Đã duyệt PTTT")
                {
                    _trangthaipttt = " and duyetpttt_stt=3 ";
                }
                //else if (cboTrangThai.Text == "Đã khóa")
                //{
                //    _trangthaipttt = " and duyetpttt_stt=99 ";
                //}

                if (chkChuaPhanLoaiPTTT.Checked)
                {
                    serf_pttt_loaiid = " and pttt_loaiid=0 ";
                }

                mbp_departmentid = " and departmentid_des in (";
                List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                {
                    mbp_departmentid += lstPhongCheck[i] + ",";
                    lstdepartmentgroupid += Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentid == Utilities.Util_TypeConvertParse.ToInt64(lstPhongCheck[i].ToString())).FirstOrDefault().departmentgroupid.ToString() + ",";
                }
                mbp_departmentid += lstPhongCheck[lstPhongCheck.Count - 1] + ") ";
                lstdepartmentgroupid += Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentid == Utilities.Util_TypeConvertParse.ToInt64(lstPhongCheck[lstPhongCheck.Count - 1].ToString())).FirstOrDefault().departmentgroupid.ToString() + " ";

                string servicepricegroupcode = "";
                if (GlobalStore.lstOtherList_Global != null && GlobalStore.lstOtherList_Global.Count > 0)
                {
                    List<ClassCommon.ToolsOtherListDTO> lstOtherList = GlobalStore.lstOtherList_Global.Where(o => o.tools_othertypelistcode == "REPORT_12_NSDD").ToList();
                    if (lstOtherList != null && lstOtherList.Count > 0)
                    {
                        servicepricegroupcode = lstOtherList[0].tools_otherlistvalue;
                    }
                }

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009") //thu thuat noio soi da day
                {
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) ";
                    serf_nhomdichvu = " and servicepricegroupcode = '" + servicepricegroupcode + "' ";
                    mbp_departmentid = "";
                    lstdepartmentgroupid = "51";
                }
                else
                {
                    serf_nhomdichvu = " and servicepricegroupcode <> '" + servicepricegroupcode + "' ";
                }

                //load dât ve nguoi thuc hien
                LoadDataNguoiThucHien(lstdepartmentgroupid);



                //Xet nghiem
                if (cboLoaiBaoCao.EditValue.ToString() == "XN")
                {
                    sql_laydulieu = "SELECT ROW_NUMBER () OVER (ORDER BY " + _sapxeptheo + ") as stt, A.patientid, A.vienphiid, A.medicalrecordid, hsba.patientname, (case when hsba.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as YEAR_NAM, (case hsba.gioitinhcode when '02' then to_char(hsba.birthday, 'yyyy') else '' end) as YEAR_NU, bh.bhytcode, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, KCHD.departmentgroupname AS khoachidinh, pcd.departmentname as phongchidinh, pth.departmentname as phongthuchien, A.NGAY_CHIDINH, (case when A.maubenhphamdate_thuchien<>'0001-01-01 00:00:00' then A.maubenhphamdate_thuchien end) as ngay_tiepnhan, (case when A.maubenhphamfinishdate<>'0001-01-01 00:00:00' then A.maubenhphamfinishdate end) as ngay_thuchien, (case when A.maubenhphamfinishdate<>'0001-01-01 00:00:00' then A.maubenhphamfinishdate end) as ngay_thuchien_tp, KCD.departmentgroupname AS khoachuyenden, KRV.departmentgroupname AS khoaravien, A.chandoan as cd_chidinh, A.maubenhphamid, A.bhyt_groupcode, A.sophieu, A.thuchienclsid, A.servicepriceid, A.servicepricecode, A.servicepricename, A.LOAIPTTT_DB, A.LOAIPTTT_L1, A.LOAIPTTT_L2, A.LOAIPTTT_L3, A.LOAIPTTT, A.SOLUONG, A.SERVICEPRICEFEE, A.TYLE, round(cast(A.THUOC_TRONGGOI as numeric),0) AS THUOC_TRONGGOI, round(cast(A.VATTU_TRONGGOI as numeric),0) AS VATTU_TRONGGOI, round(cast(A.chiphikhac as numeric),0) AS chiphikhac, (A.servicepricefee * A.soluong) as thanhtien, round(cast(((A.servicepricefee * A.soluong) - COALESCE(A.THUOC_TRONGGOI,0) - COALESCE(A.VATTU_TRONGGOI,0) - COALESCE(A.chiphikhac,0) " + chiachobacsi + " ) as numeric),0) as lai, ntkq_cc.usercode as mochinh_idbs, ntkq_cc.username AS mochinh_tenbs, (A.MOCHINH_TIEN * (A.TYLE/100)) AS MOCHINH_TIEN, A.gayme_idbs, GM.username AS GAYME_TENBS, (A.GAYME_TIEN * (A.TYLE/100)) AS GAYME_TIEN, A.phu1_idbs, P1.username AS PHU1_TENBS, (A.PHU1_TIEN * (A.TYLE/100)) AS PHU1_TIEN, A.phu2_idbs, P2.username AS PHU2_TENBS, (A.PHU2_TIEN * (A.TYLE/100)) AS PHU2_TIEN, A.giupviec1_idbs, GV1.username AS GIUPVIEC1_TENBS, (A.GIUPVIEC1_TIEN * (A.TYLE/100)) AS GIUPVIEC1_TIEN, (A.giupviec1nsdd_tien * (A.TYLE/100)) AS giupviec1nsdd_tien, A.giupviec2_idbs, GV2.username AS GIUPVIEC2_TENBS, (A.GIUPVIEC2_TIEN * (A.TYLE/100)) AS GIUPVIEC2_TIEN, A.NGAY_VAOVIEN, A.NGAY_RAVIEN, A.NGAY_THANHTOAN, ntkq_cc.username as nguoitraketqua, A.nguoinhapthuchien, coalesce(A.duyetpttt_stt,0) as duyetpttt_stt, (case A.duyetpttt_stt when 1 then 'Đã gửi YC' when 2 then 'Đã tiếp nhận YC' when 3 then 'Đã duyệt PTTT' when 99 then 'Đã khóa' else 'Chưa gửi YC' end) as duyetpttt_sttname, (case when A.duyetpttt_stt in (3,99) then A.duyetpttt_date end) as duyetpttt_date, (case when A.duyetpttt_stt in (3,99) then A.duyetpttt_usercode end) as duyetpttt_usercode, (case when A.duyetpttt_stt in (3,99) then A.duyetpttt_username end) as duyetpttt_username FROM ( SELECT vp.patientid, vp.vienphiid, ser.medicalrecordid, vp.hosobenhanid, vp.bhytid, ser.departmentgroupid as khoachidinh, ser.departmentid as phongchidinh, ser.servicepricedate as NGAY_CHIDINH, ser.maubenhphamid, ser.bhyt_groupcode, mbp.sophieu, mbp.departmentid_des, mbp.maubenhphamfinishdate, mbp.maubenhphamdate_thuchien, mbp.usertrakq, mbp.chandoan, (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenden, (case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, cls.thuchienclsid, ser.servicepriceid, ser.servicepricecode, (case ser.loaidoituong when 0 then ser.servicepricename_bhyt when 1 then ser.servicepricename_nhandan else ser.servicepricename end) as servicepricename, (case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as SERVICEPRICEFEE, (case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as TYLE, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) as THUOC_TRONGGOI, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle')) as VATTU_TRONGGOI, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as LOAIPTTT, (case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as LOAIPTTT_DB, (case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as LOAIPTTT_L1, (case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as LOAIPTTT_L2, (case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as LOAIPTTT_L3, ser.soluong as SOLUONG, ((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as MOCHINH_TIEN, cls.bacsigayme as gayme_idbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as GAYME_TIEN, cls.phumo1 as phu1_idbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 when 5 then 60000 when 6 then 27000 else 0 end) * ser.soluong) as PHU1_TIEN, cls.phumo2 as phu2_idbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 0 when 4 then 0 else 0 end) * ser.soluong) as PHU2_TIEN, cls.phumo3 as giupviec1_idbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 when 5 then 36000 when 6 then 0 when 7 then 9000 when 8 then 4500 else 0 end) * ser.soluong) as GIUPVIEC1_TIEN, ((case serf.pttt_loaiid when 6 then 21000 else 0 end) * ser.soluong) as giupviec1nsdd_tien, cls.phumo4 as giupviec2_idbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 0 else 0 end) * ser.soluong) as GIUPVIEC2_TIEN, vp.vienphidate as NGAY_VAOVIEN, (case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as NGAY_RAVIEN, (case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as NGAY_THANHTOAN, cls.tools_username as nguoinhapthuchien, ser.duyetpttt_stt, ser.duyetpttt_date, ser.duyetpttt_username, ser.duyetpttt_usercode FROM (select vienphiid,servicepriceid,departmentgroupid,departmentid,servicepricedate,maubenhphamid,servicepricecode,servicepricename,loaidoituong,medicalrecordid,servicepricename_bhyt,servicepricename_nhandan,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid,duyetpttt_stt,duyetpttt_date,duyetpttt_username,duyetpttt_usercode,bhyt_groupcode from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN','03XN','07KTC','06PTTT') " + tieuchi_date_ser + _trangthaipttt + ") ser "+_join_thuchiencls+" (select servicepriceid,thuchienclsid,bacsigayme,phumo1,phumo2,phumo3,phumo4,tools_username from thuchiencls where 1=1 "+ tieuchi_date_thuchien + ") cls on cls.servicepriceid=ser.servicepriceid inner join (select servicepricecode, pttt_loaiid from servicepriceref where servicegrouptype in (2,3) and bhyt_groupcode in ('04CDHA','05TDCN','03XN','07KTC','06PTTT') " + serf_nhomdichvu + serf_pttt_loaiid + ") serf on serf.servicepricecode=ser.servicepricecode inner join (select patientid,vienphiid,hosobenhanid,bhytid,vienphistatus,departmentgroupid,vienphidate,vienphistatus_vp,vienphidate_ravien,duyet_ngayduyet_vp from vienphi " + tieuchi_date_vp + ") vp on vp.vienphiid=ser.vienphiid INNER JOIN (select maubenhphamid,sophieu,departmentid_des,maubenhphamfinishdate,maubenhphamdate_thuchien,usertrakq,chandoan from maubenhpham where maubenhphamgrouptype in (0,1) " + tieuchi_date_tiepnhan + mbp_departmentid + ") mbp on mbp.maubenhphamid=ser.maubenhphamid ) A INNER JOIN (select hosobenhanid, patientname, gioitinhcode, birthday, bhytcode, hc_sonha, hc_thon, hc_xacode, hc_xaname, hc_huyencode, hc_huyenname, hc_tinhcode, hc_tinhname, hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=A.hosobenhanid INNER JOIN bhyt bh on bh.bhytid=A.bhytid LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCHD ON KCHD.departmentgroupid=A.khoachidinh LEFT JOIN (select departmentid, departmentname from department where departmenttype in (2,3,9,6,7)) pcd ON pcd.departmentid=A.phongchidinh LEFT JOIN (select departmentid, departmentname from department where departmenttype in (6,7)) pth ON pth.departmentid=A.departmentid_des LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCD ON KCD.departmentgroupid=A.khoachuyenden LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=A.khoaravien LEFT JOIN nhompersonnel gm ON gm.userhisid=A.gayme_idbs LEFT JOIN nhompersonnel p1 ON p1.userhisid=A.phu1_idbs LEFT JOIN nhompersonnel p2 ON p2.userhisid=A.phu2_idbs LEFT JOIN nhompersonnel gv1 ON gv1.userhisid=A.giupviec1_idbs LEFT JOIN nhompersonnel gv2 ON gv2.userhisid=A.giupviec2_idbs LEFT JOIN nhompersonnel ntkq_cc ON ntkq_cc.userhisid=A.usertrakq;";
                }
                else //CDHA
                {
                    sql_laydulieu = "SELECT ROW_NUMBER () OVER (ORDER BY " + _sapxeptheo + ") as stt, A.patientid, A.vienphiid, A.medicalrecordid, hsba.patientname, (case when hsba.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as YEAR_NAM, (case hsba.gioitinhcode when '02' then to_char(hsba.birthday, 'yyyy') else '' end) as YEAR_NU, bh.bhytcode, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, KCHD.departmentgroupname AS khoachidinh, pcd.departmentname as phongchidinh, pth.departmentname as phongthuchien, A.NGAY_CHIDINH, (case when A.maubenhphamdate_thuchien<>'0001-01-01 00:00:00' then A.maubenhphamdate_thuchien end) as ngay_tiepnhan, (case when A.maubenhphamfinishdate<>'0001-01-01 00:00:00' then A.maubenhphamfinishdate end) as ngay_thuchien, (case when A.servicetimetrakq<>'0001-01-01 00:00:00' then A.servicetimetrakq else ((case when A.maubenhphamfinishdate<>'0001-01-01 00:00:00' then A.maubenhphamfinishdate end)) end) as ngay_thuchien_tp, KCD.departmentgroupname AS khoachuyenden, KRV.departmentgroupname AS khoaravien, A.chandoan as cd_chidinh, A.maubenhphamid, A.bhyt_groupcode, A.sophieu, A.thuchienclsid, A.servicepriceid, A.servicepricecode, A.servicepricename, A.LOAIPTTT_DB, A.LOAIPTTT_L1, A.LOAIPTTT_L2, A.LOAIPTTT_L3, A.LOAIPTTT, A.SOLUONG, A.SERVICEPRICEFEE, A.TYLE, round(cast(A.THUOC_TRONGGOI as numeric),0) AS THUOC_TRONGGOI, round(cast(A.VATTU_TRONGGOI as numeric),0) AS VATTU_TRONGGOI, round(cast(A.chiphikhac as numeric),0) AS chiphikhac, (A.servicepricefee * A.soluong) as thanhtien, round(cast(((A.servicepricefee * A.soluong) - COALESCE(A.THUOC_TRONGGOI,0) - COALESCE(A.VATTU_TRONGGOI,0) - COALESCE(A.chiphikhac,0) " + chiachobacsi + " ) as numeric),0) as lai, COALESCE(ntkq.usercode,ntkq_cc.usercode) as mochinh_idbs, COALESCE(ntkq.username,ntkq_cc.username) AS mochinh_tenbs, (A.MOCHINH_TIEN * (A.TYLE/100)) AS MOCHINH_TIEN, A.gayme_idbs, GM.username AS GAYME_TENBS, (A.GAYME_TIEN * (A.TYLE/100)) AS GAYME_TIEN, A.phu1_idbs, P1.username AS PHU1_TENBS, (A.PHU1_TIEN * (A.TYLE/100)) AS PHU1_TIEN, A.phu2_idbs, P2.username AS PHU2_TENBS, (A.PHU2_TIEN * (A.TYLE/100)) AS PHU2_TIEN, A.giupviec1_idbs, GV1.username AS GIUPVIEC1_TENBS, (A.GIUPVIEC1_TIEN * (A.TYLE/100)) AS GIUPVIEC1_TIEN, (A.giupviec1nsdd_tien * (A.TYLE/100)) AS giupviec1nsdd_tien, A.giupviec2_idbs, GV2.username AS GIUPVIEC2_TENBS, (A.GIUPVIEC2_TIEN * (A.TYLE/100)) AS GIUPVIEC2_TIEN, A.NGAY_VAOVIEN, A.NGAY_RAVIEN, A.NGAY_THANHTOAN, COALESCE(ntkq.username,ntkq_cc.username) as nguoitraketqua, A.nguoinhapthuchien, coalesce(A.duyetpttt_stt,0) as duyetpttt_stt, (case A.duyetpttt_stt when 1 then 'Đã gửi YC' when 2 then 'Đã tiếp nhận YC' when 3 then 'Đã duyệt PTTT' when 99 then 'Đã khóa' else 'Chưa gửi YC' end) as duyetpttt_sttname, (case when A.duyetpttt_stt in (3,99) then A.duyetpttt_date end) as duyetpttt_date, (case when A.duyetpttt_stt in (3,99) then A.duyetpttt_usercode end) as duyetpttt_usercode, (case when A.duyetpttt_stt in (3,99) then A.duyetpttt_username end) as duyetpttt_username FROM ( SELECT vp.patientid, vp.vienphiid, ser.medicalrecordid, vp.hosobenhanid, vp.bhytid, ser.departmentgroupid as khoachidinh, ser.departmentid as phongchidinh, ser.servicepricedate as NGAY_CHIDINH, ser.maubenhphamid, ser.bhyt_groupcode, mbp.sophieu, mbp.departmentid_des, mbp.maubenhphamfinishdate, mbp.maubenhphamdate_thuchien, se.servicetimetrakq, se.serviceusertrakq, mbp.usertrakq, mbp.chandoan, (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenden, (case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, cls.thuchienclsid, ser.servicepriceid, ser.servicepricecode, (case ser.loaidoituong when 0 then ser.servicepricename_bhyt when 1 then ser.servicepricename_nhandan else ser.servicepricename end) as servicepricename, (case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as SERVICEPRICEFEE, (case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as TYLE, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) as THUOC_TRONGGOI, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle')) as VATTU_TRONGGOI, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as LOAIPTTT, (case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as LOAIPTTT_DB, (case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as LOAIPTTT_L1, (case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as LOAIPTTT_L2, (case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as LOAIPTTT_L3, ser.soluong as SOLUONG, ((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as MOCHINH_TIEN, cls.bacsigayme as gayme_idbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as GAYME_TIEN, cls.phumo1 as phu1_idbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 when 5 then 60000 when 6 then 27000 else 0 end) * ser.soluong) as PHU1_TIEN, cls.phumo2 as phu2_idbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 0 when 4 then 0 else 0 end) * ser.soluong) as PHU2_TIEN, cls.phumo3 as giupviec1_idbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 when 5 then 36000 when 6 then 0 when 7 then 9000 when 8 then 4500 else 0 end) * ser.soluong) as GIUPVIEC1_TIEN, ((case serf.pttt_loaiid when 6 then 21000 else 0 end) * ser.soluong) as giupviec1nsdd_tien, cls.phumo4 as giupviec2_idbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 0 else 0 end) * ser.soluong) as GIUPVIEC2_TIEN, vp.vienphidate as NGAY_VAOVIEN, (case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as NGAY_RAVIEN, (case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as NGAY_THANHTOAN, cls.tools_username as nguoinhapthuchien, ser.duyetpttt_stt, ser.duyetpttt_date, ser.duyetpttt_username, ser.duyetpttt_usercode FROM (select vienphiid,servicepriceid,departmentgroupid,departmentid,servicepricedate,maubenhphamid,servicepricecode,servicepricename,loaidoituong,medicalrecordid,servicepricename_bhyt,servicepricename_nhandan,servicepricemoney_bhyt,servicepricemoney_nhandan,servicepricemoney,loaipttt,soluong,chiphidauvao,chiphimaymoc,chiphipttt,mayytedbid,duyetpttt_stt,duyetpttt_date,duyetpttt_username,duyetpttt_usercode,bhyt_groupcode from serviceprice where bhyt_groupcode in ('04CDHA','05TDCN','03XN','07KTC','06PTTT') " + tieuchi_date_ser + _trangthaipttt + ") ser " + _join_thuchiencls + " (select servicepriceid,thuchienclsid,bacsigayme,phumo1,phumo2,phumo3,phumo4,tools_username from thuchiencls where 1=1 " + tieuchi_date_thuchien + ") cls on cls.servicepriceid=ser.servicepriceid inner join (select servicepricecode, pttt_loaiid from servicepriceref where servicegrouptype in (2,3) and bhyt_groupcode in ('04CDHA','05TDCN','03XN','07KTC','06PTTT') " + serf_nhomdichvu + serf_pttt_loaiid + ") serf on serf.servicepricecode=ser.servicepricecode inner join (select patientid,vienphiid,hosobenhanid,bhytid,vienphistatus,departmentgroupid,vienphidate,vienphistatus_vp,vienphidate_ravien,duyet_ngayduyet_vp from vienphi " + tieuchi_date_vp + ") vp on vp.vienphiid=ser.vienphiid INNER JOIN (select maubenhphamid,sophieu,departmentid_des,maubenhphamfinishdate,maubenhphamdate_thuchien,usertrakq,chandoan from maubenhpham where maubenhphamgrouptype in (0,1) " + tieuchi_date_tiepnhan + mbp_departmentid + ") mbp on mbp.maubenhphamid=ser.maubenhphamid INNER JOIN (select servicepriceid,servicetimetrakq,serviceusertrakq from service where servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode)) se on se.servicepriceid=ser.servicepriceid ) A INNER JOIN (select hosobenhanid, patientname, gioitinhcode, birthday, bhytcode, hc_sonha, hc_thon, hc_xacode, hc_xaname, hc_huyencode, hc_huyenname, hc_tinhcode, hc_tinhname, hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=A.hosobenhanid INNER JOIN bhyt bh on bh.bhytid=A.bhytid LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCHD ON KCHD.departmentgroupid=A.khoachidinh LEFT JOIN (select departmentid, departmentname from department where departmenttype in (2,3,9,6,7)) pcd ON pcd.departmentid=A.phongchidinh LEFT JOIN (select departmentid, departmentname from department where departmenttype in (6,7)) pth ON pth.departmentid=A.departmentid_des LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCD ON KCD.departmentgroupid=A.khoachuyenden LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=A.khoaravien LEFT JOIN nhompersonnel gm ON gm.userhisid=A.gayme_idbs LEFT JOIN nhompersonnel p1 ON p1.userhisid=A.phu1_idbs LEFT JOIN nhompersonnel p2 ON p2.userhisid=A.phu2_idbs LEFT JOIN nhompersonnel gv1 ON gv1.userhisid=A.giupviec1_idbs LEFT JOIN nhompersonnel gv2 ON gv2.userhisid=A.giupviec2_idbs LEFT JOIN nhompersonnel ntkq ON ntkq.usercode=A.serviceusertrakq LEFT JOIN nhompersonnel ntkq_cc ON ntkq_cc.userhisid=A.usertrakq;";
                }
                dataBCPTTT = condb.GetDataTable_HIS(sql_laydulieu);
                if (dataBCPTTT != null && dataBCPTTT.Rows.Count > 0)
                {
                    gridControlDataBCPTTT.DataSource = dataBCPTTT;
                }
                else
                {
                    gridControlDataBCPTTT.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #endregion

        #region Load nguoi thuc hien
        private void LoadDataNguoiThucHien(string lstdepartmentgroupid)
        {
            try
            {
                if (this.dataNguoiThucHien == null || this.dataNguoiThucHien.Rows.Count <= 0)
                {
                    string getnguoithuchien = "select 0 as userhisid, '' as usercode, '' as username, '' as usercodename union all select A.userhisid, A.usercode, A.username, A.usercodename from (select nv.userhisid, nv.usercode, nv.username, (nv.usercode || ' - ' || nv.username) as usercodename from nhompersonnel nv inner join (select usercode, departmentid from tbldepartment) ude on ude.usercode=nv.usercode inner join (select departmentid from department where departmentgroupid in (" + lstdepartmentgroupid + ")) de on de.departmentid=ude.departmentid group by nv.userhisid, nv.usercode, nv.username order by nv.username) A; ";
                    this.dataNguoiThucHien = condb.GetDataTable_HIS(getnguoithuchien);
                }

                //repositoryItemGridLookUp_MoChinh.DataSource = this.dataNguoiThucHien;
                //repositoryItemGridLookUp_MoChinh.DisplayMember = "usercodename";
                //repositoryItemGridLookUp_MoChinh.ValueMember = "usercode";

                repositoryItemGridLookUp_GayMe.DataSource = dataNguoiThucHien;
                repositoryItemGridLookUp_GayMe.DisplayMember = "usercodename";
                repositoryItemGridLookUp_GayMe.ValueMember = "userhisid";

                repositoryItemGridLookUp_Phu1.DataSource = dataNguoiThucHien;
                repositoryItemGridLookUp_Phu1.DisplayMember = "usercodename";
                repositoryItemGridLookUp_Phu1.ValueMember = "userhisid";

                repositoryItemGridLookUp_Phu2.DataSource = dataNguoiThucHien;
                repositoryItemGridLookUp_Phu2.DisplayMember = "usercodename";
                repositoryItemGridLookUp_Phu2.ValueMember = "userhisid";

                repositoryItemGridLookUp_GiupViec1.DataSource = dataNguoiThucHien;
                repositoryItemGridLookUp_GiupViec1.DisplayMember = "usercodename";
                repositoryItemGridLookUp_GiupViec1.ValueMember = "userhisid";

                repositoryItemGridLookUp_GiupViec2.DataSource = dataNguoiThucHien;
                repositoryItemGridLookUp_GiupViec2.DisplayMember = "usercodename";
                repositoryItemGridLookUp_GiupViec2.ValueMember = "userhisid";
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Hien Thi nut In xuat Excel
        private void EnableAndDisableNutIn()
        {
            try
            {
                if (cboTrangThai.Text == "Đã duyệt PTTT" || CheckPermission.ChkPerModule("SYS_05") || CheckPermission.ChkPerModule("THAOTAC_06"))
                {
                    dropDownPrint.Enabled = true;
                }
                else
                {
                    dropDownPrint.Enabled = false;
                }

                if (CheckPermission.ChkPerModule("SYS_05") || (CheckPermission.ChkPerModule("THAOTAC_07") && cboTrangThai.Text == "Đã duyệt PTTT") || CheckPermission.ChkPerModule("THAOTAC_06"))
                {
                    dropDownExport.Enabled = true;
                }
                else
                {
                    dropDownExport.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Duyet va go duyet
        private void DuyetPTTTDaChon_Click(object sender, EventArgs e)
        {
            try
            {
                List<ServicepriceDuyetPTTTDTO> lstServicepriceids = GetIdCollection();
                if (lstServicepriceids != null && lstServicepriceids.Count > 0)
                {
                    List<ServicepriceDuyetPTTTDTO> _lstDuyetPTTT_User = lstServicepriceids.Where(o => o.duyetpttt_stt == 0).ToList();
                    if (_lstDuyetPTTT_User != null && _lstDuyetPTTT_User.Count > 0)
                    {
                        string _sqlUpdate_Duyet = "UPDATE serviceprice SET duyetpttt_stt=1, duyetpttt_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', duyetpttt_usercode='" + Base.SessionLogin.SessionUsercode + "', duyetpttt_username='" + Base.SessionLogin.SessionUsername + "' WHERE servicepriceid in (" + ConvertListObjToListString(_lstDuyetPTTT_User) + ");";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate_Duyet))
                        {
                            this._duyetPTTT = true;
                            MessageBox.Show("Duyệt PTTT thành công SL=" + _lstDuyetPTTT_User.Count + "/" + lstServicepriceids.Count, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LayDuLieuBaoCao_ChayMoi();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_DA_DUOC_DUYET_PTTT);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void GoDuyetPTTTDaChon_Click(object sender, EventArgs e)
        {
            try
            {
                List<ServicepriceDuyetPTTTDTO> lstServicepriceids = GetIdCollection();
                if (lstServicepriceids != null && lstServicepriceids.Count > 0)
                {
                    List<ServicepriceDuyetPTTTDTO> _lstGoDuyetPTTT_User = lstServicepriceids.Where(o => o.duyetpttt_stt == 1 && o.duyetpttt_usercode == Base.SessionLogin.SessionUsercode).ToList();
                    if (_lstGoDuyetPTTT_User != null && _lstGoDuyetPTTT_User.Count > 0)
                    {
                        string _sqlUpdate_GoDuyet = "UPDATE serviceprice SET duyetpttt_stt=0, duyetpttt_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', duyetpttt_usercode='" + Base.SessionLogin.SessionUsercode + "', duyetpttt_username='" + Base.SessionLogin.SessionUsername + "' WHERE servicepriceid in (" + ConvertListObjToListString(_lstGoDuyetPTTT_User) + ");";
                        if (condb.ExecuteNonQuery_HIS(_sqlUpdate_GoDuyet))
                        {
                            this._duyetPTTT = true;
                            MessageBox.Show("Gỡ duyệt PTTT thành công SL=" + _lstGoDuyetPTTT_User.Count + "/" + lstServicepriceids.Count, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LayDuLieuBaoCao_ChayMoi();
                        }
                        else
                        {
                            ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.DICH_VU_CHUA_DUOC_DUYET_HOAC_NGUOI_KHAC_DUYET);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.CHUA_CHON_BAN_GHI_NAO);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Process
        private List<ServicepriceDuyetPTTTDTO> GetIdCollection()
        {
            List<ServicepriceDuyetPTTTDTO> IDs = new List<ServicepriceDuyetPTTTDTO>();
            for (int i = 0; i < helper.SelectedCount; i++)
            {
                ServicepriceDuyetPTTTDTO _serviceID = new ServicepriceDuyetPTTTDTO();
                _serviceID.servicepriceid = (helper.GetSelectedRow(i) as DataRowView)["servicepriceid"].ToString();
                _serviceID.duyetpttt_stt = Utilities.Util_TypeConvertParse.ToInt16((helper.GetSelectedRow(i) as DataRowView)["duyetpttt_stt"].ToString());
                _serviceID.vienphiid = Utilities.Util_TypeConvertParse.ToInt64((helper.GetSelectedRow(i) as DataRowView)["vienphiid"].ToString());
                _serviceID.maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64((helper.GetSelectedRow(i) as DataRowView)["maubenhphamid"].ToString());
                _serviceID.bhyt_groupcode = (helper.GetSelectedRow(i) as DataRowView)["bhyt_groupcode"].ToString();
                IDs.Add(_serviceID);
            }
            return IDs;
        }

        private string ConvertListObjToListString(List<ServicepriceDuyetPTTTDTO> IDs)
        {
            string query = "";
            try
            {
                int number = 0;
                foreach (var item in IDs)
                {
                    if (number > 0)
                    {
                        query = query + "," + "'" + item.servicepriceid + "'";
                    }
                    else
                    {
                        query = "'" + item.servicepriceid + "'";
                    }
                    number++;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return query;
        }

        #endregion


        #region Process - Cập nhật tools_duyet_pttt
        private void CapNhatTools_DuyetPTTT(List<ServicepriceDuyetPTTTDTO> lstServicepriceids, int _duyetpttt_stt)
        {
            try
            {
                string _sqlCapNhatToolsDuyet = "";

                foreach (var item_ser in lstServicepriceids)
                {
                    string _sqlKTDaInsert = "SELECT * FROM tools_duyet_pttt WHERE servicepriceid='" + item_ser.servicepriceid + "'; ";
                    DataTable _DSDVInsert = condb.GetDataTable_HIS(_sqlKTDaInsert);
                    if (_DSDVInsert != null && _DSDVInsert.Rows.Count > 0) //cap nhat
                    {
                        string _update_user = "";
                        switch (_duyetpttt_stt)
                        {
                            case 1:
                                {
                                    _update_user = " , gui_usercode='" + Base.SessionLogin.SessionUsercode + "', gui_username='" + Base.SessionLogin.SessionUsername + "', gui_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                                    break;
                                }
                            case 2:
                                {
                                    _update_user = " , tiepnhan_usercode='" + Base.SessionLogin.SessionUsercode + "', tiepnhan_username='" + Base.SessionLogin.SessionUsername + "', tiepnhan_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                                    break;
                                }
                            case 3:
                                {
                                    _update_user = " , duyet_usercode='" + Base.SessionLogin.SessionUsercode + "', duyet_username='" + Base.SessionLogin.SessionUsername + "', duyet_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                                    break;
                                }
                            case 99:
                                {
                                    _update_user = " , khoa_usercode='" + Base.SessionLogin.SessionUsercode + "', khoa_username='" + Base.SessionLogin.SessionUsername + "', khoa_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
                                    break;
                                }
                            default:
                                break;
                        }
                        _sqlCapNhatToolsDuyet += " UPDATE tools_duyet_pttt SET duyetpttt_stt='" + _duyetpttt_stt + "' " + _update_user + " WHERE servicepriceid='" + item_ser.servicepriceid + "'; ";
                    }
                    else //them moi
                    {
                        switch (_duyetpttt_stt)
                        {
                            case 1:
                                {
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt,gui_usercode,gui_username,gui_date) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                    break;
                                }
                            case 2:
                                {
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt,tiepnhan_usercode,tiepnhan_username,tiepnhan_date) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                    break;
                                }
                            case 3:
                                {
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt,duyet_usercode,duyet_username,duyet_date) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + "''" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                    break;
                                }
                            case 99:
                                {
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt,khoa_usercode,khoa_username,khoa_date) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "', '" + Base.SessionLogin.SessionUsercode + "', '" + Base.SessionLogin.SessionUsername + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                    break;
                                }
                            default:
                                {
                                    _sqlCapNhatToolsDuyet += " INSERT INTO tools_duyet_pttt(servicepriceid,vienphiid,maubenhphamid,bhyt_groupcode,duyetpttt_stt) VALUES('" + item_ser.servicepriceid + "', '" + item_ser.vienphiid + "', '" + item_ser.maubenhphamid + "', '" + item_ser.bhyt_groupcode + "', '" + _duyetpttt_stt + "'); ";
                                }
                                break;
                        }
                    }
                }
                condb.ExecuteNonQuery_HIS(_sqlCapNhatToolsDuyet);
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private int KiemTraTrangThaiKhoaGuiPTTT()
        {
            int result = 0;
            try
            {
                string _sqlTrangThai = "SELECT toolsoptionvalue FROM tools_option WHERE toolsoptioncode='REPORT_12_KhoaGuiYeuCau';";
                DataTable _dataTrangThai = condb.GetDataTable_MeL(_sqlTrangThai);
                if (_dataTrangThai != null && _dataTrangThai.Rows.Count > 0)
                {
                    result = Utilities.Util_TypeConvertParse.ToInt16(_dataTrangThai.Rows[0]["toolsoptionvalue"].ToString());
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }

        #endregion
    }
}
