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
using System.Net;
using System.Diagnostics;
using MedicalLink.Base;

namespace MedicalLink.FormCommon
{
    public partial class frmLogin : Form
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string adminuser = MedicalLink.Base.KeyTrongPhanMem.AdminUser_key;
        string adminpass = MedicalLink.Base.KeyTrongPhanMem.AdminPass_key;
        NpgsqlConnection conn;
        public frmLogin()
        {
            InitializeComponent();
        }

        #region Load
        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                if (KiemTraKetNoiDenCoSoDuLieu() == false)
                {
                    MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                KiemTraInsertMayTram();
                LoadDataFromDatabase();

                if (ConfigurationManager.AppSettings["LoginUser"].ToString() != "" && ConfigurationManager.AppSettings["LoginPassword"].ToString() != "")
                {
                    this.txtUsername.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["LoginUser"].ToString(), true);
                    this.txtPassword.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["LoginPassword"].ToString(), true);
                    this.checkEditNhoPass.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["checkEditNhoPass"]);
                }
                else
                {
                    this.txtUsername.Text = "";
                    this.txtPassword.Text = "";
                }

                txtUsername.Focus();

                // Lưu biến Session
                SessionLogin.SessionMachineName = Environment.MachineName;
                // Địa chỉ Ip
                String strHostName = Dns.GetHostName();
                IPHostEntry iphostentry = Dns.GetHostByName(strHostName);
                //int nIP = 0;
                string listIP = "";
                for (int i = 0; i < iphostentry.AddressList.Count(); i++)
                {
                    listIP += iphostentry.AddressList[i] + ";";
                }
                SessionLogin.SessionMyIP = listIP;
                //foreach (IPAddress ipaddress in iphostentry.AddressList)
                //{
                //    SessionLogin.SessionMyIP = ipaddress.ToString();
                //}
                // Lấy version PM
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                SessionLogin.SessionVersion = fvi.FileVersion;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private bool KiemTraKetNoiDenCoSoDuLieu()
        {
            bool result = false;
            try
            {
                string serverhost = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString().Trim() ?? "", true);
                string serveruser = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username"].ToString().Trim(), true);
                string serverpass = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password"].ToString().Trim(), true);
                string serverdb = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString().Trim(), true);


                if (conn == null)
                    conn = new NpgsqlConnection("Server=" + serverhost + ";Port=5432;User Id=" + serveruser + "; " + "Password=" + serverpass + ";Database=" + serverdb + ";CommandTimeout=1800000;");
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                result = true;
            }
            catch (Exception ex)
            {
                Logging.Error("Loi ket noi den CSDL: " + ex.ToString());
            }
            return result;
        }
        private void KiemTraInsertMayTram()
        {
            try
            {
                SessionLogin.MaDatabase = MedicalLink.FormCommon.DangKyBanQuyen.kiemTraLicenseHopLe.LayThongTinMaDatabase();
                string tenmay = MedicalLink.FormCommon.DangKyBanQuyen.HardwareInfo.GetComputerName();
                string license_trang = MedicalLink.Base.EncryptAndDecrypt.Encrypt("", true);

                string kiemtra_client = "SELECT * FROM tools_license WHERE datakey='" + SessionLogin.MaDatabase + "' ;";
                DataView dv = new DataView(condb.getDataTable(kiemtra_client));
                if (dv != null && dv.Count > 0)
                {
                    //Kiem tra license
                    //MedicalLink.FormCommon.DangKyBanQuyen.kiemTraLicenseHopLe.KiemTraLicenseHopLe();
                }
                else
                {
                    string insert_client = "INSERT INTO tools_license(datakey, licensekey) VALUES ('" + SessionLogin.MaDatabase + "','" + license_trang + "' );";
                    condb.ExecuteNonQuery(insert_client);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void LoadDataFromDatabase()
        {
            try
            {
                //Set default
                MedicalLink.GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp = 0;
                MedicalLink.GlobalStore.KhoangThoiGianLayDuLieu = DateTime.Now.Year - 1 + "-01-01 00:00:00";

                //Load thong tin Luu vao GlobalStore
                string sqlDSOption = "SELECT toolsoptionid, toolsoptioncode, toolsoptionname, toolsoptionvalue, toolsoptionnote FROM tools_option WHERE toolsoptionlook<>'1' ;";
                DataView dataOption = new DataView(condb.getDataTable(sqlDSOption));
                if (dataOption != null && dataOption.Count > 0)
                {
                    for (int i = 0; i < dataOption.Count; i++)
                    {
                        if (dataOption[i]["toolsoptioncode"].ToString().ToUpper() == "ThoiGianCapNhatTbl_tools_bndangdt_tmp".ToUpper())
                        {
                            MedicalLink.GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp = Utilities.Util_TypeConvertParse.ToInt64(dataOption[i]["toolsoptionvalue"].ToString());
                        }

                        if (dataOption[i]["toolsoptioncode"].ToString().ToUpper() == "KhoangThoiGianLayDuLieu".ToUpper())
                        {
                            MedicalLink.GlobalStore.KhoangThoiGianLayDuLieu = dataOption[i]["toolsoptionvalue"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Mã hóa thông tin để so sánh trong DB
                string en_txtUsername = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUsername.Text.Trim(), true);
                string en_txtPassword = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtPassword.Text.Trim(), true);

                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Có lỗi xảy ra!");
                    txtUsername.Focus();
                    return;
                }
                // tạo 1 tài khoản ở trên PM, không chứa trong DB để làm tài khoản admin
                else if (txtUsername.Text == adminuser && txtPassword.Text == adminpass)
                {
                    SessionLogin.SessionUsercode = txtUsername.Text;
                    SessionLogin.SessionUsername = "Administrator";
                    //Load data
                    SessionLogin.SessionlstPhanQuyenChucNang = MedicalLink.Base.CheckPermission.GetPhanQuyenChucNang();
                    SessionLogin.SessionlstPhanQuyenKhoaPhong = MedicalLink.Base.CheckPermission.GetPhanQuyenKhoaPhong();
                    frmMain frmm = new frmMain();
                    frmm.Show();
                    this.Visible = false;
                    MedicalLink.Base.Logging.Info("Application open successfull. Time=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"));
                }
                else
                {
                    try
                    {
                        string command = "SELECT usercode, username, userpassword FROM tools_tbluser WHERE usercode='" + en_txtUsername + "' and userpassword='" + en_txtPassword + "';";
                        DataView dv = new DataView(condb.getDataTable(command));
                        if (dv != null && dv.Count > 0)
                        {
                            MedicalLink.FormCommon.DangKyBanQuyen.kiemTraLicenseHopLe.KiemTraLicenseHopLe();
                            SessionLogin.SessionUsercode = txtUsername.Text;
                            SessionLogin.SessionUsername = MedicalLink.Base.EncryptAndDecrypt.Decrypt(dv[0]["username"].ToString(), true);
                            //Load data
                            SessionLogin.SessionlstPhanQuyenChucNang = MedicalLink.Base.CheckPermission.GetPhanQuyenChucNang();
                            SessionLogin.SessionlstPhanQuyenKhoaPhong = MedicalLink.Base.CheckPermission.GetPhanQuyenKhoaPhong();
                            frmMain frmm = new frmMain();
                            frmm.Show();
                            this.Visible = false;
                            MedicalLink.Base.Logging.Info("Application open successfull. Time=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"));
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng", "Có lỗi xảy ra");
                        }
                    }
                    catch (Exception ex)
                    {
                        MedicalLink.Base.Logging.Error(ex);
                        txtUsername.Focus();
                    }
                }

                // Khi được check vào nút ghi nhớ thì sẽ lưu tên đăng nhập và mật khẩu vào file config
                if (checkEditNhoPass.Checked)
                {
                    Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    _config.AppSettings.Settings["checkEditNhoPass"].Value = Convert.ToString(checkEditNhoPass.Checked);
                    _config.AppSettings.Settings["LoginUser"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUsername.Text.Trim(), true);
                    _config.AppSettings.Settings["LoginPassword"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtPassword.Text.Trim(), true);
                    _config.Save(ConfigurationSaveMode.Modified);

                    ConfigurationManager.RefreshSection("appSettings");
                }
                // không thì sẽ xóa giá trị đã lưu trong file congfig đi
                else
                {
                    Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    _config.AppSettings.Settings["checkEditNhoPass"].Value = "false";
                    _config.AppSettings.Settings["LoginUser"].Value = "";
                    _config.AppSettings.Settings["LoginPassword"].Value = "";
                    _config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn("Dang nhap " + ex.ToString());
            }
        }

        // Khi nhập username và nhấn enter thì forcus vào ô nhập pass
        private void txtUsername_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        // Sau khi nhập password và ấn enter thì đăng nhập luôn
        private void txtPassword_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        // nếu viết vào ô username = "config" thì mở ra bảng để cấu hình DB
        private void txtUsername_EditValueChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text.ToUpper() == "CONFIG")
            {
                frmConnectDB frmcon = new frmConnectDB();
                frmcon.Dock = System.Windows.Forms.DockStyle.Bottom;
                frmcon.ShowDialog();
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Application.Exit();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void linkTroGiup_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Liên hệ với tác giả để được trợ giúp! \nAuthor: Hồng Minh Nhất \nE-mail: hongminhnhat15@gmail.com \nPhone: 0868-915-456", "Thông tin về tác giả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
