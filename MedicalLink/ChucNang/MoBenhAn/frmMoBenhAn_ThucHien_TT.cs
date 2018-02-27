using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Editors;
using MedicalLink.Base;
using DevExpress.XtraSplashScreen;

namespace MedicalLink.ChucNang.MoBenhAn
{
    internal partial class frmMoBenhAn_ThucHien_TT : Form
    {
        #region Khai bao
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        string _mabn;
        string _madt;
        string _mavp;
        string _hosobn;
        int _kt_khoacuoi;
        string _idkhoa;
        string _idphong;
        string _tenbn;
        string _khoa;
        string _phong;
        int _ktbdt_khoasau;
        string _madtsau;
        string _loaibenhan;
        DataView dv;

        #endregion

        internal frmMoBenhAn_ThucHien_TT(string mabn, string madt, string mavp, string hosobn, int kt_khoacuoi, string idkhoa, string idphong, string tenbn, string khoa, string phong, int ktbdt_khoasau, string madtsau, string loaibenhan)
        {
            InitializeComponent();
            _mabn = mabn.ToString();
            _madt = madt.ToString();
            _mavp = mavp.ToString();
            _hosobn = hosobn.ToString();
            _kt_khoacuoi = kt_khoacuoi;
            _idkhoa = idkhoa.ToString();
            _idphong = idphong.ToString();
            _tenbn = tenbn.ToString();
            _khoa = khoa.ToString();
            _phong = phong.ToString();
            _ktbdt_khoasau = ktbdt_khoasau;
            _madtsau = madtsau.ToString();
            _loaibenhan = loaibenhan.ToString();

        }

