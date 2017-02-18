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
        public static string SessionUsercode { get; set; }  // ID user
        public static string SessionUsername { get; set; }  // Tên user
        public static string SessionMachineName { get; set; }   // Tên máy
        public static string SessionMyIP { get; set; }  // Địa chỉ IP máy
        public static string SessionVersion { get; set; } // Version phần mềm
        //public static string SessionDateTime { get; set; }  // Lấy thời gian
        public static bool KiemTraLicenseSuDung { get; set; } //kiem tra license: neu false thi out phan mem, neu true thi cho su dung tiep
        public static string License_KeyDB { get; set; } //License lay tu DB

        public static string MaDatabase { get; set; }//Lay thong tin database

        public static List<ClassCommon.classPermission> SessionlstPhanQuyenChucNang { get; set; }
        public static List<ClassCommon.classUserDepartment> SessionlstPhanQuyenKhoaPhong { get; set; }

    }


}

