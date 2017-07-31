using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class BCChenhLechNgayGiuongDTO
    {
        public string stt { get; set; }
        public string patientid { get; set; }
        public string vienphiid { get; set; }
        public string patientname { get; set; }
        public string bhytcode { get; set; }
        public long khoachuyendenid { get; set; }
        public string khoachuyenden { get; set; }
        public string khoaravien { get; set; }
        public string phongravien { get; set; }
        public object vienphidate { get; set; }
        public object vienphidate_ravien { get; set; }
        public object duyet_ngayduyet_vp { get; set; }
        public string servicepricecode { get; set; }
        public string servicepricename { get; set; }
        public object servicepricedate { get; set; }
        public string khoachidinh { get; set; }
        public string loaidoituong { get; set; }
        public decimal soluong { get; set; }
        public decimal servicepricemoney { get; set; }
        public decimal servicepricemoney_bhyt { get; set; }
        public decimal thu_chenhlech { get; set; }
        public decimal bn_thanhtoan { get; set; }
        public decimal bhyt_thanhtoan { get; set; }
        public decimal tongthu { get; set; }
        public int isgroup { get; set; }

        public int bhyt_tuyenbenhvien { get; set; }
        public int bhyt_loaiid { get; set; }
        public int loaivienphiid { get; set; }
        public int du5nam6thangluongcoban { get; set; }
        public int thangluongcoban { get; set; }
    }
}
