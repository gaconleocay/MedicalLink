using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.Dashboard.BCQLTongTheKhoa
{
    public class QLTongTheKhoaDetailFilterDTO
    {
        public int loaiLayDuLieu { get; set; }
        public string dateTuNgay { get; set; }
        public string dateDenNgay { get; set; }
        public string lstPhongChonLayBC { get; set; }
        public string lstKhoaChonLayBC { get; set; }
        public string dateKhoangDLTu { get; set; }
        public bool thutienstatus { get; set; }
        public string cboDoiTuongText { get; set; }
    }
}
