using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BCBHYT21Chenh2018DTO
    {
        public string stt { get; set; }
        public string servicepricecode { get; set; }
        public string bhyt_groupcode { get;set;}
        public string tennhom_bhyt { get; set; }
        public string servicepricecodeuser { get; set; }
        public string servicepricenamebhyt { get; set; }
        public string tyle { get; set; }
        public decimal soluong { get; set; }
        public decimal giabhyt_truoc13 { get; set; }
        public decimal thanhtien_truoc13 { get; set; }
        public decimal giabhyt_13 { get; set; }
        public decimal thanhtien_13 { get; set; }
        public decimal giabhyt_17 { get; set; }
        public decimal thanhtien_17 { get; set; }
        public decimal chenh_17_13 { get; set; }
        public decimal chenh_17_truoc13 { get; set; }
        public int isgroup { get; set; }
    }
}
