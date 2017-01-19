using System;
using System.Windows.Forms;

namespace MedicalLink.FormCommon
{
    public partial class frmMain : Form
    {
        private void KiemTraPhanQuyenNguoiDung()
        {
            try
            {
                //// Ket Noi DB
                //Menu_KetNoiDB.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "SYS_01");
                ////Quản lý người dùng
                //MenuQuanLyNguoiDung.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "SYS_02");
                //// Sửa thời gian ra viện
                //navSuaTGRV.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_01");
                //// Chuyển tiền tạm ứng
                //navChuyenTien.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_02");
                ////Mở bệnh án
                //navMoBenhAn.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_03");
                ////Sửa ngày duyệt kế toán
                //navSuaNgayDuyetKT.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_04");
                //// Xử lý bệnh nhân bỏ khoa
                //navXuLyBNBoKhoa.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_05");
                ////Update danh mục thuốc
                //navDanhMucThuoc.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "UPD_01");
                //// Update danh mục dịch vụ
                //navDanhMucDichVu.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "UPD_02");
                ////BC bệnh nhân sử dụng dịch vụ ...
                //navDSBNSDDVz.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_02");
                ////Danh sách nhân viên
                //MenuDSUserMoBA.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "MENU_01");
                ////Sửa mã, tên, giá dịch vụ/thuốc của BN
                //navSuaGiaDV.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_06");
                ////TOL_07
                //navUpdateDataSerPrice.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_07");
                //// BC thống kê bệnh theo ICD10
                //navThongKeTheoICD.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_03");
                //// Tìm phiếu tổng hợp y lệnh
                //navTimTHYL.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_04");
                //// BC BHYT 21 - chênh
                //navBHYT21ChenhTT37.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_05");
                ////BC chi dinh PTTT
                //navBCSuDungPTTT.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_06");
                //// BC chi phí tăng thêm do thay đổi theo TT37 BHYT
                //navBCCV1054HaiPhong.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "REP_07");

                ////Chạy update khả dụng-tồn kho
                //navChayKDTK.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_08");
                ////Sửa phơi thanh toán của bệnh nhân
                //navSuaPhoiThanhToan.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_09");
                ////Sửa phiếu chỉ định dịch vụ
                //navSuaMauBenhPham.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_10");
                ////Tìm dịch vụ/thuốc không có mã trong danh mục
                //navTimDVKhongMa.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "SUPPORT_01");
                ////Sửa thông tin bệnh án
                //navSuaThongTinBenhAn.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_11");
                ////Kiểm tra HSBA sai trạng thái
                //navKTTrangThaiHSBA.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_12");
                ////Kiểm tra Đơn thuốc nội trú sai trạng thái
                //navKTDonThuocNoiTru.Enabled = CheckPermission.ChkPerModule(SessionLogin.SessionUsercode.ToString(), "TOL_13");
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
                throw;
            }
        }

        private void EnableAndDisableChucNang(bool enabledisable)
        {
            try
            {
                //// Ket Noi DB
                //Menu_KetNoiDB.Enabled = enabledisable;
                ////Quản lý người dùng
                //MenuQuanLyNguoiDung.Enabled = enabledisable;
                //// Sửa thời gian ra viện
                //navSuaTGRV.Enabled = enabledisable;
                //// Chuyển tiền tạm ứng
                //navChuyenTien.Enabled = enabledisable;
                ////Mở bệnh án
                //navMoBenhAn.Enabled = enabledisable;
                ////Sửa ngày duyệt kế toán
                //navSuaNgayDuyetKT.Enabled = enabledisable;
                //// Xử lý bệnh nhân bỏ khoa
                //navXuLyBNBoKhoa.Enabled = enabledisable;
                ////Update danh mục thuốc
                //navDanhMucThuoc.Enabled = enabledisable;
                //// Update danh mục dịch vụ
                //navDanhMucDichVu.Enabled = enabledisable;
                ////BC bệnh nhân sử dụng dịch vụ ...
                //navDSBNSDDVz.Enabled = enabledisable;
                ////Danh sách nhân viên
                //MenuDSUserMoBA.Enabled = enabledisable;
                ////Sửa mã, tên, giá dịch vụ/thuốc của BN
                //navSuaGiaDV.Enabled = enabledisable;
                ////TOL_07
                //navUpdateDataSerPrice.Enabled = enabledisable;
                //// BC thống kê bệnh theo ICD10
                //navThongKeTheoICD.Enabled = enabledisable;
                //// Tìm phiếu tổng hợp y lệnh
                //navTimTHYL.Enabled = enabledisable;
                ////Chạy update khả dụng-tồn kho
                //navChayKDTK.Enabled = enabledisable;
                ////Sửa phơi thanh toán của bệnh nhân
                //navSuaPhoiThanhToan.Enabled = enabledisable;
                ////Sửa phiếu chỉ định dịch vụ
                //navSuaMauBenhPham.Enabled = enabledisable;
                ////Tìm dịch vụ/thuốc không có mã trong danh mục
                //navTimDVKhongMa.Enabled = enabledisable;
                ////Sửa thông tin bệnh án
                //navSuaThongTinBenhAn.Enabled = enabledisable;
                //// BC BHYT 21 - chênh
                //navBHYT21ChenhTT37.Enabled = enabledisable;
                ////BC chi dinh PTTT
                //navBCSuDungPTTT.Enabled = enabledisable;
                //// BC chi phí tăng thêm do thay đổi theo TT37 BHYT
                //navBCCV1054HaiPhong.Enabled = enabledisable;
                ////Kiểm tra HSBA sai trạng thái
                //navKTTrangThaiHSBA.Enabled = enabledisable;
                ////Kiểm tra Đơn thuốc nội trú sai trạng thái
                //navKTDonThuocNoiTru.Enabled = enabledisable;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
                throw;
            }
        }

    }
}
