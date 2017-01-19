using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ChucNang.ImportDMDichVu
{
    public class ClassDichVu
    {
        public string MA_DV { get; set; }
        public string MA_NHOM { get; set; }
        public string MA_DV_USER { get; set; }
        public string MA_DV_STTTHAU { get; set; }
        public string TEN_VP { get; set; }
        public string TEN_BH { get; set; }
        public string DVT { get; set; }
        public long GIA_BH { get; set; }
        public long GIA_VP { get; set; }
        public long GIA_YC { get; set; }
        public long GIA_NNN { get; set; }
        public string THOIGIAN_APDUNG { get; set; }
        public string THEO_NGAY_CHI_DINH { get; set; }
        public string LOAI_PTTT { get; set; }
        public int KHOA { get; set; }

    }
}
