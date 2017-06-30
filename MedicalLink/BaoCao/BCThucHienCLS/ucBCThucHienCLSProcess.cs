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
    public partial class ucBCThucHienCLS : UserControl
    {
        internal void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                this.kiemtrasuadulieu = false;
                string sql_laydulieu = "";

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string tieuchi_date = "";
                string tieuchi_date_thuchien = "";
                string mbp_departmentid = "";
                string serf_pttt_loaiid = " and serf.pttt_loaiid>0 ";
                string lstdepartmentgroupid = "";
                string serf_nhomdichvu = "";

                string chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GAYME_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.PHU2_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100))-(A.GIUPVIEC2_TIEN * (A.TYLE/100)) ";

                if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_date = " and vp.duyet_ngayduyet_vp between '" + tungay + "' and '" + denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    tieuchi_date_thuchien = " and maubenhphamfinishdate between '" + tungay + "' and '" + denngay + "' ";
                }
                else
                {
                    tieuchi_date = " and ser.servicepricedate between '" + tungay + "' and '" + denngay + "' ";
                }

                if (chkChuaPhanLoaiPTTT.Checked)
                {
                    serf_pttt_loaiid = " and serf.pttt_loaiid=0 ";
                }

                mbp_departmentid = " and mbp.departmentid_des in (";
                List<Object> lstPhongCheck = chkcomboListDSPhong.Properties.Items.GetCheckedValues();
                for (int i = 0; i < lstPhongCheck.Count - 1; i++)
                {
                    mbp_departmentid += lstPhongCheck[i] + ",";
                    lstdepartmentgroupid += Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentid == Utilities.Util_TypeConvertParse.ToInt64(lstPhongCheck[i].ToString())).FirstOrDefault().departmentgroupid.ToString() + ",";
                }
                mbp_departmentid += lstPhongCheck[lstPhongCheck.Count - 1] + ") ";
                lstdepartmentgroupid += Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentid == Utilities.Util_TypeConvertParse.ToInt64(lstPhongCheck[lstPhongCheck.Count - 1].ToString())).FirstOrDefault().departmentgroupid.ToString() + " ";

                //
                LoadDataNguoiThucHien(lstdepartmentgroupid);

                if (cboLoaiBaoCao.EditValue.ToString() == "BAOCAO_009") //thu thuat noio soi da day
                {
                    chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100)) ";
                    serf_nhomdichvu = " and servicepricegroupcode = 'NSDDTT37_TT1' ";
                    serf_pttt_loaiid = " and serf.pttt_loaiid=6 ";
                    mbp_departmentid = "";
                }

                sql_laydulieu = "SELECT ROW_NUMBER () OVER (ORDER BY mbp.maubenhphamfinishdate desc) as stt, A.patientid, A.vienphiid, A.medicalrecordid, hsba.patientname, (case when hsba.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as YEAR_NAM, (case hsba.gioitinhcode when '02' then to_char(hsba.birthday, 'yyyy') else '' end) as YEAR_NU, bh.bhytcode, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, KCHD.departmentgroupname AS khoachidinh, pcd.departmentname as phongchidinh, A.NGAY_CHIDINH, mbp.maubenhphamfinishdate as ngay_thuchien, KCD.departmentgroupname AS khoachuyenden, KRV.departmentgroupname AS khoaravien, mbp.chandoan as cd_chidinh, mbp.maubenhphamid, mbp.sophieu, A.thuchienclsid, A.servicepriceid, A.servicepricecode, A.servicepricename, A.LOAIPTTT_DB, A.LOAIPTTT_L1, A.LOAIPTTT_L2, A.LOAIPTTT_L3, A.LOAIPTTT, A.SOLUONG, A.SERVICEPRICEFEE, A.TYLE, round(cast(A.THUOC_TRONGGOI as numeric),0) AS THUOC_TRONGGOI, round(cast(A.VATTU_TRONGGOI as numeric),0) AS VATTU_TRONGGOI, round(cast(A.chiphikhac as numeric),0) AS chiphikhac, (A.servicepricefee * A.soluong) as thanhtien, round(cast(((A.servicepricefee * A.soluong) - COALESCE(A.THUOC_TRONGGOI,0) - COALESCE(A.VATTU_TRONGGOI,0) - COALESCE(A.chiphikhac,0) " + chiachobacsi + " ) as numeric),0) as lai, mbp.usertrakq as mochinh_idbs, ntkq.username AS MOCHINH_TENBS, (A.MOCHINH_TIEN * (A.TYLE/100)) AS MOCHINH_TIEN, A.gayme_idbs, GM.username AS GAYME_TENBS, (A.GAYME_TIEN * (A.TYLE/100)) AS GAYME_TIEN, A.phu1_idbs, P1.username AS PHU1_TENBS, (A.PHU1_TIEN * (A.TYLE/100)) AS PHU1_TIEN, A.phu2_idbs, P2.username AS PHU2_TENBS, (A.PHU2_TIEN * (A.TYLE/100)) AS PHU2_TIEN, A.giupviec1_idbs, GV1.username AS GIUPVIEC1_TENBS, (A.GIUPVIEC1_TIEN * (A.TYLE/100)) AS GIUPVIEC1_TIEN, A.giupviec2_idbs, GV2.username AS GIUPVIEC2_TENBS, (A.GIUPVIEC2_TIEN * (A.TYLE/100)) AS GIUPVIEC2_TIEN, A.NGAY_VAOVIEN, A.NGAY_RAVIEN, A.NGAY_THANHTOAN, ntkq.username as nguoitraketqua, A.nguoinhapthuchien FROM ( SELECT vp.patientid, vp.vienphiid, ser.medicalrecordid, vp.hosobenhanid, vp.bhytid, ser.departmentgroupid as khoachidinh, ser.departmentid as phongchidinh, ser.servicepricedate as NGAY_CHIDINH, ser.maubenhphamid, (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenden, (case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, cls.thuchienclsid, ser.servicepriceid, ser.servicepricecode, ser.servicepricename, (case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as SERVICEPRICEFEE, (case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as TYLE, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) as THUOC_TRONGGOI, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle')) as VATTU_TRONGGOI, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as LOAIPTTT, (case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as LOAIPTTT_DB, (case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as LOAIPTTT_L1, (case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as LOAIPTTT_L2, (case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as LOAIPTTT_L3, ser.soluong as SOLUONG, ((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as MOCHINH_TIEN, cls.bacsigayme as gayme_idbs, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as GAYME_TIEN, cls.phumo1 as phu1_idbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 when 5 then 60000 when 6 then 27000 else 0 end) * ser.soluong) as PHU1_TIEN, cls.phumo2 as phu2_idbs, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 0 when 4 then 0 else 0 end) * ser.soluong) as PHU2_TIEN, cls.phumo3 as giupviec1_idbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 when 5 then 36000 when 6 then 21000 when 7 then 9000 when 8 then 4500 else 0 end) * ser.soluong) as GIUPVIEC1_TIEN, cls.phumo4 as giupviec2_idbs, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 0 else 0 end) * ser.soluong) as GIUPVIEC2_TIEN, vp.vienphidate as NGAY_VAOVIEN, (case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as NGAY_RAVIEN, (case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as NGAY_THANHTOAN, cls.tools_username as nguoinhapthuchien FROM serviceprice ser left join thuchiencls cls on cls.servicepriceid=ser.servicepriceid inner join (select servicepricecode, pttt_loaiid from servicepriceref where servicegrouptype in (2,3) and bhyt_groupcode in ('04CDHA','05TDCN','03XN','07KTC') " + serf_nhomdichvu + ") serf on serf.servicepricecode=ser.servicepricecode inner join (select patientid,vienphiid,hosobenhanid,bhytid,vienphistatus,departmentgroupid,vienphidate,vienphistatus_vp,vienphidate_ravien,duyet_ngayduyet_vp from vienphi) vp on vp.vienphiid=ser.vienphiid WHERE ser.bhyt_groupcode in ('04CDHA','05TDCN','03XN','07KTC') " + serf_pttt_loaiid + tieuchi_date + " ) A INNER JOIN (select maubenhphamid, sophieu, departmentid_des, maubenhphamfinishdate, usertrakq, chandoan from maubenhpham where maubenhphamgrouptype in (0,1) " + tieuchi_date_thuchien + ") mbp on mbp.maubenhphamid=A.maubenhphamid " + mbp_departmentid + " INNER JOIN (select hosobenhanid, patientname, gioitinhcode, birthday, bhytcode, hc_sonha, hc_thon, hc_xacode, hc_xaname, hc_huyencode, hc_huyenname, hc_tinhcode, hc_tinhname, hc_quocgianame from hosobenhan) hsba on hsba.hosobenhanid=A.hosobenhanid INNER JOIN bhyt bh on bh.bhytid=A.bhytid LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCHD ON KCHD.departmentgroupid=A.khoachidinh LEFT JOIN (select departmentid, departmentname from department where departmenttype in (2,3,9,6,7)) pcd ON pcd.departmentid=A.phongchidinh LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) KCD ON KCD.departmentgroupid=A.khoachuyenden LEFT JOIN (select departmentgroupid, departmentgroupname from departmentgroup) krv ON krv.departmentgroupid=A.khoaravien LEFT JOIN tools_tblnhanvien gm ON gm.userhisid=A.gayme_idbs LEFT JOIN tools_tblnhanvien p1 ON p1.userhisid=A.phu1_idbs LEFT JOIN tools_tblnhanvien p2 ON p2.userhisid=A.phu2_idbs LEFT JOIN tools_tblnhanvien gv1 ON gv1.userhisid=A.giupviec1_idbs LEFT JOIN tools_tblnhanvien gv2 ON gv2.userhisid=A.giupviec2_idbs LEFT JOIN tools_tblnhanvien ntkq ON ntkq.userhisid=mbp.usertrakq;  ";

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

        private void LoadDataNguoiThucHien(string lstdepartmentgroupid)
        {
            try
            {
                if (this.dataNguoiThucHien == null || this.dataNguoiThucHien.Rows.Count <= 0)
                {
                    string getnguoithuchien = "select nv.userhisid, nv.usercode, nv.username, (nv.usercode || ' - ' || nv.username) as usercodename from tools_tblnhanvien nv inner join (select usercode, departmentid from tbldepartment) ude on ude.usercode=nv.usercode inner join (select departmentid from department where departmentgroupid in (" + lstdepartmentgroupid + ")) de on de.departmentid=ude.departmentid group by nv.userhisid, nv.usercode, nv.username order by nv.username; ";
                    this.dataNguoiThucHien = condb.GetDataTable_HIS(getnguoithuchien);
                }

                repositoryItemGridLookUp_MoChinh.DataSource = this.dataNguoiThucHien;
                repositoryItemGridLookUp_MoChinh.DisplayMember = "usercodename";
                repositoryItemGridLookUp_MoChinh.ValueMember = "userhisid";

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


    }
}
