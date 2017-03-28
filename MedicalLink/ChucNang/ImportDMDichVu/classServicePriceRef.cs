using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ChucNang.ImportDMDichVu
{
    public class classServicePriceRef
    {
        public long stt { get; set; }
        public long servicepricerefid { get; set; }
        public string servicepricegroupcode { get; set; }
        public long servicepricetype { get; set; }
        public long servicegrouptype { get; set; }
        public string servicepricecode { get; set; }
        public string bhyt_groupcode { get; set; }
        public string report_groupcode { get; set; }
        public string report_tkcode { get; set; }
        public string servicepricename { get; set; }
        public string servicepricenamenhandan { get; set; }
        public string servicepricenamebhyt { get; set; }
        public string servicepricenamenuocngoai { get; set; }
        public string servicepricebhytquydoi { get; set; }
        public string servicepricebhytquydoi_tt { get; set; }
        public string servicepriceunit { get; set; }
        public decimal servicepricefee { get; set; }
        public decimal servicepricefeenhandan { get; set; }
        public decimal servicepricefeebhyt { get; set; }
        public decimal servicepricefeenuocngoai { get; set; }
        public string listdepartmentphongthuchien { get; set; }
        public string servicepricefee_old_date { get; set; } //time
        public string servicepricefee_old { get; set; }
        public string servicepricefeenhandan_old { get; set; }
        public string servicepricefeebhyt_old { get; set; }
        public string servicepricefeenuocngoai_old { get; set; }
        public long servicepriceprintorder { get; set; } //DEFAULT 0
        public long servicepricerefid_master { get; set; } //DEFAULT 0
        public long servicelock { get; set; } //DEFAULT 0
        public long pttt_hangid { get; set; } //DEFAULT 0
        public long laymauphongthuchien { get; set; } //DEFAULT 0
        public long khongchuyendoituonghaophi { get; set; } //DEFAULT 0
        public string cdha_soluongthuoc { get; set; } //DEFAULT 0
        public string cdha_soluongvattu { get; set; } //DEFAULT 0
        public string tylelaichidinh { get; set; } //DEFAULT 0
        public string tylelaithuchien { get; set; } //DEFAULT 0
        public string version { get; set; }
        public long luonchuyendoituonghaophi { get; set; } //DEFAULT 0
        public long tinhtoanlaigiadvktc { get; set; } //DEFAULT 0
        public string servicepricecodeuser { get; set; }
        public long lastuserupdated { get; set; }
        public string lasttimeupdated { get; set; } //time,
        public long sync_flag { get; set; }
        public long update_flag { get; set; }
        public string servicepricebhytdinhmuc { get; set; }
        public string listdepartmentphongthuchienkhamgoi { get; set; }
        public string ck_groupcode { get; set; }
        public string servicepricecode_ng { get; set; }
        public string pttt_dinhmucvtth { get; set; } //DEFAULT 0
        public string pttt_dinhmucthuoc { get; set; } //DEFAULT 0
        public long isremove { get; set; }
        public long servicepricefee_old_type { get; set; } //DEFAULT 0
        public string servicepricesttuser { get; set; }
        public string servicepricenamebhyt_old { get; set; }
        public long isdichvumoitt37 { get; set; }
        public long pttt_loaiid { get; set; } //DEFAULT 0
        public string chiphidauvao { get; set; }
        public string chiphimaymoc { get; set; }
        public string chiphipttt { get; set; }
        public long sotieuban { get; set; }

        public string servicegrouptype_name { get; set; }
        public string servicepricefee_old_type_name { get; set; }
        public string pttt_hangid_name { get; set; }
        public string pttt_loaiid_name { get; set; }
        public string bhyt_groupcode_name { get; set; }



    }
}
