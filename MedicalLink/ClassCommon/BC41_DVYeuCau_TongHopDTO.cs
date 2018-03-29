using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BC41_DVYeuCau_TongHopDTO
    {
        public string stt { get; set; }
        public string servicepricecode { get; set; }
        public string servicepricename { get; set; }
        public string loaidoituong { get; set; }
        public string bhyt_groupcode { get; set; }
        public decimal soluong { get; set; }
        public decimal servicepricemoney { get; set; }
        public decimal thanhtien { get; set; }
        public decimal servicepricemoney_bhyt { get; set; }
        public decimal thanhtien_bhyt { get; set; }
        public decimal chenhlech { get; set; }
        public int isgroup { get; set; }
    }
}
