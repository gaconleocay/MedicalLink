using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using DevExpress.XtraGrid.Views.Grid;
using System.Net;
using System.Diagnostics;


namespace MedicalLink.Base
{
    /// <summary>
    /// Clast lưu biến tên user đăng nhập hệ thống để sử dụng cho mọi nơi trong chương trình
    /// </summary>
    public static class SessionLogin
    {
        public static long SessionUserID { get; set; }  // ID user
        public static string SessionUsercode { get; set; }  //  user code
        public static string SessionUsername { get; set; }  // Tên user
        public static string SessionMachineName { get; set; }   // Tên máy
        public static string SessionMyIP { get; set; }  // Địa chỉ IP máy
        public static string SessionVersion { get; set; } // Version phần mềm
        public static bool KiemTraLicenseSuDung { get; set; } //kiem tra license: neu false thi out phan mem, neu true thi cho su dung tiep
        public static string License_KeyDB { get; set; } //License lay tu DB
        public static string MaDatabase { get; set; }//Lay thong tin database
        public static List<ClassCommon.classPermission> LstPhanQuyenUser { get; set; }
        public static List<ClassCommon.classUserDepartment> SessionlstPhanQuyen_KhoaPhong { get; set; }
        public static List<ClassCommon.classUserMedicineStore> SessionLstPhanQuyen_KhoThuoc { get; set; }
        public static List<ClassCommon.classUserMedicinePhongLuu> SessionLstPhanQuyen_PhongLuu { get; set; }


        #region Chuc nang cho Tab menu
        public static List<ClassCommon.classPermission> LstPhanQuyen_HeThong { get; set; }
        public static List<ClassCommon.classPermission> LstPhanQuyen_ChucNang { get; set; }
        public static List<ClassCommon.classPermission> LstPhanQuyen_BaoCaoKhoa { get; set; }
        public static List<ClassCommon.classPermission> LstPhanQuyen_BaoCaoDoanhThu { get; set; }
        public static List<ClassCommon.classPermission> LstPhanQuyen_BaoCaoIn { get; set; }
        public static List<ClassCommon.classPermission> LstPhanQuyen_QLTaiChinh { get; set; }
        public static List<ClassCommon.classPermission> LstPhanQuyen_Dashboard { get; set; }



        #endregion


    }


}

