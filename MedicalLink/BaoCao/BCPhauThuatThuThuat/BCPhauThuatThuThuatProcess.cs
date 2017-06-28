using DevExpress.Utils.Menu;
using DevExpress.XtraSplashScreen;
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
    public partial class BCPhauThuatThuThuat : UserControl
    {
        internal void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                string sql_laydulieu = "";

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string tieuchi_date = "";
                string ser_departmentid = "";
                string serf_pttt_loaiid = "";
                string chiachobacsi = "";
                string baocaotungloai = "";

                if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_date = " vp.duyet_ngayduyet_vp>='" + tungay + "' and vp.duyet_ngayduyet_vp<='" + denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    tieuchi_date = " pttt.phauthuatthuthuatdate>='" + tungay + "' and pttt.phauthuatthuthuatdate<='" + denngay + "' ";
                }
                else
                {
                    tieuchi_date = " ser.servicepricedate>='" + tungay + "' and ser.servicepricedate<='" + denngay + "' ";
                }

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_001")//phau thuat - lay phong mo phien va phong mo cap cuu: ser.departmentid in (285,34)
                {
                    ser_departmentid = " ser.departmentid in (285,34) ";
                    serf_pttt_loaiid = " pttt_loaiid in (1,2,3,4) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100)) - (A.GAYME_TIEN * (A.TYLE/100)) - (A.PHU1_TIEN * (A.TYLE/100)) - (A.PHU2_TIEN * (A.TYLE/100)) - (A.GIUPVIEC1_TIEN * (A.TYLE/100)) - (A.GIUPVIEC2_TIEN * (A.TYLE/100)) - (A.moimochinh_tien * (A.tyle/100)) - (A.moigayme_tien * (A.tyle/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as mochinh_tien, (case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, (case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, pttt.bacsigayme as gayme_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as gayme_tien, pttt.phume2 as moigayme_tenbs, (case when pttt.phume2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as phu1_tien, pttt.phumo2 as phu2_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 else 0 end) * ser.soluong) as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 else 0 end) * ser.soluong) as giupviec1_tien, pttt.phumo4 as giupviec2_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 else 0 end) * ser.soluong) as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_002")//phau thuat - lay khoa tai mui hong: ser.departmentid in (122); ser.departmengrouptid in (10)
                {
                    ser_departmentid = " ser.departmentid=122 ";
                    serf_pttt_loaiid = " pttt_loaiid in (1,2,3,4) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.PHU2_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100))-(A.GIUPVIEC2_TIEN * (A.TYLE/100)) - (A.moimochinh_tien * (A.tyle/100)) - (A.moigayme_tien * (A.tyle/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as mochinh_tien, (case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, (case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, 0 as gayme_tenbs, 0 as gayme_tien, 0 as moigayme_tenbs, 0 as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as phu1_tien, pttt.phumo2 as phu2_tenbs, ((case serf.pttt_loaiid when 2 then 90000 else 0 end) * ser.soluong) as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 2 then 70000 when 3 then 30000 when 4 then 15000 else 0 end) * ser.soluong) as giupviec1_tien, pttt.phumo4 as giupviec2_tenbs, ((case serf.pttt_loaiid when 2 then 70000 when 3 then 30000 else 0 end) * ser.soluong) as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_003") //phau thuat - lay khoa rang ham mat: ser.departmentid in (116); ser.departmengrouptid in (9)
                {
                    ser_departmentid = " ser.departmentid=116 ";
                    serf_pttt_loaiid = " pttt_loaiid in (1,2,3,4) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) - (A.moimochinh_tien * (A.tyle/100)) - (A.moigayme_tien * (A.tyle/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as mochinh_tien, (case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, (case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, 0 as gayme_tenbs, 0 as gayme_tien, 0 as moigayme_tenbs, 0 as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as phu1_tien, 0 as phu2_tenbs, 0 as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 else 0 end) * ser.soluong) as giupviec1_tien, 0 as giupviec2_tenbs, 0 as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_004") //phau thuat - lay Mổ Mắt_B_Điều Trị + phong kham mổ mat + BDT khoa mat, kkb-kham mat
                {
                    ser_departmentid = " ser.departmentid in (269,335,80,212) ";
                    serf_pttt_loaiid = " pttt_loaiid in (1,2,3,4) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GAYME_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) - (A.moimochinh_tien * (A.tyle/100)) - (A.moigayme_tien * (A.tyle/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as mochinh_tien, (case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, (case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, pttt.bacsigayme as gayme_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as gayme_tien, pttt.phume2 as moigayme_tenbs, (case when pttt.phume2>0 then ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 else 0 end) * ser.soluong) else 0 end) as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as phu1_tien, 0 as phu2_tenbs, 0 as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 else 0 end) * ser.soluong) as giupviec1_tien, 0 as giupviec2_tenbs, 0 as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_005") //phau thuat - khoa khac
                {
                    ser_departmentid = " ser.departmentid in (";
                    List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        ser_departmentid += lstPhongCheck[i] + ",";
                    }
                    ser_departmentid += lstPhongCheck[lstPhongCheck.Count - 1] + ") ";

                    serf_pttt_loaiid = " pttt_loaiid in (1,2,3,4) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GAYME_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.PHU2_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100))-(A.GIUPVIEC2_TIEN * (A.TYLE/100)) - (A.moimochinh_tien * (A.tyle/100)) - (A.moigayme_tien * (A.tyle/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as mochinh_tien, (case when pttt.phauthuatvien2>0 then pttt.phauthuatvien2 end) as moimochinh_tenbs, (case when pttt.phauthuatvien2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moimochinh_tien, pttt.bacsigayme as gayme_tenbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as gayme_tien, pttt.phume2 as moigayme_tenbs, (case when pttt.phume2>0 then ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 else 0 end) * ser.soluong) else 0 end) as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 else 0 end) * ser.soluong) as phu1_tien, pttt.phumo2 as phu2_tenbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 else 0 end) * ser.soluong) as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 else 0 end) * ser.soluong) as giupviec1_tien, pttt.phumo4 as giupviec2_tenbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 else 0 end) * ser.soluong) as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_006")// thu thuat - khoa mat + phong kham mat ser.departmentid in (80,212)
                {
                    ser_departmentid = " ser.departmentid in (80,212) ";
                    serf_pttt_loaiid = " pttt_loaiid in (5,6,7,8) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) ";
                    baocaotungloai = "  pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as mochinh_tien, 0 as moimochinh_tenbs, 0 as moimochinh_tien, 0 as gayme_tenbs, 0 as gayme_tien, 0 as moigayme_tenbs, 0 as moigayme_tien, 0 as phu1_tenbs, 0 as phu1_tien, 0 as phu2_tenbs, 0 as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 5 then 36000 when 6 then 21000 when 7 then 9000 when 8 then 4500 else 0 end) * ser.soluong) as giupviec1_tien, 0 as giupviec2_tenbs, 0 as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_007")//Báo cáo Thủ thuật - Các khoa khác (trừ khoa mắt & PK mắt): ser.departmentid not in (80,212)
                {
                    ser_departmentid = " ser.departmentid not in (80,212) ";
                    serf_pttt_loaiid = " pttt_loaiid in (5,6,7,8) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as mochinh_tien, 0 as moimochinh_tenbs, 0 as moimochinh_tien, 0 as gayme_tenbs, 0 as gayme_tien, 0 as moigayme_tenbs, 0 as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 5 then 60000 when 6 then 27000 else 0 end) * ser.soluong) as phu1_tien, 0 as phu2_tenbs, 0 as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 5 then 36000 when 7 then 9000 when 8 then 4500 else 0 end) * ser.soluong) as giupviec1_tien, 0 as giupviec2_tenbs, 0 as giupviec2_tien, ";
                }
                else if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_008")//Báo cáo Thủ thuật - khoa khac
                {
                    ser_departmentid = " ser.departmentid in (";
                    List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                    for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                    {
                        ser_departmentid += lstPhongCheck[i] + ",";
                    }
                    ser_departmentid += lstPhongCheck[lstPhongCheck.Count - 1] + ") ";
                    serf_pttt_loaiid = " pttt_loaiid in (5,6,7,8) ";
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) ";
                    baocaotungloai = " pttt.phauthuatvien as mochinh_tenbs, ((case serf.pttt_loaiid when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as mochinh_tien, 0 as moimochinh_tenbs, 0 as moimochinh_tien, 0 as gayme_tenbs, 0 as gayme_tien, 0 as moigayme_tenbs, 0 as moigayme_tien, pttt.phumo1 as phu1_tenbs, ((case serf.pttt_loaiid when 5 then 60000 when 6 then 27000 else 0 end) * ser.soluong) as phu1_tien, 0 as phu2_tenbs, 0 as phu2_tien, pttt.phumo3 as giupviec1_tenbs, ((case serf.pttt_loaiid when 5 then 36000 when 7 then 9000 when 8 then 4500 else 0 end) * ser.soluong) as giupviec1_tien, 0 as giupviec2_tenbs, 0 as giupviec2_tien, ";
                }

                if (chkChuaPhanLoaiPTTT.Checked)
                {
                    serf_pttt_loaiid = " pttt_loaiid=0";
                }
                //ngay 28/6/2017
                sql_laydulieu = "SELECT row_number () over (order by A.ngay_thuchien desc) as stt, A.patientid, A.vienphiid, hsbA.patientname, (case when hsbA.gioitinhcode='01' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nam, (case hsbA.gioitinhcode when '02' then to_char(hsbA.birthday, 'yyyy') else '' end) as year_nu, hsba.bhytcode, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, kchd.departmentgroupname as khoachidinh, pcd.departmentname as phongchidinh, A.ngay_chidinh, A.ngay_thuchien, kcd.departmentgroupname as khoachuyenden, krv.departmentgroupname as khoaravien, mbp.chandoan as cd_chidinh, A.servicepricecode, A.servicepricename, A.loaipttt_db, A.loaipttt_l1, A.loaipttt_l2, A.loaipttt_l3, A.loaipttt, A.soluong, A.servicepricefee, A.tyle, round(cast(A.thuoc_tronggoi as numeric),0) as thuoc_tronggoi, round(cast(A.vattu_tronggoi as numeric),0) as vattu_tronggoi, round(cast(A.chiphikhac as numeric),0) AS chiphikhac, (A.servicepricefee * A.soluong) as thanhtien, round(cast(((A.servicepricefee * A.soluong) - coalesce(A.thuoc_tronggoi,0) - coalesce(A.vattu_tronggoi,0) - coalesce(A.chiphikhac,0) " + chiachobacsi + " ) as numeric),0) as lai, mc.username as mochinh_tenbs, (A.mochinh_tien * (A.tyle/100)) as mochinh_tien, mmc.username as moimochinh_tenbs, (A.moimochinh_tien * (A.tyle/100)) as moimochinh_tien, gm.username as gayme_tenbs, (A.gayme_tien * (A.tyle/100)) as gayme_tien, mgm.username as moigayme_tenbs, (A.moigayme_tien * (A.tyle/100)) as moigayme_tien, p1.username as phu1_tenbs, (A.phu1_tien * (A.tyle/100)) as phu1_tien, p2.username as phu2_tenbs, (A.phu2_tien * (A.tyle/100)) as phu2_tien, gv1.username as giupviec1_tenbs, (A.giupviec1_tien * (A.tyle/100)) as giupviec1_tien, gv2.username as giupviec2_tenbs, (A.giupviec2_tien * (A.tyle/100)) as giupviec2_tien, A.ngay_vaovien, A.ngay_ravien, A.ngay_thanhtoan FROM (SELECT vp.patientid, vp.vienphiid, vp.hosobenhanid, vp.bhytid, ser.maubenhphamid, ser.departmentgroupid as khoachidinh, ser.departmentid as phongchidinh, ser.servicepricedate as ngay_chidinh, pttt.phauthuatthuthuatdate as ngay_thuchien, (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenden, (case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, ser.servicepricecode, ser.servicepricename, (case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as servicepricefee, (case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as tyle, (case when serf.tinhtoanlaigiadvktc=1 then (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) end) as thuoc_tronggoi, (case when serf.tinhtoanlaigiadvktc=1 then (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) else (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong in (2,5,7,9) and ser_dikem.bhyt_groupcode in ('10VT','101VTtrongDM','101VTtrongDMTT','102VTngoaiDM','103VTtyle')) end) as vattu_tronggoi, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as loaipttt, (case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as loaipttt_db, (case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as loaipttt_l1, (case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as loaipttt_l2, (case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as loaipttt_l3, ser.soluong as soluong, ((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, " + baocaotungloai + " vp.vienphidate as ngay_vaovien, (case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as ngay_ravien, (case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as ngay_thanhtoan FROM serviceprice ser left join phauthuatthuthuat pttt on pttt.servicepriceid=ser.servicepriceid inner join (select patientid, vienphiid, hosobenhanid, bhytid, vienphistatus, departmentgroupid, vienphidate, vienphidate_ravien, vienphistatus_vp, duyet_ngayduyet_vp from vienphi) vp on vp.vienphiid=ser.vienphiid inner join (select servicepricecode, tinhtoanlaigiadvktc, pttt_loaiid from servicepriceref where servicegrouptype=4 and bhyt_groupcode in ('06PTTT','07KTC') and " + serf_pttt_loaiid + ") serf on serf.servicepricecode=ser.servicepricecode WHERE ser.bhyt_groupcode in ('06PTTT','07KTC') and " + ser_departmentid + " and " + tieuchi_date + ") A INNER JOIN (select hosobenhanid, patientname, gioitinhcode, birthday, bhytcode, hc_sonha, hc_thon, hc_xacode, hc_xaname, hc_huyencode, hc_huyenname, hc_tinhcode, hc_tinhname, hc_quocgianame from hosobenhan) hsba on hsbA.hosobenhanid=A.hosobenhanid INNER JOIN (select maubenhphamid, chandoan from maubenhpham) mbp on mbp.maubenhphamid=A.maubenhphamid LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCHD ON KCHD.departmentgroupid=A.khoachidinh LEFT JOIN department pcd ON pcd.departmentid=A.phongchidinh LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCD ON KCD.departmentgroupid=A.khoachuyenden LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=A.khoaravien LEFT JOIN tools_tblnhanvien mc ON mc.userhisid=A.mochinh_tenbs LEFT JOIN tools_tblnhanvien mmc ON mmc.userhisid=A.moimochinh_tenbs LEFT JOIN tools_tblnhanvien gm ON gm.userhisid=A.gayme_tenbs LEFT JOIN tools_tblnhanvien mgm ON mgm.userhisid=A.moigayme_tenbs LEFT JOIN tools_tblnhanvien p1 ON p1.userhisid=A.phu1_tenbs LEFT JOIN tools_tblnhanvien p2 ON p2.userhisid=A.PHU2_TENBS LEFT JOIN tools_tblnhanvien gv1 ON gv1.userhisid=A.giupviec1_tenbs LEFT JOIN tools_tblnhanvien gv2 ON gv2.userhisid=A.giupviec2_tenbs;   ";

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


        #region Export


        #endregion
    }
}
