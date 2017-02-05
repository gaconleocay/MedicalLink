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
        #endregion

        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private string MaMayMaHoa = String.Empty;

        public string serverhost = ConfigurationManager.AppSettings["ServerHost"].ToString();
        public string serveruser = ConfigurationManager.AppSettings["Username"].ToString();
        public string serverpass = ConfigurationManager.AppSettings["Password"].ToString();
        public string serverdb = ConfigurationManager.AppSettings["Database"].ToString();

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
                EnablePhanQuyenNguoiDung();
                LoadThongTinCoBan();
                LoadVersion();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void EnablePhanQuyenNguoiDung()
        {
            try
            {
                btnDSNguoiDung.Enabled = MedicalLink.Base.CheckPermission.ChkPerModule("SYS_02");
                btnDSNhanVien.Enabled = MedicalLink.Base.CheckPermission.ChkPerModule("SYS_03");
                btnDSOption.Enabled = MedicalLink.Base.CheckPermission.ChkPerModule("SYS_04");
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void LoadThongTinCoBan()
        {
            try
            {
                //Thong tin ve License
                HienThiThongTinVeLicense();
                btnLicenseKiemTra_Click(null, null);
                //Thong tin ve Database
                linkLabelTenDatabase.Text = MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString().Trim(), true) + " [" + MedicalLink.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString().Trim(), true) + "]";

                //THong tin ve tai khoan dang nhap
                if (MedicalLink.Base.SessionLogin.SessionUsername == "" || MedicalLink.Base.SessionLogin.SessionUsername == null)
                {
                    linkLabelTenNguoiDung.Text = ".........";
                }
                else
                {
                    linkLabelTenNguoiDung.Text = MedicalLink.Base.SessionLogin.SessionUsername;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
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
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region License
        private void linkLabelThoiHan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                groupBoxDatabase.Visible = false;
                groupBoxDoiMatKhau.Visible = false;
                if (groupBoxLicense.Visible == false)
                {
                    groupBoxLicense.Visible = true;
                    HienThiThongTinVeLicense();
                    LoadFormTaoLicense();
                }
                else
                {
                    groupBoxLicense.Visible = false;
                    groupBoxTaoLicense.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void HienThiThongTinVeLicense()
        {
            try
            {
                MaMayMaHoa = MedicalLink.FormCommon.DangKyBanQuyen.kiemTraLicenseHopLe.LayThongTinMaMayVaMaHoa();
                txtMaMay.Text = MaMayMaHoa;
                txtMaMay.ReadOnly = true;
                //Load License tu DB ra
                string kiemtra_licensetag = "SELECT * FROM tools_clients WHERE clientcode='" + MaMayMaHoa + "' ;";
                DataView dv = new DataView(condb.getDataTable(kiemtra_licensetag));
                if (dv != null && dv.Count > 0)
                {
                    txtKeyKichHoat.Text = dv[0]["clientlicense"].ToString();
                }
                txtKeyKichHoat.Focus();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void LoadFormTaoLicense()
        {
            try
            {
                SessionLogin.SessionUsercode = MedicalLink.Base.KeyTrongPhanMem.AdminUser_key;
                if (SessionLogin.SessionUsercode == MedicalLink.Base.KeyTrongPhanMem.AdminUser_key)
                {
                    groupBoxTaoLicense.Visible = true;
                    txtTaoLicensePassword.Focus();
                    btnTaoLicenseTao.Enabled = false;
                    DateTime tuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                    DateTime denNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                    dtTaoLicenseKeyTuNgay.Value = tuNgay;
                    dtTaoLicenseKeyDenNgay.Value = denNgay;
                }
                else
                {
                    groupBoxTaoLicense.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnLicenseKiemTra_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtKeyKichHoat.Text.Trim()))
                {
                    //Giai ma
                    string makichhoat_giaima = MedicalLink.FormCommon.DangKyBanQuyen.EncryptAndDecryptLicense.Decrypt(txtKeyKichHoat.Text.Trim(), true);
                    //Tach ma kich hoat:
                    string mamay_keykichhoat = "";
                    long thoigianTu = 0;
                    long thoigianDen = 0;
                    string[] makichhoat_tach = makichhoat_giaima.Split('$');

                    if (makichhoat_tach.Length == 4)
                    {
                        mamay_keykichhoat = makichhoat_tach[1];
                        thoigianTu = Convert.ToInt64(makichhoat_tach[2].ToString().Trim() ?? "0");
                        thoigianDen = Convert.ToInt64(makichhoat_tach[3].ToString().Trim() ?? "0");
                        //Thoi gian hien tai
                        long datetime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                        string thoigianTu_text = DateTime.ParseExact(thoigianTu.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
                        string thoigianDen_text = DateTime.ParseExact(thoigianDen.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
                        //Kiem tra License hop le
                        if (mamay_keykichhoat == SessionLogin.MaMayTinhNguoiDungMaHoa && datetime < thoigianDen)
                        {
                            SessionLogin.KiemTraLicenseSuDung = true;
                            linkLabelThoiHan.Text = "Từ: " + thoigianTu_text + " đến: " + thoigianDen_text;
                        }
                        else
                        {
                            SessionLogin.KiemTraLicenseSuDung = false;
                            linkLabelThoiHan.Text = "Mã kích hoạt hết hạn sử dụng";
                        }
                    }
                    else
                    {
                        SessionLogin.KiemTraLicenseSuDung = false;
                        linkLabelThoiHan.Text = "Sai mã kích hoạt";
                    }
                }
                else
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Chưa nhập mã kích hoạt!";
                    linkLabelThoiHan.Text = "none";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnLicenseLuu_Click(object sender, EventArgs e)
        {
            try
            {
                //Luu key kich hoat vao DB
                string update_license = "UPDATE tools_clients SET clientlicense='" + txtKeyKichHoat.Text.Trim() + "' WHERE clientcode='" + MaMayMaHoa + "' ;";
                if (condb.ExecuteNonQuery(update_license))
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Lưu mã kích hoạt thành công";
                }
                else
                {
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnLicenseCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();    //Clear if any old value is there in Clipboard        
                Clipboard.SetText(txtMaMay.Text); //Copy text to Clipboard
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Tao License
        private void btnTaoLicenseTao_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTaoLicenseMaMay.Text != "")
                {
                    // Lấy từ ngày, đến ngày
                    string datetungay = DateTime.ParseExact(dtTaoLicenseKeyTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss");
                    string datedenngay = DateTime.ParseExact(dtTaoLicenseKeyDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMddHHmmss");

                    string MaMayVaThoiGianSuDung = MedicalLink.Base.KeyTrongPhanMem.SaltEncrypt + "$" + txtTaoLicenseMaMay.Text + "$" + datetungay + "$" + datedenngay;

                    txtTaoLicenseMaKichHoat.Text = MedicalLink.FormCommon.DangKyBanQuyen.EncryptAndDecryptLicense.Encrypt(MaMayVaThoiGianSuDung, true);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnTaoLicenseCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();    //Clear if any old value is there in Clipboard        
                Clipboard.SetText(txtTaoLicenseMaKichHoat.Text); //Copy text to Clipboard
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void txtTaoLicensePassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //Kiem tra pass dung hay sai?
                    if (txtTaoLicensePassword.Text.Trim() == MedicalLink.Base.KeyTrongPhanMem.LayLicense_key && SessionLogin.SessionUsercode == MedicalLink.Base.KeyTrongPhanMem.AdminUser_key)
                    {
                        btnTaoLicenseTao.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Ket noi database
        private void linkLabelTenDatabase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                groupBoxLicense.Visible = false;
                groupBoxDoiMatKhau.Visible = false;
                groupBoxTaoLicense.Visible = false;
                if (groupBoxDatabase.Visible == false)
                {
                    groupBoxDatabase.Visible = true;
                    LoadKetNoiDatabase();
                }
                else
                {
                    groupBoxDatabase.Visible = false;
                }
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
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void btnDBKiemTra_Click(object sender, EventArgs e)
        {
            try
            {
                bool boolfound = false;
                // PostgeSQL-style connection string
                string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                    txtDBHost.Text, txtDBPort.Text, txtDBUser.Text, txtDBPass.Text, txtDBName.Text);
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // quite complex sql statement
                string sql = "SELECT * FROM tbuser";
                // Tạo cầu nối giữa dataset và datasource để thực hiện công việc như đọc hay cập nhật dữ liệu
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    boolfound = true;
                    MessageBox.Show("Kết nối đến cơ sở dữ liệu thành công!", "Thông báo");
                }
                if (boolfound == false)
                {
                    MessageBox.Show("Lỗi kết nối đến cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dr.Close();
                conn.Close();
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
                Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                _config.AppSettings.Settings["ServerHost"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBHost.Text.Trim(), true);
                _config.AppSettings.Settings["ServerPort"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBPort.Text.Trim(), true);
                _config.AppSettings.Settings["Username"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBUser.Text.Trim(), true);
                _config.AppSettings.Settings["Password"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBPass.Text.Trim(), true);
                _config.AppSettings.Settings["Database"].Value = MedicalLink.Base.EncryptAndDecrypt.Encrypt(txtDBName.Text.Trim(), true);
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                MessageBox.Show("Lưu dữ liệu thành công", "Thông báo");
                //this.Visible = false;
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
                if (KetNoiSCDLProcess.CreateTableTblUser() && KetNoiSCDLProcess.CreateTableTblPermission() && KetNoiSCDLProcess.CreateTableTblDepartment() && KetNoiSCDLProcess.CreateTableTblLog() && KetNoiSCDLProcess.CreateTableTblUpdateKhaDung() && KetNoiSCDLProcess.CreateTableTblServiceFull() && KetNoiSCDLProcess.CreateTableTblClients() && KetNoiSCDLProcess.CreateTableTblDVKTBHYTChenh() && KetNoiSCDLProcess.CreateTableTblDVKTBHYTChenhNew() && KetNoiSCDLProcess.CreateViewServicepriceDichVu() && KetNoiSCDLProcess.CreateViewServicepriceThuoc() && KetNoiSCDLProcess.CreateTableBCBNDangDTTmp() && KetNoiSCDLProcess.CreateTableOption())
                {
                    MessageBox.Show("Cập nhật cơ sở dữ liệu thành công", "Thông báo");
                }
                //KetNoiSCDLProcess.CreateTableColumeBackupDichVu() &&
                //KetNoiSCDLProcess.UpdateTableWithVersion()
                //KetNoiSCDLProcess.CreateTableBCTongTheKhoa()
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật cơ sở dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MedicalLink.Base.Logging.Error("Lỗi cập nhật cơ sở dữ liệu!" + ex.ToString());
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        #region Thay doi mat khau
        private void linkLabelTenNguoiDung_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                groupBoxLicense.Visible = false;
                groupBoxDatabase.Visible = false;
                groupBoxTaoLicense.Visible = false;
                if (groupBoxDoiMatKhau.Visible == false)
                {
                    groupBoxDoiMatKhau.Visible = true;
                }
                else
                {
                    groupBoxDoiMatKhau.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnDoiMKLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPasswordOld.Text == "" || txtPasswordNew1.Text == "" || txtPasswordNew2.Text == "")
                    MessageBox.Show("Xin vui lòng nhập đầy đủ thông tin");
                else if (txtPasswordNew1.Text == "") MessageBox.Show("Bạn chưa nhập mật khẩu mới");
                else if (txtPasswordNew2.Text == "") MessageBox.Show("Bạn chưa nhập lại mật khẩu mới");
                else if (txtPasswordNew1.Text != txtPasswordNew2.Text) MessageBox.Show("Mật khẩu mới của bạn không trùng khớp");
                else
                {
                    // Thực hiện đổi pass
                    try
                    {
                        // Querry lấy dữ liệu về bn có VP nhập vào
                        string sqlquerry = "select * from tools_tbluser where usercode='" + EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true) + "' and userpassword='" + EncryptAndDecrypt.Encrypt(txtPasswordOld.Text, true) + "'";
                        DataView dv_bhytgroup = new DataView(condb.getDataTable(sqlquerry));

                        if (dv_bhytgroup != null && dv_bhytgroup.Count > 0)
                        {
                            string sqlupdate = "update tools_tbluser set userpassword='" + EncryptAndDecrypt.Encrypt(txtPasswordNew1.Text, true) + "' where usercode='" + EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true) + "'";
                            if (condb.ExecuteNonQuery(sqlupdate))
                            {
                                timerThongBao.Start();
                                lblThongBao.Visible = true;
                                lblThongBao.Text = "Thay đổi mật khẩu thành công !";
                            }
                        }
                        else
                        {
                            timerThongBao.Start();
                            lblThongBao.Visible = true;
                            lblThongBao.Text = "Tên tài khoản hoặc mật khẩu cũ sai!";
                        }
                    }
                    catch (Exception ex)
                    {
                        timerThongBao.Start();
                        lblThongBao.Visible = true;
                        lblThongBao.Text = MedicalLink.Base.ThongBaoLable.CO_LOI_XAY_RA;
                        MedicalLink.Base.Logging.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void txtPasswordOld_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPasswordNew1.Focus();
            }
        }

        private void txtPasswordNew1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPasswordNew2.Focus();
            }
        }

        private void txtPasswordNew2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDoiMKLuu.Focus();
            }
        }

        // bắt sự kiện khi check vào nút hiển thị mật khẩu
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEditHienThi.Checked == true)
            {
                txtPasswordNew1.Properties.PasswordChar = '\0';
                txtPasswordNew2.Properties.PasswordChar = '\0';
            }
            else
            {
                txtPasswordNew1.Properties.PasswordChar = '*';
                txtPasswordNew2.Properties.PasswordChar = '*';
            }
        }

        #endregion

        private void cboGiaoDien_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

        private void btnDSNguoiDung_Click(object sender, EventArgs e)
        {
            UserControl ucControlActive = new UserControl();
            try
            {
                ucControlActive = TabControlProcess.SelectUCControlActive("SYS_02");
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlHome, "SYS_02", "Quản lý người dùng", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void btnDSNhanVien_Click(object sender, EventArgs e)
        {
            UserControl ucControlActive = new UserControl();
            try
            {
                //Chon ucControl
                ucControlActive = TabControlProcess.SelectUCControlActive("SYS_03");
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlHome, "SYS_03", "Danh sách nhân viên", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

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
                MedicalLink.Base.Logging.Warn(ex);
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
                    //frmMain.StatusTenBC.Caption = e.Page.Tooltip;
                    frmMain.HienThiTenChucNang();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        #endregion

        private void btnDSOption_Click(object sender, EventArgs e)
        {
            UserControl ucControlActive = new UserControl();
            try
            {
                ucControlActive = TabControlProcess.SelectUCControlActive("SYS_04");
                MedicalLink.FormCommon.TabControlProcess.TabCreating(xtraTabControlHome, "SYS_04", "Danh sách option", ucControlActive);
                ucControlActive.Show();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }



    }
}
