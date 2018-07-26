using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class DVXetNghiemChiPhiDTO
    {
        public int? STT { get; set; }
        public int? MAYXNDMXNCPID { get; set; }
        public string MAYXN_MA { get; set; }
        public string MAYXN_TEN { get; set; }
        public string SERVICEPRICECODE { get; set; }
        public string SERVICEPRICENAME { get; set; }
        public string SERVICEPRICEUNIT { get; set; }
        public decimal? CP_HOACHAT { get; set; }
        public decimal? CP_HAOPHIXN { get; set; }
        public decimal? CP_LUONG { get; set; }
        public decimal? CP_DIENNUOC { get; set; }
        public decimal? CP_KHMAYMOC { get; set; }
        public decimal? CP_KHXAYDUNG { get; set; }
        public string NHOMBC_MA { get; set; }
        public string LASTUSERUPDATED { get; set; }
        public DateTime? LASTTIMEUPDATED { get; set; }
    }


    public class MayXetNghiemNhomBCDTO
    {
        public int? STT { get; set; }
        public int? MAYXNNHOMBCID { get; set; }
        public string NHOMBC_MA { get; set; }
        public string NHOMBC_TEN { get; set; }
        public int? ISTRAKQ { get; set; }
        public string GHICHU { get; set; }
        public string LASTUSERUPDATED { get; set; }
        public DateTime? LASTTIMEUPDATED { get; set; }
    }
}
