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
                //permissiontype = 3 report
                //permissiontype = 4 phan quyen thao tac
                //permissiontype = 5 Dashboard
                //permissiontype = 10 Bao cao in ra

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

                ClassCommon.classPermission SYS_06 = new ClassCommon.classPermission();
                SYS_06.permissioncheck = false;
                SYS_06.permissioncode = "SYS_06";
                SYS_06.permissionname = "Danh mục dùng chung";
                SYS_06.permissiontype = 1;
                SYS_06.permissionnote = "Danh mục dùng chung";
                lstresult.Add(SYS_06);

                ClassCommon.classPermission SYS_07 = new ClassCommon.classPermission();
                SYS_07.permissioncheck = false;
                SYS_07.permissioncode = "SYS_07";
                SYS_07.permissionname = "Danh mục cơ sở khám chữa bệnh";
                SYS_07.permissiontype = 1;
                SYS_07.permissionnote = "Danh mục cơ sở khám chữa bệnh";
                lstresult.Add(SYS_07);




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
                TOOL_08.permissionnote = "Sửa mã, tên, giá dịch vụ/thuốc của BN. Lấy theo thời gian chỉ định dịch vụ";
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

                ClassCommon.classPermission TOOL_18 = new ClassCommon.classPermission();
                TOOL_18.permissioncheck = false;
                TOOL_18.permissioncode = "TOOL_18";
                TOOL_18.permissionname = "Nhập thông tin thực hiện Cận lâm sàng";
                TOOL_18.permissiontype = 2;
                TOOL_18.permissionnote = "Nhập thông tin thực hiện Cận lâm sàng";
                lstresult.Add(TOOL_18);

                ClassCommon.classPermission TOOL_19 = new ClassCommon.classPermission();
                TOOL_19.permissioncheck = false;
                TOOL_19.permissioncode = "TOOL_19";
                TOOL_19.permissionname = "Cập nhật dịch vụ (đi kèm, thanh toán riêng)";
                TOOL_19.permissiontype = 2;
                TOOL_19.permissionnote = "Cập nhật dịch vụ (đi kèm, thanh toán riêng)";
                lstresult.Add(TOOL_19);

                ClassCommon.classPermission TOOL_20 = new ClassCommon.classPermission();
                TOOL_20.permissioncheck = false;
                TOOL_20.permissioncode = "TOOL_20";
                TOOL_20.permissionname = "Cập nhật tháng lương cơ bản của bệnh nhân";
                TOOL_20.permissiontype = 2;
                TOOL_20.permissionnote = "Cập nhật tháng lương cơ bản của bệnh nhân";
                lstresult.Add(TOOL_20);




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
                REPORT_04.permissionname = "BC chỉ định PTTT theo nhóm";
                REPORT_04.permissiontype = 3;
                REPORT_04.permissionnote = "BC chỉ định PTTT theo nhóm";
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
                REPORT_07.permissionnote = "Tìm dịch vụ/thuốc không có mã trong danh mục. Lấy theo thời gian chỉ định dịch vụ.";
                lstresult.Add(REPORT_07);

                ClassCommon.classPermission REPORT_08 = new ClassCommon.classPermission();
                REPORT_08.permissioncheck = false;
                REPORT_08.permissioncode = "REPORT_08";
                REPORT_08.permissionname = "Báo cáo phẫu thuật thủ thuật (doanh thu chia bác sĩ)";
                REPORT_08.permissiontype = 3;
                REPORT_08.permissionnote = "Báo cáo phẫu thuật thủ thuật (doanh thu chia bác sĩ)";
                lstresult.Add(REPORT_08);

                ClassCommon.classPermission REPORT_09 = new ClassCommon.classPermission();
                REPORT_09.permissioncheck = false;
                REPORT_09.permissioncode = "REPORT_09";
                REPORT_09.permissionname = "Báo cáo bệnh nhân sử dụng nhóm dịch vụ - Xuất ăn";
                REPORT_09.permissiontype = 3;
                REPORT_09.permissionnote = "Báo cáo bệnh nhân sử dụng nhóm dịch vụ - Xuất ăn";
                lstresult.Add(REPORT_09);

                ClassCommon.classPermission REPORT_10 = new ClassCommon.classPermission();
                REPORT_10.permissioncheck = false;
                REPORT_10.permissioncode = "REPORT_10";
                REPORT_10.permissionname = "Báo cáo thu tiền hàng ngày - Nhà thuốc";
                REPORT_10.permissiontype = 3;
                REPORT_10.permissionnote = "Báo cáo thu tiền hàng ngày - Nhà thuốc. Lấy theo thời gian xuất thuốc";
                lstresult.Add(REPORT_10);

                ClassCommon.classPermission REPORT_11 = new ClassCommon.classPermission();
                REPORT_11.permissioncheck = false;
                REPORT_11.permissioncode = "REPORT_11";
                REPORT_11.permissionname = "Báo cáo thuốc theo người kê - Nhà thuốc";
                REPORT_11.permissiontype = 3;
                REPORT_11.permissionnote = "Báo cáo thuốc theo người kê - Nhà thuốc. Lấy theo thời gian xuất thuốc";
                lstresult.Add(REPORT_11);

                ClassCommon.classPermission REPORT_12 = new ClassCommon.classPermission();
                REPORT_12.permissioncheck = false;
                REPORT_12.permissioncode = "REPORT_12";
                REPORT_12.permissionname = "Báo cáo thực hiện Cận lâm sàng (doanh thu chia bác sĩ)";
                REPORT_12.permissiontype = 3;
                REPORT_12.permissionnote = "Báo cáo thực hiện Cận lâm sàng (doanh thu chia bác sĩ)";
                lstresult.Add(REPORT_12);

                ClassCommon.classPermission REPORT_13 = new ClassCommon.classPermission();
                REPORT_13.permissioncheck = false;
                REPORT_13.permissioncode = "REPORT_13";
                REPORT_13.permissionname = "Sổ chuẩn đoán hình ảnh";
                REPORT_13.permissiontype = 3;
                REPORT_13.permissionnote = "Sổ chẩn đoán hình ảnh";
                lstresult.Add(REPORT_13);

                ClassCommon.classPermission REPORT_14 = new ClassCommon.classPermission();
                REPORT_14.permissioncheck = false;
                REPORT_14.permissioncode = "REPORT_14";
                REPORT_14.permissionname = "Báo cáo bệnh nhân sử dụng kết hợp dịch vụ";
                REPORT_14.permissiontype = 3;
                REPORT_14.permissionnote = "Báo cáo bệnh nhân sử dụng kết hợp dịch vụ";
                lstresult.Add(REPORT_14);

                ClassCommon.classPermission REPORT_15 = new ClassCommon.classPermission();
                REPORT_15.permissioncheck = false;
                REPORT_15.permissioncode = "REPORT_15";
                REPORT_15.permissionname = "Sổ xét nghiệm";
                REPORT_15.permissiontype = 3;
                REPORT_15.permissionnote = "Sổ xét nghiệm";
                lstresult.Add(REPORT_15);

                ClassCommon.classPermission REPORT_16 = new ClassCommon.classPermission();
                REPORT_16.permissioncheck = false;
                REPORT_16.permissioncode = "REPORT_16";
                REPORT_16.permissionname = "Báo cáo doanh thu theo máy xét nghiệm";
                REPORT_16.permissiontype = 3;
                REPORT_16.permissionnote = "Báo cáo doanh thu theo máy xét nghiệm";
                lstresult.Add(REPORT_16);

                ClassCommon.classPermission REPORT_17 = new ClassCommon.classPermission();
                REPORT_17.permissioncheck = false;
                REPORT_17.permissioncode = "REPORT_17";
                REPORT_17.permissionname = "Báo cáo phẫu thuật thủ thuật yêu cầu (doanh thu chia bác sĩ)";
                REPORT_17.permissiontype = 3;
                REPORT_17.permissionnote = "Báo cáo phẫu thuật thủ thuật yêu cầu (doanh thu chia bác sĩ)";
                lstresult.Add(REPORT_17);

                ClassCommon.classPermission REPORT_18 = new ClassCommon.classPermission();
                REPORT_18.permissioncheck = false;
                REPORT_18.permissioncode = "REPORT_18";
                REPORT_18.permissionname = "Báo cáo Sổ chi tiết bệnh nhân";
                REPORT_18.permissiontype = 3;
                REPORT_18.permissionnote = "Báo cáo Sổ chi tiết bệnh nhân";
                lstresult.Add(REPORT_18);

                ClassCommon.classPermission REPORT_19 = new ClassCommon.classPermission();
                REPORT_19.permissioncheck = false;
                REPORT_19.permissioncode = "REPORT_19";
                REPORT_19.permissionname = "Sổ thủ thuật";
                REPORT_19.permissiontype = 3;
                REPORT_19.permissionnote = "Sổ thủ thuật";
                lstresult.Add(REPORT_19);

                ClassCommon.classPermission REPORT_20 = new ClassCommon.classPermission();
                REPORT_20.permissioncheck = false;
                REPORT_20.permissioncode = "REPORT_20";
                REPORT_20.permissionname = "Báo cáo phẫu thuật thủ thuật yêu cầu QĐ 1055 (doanh thu chia bác sĩ)";
                REPORT_20.permissiontype = 3;
                REPORT_20.permissionnote = "Báo cáo phẫu thuật thủ thuật yêu cầu QĐ 1055 (doanh thu chia bác sĩ)";
                lstresult.Add(REPORT_20);

                ClassCommon.classPermission REPORT_21 = new ClassCommon.classPermission();
                REPORT_21.permissioncheck = false;
                REPORT_21.permissioncode = "REPORT_21";
                REPORT_21.permissionname = "Báo cáo sử dụng thuốc";
                REPORT_21.permissiontype = 3;
                REPORT_21.permissionnote = "Báo cáo sử dụng thuốc";
                lstresult.Add(REPORT_21);

                ClassCommon.classPermission REPORT_22 = new ClassCommon.classPermission();
                REPORT_22.permissioncheck = false;
                REPORT_22.permissioncode = "REPORT_22";
                REPORT_22.permissionname = "Bảng kê tổng hợp hóa đơn";
                REPORT_22.permissiontype = 3;
                REPORT_22.permissionnote = "Bảng kê tổng hợp hóa đơn";
                lstresult.Add(REPORT_22);

                ClassCommon.classPermission REPORT_23 = new ClassCommon.classPermission();
                REPORT_23.permissioncheck = false;
                REPORT_23.permissioncode = "REPORT_23";
                REPORT_23.permissionname = "Báo cáo doanh thu theo nhóm dịch vụ";
                REPORT_23.permissiontype = 3;
                REPORT_23.permissionnote = "Báo cáo doanh thu theo nhóm dịch vụ";
                lstresult.Add(REPORT_23);

                ClassCommon.classPermission REPORT_24 = new ClassCommon.classPermission();
                REPORT_24.permissioncheck = false;
                REPORT_24.permissioncode = "REPORT_24";
                REPORT_24.permissionname = "Báo cáo bệnh nhân thiếu tiền tạm ứng";
                REPORT_24.permissiontype = 3;
                REPORT_24.permissionnote = "Báo cáo bệnh nhân thiếu tiền tạm ứng";
                lstresult.Add(REPORT_24);




                //Dashboard
                ClassCommon.classPermission DASHBOARD_01 = new ClassCommon.classPermission();
                DASHBOARD_01.permissioncheck = false;
                DASHBOARD_01.permissioncode = "DASHBOARD_01";
                DASHBOARD_01.permissionname = "Dashboard BC quản lý tổng thể khoa";
                DASHBOARD_01.permissiontype = 5;
                DASHBOARD_01.permissionnote = "Dashboard BC quản lý tổng thể khoa. \n Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện";
                lstresult.Add(DASHBOARD_01);

                ClassCommon.classPermission DASHBOARD_02 = new ClassCommon.classPermission();
                DASHBOARD_02.permissioncheck = false;
                DASHBOARD_02.permissioncode = "DASHBOARD_02";
                DASHBOARD_02.permissionname = "Dashboard BC bệnh nhân nội trú";
                DASHBOARD_02.permissiontype = 5;
                DASHBOARD_02.permissionnote = "Dashboard BC bệnh nhân nội trú. \n Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện";
                lstresult.Add(DASHBOARD_02);

                ClassCommon.classPermission DASHBOARD_03 = new ClassCommon.classPermission();
                DASHBOARD_03.permissioncheck = false;
                DASHBOARD_03.permissioncode = "DASHBOARD_03";
                DASHBOARD_03.permissionname = "Dashboard BC bệnh nhân ngoại trú";
                DASHBOARD_03.permissiontype = 5;
                DASHBOARD_03.permissionnote = "Dashboard BC bệnh nhân ngoại trú. \n Lấy theo tiêu chí thời gian bệnh nhân đến khám; doanh thu chia theo khoa/phòng chỉ định";
                lstresult.Add(DASHBOARD_03);

                ClassCommon.classPermission DASHBOARD_04 = new ClassCommon.classPermission();
                DASHBOARD_04.permissioncheck = false;
                DASHBOARD_04.permissioncode = "DASHBOARD_04";
                DASHBOARD_04.permissionname = "Dashboard BC tổng hợp toàn viện";
                DASHBOARD_04.permissiontype = 5;
                DASHBOARD_04.permissionnote = "Dashboard BC tổng hợp toàn viện. \n Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện";
                lstresult.Add(DASHBOARD_04);

                ClassCommon.classPermission DASHBOARD_05 = new ClassCommon.classPermission();
                DASHBOARD_05.permissioncheck = false;
                DASHBOARD_05.permissioncode = "DASHBOARD_05";
                DASHBOARD_05.permissionname = "Dashboard BC doanh thu cận lâm sàng";
                DASHBOARD_05.permissiontype = 5;
                DASHBOARD_05.permissionnote = "Dashboard BC doanh thu cận lâm sàng. \n Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng chỉ định";
                lstresult.Add(DASHBOARD_05);

                ClassCommon.classPermission DASHBOARD_06 = new ClassCommon.classPermission();
                DASHBOARD_06.permissioncheck = false;
                DASHBOARD_06.permissioncode = "DASHBOARD_06";
                DASHBOARD_06.permissionname = "Dashboard BC xuất nhập tồn tủ trực";
                DASHBOARD_06.permissiontype = 5;
                DASHBOARD_06.permissionnote = "Dashboard BC xuất nhập tồn tủ trực";
                lstresult.Add(DASHBOARD_06);

                ClassCommon.classPermission DASHBOARD_07 = new ClassCommon.classPermission();
                DASHBOARD_07.permissioncheck = false;
                DASHBOARD_07.permissioncode = "DASHBOARD_07";
                DASHBOARD_07.permissionname = "BC bệnh nhân sử dụng thuốc/vật tư tại khoa";
                DASHBOARD_07.permissiontype = 5;
                DASHBOARD_07.permissionnote = "BC bệnh nhân sử dụng thuốc/vật tư tại khoa";
                lstresult.Add(DASHBOARD_07);

                //
                ClassCommon.classPermission DASHBOARD_08 = new ClassCommon.classPermission();
                DASHBOARD_08.permissioncheck = false;
                DASHBOARD_08.permissioncode = "DASHBOARD_08";
                DASHBOARD_08.permissionname = "Biểu đồ doanh thu khoa";
                DASHBOARD_08.permissiontype = 5;
                DASHBOARD_08.permissionnote = "Biểu đồ doanh thu khoa. Lấy theo tiêu chí khoa chỉ định dịch vụ";
                lstresult.Add(DASHBOARD_08);

                ClassCommon.classPermission DASHBOARD_09 = new ClassCommon.classPermission();
                DASHBOARD_09.permissioncheck = false;
                DASHBOARD_09.permissioncode = "DASHBOARD_09";
                DASHBOARD_09.permissionname = "Biểu đồ doanh thu theo khoa";
                DASHBOARD_09.permissiontype = 5;
                DASHBOARD_09.permissionnote = "Biểu đồ doanh thu theo khoa";
                lstresult.Add(DASHBOARD_09);

                ClassCommon.classPermission DASHBOARD_10 = new ClassCommon.classPermission();
                DASHBOARD_10.permissioncheck = false;
                DASHBOARD_10.permissioncode = "DASHBOARD_10";
                DASHBOARD_10.permissionname = "Báo cáo tổng hợp doanh thu khoa - toàn viện";
                DASHBOARD_10.permissiontype = 5;
                DASHBOARD_10.permissionnote = "Báo cáo tổng hợp doanh thu khoa - toàn viện. Doanh thu chia theo khoa/phòng chỉ định";
                lstresult.Add(DASHBOARD_10);

                ClassCommon.classPermission DASHBOARD_11 = new ClassCommon.classPermission();
                DASHBOARD_11.permissioncheck = false;
                DASHBOARD_11.permissioncode = "DASHBOARD_11";
                DASHBOARD_11.permissionname = "BC BN sử dụng thuốc theo nhóm \"Hạn chế sử dụng\" - Theo khoa";
                DASHBOARD_11.permissiontype = 5;
                DASHBOARD_11.permissionnote = "BC BN sử dụng thuốc theo nhóm \"Hạn chế sử dụng\" - Theo khoa";
                lstresult.Add(DASHBOARD_11);

                ClassCommon.classPermission DASHBOARD_12 = new ClassCommon.classPermission();
                DASHBOARD_12.permissioncheck = false;
                DASHBOARD_12.permissioncode = "DASHBOARD_12";
                DASHBOARD_12.permissionname = "BC BN sử dụng thuốc theo nhóm \"Hạn chế sử dụng\" - Tổng hợp";
                DASHBOARD_12.permissiontype = 5;
                DASHBOARD_12.permissionnote = "BC BN sử dụng thuốc theo nhóm \"Hạn chế sử dụng\" - Tổng hợp";
                lstresult.Add(DASHBOARD_12);




                //Bao cao in ra
                ClassCommon.classPermission BAOCAO_001 = new ClassCommon.classPermission();
                BAOCAO_001.permissioncheck = false;
                BAOCAO_001.permissioncode = "BAOCAO_001";
                BAOCAO_001.permissionname = "Báo cáo Phẫu thuật - Khoa Gây mê hồi tỉnh";
                BAOCAO_001.permissiontype = 10;
                BAOCAO_001.permissionnote = "Báo cáo Phẫu thuật - Khoa Gây mê hồi tỉnh";
                lstresult.Add(BAOCAO_001);

                ClassCommon.classPermission BAOCAO_002 = new ClassCommon.classPermission();
                BAOCAO_002.permissioncheck = false;
                BAOCAO_002.permissioncode = "BAOCAO_002";
                BAOCAO_002.permissionname = "Báo cáo Phẫu thuật - Khoa Tai mũi họng";
                BAOCAO_002.permissiontype = 10;
                BAOCAO_002.permissionnote = "Báo cáo Phẫu thuật - Khoa Tai mũi họng";
                lstresult.Add(BAOCAO_002);

                ClassCommon.classPermission BAOCAO_003 = new ClassCommon.classPermission();
                BAOCAO_003.permissioncheck = false;
                BAOCAO_003.permissioncode = "BAOCAO_003";
                BAOCAO_003.permissionname = "Báo cáo Phẫu thuật - Khoa Răng hàm mặt";
                BAOCAO_003.permissiontype = 10;
                BAOCAO_003.permissionnote = "Báo cáo Phẫu thuật - Khoa Răng hàm mặt";
                lstresult.Add(BAOCAO_003);

                ClassCommon.classPermission BAOCAO_004 = new ClassCommon.classPermission();
                BAOCAO_004.permissioncheck = false;
                BAOCAO_004.permissioncode = "BAOCAO_004";
                BAOCAO_004.permissionname = "Báo cáo Phẫu thuật - Khoa Mắt";
                BAOCAO_004.permissiontype = 10;
                BAOCAO_004.permissionnote = "Báo cáo Phẫu thuật - Khoa Mắt";
                lstresult.Add(BAOCAO_004);

                ClassCommon.classPermission BAOCAO_005 = new ClassCommon.classPermission();
                BAOCAO_005.permissioncheck = false;
                BAOCAO_005.permissioncode = "BAOCAO_005";
                BAOCAO_005.permissionname = "Báo cáo Phẫu thuật - Chung";
                BAOCAO_005.permissiontype = 10;
                BAOCAO_005.permissionnote = "Báo cáo Phẫu thuật - Chung";
                lstresult.Add(BAOCAO_005);

                ClassCommon.classPermission BAOCAO_006 = new ClassCommon.classPermission();
                BAOCAO_006.permissioncheck = false;
                BAOCAO_006.permissioncode = "BAOCAO_006";
                BAOCAO_006.permissionname = "Báo cáo Thủ thuật - Khoa Mắt";
                BAOCAO_006.permissiontype = 10;
                BAOCAO_006.permissionnote = "Báo cáo Thủ thuật - Khoa Mắt";
                lstresult.Add(BAOCAO_006);

                ClassCommon.classPermission BAOCAO_007 = new ClassCommon.classPermission();
                BAOCAO_007.permissioncheck = false;
                BAOCAO_007.permissioncode = "BAOCAO_007";
                BAOCAO_007.permissionname = "Báo cáo Thủ thuật - Các khoa khác (trừ khoa mắt & PK mắt)";
                BAOCAO_007.permissiontype = 10;
                BAOCAO_007.permissionnote = "Báo cáo Thủ thuật - Các khoa khác (trừ khoa mắt & PK mắt)";
                lstresult.Add(BAOCAO_007);

                ClassCommon.classPermission BAOCAO_008 = new ClassCommon.classPermission();
                BAOCAO_008.permissioncheck = false;
                BAOCAO_008.permissioncode = "BAOCAO_008";
                BAOCAO_008.permissionname = "Báo cáo Thủ thuật - Chung";
                BAOCAO_008.permissiontype = 10;
                BAOCAO_008.permissionnote = "Báo cáo Thủ thuật - Chung";
                lstresult.Add(BAOCAO_008);

                ClassCommon.classPermission BAOCAO_009 = new ClassCommon.classPermission();
                BAOCAO_009.permissioncheck = false;
                BAOCAO_009.permissioncode = "BAOCAO_009";
                BAOCAO_009.permissionname = "Báo cáo Thủ thuật Nội soi dạ dày";
                BAOCAO_009.permissiontype = 10;
                BAOCAO_009.permissionnote = "Báo cáo Thủ thuật Nội soi dạ dày";
                lstresult.Add(BAOCAO_009);


            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            return lstresult;
        }
    }
}
