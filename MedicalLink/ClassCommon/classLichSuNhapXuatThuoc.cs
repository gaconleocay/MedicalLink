using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class classLichSuNhapXuatThuoc
    {
        public long stt { get; set; }
        public long medicinerefid { get; set; }
      //  public long medicinerefid_org { get; set; }
        public string medicinecode { get; set; }
        public string medicinename { get; set; }
        public DateTime medicinedate { get; set; }
        public string medicinestorebillcode { get; set; }
        public string medicinestorebilltype { get; set; }
        public decimal? accept_soluong { get; set; }
        public decimal? accept_money { get; set; }
        public string medicinestorebillremark { get; set; }
        public string solo { get; set; }
        public string sodangky { get; set; }
        public long medicinekiemkeid { get; set; }
        public string departmentgroupname { get; set; }
        public string departmentname { get; set; }

    }
}
