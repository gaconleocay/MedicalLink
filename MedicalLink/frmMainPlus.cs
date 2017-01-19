using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using Report01.ChucNang;
using Report01.ChucNang.XuLyMauBenhPham;
using Report01.ClassCommon;

namespace Report01
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private void KiemTraPhanQuyen_EnableButton()
        {
            // Kiểm tra phân quyền
            if (SessionLogin.SessionUsercode == Report01.Base.KeyTrongPhanMem.AdminUser_key)
            {
                EnableAndDisableChucNang(true); //admin              
            }
            else
            {
                if (SessionLogin.KiemTraLicenseSuDung)
                {
                    KiemTraPhanQuyenNguoiDung();
                }
                else
                {
                    EnableAndDisableChucNang(false);
                    DialogResult dialogResult = MessageBox.Show("Phần mềm hết bản quyền! \nVui lòng liên hệ với tác giả để được trợ giúp.\nAuthor: Hồng Minh Nhất \nE-mail: hongminhnhat15@gmail.com \nPhone: 0868-915-456", "Thông báo !!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                    barSubItemSkin.AddItem(item);
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

            //System.Random r = new Random();
            //DevExpress.Skins.SkinContainerCollection skinCollection = DevExpress.Skins.SkinManager.Default.Skins;
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinCollection[r.Next(0, skinCollection.Count)].SkinName);
            ////DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2013");
        }

        private void item_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DevExpress.XtraBars.BarItem item = e.Item;
                //defaultLookAndFeel1.LookAndFeel.SkinName = item.Caption;
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
        private void KiemTraPhanQuyenNguoiDung()
        {
            try
            {
                // Ket Noi DB
                Menu_KetNoiDB.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "SYS_01");
                //Quản lý người dùng
                MenuQuanLyNguoiDung.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "SYS_02");
                // Sửa thời gian ra viện
                navSuaTGRV.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_01");
                // Chuyển tiền tạm ứng
                navChuyenTien.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_02");
                //Mở bệnh án
                navMoBenhAn.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_03");
                //Sửa ngày duyệt kế toán
                navSuaNgayDuyetKT.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_04");
                // Xử lý bệnh nhân bỏ khoa
                navXuLyBNBoKhoa.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_05");
                //Update danh mục thuốc
                navDanhMucThuoc.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "UPD_01");
                // Update danh mục dịch vụ
                navDanhMucDichVu.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "UPD_02");
                //BC bệnh nhân sử dụng dịch vụ ...
                navDSBNSDDVz.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_02");
                //Danh sách nhân viên
                MenuDSUserMoBA.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "MENU_01");
                //Sửa mã, tên, giá dịch vụ/thuốc của BN
                navSuaGiaDV.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_06");
                //TOL_07
                navUpdateDataSerPrice.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_07");
                // BC thống kê bệnh theo ICD10
                navThongKeTheoICD.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_03");
                // Tìm phiếu tổng hợp y lệnh
                navTimTHYL.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_04");
                // BC BHYT 21 - chênh
                navBHYT21ChenhTT37.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_05");
                //BC chi dinh PTTT
                navBCSuDungPTTT.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_06");
                // BC chi phí tăng thêm do thay đổi theo TT37 BHYT
                navBCCV1054HaiPhong.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_07");

                //Chạy update khả dụng-tồn kho
                navChayKDTK.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_08");
                //Sửa phơi thanh toán của bệnh nhân
                navSuaPhoiThanhToan.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_09");
                //Sửa phiếu chỉ định dịch vụ
                navSuaMauBenhPham.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_10");
                //Tìm dịch vụ/thuốc không có mã trong danh mục
                navTimDVKhongMa.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "SUPPORT_01");
                //Sửa thông tin bệnh án
                navSuaThongTinBenhAn.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_11");
                //Kiểm tra HSBA sai trạng thái
                navKTTrangThaiHSBA.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_12");
                //Kiểm tra Đơn thuốc nội trú sai trạng thái
                navKTDonThuocNoiTru.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_13");
            }
            catch (Exception ex)
            {
                Report01.ClassCommon.Logging.Warn(ex);
                throw;
            }
        }

        private void EnableAndDisableChucNang(bool enabledisable)
        {
            try
            {
                // Ket Noi DB
                Menu_KetNoiDB.Enabled = enabledisable;
                //Quản lý người dùng
                MenuQuanLyNguoiDung.Enabled = enabledisable;
                // Sửa thời gian ra viện
                navSuaTGRV.Enabled = enabledisable;
                // Chuyển tiền tạm ứng
                navChuyenTien.Enabled = enabledisable;
                //Mở bệnh án
                navMoBenhAn.Enabled = enabledisable;
                //Sửa ngày duyệt kế toán
                navSuaNgayDuyetKT.Enabled = enabledisable;
                // Xử lý bệnh nhân bỏ khoa
                navXuLyBNBoKhoa.Enabled = enabledisable;
                //Update danh mục thuốc
                navDanhMucThuoc.Enabled = enabledisable;
                // Update danh mục dịch vụ
                navDanhMucDichVu.Enabled = enabledisable;
                //BC bệnh nhân sử dụng dịch vụ ...
                navDSBNSDDVz.Enabled = enabledisable;
                //Danh sách nhân viên
                MenuDSUserMoBA.Enabled = enabledisable;
                //Sửa mã, tên, giá dịch vụ/thuốc của BN
                navSuaGiaDV.Enabled = enabledisable;
                //TOL_07
                navUpdateDataSerPrice.Enabled = enabledisable;
                // BC thống kê bệnh theo ICD10
                navThongKeTheoICD.Enabled = enabledisable;
                // Tìm phiếu tổng hợp y lệnh
                navTimTHYL.Enabled = enabledisable;
                //Chạy update khả dụng-tồn kho
                navChayKDTK.Enabled = enabledisable;
                //Sửa phơi thanh toán của bệnh nhân
                navSuaPhoiThanhToan.Enabled = enabledisable;
                //Sửa phiếu chỉ định dịch vụ
                navSuaMauBenhPham.Enabled = enabledisable;
                //Tìm dịch vụ/thuốc không có mã trong danh mục
                navTimDVKhongMa.Enabled = enabledisable;
                //Sửa thông tin bệnh án
                navSuaThongTinBenhAn.Enabled = enabledisable;
                // BC BHYT 21 - chênh
                navBHYT21ChenhTT37.Enabled = enabledisable;
                //BC chi dinh PTTT
                navBCSuDungPTTT.Enabled = enabledisable;
                // BC chi phí tăng thêm do thay đổi theo TT37 BHYT
                navBCCV1054HaiPhong.Enabled = enabledisable;
                //Kiểm tra HSBA sai trạng thái
                navKTTrangThaiHSBA.Enabled = enabledisable;
                //Kiểm tra Đơn thuốc nội trú sai trạng thái
                navKTDonThuocNoiTru.Enabled = enabledisable;
            }
            catch (Exception ex)
            {
                Report01.ClassCommon.Logging.Warn(ex);
                throw;
            }
        }

    }
}
