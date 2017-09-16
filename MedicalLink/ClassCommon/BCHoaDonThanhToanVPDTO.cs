using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BCHoaDonThanhToanVPDTO
    {
        public string stt { get; set; }
        public string billgroupcode { get; set; }
        public string billcode { get; set; }
        public string billgrouptu_den { get; set; }
        public object billdate { get; set; }
        public decimal sotien { get; set; }
        public decimal miengiam { get; set; }
        public string patientid { get; set; }
        public string vienphiid { get; set; }
        public string patientname { get; set; }
        public string diachi { get; set; }
        public string bhytcode { get; set; }
        public string userid { get; set; }
        public string nguoithu { get; set; }
        public int isgroup { get; set; }
    }
}
