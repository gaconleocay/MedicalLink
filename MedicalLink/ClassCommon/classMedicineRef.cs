using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class classMedicineRef
    {
        public object stt { get; set; }
        public long medicinerefid { get; set; }
        public long medicinerefid_org { get; set; }
        public string medicinecode { get; set; }
        public string medicinename { get; set; }
        public decimal giaban { get; set; }
        public string medicinerefid_orgcode { get; set; }
        public string medicinegroupcode { get; set; }
        public string donvitinh { get; set; }
        public decimal soluongtonkho { get; set; }
        public decimal soluongkhadung { get; set; }
        public decimal soluongtutruc { get; set; }
        public object hansudung { get; set; }
        public string solo { get;set;}
        public int isgroup { get; set; }

    }
}
