using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BCHoaDonChungTuThuocSDDTO
    {
        public string stt { get; set; }
        public string medicinestorebillcode { get; set; }
        public object medicinestorebilldate { get; set; }
        public string sochungtu { get; set; }
        public decimal tongtien { get; set; }
        public decimal soluong { get; set; }
        public string nhacungcap { get; set; }
        public string nguoigiaohang { get; set; }
        public object ngaynhanduhang { get; set; }
        public string nguoinhanhang { get; set; }
        public long medicinestoreid { get; set; }
        public string khonhanhang { get; set; }
        public string ghichu { get; set; }
        public int isgroup { get; set; }

    }
}
