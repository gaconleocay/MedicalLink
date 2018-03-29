using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class Departmentgroup_KhoaPTTTDTO
    {
        public long stt { get; set; }
        public long departmentgroupid { get; set; }
        public string departmentgroupcode { get; set; }
        public string departmentgroupname { get; set; }
        public bool pttt_khoaguiyc { get; set; }
        public string pttt_khoaguiyc_name { get; set; }
        public string pttt_lastuserupdated { get; set; }
        public DateTime? pttt_lasttimeupdated { get; set; }
    }
}
