using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.DatabaseProcess.FilterDTO
{
    public class BCDashboardTongHopToanVienFilter
    {
        public string loaiBaoCao { get; set; } // mã của báo cáo
        public string dateTu { get; set; }
        public string dateDen { get; set; }
        public string dateKhoangDLTu { get; set; }
        public int chayTuDong { get; set; } //=1: tu dong chay   
    }
}
