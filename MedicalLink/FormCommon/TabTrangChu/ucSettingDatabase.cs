using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Npgsql;
using DevExpress.XtraSplashScreen;
using MedicalLink.Base;

namespace MedicalLink.FormCommon.TabTrangChu
{
    public partial class ucSettingDatabase : UserControl
    {
        private ConnectDatabase condb = new ConnectDatabase();
        public ucSettingDatabase()
        {
            InitializeComponent();
        }

        #region Load
        private void ucSettingDatabase_Load(object sender, EventArgs e)
        {
            try
            {
                LoadKetNoiDatabase();
                Load_DuongDanDenFolderVersion();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadKetNoiDatabase()
        {
            try
            {
                // Giải mã giá trị lưu trong config
                this.txtDBHost.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString().Trim(), true);
                this.txtDBPort.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerPort"].ToString().Trim(), true);
                this.txtDBUser.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username"].ToString().Trim(), true);
                this.txtDBPass.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password"].ToString().Trim(), true);
                this.txtDBName.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString().Trim(), true);
                this.txtDBHost_MeL.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost_MeL"].ToString().Trim(), true);
                this.txtDBPort_MeL.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerPort_MeL"].ToString().Trim(), true);
                this.txtDBUser_MeL.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username_MeL"].ToString().Trim(), true);
                this.txtDBPass_MeL.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password_MeL"].ToString().Trim(), true);
                this.txtDBName_MeL.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database_MeL"].ToString().Trim(), true);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void Load_DuongDanDenFolderVersion()
        {
            try
            {
                string kiemtraApp = "SELECT * FROM tools_version WHERE app_type=0;";
                DataTable dataApp = condb.GetDataTable_MeL(kiemtraApp);
                if (dataApp != null || dataApp.Rows.Count > 0)
                {
                    txtUrlVersionServer.Text = dataApp.Rows[0]["app_link"].ToString();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion
        private void btnDBKiemTra_Click(object sender, EventArgs e)
        {
            try
            {
                //May chu HIS
                bool boolfound = false;
                string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                    txtDBHost.Text, txtDBPort.Text, txtDBUser.Text, txtDBPass.Text, txtDBName.Text);
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                string sql = "SELECT * FROM tbuser";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    boolfound = true;
                    MessageBox.Show("Kết nối đến cơ sở dữ liệu HIS thành công!", "Thông báo");
                }
                if (boolfound == false)
                {
                    MessageBox.Show("Lỗi kết nối đến cơ sở dữ liệu HIS!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dr.Close();
                conn.Close();
                //May chu HSBA
                bool boolfound_MeL = false;
                string connstring_MeL = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                    txtDBHost_MeL.Text, txtDBPort_MeL.Text, txtDBUser_MeL.Text, txtDBPass_MeL.Text, txtDBName_MeL.Text);
                NpgsqlConnection conn_MeL = new NpgsqlConnection(connstring_MeL);
                conn_MeL.Open();
                string sql_MeL = "SELECT * FROM tools_license";
                NpgsqlCommand command_MeL = new NpgsqlCommand(sql_MeL, conn_MeL);
                NpgsqlDataReader dr_MeL = command_MeL.ExecuteReader();
                if (dr_MeL.Read())
                {
                    boolfound_MeL = true;
                    MessageBox.Show("Kết nối đến cơ sở dữ liệu MedicalLink thành công!", "Thông báo");
                }
                if (boolfound_MeL == false)
                {
                    MessageBox.Show("Lỗi kết nối đến cơ sở dữ liệu MedicalLink!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dr_MeL.Close();
                conn_MeL.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void btnDBLuu_Click(object sender, EventArgs e)
        {
            try
            {
                LuuLaiCauHinhPhanMem_FileConfig();
                LuuLaiDuongDanFolderVersion();
                ConfigurationManager.RefreshSection("appSettings");
                MessageBox.Show("Lưu dữ liệu thành công", "Thông báo");
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void LuuLaiCauHinhPhanMem_FileConfig()
        {
            try
            {
                Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                _config.AppSettings.Settings["ServerHost"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBHost.Text.Trim(), true);
                _config.AppSettings.Settings["ServerPort"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBPort.Text.Trim(), true);
                _config.AppSettings.Settings["Username"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBUser.Text.Trim(), true);
                _config.AppSettings.Settings["Password"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBPass.Text.Trim(), true);
                _config.AppSettings.Settings["Database"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBName.Text.Trim(), true);
                _config.AppSettings.Settings["ServerHost_MeL"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBHost_MeL.Text.Trim(), true);
                _config.AppSettings.Settings["ServerPort_MeL"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBPort_MeL.Text.Trim(), true);
                _config.AppSettings.Settings["Username_MeL"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBUser_MeL.Text.Trim(), true);
                _config.AppSettings.Settings["Password_MeL"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBPass_MeL.Text.Trim(), true);
                _config.AppSettings.Settings["Database_MeL"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBName_MeL.Text.Trim(), true);
                _config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void LuuLaiDuongDanFolderVersion()
        {
            try
            {
                string sqlcommit = "update tools_version set app_link= '" + txtUrlVersionServer.Text.Trim() + "';";
                condb.ExecuteNonQuery_MeL(sqlcommit);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void btnDBUpdate_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                if (KetNoiSCDLProcess.CapNhatCoSoDuLieu())
                {
                    MessageBox.Show("Cập nhật cơ sở dữ liệu thành công", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Cập nhật cơ sở dữ liệu thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MedicalLink.Base.Logging.Error("Lỗi cập nhật cơ sở dữ liệu!" + ex.ToString());
            }
            SplashScreenManager.CloseForm();
        }


    }
}
