using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BC50MedicineRefDTO
    {
        public long medicinerefid { get; set; }
        public long medicinerefid_org { get; set; }
        public string medicinecode { get; set; }
        public string medicinename { get; set; }
        public string dangdung { get; set; }
        public string hoatchat { get; set; }
        public string nhomthau { get; set; }

    }
    public class BC50DSThuocVTDTO
    {
        public long vienphiid { get; set; }
        public long medicinerefid_org { get; set; }
        public string servicepricecode { get; set; }
        public string servicepricedate { get; set; }
        public long servicepricedatelong { get; set; }
        public decimal soluong { get; set; }
        public decimal thanhtien { get; set; }

    }

    public class BC50DSBenhNhanDTO
    {
        public long stt { get; set; }
        public long vienphiid { get; set; }
        public long patientid { get; set; }
        public string patientname { get; set; }
        public string bhytcode { get; set; }

    }




}
