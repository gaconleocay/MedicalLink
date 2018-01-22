using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class ServicepriceDuyetPTTTDTO
    {
        public string servicepriceid { get; set; }
        public int? duyetpttt_stt { get; set; }
        public string duyetpttt_usercode { get; set; }
        public long vienphiid { get; set; }
        public long maubenhphamid { get; set; }
        public string bhyt_groupcode { get; set; }

    }
}
