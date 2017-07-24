using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BNThieuTienTamUngDTO
    {
        public string stt { get; set; }
        public string vienphiid { get; set; }
        public string patientid { get; set; }
        public string patientcode { get; set; }
        public string patientname { get; set; }
        public string diachi { get; set; }
        public string bhytcode { get; set; }
        public string macskcbbd { get; set; }
        public string vienphidate { get; set; }
        public string vienphidate_ravien { get; set; }
        public long departmentgroupid { get; set; }
        public string departmentgroupname { get; set; }
        public string departmentname { get; set; }
        public decimal money_tong { get; set; }
        public decimal money_tong_bh { get; set; }
        public decimal money_tong_vp { get; set; }
        public decimal money_bhyttt { get; set; }
        public decimal money_bntt { get; set; }
        public decimal money_tamung { get; set; }
        public decimal money_datra { get; set; }
        public decimal money_hoanung { get; set; }
        public decimal money_thieu { get; set; }
        public int tyle_bntt { get; set; }
        public int bhyt_tuyenbenhvien { get; set; }
        public int bhyt_loaiid { get; set; }
        public int loaivienphiid { get; set; }
        public int du5nam6thangluongcoban { get; set; }
        public int thangluongcoban { get; set; }
        public int isgroup { get; set; }
    }
}
