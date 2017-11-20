using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
   public class BCDoanhThuDichVuBC08DTO
    {
        public string servicepricegroupcode { get; set; }
        public string bhyt_groupcode { get; set; }
        public int servicegrouptype { get; set; }
        public string servicegrouptype_name { get; set; }
        public string servicepricecode { get; set; }
        public string servicepricename { get; set; }
        public decimal servicepricenamebhyt { get; set; }
        public string servicepriceunit { get; set; }
        public int loaidoituong { get; set; }
        public string loaidoituong_name { get; set; }
        public decimal soluong { get; set; }
        public decimal servicepricemoney { get; set; }
        public decimal thanhtien { get; set; }

    }
}
