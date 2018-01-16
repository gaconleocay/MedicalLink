using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class SuaPhieuTamUng_BCDTO : SuaPhieuTamUngDTO
    {
        public string stt { get; set; }
        public string cumthutien { get; set; }
        public long userid_nhan { get; set; }
        public string username_nhan { get; set; }
        public long departmentgroupid_nhan { get; set; }
        public string departmentgroupname_nhan { get; set; }
        public long departmentid_nhan { get; set; }
        public string departmentname_nhan { get; set; }
        public int isgroup { get; set; }

    }
}
