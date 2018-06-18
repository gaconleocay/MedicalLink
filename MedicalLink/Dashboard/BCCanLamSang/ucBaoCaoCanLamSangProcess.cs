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

namespace MedicalLink.Dashboard
{
    public partial class ucBaoCaoCanLamSang : UserControl
    {
        internal void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                //BCDashboardTongHopToanVienFilter filter = new BCDashboardTongHopToanVienFilter();
                thoiGianTu = System.DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = System.DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //filter.loaiBaoCao = "REPORT_12";
                //filter.dateTu = this.thoiGianTu;
                //filter.dateDen = this.thoiGianDen;
                //filter.chayTuDong = 0;
                //List<BCDashboardTongHopToanVien> lstBCBTongHopToanVien = BCTongHopToanVien_Process.BCTongHopToanVien_ChayMoi(filter);
                //HienThiDuLieuBaoCao(lstBCBTongHopToanVien);
                string sqlBCCSL = "SELECT (case A.bhyt_groupcode when '01KB' then 'Khám bệnh' when '03XN' then 'Xét nghiệm' when '04CDHA' then 'Chẩn đoán hình ảnh' when '05TDCN' then 'Thăm dò chức năng' when '06PTTT' then 'Phẫu thuật-thủ thuật' when '07KTC' then 'DV kỹ thuật cao' when '12NG' then 'Ngày giường' else '' end) as bhyt_groupcode, sum(case when de.departmenttype in (2,9) then A.money_groupcode else 0 end) as money_ngoaitru, sum(case when de.departmenttype=3 then A.money_groupcode else 0 end) as money_noitru, sum(COALESCE(A.chiphi_1,0) + COALESCE(A.chiphi_2,0)) as chi_phi,  (sum(A.money_groupcode) - sum(COALESCE(A.chiphi_1,0) + COALESCE(A.chiphi_2,0))) as lai  FROM (SELECT ser.bhyt_groupcode,ser.departmentid, (sum(case when ser.loaidoituong in (0,4,6) then ser.servicepricemoney_bhyt * ser.soluong else 0 end) + sum(case when ser.loaidoituong in (1,3,8) then ser.servicepricemoney_nhandan * ser.soluong else 0 end)  +  sum(case when ser.loaidoituong in (4,6) then (ser.servicepricemoney - servicepricemoney_bhyt) * ser.soluong else 0 end)) as money_groupcode, sum(ser.chiphidauvao + ser.chiphimaymoc + ser.chiphipttt) as chiphi_1,sum(case when ser.mayytedbid<>0 then (select myt.chiphiliendoanh from mayyte myt where myt.mayytedbid=ser.mayytedbid) else 0 end) as chiphi_2 FROM vienphi vp inner join serviceprice ser on vp.vienphiid=ser.vienphiid WHERE ser.bhyt_groupcode in ('01KB','03XN','04CDHA','05TDCN','06PTTT','07KTC','12NG') and vp.duyet_ngayduyet_vp >='" + this.thoiGianTu + "' and vp.duyet_ngayduyet_vp<='" + this.thoiGianDen + "' GROUP BY ser.bhyt_groupcode,ser.departmentid ORDER BY ser.bhyt_groupcode) A INNER JOIN department de on A.departmentid=de.departmentid GROUP BY A.bhyt_groupcode;";
                DataView dataCLS = new DataView(condb.GetDataTable_HIS(sqlBCCSL));
                HienThiDuLieuBaoCao(dataCLS);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void LayDuLieuBaoCao_DaChayDuLieu()
        {
            try
            {
                //BCDashboardTongHopToanVienFilter filter = new BCDashboardTongHopToanVienFilter();
                //thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                //filter.loaiBaoCao = "REPORT_09";
                //filter.dateTu = this.thoiGianTu;
                //filter.dateDen = this.thoiGianDen;
                //filter.dateKhoangDLTu = this.KhoangThoiGianLayDuLieu;
                //filter.departmentgroupid = 0;
                //filter.loaivienphiid = 0;
                //filter.chayTuDong = 0;
                //DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienDaTT_Tmp(filter);
                //SQLLayDuLieuBaoCao_DaChayDuLieu();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void HienThiDuLieuBaoCao(DataView dataBC)
        {
            try
            {
                if (dataBC != null && dataBC.Count > 0)
                {
                    gridControlBCCLS.DataSource = dataBC;
                }
                else
                {
                    gridControlBCCLS.DataSource = null;
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

    }
}
