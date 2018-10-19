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
        #region Khai bao
        private string serverhost = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString(), true);
        private string serverdb = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString(), true);
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        internal string lblHienThiThongTinChucNang { get; set; }
        private DialogResult HoiKhoiDongLai;

        #endregion
        public frmMain()
        {
            InitializeComponent();
        }

        #region Load
        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                timerClock.Start();
                timerKiemTraLicense.Interval = MedicalLink.Base.KeyTrongPhanMem.ThoiGianKiemTraLicense;
                timerKiemTraLicense.Start();

                LoadPageMenu();
                KiemTraLicense_EnableButton(); //kiem tra license truoc khi kiem tra phan quyen
                KiemTraPhanQuyenNguoiDung();
                LoadThongTinVePhanMem_Version();
                LoadGiaoDien();

                TimerChayChuongTrinhServiceAn(); // Chay du lieu ngam   - TAM THOI KHONG SU DUNG
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadPageMenu()
        {
            try
            {
                //tabMenuTrangChu.Controls.Clear();
                //MedicalLink.FormCommon.ucTrangChu ucTrangChu = new FormCommon.ucTrangChu();
                //ucTrangChu.MyGetData = new FormCommon.ucTrangChu.GetString(HienThiTenChucNang);
                //ucTrangChu.Dock = System.Windows.Forms.DockStyle.Fill;
                //tabMenuTrangChu.Controls.Add(ucTrangChu);

                //tabMenuChucNang.Controls.Clear();
                //MedicalLink.FormCommon.ucChucNang ucChucNang = new FormCommon.ucChucNang();
                //ucChucNang.MyGetData = new FormCommon.ucChucNang.GetString(HienThiTenChucNang);
                //ucChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
                //tabMenuChucNang.Controls.Add(ucChucNang);

                //trang chu
                tabMenuTrangChu.Controls.Clear();
                MedicalLink.FormCommon.ucTrangChu _ucTrangChu = new FormCommon.ucTrangChu();
                _ucTrangChu.MyGetData = new FormCommon.ucTrangChu.GetString(HienThiTenChucNang);
                _ucTrangChu.Dock = System.Windows.Forms.DockStyle.Fill;
                tabMenuTrangChu.Controls.Add(_ucTrangChu);
                //chuc nang
                tabMenuChucNang.Controls.Clear();
                MedicalLink.FormCommon.ucChucNang _ucChucNang = new FormCommon.ucChucNang();
                _ucChucNang.MyGetData = new FormCommon.ucChucNang.GetString(HienThiTenChucNang);
                _ucChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
                tabMenuChucNang.Controls.Add(_ucChucNang);
                //bao cao
                tabMenuBaoCao.Controls.Clear();
                MedicalLink.FormCommon.ucBaoCao _ucBaoCao = new FormCommon.ucBaoCao();
                _ucBaoCao.MyGetData = new FormCommon.ucBaoCao.GetString(HienThiTenChucNang);
                _ucBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
                tabMenuBaoCao.Controls.Add(_ucBaoCao);
                //ql tai chinh
                tabMenuQLTaiChinh.Controls.Clear();
                MedicalLink.FormCommon.ucQLTaiChinh _ucQLTaiChinh = new FormCommon.ucQLTaiChinh();
                _ucQLTaiChinh.MyGetData = new FormCommon.ucQLTaiChinh.GetString(HienThiTenChucNang);
                _ucQLTaiChinh.Dock = System.Windows.Forms.DockStyle.Fill;
                tabMenuQLTaiChinh.Controls.Add(_ucQLTaiChinh);
                //dashboard
                tabMenuDashboard.Controls.Clear();
                MedicalLink.FormCommon.ucDashboard _ucDashboard = new FormCommon.ucDashboard();
                _ucDashboard.MyGetData = new FormCommon.ucDashboard.GetString(HienThiTenChucNang);
                _ucDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
                tabMenuDashboard.Controls.Add(_ucDashboard);

                EnableAndDisableChucNang(false);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void LoadThongTinVePhanMem_Version()
        {
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                this.Text = "Phần mềm quản lý tổng thể bệnh viện (v" + version + ")";
                StatusUsername.Caption = SessionLogin.SessionUsername;
                StatusDBName.Caption = serverhost;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

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
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Events
        private void KiemTraLicense_EnableButton()
        {
            try
            {
                //MedicalLink.Base.Logging.Warn("Kiểm tra phân quyền");
                //Kiểm tra phân quyền
                if (SessionLogin.SessionUsercode == MedicalLink.Base.KeyTrongPhanMem.AdminUser_key)
                {
                    EnableAndDisableChucNang(true); //admin              
                }
                else
                {
                    if (SessionLogin.KiemTraLicenseSuDung)
                    {
                        EnableAndDisableChucNang(true);
                    }
                    else
                    {
                        EnableAndDisableChucNang(false);
                        DialogResult dialogResult = MessageBox.Show("Phần mềm hết bản quyền! \nVui lòng liên hệ với tác giả để được trợ giúp.\nAuthor: Hồng Minh Nhất \nE-mail: hongminhnhat15@gmail.com \nPhone: 0868-915-456", "Thông báo !!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.HoiKhoiDongLai != DialogResult.Retry)
                {
                    this.HoiKhoiDongLai = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (this.HoiKhoiDongLai == DialogResult.No)
                        e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void tabPaneMenu_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            try
            {
                //if (tabPaneMenu.SelectedPage == tabMenuRestart)
                //{
                //    hoi = DialogResult.Retry;
                //    Application.Restart();
                //    Application.ExitThread();
                //    System.Diagnostics.Process.Start(@"MedicalLinkLauncher.exe");
                //    Application.Exit();
                //}
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnResert_Click(object sender, EventArgs e)
        {
            try
            {
               HoiKhoiDongLai = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình ?", "Thông báo!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (HoiKhoiDongLai == DialogResult.Yes)
                {
                    HoiKhoiDongLai = DialogResult.Retry;
                    //Application.Restart();
                    Application.ExitThread();
                    System.Diagnostics.Process.Start(@"MedicalLinkLauncher.exe");
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        //delegate
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

        #endregion

        #region Process
        private void timerClock_Tick(object sender, EventArgs e)
        {
            StatusClock.Caption = DateTime.Now.ToString("dddd, HH:mm:ss dd/MM/yyyy");
        }

        private void timerKiemTraLicense_Tick(object sender, EventArgs e)
        {
            try
            {
                if (SessionLogin.SessionUsercode != MedicalLink.Base.KeyTrongPhanMem.AdminUser_key)
                {
                    KiemTraLicense.KiemTraLicenseHopLe();
                    if (SessionLogin.KiemTraLicenseSuDung == false)
                    {
                        timerKiemTraLicense.Stop();
                        DialogResult dialogResult = MessageBox.Show("Phần mềm hết bản quyền! \nVui lòng liên hệ với tác giả để được trợ giúp.\nAuthor: Hồng Minh Nhất \nE-mail: hongminhnhat15@gmail.com \nPhone: 0868-915-456", "Thông báo !!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.OK)
                        {
                            this.Visible = false;
                            this.Dispose();
                            frmLogin frm = new frmLogin();
                            frm.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion
    }
}
