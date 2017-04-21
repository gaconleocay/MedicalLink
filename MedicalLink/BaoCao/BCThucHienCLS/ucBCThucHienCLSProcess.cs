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
                string sql_laydulieu = "";

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string tieuchi_date = "";
                string mbp_departmentid = "";
                string serf_pttt_loaiid = " and serf.pttt_loaiid>0 ";

                string chiachobacsi = " -(A.MOCHINH_TIEN * (A.TYLE/100))-(A.GAYME_TIEN * (A.TYLE/100))-(A.PHU1_TIEN * (A.TYLE/100))-(A.PHU2_TIEN * (A.TYLE/100))-(A.GIUPVIEC1_TIEN * (A.TYLE/100))-(A.GIUPVIEC2_TIEN * (A.TYLE/100)) ";

                if (cboTieuChi.Text == "Theo ngày thanh toán")
                {
                    tieuchi_date = " and vp.duyet_ngayduyet_vp>='" + tungay + "' and vp.duyet_ngayduyet_vp<='" + denngay + "' ";
                }
                else if (cboTieuChi.Text == "Theo ngày thực hiện")
                {
                    tieuchi_date = " and cls.thuchienclsdate>='" + tungay + "' and cls.thuchienclsdate<='" + denngay + "' ";
                }
                else
                {
                    tieuchi_date = " and ser.servicepricedate>='" + tungay + "' and ser.servicepricedate<='" + denngay + "' ";
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
                }
                mbp_departmentid += lstPhongCheck[lstPhongCheck.Count - 1] + ") ";

                sql_laydulieu = "SELECT ROW_NUMBER () OVER (ORDER BY A.NGAY_THUCHIEN desc) as stt, A.patientid, A.vienphiid, hsba.patientname, (case when hsba.gioitinhcode='01' then to_char(hsba.birthday, 'yyyy') else '' end) as YEAR_NAM, (case hsba.gioitinhcode when '02' then to_char(hsba.birthday, 'yyyy') else '' end) as YEAR_NU, bh.bhytcode, KCHD.departmentgroupname AS khoachidinh, pcd.departmentname as phongchidinh, A.NGAY_CHIDINH, A.NGAY_THUCHIEN, KCD.departmentgroupname AS khoachuyenden, KRV.departmentgroupname AS khoaravien, A.servicepricecode, A.servicepricename, A.LOAIPTTT_DB, A.LOAIPTTT_L1, A.LOAIPTTT_L2, A.LOAIPTTT_L3, A.LOAIPTTT, A.SOLUONG, A.SERVICEPRICEFEE, A.TYLE, round(cast(A.THUOC_TRONGGOI as numeric),0) AS THUOC_TRONGGOI, round(cast(A.VATTU_TRONGGOI as numeric),0) AS VATTU_TRONGGOI, round(cast(A.chiphikhac as numeric),0) AS chiphikhac, (A.servicepricefee * A.soluong) as thanhtien, round(cast(((A.servicepricefee * A.soluong) - COALESCE(A.THUOC_TRONGGOI,0) - COALESCE(A.VATTU_TRONGGOI,0) - COALESCE(A.chiphikhac,0) " + chiachobacsi + " ) as numeric),0) as lai, MC.username AS MOCHINH_TENBS, (A.MOCHINH_TIEN * (A.TYLE/100)) AS MOCHINH_TIEN, GM.username AS GAYME_TENBS, (A.GAYME_TIEN * (A.TYLE/100)) AS GAYME_TIEN, P1.username AS PHU1_TENBS, (A.PHU1_TIEN * (A.TYLE/100)) AS PHU1_TIEN, P2.username AS PHU2_TENBS, (A.PHU2_TIEN * (A.TYLE/100)) AS PHU2_TIEN, GV1.username AS GIUPVIEC1_TENBS, (A.GIUPVIEC1_TIEN * (A.TYLE/100)) AS GIUPVIEC1_TIEN, GV2.username AS GIUPVIEC2_TENBS, (A.GIUPVIEC2_TIEN * (A.TYLE/100)) AS GIUPVIEC2_TIEN, A.NGAY_VAOVIEN, A.NGAY_RAVIEN, A.NGAY_THANHTOAN FROM (SELECT vp.patientid, vp.vienphiid, vp.hosobenhanid, vp.bhytid, ser.departmentgroupid as khoachidinh, ser.departmentid as phongchidinh, ser.servicepricedate as NGAY_CHIDINH, cls.thuchienclsdate as NGAY_THUCHIEN, ser.maubenhphamid, (select mrd.backdepartmentid from medicalrecord mrd where mrd.medicalrecordid=ser.medicalrecordid) as khoachuyenden, (case when vp.vienphistatus<>0 then vp.departmentgroupid else 0 end) as khoaravien, ser.servicepricecode, ser.servicepricename, (case ser.loaidoituong when 0 then ser.servicepricemoney_bhyt when 1 then ser.servicepricemoney_nhandan else ser.servicepricemoney end) as SERVICEPRICEFEE, (case ser.loaipttt when 1 then 50.0 when 2 then 80.0 else 100.0 end) as TYLE, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('09TDT','091TDTtrongDM','093TDTUngthu','092TDTngoaiDM','094TDTTyle')) as THUOC_TRONGGOI, (select sum(case when ser_dikem.maubenhphamphieutype=0 then ser_dikem.servicepricemoney_nhandan*ser_dikem.soluong else 0-(ser_dikem.servicepricemoney_nhandan * ser_dikem.soluong) end) from serviceprice ser_dikem where ser_dikem.servicepriceid_master=ser.servicepriceid and ser_dikem.loaidoituong=2 and ser_dikem.bhyt_groupcode in ('10VT', '101VTtrongDM', '101VTtrongDMTT', '102VTngoaiDM','103VTtyle')) as VATTU_TRONGGOI, (case serf.pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as LOAIPTTT, (case when serf.pttt_loaiid in (1,5) then 'x' else '' end) as LOAIPTTT_DB, (case when serf.pttt_loaiid in (2,6) then 'x' else '' end) as LOAIPTTT_L1, (case when serf.pttt_loaiid in (3,7) then 'x' else '' end) as LOAIPTTT_L2, (case when serf.pttt_loaiid in (4,8) then 'x' else '' end) as LOAIPTTT_L3, ser.soluong as SOLUONG, ((ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) + COALESCE((case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end),0))* ser.soluong as chiphikhac, cls.phauthuatvien as MOCHINH_TENBS, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 when 5 then 84000 when 6 then 37500 when 7 then 19500 when 8 then 15000 else 0 end) * ser.soluong) as MOCHINH_TIEN, cls.bacsigayme as GAYME_TENBS, ((case serf.pttt_loaiid when 1 then 280000 when 2 then 125000 when 3 then 65000 when 4 then 50000 else 0 end) * ser.soluong) as GAYME_TIEN, cls.phumo1 as PHU1_TENBS, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 50000 when 4 then 30000 when 5 then 60000 when 6 then 27000 else 0 end) * ser.soluong) as PHU1_TIEN, cls.phumo2 as PHU2_TENBS, ((case serf.pttt_loaiid when 1 then 200000 when 2 then 90000 when 3 then 0 when 4 then 0 else 0 end) * ser.soluong) as PHU2_TIEN, cls.phumo3 as GIUPVIEC1_TENBS, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 15000 when 5 then 36000 when 6 then 21000 when 7 then 9000 when 8 then 4500 else 0 end) * ser.soluong) as GIUPVIEC1_TIEN, cls.phumo4 as GIUPVIEC2_TENBS, ((case serf.pttt_loaiid when 1 then 120000 when 2 then 70000 when 3 then 30000 when 4 then 0 else 0 end) * ser.soluong) as GIUPVIEC2_TIEN, vp.vienphidate as NGAY_VAOVIEN, (case when vp.vienphistatus <>0 then vp.vienphidate_ravien end) as NGAY_RAVIEN, (case when vp.vienphistatus_vp=1 then vp.duyet_ngayduyet_vp end) as NGAY_THANHTOAN FROM serviceprice ser left join thuchiencls cls on cls.servicepriceid=ser.servicepriceid inner join servicepriceref serf on serf.servicepricecode=ser.servicepricecode inner join vienphi vp on vp.vienphiid=ser.vienphiid WHERE serf.servicegrouptype=3 and serf.bhyt_groupcode in ('04CDHA','05TDCN') and ser.bhyt_groupcode in ('04CDHA','05TDCN') " + serf_pttt_loaiid + tieuchi_date + " ) A INNER JOIN maubenhpham mbp on mbp.maubenhphamid=A.maubenhphamid and mbp.maubenhphamgrouptype=1 " + mbp_departmentid + " INNER JOIN hosobenhan hsba on hsba.hosobenhanid=A.hosobenhanid INNER JOIN bhyt bh on bh.bhytid=A.bhytid LEFT JOIN departmentgroup KCHD ON KCHD.departmentgroupid=A.khoachidinh LEFT JOIN department pcd ON pcd.departmentid=A.phongchidinh LEFT JOIN departmentgroup KCD ON KCD.departmentgroupid=A.khoachuyenden LEFT JOIN departmentgroup krv ON krv.departmentgroupid=A.khoaravien LEFT JOIN tools_tblnhanvien mc ON mc.userhisid=A.MOCHINH_TENBS LEFT JOIN tools_tblnhanvien gm ON gm.userhisid=A.GAYME_TENBS LEFT JOIN tools_tblnhanvien p1 ON p1.userhisid=A.PHU1_TENBS LEFT JOIN tools_tblnhanvien p2 ON p2.userhisid=A.PHU2_TENBS LEFT JOIN tools_tblnhanvien gv1 ON gv1.userhisid=A.GIUPVIEC1_TENBS LEFT JOIN tools_tblnhanvien gv2 ON gv2.userhisid=A.GIUPVIEC2_TENBS; ";

                dataBCPTTT = condb.getDataTable(sql_laydulieu);
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

    }
}
