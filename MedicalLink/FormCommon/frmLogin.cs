﻿using System;
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
using System.IO;
using MedicalLink.Utilities;
using MedicalLink.ClassCommon;

namespace MedicalLink.FormCommon
{
    public partial class frmLogin : Form
    {
        #region Khai bao
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private NpgsqlConnection conn;

        #endregion
        public frmLogin()
        {
            InitializeComponent();
        }

        #region Load
        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLogoBenhVien();
                if (KiemTraKetNoiDenCoSoDuLieu() == false)
                {
                    MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                SessionLogin.SessionVersion = fvi.FileVersion;
                KiemTraVaCopyFileLaucherNew();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadLogoBenhVien()
        {
            try
            {
                picture_Logobenhvien.Image = Image.FromFile(@"Picture\Logo_benhvien.jpg");
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
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
                O2S_Common.Logging.LogSystem.Error("Loi ket noi den CSDL: " + ex.ToString());
            }
            return result;
        }
        private void KiemTraInsertMayTram()
        {
            try
            {
                SessionLogin.MaDatabase = MedicalLink.FormCommon.DangKyBanQuyen.KiemTraLicense.LayThongTinMaDatabase();
                string tenmay = MedicalLink.FormCommon.DangKyBanQuyen.HardwareInfo.GetComputerName();
                string license_trang = MedicalLink.Base.EncryptAndDecrypt.Encrypt("", true);

                string kiemtra_client = "SELECT * FROM tools_license WHERE datakey='" + SessionLogin.MaDatabase + "' ;";
                DataView dv = new DataView(condb.GetDataTable_MeL(kiemtra_client));
                if (dv != null && dv.Count > 0)
                {
                    //Kiem tra license
                    //MedicalLink.FormCommon.DangKyBanQuyen.kiemTraLicenseHopLe.KiemTraLicenseHopLe();
                }
                else
                {
                    string insert_client = "INSERT INTO tools_license(datakey, licensekey) VALUES ('" + SessionLogin.MaDatabase + "','" + license_trang + "' );";
                    condb.ExecuteNonQuery_MeL(insert_client);
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private void LoadDataFromDatabase()
        {
            try
            {
                BUS.LoadDataSystems.LoadDanhSachCauHinhDungChung();
                BUS.LoadDataSystems.LoadCauHinhThoiGianLayDuLieu();
                //BUS.LoadDataSystems.LoadDanhSachKhoaTrongBenhVien(); //chua su dung
                BUS.LoadDataSystems.LoadDanhMucDichVuKyThuat();
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        #region kiem tra va copy ban moi
        private void KiemTraVaCopyFileLaucherNew()
        {
            try
            {
                DataView dataurlfile = new DataView(condb.GetDataTable_MeL("select app_link from tools_version where app_type=1 limit 1;"));
                if (dataurlfile != null && dataurlfile.Count > 0)
                {
                    string tempDirectory = dataurlfile[0]["app_link"].ToString();
                    CopyFolder_CheckSum(tempDirectory, Environment.CurrentDirectory);
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
        private static void CopyFolder_CheckSum(string SourceFolder, string DestFolder)
        {
            Directory.CreateDirectory(DestFolder); //Tao folder moi
            string[] files = Directory.GetFiles(SourceFolder);
            //Neu co file thy phai copy file
            foreach (string file in files)
            {
                try
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(DestFolder, name);
                    if (name.Contains("MedicalLinkLauncher"))
                    {
                        //Check file
                        if (FileCheckSum.GetMD5HashFromFile(file) != FileCheckSum.GetMD5HashFromFile(dest))
                        {
                            File.Copy(file, dest, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    continue;
                     O2S_Common.Logging.LogSystem.Error("Lỗi copy file check_sum" + ex.ToString());
                }
            }

            string[] folders = Directory.GetDirectories(SourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(DestFolder, name);
                CopyFolder_CheckSum(folder, dest);
            }
        }

        #endregion


        #endregion

        #region Events
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Mã hóa thông tin để so sánh trong DB
                string en_txtUsername = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtUsername.Text.Trim().ToLower(), true);
                string en_txtPassword = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtPassword.Text.Trim(), true);

                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.Focus();
                    return;
                }
                // tạo 1 tài khoản ở trên PM, không chứa trong DB để làm tài khoản admin
                else if (txtUsername.Text.ToLower() == Base.KeyTrongPhanMem.AdminUser_key && txtPassword.Text == Base.KeyTrongPhanMem.AdminPass_key)
                {
                    SessionLogin.SessionUsercode = txtUsername.Text.Trim().ToLower();
                    SessionLogin.SessionUsername = "Administrator";
                    SessionLogin.SessionUserHISID = "0";

                    LoadDataSauKhiDangNhap();
                }
                else
                {
                    try
                    {
                        string command = "SELECT userid, usercode, username, userpassword,coalesce(userhisid,0) as userhisid FROM tools_tbluser WHERE usercode='" + en_txtUsername + "' and userpassword='" + en_txtPassword + "';";
                        DataView dv = new DataView(condb.GetDataTable_MeL(command));
                        if (dv != null && dv.Count > 0)
                        {
                            MedicalLink.FormCommon.DangKyBanQuyen.KiemTraLicense.KiemTraLicenseHopLe();
                            SessionLogin.SessionUserID = Utilities.TypeConvertParse.ToInt64(dv[0]["userid"].ToString());
                            SessionLogin.SessionUsercode = txtUsername.Text.Trim().ToLower();
                            SessionLogin.SessionUsername = MedicalLink.Base.EncryptAndDecrypt.Decrypt(dv[0]["username"].ToString(), true);
                            SessionLogin.SessionUserHISID = dv[0]["userhisid"].ToString();
                            //Load data
                            LoadDataSauKhiDangNhap();
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        O2S_Common.Logging.LogSystem.Error(ex);
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
                O2S_Common.Logging.LogSystem.Error("Dang nhap " + ex.ToString());
            }
        }

        private void LoadDataSauKhiDangNhap()
        {
            try
            {
                //Load data
                SessionLogin.LstPhanQuyenUser = Base.CheckPermission.GetListPhanQuyenUser();
                SessionLogin.LstPhanQuyen_KhoaPhong = Base.CheckPermission.GetPhanQuyen_KhoaPhong();
                SessionLogin.LstPhanQuyen_KhoThuoc = Base.CheckPermission.GetPhanQuyen_KhoThuoc();
                SessionLogin.LstPhanQuyen_PhongLuu = Base.CheckPermission.GetPhanQuyen_PhongLuu();
                frmMain frmm = new frmMain();
                frmm.Show();
                this.Visible = false;
                O2S_Common.Logging.LogSystem.Info("Application open successfull. Time=" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"));
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        private void linkTroGiup_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Liên hệ với quản trị để được trợ giúp!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }


        #endregion

        #region Custom
        private void txtUsername_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

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
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #endregion




    }
}