        #region Events
        private void btnDBLuu_Click(object sender, EventArgs e)
        {
            // Lấy thời gian
            String datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                string ma_nguoiyeucau = "";
                string ten_nguoiyeucau = "";
                string _xoathoigianravien_medi = "";
                // Lấy dữ liệu danh sách dịch vụ nhập vào
                string[] dsdv_temp = comboBoxNYC.Text.Split('|');
                for (int i = 0; i < dsdv_temp.Length - 1; i++)
                {
                    ma_nguoiyeucau += dsdv_temp[0].ToString().Trim();
                    ten_nguoiyeucau += dsdv_temp[1].ToString().Trim();
                }

                if (cboLyDoMoBenhAn.Text == "Đóng bệnh án nhầm")
                {
                    _xoathoigianravien_medi = " ,thoigianravien='0001-01-01 00:00:00',xutrikhambenhid=0,hinhthucravienid=0 ";
                }


                if (_kt_khoacuoi == 1)
                {
                    //MessageBox.Show("là BN khoa cuối");
                    string sqlmobenhan = "UPDATE medicalrecord SET medicalrecordstatus='2' "+ _xoathoigianravien_medi + " WHERE medicalrecordid=" + _madt + "; UPDATE vienphi SET vienphistatus='0', vienphidate_ravien='0001-01-01 00:00:00', chandoanravien='', chandoanravien_code='',   chandoanravien_kemtheo='', chandoanravien_kemtheo_code='' WHERE vienphiid=" + _mavp + "; UPDATE hosobenhan SET hosobenhandate_ravien='0001-01-01 00:00:00', hosobenhanstatus='0', xutrikhambenhid='0' WHERE hosobenhanid=" + _hosobn + ";";
                    string sqlinsert = "INSERT INTO logevent (LogApp, LogUser, LogForm, SoftVersion, LogTime, IPAddress, ComputerName, PatientID, HoSoBenhAnID, VienPhiID, MedicalRecordID, MauBenhPhamID, SoThuTuPhongKhamID, ServicePriceID, DepartmentGroupID, DepartmentID, LogEventType, LogEventContentDetail, LogEventContent)  VALUES( 'Tools', '" + SessionLogin.SessionUsercode + "', '', '" + SessionLogin.SessionVersion + "', '" + datetime + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + _mabn + "', '" + _hosobn + "', '" + _mavp + "', '" + _madt + "', '0', '0', '0', '" + _idkhoa + "', '" + _idphong + "', '4', '" + _mabn + "|" + _tenbn + "|" + _khoa + "|" + _phong + "|" + SessionLogin.SessionUsercode + "|" + SessionLogin.SessionUsername + "|" + ma_nguoiyeucau + "|" + ten_nguoiyeucau + "|" + memoEditLyDo.Text + "', 'Mở bệnh án " + _tenbn + "," + _mabn + "," + _madt + " -> " + ma_nguoiyeucau + "|" + ten_nguoiyeucau + "," + memoEditLyDo.Text + "');";
                    string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Mở lại bệnh án: Mã BN: " + _mabn + " tên BN: " + _tenbn + " mã VP: " + _mavp + " mã ĐT: " + _madt + " ','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_03');";
                    if (condb.ExecuteNonQuery_HIS(sqlmobenhan))
                    {
                        condb.ExecuteNonQuery_HIS(sqlinsert);
                        condb.ExecuteNonQuery_MeL(sqlinsert_log);
                        //this.Enabled = false;
                        //ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.MO_BENH_AN_THANH_CONG);
                        //frmthongbao.Show();
                        SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm_MBA));
                        System.Threading.Thread.Sleep(2000);
                        SplashScreenManager.CloseForm();
                        this.Close();
                        this.Dispose();
                    }
                }
                else // không là khoa cuối
                {
                    if (_ktbdt_khoasau == 1)
                    {
                        //MessageBox.Show("Không là BN khoa cuối và khoa tiếp theo CHƯA tiếp nhận vào buồng ĐT");
                        // Kiểm tra có phải đang mở mã ĐT ngoài PK hay ko?
                        if (_loaibenhan == "24") // nếu là ngoài PK thì chuyển sang BA ngoại trú
                        {
                            string sqlmobenhan = "UPDATE MedicalRecord SET MedicalRecordStatus = '2', medicalrecordid_next=0 " + _xoathoigianravien_medi + " WHERE MedicalRecordID=" + _madt + "; UPDATE vienphi SET DepartmentGroupID = '" + _idkhoa + "', DepartmentID = '" + _idphong + "' WHERE VienPhiID = '" + _mavp + "'";
                            string sqlxoamadt = "DELETE FROM MedicalRecord WHERE MedicalRecordID =" + _madtsau + " and departmentid=0;";
                            string sqlchuyenngt = "UPDATE vienphi SET LoaiVienPhiID = '1' WHERE VienPhiID = '" + _mavp + "'";
                            string sqlinsert = "INSERT INTO logevent (LogApp, LogUser, LogForm, SoftVersion, LogTime, IPAddress, ComputerName, PatientID, HoSoBenhAnID, VienPhiID, MedicalRecordID, MauBenhPhamID, SoThuTuPhongKhamID, ServicePriceID, DepartmentGroupID, DepartmentID, LogEventType, LogEventContentDetail, LogEventContent)  VALUES( 'Tools', '" + SessionLogin.SessionUsercode + "', '', '" + SessionLogin.SessionVersion + "', '" + datetime + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + _mabn + "', '" + _hosobn + "', '" + _mavp + "', '" + _madt + "', '0', '0', '0', '" + _idkhoa + "', '" + _idphong + "', '4', '" + _mabn + "|" + _tenbn + "|" + _khoa + "|" + _phong + "|" + SessionLogin.SessionUsercode + "|" + SessionLogin.SessionUsername + "|" + ma_nguoiyeucau + "|" + ten_nguoiyeucau + "|" + memoEditLyDo.Text + "', 'Mở bệnh án " + _tenbn + "," + _mabn + "," + _madt + " -> " + ma_nguoiyeucau + "|" + ten_nguoiyeucau + "," + memoEditLyDo.Text + "');";
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Mở lại bệnh án: Mã BN: " + _mabn + " tên BN: " + _tenbn + " mã VP: " + _mavp + " mã ĐT: " + _madt + ". Xóa mã điều trị ở phòng hành chính: " + _madtsau + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_03');";
                            condb.ExecuteNonQuery_HIS(sqlmobenhan);
                            condb.ExecuteNonQuery_HIS(sqlxoamadt);
                            condb.ExecuteNonQuery_HIS(sqlchuyenngt);
                            condb.ExecuteNonQuery_HIS(sqlinsert);
                            condb.ExecuteNonQuery_MeL(sqlinsert_log);
                            //this.Enabled = false;
                            //ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.MO_BENH_AN_THANH_CONG);
                            //frmthongbao.Show();
                            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm_MBA));
                            System.Threading.Thread.Sleep(2000);
                            SplashScreenManager.CloseForm();
                            this.Close();
                            this.Dispose();
                        }
                        else
                        {
                            string sqlmobenhan = "UPDATE MedicalRecord SET MedicalRecordStatus = '2', medicalrecordid_next=0 " + _xoathoigianravien_medi + " WHERE MedicalRecordID=" + _madt + "; UPDATE vienphi SET DepartmentGroupID = '" + _idkhoa + "', DepartmentID = '" + _idphong + "' WHERE VienPhiID = '" + _mavp + "'";
                            string sqlxoamadt = "DELETE FROM MedicalRecord WHERE MedicalRecordID =" + _madtsau + " and departmentid=0;";
                            string sqlinsert = "INSERT INTO logevent (LogApp, LogUser, LogForm, SoftVersion, LogTime, IPAddress, ComputerName, PatientID, HoSoBenhAnID, VienPhiID, MedicalRecordID, MauBenhPhamID, SoThuTuPhongKhamID, ServicePriceID, DepartmentGroupID, DepartmentID, LogEventType, LogEventContentDetail, LogEventContent)  VALUES( 'Tools', '" + SessionLogin.SessionUsercode + "', '', '" + SessionLogin.SessionVersion + "', '" + datetime + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + _mabn + "', '" + _hosobn + "', '" + _mavp + "', '" + _madt + "', '0', '0', '0', '" + _idkhoa + "', '" + _idphong + "', '4', '" + _mabn + "|" + _tenbn + "|" + _khoa + "|" + _phong + "|" + SessionLogin.SessionUsercode + "|" + SessionLogin.SessionUsername + "|" + ma_nguoiyeucau + "|" + ten_nguoiyeucau + "|" + memoEditLyDo.Text + "', 'Mở bệnh án " + _tenbn + "," + _mabn + "," + _madt + " -> " + ma_nguoiyeucau + "|" + ten_nguoiyeucau + "," + memoEditLyDo.Text + "');";
                            string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Mở lại bệnh án: Mã BN: " + _mabn + " tên BN: " + _tenbn + " mã VP: " + _mavp + " mã ĐT: " + _madt + ". Xóa mã điều trị ở phòng hành chính: " + _madtsau + "','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_03');";

                            if (condb.ExecuteNonQuery_HIS(sqlmobenhan))
                            {
                                condb.ExecuteNonQuery_HIS(sqlxoamadt);
                                condb.ExecuteNonQuery_HIS(sqlinsert);
                                condb.ExecuteNonQuery_MeL(sqlinsert_log);
                                this.Enabled = false;

                                //ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.MO_BENH_AN_THANH_CONG);
                                //frmthongbao.Show();
                                SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm_MBA));
                                System.Threading.Thread.Sleep(2000);
                                SplashScreenManager.CloseForm();
                                this.Close();
                                this.Dispose();
                            }
                        }

                    }
                    else
                    {
                        //MessageBox.Show("Không là BN khoa cuối và khoa tiếp theo đã tiếp nhận vào buồng ĐT");
                        string sqlmobenhan = "UPDATE MedicalRecord SET MedicalRecordStatus = '2' " + _xoathoigianravien_medi + " WHERE MedicalRecordID=" + _madt + ";";
                        string sqlinsert = "INSERT INTO logevent (LogApp, LogUser, LogForm, SoftVersion, LogTime, IPAddress, ComputerName, PatientID, HoSoBenhAnID, VienPhiID, MedicalRecordID, MauBenhPhamID, SoThuTuPhongKhamID, ServicePriceID, DepartmentGroupID, DepartmentID, LogEventType, LogEventContentDetail, LogEventContent)  VALUES( 'Tools', '" + SessionLogin.SessionUsercode + "', '', '" + SessionLogin.SessionVersion + "', '" + datetime + "', '" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + _mabn + "', '" + _hosobn + "', '" + _mavp + "', '" + _madt + "', '0', '0', '0', '" + _idkhoa + "', '" + _idphong + "', '4', '" + _mabn + "|" + _tenbn + "|" + _khoa + "|" + _phong + "|" + SessionLogin.SessionUsercode + "|" + SessionLogin.SessionUsername + "|" + ma_nguoiyeucau + "|" + ten_nguoiyeucau + "|" + memoEditLyDo.Text + "', 'Mở bệnh án " + _tenbn + "," + _mabn + "," + _madt + " -> " + ma_nguoiyeucau + "|" + ten_nguoiyeucau + "," + memoEditLyDo.Text + "');";
                        string sqlinsert_log = "INSERT INTO tools_tbllog(loguser, logvalue, ipaddress, computername, softversion, logtime, logtype) VALUES ('" + SessionLogin.SessionUsercode + "', 'Mở lại bệnh án: Mã BN: " + _mabn + " tên BN: " + _tenbn + " mã VP: " + _mavp + " mã ĐT: " + _madt + " ','" + SessionLogin.SessionMyIP + "', '" + SessionLogin.SessionMachineName + "', '" + SessionLogin.SessionVersion + "', '" + datetime + "', 'TOOL_03');";
                        if (condb.ExecuteNonQuery_HIS(sqlmobenhan))
                        {
                            condb.ExecuteNonQuery_HIS(sqlinsert);
                            condb.ExecuteNonQuery_MeL(sqlinsert_log);
                            //ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.MO_BENH_AN_THANH_CONG);
                            //frmthongbao.Show();
                            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm_MBA));
                            System.Threading.Thread.Sleep(2000);
                            SplashScreenManager.CloseForm();
                            this.Close();
                            this.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
                SplashScreenManager.CloseForm();
            }
        }
        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            string en_pass = MedicalLink.Base.EncryptAndDecrypt.Encrypt("", true);
            try
            {
                string sql_kttontai = "SELECT usercode as manv, username as tennv FROM nhompersonnel WHERE usercode='" + txtNVID.Text.Trim() + "' ORDER BY manv";
                DataView dv_dsnv = new DataView(condb.GetDataTable_HIS(sql_kttontai));
                if (dv_dsnv != null && dv_dsnv.Count > 0)
                {
                    //su dung form thong bao nay thi khong hien thi duoc
                    //ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Đã tồn tại mã nhân viên trong hệ thống!");
                    //frmthongbao.Show();
                    MessageBox.Show("Đã tồn tại mã nhân viên trong hệ thống!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sql = "INSERT INTO nhompersonnel(usercode, username, userpassword, userstatus, usergnhom, usernote) VALUES ('" + txtNVID.Text.Trim() + "','" + txtNVName.Text.Trim() + "','" + en_pass + "','0','3','Nhân viên');";
                    condb.ExecuteNonQuery_HIS(sql);
                    MessageBox.Show("Thêm nhân viên mới thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmMoBenhAn_ThucHien_TT_Load(null, null);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Load
        private void frmMoBenhAn_ThucHien_TT_Load(object sender, EventArgs e)
        {
            try
            {
                txtNVID.Text = "";
                txtNVName.Text = "";
                LoadDanhSachNguoiYeuCau();
                comboBoxNYC.SelectAll();
                comboBoxNYC.Focus();
                comboBoxNYC.ResetText();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void LoadDanhSachNguoiYeuCau()
        {
            try
            {
                string sqldsnv = "SELECT usercode as manv, username as tennv FROM nhompersonnel ORDER BY usercode";
                DataTable da_dt = condb.GetDataTable_HIS(sqldsnv);

                //comboBoxNYC.ValueMember = "InvtID"; 
                DataView da_dt_gop = new DataView(MedicalLink.Base.UtilsTable.getTableDisplayWrapper(da_dt, " | ", "maten_nv", "manv", "tennv"));
                //comboBoxNYC.DataSource = da_dt_gop;
                //comboBoxNYC.DisplayMember = "maten_nv";
                //comboBoxNYC.ValueMember = "manv";

                comboBoxNYC.Properties.DataSource = da_dt_gop;
                comboBoxNYC.Properties.DisplayMember = "maten_nv";
                comboBoxNYC.Properties.ValueMember = "manv";

            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Custom
        private void comboBoxNYC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    memoEditLyDo.Focus();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void txtNVID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtNVName.Focus();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void txtNVName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnThemNhanVien.Focus();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void cboLyDoMoBenhAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                memoEditLyDo.Text = cboLyDoMoBenhAn.Text;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion


    }
}
