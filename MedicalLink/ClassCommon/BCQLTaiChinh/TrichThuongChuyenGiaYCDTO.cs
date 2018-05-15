using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon.BCQLTaiChinh
{
    public class TrichThuongChuyenGiaYCDTO
    {
        public string stt { get; set; }
        public string usercode { get;set;}
        public string username { get; set; }
        public int? userhisid { get; set; }
        public int? nhom_bcid { get; set; }
        public string nhom_bcten { get; set; }
        public int? soluong { get; set; }
        public decimal? thanhtien { get; set; }
        public int? tylehuong { get; set; }
        public decimal? tongtien { get; set; }
        public decimal? tienthue { get; set; }
        public decimal? thuclinh { get; set; }
        public string kynhan { get; set; }
        public int? isgroup { get; set; }
    }
}
