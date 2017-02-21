using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MedicalLinkUpdate
{
    public partial class FormUpdate : Form
    {
        public FormUpdate()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    treeViewSelect.Nodes.Clear();
                    textFilePath.Text = folderBrowserDialogSelect.SelectedPath;

                    TreeNode tn = new TreeNode(folderBrowserDialogSelect.SelectedPath);
                    treeViewSelect.Nodes.Add(tn);
                    tn.Nodes.Add(new TreeNode("*"));

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void treeViewSelect_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                FormUpdate.ActiveForm.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                if (e.Node.Nodes.Count != 0)
                    e.Node.Nodes.RemoveAt(0);
                String[] dirs;
                try
                {

                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    Array.Sort(dirs);
                }
                catch (Exception pe)
                {
                    MessageBox.Show(pe.Message, "Error!");
                    goto err;
                }
                for (int i = 0; i < dirs.Length; i++)
                {
                    String dirName = dirs[i];
                    TreeNode tn = new TreeNode(Path.GetFileName(dirName));
                    e.Node.Nodes.Add(tn);
                    String[] subdirs;
                    try
                    {
                        subdirs = Directory.GetDirectories(dirs[i]);
                        if (subdirs.Length > 0)
                            tn.Nodes.Add("temp");

                        for (int j = 0; j < subdirs.Length; j++)
                        {
                            String dirFile = subdirs[j];
                            TreeNode file = new TreeNode(Path.GetFileName(dirFile));
                            e.Node.Nodes.Add(file);
                            String[] subFile;
                            try
                            {
                                subFile = Directory.GetDirectories(dirs[i]);
                                if (subFile.Length > 0)
                                    file.Nodes.Add("temp");
                            }
                            catch
                            { goto err; }

                        }
                    }
                    catch
                    { goto err; }

                }
            err:
                FormUpdate.ActiveForm.Cursor = System.Windows.Forms.Cursors.Default;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void treeViewSelect_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                FormUpdate.ActiveForm.Text = e.Node.FullPath;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
