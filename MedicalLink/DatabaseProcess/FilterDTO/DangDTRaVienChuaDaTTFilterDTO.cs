using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.DatabaseProcess.FilterDTO
{
    public class DangDTRaVienChuaDaTTFilterDTO
    {
        public string loaiBaoCao { get; set; } // mã của báo cáo
        public string dateTu { get; set; }
        public string dateDen { get; set; }
        public string dateKhoangDLTu { get; set; }
        public int departmentgroupid { get; set; } //=0: toàn viện
        public int loaivienphiid { get; set; } //=-1: toàn viện; =0: nội trú ; =1: ngoại trú
        public int chayTuDong { get; set; } //=1: tu dong chay

    }
}
