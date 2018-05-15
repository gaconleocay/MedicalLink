using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class DanhSachNhanVienDTO
    {
        public int? STT { get; set; }
        public string USERCODE { get; set; }
        public string USERNAME { get; set; }
        //public string USERPASSWORD { get; set; }
        public int? USERSTATUS { get; set; }
        public int? USERGNHOM { get; set; }
        public string USERNOTE { get; set; }
        public int? USERHISID { get; set; }
        public string USERGNHOM_NAME { get; set; }
        public int? NHOM_BCID { get; set; }
        public string NHOM_BCTEN { get; set; }
    }
}
