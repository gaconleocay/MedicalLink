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
using DevExpress.XtraEditors;
using System.Globalization;
using MedicalLink.Base;
using Npgsql;
using DevExpress.XtraTab;
using System.Diagnostics;
using DevExpress.XtraSplashScreen;

namespace MedicalLink.FormCommon
{
    public partial class ucTrangChu : UserControl
    {
        #region Declaration
        public string CurrentTabPage { get; set; }
        public int SelectedTabPageIndex { get; set; }
        internal frmMain frmMain;
        Base.ConnectDatabase condb = new Base.ConnectDatabase();
        private string MaDatabase = String.Empty;

        public string serverhost = ConfigurationManager.AppSettings["ServerHost"].ToString();
        public string serveruser = ConfigurationManager.AppSettings["Username"].ToString();
        public string serverpass = ConfigurationManager.AppSettings["Password"].ToString();
        public string serverdb = ConfigurationManager.AppSettings["Database"].ToString();

        // khai báo 1 hàm delegate
        public delegate void GetString(string thoigian);
        // khai báo 1 kiểu hàm delegate
        public GetString MyGetData;
        #endregion

        public ucTrangChu()
        {
            InitializeComponent();
        }

        #region Load
        private void ucTrangChu_Load(object sender, EventArgs e)
        {
            try
            {
                LoadGiaoDienDevexpress();
                EnablePhanQuyenVaLicense();
                LoadThongTinCoBan();
                LoadVersion();
                LoadLogoThongTin();
                LoadThongTinVeCSYT();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void LoadGiaoDienDevexpress()
        {
            try
            {
                // Handle the SelectedIndexChanged event to respond to selecting the skin name.
                cboGiaoDien.SelectedIndexChanged += new EventHandler(comboBoxEdit1_SelectedIndexChanged);
                // Add available skin names to the combo box.
                foreach (DevExpress.Skins.SkinContainer cnt in DevExpress.Skins.SkinManager.Default.Skins)
                {
                    cboGiaoDien.Properties.Items.Add(cnt.SkinName);
                }
                cboGiaoDien.Text = ConfigurationManager.AppSettings["skin"].ToString();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void EnablePhanQuyenVaLicense()
        {
            try
            {
                //Kiểm tra phân quyền
                if (SessionLogin.SessionUsercode != Base.KeyTrongPhanMem.AdminUser_key)
                {
                    if (SessionLogin.KiemTraLicenseSuDung)
                    {
                        if (Base.CheckPermission.ChkPerModule("SYS_05"))
                        {
                            xtraTabCaiDat.PageVisible = true;
                            navBarItemConnectDB.Visible = Base.CheckPermission.ChkPerModule("SYS_01");
                            navBarItemListNguoiDung.Visible = Base.CheckPermission.ChkPerModule("SYS_02");
                            navBarItemListNhanVien.Visible = Base.CheckPermission.ChkPerModule("SYS_03");
                            navBarItemListOption.Visible = Base.CheckPermission.ChkPerModule("SYS_04");
                            navBarItemDMDungChung.Visible = Base.CheckPermission.ChkPerModule("SYS_06");
                            navBarItemDMBenhVien.Visible = Base.CheckPermission.ChkPerModule("SYS_07");
                            navBarItemDMDichVu.Visible = Base.CheckPermission.ChkPerModule("SYS_08");
                            navBarItemHaoPhiMayXN.Visible = Base.CheckPermission.ChkPerModule("SYS_09");

                        }
                        else
                        {
                            xtraTabCaiDat.PageVisible = false;
                        }
                    }
                    else
                    {
                        xtraTabCaiDat.PageVisible = true;
                        navBarItemConnectDB.Visible = false;
                        navBarItemListNguoiDung.Visible = false;
                        navBarItemListNhanVien.Visible = false;
                        navBarItemListOption.Visible = false;
                        navBarItemDMDungChung.Visible = false;
                        navBarItemDMBenhVien.Visible = false;
                        navBarItemDMDichVu.Visible = false;
                        navBarItemHaoPhiMayXN.Visible = false;

                    }
                    navBarItemMaHoaGiaiMa.Visible = false;//luon luon false
                    //navBarItemNhatKySuKien.Visible = false;
                    navBarItemQLMayTram.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void LoadThongTinCoBan()
        {
            try
            {
                HienThiThongTinVeLicense();
                //Thong tin ve Database
                linkLabelTenDatabase.Text = Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString().Trim(), true) + " [" + Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString().Trim(), true) + "]";

                //THong tin ve tai khoan dang nhap
                if (Base.SessionLogin.SessionUsername == "" || Base.SessionLogin.SessionUsername == null)
                {
                    linkLabelTenNguoiDung.Text = ".........";
                }
                else
                {
                    linkLabelTenNguoiDung.Text = Base.SessionLogin.SessionUsername;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void LoadVersion()
        {
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                lblVersion.Text = fvi.FileVersion;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void HienThiThongTinVeLicense()
        {
            try
            {
                string MaDatabase = MedicalLink.FormCommon.DangKyBanQuyen.KiemTraLicense.LayThongTinMaDatabase();
                //Load License tu DB ra
                string kiemtra_licensetag = "SELECT datakey, licensekey FROM tools_license WHERE datakey='" + MaDatabase + "' limit 1;";
                DataView dataLicense = new DataView(condb.GetDataTable_MeL(kiemtra_licensetag));
                if (dataLicense != null && dataLicense.Count > 0)
                {
                    linkLabelThoiHan.Text = FormCommon.DangKyBanQuyen.KiemTraLicense.KiemTraThoiHanLicense(dataLicense[0]["licensekey"].ToString());
                }
                else
                {
                    linkLabelThoiHan.Text = "Chưa đăng ký bản quyền";
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadLogoThongTin()
        {
            try
            {
                pictureLogoBenhVien.Image = Image.FromFile(@"Picture\Logo_benhvien.jpg");
                pictureAnhBenhVien.Image = Image.FromFile(@"Picture\logo_user.jpg");
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadThongTinVeCSYT()
        {
            try
            {
                string thongtinbv = "SELECT hospitalcode,hospitalname,hospitaladdress,giamdocname,soytename FROM hospital limit 1;";
                DataView dtthongtindv = new DataView(condb.GetDataTable_HIS(thongtinbv));
                if (dtthongtindv != null && dtthongtindv.Count > 0)
                {
                    lblTenCSYT.Text = dtthongtindv[0]["hospitalname"].ToString();
                    lblMaCSYT.Text = dtthongtindv[0]["hospitalcode"].ToString();
                    lblDiaChi.Text = dtthongtindv[0]["hospitaladdress"].ToString();
                    lblGiamDocBV.Text = dtthongtindv[0]["giamdocname"].ToString();
                    GlobalStore.SoYTe_String = dtthongtindv[0]["soytename"].ToString();
                    GlobalStore.TenBenhVien_String = dtthongtindv[0]["hospitalname"].ToString();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBoxEdit comboBox = sender as ComboBoxEdit;
                string skinName = comboBox.Text;
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = skinName;

                Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                _config.AppSettings.Settings["skin"].Value = skinName;
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Events

        private void linkLabelTenNguoiDung_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                TabTrangChu.frmThayPass frmPass = new TabTrangChu.frmThayPass();
                frmPass.ShowDialog();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }
        private void linkLabelTenNguoiDung_MouseHover(object sender, EventArgs e)
        {
            try
            {
                timerThongBao.Start();
                lblThongBao.Visible = true;
                lblThongBao.Text = "Click vào đây để thay đổi mật khẩu";
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Tabcontrol function
        //Dong tab
        private void xtraTabControlHome_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                XtraTabControl xtab = (XtraTabControl)sender;
                int i = xtab.SelectedTabPageIndex;
                DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs arg = e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs;
                xtab.TabPages.Remove((arg.Page as XtraTabPage));
                xtab.SelectedTabPageIndex = i - 1;
                //(arg.Page as XtraTabPage).PageVisible = false;
                System.GC.Collect();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void xtraTabControlHome_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                frmMain = new frmMain();
                this.CurrentTabPage = e.Page.Name;
                XtraTabControl xtab = new XtraTabControl();
                xtab = (XtraTabControl)sender;
                if (xtab != null)
                {
                    this.SelectedTabPageIndex = xtab.SelectedTabPageIndex;
                    //delegate - thong tin chuc nang
                    if (MyGetData != null)
                    {// tại đây gọi nó
                        MyGetData(xtab.TabPages[xtab.SelectedTabPageIndex].Text);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion



    }
}
