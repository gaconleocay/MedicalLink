using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class classBCQLTongTheKhoaBNDetail
    {
        public long stt { get; set; }
        public long vienphiid { get; set; }
        public long patientid { get; set; }
        public string patientname { get; set; }
        public string bhytcode { get; set; }
        public int muchuong { get; set; }
        public long departmentgroupid { get; set; }
        public string departmentname { get; set; }
        public DateTime vienphidate { get; set; }
        public string vienphidate_ravien { get; set; }
        public string duyet_ngayduyet_vp { get; set; }
        public decimal money_khambenh { get; set; }
        public decimal money_xetnghiem { get; set; }
        public decimal money_cdhatdcn { get; set; }
        public decimal money_pttt { get; set; }
        public decimal money_dvktc { get; set; }
        public decimal money_giuongthuong { get; set; }
        public decimal money_giuongyeucau { get; set; }
        public decimal money_khac { get; set; }
        public decimal money_vattu { get; set; }
        public decimal money_mau { get; set; }
        public decimal money_thuoc { get; set; }
        public decimal money_tong { get; set; }
        public decimal tam_ung { get; set; }
        public decimal ty_le_thuoc { get; set; }

        public int bhyt_loaiid { get; set; }
        public int loaivienphiid { get; set; }
        public int du5nam6thangluongcoban { get; set; }
        public int bhyt_tuyenbenhvien { get; set; }


    }
}
