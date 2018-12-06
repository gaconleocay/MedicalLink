using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon.BCQLTaiChinh
{
    public class BC117MoYCBangPPKinhHienViDTO
    {
        public int stt { get; set; }
        public string username { get; set; }
        public string usercode { get; set; }
        public string departmentgroupname { get; set; }
        public decimal sl_mochinh { get; set; }
        public decimal tien_mochinh { get; set; }
        public decimal sl_phu { get; set; }
        public decimal tien_phu { get; set; }
        public decimal sl_bacsigayme { get; set; }
        public decimal tien_bacsigayme { get; set; }
        public decimal sl_ktvphume { get; set; }
        public decimal tien_ktvphume { get; set; }
        public decimal sl_dungcuvien { get; set; }
        public decimal tien_dungcuvien { get; set; }
        public decimal thuclinh { get; set; }
        public string kynhan { get; set; }
        public string ghichu { get; set; }
    }


    public class ThucHienPTTTDTO
    {
        public int vienphiid { get; set; }
        public int phauthuatvien { get; set; }
        public int phumo1 { get; set; }
        public int phumo2 { get; set; }
        public int bacsigayme { get; set; }
        public int phume { get; set; }
        public int dungcuvien { get; set; }
        public decimal soluong { get; set; }
        public decimal soluong_phu { get; set; }
    }

    public class BacSiDTO
    {
        public int userhisid { get; set; }
        public string usercode { get; set; }
        public string username { get; set; }
        public string departmentgroupname { get; set; }
    }

}
