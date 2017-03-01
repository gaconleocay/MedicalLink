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
        /// <summary>
        /// tieuchi">=0: theo khoa ra vien; =1: theo khoa chi dinh
        /// kieuxem">=0: xem tong hop; =1: xem chi tiet
        /// </summary>
        /// <param name="tieuchi"></param>
        /// <param name="kieuxem"></param>
        internal void LayDuLieuBaoCao_ChayMoi(int tieuchi, int kieuxem)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                BCDashboardTongHopToanVienFilter filter = new BCDashboardTongHopToanVienFilter();
                filter.loaiBaoCao = "REPORT_10";
                filter.dateTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                filter.dateDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                filter.chayTuDong = 0;
                filter.kieuXem = kieuxem;
                filter.tieuChi = tieuchi;
                lstBCBTongHopToanVien = BCTongHopToanVien_Process.BCTongHopToanVien_ChayMoi(filter);
                HienThiDuLieuBaoCao(lstBCBTongHopToanVien);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
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
