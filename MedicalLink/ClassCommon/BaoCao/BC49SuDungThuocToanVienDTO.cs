using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon.BaoCao
{
    public class BC49SuDungThuocToanVienDTO
    {
        public string stt { get; set; }
        public int departmentgroupid { get; set; }
        public string departmentgroupname { get; set; }
        public string medicinecode { get; set; }
        public string medicinename { get; set; }
        public string medicinegroupcode { get; set; }
        public decimal? dongia { get; set; }
        public decimal noitru_sl { get; set; }
        public decimal noitru_thanhtien { get; set; }
        public decimal tutruc_sl { get; set; }
        public decimal tutruc_thanhtien { get; set; }
        public int isgroup { get; set; }

    }

    public class DanhMucNhomThuocDTO
    {
        public string medicinecode { get; set; }
        public string medicinename { get; set; }
    }

}
