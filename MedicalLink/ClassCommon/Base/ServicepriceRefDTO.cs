using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon.Base
{
    public class ServicepriceRefDTO
    {
        public int servicepricerefid { get; set; }
        public string servicepricegroupcode { get; set; }
        public int servicepricetype { get; set; }
        public int servicegrouptype { get; set; }
        public string servicepricecode { get; set; }
        public string bhyt_groupcode { get; set; }
        //public string report_groupcode {get;set;}
        //public string report_tkcode {get;set;}
        public string servicepricename { get; set; }
        public string servicepricenamenhandan { get; set; }
        public string servicepricenamebhyt { get; set; }
        public string servicepricenamenuocngoai { get; set; }
        public string servicepriceunit { get; set; }
        public string servicepricefee { get; set; }
        public string servicepricefeenhandan { get; set; }
        public string servicepricefeebhyt { get; set; }
        public string servicepricefeenuocngoai { get; set; }
        //servicepricefee_old_date {get;set;}
        //servicepricefee_old {get;set;}
        //servicepricefeenhandan_old {get;set;}
        //servicepricefeebhyt_old {get;set;}
        //servicepricefeenuocngoai_old {get;set;}
        public int servicepricerefid_master { get; set; }
        public int servicelock { get; set; }
        public int pttt_hangid { get; set; }
        public int khongchuyendoituonghaophi { get; set; }
        public decimal cdha_soluongthuoc { get; set; }
        public decimal cdha_soluongvattu { get; set; }
        public int tinhtoanlaigiadvktc { get; set; }
        public string servicepricecodeuser { get; set; }
        public string servicepricebhytdinhmuc { get; set; }
        public string ck_groupcode { get; set; }
        public string servicepricecode_ng { get; set; }
        public decimal pttt_dinhmucvtth { get; set; }
        public decimal pttt_dinhmucthuoc { get; set; }
        public int pttt_loaiid { get; set; }
        public decimal chiphidauvao { get; set; }
        public decimal chiphimaymoc { get; set; }
        public decimal chiphipttt { get; set; }
    }
}
