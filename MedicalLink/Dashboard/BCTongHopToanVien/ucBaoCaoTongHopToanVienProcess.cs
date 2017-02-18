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
    public partial class ucBaoCaoTongHopToanVien : UserControl
    {
        internal void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                BCDashboardTongHopToanVienFilter filter = new BCDashboardTongHopToanVienFilter();
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                filter.loaiBaoCao = "REPORT_10";
                filter.dateTu = this.thoiGianTu;
                filter.dateDen = this.thoiGianDen;
                filter.chayTuDong = 0;
                List<BCDashboardTongHopToanVien> lstBCBTongHopToanVien = BCTongHopToanVien_Process.BCTongHopToanVien_ChayMoi(filter);
                HienThiDuLieuBaoCao(lstBCBTongHopToanVien);
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
        private void HienThiDuLieuBaoCao(List<BCDashboardTongHopToanVien> dataBC)
        {
            try
            {
                if (dataBC != null && dataBC.Count > 0)
                {
                    gridControlDataBNNT.DataSource = dataBC;
                }
                else
                {
                    gridControlDataBNNT.DataSource = null;
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

    }
}
