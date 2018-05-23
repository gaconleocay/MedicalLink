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
using System.IO;
using MedicalLink.Utilities;
using MedicalLink.ClassCommon;

namespace MedicalLink.FormCommon
{
    public partial class frmLogin : Form
    {
        #region Khai bao
        private MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
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
                MedicalLink.Base.Logging.Warn(ex);
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
                Base.Logging.Error(ex);
            }
        }
        private void LoadDataFromDatabase()
        {
            try
            {
                LoadDanhSachCauHinhDungChung();
                LoadCauHinhThoiGianLayDuLieu();
                //LoadDanhSachKhoaTrongBenhVien(); //chua su dung
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadCauHinhThoiGianLayDuLieu()
        {
            try
            {
                //Set default
                MedicalLink.GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp = 0;
                MedicalLink.GlobalStore.KhoangThoiGianLayDuLieu = DateTime.Now.Year - 1 + "-01-01 00:00:00";
                if (GlobalStore.lstOption != null && GlobalStore.lstOption.Count > 0)
                {
                    GlobalStore.ThoiGianCapNhatTbl_tools_bndangdt_tmp = Util_TypeConvertParse.ToInt64(GlobalStore.lstOption.Where(o => o.toolsoptioncode == "ThoiGianCapNhatTbl_tools_bndangdt_tmp").FirstOrDefault().toolsoptionvalue);
                    GlobalStore.KhoangThoiGianLayDuLieu = GlobalStore.lstOption.Where(o => o.toolsoptioncode == "KhoangThoiGianLayDuLieu").FirstOrDefault().toolsoptionvalue;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void LoadDanhSachCauHinhDungChung()
        {
            try
            {
                //Danh muc Dung chung
                string sqlNhomDV = "select ot.tools_othertypelistid, ot.tools_othertypelistcode, ot.tools_othertypelistname, ot.tools_othertypeliststatus, ot.tools_othertypelistnote, o.tools_otherlistid, o.tools_otherlistcode, o.tools_otherlistname, o.tools_otherlistvalue, o.tools_otherliststatus from tools_othertypelist ot inner join tools_otherlist o on o.tools_othertypelistid=ot.tools_othertypelistid;";
                DataTable dataLoaiBaoCao = condb.GetDataTable_MeL(sqlNhomDV);
                if (dataLoaiBaoCao != null && dataLoaiBaoCao.Rows.Count > 0)
                {
                    GlobalStore.lstOtherList_Global = new List<ToolsOtherListDTO>();
                    for (int i = 0; i < dataLoaiBaoCao.Rows.Count; i++)
                    {
                        ClassCommon.ToolsOtherListDTO otherList = new ToolsOtherListDTO();
                        otherList.tools_othertypelistid = Utilities.Util_TypeConvertParse.ToInt64(dataLoaiBaoCao.Rows[i]["tools_othertypelistid"].ToString());
                        otherList.tools_othertypelistcode = dataLoaiBaoCao.Rows[i]["tools_othertypelistcode"].ToString();
                        otherList.tools_othertypelistcode = dataLoaiBaoCao.Rows[i]["tools_othertypelistcode"].ToString();
                        //otherList.tools_othertypeliststatus = dataLoaiBaoCao.Rows[i]["tools_othertypeliststatus"].ToString();
                        otherList.tools_othertypelistnote = dataLoaiBaoCao.Rows[i]["tools_othertypelistnote"].ToString();
                        otherList.tools_otherlistid = Utilities.Util_TypeConvertParse.ToInt64(dataLoaiBaoCao.Rows[i]["tools_otherlistid"].ToString());
                        otherList.tools_otherlistcode = dataLoaiBaoCao.Rows[i]["tools_otherlistcode"].ToString();
                        otherList.tools_otherlistname = dataLoaiBaoCao.Rows[i]["tools_otherlistname"].ToString();
                        otherList.tools_otherlistvalue = dataLoaiBaoCao.Rows[i]["tools_otherlistvalue"].ToString();
                        //otherList.tools_otherliststatus = dataLoaiBaoCao.Rows[i]["tools_otherliststatus"].ToString();
                        GlobalStore.lstOtherList_Global.Add(otherList);
                    }
                }

                //Danh muc Option
                //Load thong tin Luu vao GlobalStore
                string sqlDSOption = "SELECT toolsoptionid, toolsoptioncode, toolsoptionname, toolsoptionvalue, toolsoptionnote,toolsoptionlook,toolsoptiondate,toolsoptioncreateuser FROM tools_option WHERE toolsoptionlook<>'1' ;";
                DataTable dataOption = condb.GetDataTable_MeL(sqlDSOption);
                if (dataOption != null && dataOption.Rows.Count > 0)
                {
                    GlobalStore.lstOption = Utilities.Util_DataTable.DataTableToList<ToolsOptionDTO>(dataOption);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LoadDanhSachKhoaTrongBenhVien()
        {
            try
            {
                string sqlKhoaBV = "SELECT degp.departmentgroupid,degp.departmentgroupcode,degp.departmentgroupname,degp.departmentgrouptype,de.departmentid,de.departmentcode,de.departmentname,de.departmenttype FROM departmentgroup degp inner join department de on de.departmentgroupid=degp.departmentgroupid; ";
                DataTable dataKhoaBV = condb.GetDataTable_MeL(sqlKhoaBV);
                if (dataKhoaBV != null && dataKhoaBV.Rows.Count > 0)
                {
                    GlobalStore.lstDepartmentBV = new List<DepartmentDTO>();
                    for (int i = 0; i < dataKhoaBV.Rows.Count; i++)
                    {
                        ClassCommon.DepartmentDTO otherList = new DepartmentDTO();
                        otherList.departmentgroupid = Utilities.Util_TypeConvertParse.ToInt32(dataKhoaBV.Rows[i]["departmentgroupid"].ToString());
                        otherList.departmentgroupcode = dataKhoaBV.Rows[i]["departmentgroupcode"].ToString();
                        otherList.departmentgroupname = dataKhoaBV.Rows[i]["departmentgroupname"].ToString();
                        otherList.departmentgrouptype = Utilities.Util_TypeConvertParse.ToInt32(dataKhoaBV.Rows[i]["departmentgrouptype"].ToString());
                        otherList.departmentid = Utilities.Util_TypeConvertParse.ToInt32(dataKhoaBV.Rows[i]["departmentid"].ToString());
                        otherList.departmentcode = dataKhoaBV.Rows[i]["departmentcode"].ToString();
                        otherList.departmentname = dataKhoaBV.Rows[i]["departmentname"].ToString();
                        otherList.departmenttype = Utilities.Util_TypeConvertParse.ToInt32(dataKhoaBV.Rows[i]["departmenttype"].ToString());
                        GlobalStore.lstDepartmentBV.Add(otherList);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
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
                Base.Logging.Error(ex);
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
                        if (Util_FileCheckSum.GetMD5HashFromFile(file) != Util_FileCheckSum.GetMD5HashFromFile(dest))
                        {
                            File.Copy(file, dest, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    continue;
                    Base.Logging.Error("Lỗi copy file check_sum" + ex.ToString());
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
                            SessionLogin.SessionUserID = Utilities.Util_TypeConvertParse.ToInt64(dv[0]["userid"].ToString());
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
                MedicalLink.Base.Logging.Error("Dang nhap " + ex.ToString());
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
                MedicalLink.Base.Logging.Info("Application open successfull. Time=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"));
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
                MessageBox.Show("Liên hệ với quản trị để được trợ giúp!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion




    }
}
