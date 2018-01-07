using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class MedicalrecordGopBADTO
    {
        public long medicalrecordid { get; set; }
        public long vienphiid { get; set; }
        public long patientid { get; set; }
        public string patientname { get; set; }
        public string vienphistatus { get; set; }
        public string thoigianvaovien { get; set; }
        public string thoigianravien { get; set; }
        public string departmentgroupname { get; set; }
        public long departmentid { get; set; }
        public string departmentname { get; set; }
        public long hosobenhanid { get; set; }
        public int vienphistatus_vp { get; set; }
        public long bhytid { get; set; }
    }
}
