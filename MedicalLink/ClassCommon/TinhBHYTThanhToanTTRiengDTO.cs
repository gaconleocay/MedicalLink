using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class TinhBHYTThanhToanTTRiengDTO
    {
        public decimal tongtien_vtyttran { get; set; }
        public decimal stent1 { get; set; }
        public decimal stent2 { get; set; }
        public string bhytcode { get; set; }
        public int bhyt_loaiid { get; set; } //= 1 đúng tuyến; =2: đúng tuyến giới thiệu; =3 đúng tuyến cấp cứu; =4 trái tuyến
        public int loaivienphiid { get; set; }// loaivienphiid=0 nội trú; =1 ngoại trú
        public int du5nam6thangluongcoban { get; set; }
        public int du5nam { get; set; }
        public int bhyt_tuyenbenhvien { get; set; }// bhyt_tuyenbenhvien=1: huyen; =2: tinh; ==3 TW
        public int maquyenloithe { get; set; } //tmp de tinh tu bhytcode

        //public int _6thangluongcoban { get; set; }
        //public decimal chiphi_trongpvql { get; set; }
        //public decimal chiphi_goidvktc { get; set; }
        //public decimal chiphi_ngoaigoidvktc { get; set; }
        //public decimal bhyt_thangluongtoithieu { get; set; }
        //public decimal _15thanhluongcoban { get; set; }
        //public decimal _46thanhluongcoban { get; set; }
        //public decimal tyle_bntt_tb { get; set; }

    }
}
