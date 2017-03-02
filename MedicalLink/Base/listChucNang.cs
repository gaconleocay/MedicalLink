using MedicalLink.ClassCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.Base
{
    public class listChucNang
    {
        public static List<ClassCommon.classPermission> getDanhSachChucNang()
        {
            List<ClassCommon.classPermission> lstresult = new List<classPermission>();
            try
            {
                //permissiontype = 1 system
                //permissiontype = 2 Tools
                //permissiontype = 3 bao cao
                //permissiontype = 4 phan quyen thao tac
                //permissiontype = 5 Dashboard
                //System
                ClassCommon.classPermission SYS_01 = new ClassCommon.classPermission();
                SYS_01.permissioncheck = false;
                SYS_01.permissioncode = "SYS_01";
                SYS_01.permissionname = "Kết nối cơ sở dữ liệu";
                SYS_01.permissiontype = 1;
                SYS_01.permissionnote = "Kết nối cơ sở dữ liệu";
                lstresult.Add(SYS_01);

                ClassCommon.classPermission SYS_02 = new ClassCommon.classPermission();
                SYS_02.permissioncheck = false;
                SYS_02.permissioncode = "SYS_02";
                SYS_02.permissionname = "Quản lý người dùng";
                SYS_02.permissiontype = 1;
                SYS_02.permissionnote = "Quản lý người dùng";
                lstresult.Add(SYS_02);

                ClassCommon.classPermission SYS_03 = new ClassCommon.classPermission();
                SYS_03.permissioncheck = false;
                SYS_03.permissioncode = "SYS_03";
                SYS_03.permissionname = "Danh sách nhân viên";
                SYS_03.permissiontype = 1;
                SYS_03.permissionnote = "Danh sách nhân viên";
                lstresult.Add(SYS_03);

                ClassCommon.classPermission SYS_04 = new ClassCommon.classPermission();
                SYS_04.permissioncheck = false;
                SYS_04.permissioncode = "SYS_04";
                SYS_04.permissionname = "Danh sách option";
                SYS_04.permissiontype = 1;
                SYS_04.permissionnote = "Danh sách option";
                lstresult.Add(SYS_04);

                ClassCommon.classPermission SYS_05 = new ClassCommon.classPermission();
                SYS_05.permissioncheck = false;
                SYS_05.permissioncode = "SYS_05";
                SYS_05.permissionname = "Quản trị hệ thống";
                SYS_05.permissiontype = 1;
                SYS_05.permissionnote = "Quản trị hệ thống";
                lstresult.Add(SYS_05);

                //Tools
                ClassCommon.classPermission TOOL_01 = new ClassCommon.classPermission();
                TOOL_01.permissioncheck = false;
                TOOL_01.permissioncode = "TOOL_01";
                TOOL_01.permissionname = "Sửa thời gian ra viện";
                TOOL_01.permissiontype = 2;
                TOOL_01.permissionnote = "Sửa thời gian ra viện";
                lstresult.Add(TOOL_01);

                ClassCommon.classPermission TOOL_02 = new ClassCommon.classPermission();
                TOOL_02.permissioncheck = false;
                TOOL_02.permissioncode = "TOOL_02";
                TOOL_02.permissionname = "Chuyển tiền tạm ứng";
                TOOL_02.permissiontype = 2;
                TOOL_02.permissionnote = "Chuyển tiền tạm ứng";
                lstresult.Add(TOOL_02);

                ClassCommon.classPermission TOOL_03 = new ClassCommon.classPermission();
                TOOL_03.permissioncheck = false;
                TOOL_03.permissioncode = "TOOL_03";
                TOOL_03.permissionname = "Mở bệnh án";
                TOOL_03.permissiontype = 2;
                TOOL_03.permissionnote = "Mở bệnh án";
                lstresult.Add(TOOL_03);

                ClassCommon.classPermission TOOL_04 = new ClassCommon.classPermission();
                TOOL_04.permissioncheck = false;
                TOOL_04.permissioncode = "TOOL_04";
                TOOL_04.permissionname = "Sửa ngày duyệt kế toán";
                TOOL_04.permissiontype = 2;
                TOOL_04.permissionnote = "Sửa ngày duyệt kế toán";
                lstresult.Add(TOOL_04);

                ClassCommon.classPermission TOOL_05 = new ClassCommon.classPermission();
                TOOL_05.permissioncheck = false;
                TOOL_05.permissioncode = "TOOL_05";
                TOOL_05.permissionname = "Xử lý bệnh nhân bỏ khoa";
                TOOL_05.permissiontype = 2;
                TOOL_05.permissionnote = "Xử lý bệnh nhân bỏ khoa";
                lstresult.Add(TOOL_05);

                ClassCommon.classPermission TOOL_06 = new ClassCommon.classPermission();
                TOOL_06.permissioncheck = false;
                TOOL_06.permissioncode = "TOOL_06";
                TOOL_06.permissionname = "Update danh mục thuốc";
                TOOL_06.permissiontype = 2;
                TOOL_06.permissionnote = "Update danh mục thuốc";
                lstresult.Add(TOOL_06);

                ClassCommon.classPermission TOOL_07 = new ClassCommon.classPermission();
                TOOL_07.permissioncheck = false;
                TOOL_07.permissioncode = "TOOL_07";
                TOOL_07.permissionname = "Update danh mục dịch vụ";
                TOOL_07.permissiontype = 2;
                TOOL_07.permissionnote = "Update danh mục dịch vụ";
                lstresult.Add(TOOL_07);

                ClassCommon.classPermission TOOL_08 = new ClassCommon.classPermission();
                TOOL_08.permissioncheck = false;
                TOOL_08.permissioncode = "TOOL_08";
                TOOL_08.permissionname = "Sửa mã, tên, giá dịch vụ/thuốc của BN";
                TOOL_08.permissiontype = 2;
                TOOL_08.permissionnote = "Sửa mã, tên, giá dịch vụ/thuốc của BN";
                lstresult.Add(TOOL_08);

                ClassCommon.classPermission TOOL_09 = new ClassCommon.classPermission();
                TOOL_09.permissioncheck = false;
                TOOL_09.permissioncode = "TOOL_09";
                TOOL_09.permissionname = "Update data table Serviceprice";
                TOOL_09.permissiontype = 2; 
                TOOL_09.permissionnote = "Update data table Serviceprice";
                lstresult.Add(TOOL_09);

                ClassCommon.classPermission TOOL_10 = new ClassCommon.classPermission();
                TOOL_10.permissioncheck = false;
                TOOL_10.permissioncode = "TOOL_10";
                TOOL_10.permissionname = "Chạy update khả dụng-tồn kho";
                TOOL_10.permissiontype = 2;
                TOOL_10.permissionnote = "Chạy update khả dụng-tồn kho";
                lstresult.Add(TOOL_10);

                ClassCommon.classPermission TOOL_11 = new ClassCommon.classPermission();
                TOOL_11.permissioncheck = false;
                TOOL_11.permissioncode = "TOOL_11";
                TOOL_11.permissionname = "Sửa phơi thanh toán của bệnh nhân";
                TOOL_11.permissiontype = 2;
                TOOL_11.permissionnote = "Sửa phơi thanh toán của bệnh nhân";
                lstresult.Add(TOOL_11);

                ClassCommon.classPermission TOOL_12 = new ClassCommon.classPermission();
                TOOL_12.permissioncheck = false;
                TOOL_12.permissioncode = "TOOL_12";
                TOOL_12.permissionname = "Sửa phiếu chỉ định dịch vụ";
                TOOL_12.permissiontype = 2;
                TOOL_12.permissionnote = "Sửa phiếu chỉ định dịch vụ; Xóa đơn thuốc kê tủ trực";
                lstresult.Add(TOOL_12);

                ClassCommon.classPermission TOOL_13 = new ClassCommon.classPermission();
                TOOL_13.permissioncheck = false;
                TOOL_13.permissioncode = "TOOL_13";
                TOOL_13.permissionname = "Sửa thông tin bệnh án";
                TOOL_13.permissiontype = 2;
                TOOL_13.permissionnote = "Sửa thông tin bệnh án";
                lstresult.Add(TOOL_13);

                ClassCommon.classPermission TOOL_14 = new ClassCommon.classPermission();
                TOOL_14.permissioncheck = false;
                TOOL_14.permissioncode = "TOOL_14";
                TOOL_14.permissionname = "Kiểm tra và xử lý HSBA lỗi trạng thái";
                TOOL_14.permissiontype = 2;
                TOOL_14.permissionnote = "Kiểm tra và xử lý HSBA lỗi trạng thái";
                lstresult.Add(TOOL_14);

                ClassCommon.classPermission TOOL_15 = new ClassCommon.classPermission();
                TOOL_15.permissioncheck = false;
                TOOL_15.permissioncode = "TOOL_15";
                TOOL_15.permissionname = "Kiểm tra đơn thuốc Nội trú chưa kết thúc khi đã đóng bệnh án";
                TOOL_15.permissiontype = 2;
                TOOL_15.permissionnote = "Kiểm tra đơn thuốc Nội trú chưa kết thúc khi đã đóng bệnh án";
                lstresult.Add(TOOL_15);

                ClassCommon.classPermission TOOL_16 = new ClassCommon.classPermission();
                TOOL_16.permissioncheck = false;
                TOOL_16.permissioncode = "TOOL_16";
                TOOL_16.permissionname = "Xử lý mã viện phí trắng (không có dịch vụ)";
                TOOL_16.permissiontype = 2;
                TOOL_16.permissionnote = "Xử lý mã viện phí trắng (không có dịch vụ)";
                lstresult.Add(TOOL_16);

                ClassCommon.classPermission TOOL_17 = new ClassCommon.classPermission();
                TOOL_17.permissioncheck = false;
                TOOL_17.permissioncode = "TOOL_17";
                TOOL_17.permissionname = "Chuyển đổi nhóm dịch vụ";
                TOOL_17.permissiontype = 2;
                TOOL_17.permissionnote = "Chuyển đổi nhóm dịch vụ";
                lstresult.Add(TOOL_17);

                // Phan quyen thao tac
                ClassCommon.classPermission THAOTAC_01 = new ClassCommon.classPermission();
                THAOTAC_01.permissioncheck = false;
                THAOTAC_01.permissioncode = "THAOTAC_01";
                THAOTAC_01.permissionname = "Sửa phiếu chỉ định dịch vụ - Sửa TG chỉ định/sử dụng";
                THAOTAC_01.permissiontype = 4;
                THAOTAC_01.permissionnote = "Sửa phiếu chỉ định dịch vụ - Sửa TG chỉ định/sử dụng";
                lstresult.Add(THAOTAC_01);

                ClassCommon.classPermission THAOTAC_02 = new ClassCommon.classPermission();
                THAOTAC_02.permissioncheck = false;
                THAOTAC_02.permissioncode = "THAOTAC_02";
                THAOTAC_02.permissionname = "Thiết lập khoảng thời gian lấy dữ liệu trong báo cáo";
                THAOTAC_02.permissiontype = 4;
                THAOTAC_02.permissionnote = "Thiết lập khoảng thời gian lấy dữ liệu trong báo cáo";
                lstresult.Add(THAOTAC_02);

                //report
                ClassCommon.classPermission REPORT_01 = new ClassCommon.classPermission();
                REPORT_01.permissioncheck = false;
                REPORT_01.permissioncode = "REPORT_01";
                REPORT_01.permissionname = "BC danh sách BN sử dụng dịch vụ...";
                REPORT_01.permissiontype = 3;
                REPORT_01.permissionnote = "BC danh sách BN sử dụng dịch vụ...";
                lstresult.Add(REPORT_01);

                ClassCommon.classPermission REPORT_02 = new ClassCommon.classPermission();
                REPORT_02.permissioncheck = false;
                REPORT_02.permissioncode = "REPORT_02";
                REPORT_02.permissionname = "Thống kê bệnh theo ICD10";
                REPORT_02.permissiontype = 3;
                REPORT_02.permissionnote = "Thống kê bệnh theo ICD10";
                lstresult.Add(REPORT_02);

                ClassCommon.classPermission REPORT_03 = new ClassCommon.classPermission();
                REPORT_03.permissioncheck = false;
                REPORT_03.permissioncode = "REPORT_03";
                REPORT_03.permissionname = "Tìm phiếu tổng hợp y lệnh";
                REPORT_03.permissiontype = 3;
                REPORT_03.permissionnote = "Tìm phiếu tổng hợp y lệnh";
                lstresult.Add(REPORT_03);

                ClassCommon.classPermission REPORT_04 = new ClassCommon.classPermission();
                REPORT_04.permissioncheck = false;
                REPORT_04.permissioncode = "REPORT_04";
                REPORT_04.permissionname = "BC chỉ định PTTT ";
                REPORT_04.permissiontype = 3;
                REPORT_04.permissionnote = "BC chỉ định PTTT ";
                lstresult.Add(REPORT_04);

                ClassCommon.classPermission REPORT_05 = new ClassCommon.classPermission();
                REPORT_05.permissioncheck = false;
                REPORT_05.permissionid = 22;
                REPORT_05.permissioncode = "REPORT_05";
                REPORT_05.permissionname = "BC BHYT 21 - chênh";
                REPORT_05.permissiontype = 3;
                REPORT_05.permissionnote = "BC BHYT 21 - chênh";
                lstresult.Add(REPORT_05);

                ClassCommon.classPermission REPORT_06 = new ClassCommon.classPermission();
                REPORT_06.permissioncheck = false;
                REPORT_06.permissioncode = "REPORT_06";
                REPORT_06.permissionname = "BC chi phí tăng thêm do thay đổi theo TT37 BHYT (BC CV1054 Hải Phòng)";
                REPORT_06.permissiontype = 3;
                REPORT_06.permissionnote = "BC chi phí tăng thêm do thay đổi theo TT37 BHYT (BC CV1054 Hải Phòng)";
                lstresult.Add(REPORT_06);

                ClassCommon.classPermission REPORT_07 = new ClassCommon.classPermission();
                REPORT_07.permissioncheck = false;
                REPORT_07.permissioncode = "REPORT_07";
                REPORT_07.permissionname = "Tìm dịch vụ/thuốc không có mã trong danh mục";
                REPORT_07.permissiontype = 3;
                REPORT_07.permissionnote = "Tìm dịch vụ/thuốc không có mã trong danh mục";
                lstresult.Add(REPORT_07);

                //Dashboard
                ClassCommon.classPermission REPORT_08 = new ClassCommon.classPermission();
                REPORT_08.permissioncheck = false;
                REPORT_08.permissioncode = "REPORT_08";
                REPORT_08.permissionname = "Dashboard BC quản lý tổng thể khoa";
                REPORT_08.permissiontype = 5;
                REPORT_08.permissionnote = "Dashboard BC quản lý tổng thể khoa. \n Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện";
                lstresult.Add(REPORT_08);

                ClassCommon.classPermission REPORT_09 = new ClassCommon.classPermission();
                REPORT_09.permissioncheck = false;
                REPORT_09.permissioncode = "REPORT_09";
                REPORT_09.permissionname = "Dashboard BC bệnh nhân nội trú";
                REPORT_09.permissiontype = 5;
                REPORT_09.permissionnote = "Dashboard BC bệnh nhân nội trú. \n Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện";
                lstresult.Add(REPORT_09);

                ClassCommon.classPermission REPORT_10 = new ClassCommon.classPermission();
                REPORT_10.permissioncheck = false;
                REPORT_10.permissioncode = "REPORT_10";
                REPORT_10.permissionname = "Dashboard BC bệnh nhân ngoại trú";
                REPORT_10.permissiontype = 5;
                REPORT_10.permissionnote = "Dashboard BC bệnh nhân ngoại trú. \n Lấy theo tiêu chí thời gian bệnh nhân đến khám; doanh thu chia theo khoa/phòng chỉ định";
                lstresult.Add(REPORT_10);

                ClassCommon.classPermission REPORT_11 = new ClassCommon.classPermission();
                REPORT_11.permissioncheck = false;
                REPORT_11.permissioncode = "REPORT_11";
                REPORT_11.permissionname = "Dashboard BC tổng hợp toàn viện";
                REPORT_11.permissiontype = 5;
                REPORT_11.permissionnote = "Dashboard BC tổng hợp toàn viện. \n Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện";
                lstresult.Add(REPORT_11);

                ClassCommon.classPermission REPORT_12 = new ClassCommon.classPermission();
                REPORT_12.permissioncheck = false;
                REPORT_12.permissioncode = "REPORT_12";
                REPORT_12.permissionname = "Dashboard BC doanh thu cận lâm sàng";
                REPORT_12.permissiontype = 5;
                REPORT_12.permissionnote = "Dashboard BC doanh thu cận lâm sàng. \n Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng chỉ định";
                lstresult.Add(REPORT_12);

                ClassCommon.classPermission REPORT_14 = new ClassCommon.classPermission();
                REPORT_14.permissioncheck = false;
                REPORT_14.permissioncode = "REPORT_14";
                REPORT_14.permissionname = "Dashboard BC xuất nhập tồn tủ trực";
                REPORT_14.permissiontype = 5;
                REPORT_14.permissionnote = "Dashboard BC xuất nhập tồn tủ trực";
                lstresult.Add(REPORT_14);

                ClassCommon.classPermission REPORT_15 = new ClassCommon.classPermission();
                REPORT_15.permissioncheck = false;
                REPORT_15.permissioncode = "REPORT_15";
                REPORT_15.permissionname = "BC bệnh nhân sử dụng thuốc/vật tư tại khoa";
                REPORT_15.permissiontype = 5;
                REPORT_15.permissionnote = "BC bệnh nhân sử dụng thuốc/vật tư tại khoa";
                lstresult.Add(REPORT_15);



                //
                ClassCommon.classPermission DASHBOARD_01 = new ClassCommon.classPermission();
                DASHBOARD_01.permissioncheck = false;
                DASHBOARD_01.permissioncode = "DASHBOARD_01";
                DASHBOARD_01.permissionname = "Biểu đồ doanh thu khoa";
                DASHBOARD_01.permissiontype = 5;
                DASHBOARD_01.permissionnote = "Biểu đồ doanh thu khoa";
                lstresult.Add(DASHBOARD_01);

                ClassCommon.classPermission DASHBOARD_02 = new ClassCommon.classPermission();
                DASHBOARD_02.permissioncheck = false;
                DASHBOARD_02.permissioncode = "DASHBOARD_02";
                DASHBOARD_02.permissionname = "Biểu đồ doanh thu theo khoa";
                DASHBOARD_02.permissiontype = 5;
                DASHBOARD_02.permissionnote = "Biểu đồ doanh thu theo khoa";
                lstresult.Add(DASHBOARD_02);

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            return lstresult;
        }
    }
}
