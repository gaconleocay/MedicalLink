using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class SuaPhieuCDDVDTO
    {
        public long vienphiid { get; set; }
        public long maubenhphamid { get; set; }
        public DateTime thoigianchidinh { get; set; }
        public DateTime thoigiansudung { get; set; }
        // public DateTime? phauthuatthuthuatdate { get; set; }
        public DateTime maubenhphamfinishdate { get; set; }
        public long phieudieutriid { get; set; }
        public long maubenhphamgrouptypeid { get; set; }
        public long medicalrecordid { get; set; }
    }

    public class SuaPhieuCDDVTraKetQuaDTO
    {
        public long servicepriceid { get; set; }
        public string servicepricename { get; set; }
        public DateTime phauthuatthuthuatdate { get; set; }
        public DateTime thoigiantraketqua { get; set; }
        public long maubenhphamgrouptypeid { get; set; }
    }
}
