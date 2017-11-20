using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BCDoanhThuTheoLoaiHinhDVDTO
    {
        public string stt { get; set; }  
        public long departmentgroupid { get; set; }
        public string departmentgroupname { get; set; }
        public string servicepricegroupcode { get; set; }
        public string bhyt_groupcode { get; set; }
        public long servicegrouptype { get; set; }
        public string servicegrouptype_name { get; set; }
        //public long servicepricetype { get; set; }
        public string servicepricecode { get; set; }
        public string servicepricename { get; set; }
        public decimal soluong { get; set; }
        public decimal servicepricemoney { get; set; }
        public decimal thanhtien_dv { get; set; }
        public decimal servicepricemoney_bhyt { get; set; }
        public decimal thanhtien_bh { get; set; }
        public decimal thanhtien_chenh { get; set; }
        public decimal tienthucthu { get; set; }
        public int isgroup { get; set; }
    }
}
