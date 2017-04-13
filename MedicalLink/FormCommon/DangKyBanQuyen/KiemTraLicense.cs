using MedicalLink.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.FormCommon.DangKyBanQuyen
{
    internal static class KiemTraLicense
    {
        static MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        //private static string license_keydb = "";

        internal static void KiemTraLicenseHopLe()
        {
            try
            {
                SessionLogin.KiemTraLicenseSuDung = false;
                string license_keydb = "";
                //Load License tu DB ra
                string kiemtra_licensetag = "SELECT datakey,licensekey FROM tools_license WHERE datakey='" + SessionLogin.MaDatabase + "' ;";
                DataView dataLicense = new DataView(condb.getDataTable(kiemtra_licensetag));
                if (dataLicense != null && dataLicense.Count > 0)
                {
                    license_keydb = dataLicense[0]["licensekey"].ToString();
                }

                if (license_keydb != "")
                {
                    //Giai ma
                    string makichhoat_giaima = EncryptAndDecrypt.Decrypt(license_keydb, true);
                    //Tach ma kich hoat:
                    string mamay_keykichhoat = "";
                    string mabanquyenkhongthoihan = "";
                    long datetimenow = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
                    //lay thoi gian may chu database: neu khong lay duoc thi lay thoi gian tren may client
                    try
                    {
                        string sql_dateDB = "SELECT TO_CHAR(NOW(), 'yyyyMMdd') as sysdatedb;";
                        DataView dtdatetime = new DataView(condb.getDataTable(sql_dateDB));
                        if (dtdatetime != null && dtdatetime.Count > 0)
                        {
                            datetimenow = Utilities.Util_TypeConvertParse.ToInt64(dtdatetime[0]["sysdatedb"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        Base.Logging.Error(ex);
                    }

                    if (!String.IsNullOrEmpty(makichhoat_giaima))
                    {
                        string[] makichhoat_tach = makichhoat_giaima.Split('$');
                        if (makichhoat_tach.Length == 4)
                        {
                            mamay_keykichhoat = makichhoat_tach[1];
                            //Thoi gian hien tai
                            datetimenow = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));

                            //Kiem tra License hop le
                            if (mamay_keykichhoat == SessionLogin.MaDatabase && datetimenow <= Convert.ToInt64(makichhoat_tach[3].ToString().Trim() ?? "0"))
                            {
                                SessionLogin.KiemTraLicenseSuDung = true;
                            }
                        }
                        else if (makichhoat_tach.Length == 3)
                        {
                            mamay_keykichhoat = makichhoat_tach[1];
                            mabanquyenkhongthoihan = makichhoat_tach[2];
                            if (mamay_keykichhoat == SessionLogin.MaDatabase && mabanquyenkhongthoihan == Base.KeyTrongPhanMem.BanQuyenKhongThoiHan)
                            {
                                SessionLogin.KiemTraLicenseSuDung = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn("Kiem tra license " + ex.ToString());
            }
        }

        internal static string LayThongTinMaDatabase()
        {
            string MaDatabase = "";
            try
            {
                string sqlLayMaDatabase = "SELECT datid, datname FROM pg_stat_activity where pid=(select pg_backend_pid());";
                DataView dataMaDB = new DataView(condb.getDataTable(sqlLayMaDatabase));
                if (dataMaDB != null && dataMaDB.Count > 0)
                {
                    MaDatabase = dataMaDB[0]["datid"].ToString() + dataMaDB[0]["datname"].ToString();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn("Lay thong tin ma database " + ex.ToString());
            }
            return MaDatabase;
        }

        internal static string KiemTraThoiHanLicense(string makichhoat_mahoa)
        {
            string thoiGianSuDung = "";
            try
            {
                //Giai ma
                string makichhoat_giaima = EncryptAndDecrypt.Decrypt(makichhoat_mahoa, true);
                //Tach ma kich hoat:
                string mamay_keykichhoat = "";
                long thoigianTu = 0;
                long thoigianDen = 0;
                string[] makichhoat_tach = makichhoat_giaima.Split('$');
                string mabanquyenkhongthoihan = "";

                if (makichhoat_tach.Length == 4)
                {
                    mamay_keykichhoat = makichhoat_tach[1];
                    thoigianTu = Convert.ToInt64((makichhoat_tach[2].ToString().Trim() ?? "0") + "000000");
                    thoigianDen = Convert.ToInt64((makichhoat_tach[3].ToString().Trim() ?? "0") + "235959");
                    //Thoi gian hien tai
                    long datetime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    string thoigianTu_text = DateTime.ParseExact(thoigianTu.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
                    string thoigianDen_text = DateTime.ParseExact(thoigianDen.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
                    //Kiem tra License hop le
                    if (mamay_keykichhoat == SessionLogin.MaDatabase && datetime < thoigianDen)
                    {
                        thoiGianSuDung = "Từ: " + thoigianTu_text + " đến: " + thoigianDen_text;
                    }
                    else
                    {
                        thoiGianSuDung = "Mã kích hoạt hết hạn sử dụng";
                    }
                }
                else if (makichhoat_tach.Length == 3)
                {
                    mamay_keykichhoat = makichhoat_tach[1];
                    mabanquyenkhongthoihan = makichhoat_tach[2];
                    if (mamay_keykichhoat == SessionLogin.MaDatabase && mabanquyenkhongthoihan == Base.KeyTrongPhanMem.BanQuyenKhongThoiHan)
                    {
                        thoiGianSuDung = "License không thời hạn!";
                    }
                    else
                    {
                        thoiGianSuDung = "Mã kích hoạt hết hạn sử dụng";
                    }
                }
                else
                {
                    thoiGianSuDung = "Sai mã kích hoạt";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn("Kiem tra license " + ex.ToString());
            }
            return thoiGianSuDung;
        }






    }
}
