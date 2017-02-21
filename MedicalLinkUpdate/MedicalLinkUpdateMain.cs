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


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogShelect.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialogShelect.FileName;

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
                string sqlcommit = "insert into tools_version(appversion, updateapp)  values ('" + txtVersion.Text.Trim() + "', (SELECT bytea_import('" + txtFilePath.Text.Trim() + "')));";
                string deleteversionold = "delete from tools_version where appversion <>'" + txtVersion.Text.Trim() + "';";

                condb.ExecuteNonQuery(sqlcommit);
                condb.ExecuteNonQuery(deleteversionold);
                MessageBox.Show("Commit thành công.", "Thông báo");
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void Update_Load(object sender, EventArgs e)
        {
            try
            {
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                this.Text = "Update version phần mềm (v" + version + ")";
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
