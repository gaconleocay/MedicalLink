using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BCBNSDDV_ChiPhiKhacDTO
    {
        public string stt { get; set; }
        public string vienphiid { get; set; }
        public string patientid { get; set; }
        public string patientname { get; set; }
        public string namsinh { get; set; }
        public string gioitinh { get; set; }
        public string bhytcode { get; set; }
        public string departmentgroupname { get; set; }
        //public string departmentname { get; set; }
        public string doituongbenhnhanid { get; set; }
        public object servicepricedate { get; set; }
        public long departmentgroupid { get; set; }
        public string servicepricecode { get; set; }
        public string servicepricename { get; set; }
        //public string servicepricegroupcode { get; set; }
        public string loaidoituong { get; set; }
        public decimal soluong { get; set; }
        public decimal servicepricemoney { get; set; }
        public decimal thanhtien { get; set; }
        public decimal dathu { get; set; }
        public string trangthai { get; set; }
        public object vienphidate { get; set; }
        public object vienphidate_ravien { get; set; }
        public object duyet_ngayduyet_vp { get; set; }
        public int isgroup { get; set; }
    }
}
