using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
   public class classDanhMucDichVu
    {
       public long? servicepricerefid { get; set; }
       public string servicepricegroupcode { get; set; }
       public long? servicepricetype { get; set; }
       public long? servicegrouptype { get; set; }
       public string servicepricecode { get; set; }
       public string bhyt_groupcode { get; set; }
       public string report_groupcode { get; set; }
       public string report_tkcode { get; set; }
      // public string servicepricename { get; set; }
       public string servicepricenamenhandan { get; set; }
       public string servicepricenamebhyt { get; set; }
      // public string servicepricenamenuocngoai { get; set; }
       public string servicepriceunit { get; set; }
       public decimal? servicepricefee { get; set; }
       public decimal? servicepricefeenhandan { get; set; }
       public decimal? servicepricefeebhyt { get; set; }
       public decimal? servicepricefeenuocngoai { get; set; }
       public DateTime? servicepricefee_old_date { get; set; }
       public decimal? servicepricefee_old { get; set; }
       public decimal? servicepricefeenhandan_old { get; set; }
       public decimal? servicepricefeebhyt_old { get; set; }
       public decimal? servicepricefeenuocngoai_old { get; set; }
       public int? pttt_hangid { get; set; }
       public int? khongchuyendoituonghaophi { get; set; }
       public int? servicelock { get; set; }
       public int? tinhtoanlaigiadvktc { get; set; }
       public string servicepricecodeuser { get; set; }
       public string servicepricebhytdinhmuc { get; set; }
       public string ck_groupcode { get; set; }
       public decimal? pttt_dinhmucvtth { get; set; }
       public decimal? pttt_dinhmucthuoc { get; set; }
       public int? pttt_loaiid { get; set; }

    }
}
