using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BCDashboardQLTongTheKhoa
    {
        public long? BNDangDT_stt { get; set; }
        public string BNDangDT_name { get; set; }
        public string BNDangDT_value { get; set; }
        public string BNDangDT_unit { get; set; }
        public long? RaVienChuaTT_stt { get; set; }
        public string RaVienChuaTT_name { get; set; }
        public string RaVienChuaTT_value { get; set; }
        public string RaVienChuaTT_unit { get; set; }
        public long? DaTT_stt { get { return RaVienChuaTT_stt; } }
        public string DaTT_name { get { return RaVienChuaTT_name; } }
        public string DaTT_value { get; set; }
        public string DaTT_unit { get { return RaVienChuaTT_unit; } }
        public long? DoanhThu_stt { get { return RaVienChuaTT_stt; } }
        public string DoanhThu_name { get { return RaVienChuaTT_name; } }
        public string DoanhThu_value { get; set; }
        public string DoanhThu_unit { get { return RaVienChuaTT_unit; } }

    }
}
