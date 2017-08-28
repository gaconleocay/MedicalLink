using MedicalLink.ClassCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink
{
    public class GlobalStore
    {
        public static long ThoiGianCapNhatTbl_tools_bndangdt_tmp { get; set; } //phut
        public static string KhoangThoiGianLayDuLieu { get; set; } //Định dạng: yyyy-MM-dd HH:mm:ss
        public static List<ClassCommon.ToolsOtherListDTO> lstOtherList_Global { get; set; }
        public static string SoYTe_String { get; set; }
        public static string TenBenhVien_String { get; set; }
        public static List<DepartmentDTO> lstDepartmentBV { get; set; }



    }
}
