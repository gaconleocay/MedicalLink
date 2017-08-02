using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BCXuatNhapTonTuTruc
    {
        public string stt { get; set; }
        public string medicinerefid_org { get; set; }
        public string medicinegroupcode { get; set; }
        public string medicinecode { get; set; }
        public string medicinename { get; set; }
        public string donvitinh { get; set; }
        public decimal soluongtonkho { get; set; }
        public decimal soluongkhadung { get; set; }
        public decimal soluongtutruc { get; set; }
        public int isgroup {get;set;}

    }
}
