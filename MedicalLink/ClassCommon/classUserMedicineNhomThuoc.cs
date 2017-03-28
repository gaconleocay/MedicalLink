using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.ClassCommon
{
    public class classUserMedicineNhomThuoc
    {
        public bool NhomThuocCheck { get; set; }
        public Int32 NhomThuocId { get; set; }
        public string NhomThuocCode { get; set; }
        public string NhomThuocName { get; set; }
        public Int32 NhomThuocType { get; set; } //=1: nha thuoc
        public string NhomThuocTypeName { get; set; }
    }
}
