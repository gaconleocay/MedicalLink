using MedicalLink.Base;
using MedicalLink.FormCommon.DangKyBanQuyen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MedicalLink.FormCommon
{
    public partial class frmMain : Form
    {
        public string serverhost = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString(), true);
        public string serverdb = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString(), true);
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();

        internal string lblHienThiThongTinChucNang { get; set; }

        DialogResult hoi;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Thread.Sleep(4000);
            try
            {
                timerClock.Start();
                //timerKiemTraLicense.Interval = MedicalLink.Base.KeyTrongPhanMem.ThoiGianKiemTraLicense;
                //timerKiemTraLicense.Start();
                KiemTraPhanQuyenNguoiDung();
                LoadThongTinVePhanMem_Version();
                LoadGiaoDien();
                //  KiemTraPhanQuyen_EnableButton();
                LoadPageMenu();

                TimerChayChuongTrinhServiceAn(); // Chay du lieu ngam   - TAM THOI KHONG SU DUNG
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        // sự kiện hỏi khi ấn nút X để đóng chương trình
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (hoi != DialogResult.Retry)
                {
                    hoi = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (hoi == DialogResult.No)
                        e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        // Thoát chương trình 
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // Hiển thị thời gian ngày tháng
        private void timerClock_Tick(object sender, EventArgs e)
        {
            StatusClock.Caption = DateTime.Now.ToString("dddd, HH:mm:ss dd/MM/yyyy");
        }

        private void timerKiemTraLicense_Tick(object sender, EventArgs e)
        {
            try
            {
                if (SessionLogin.SessionUsercode == MedicalLink.Base.KeyTrongPhanMem.AdminUser_key)
                {
                    //EnableAndDisableChucNang(true); //admin              
                }
                else
                {
                    kiemTraLicenseHopLe.KiemTraLicenseHopLe();
                    if (SessionLogin.KiemTraLicenseSuDung == false)
                    {
                        DialogResult dialogResult = MessageBox.Show("Phần mềm hết bản quyền! \nVui lòng liên hệ với tác giả để được trợ giúp.\nAuthor: Hồng Minh Nhất \nE-mail: hongminhnhat15@gmail.com \nPhone: 0868-915-456", "Thông báo !!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.OK)
                        {
                            this.Visible = false;
                            this.Dispose();
                            frmLogin frm = new frmLogin();
                            frm.Show();
                        }
                        timerKiemTraLicense.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void KiemTraPhanQuyen_EnableButton()
        {
            try
            {
                //Kiểm tra phân quyền
                //if (SessionLogin.SessionUsercode == MedicalLink.Base.KeyTrongPhanMem.AdminUser_key)
                //{
                //    EnableAndDisableChucNang(true); //admin              
                //}
                //else
                //{
                //    if (SessionLogin.KiemTraLicenseSuDung)
                //    {
                //        KiemTraPhanQuyenNguoiDung();
                //    }
                //    else
                //    {
                //        EnableAndDisableChucNang(false);
                //        DialogResult dialogResult = MessageBox.Show("Phần mềm hết bản quyền! \nVui lòng liên hệ với tác giả để được trợ giúp.\nAuthor: Hồng Minh Nhất \nE-mail: hongminhnhat15@gmail.com \nPhone: 0868-915-456", "Thông báo !!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    }
                //}
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadThongTinVePhanMem_Version()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = "Công cụ sửa trong DB Alibobo HIS (v" + version + ")";
            StatusUsername.Caption = SessionLogin.SessionUsername;
            StatusDBName.Caption = serverhost + " [ " + serverdb + " ]";
        }

        #region Giao dien
        private void LoadGiaoDien()
        {
            try
            {
                foreach (DevExpress.Skins.SkinContainer skin in DevExpress.Skins.SkinManager.Default.Skins)
                {
                    DevExpress.XtraBars.BarButtonItem item = new DevExpress.XtraBars.BarButtonItem();
                    item.Caption = skin.SkinName;
                    item.Name = "button" + skin.SkinName;
                    item.ItemClick += item_ItemClick;
                    // barSubItemSkin.AddItem(item);
                }
                if (ConfigurationManager.AppSettings["skin"].ToString() != "")
                {
                    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(ConfigurationManager.AppSettings["skin"].ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void item_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DevExpress.XtraBars.BarItem item = e.Item;
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(item.Caption);
                Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                _config.AppSettings.Settings["skin"].Value = item.Caption;
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception)
            {
            }
        }
        #endregion

        private void tabPaneMenu_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            try
            {
                if (tabPaneMenu.SelectedPage == tabMenuRestart)
                {
                    //frmLogin frm = new frmLogin();
                    //this.Visible = false;
                    //this.Dispose();
                    //frm.Show();
                    hoi = DialogResult.Retry;
                    Application.Restart();
                    Application.ExitThread();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
                throw;
            }
        }

        //TAB MENU
        private void LoadPageMenu()
        {
            try
            {
                //StatusTenBC.Caption = "";
                tabMenuTrangChu.Controls.Clear();
                MedicalLink.FormCommon.ucTrangChu ucTrangChu = new FormCommon.ucTrangChu();
                ucTrangChu.MyGetData = new FormCommon.ucTrangChu.GetString(HienThiTenChucNang);
                ucTrangChu.Dock = System.Windows.Forms.DockStyle.Fill;
                tabMenuTrangChu.Controls.Add(ucTrangChu);

                tabMenuChucNang.Controls.Clear();
                MedicalLink.FormCommon.ucChucNang ucChucNang = new FormCommon.ucChucNang();
                ucChucNang.MyGetData = new FormCommon.ucChucNang.GetString(HienThiTenChucNang);
                ucChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
                tabMenuChucNang.Controls.Add(ucChucNang);

                tabMenuDashboard.Controls.Clear();
                MedicalLink.FormCommon.ucDashboard ucDashboard = new FormCommon.ucDashboard();
                ucDashboard.MyGetData = new FormCommon.ucDashboard.GetString(HienThiTenChucNang);
                ucDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
                tabMenuDashboard.Controls.Add(ucDashboard);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        internal void HienThiTenChucNang(string _message)
        {
            try
            {
                lblHienThiThongTinChucNang = _message;
                lblStatusTenBC.Caption = lblHienThiThongTinChucNang;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }






    }
}
