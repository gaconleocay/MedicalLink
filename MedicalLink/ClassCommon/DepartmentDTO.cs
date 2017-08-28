using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class DepartmentDTO
    {
        public Int32 departmentgroupid { get; set; }
        public string departmentgroupcode { get; set; }
        public string departmentgroupname { get; set; }
        public Int32 departmentgrouptype { get; set; }

        public Int32 departmentid { get; set; }
        public string departmentcode { get; set; }
        public string departmentname { get; set; }
        public Int32 departmenttype { get; set; }
    }
}
