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

    public class Department_KhoaCLSDTO
    {
        public long stt { get; set; }
        public long departmentid { get; set; }
        public string departmentcode { get; set; }
        public string departmentname { get; set; }
        public bool cls_khoaguiyc { get; set; }
        public string cls_khoaguiyc_name { get; set; }
        public string cls_lastuserupdated { get; set; }
        public DateTime? cls_lasttimeupdated { get; set; }
    }
}
