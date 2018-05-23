using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon.BCQLTaiChinh
{
    public class ChiThuongDichVuVienPhiTmpDTO
    {
        public int? departmentgroupid { get; set; }
        public string keymapping { get; set; }
        public decimal? soluong_tong { get; set; }
        public decimal? soluong_ngaythuong { get; set; }
        public decimal? soluong_th7cn { get; set; }
        public decimal? dongia { get; set; }
        public decimal? thanhtien_tong { get; set; }
        public decimal? thanhtien_ngaythuong { get; set; }
        public decimal? thanhtien_th7cn { get; set; }
    }

    public class ChiThuongDichVuVienPhiDTO
    {
        public int? stt { get; set; }
        public string departmentgroupname { get; set; }
        public string quyetdinh_so { get; set; }
        public string quyetdinh_ngay { get; set; }
        public decimal? soluong_tong { get; set; }
        public decimal? soluong_ngaythuong { get; set; }
        public decimal? soluong_th7cn { get; set; }
        public decimal? dongia { get; set; }
        public decimal? thanhtien_tong { get; set; }
        public decimal? thanhtien_ngaythuong { get; set; }
        public decimal? thanhtien_th7cn { get; set; }
        public decimal? tylehuong { get; set; }
        public decimal? tienhuong { get; set; }
        public decimal? chiphi { get; set; }
        public decimal? tienthuong_th7cn { get; set; }
        public decimal? tienbsi_th7cn { get; set; }
        public decimal? tonghuong { get; set; }
        public string kynhan { get; set; }

    }


}
