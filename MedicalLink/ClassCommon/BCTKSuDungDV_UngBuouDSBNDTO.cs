using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BCTKSuDungDV_UngBuouDSBNDTO
    {
        public string stt { get; set; }
        public string patientid { get; set; }
        public string vienphiid { get; set; }
        public string patientname { get; set; }
        public string year { get; set; }
        public string bhytcode { get; set; }
        public string diachi { get; set; }   
        public decimal thanhtien { get; set; }
        public string departmentid { get; set; }
        public string departmentname { get; set; }
        public string departmentgroupid { get; set; }
        public string departmentgroupname { get; set; }
        public int isgroup { get; set; }
    }
}
