using MedicalLink.DatabaseProcess.FilterDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.FormCommon
{
    public partial class frmMain : Form
    {
        private void TimerChayChuongTrinhServiceAn()
        {
            try
            {
                TimerUpdateData_BCNoiTru();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #region Service chay an
        private void TimerUpdateData_BCNoiTru()
        {
            try
            {
                if (MedicalLink.GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp > 0)
                {
                    timerTblBNDangDT.Interval = Utilities.TypeConvertParse.ToInt32((GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp * 60 * 1000).ToString());
                    timerTblBNDangDT.Start();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void timerTblBCNoiTru_Tick(object sender, EventArgs e)
        {
            try
            {
                DangDTRaVienChuaDaTTFilterDTO filter = new DangDTRaVienChuaDaTTFilterDTO();
                filter.loaiBaoCao = "REPORT_09";
                filter.dateTu = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
                filter.dateDen = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
                filter.dateKhoangDLTu = GlobalStore.KhoangThoiGianLayDuLieu;
                filter.departmentgroupid = 0;
                filter.loaivienphiid = 0;
                filter.chayTuDong = 1;
               // DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_DangDT_Tmp(filter);
               // DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienChuaTT_Tmp(filter);
                //DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienDaTT_Tmp(filter);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


        #endregion


    }
}
