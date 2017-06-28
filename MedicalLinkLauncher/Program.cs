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
                if (CheckVersionUpdate()) //update
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật lên phiên bản mới? \n Hãy tắt phần mềm đang chạy trước khi cập nhật.", "Thông báo có phiên bản mới.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //đường dẫn tới thư mục tạm trên máy Local
                        //string localPath = Environment.CurrentDirectory + "\\" + TEMP_DIR + "\\" + UPDATE_FILE_NAME;
                        //tạo thư mục tạm nếu chưa có (để tránh bị lỗi khi download)
                        // CreateTempDirectory();

                        //PhanQuyenFolder(Environment.CurrentDirectory + "\\" + TEMP_DIR);
                        //tải tệp tin MedicalLinkUpdate.zip từ Database về thư mục tạm
                        //try
                        //{
                        //    DownloadFile(localPath);
                        //    GiaiNenVaCopyFile();
                        //}
                        //catch (Exception)
                        //{
                        //}
                        try
                        {
                            CopyFiles();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                //sau khi copy đè tất cả các file xong, ta sẽ tiến hành gọi lại chương trình chính (MedicalLink.exe) để chạy lại chương trình
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
                DataView dataVer = new DataView(condb.GetDataTable_MeL("SELECT appversion from tools_version where app_type=0 LIMIT 1;"));
                if (dataVer != null && dataVer.Count > 0)
                {
                    versionDatabase = dataVer[0]["appversion"].ToString();
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

        //private static void GiaiNenVaCopyFile()
        //{
        //    try
        //    {
        //        //xác định thư mục hiện thời, nơi chương trình đang chạy
        //        string currentDirectory = Environment.CurrentDirectory;
        //        //xác định thư mục tạm, nơi Program1.exe đã tải các file cần cập nhật về
        //        string tempDirectory = Environment.CurrentDirectory + "\\" + TEMP_DIR;
        //        //lấy danh sách tất cả các file trong thư mục tạm
        //        string[] fileList = Directory.GetFiles(tempDirectory);
        //        //duyệt từng file và copy đè lên file cũ trong thư mục đang chạy chương trình
        //        foreach (string sourceFile in fileList)
        //        {
        //            //var zipFileName = @"C:\Users\Mac\Desktop\GiaiNen\gianhandan.zip";
        //            //var targetDir = @"C:\Users\Mac\Desktop\GiaiNen";
        //            FastZip fastZip = new FastZip();
        //            string fileFilter = null;

        //            // Will always overwrite if target filenames already exist
        //            fastZip.ExtractZip(sourceFile, currentDirectory, fileFilter);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        private static void CopyFiles()
        {
            try
            {
                // Lay duong dan cau hinh luu luu file Share
                DataView dataurlfile = new DataView(condb.GetDataTable_MeL("select app_link from tools_version where app_type=0 limit 1; "));
                if (dataurlfile != null && dataurlfile.Count > 0)
                {
                    string tempDirectory = dataurlfile[0]["app_link"].ToString();
                    CopyFolder(tempDirectory, Environment.CurrentDirectory);
                }
            }
            catch (Exception)
            {
            }
        }
        private static void CopyFolder(string SourceFolder, string DestFolder)
        {
            //if (Directory.Exists(DestFolder)) // folder ton tai thi moi thuc hien copy
            //{
            Directory.CreateDirectory(DestFolder); //Tao folder moi
            string[] files = Directory.GetFiles(SourceFolder);
            //Neu co file thy phai copy file
            foreach (string file in files)
            {
                try
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(DestFolder, name);
                    File.Copy(file, dest, true);
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
                CopyFolder(folder, dest);
            }
            //}
            //else //chua co thi tao folder moi va copy
            //{
            //    Directory.CreateDirectory(DestFolder); //Tao folder moi
            //    CopyFolder(SourceFolder,DestFolder);
            //}
        }


        //private static void CreateTempDirectory()
        //{
        //    try
        //    {
        //        //kiểm tra đường dấn tới thư mục tạm, nếu chưa có thì tạo thư mục; nếu có thì xóa đi và tạo lại
        //        string tempPath = Environment.CurrentDirectory + "\\" + TEMP_DIR;
        //        if (!System.IO.Directory.Exists(tempPath))
        //        {
        //            System.IO.Directory.CreateDirectory(tempPath);
        //        }
        //        else
        //        {
        //            System.IO.Directory.Delete(tempPath, true);
        //            System.IO.Directory.CreateDirectory(tempPath);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        //private static void DownloadFile(string localPath)
        //{
        //    try
        //    {
        //        string sqlgetfile = "COPY (SELECT updateapp FROM tools_version WHERE appversion='" + versionDatabase + "') TO '" + localPath + "' (FORMAT binary) ;";
        //        condb.ExecuteNonQuery(sqlgetfile);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


        #region Util
        /*
        private static void PhanQuyenFolder(string DirectoryName)
        {
            try
            {
                SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

                // Add the access control entry to the directory.
                AddDirectorySecurity(DirectoryName, "Everyone", FileSystemRights.FullControl, AccessControlType.Allow);
                AddDirectorySecurity(DirectoryName, "Everyone", FileSystemRights.ExecuteFile, AccessControlType.Allow);
                AddDirectorySecurity(DirectoryName, "Everyone", FileSystemRights.Write, AccessControlType.Allow);
                AddDirectorySecurity(DirectoryName, "Everyone", FileSystemRights.WriteAttributes, AccessControlType.Allow);
                AddDirectorySecurity(DirectoryName, "Everyone", FileSystemRights.WriteData, AccessControlType.Allow);
                AddDirectorySecurity(DirectoryName, "Everyone", FileSystemRights.WriteExtendedAttributes, AccessControlType.Allow);
                AddDirectorySecurity(DirectoryName, "Everyone", FileSystemRights.CreateFiles, AccessControlType.Allow);
                AddDirectorySecurity(DirectoryName, "Everyone", FileSystemRights.Modify, AccessControlType.Allow);
                AddDirectorySecurity(DirectoryName, "Everyone", FileSystemRights.ChangePermissions, AccessControlType.Allow);

                // Remove the access control entry from the directory.
                //RemoveDirectorySecurity(DirectoryName, @"MYDOMAIN\MyAccount", FileSystemRights.ReadData, AccessControlType.Allow);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // Adds an ACL entry on the specified directory for the specified account.
        public static void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }
        */
        #endregion





    }
}
