using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class MayXetNghiemKhuVucDTO
    {
        //public int? MAYXNKHUVUCID { get; set; }
        public int? STT { get; set; }
        public string MAYXN_MA { get; set; }
        public string MAYXN_TEN { get; set; }
        public string KHUVUC_MA { get; set; }
        public string KHUVUC_TEN { get; set; }
        public string LASTUSERUPDATED { get; set; }
        public DateTime? LASTTIMEUPDATED { get; set; }
    }
}
