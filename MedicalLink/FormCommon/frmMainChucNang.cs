using MedicalLink.TimerService;
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
                TimerUpdateDataTableBNDangDTTmp();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #region Service chay an
        private void TimerUpdateDataTableBNDangDTTmp()
        {
            try
            {
                if (MedicalLink.GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp > 0)
                {
                    timerTblBNDangDT.Interval = Utilities.Util_TypeConvertParse.ToInt32((GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp * 60 * 1000).ToString());
                    timerTblBNDangDT.Start();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void timerTblBNDangDT_Tick(object sender, EventArgs e)
        {
            try
            {
                //TimerServiceProcess.SQLKiemTraVaUpdateTableDangDTTmp();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }


        #endregion


    }
}
