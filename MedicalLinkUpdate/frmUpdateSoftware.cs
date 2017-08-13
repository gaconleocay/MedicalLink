using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLinkUpdate
{
    public partial class frmUpdateSoftware : Form
    {
        private static ConnectDatabase condb = new ConnectDatabase();
        private string linkupdateServer { get; set; }



        public frmUpdateSoftware()
        {
            InitializeComponent();
        }

        #region Load
        private void frmUpdateSoftware_Load(object sender, EventArgs e)
        {
            try
            {
                txtKeyDangNhap.Focus();
                btnUpdateLink.Enabled = false;
                btnUpdateToServer.Enabled = false;
                KiemTraTonTaiVaInsert();
                LoadDuLieuMacDinhLenForm();
                //this.linkupdateServer = "C:\\PROJECT\\O2S_MedicalLink\\trunk\\MedicalLink\\bin\\Debug\\0-Update";
            }
            catch (Exception)
            {
            }
        }
        private void KiemTraTonTaiVaInsert()
        {
            try
            {
                string kiemtraApp = "SELECT * FROM tools_version WHERE app_type=0;";
                string kiemtraLauncher = "SELECT * FROM tools_version WHERE app_type=1;";
                DataTable dataApp = condb.GetDataTable_MeL(kiemtraApp);
                DataTable dataLauncher = condb.GetDataTable_MeL(kiemtraLauncher);
                if (dataApp == null || dataApp.Rows.Count != 1)
                {
                    string insertApp = "INSERT INTO tools_version(app_type) values('0') ;";
                    condb.ExecuteNonQuery_MeL(insertApp);
                }
                if (dataLauncher == null || dataLauncher.Rows.Count != 1)
                {
                    string insertApp = "INSERT INTO tools_version(app_type) values('1') ;";
                    condb.ExecuteNonQuery_MeL(insertApp);
                }
            }
            catch (Exception)
            {
            }
        }
        private void LoadDuLieuMacDinhLenForm()
        {
            try
            {
                string kiemtraApp = "SELECT * FROM tools_version WHERE app_type=0;";
                string kiemtraLauncher = "SELECT * FROM tools_version WHERE app_type=1;";
                DataTable dataApp = condb.GetDataTable_MeL(kiemtraApp);
                DataTable dataLauncher = condb.GetDataTable_MeL(kiemtraLauncher);
                if (dataApp != null || dataApp.Rows.Count > 0)
                {
                    this.linkupdateServer = dataApp.Rows[0]["app_link"].ToString();
                    txtUpdateLink.Text = this.linkupdateServer;
                    lblMedicalLinkVersion.Text = "MedicalLink v" + dataApp.Rows[0]["appversion"].ToString();
                }
                if (dataLauncher != null || dataLauncher.Rows.Count > 0)
                {
                    lblLauncherVersion.Text = "Launcher v" + dataLauncher.Rows[0]["appversion"].ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKeyDangNhap.Text.Trim() == KeyTrongPhanMem.Key_UpdatePhanMem)
                {
                    btnUpdateLink.Enabled = true;
                    btnUpdateToServer.Enabled = true;
                }
                else
                {
                    btnUpdateLink.Enabled = false;
                    btnUpdateToServer.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtKeyDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void btnUpdateLink_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlcommit = "update tools_version set app_link= '" + txtUpdateLink.Text.Trim() + "';";
                condb.ExecuteNonQuery_MeL(sqlcommit);
                MessageBox.Show("Update thư mục lưu trữ trên server thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = folderBrowserDialogSelect.SelectedPath;
                    txtUpdateLink.Text = folderBrowserDialogSelect.SelectedPath;
                    LoadDirectoryExplorer_Tree(folderBrowserDialogSelect.SelectedPath);
                    //LoadDirectoryExplorer_Tree("C:\\PROJECT\\O2S_MedicalLink\\trunk\\MedicalLink\\bin\\Debug\\0-Update");
                }
            }
            catch (Exception)
            {
            }
        }

        private void LoadDirectoryExplorer_Tree(string path)
        {
            try
            {
                treeListDirectoryExplorer.ClearNodes();
                TreeListNode parentForRootNodes = null;
                TreeListNode rootNode_0 = treeListDirectoryExplorer.AppendNode(new object[] { path, null, null, null, null, path, 0 }, parentForRootNodes, null);
                CreateChildNod(rootNode_0, path);
                treeListDirectoryExplorer.ExpandAll();
            }
            catch (Exception)
            {
            }
        }
        private void CreateChildNod(TreeListNode rootNode, string path)
        {
            try
            {
                DirectoryInfo diRoot = new DirectoryInfo(path + "");
                FileInfo[] theFiles = diRoot.GetFiles();
                foreach (FileInfo file in theFiles)
                {
                    string _filesize = string.Empty;
                    if (file.Length >= 1024)
                        _filesize = string.Format("{0:### ### ###} KB", file.Length / 1024);
                    else _filesize = string.Format("{0} Bytes", file.Length);

                    string[] type = file.Name.Split('.');
                    string _filetype = type[type.Length - 1];

                    string _fileversion = "";
                    if (type[type.Length - 1] == "exe")
                    {
                        FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(file.FullName);
                        _fileversion = myFileVersionInfo.FileVersion.ToString();
                    }

                    TreeListNode rootNode_0 = treeListDirectoryExplorer.AppendNode(new object[] { file.Name, _filetype, _filesize, file.LastWriteTime, _fileversion, file.FullName, 1 }, rootNode, null);
                }

                DirectoryInfo[] dirs = diRoot.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    TreeListNode rootNode_0 = treeListDirectoryExplorer.AppendNode(new object[] { dir.Name, null, null, null, null, dir.FullName, 0 }, rootNode, null);
                    CreateChildNod(rootNode_0, dir.FullName);
                }
            }
            catch (Exception)
            {
            }
        }

        private void treeListDirectoryExplorer_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            try
            {
                if (e.Node == (sender as TreeList).FocusedNode)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception)
            {
            }
        }

        //Check All tree
        private void treeListDirectoryExplorer_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            try
            {
                TreeListNode node = e.Node;
                if (node.Checked)
                {
                    node.UncheckAll();
                }
                else
                {
                    node.CheckAll();
                }
                // e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);

                while (node.ParentNode != null)
                {
                    node = node.ParentNode;
                    bool oneOfChildIsChecked = OneOfChildsIsChecked(node);
                    if (oneOfChildIsChecked)
                    {
                        node.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        node.CheckState = CheckState.Unchecked;
                    }
                }

            }
            catch (Exception)
            {
            }
        }
        private bool OneOfChildsIsChecked(TreeListNode node)
        {
            bool result = false;
            foreach (TreeListNode item in node.Nodes)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    result = true;
                }
            }
            return result;
        }

        private void btnUpdateToServer_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TreeListNode n in treeListDirectoryExplorer.GetAllCheckedNodes())
                {
                    if (n.GetValue("nodetype").ToString() == "1")
                    {
                        CopyFolder_CheckSum(n.GetValue("fullpath").ToString(), this.linkupdateServer);
                    }
                }
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
            }
        }

        private void CopyFolder_CheckSum(string SourceFolder, string DestFolder)
        {
            string folder_source = SourceFolder.Replace(Path.GetFileName(SourceFolder),"");
            string folder_dest = folder_source.Replace(txtFilePath.Text, this.linkupdateServer);
            Directory.CreateDirectory(folder_dest);

            string file_dest = SourceFolder.Replace(txtFilePath.Text, this.linkupdateServer);
            //Check file
            if (Util_FileCheckSum.GetMD5HashFromFile(SourceFolder) != Util_FileCheckSum.GetMD5HashFromFile(file_dest))
            {
                File.Copy(SourceFolder, file_dest, true);
            }
        }

    }
}
