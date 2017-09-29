using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.BaoCao
{
    public partial class ucBCSoXetNghiem : UserControl
    {
        private void LayDuLieu_SoViSinh(string _tieuchi)
        {
            try
            {
                string sql_timkiem = "SELECT ROW_NUMBER () OVER (ORDER BY hsba.patientcode, A.maubenhphamid) as stt, hsba.patientcode, hsba.patientname, A.maubenhphamid, (case when hsba.gioitinhcode='01' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nam, (case hsba.gioitinhcode when '02' then cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) else '' end) as year_nu, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, (case when hsba.bhytcode <>'' then 'x' else '' end) as COBHYT, A.CHANDOAN, (KYC.DEPARTMENTGROUPNAME || ' - ' || PYC.DEPARTMENTNAME) AS NOIGUI, A.YEUCAU, A.KETQUA, A.MAUBENHPHAMDATE, (case when A.maubenhphamfinishdate<>'0001-01-01 00:00:00' then A.maubenhphamfinishdate end) as maubenhphamfinishdate, nth.username AS nguoithuchien, nd.username as nguoiduyet FROM (SELECT mbp.maubenhphamid, mbp.hosobenhanid, mbp.departmentgroupid, mbp.departmentid, mbp.departmentid_des, mbp.chandoan, mbp.usertrakq, mbp.userthuchien, mbp.maubenhphamdate, mbp.maubenhphamfinishdate, se.servicename as yeucau, se.servicevalue as ketqua, se.servicedoer, se.servicecomment FROM maubenhpham mbp LEFT JOIN service se ON se.maubenhphamid=mbp.maubenhphamid WHERE se.servicecode not in (select sef.servicegroupcode from service_ref sef group by sef.servicegroupcode) and mbp.maubenhphamgrouptype=0 and mbp.departmentid_des='" + 253 + "' and " + _tieuchi + " ) A INNER JOIN hosobenhan hsba ON hsba.hosobenhanid=A.hosobenhanid LEFT JOIN departmentgroup kyc ON kyc.departmentgroupid=A.departmentgroupid LEFT JOIN department pyc ON pyc.departmentid=A.departmentid and pyc.departmenttype in (2,3,9) LEFT JOIN nhompersonnel nth ON nth.usercode=A.servicedoer LEFT JOIN nhompersonnel nd ON 'UP_OK:' || nd.usercode=A.servicecomment;  ";
                dataBaoCao = condb.GetDataTable_HIS(sql_timkiem);
                if (this.dataBaoCao != null && this.dataBaoCao.Rows.Count > 0)
                {
                    gridControlSoViSinh.DataSource = this.dataBaoCao;
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

        private void LayDuLieu_SoSinhHoaThuongQuy(string _tieuchi)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
    }
}
