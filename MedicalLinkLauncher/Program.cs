using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
namespace MedicalLinkLauncher
{
    static class Program
    {
        private const string TEMP_DIR = "MedicalLinkUpdate_Temp";
        private const string UPDATE_FILE_NAME = "MedicalLinkUpdate.zip";
        private static ConnectDatabase condb = new ConnectDatabase();
        private static string versionDatabase = "";
        private static string tempDirectory = "";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (CheckVersionUpdate()) //update - co ban cap nhat moi
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật lên phiên bản mới? \nHãy tắt phần mềm đang chạy trước khi cập nhật.", "Thông báo có phiên bản mới.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Yes)
                    {
                        KillProcess_MedicalLink();
                        CopyFolder_CheckSum(tempDirectory, Environment.CurrentDirectory);
                    }
                }
                else //khong co ban cap nhat moi thi cung checksum file de tu dong cap nhat
                {
                    CopyFolder_CheckSum(tempDirectory, Environment.CurrentDirectory);
                }

                System.Diagnostics.Process.Start(@"MedicalLink.exe");
                Application.Exit();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Process.Start(@"MedicalLink.exe");
                Application.Exit();
            }
        }

        private static bool CheckVersionUpdate()
        {
            bool result = false;
            try
            {
                // string sqlgetVersion = "SELECT appversion from tools_version LIMIT 1;";
                DataView dataVer = new DataView(condb.GetDataTable_MeL("SELECT appversion,app_link from tools_version where app_type=0 LIMIT 1;"));
                if (dataVer != null && dataVer.Count > 0)
                {
                    versionDatabase = dataVer[0]["appversion"].ToString();
                    tempDirectory = dataVer[0]["app_link"].ToString();
                }
                //lấy thông tin version của phần mềm MedicalLink.exe
                FileVersionInfo.GetVersionInfo(Path.Combine(Environment.CurrentDirectory, "MedicalLink.exe"));
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Environment.CurrentDirectory + "\\MedicalLink.exe");
                if (myFileVersionInfo.FileVersion.ToString() != versionDatabase)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = true;
            }
            return result;
        }
        private static void KillProcess_MedicalLink()
        {
            try
            {
                Process[] pProcess = System.Diagnostics.Process.GetProcessesByName("MedicalLink");
                foreach (Process p in pProcess)
                {
                    p.Kill();
                }
            }
            catch (Exception)
            {
                throw;
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

                    //Check file
                    if (Ultil_FileCheckSum.GetMD5HashFromFile(file) != Ultil_FileCheckSum.GetMD5HashFromFile(dest))
                    {
                        File.Copy(file, dest, true);
                    }
                }
                catch (Exception ex)
                {
                    continue;
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



    }
}
