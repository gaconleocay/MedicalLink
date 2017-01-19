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
    internal static class kiemTraLicenseHopLe
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
                string kiemtra_licensetag = "SELECT * FROM tools_clients WHERE clientcode='" + SessionLogin.MaMayTinhNguoiDungMaHoa + "' ;";
                DataView dv = new DataView(condb.getDataTable(kiemtra_licensetag));
                if (dv != null && dv.Count > 0)
                {
                    license_keydb = dv[0]["clientlicense"].ToString();
                }

                if (license_keydb != "")
                {
                    //Giai ma
                    string makichhoat_giaima = MedicalLink.FormCommon.DangKyBanQuyen.EncryptAndDecryptLicense.Decrypt(license_keydb, true);
                    //Tach ma kich hoat:
                    string mamay_keykichhoat = "";
                    long thoigianTu = 0;
                    long thoigianDen = 0;
                    if (!String.IsNullOrEmpty(makichhoat_giaima))
                    {
                        string[] makichhoat_tach = makichhoat_giaima.Split('$');
                        if (makichhoat_tach.Length == 4)
                        {
                            mamay_keykichhoat = makichhoat_tach[1];
                            thoigianTu = Convert.ToInt64(makichhoat_tach[2].ToString().Trim() ?? "0");
                            thoigianDen = Convert.ToInt64(makichhoat_tach[3].ToString().Trim() ?? "0");

                            //Thoi gian hien tai
                            long datetime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                            string thoigianTu_text = DateTime.ParseExact(thoigianTu.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture).ToString("HH:mm:ss dd-MM-yyyy");
                            string thoigianDen_text = DateTime.ParseExact(thoigianDen.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture).ToString("HH:mm:ss dd-MM-yyyy");
                            //Kiem tra License hop le
                            if (mamay_keykichhoat == SessionLogin.MaMayTinhNguoiDungMaHoa && datetime < thoigianDen)
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

        internal static string LayThongTinMaMayVaMaHoa()
        {
            string MaMayMaHoa = "";
            try
            {
                string cpuId = "";
                string mainId = "";
                string hddId = "";
                string KeyPhanCungMayTinh = "";

                //Lay ID CPU + main + ID HDD
                cpuId = HardwareInfo.GetProcessorId();
                mainId = HardwareInfo.GetBoardMaker();
                hddId = HardwareInfo.GetHDDSerialNo();
                KeyPhanCungMayTinh = cpuId.Replace(" ", "") + mainId.Replace(" ", "") + hddId.Replace(" ", "");
                if (KeyPhanCungMayTinh != "")
                {
                    //ma hoa chuoi
                    MaMayMaHoa = EncryptAndDecryptLicense.Encrypt(KeyPhanCungMayTinh, true);
                    //MaMayMaHoa = MedicalLink.FormCommon.DangKyBanQuyen.ClassEncoding.MaHoaCoDinhDang(KeyPhanCungMayTinh, "UTF-8_Bytes");
                }
                else
                {
                    MaMayMaHoa = EncryptAndDecryptLicense.Encrypt("", true);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn("Lay thong tin ma may va ma hoa " + ex.ToString());
            }
            return MaMayMaHoa;
        }








    }
}
