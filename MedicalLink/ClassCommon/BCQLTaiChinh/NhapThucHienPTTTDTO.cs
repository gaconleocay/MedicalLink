using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon.BCQLTaiChinh
{
    public class NhapThucHienPTTTDTO
    {
        public long? stt { get; set; }
        public long? thuchienptttid { get; set; }
        public long servicepriceid { get; set; }
        public long vienphiid { get; set; }
        public long patientid { get; set; }
        public string patientname { get; set; }
        public long medicalrecordid { get; set; }
        public long hosobenhanid { get; set; }
        public int? doituongbenhnhanid { get; set; }
        public int? loaivienphiid { get; set; }
        public int? vienphistatus { get; set; }
        public long maubenhphamid { get; set; }
        public long bhytid { get; set; }
        public string bhytcode { get; set; }
        public string servicepricecode { get; set; }
        public string servicepricename { get; set; }
        public DateTime? servicepricedate { get; set; }
        public string loaidoituong { get; set; }
        public decimal? dongia { get; set; }
        public decimal? soluong { get; set; }
        public int? pttt_loaiid { get; set; }
        public string pttt_loaiten { get; set; }
        public int? departmentgroupid { get; set; }
        public string departmentgroupname { get; set; }
        public int? departmentid { get; set; }
        public string departmentname { get; set; }
        public int? ngchidinhid { get; set; }
        public string ngchidinhname { get; set; }
        public int? mochinhid { get; set; }
        public int? moimochinhid { get; set; }
        public int? bacsigaymeid { get; set; }
        public int? moigaymeid { get; set; }
        public int? ktvphumeid { get; set; }
        public int? phu1id { get; set; }
        public int? phu2id { get; set; }
        public int? ktvhoitinhid { get; set; }
        public int? ddhoitinhid { get; set; }
        public int? ddhanhchinhid { get; set; }
        public int? holyid { get; set; }
        public int? dungcuvienid { get; set; }
        public string mota { get; set; }
        public DateTime? thuchienttdate { get; set; }
        public int? nguoinhapid { get; set; }
        public string nguoinhapname { get; set; }
        public int? lastuserupdatedid { get; set; }
        public DateTime? lasttimeupdated { get; set; }
    }
}
