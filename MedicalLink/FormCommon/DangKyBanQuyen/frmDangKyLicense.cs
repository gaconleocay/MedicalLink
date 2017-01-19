using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;
using System.Globalization;
using MedicalLink.Base;


namespace MedicalLink.FormCommon.DangKyBanQuyen
{
    public partial class frmDangKyLicense : Form
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private string MaMayMaHoa = String.Empty;
        public frmDangKyLicense()
        {
            InitializeComponent();
        }

        private void frmConnectDB_Load(object sender, EventArgs e)
        {
            try
            {
                LayThongTinMaMayVaMaHoa();
                HienThiThongTinVeLicense();
                KiemTraTaiKhoanCoPhaiAdmin();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tbnDBLuu_Click(object sender, EventArgs e)
        {
            try
            {
                //Luu key kich hoat vao DB
                string update_license = "UPDATE tools_clients SET clientlicense='" + txtKeyKichHoat.Text.Trim() + "' WHERE clientcode='" + MaMayMaHoa + "' ;";
                condb.ExecuteNonQuery(update_license);
                timerThongBao.Start();
                lblThongBao.Visible = true;
                lblThongBao.Text = "Lưu mã kích hoạt thành công";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Kiem tra License
        private void btnDBKiemTra_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtKeyKichHoat.Text.Trim()))
                {
                    //Giai ma
                    string makichhoat_giaima = MedicalLink.FormCommon.DangKyBanQuyen.EncryptAndDecryptLicense.Decrypt(txtKeyKichHoat.Text.Trim(), true);
                    //Tach ma kich hoat:
                    string mamay_keykichhoat = "";
                    long thoigianTu = 0;
                    long thoigianDen = 0;
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
                            lblThoiGianSuDung.Text = "Từ: " + thoigianTu_text + " đến: " + thoigianDen_text;
                        }
                        else
                        {
                            SessionLogin.KiemTraLicenseSuDung = false;
                            lblThoiGianSuDung.Text = "Mã kích hoạt hết hạn sử dụng";
                        }
                    }
                    else
                    {
                        SessionLogin.KiemTraLicenseSuDung = false;
                        lblThoiGianSuDung.Text = "Sai mã kích hoạt";
                    }

                    //    mamay_keykichhoat = makichhoat_tach[1];
                    //    thoigianTu = Convert.ToInt64(makichhoat_tach[2].ToString().Trim() ?? "0");
                    //    thoigianDen = Convert.ToInt64(makichhoat_tach[3].ToString().Trim() ?? "0");
                    //string thoigianTu_text = DateTime.ParseExact(thoigianTu.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture).ToString("HH:mm:ss dd-MM-yyyy");
                    //string thoigianDen_text = DateTime.ParseExact(thoigianDen.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture).ToString("HH:mm:ss dd-MM-yyyy");
                    ////Kiem tra License hop le
                    //if (mamay_keykichhoat == txtMaMay.Text.Trim() && datetime < thoigianDen)
                    //{
                    //    //lblThoiGianSuDung.Text = "<color=green>Từ: </color>" + thoigianTu_text + "<color=green> đến: </color>" + thoigianDen_text;
                    //    lblThoiGianSuDung.Text = "Từ: " + thoigianTu_text + " đến: " + thoigianDen_text;
                    //    SessionLogin.KiemTraLicenseSuDung = true;
                    //}
                    //else
                    //{
                    //    lblThoiGianSuDung.Text = "none";
                    //    SessionLogin.KiemTraLicenseSuDung = false;
                    //}

                }
                else
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Chưa nhập mã kích hoạt!";
                    lblThoiGianSuDung.Text = "none";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LayThongTinMaMayVaMaHoa()
        {
            try
            {
                MaMayMaHoa = MedicalLink.FormCommon.DangKyBanQuyen.kiemTraLicenseHopLe.LayThongTinMaMayVaMaHoa();
                //string cpuId = "";
                //string mainId = "";
                //string hddId = "";
                //string KeyPhanCungMayTinh = "";
                ////Lay ID CPU + main + ID HDD
                //cpuId = HardwareInfo.GetProcessorId();
                //mainId = HardwareInfo.GetBoardMaker();
                //hddId = HardwareInfo.GetHDDSerialNo();

                //KeyPhanCungMayTinh = cpuId + mainId + hddId;
                //if (KeyPhanCungMayTinh != "")
                //{
                //    //ma hoa chuoi
                //    MaMayMaHoa = EncryptAndDecryptLicense.Encrypt(KeyPhanCungMayTinh, true);
                //}
                //else
                //{
                //    MessageBox.Show("Không lấy được thông tin phần cứng máy tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void HienThiThongTinVeLicense()
        {
            try
            {
                txtMaMay.Text = MaMayMaHoa;
                txtMaMay.ReadOnly = true;
                //Load License tu DB ra
                string kiemtra_licensetag = "SELECT * FROM tools_clients WHERE clientcode='" + MaMayMaHoa + "' ;";
                DataView dv = new DataView(condb.getDataTable(kiemtra_licensetag));
                if (dv != null && dv.Count > 0)
                {
                    txtKeyKichHoat.Text = dv[0]["clientlicense"].ToString();
                }

                txtKeyKichHoat.Focus();
                btnDBKiemTra_Click(null, null);
            }
            catch (Exception)
            {
            }
        }

        private void KiemTraTaiKhoanCoPhaiAdmin()
        {
            try
            {
                if (SessionLogin.SessionUsercode == MedicalLink.Base.KeyTrongPhanMem.AdminUser_key)
                {
                    linkLaykeyKichHoat.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        //Lay key kich hoat. Chi danh cho Admin
        private void linkLaykeyKichHoat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmLayLicense layLicense = new frmLayLicense();
                layLicense.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();    //Clear if any old value is there in Clipboard        
                Clipboard.SetText(txtMaMay.Text); //Copy text to Clipboard
                //string strClip = Clipboard.GetText(); //Get text from Clipboard
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void linkTroGiup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Copy mã máy và liên hệ với tác giả để được cung cấp mã kích hoạt.\nAuthor: Hồng Minh Nhất \nE-mail: hongminhnhat15@gmail.com \nPhone: 0868-915-456", "Thông báo !!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                
                throw;
            }
        }


    }
}
