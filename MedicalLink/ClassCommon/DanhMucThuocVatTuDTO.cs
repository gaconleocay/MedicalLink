using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class DanhMucThuocVatTuDTO
    {
        public long? stt { get; set; }
        public long? medicinerefid { get; set; }
        public string medicinecode { get; set; }
        public string medicinename { get; set; }
        public string donvitinh { get; set; }
        public decimal? servicepricefeebhyt { get; set; }
        public decimal? soluongtonkho { get; set; }
        public decimal? soluongkhadung { get; set; }
        public string medicinegroupcode { get; set; }
        public DateTime? hansudung { get; set; }
        public string solo { get; set; }
        public string hoatchat { get; set; }
        public string sodangky { get; set; }

    }
}
