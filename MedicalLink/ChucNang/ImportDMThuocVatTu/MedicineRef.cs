using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ChucNang.ImportDMDichVu
{
    public class MedicineRef
    {
        public long stt { get; set; }
        public long medicinerefid { get; set; }
        public int datatype { get; set; }
        public string datatype_name { get; set; }
        public string medicinecode { get; set; }
        public string medicinename { get; set; }
        public string medicinecodeuser { get; set; }
        public string stt_dauthau { get; set; }
        public string namcungung { get; set; }
        public string danhsttdungthuoc { get; set; }
        public string dangdung { get; set; }
        public string donggoi { get; set; }
        public string sodangky { get; set; }
        public string solo { get; set; }
        public string nongdo { get; set; }
      
    }
}
