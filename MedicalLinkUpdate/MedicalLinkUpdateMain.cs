using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MedicalLinkUpdate
{
    public partial class Update : Form
    {
        private static ConnectDatabase condb = new ConnectDatabase();
        public Update()
        {
            InitializeComponent();
        }
        #region Load
        private void Update_Load(object sender, EventArgs e)
        {
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                this.Text = "Update version phần mềm (v" + version + ")";
                KiemTraTonTaiVaInsert();
                LoadDuLieuMacDinhLenForm();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void KiemTraTonTaiVaInsert()
        {
            try
            {
                string kiemtraApp = "SELECT * FROM tools_version WHERE app_type=0;";
                string kiemtraLauncher = "SELECT * FROM tools_version WHERE app_type=1;";
                DataTable dataApp = condb.getDataTable(kiemtraApp);
                DataTable dataLauncher = condb.getDataTable(kiemtraLauncher);
                if (dataApp == null || dataApp.Rows.Count != 1)
                {
                    string insertApp = "INSERT INTO tools_version(app_type) values('0') ;";
                    condb.ExecuteNonQuery(insertApp);
                }
                if (dataLauncher == null || dataLauncher.Rows.Count != 1)
                {
                    string insertApp = "INSERT INTO tools_version(app_type) values('1') ;";
                    condb.ExecuteNonQuery(insertApp);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LoadDuLieuMacDinhLenForm()
        {
            try
            {
                string kiemtraApp = "SELECT * FROM tools_version WHERE app_type=0;";
                string kiemtraLauncher = "SELECT * FROM tools_version WHERE app_type=1;";
                DataTable dataApp = condb.getDataTable(kiemtraApp);
                DataTable dataLauncher = condb.getDataTable(kiemtraLauncher);
                if (dataApp != null || dataApp.Rows.Count >0)
                {
                    txtUpdateLink.Text = dataApp.Rows[0]["app_link"].ToString();
                    txtVersionMecicalLink.Text = dataApp.Rows[0]["appversion"].ToString();
                }
                if (dataLauncher != null || dataLauncher.Rows.Count > 0)
                {
                    txtVersionLauncher.Text = dataLauncher.Rows[0]["appversion"].ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogShelect.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialogShelect.FileName;
                    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(openFileDialogShelect.FileName);
                    txtVersionMecicalLink.Text = myFileVersionInfo.FileVersion.ToString();
                    txtUpdateLink.Text = openFileDialogShelect.FileName;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlcommit = "insert into tools_version(appversion, updateapp)  values ('" + txtVersionMecicalLink.Text.Trim() + "', (SELECT bytea_import('" + txtFilePath.Text.Trim() + "')));";
                string deleteversionold = "delete from tools_version where appversion <>'" + txtVersionMecicalLink.Text.Trim() + "';";

                condb.ExecuteNonQuery(sqlcommit);
                condb.ExecuteNonQuery(deleteversionold);
                MessageBox.Show("Commit thành công.", "Thông báo");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnUpdateLink_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlcommit = "update tools_version set app_link= '" + txtUpdateLink.Text.Trim() + "';";
                condb.ExecuteNonQuery(sqlcommit);
                MessageBox.Show("Update Link thành công.", "Thông báo");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnUpdateMedicalLink_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlcommit = "update tools_version set appversion='" + txtVersionMecicalLink.Text.Trim() + "' where app_type=0;";
                condb.ExecuteNonQuery(sqlcommit);
                MessageBox.Show("Update Version MedicalLink thành công.", "Thông báo");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnUpdateLauncher_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlcommit = "update tools_version set appversion='" + txtVersionLauncher.Text.Trim() + "' where app_type=1;";
                condb.ExecuteNonQuery(sqlcommit);
                MessageBox.Show("Update Version Launcher thành công.", "Thông báo");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtKeyDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKeyDangNhap.Text.Trim() == KeyTrongPhanMem.AdminPass_key)
                {
                    btnUpdateLink.Enabled = true;
                    btnUpdateMedicalLink.Enabled = true;
                    btnUpdateLauncher.Enabled = true;
                }
                else
                {
                    btnUpdateLink.Enabled = false;
                    btnUpdateMedicalLink.Enabled = false;
                    btnUpdateLauncher.Enabled = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
