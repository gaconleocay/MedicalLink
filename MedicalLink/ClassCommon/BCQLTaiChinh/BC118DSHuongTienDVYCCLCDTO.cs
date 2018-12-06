using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon.BCQLTaiChinh
{
    public class BC118DSHuongTienDVYCCLCDTO
    {
        public int stt { get; set; }
        public string username { get; set; }
        public string usercode { get; set; }
        public string departmentgroupname { get; set; }
        public decimal mochinh_sl2 { get; set; }
        public decimal mochinh_sl3 { get; set; }
        public decimal mochinh_sl5 { get; set; }
        public decimal phu_sl2 { get; set; }
        public decimal phu_sl3 { get; set; }
        public decimal phu_sl5 { get; set; }
        public decimal mochinh_tien { get; set; }
        public decimal mochinh_thue2 { get; set; }
        public decimal mochinh_sauthue { get; set; }
        public decimal phu_tien { get; set; }
        public decimal phu_thue2 { get; set; }
        public decimal phu_sauthue { get; set; }
        public decimal thuclinh { get; set; }
        public string kynhan { get; set; }
    }


    public class BC118ThucHienPTTTDTO
    {
        public int vienphiid { get; set; }
        public int phauthuatvien { get; set; }
        public int phumo1 { get; set; }
        public int phumo2 { get; set; }
        public decimal soluong { get; set; }
        public decimal dongia { get; set; }
        public decimal soluong_phu { get; set; }
    }

}
