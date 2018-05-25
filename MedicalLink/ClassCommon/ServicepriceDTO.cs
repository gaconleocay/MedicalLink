using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class ServicepriceDTO
    {
        public string servicepricegroupcode { get; set; }
        public string servicepricecode { get; set; }
        public int loaidichvu { get;set;} //dich vu=1; thuoc,vt=2

    }
}
