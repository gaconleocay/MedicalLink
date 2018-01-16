using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class SuaPhieuTamUngDTO
    {
        public long billid { get; set; }
        public string billcode { get; set; }
        public string billgroupcode { get; set; }
        public long patientid { get; set; }
        public long vienphiid { get; set; }
        public long hosobenhanid { get; set; }
        public string patientname { get; set; }
        public string loaiphieuthuid { get; set; }
        public decimal sotien { get; set; }
        public string billdate { get; set; }
        public string vienphistatus_vp { get; set; }
        public long departmentgroupid { get; set; }
        public long departmentid { get; set; }
        public string departmentgroupname { get; set; }
        public string departmentname { get; set; }
        public long userid { get; set; }
        public string username { get; set; }

    }
}
