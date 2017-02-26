using MedicalLink.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;

namespace MedicalLink.FormCommon
{
    public partial class frmMain : Form
    {
        private void KiemTraPhanQuyenNguoiDung()
        {
            try
            {
                List<ClassCommon.classPermission> lstDSChucNang = new List<ClassCommon.classPermission>();
                lstDSChucNang = MedicalLink.Base.listChucNang.getDanhSachChucNang().Where(o => o.permissiontype == 2).ToList();
                if (SessionLogin.SessionUsercode != KeyTrongPhanMem.AdminUser_key)
                {
                    string sqlquerry_per = "SELECT permissioncode, permissionname, permissioncheck FROM tools_tbluser_permission WHERE usercode='" + MedicalLink.Base.EncryptAndDecrypt.Encrypt(SessionLogin.SessionUsercode, true) + "';";
                    DataView dv_per = new DataView(condb.getDataTable(sqlquerry_per));
                    if (dv_per != null && dv_per.Count > 0)
                    {
                        for (int i = 0; i < lstDSChucNang.Count; i++)
                        {
                            for (int j = 0; j < dv_per.Count; j++)
                            {
                                if (lstDSChucNang[i].permissioncode == EncryptAndDecrypt.Decrypt(dv_per[j]["permissioncode"].ToString(), true))
                                {
                                    lstDSChucNang[i].permissioncheck = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < lstDSChucNang.Count; i++)
                    {
                        lstDSChucNang[i].permissioncheck = true;
                    }
                }
                var lstchucnang= lstDSChucNang.Where(o=>o.permissioncheck==true).ToList();
                if (lstchucnang.Count > 0)
                { 
                }
                else
                {
                    tabMenuChucNang.PageVisible = false;
                }
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
