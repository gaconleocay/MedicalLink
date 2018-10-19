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
}
