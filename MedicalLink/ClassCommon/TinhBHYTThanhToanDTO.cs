using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class TinhBHYTThanhToanDTO
    {
        public string bhytcode { get; set; }
        public int bhyt_loaiid { get; set; }
        public int loaivienphiid { get; set; }
        public int du5nam6thangluongcoban { get; set; }
        public int du5nam { get; set; }
        public int _6thangluongcoban { get; set; }
        public int bhyt_tuyenbenhvien { get; set; }
        public decimal chiphi_trongpvql { get; set; }
        public decimal chiphi_goidvktc { get; set; }
        public decimal chiphi_ngoaigoidvktc { get; set; }
        public decimal bhyt_thangluongtoithieu { get; set; }
        public decimal _15thanhluongcoban { get; set; }
        public decimal _46thanhluongcoban { get; set; }
        public int maquyenloithe { get; set; }

        public decimal tyle_bntt_tb { get; set; }

    }
}
