using DevExpress.XtraCharts;
using DevExpress.XtraSplashScreen;
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
    public partial class ucDashboardBenhNhanNoiTru : UserControl
    {
        internal void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                thoiGianTu = DateTime.ParseExact(this.dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string sqllaydulieu = "SELECT ROW_NUMBER () OVER (ORDER BY O.departmentgroupname) as stt, O.* FROM (SELECT depg.departmentgroupname, COALESCE(B.money_tong_nhandan,0) as money_tong_nhandan, COALESCE(B.money_tong_bhyt,0) as money_tong_bhyt, A.tam_ung FROM departmentgroup depg LEFT JOIN (SELECT sum(b.datra) as tam_ung, b.departmentgroupid FROM vienphi vp LEFT JOIN bill b on vp.vienphiid=b.vienphiid and b.loaiphieuthuid=2 and b.dahuyphieu=0 WHERE vp.vienphistatus_vp=1 and vp.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' GROUP BY b.departmentgroupid) A ON A.departmentgroupid=depg.departmentgroupid LEFT JOIN (SELECT spt.departmentgroupid, sum(spt.money_pttt_vp + spt.money_dvktc_vp + spt.money_thuoc_vp + spt.money_vattu_vp + spt.money_vtthaythe_vp + spt.money_xetnghiem_vp + spt.money_cdha_vp + spt.money_tdcn_vp + spt.money_khambenh_vp + spt.money_mau_vp + spt.money_giuongthuong_vp + spt.money_giuongyeucau_vp + spt.money_phuthu_vp + spt.money_vanchuyen_vp + spt.money_khac_vp + spt.money_dkpttt_thuoc_vp + spt.money_dkpttt_vattu_vp + spt.money_nuocsoi_vp + spt.money_xuatan_vp + spt.money_diennuoc_vp) as money_tong_nhandan, sum(spt.money_pttt_bh + spt.money_dvktc_bh + spt.money_thuoc_bh + spt.money_vattu_bh + spt.money_vtthaythe_bh + spt.money_xetnghiem_bh + spt.money_cdha_bh + spt.money_tdcn_bh + spt.money_khambenh_bh + spt.money_mau_bh + spt.money_giuongthuong_bh + spt.money_giuongyeucau_bh + spt.money_phuthu_bh + spt.money_vanchuyen_bh + spt.money_khac_bh + spt.money_dkpttt_thuoc_bh + spt.money_dkpttt_vattu_bh + spt.money_nuocsoi_bh + spt.money_xuatan_bh + spt.money_diennuoc_bh) as money_tong_bhyt FROM tools_serviceprice_pttt spt WHERE spt.vienphistatus_vp=1 and spt.duyet_ngayduyet_vp between '" + this.thoiGianTu + "' and '" + this.thoiGianDen + "' GROUP BY spt.departmentgroupid) B ON B.departmentgroupid=depg.departmentgroupid WHERE depg.departmentgrouptype in (1,4,11,10,100)) O; ";
                DataView dataBCTongTheKhoa = new DataView(condb.GetDataTable_HIS(sqllaydulieu));
                HienThiDuLieuBaoCao(dataBCTongTheKhoa);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void HienThiDuLieuBaoCao(DataView dataBC)
        {
            try
            {
                if (dataBC != null && dataBC.Count > 0)
                {
                    dataBCTongTheKhoa = new DataView();
                    dataBCTongTheKhoa = dataBC;
                    chartControlBNNoiTru.Series[0].Points.Clear();
                    chartControlBNNoiTru.Series[1].Points.Clear();
                    chartControlBNNoiTru.Series[2].Points.Clear();
                    for (int i = 0; i < dataBC.Count; i++)
                    {
                        chartControlBNNoiTru.Series[0].Points.Add(new SeriesPoint(dataBC[i]["departmentgroupname"].ToString(), Utilities.Util_TypeConvertParse.ToDouble(dataBC[i]["money_tong_bhyt"].ToString())));
                    }
                    for (int i = 0; i < dataBC.Count; i++)
                    {
                        chartControlBNNoiTru.Series[1].Points.Add(new SeriesPoint(dataBC[i]["departmentgroupname"].ToString(), Utilities.Util_TypeConvertParse.ToDouble(dataBC[i]["money_tong_nhandan"].ToString())));
                    }
                    for (int i = 0; i < dataBC.Count; i++)
                    {
                        chartControlBNNoiTru.Series[2].Points.Add(new SeriesPoint(dataBC[i]["departmentgroupname"].ToString(), Utilities.Util_TypeConvertParse.ToDouble(dataBC[i]["tam_ung"].ToString())));
                    }
                }
                else
                {
                    dataBCTongTheKhoa = null;
                    chartControlBNNoiTru.Series[0].Points.Clear();
                    chartControlBNNoiTru.Series[1].Points.Clear();
                    chartControlBNNoiTru.Series[2].Points.Clear();
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
