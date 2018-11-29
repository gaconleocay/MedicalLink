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

                //tabMenuId: // 1=Trang chu; 2=chuc nang; 3=Dashboard; 4=BC QL tai chinh; 5=báo cáo thường ; 6=báo cáo doanh thu

                //System + trang chu
                ClassCommon.classPermission SYS_01 = new ClassCommon.classPermission();
                SYS_01.permissioncheck = false;
                SYS_01.permissioncode = "SYS_01";
                SYS_01.permissionname = "Kết nối cơ sở dữ liệu";
                SYS_01.permissiontype = 1;
                SYS_01.tabMenuId = 1;
                SYS_01.permissionnote = "Kết nối cơ sở dữ liệu";
                lstresult.Add(SYS_01);

                ClassCommon.classPermission SYS_02 = new ClassCommon.classPermission();
                SYS_02.permissioncheck = false;
                SYS_02.permissioncode = "SYS_02";
                SYS_02.permissionname = "Quản lý người dùng";
                SYS_02.permissiontype = 1;
                SYS_02.tabMenuId = 1;
                SYS_02.permissionnote = "Quản lý người dùng";
                lstresult.Add(SYS_02);

                ClassCommon.classPermission SYS_03 = new ClassCommon.classPermission();
                SYS_03.permissioncheck = false;
                SYS_03.permissioncode = "SYS_03";
                SYS_03.permissionname = "Danh sách nhân viên";
                SYS_03.permissiontype = 1;
                SYS_03.tabMenuId = 1;
                SYS_03.permissionnote = "Danh sách nhân viên";
                lstresult.Add(SYS_03);

                ClassCommon.classPermission SYS_04 = new ClassCommon.classPermission();
                SYS_04.permissioncheck = false;
                SYS_04.permissioncode = "SYS_04";
                SYS_04.permissionname = "Danh sách option";
                SYS_04.permissiontype = 1;
                SYS_04.tabMenuId = 1;
                SYS_04.permissionnote = "Danh sách option";
                lstresult.Add(SYS_04);

                ClassCommon.classPermission SYS_05 = new ClassCommon.classPermission();
                SYS_05.permissioncheck = false;
                SYS_05.permissioncode = "SYS_05";
                SYS_05.permissionname = "Quản trị hệ thống";
                SYS_05.permissiontype = 1;
                SYS_05.tabMenuId = 1;
                SYS_05.permissionnote = "Quản trị hệ thống";
                lstresult.Add(SYS_05);

                ClassCommon.classPermission SYS_06 = new ClassCommon.classPermission();
                SYS_06.permissioncheck = false;
                SYS_06.permissioncode = "SYS_06";
                SYS_06.permissionname = "Danh mục dùng chung";
                SYS_06.permissiontype = 1;
                SYS_06.tabMenuId = 1;
                SYS_06.permissionnote = "Danh mục dùng chung";
                lstresult.Add(SYS_06);

                ClassCommon.classPermission SYS_07 = new ClassCommon.classPermission();
                SYS_07.permissioncheck = false;
                SYS_07.permissioncode = "SYS_07";
                SYS_07.permissionname = "Danh mục cơ sở khám chữa bệnh";
                SYS_07.permissiontype = 1;
                SYS_07.tabMenuId = 1;
                SYS_07.permissionnote = "Danh mục cơ sở khám chữa bệnh";
                lstresult.Add(SYS_07);

                ClassCommon.classPermission SYS_08 = new ClassCommon.classPermission();
                SYS_08.permissioncheck = false;
                SYS_08.permissioncode = "SYS_08";
                SYS_08.permissionname = "Danh mục dịch vụ - cấu hình sổ xét nghiệm";
                SYS_08.permissiontype = 1;
                SYS_08.tabMenuId = 1;
                SYS_08.permissionnote = "Danh mục dịch vụ - cấu hình sổ xét nghiệm";
                lstresult.Add(SYS_08);

                ClassCommon.classPermission SYS_09 = new ClassCommon.classPermission();
                SYS_09.permissioncheck = false;
                SYS_09.permissioncode = "SYS_09";
                SYS_09.permissionname = "Cấu hình hao phí máy xét nghiệm";
                SYS_09.permissiontype = 1;
                SYS_09.tabMenuId = 1;
                SYS_09.permissionnote = "Cấu hình hao phí máy xét nghiệm";
                lstresult.Add(SYS_09);



                //Tools + tabMenuId = 2; chuc nang
                ClassCommon.classPermission TOOL_01 = new ClassCommon.classPermission();
                TOOL_01.permissioncheck = false;
                TOOL_01.permissioncode = "TOOL_01";
                TOOL_01.permissionname = "Sửa thời gian ra viện";
                TOOL_01.permissiontype = 2;
                TOOL_01.tabMenuId = 2;
                TOOL_01.permissionnote = "Sửa thời gian ra viện";
                lstresult.Add(TOOL_01);

                ClassCommon.classPermission TOOL_02 = new ClassCommon.classPermission();
                TOOL_02.permissioncheck = false;
                TOOL_02.permissioncode = "TOOL_02";
                TOOL_02.permissionname = "Chuyển tiền tạm ứng";
                TOOL_02.permissiontype = 2;
                TOOL_02.tabMenuId = 2;
                TOOL_02.permissionnote = "Chuyển tiền tạm ứng";
                lstresult.Add(TOOL_02);

                ClassCommon.classPermission TOOL_03 = new ClassCommon.classPermission();
                TOOL_03.permissioncheck = false;
                TOOL_03.permissioncode = "TOOL_03";
                TOOL_03.permissionname = "Mở bệnh án";
                TOOL_03.permissiontype = 2;
                TOOL_03.tabMenuId = 2;
                TOOL_03.permissionnote = "Mở bệnh án";
                lstresult.Add(TOOL_03);

                ClassCommon.classPermission TOOL_04 = new ClassCommon.classPermission();
                TOOL_04.permissioncheck = false;
                TOOL_04.permissioncode = "TOOL_04";
                TOOL_04.permissionname = "Sửa ngày duyệt kế toán";
                TOOL_04.permissiontype = 2;
                TOOL_04.tabMenuId = 2;
                TOOL_04.permissionnote = "Sửa ngày duyệt kế toán";
                lstresult.Add(TOOL_04);

                ClassCommon.classPermission TOOL_05 = new ClassCommon.classPermission();
                TOOL_05.permissioncheck = false;
                TOOL_05.permissioncode = "TOOL_05";
                TOOL_05.permissionname = "Xử lý bệnh nhân bỏ khoa";
                TOOL_05.permissiontype = 2;
                TOOL_05.tabMenuId = 2;
                TOOL_05.permissionnote = "Xử lý bệnh nhân bỏ khoa";
                lstresult.Add(TOOL_05);

                ClassCommon.classPermission TOOL_06 = new ClassCommon.classPermission();
                TOOL_06.permissioncheck = false;
                TOOL_06.permissioncode = "TOOL_06";
                TOOL_06.permissionname = "Update danh mục thuốc";
                TOOL_06.permissiontype = 2;
                TOOL_06.tabMenuId = 2;
                TOOL_06.permissionnote = "Update danh mục thuốc";
                lstresult.Add(TOOL_06);

                ClassCommon.classPermission TOOL_07 = new ClassCommon.classPermission();
                TOOL_07.permissioncheck = false;
                TOOL_07.permissioncode = "TOOL_07";
                TOOL_07.permissionname = "Update danh mục dịch vụ";
                TOOL_07.permissiontype = 2;
                TOOL_07.tabMenuId = 2;
                TOOL_07.permissionnote = "Update danh mục dịch vụ";
                lstresult.Add(TOOL_07);

                ClassCommon.classPermission TOOL_08 = new ClassCommon.classPermission();
                TOOL_08.permissioncheck = false;
                TOOL_08.permissioncode = "TOOL_08";
                TOOL_08.permissionname = "Sửa mã, tên, giá dịch vụ/thuốc của BN";
                TOOL_08.permissiontype = 2;
                TOOL_08.tabMenuId = 2;
                TOOL_08.permissionnote = "Sửa mã, tên, giá dịch vụ/thuốc của BN. Lấy theo thời gian chỉ định dịch vụ";
                lstresult.Add(TOOL_08);

                ClassCommon.classPermission TOOL_09 = new ClassCommon.classPermission();
                TOOL_09.permissioncheck = false;
                TOOL_09.permissioncode = "TOOL_09";
                TOOL_09.permissionname = "Update data table Serviceprice";
                TOOL_09.permissiontype = 2;
                TOOL_09.tabMenuId = 2;
                TOOL_09.permissionnote = "Update data table Serviceprice";
                lstresult.Add(TOOL_09);

                ClassCommon.classPermission TOOL_10 = new ClassCommon.classPermission();
                TOOL_10.permissioncheck = false;
                TOOL_10.permissioncode = "TOOL_10";
                TOOL_10.permissionname = "Chạy update khả dụng-tồn kho";
                TOOL_10.permissiontype = 2;
                TOOL_10.tabMenuId = 2;
                TOOL_10.permissionnote = "Chạy update khả dụng-tồn kho";
                lstresult.Add(TOOL_10);

                ClassCommon.classPermission TOOL_11 = new ClassCommon.classPermission();
                TOOL_11.permissioncheck = false;
                TOOL_11.permissioncode = "TOOL_11";
                TOOL_11.permissionname = "Sửa phơi thanh toán của bệnh nhân";
                TOOL_11.permissiontype = 2;
                TOOL_11.tabMenuId = 2;
                TOOL_11.permissionnote = "Sửa phơi thanh toán của bệnh nhân";
                lstresult.Add(TOOL_11);

                ClassCommon.classPermission TOOL_12 = new ClassCommon.classPermission();
                TOOL_12.permissioncheck = false;
                TOOL_12.permissioncode = "TOOL_12";
                TOOL_12.permissionname = "Sửa phiếu chỉ định dịch vụ";
                TOOL_12.permissiontype = 2;
                TOOL_12.tabMenuId = 2;
                TOOL_12.permissionnote = "Sửa phiếu chỉ định dịch vụ; Xóa đơn thuốc kê tủ trực";
                lstresult.Add(TOOL_12);

                ClassCommon.classPermission TOOL_13 = new ClassCommon.classPermission();
                TOOL_13.permissioncheck = false;
                TOOL_13.permissioncode = "TOOL_13";
                TOOL_13.permissionname = "Sửa thông tin bệnh án";
                TOOL_13.permissiontype = 2;
                TOOL_13.tabMenuId = 2;
                TOOL_13.permissionnote = "Sửa thông tin bệnh án";
                lstresult.Add(TOOL_13);

                ClassCommon.classPermission TOOL_14 = new ClassCommon.classPermission();
                TOOL_14.permissioncheck = false;
                TOOL_14.permissioncode = "TOOL_14";
                TOOL_14.permissionname = "Kiểm tra và xử lý HSBA lỗi trạng thái";
                TOOL_14.permissiontype = 2;
                TOOL_14.tabMenuId = 2;
                TOOL_14.permissionnote = "Kiểm tra và xử lý HSBA lỗi trạng thái";
                lstresult.Add(TOOL_14);

                ClassCommon.classPermission TOOL_15 = new ClassCommon.classPermission();
                TOOL_15.permissioncheck = false;
                TOOL_15.permissioncode = "TOOL_15";
                TOOL_15.permissionname = "Kiểm tra đơn thuốc Nội trú chưa kết thúc khi đã đóng bệnh án";
                TOOL_15.permissiontype = 2;
                TOOL_15.tabMenuId = 2;
                TOOL_15.permissionnote = "Kiểm tra đơn thuốc Nội trú chưa kết thúc khi đã đóng bệnh án";
                lstresult.Add(TOOL_15);

                ClassCommon.classPermission TOOL_16 = new ClassCommon.classPermission();
                TOOL_16.permissioncheck = false;
                TOOL_16.permissioncode = "TOOL_16";
                TOOL_16.permissionname = "Xử lý mã viện phí trắng (không có dịch vụ)";
                TOOL_16.permissiontype = 2;
                TOOL_16.tabMenuId = 2;
                TOOL_16.permissionnote = "Xử lý mã viện phí trắng (không có dịch vụ)";
                lstresult.Add(TOOL_16);

                ClassCommon.classPermission TOOL_17 = new ClassCommon.classPermission();
                TOOL_17.permissioncheck = false;
                TOOL_17.permissioncode = "TOOL_17";
                TOOL_17.permissionname = "Chuyển đổi nhóm dịch vụ";
                TOOL_17.permissiontype = 2;
                TOOL_17.tabMenuId = 2;
                TOOL_17.permissionnote = "Chuyển đổi nhóm dịch vụ";
                lstresult.Add(TOOL_17);

                ClassCommon.classPermission TOOL_18 = new ClassCommon.classPermission();
                TOOL_18.permissioncheck = false;
                TOOL_18.permissioncode = "TOOL_18";
                TOOL_18.permissionname = "Nhập thông tin thực hiện Cận lâm sàng";
                TOOL_18.permissiontype = 2;
                TOOL_18.tabMenuId = 2;
                TOOL_18.permissionnote = "Nhập thông tin thực hiện Cận lâm sàng";
                lstresult.Add(TOOL_18);

                ClassCommon.classPermission TOOL_19 = new ClassCommon.classPermission();
                TOOL_19.permissioncheck = false;
                TOOL_19.permissioncode = "TOOL_19";
                TOOL_19.permissionname = "Cập nhật dịch vụ (đi kèm, thanh toán riêng)";
                TOOL_19.permissiontype = 2;
                TOOL_19.tabMenuId = 2;
                TOOL_19.permissionnote = "Cập nhật dịch vụ (đi kèm, thanh toán riêng)";
                lstresult.Add(TOOL_19);

                ClassCommon.classPermission TOOL_20 = new ClassCommon.classPermission();
                TOOL_20.permissioncheck = false;
                TOOL_20.permissioncode = "TOOL_20";
                TOOL_20.permissionname = "Cập nhật tháng lương cơ bản của bệnh nhân";
                TOOL_20.permissiontype = 2;
                TOOL_20.tabMenuId = 2;
                TOOL_20.permissionnote = "Cập nhật tháng lương cơ bản của bệnh nhân";
                lstresult.Add(TOOL_20);

                ClassCommon.classPermission TOOL_21 = new ClassCommon.classPermission();
                TOOL_21.permissioncheck = false;
                TOOL_21.permissioncode = "TOOL_21";
                TOOL_21.permissionname = "Sửa hóa đơn tạm ứng";
                TOOL_21.permissiontype = 2;
                TOOL_21.tabMenuId = 2;
                TOOL_21.permissionnote = "Sửa hóa đơn tạm ứng";
                lstresult.Add(TOOL_21);

                ClassCommon.classPermission TOOL_22 = new ClassCommon.classPermission();
                TOOL_22.permissioncheck = false;
                TOOL_22.permissioncode = "TOOL_22";
                TOOL_22.permissionname = "Gộp bệnh án";
                TOOL_22.permissiontype = 2;
                TOOL_22.tabMenuId = 2;
                TOOL_22.permissionnote = "Gộp bệnh án";
                lstresult.Add(TOOL_22);

                ClassCommon.classPermission TOOL_23 = new ClassCommon.classPermission();
                TOOL_23.permissioncheck = false;
                TOOL_23.permissioncode = "TOOL_23";
                TOOL_23.permissionname = "Cập nhật thuốc phát miễn phí";
                TOOL_23.permissiontype = 2;
                TOOL_23.tabMenuId = 2;
                TOOL_23.permissionnote = "Cập nhật thuốc phát miễn phí";
                lstresult.Add(TOOL_23);

                ClassCommon.classPermission TOOL_24 = new ClassCommon.classPermission();
                TOOL_24.permissioncheck = false;
                TOOL_24.permissioncode = "TOOL_24";
                TOOL_24.permissionname = "Cập nhật loại hình thanh toán theo dịch vụ/thuốc";
                TOOL_24.permissiontype = 2;
                TOOL_24.tabMenuId = 2;
                TOOL_24.permissionnote = "Cập nhật loại hình thanh toán theo dịch vụ/thuốc";
                lstresult.Add(TOOL_24);

                ClassCommon.classPermission TOOL_25 = new ClassCommon.classPermission();
                TOOL_25.permissioncheck = false;
                TOOL_25.permissioncode = "TOOL_25";
                TOOL_25.permissionname = "Đóng hồ sơ bệnh án và duyệt viện phí";
                TOOL_25.permissiontype = 2;
                TOOL_25.tabMenuId = 2;
                TOOL_25.permissionnote = "Đóng HSBA BN Viện phí ngoại trú";
                lstresult.Add(TOOL_25);

                ClassCommon.classPermission TOOL_26 = new ClassCommon.classPermission();
                TOOL_26.permissioncheck = false;
                TOOL_26.permissioncode = "TOOL_26";
                TOOL_26.permissionname = "Cập nhật hạn sử dụng thuốc/vật tư";
                TOOL_26.permissiontype = 2;
                TOOL_26.tabMenuId = 2;
                TOOL_26.permissionnote = "Cập nhật hạn sử dụng thuốc/vật tư";
                lstresult.Add(TOOL_26);

                ClassCommon.classPermission TOOL_27 = new ClassCommon.classPermission();
                TOOL_27.permissioncheck = false;
                TOOL_27.permissioncode = "TOOL_27";
                TOOL_27.permissionname = "Kiểm tra số lượng kết nối đến CSDL";
                TOOL_27.permissiontype = 2;
                TOOL_27.tabMenuId = 2;
                TOOL_27.permissionnote = "Kiểm tra số lượng kết nối đến CSDL";
                lstresult.Add(TOOL_27);


                // Phan quyen thao tac + chuc nang
                ClassCommon.classPermission THAOTAC_01 = new ClassCommon.classPermission();
                THAOTAC_01.permissioncheck = false;
                THAOTAC_01.permissioncode = "THAOTAC_01";
                THAOTAC_01.permissionname = "Sửa phiếu chỉ định dịch vụ - Sửa TG chỉ định/sử dụng/trả kết quả";
                THAOTAC_01.permissiontype = 4;
                THAOTAC_01.tabMenuId = 2;
                THAOTAC_01.permissionnote = "Sửa phiếu chỉ định dịch vụ - Sửa TG chỉ định/sử dụng/trả kết quả";
                lstresult.Add(THAOTAC_01);

                ClassCommon.classPermission THAOTAC_02 = new ClassCommon.classPermission();
                THAOTAC_02.permissioncheck = false;
                THAOTAC_02.permissioncode = "THAOTAC_02";
                THAOTAC_02.permissionname = "Thiết lập khoảng thời gian lấy dữ liệu trong báo cáo";
                THAOTAC_02.permissiontype = 4;
                THAOTAC_02.tabMenuId = 2;
                THAOTAC_02.permissionnote = "Thiết lập khoảng thời gian lấy dữ liệu trong báo cáo";
                lstresult.Add(THAOTAC_02);

                ClassCommon.classPermission THAOTAC_03 = new ClassCommon.classPermission();
                THAOTAC_03.permissioncheck = false;
                THAOTAC_03.permissioncode = "THAOTAC_03";
                THAOTAC_03.permissionname = "Sửa phiếu chỉ định dịch vụ - Sửa TG chỉ định/sử dụng/trả kết quả (đã ra viện)";
                THAOTAC_03.permissiontype = 4;
                THAOTAC_03.tabMenuId = 2;
                THAOTAC_03.permissionnote = "Sửa phiếu chỉ định dịch vụ - Sửa TG chỉ định/sử dụng/trả kết quả (đã ra viện)";
                lstresult.Add(THAOTAC_03);

                ClassCommon.classPermission THAOTAC_04 = new ClassCommon.classPermission();
                THAOTAC_04.permissioncheck = false;
                THAOTAC_04.permissioncode = "THAOTAC_04";
                THAOTAC_04.permissionname = "Thêm mới và cập nhật nhân sự";
                THAOTAC_04.permissiontype = 4;
                THAOTAC_04.tabMenuId = 2;
                THAOTAC_04.permissionnote = "Thêm mới và cập nhật nhân sự";
                lstresult.Add(THAOTAC_04);

                ClassCommon.classPermission THAOTAC_05 = new ClassCommon.classPermission();
                THAOTAC_05.permissioncheck = false;
                THAOTAC_05.permissioncode = "THAOTAC_05";
                THAOTAC_05.permissionname = "Duyệt giám định phẫu thuật thủ thuật";
                THAOTAC_05.permissiontype = 4;
                THAOTAC_05.tabMenuId = 2;
                THAOTAC_05.permissionnote = "Duyệt giám định phẫu thuật thủ thuật";
                lstresult.Add(THAOTAC_05);

                ClassCommon.classPermission THAOTAC_06 = new ClassCommon.classPermission();
                THAOTAC_06.permissioncheck = false;
                THAOTAC_06.permissioncode = "THAOTAC_06";
                THAOTAC_06.permissionname = "In và xuất excel khi chưa duyệt PTTT";
                THAOTAC_06.permissiontype = 4;
                THAOTAC_06.tabMenuId = 2;
                THAOTAC_06.permissionnote = "In và xuất excel khi chưa duyệt PTTT";
                lstresult.Add(THAOTAC_06);

                ClassCommon.classPermission THAOTAC_07 = new ClassCommon.classPermission();
                THAOTAC_07.permissioncheck = false;
                THAOTAC_07.permissioncode = "THAOTAC_07";
                THAOTAC_07.permissionname = "Xuất file excel";
                THAOTAC_07.permissiontype = 4;
                THAOTAC_07.tabMenuId = 2;
                THAOTAC_07.permissionnote = "Xuất file excel";
                lstresult.Add(THAOTAC_07);


                //Báo cáo thường type=3 = tabmeniid = 2
                ClassCommon.classPermission REPORT_01 = new ClassCommon.classPermission();
                REPORT_01.permissioncheck = false;
                REPORT_01.permissioncode = "REPORT_01";
                REPORT_01.permissionname = "BC danh sách BN sử dụng dịch vụ...";
                REPORT_01.permissiontype = 3;
                REPORT_01.tabMenuId = 5;
                REPORT_01.permissionnote = "BC danh sách BN sử dụng dịch vụ...";
                lstresult.Add(REPORT_01);

                ClassCommon.classPermission REPORT_02 = new ClassCommon.classPermission();
                REPORT_02.permissioncheck = false;
                REPORT_02.permissioncode = "REPORT_02";
                REPORT_02.permissionname = "Thống kê bệnh theo ICD10";
                REPORT_02.permissiontype = 3;
                REPORT_02.tabMenuId = 5;
                REPORT_02.permissionnote = "Thống kê bệnh theo ICD10";
                lstresult.Add(REPORT_02);

                ClassCommon.classPermission REPORT_03 = new ClassCommon.classPermission();
                REPORT_03.permissioncheck = false;
                REPORT_03.permissioncode = "REPORT_03";
                REPORT_03.permissionname = "Tìm phiếu tổng hợp y lệnh";
                REPORT_03.permissiontype = 3;
                REPORT_03.tabMenuId = 5;
                REPORT_03.permissionnote = "Tìm phiếu tổng hợp y lệnh";
                lstresult.Add(REPORT_03);

                ClassCommon.classPermission REPORT_04 = new ClassCommon.classPermission();
                REPORT_04.permissioncheck = false;
                REPORT_04.permissioncode = "REPORT_04";
                REPORT_04.permissionname = "BC chỉ định PTTT theo nhóm";
                REPORT_04.permissiontype = 3;
                REPORT_04.tabMenuId = 5;
                REPORT_04.permissionnote = "BC chỉ định PTTT theo nhóm";
                lstresult.Add(REPORT_04);

                ClassCommon.classPermission REPORT_05 = new ClassCommon.classPermission();
                REPORT_05.permissioncheck = false;
                REPORT_05.permissionid = 22;
                REPORT_05.permissioncode = "REPORT_05";
                REPORT_05.permissionname = "BC BHYT 21 - chênh";
                REPORT_05.permissiontype = 3;
                REPORT_05.tabMenuId = 5;
                REPORT_05.permissionnote = "BC BHYT 21 - chênh";
                lstresult.Add(REPORT_05);

                ClassCommon.classPermission REPORT_06 = new ClassCommon.classPermission();
                REPORT_06.permissioncheck = false;
                REPORT_06.permissioncode = "REPORT_06";
                REPORT_06.permissionname = "BC chi phí tăng thêm do thay đổi theo TT37 BHYT (BC CV1054 Hải Phòng)";
                REPORT_06.permissiontype = 3;
                REPORT_06.tabMenuId = 5;
                REPORT_06.permissionnote = "BC chi phí tăng thêm do thay đổi theo TT37 BHYT (BC CV1054 Hải Phòng)";
                lstresult.Add(REPORT_06);

                ClassCommon.classPermission REPORT_07 = new ClassCommon.classPermission();
                REPORT_07.permissioncheck = false;
                REPORT_07.permissioncode = "REPORT_07";
                REPORT_07.permissionname = "Tìm dịch vụ/thuốc không có mã trong danh mục";
                REPORT_07.permissiontype = 3;
                REPORT_07.tabMenuId = 5;
                REPORT_07.permissionnote = "Tìm dịch vụ/thuốc không có mã trong danh mục. Lấy theo thời gian chỉ định dịch vụ.";
                lstresult.Add(REPORT_07);

                ClassCommon.classPermission REPORT_08 = new ClassCommon.classPermission();
                REPORT_08.permissioncheck = false;
                REPORT_08.permissioncode = "REPORT_08";
                REPORT_08.permissionname = "Báo cáo phẫu thuật thủ thuật (doanh thu chia bác sĩ)";
                REPORT_08.permissiontype = 3;
                REPORT_08.tabMenuId = 5;
                REPORT_08.permissionnote = "Báo cáo phẫu thuật thủ thuật (doanh thu chia bác sĩ)";
                lstresult.Add(REPORT_08);

                ClassCommon.classPermission REPORT_09 = new ClassCommon.classPermission();
                REPORT_09.permissioncheck = false;
                REPORT_09.permissioncode = "REPORT_09";
                REPORT_09.permissionname = "Báo cáo bệnh nhân sử dụng nhóm dịch vụ - Xuất ăn";
                REPORT_09.permissiontype = 3;
                REPORT_09.tabMenuId = 5;
                REPORT_09.permissionnote = "Báo cáo bệnh nhân sử dụng nhóm dịch vụ - Xuất ăn";
                lstresult.Add(REPORT_09);

                ClassCommon.classPermission REPORT_10 = new ClassCommon.classPermission();
                REPORT_10.permissioncheck = false;
                REPORT_10.permissioncode = "REPORT_10";
                REPORT_10.permissionname = "Báo cáo thu tiền hàng ngày - Nhà thuốc";
                REPORT_10.permissiontype = 3;
                REPORT_10.tabMenuId = 5;
                REPORT_10.permissionnote = "Báo cáo thu tiền hàng ngày - Nhà thuốc. Lấy theo thời gian xuất thuốc";
                lstresult.Add(REPORT_10);

                ClassCommon.classPermission REPORT_11 = new ClassCommon.classPermission();
                REPORT_11.permissioncheck = false;
                REPORT_11.permissioncode = "REPORT_11";
                REPORT_11.permissionname = "Báo cáo thuốc theo người kê - Nhà thuốc";
                REPORT_11.permissiontype = 3;
                REPORT_11.tabMenuId = 5;
                REPORT_11.permissionnote = "Báo cáo thuốc theo người kê - Nhà thuốc. Lấy theo thời gian xuất thuốc";
                lstresult.Add(REPORT_11);

                ClassCommon.classPermission REPORT_12 = new ClassCommon.classPermission();
                REPORT_12.permissioncheck = false;
                REPORT_12.permissioncode = "REPORT_12";
                REPORT_12.permissionname = "Báo cáo thực hiện Cận lâm sàng (doanh thu chia bác sĩ)";
                REPORT_12.permissiontype = 3;
                REPORT_12.tabMenuId = 5;
                REPORT_12.permissionnote = "Báo cáo thực hiện Cận lâm sàng (doanh thu chia bác sĩ)";
                lstresult.Add(REPORT_12);

                ClassCommon.classPermission REPORT_13 = new ClassCommon.classPermission();
                REPORT_13.permissioncheck = false;
                REPORT_13.permissioncode = "REPORT_13";
                REPORT_13.permissionname = "Sổ chuẩn đoán hình ảnh";
                REPORT_13.permissiontype = 3;
                REPORT_13.tabMenuId = 5;
                REPORT_13.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_13_SoCDHA";
                lstresult.Add(REPORT_13);

                ClassCommon.classPermission REPORT_14 = new ClassCommon.classPermission();
                REPORT_14.permissioncheck = false;
                REPORT_14.permissioncode = "REPORT_14";
                REPORT_14.permissionname = "Báo cáo bệnh nhân sử dụng kết hợp dịch vụ";
                REPORT_14.permissiontype = 3;
                REPORT_14.tabMenuId = 5;
                REPORT_14.permissionnote = "Báo cáo bệnh nhân sử dụng kết hợp dịch vụ";
                lstresult.Add(REPORT_14);

                ClassCommon.classPermission REPORT_15 = new ClassCommon.classPermission();
                REPORT_15.permissioncheck = false;
                REPORT_15.permissioncode = "REPORT_15";
                REPORT_15.permissionname = "Sổ xét nghiệm";
                REPORT_15.permissiontype = 3;
                REPORT_15.tabMenuId = 5;
                REPORT_15.permissionnote = "Sổ xét nghiệm";
                lstresult.Add(REPORT_15);

                ClassCommon.classPermission REPORT_16 = new ClassCommon.classPermission();
                REPORT_16.permissioncheck = false;
                REPORT_16.permissioncode = "REPORT_16";
                REPORT_16.permissionname = "Báo cáo doanh thu theo máy xét nghiệm";
                REPORT_16.permissiontype = 3;
                REPORT_16.tabMenuId = 5;
                REPORT_16.permissionnote = "Báo cáo doanh thu theo máy xét nghiệm";
                lstresult.Add(REPORT_16);

                ClassCommon.classPermission REPORT_17 = new ClassCommon.classPermission();
                REPORT_17.permissioncheck = false;
                REPORT_17.permissioncode = "REPORT_17";
                REPORT_17.permissionname = "Báo cáo phẫu thuật thủ thuật yêu cầu (doanh thu chia bác sĩ)";
                REPORT_17.permissiontype = 3;
                REPORT_17.tabMenuId = 5;
                REPORT_17.permissionnote = "Báo cáo phẫu thuật thủ thuật yêu cầu (doanh thu chia bác sĩ)";
                lstresult.Add(REPORT_17);

                ClassCommon.classPermission REPORT_18 = new ClassCommon.classPermission();
                REPORT_18.permissioncheck = false;
                REPORT_18.permissioncode = "REPORT_18";
                REPORT_18.permissionname = "Báo cáo Sổ chi tiết bệnh nhân";
                REPORT_18.permissiontype = 3;
                REPORT_18.tabMenuId = 5;
                REPORT_18.permissionnote = "Báo cáo Sổ chi tiết bệnh nhân";
                lstresult.Add(REPORT_18);

                ClassCommon.classPermission REPORT_19 = new ClassCommon.classPermission();
                REPORT_19.permissioncheck = false;
                REPORT_19.permissioncode = "REPORT_19";
                REPORT_19.permissionname = "Sổ thủ thuật  - Khoa lâm sàng";
                REPORT_19.permissiontype = 3;
                REPORT_19.tabMenuId = 5;
                REPORT_19.permissionnote = "Sổ thủ thuật - Khoa lâm sàng";
                lstresult.Add(REPORT_19);

                ClassCommon.classPermission REPORT_20 = new ClassCommon.classPermission();
                REPORT_20.permissioncheck = false;
                REPORT_20.permissioncode = "REPORT_20";
                REPORT_20.permissionname = "Báo cáo phẫu thuật thủ thuật yêu cầu QĐ 1055 (doanh thu chia bác sĩ)";
                REPORT_20.permissiontype = 3;
                REPORT_20.tabMenuId = 5;
                REPORT_20.permissionnote = "Báo cáo phẫu thuật thủ thuật yêu cầu QĐ 1055 (doanh thu chia bác sĩ)";
                lstresult.Add(REPORT_20);

                ClassCommon.classPermission REPORT_21 = new ClassCommon.classPermission();
                REPORT_21.permissioncheck = false;
                REPORT_21.permissioncode = "REPORT_21";
                REPORT_21.permissionname = "Báo cáo sử dụng thuốc";
                REPORT_21.permissiontype = 3;
                REPORT_21.tabMenuId = 5;
                REPORT_21.permissionnote = "Báo cáo sử dụng thuốc";
                lstresult.Add(REPORT_21);

                ClassCommon.classPermission REPORT_22 = new ClassCommon.classPermission();
                REPORT_22.permissioncheck = false;
                REPORT_22.permissioncode = "REPORT_22";
                REPORT_22.permissionname = "Bảng kê tổng hợp hóa đơn";
                REPORT_22.permissiontype = 3;
                REPORT_22.tabMenuId = 5;
                REPORT_22.permissionnote = "Bảng kê tổng hợp hóa đơn";
                lstresult.Add(REPORT_22);

                ClassCommon.classPermission REPORT_23 = new ClassCommon.classPermission();
                REPORT_23.permissioncheck = false;
                REPORT_23.permissioncode = "REPORT_23";
                REPORT_23.permissionname = "Báo cáo doanh thu theo nhóm dịch vụ";
                REPORT_23.permissiontype = 3;
                REPORT_23.tabMenuId = 5;
                REPORT_23.permissionnote = "Báo cáo doanh thu theo nhóm dịch vụ";
                lstresult.Add(REPORT_23);

                ClassCommon.classPermission REPORT_24 = new ClassCommon.classPermission();
                REPORT_24.permissioncheck = false;
                REPORT_24.permissioncode = "REPORT_24";
                REPORT_24.permissionname = "Báo cáo bệnh nhân thiếu tiền tạm ứng";
                REPORT_24.permissiontype = 3;
                REPORT_24.tabMenuId = 5;
                REPORT_24.permissionnote = "Báo cáo bệnh nhân thiếu tiền tạm ứng";
                lstresult.Add(REPORT_24);

                ClassCommon.classPermission REPORT_25 = new ClassCommon.classPermission();
                REPORT_25.permissioncheck = false;
                REPORT_25.permissioncode = "REPORT_25";
                REPORT_25.permissionname = "Báo cáo số tiền bệnh nhân phải thanh toán";
                REPORT_25.permissiontype = 3;
                REPORT_25.tabMenuId = 5;
                REPORT_25.permissionnote = "Báo cáo số tiền bệnh nhân phải thanh toán";
                lstresult.Add(REPORT_25);

                ClassCommon.classPermission REPORT_26 = new ClassCommon.classPermission();
                REPORT_26.permissioncheck = false;
                REPORT_26.permissioncode = "REPORT_26";
                REPORT_26.permissionname = "Báo cáo chênh lệch tiền ngày giường";
                REPORT_26.permissiontype = 3;
                REPORT_26.tabMenuId = 5;
                REPORT_26.permissionnote = "Báo cáo chênh lệch tiền ngày giường";
                lstresult.Add(REPORT_26);

                ClassCommon.classPermission REPORT_27 = new ClassCommon.classPermission();
                REPORT_27.permissioncheck = false;
                REPORT_27.permissioncode = "REPORT_27";
                REPORT_27.permissionname = "Báo cáo tình hình bệnh nhân ra vào viện";
                REPORT_27.permissiontype = 3;
                REPORT_27.tabMenuId = 5;
                REPORT_27.permissionnote = "Báo cáo tình hình bệnh nhân ra vào viện";
                lstresult.Add(REPORT_27);

                ClassCommon.classPermission REPORT_28 = new ClassCommon.classPermission();
                REPORT_28.permissioncheck = false;
                REPORT_28.permissioncode = "REPORT_28";
                REPORT_28.permissionname = "Báo cáo sử dụng dịch vụ - Chi phí khác";
                REPORT_28.permissiontype = 3;
                REPORT_28.tabMenuId = 5;
                REPORT_28.permissionnote = "Nhóm dịch vụ cấu hình trong danh mục dùng chung mã:REPORT_28_NHOMDV";
                lstresult.Add(REPORT_28);

                ClassCommon.classPermission REPORT_29 = new ClassCommon.classPermission();
                REPORT_29.permissioncheck = false;
                REPORT_29.permissioncode = "REPORT_29";
                REPORT_29.permissionname = "Báo cáo thực hiện Cận lâm sàng - K.Ung bướu (doanh thu chia bác sĩ)";
                REPORT_29.permissiontype = 3;
                REPORT_29.tabMenuId = 5;
                REPORT_29.permissionnote = "Danh mục dịch vụ cấu hình trong danh mục dùng chung mã:REPORT_29_NHOMDV";
                lstresult.Add(REPORT_29);

                ClassCommon.classPermission REPORT_30 = new ClassCommon.classPermission();
                REPORT_30.permissioncheck = false;
                REPORT_30.permissioncode = "REPORT_30";
                REPORT_30.permissionname = "Báo cáo BN sử dụng VTYT thanh toán riêng lớn hơn 45 TLCB";
                REPORT_30.permissiontype = 3;
                REPORT_30.tabMenuId = 5;
                REPORT_30.permissionnote = "Báo cáo BN sử dụng VTYT thanh toán riêng lớn hơn 45 TLCB";
                lstresult.Add(REPORT_30);

                ClassCommon.classPermission REPORT_31 = new ClassCommon.classPermission();
                REPORT_31.permissioncheck = false;
                REPORT_31.permissioncode = "REPORT_31";
                REPORT_31.permissionname = "Báo cáo hóa đơn thanh toán viện phí";
                REPORT_31.permissiontype = 3;
                REPORT_31.tabMenuId = 5;
                REPORT_31.permissionnote = "Báo cáo hóa đơn thanh toán viện phí";
                lstresult.Add(REPORT_31);

                ClassCommon.classPermission REPORT_32 = new ClassCommon.classPermission();
                REPORT_32.permissioncheck = false;
                REPORT_32.permissioncode = "REPORT_32";
                REPORT_32.permissionname = "Báo cáo thanh toán PTTT cho bác sĩ nội trú (tổng hợp)";
                REPORT_32.permissiontype = 3;
                REPORT_32.tabMenuId = 5;
                REPORT_32.permissionnote = "Báo cáo thanh toán PTTT cho bác sĩ nội trú (tổng hợp)";
                lstresult.Add(REPORT_32);

                ClassCommon.classPermission REPORT_33 = new ClassCommon.classPermission();
                REPORT_33.permissioncheck = false;
                REPORT_33.permissioncode = "REPORT_33";
                REPORT_33.permissionname = "Báo cáo thanh toán PTTT cho bác sĩ cận lâm sàng (tổng hợp)";
                REPORT_33.permissiontype = 3;
                REPORT_33.tabMenuId = 5;
                REPORT_33.permissionnote = "Báo cáo thanh toán PTTT cho bác sĩ cận lâm sàng (tổng hợp)";
                lstresult.Add(REPORT_33);

                ClassCommon.classPermission REPORT_34 = new ClassCommon.classPermission();
                REPORT_34.permissioncheck = false;
                REPORT_34.permissioncode = "REPORT_34";
                REPORT_34.permissionname = "Báo cáo thống kê sử dụng dịch vụ (Ung bướu)";
                REPORT_34.permissiontype = 3;
                REPORT_34.tabMenuId = 5;
                REPORT_34.permissionnote = "Cấu hình danh sách khoa trong danh mục dùng chung mã:REPORT_34_KHOA";
                lstresult.Add(REPORT_34);

                ClassCommon.classPermission REPORT_35 = new ClassCommon.classPermission();
                REPORT_35.permissioncheck = false;
                REPORT_35.permissioncode = "REPORT_35";
                REPORT_35.permissionname = "Bảng kê hóa đơn, chứng từ của thuốc, VTYT-HCXN sử dụng";
                REPORT_35.permissiontype = 3;
                REPORT_35.tabMenuId = 5;
                REPORT_35.permissionnote = "Cấu hình kho trong danh mục dùng chung mã:REPORT_35_KHO";
                lstresult.Add(REPORT_35);

                ClassCommon.classPermission REPORT_36 = new ClassCommon.classPermission();
                REPORT_36.permissioncheck = false;
                REPORT_36.permissioncode = "REPORT_36";
                REPORT_36.permissionname = "Báo cáo doanh thu theo dịch vụ (BC08)";
                REPORT_36.permissiontype = 3;
                REPORT_36.tabMenuId = 5;
                REPORT_36.permissionnote = "Báo cáo doanh thu theo dịch vụ (BC08)";
                lstresult.Add(REPORT_36);

                ClassCommon.classPermission REPORT_37 = new ClassCommon.classPermission();
                REPORT_37.permissioncheck = false;
                REPORT_37.permissioncode = "REPORT_37";
                REPORT_37.permissionname = "Báo cáo doanh thu theo loại hình dịch vụ";
                REPORT_37.permissiontype = 3;
                REPORT_37.tabMenuId = 5;
                REPORT_37.permissionnote = "Báo cáo doanh thu theo loại hình dịch vụ";
                lstresult.Add(REPORT_37);

                ClassCommon.classPermission REPORT_38 = new ClassCommon.classPermission();
                REPORT_38.permissioncheck = false;
                REPORT_38.permissioncode = "REPORT_38";
                REPORT_38.permissionname = "Báo cáo tình hình khám chữa bệnh - Tổng hợp";
                REPORT_38.permissiontype = 3;
                REPORT_38.tabMenuId = 5;
                REPORT_38.permissionnote = "Báo cáo tình hình khám chữa bệnh - Tổng hợp";
                lstresult.Add(REPORT_38);

                ClassCommon.classPermission REPORT_39 = new ClassCommon.classPermission();
                REPORT_39.permissioncheck = false;
                REPORT_39.permissioncode = "REPORT_39";
                REPORT_39.permissionname = "Báo cáo chỉnh sửa hóa đơn tạm ứng";
                REPORT_39.permissiontype = 3;
                REPORT_39.tabMenuId = 5;
                REPORT_39.permissionnote = "Báo cáo chỉnh sửa hóa đơn tạm ứng";
                lstresult.Add(REPORT_39);

                ClassCommon.classPermission REPORT_40 = new ClassCommon.classPermission();
                REPORT_40.permissioncheck = false;
                REPORT_40.permissioncode = "REPORT_40";
                REPORT_40.permissionname = "Báo cáo BHYT 21 chênh-2018";
                REPORT_40.permissiontype = 3;
                REPORT_40.tabMenuId = 5;
                REPORT_40.permissionnote = "Báo cáo BHYT 21 chênh-2018";
                lstresult.Add(REPORT_40);

                ClassCommon.classPermission REPORT_41 = new ClassCommon.classPermission();
                REPORT_41.permissioncheck = false;
                REPORT_41.permissioncode = "REPORT_41";
                REPORT_41.permissionname = "Báo cáo sử dụng dịch vụ - Đối tượng thanh toán Yêu cầu";
                REPORT_41.permissiontype = 3;
                REPORT_41.tabMenuId = 5;
                REPORT_41.permissionnote = "Báo cáo sử dụng dịch vụ - Đối tượng thanh toán Yêu cầu";
                lstresult.Add(REPORT_41);

                ClassCommon.classPermission REPORT_42 = new ClassCommon.classPermission();
                REPORT_42.permissioncheck = false;
                REPORT_42.permissioncode = "REPORT_42";
                REPORT_42.permissionname = "Báo cáo thống kê tai nạn thương tích";
                REPORT_42.permissiontype = 3;
                REPORT_42.tabMenuId = 5;
                REPORT_42.permissionnote = "Lấy theo thời gian tai nạn thương tích";
                lstresult.Add(REPORT_42);

                ClassCommon.classPermission REPORT_43 = new ClassCommon.classPermission();
                REPORT_43.permissioncheck = false;
                REPORT_43.permissioncode = "REPORT_43";
                REPORT_43.permissionname = "Sổ thủ thuật  - khoa cận lâm sàng";
                REPORT_43.permissiontype = 3;
                REPORT_43.tabMenuId = 5;
                REPORT_43.permissionnote = "Lấy dịch vụ được xếp loại PTTT";
                lstresult.Add(REPORT_43);

                ClassCommon.classPermission REPORT_44 = new ClassCommon.classPermission();
                REPORT_44.permissioncheck = false;
                REPORT_44.permissioncode = "REPORT_44";
                REPORT_44.permissionname = "Báo cáo sử dụng dịch vụ - Đối tượng thanh toán Yêu cầu (ver 2)";
                REPORT_44.permissiontype = 3;
                REPORT_44.tabMenuId = 5;
                REPORT_44.permissionnote = "Báo cáo sử dụng dịch vụ - Đối tượng thanh toán Yêu cầu (ver 2)";
                lstresult.Add(REPORT_44);

                ClassCommon.classPermission REPORT_45 = new ClassCommon.classPermission();
                REPORT_45.permissioncheck = false;
                REPORT_45.permissioncode = "REPORT_45";
                REPORT_45.permissionname = "Sổ phẫu thuật - Khoa Lâm sàng";
                REPORT_45.permissiontype = 3;
                REPORT_45.tabMenuId = 5;
                REPORT_45.permissionnote = "";
                lstresult.Add(REPORT_45);

                ClassCommon.classPermission REPORT_46 = new ClassCommon.classPermission();
                REPORT_46.permissioncheck = false;
                REPORT_46.permissioncode = "REPORT_46";
                REPORT_46.permissionname = "Báo cáo tình hình thanh toán bênh nhân ra viện";
                REPORT_46.permissiontype = 3;
                REPORT_46.tabMenuId = 5;
                REPORT_46.permissionnote = "";
                lstresult.Add(REPORT_46);

                ClassCommon.classPermission REPORT_47 = new ClassCommon.classPermission();
                REPORT_47.permissioncheck = false;
                REPORT_47.permissioncode = "REPORT_47";
                REPORT_47.permissionname = "Báo cáo bơm, kim tiêm hao phí trong xét nghiệm";
                REPORT_47.permissiontype = 3;
                REPORT_47.tabMenuId = 5;
                REPORT_47.permissionnote = "";
                lstresult.Add(REPORT_47);

                ClassCommon.classPermission REPORT_48 = new ClassCommon.classPermission();
                REPORT_48.permissioncheck = false;
                REPORT_48.permissioncode = "REPORT_48";
                REPORT_48.permissionname = "Báo cáo phẫu thuật thủ thuật - chi tiết thuốc/vật tư trong gói";
                REPORT_48.permissiontype = 3;
                REPORT_48.tabMenuId = 5;
                REPORT_48.permissionnote = "";
                lstresult.Add(REPORT_48);

                ClassCommon.classPermission REPORT_49 = new ClassCommon.classPermission();
                REPORT_49.permissioncheck = false;
                REPORT_49.permissioncode = "REPORT_49";
                REPORT_49.permissionname = "Báo cáo sử dụng thuốc trong toàn viện - theo khoa";
                REPORT_49.permissiontype = 3;
                REPORT_49.tabMenuId = 5;
                REPORT_49.permissionnote = "";
                lstresult.Add(REPORT_49);

                ClassCommon.classPermission REPORT_50 = new ClassCommon.classPermission();
                REPORT_50.permissioncheck = false;
                REPORT_50.permissioncode = "REPORT_50";
                REPORT_50.permissionname = "Báo cáo chi tiết sử dụng thuốc - theo khoa";
                REPORT_50.permissiontype = 3;
                REPORT_50.tabMenuId = 5;
                REPORT_50.permissionnote = "";
                lstresult.Add(REPORT_50);

                ClassCommon.classPermission REPORT_52 = new ClassCommon.classPermission();
                REPORT_52.permissioncheck = false;
                REPORT_52.permissioncode = "REPORT_52";
                REPORT_52.permissionname = "Báo cáo thời gian khám chữa bệnh trung bình";
                REPORT_52.permissiontype = 3;
                REPORT_52.tabMenuId = 5;
                REPORT_52.permissionnote = "Thời gian tính bằng phút";
                lstresult.Add(REPORT_52);

                ClassCommon.classPermission REPORT_53 = new ClassCommon.classPermission();
                REPORT_53.permissioncheck = false;
                REPORT_53.permissioncode = "REPORT_53";
                REPORT_53.permissionname = "Báo cáo bệnh nhân không làm DVKT phòng khám";
                REPORT_53.permissiontype = 3;
                REPORT_53.tabMenuId = 5;
                REPORT_53.permissionnote = "";
                lstresult.Add(REPORT_53);





                ClassCommon.classPermission REPORT_57 = new ClassCommon.classPermission();
                REPORT_57.permissioncheck = false;
                REPORT_57.permissioncode = "REPORT_57";
                REPORT_57.permissionname = "Báo cáo kê thuốc/vật tư trước ngày";
                REPORT_57.permissiontype = 3;
                REPORT_57.tabMenuId = 5;
                REPORT_57.permissionnote = "";
                lstresult.Add(REPORT_57);


                //Báo cáo quản lý =6; permissiontype = 3;
                ClassCommon.classPermission DASHBOARD_01 = new ClassCommon.classPermission();
                DASHBOARD_01.permissioncheck = false;
                DASHBOARD_01.permissioncode = "DASHBOARD_01";
                DASHBOARD_01.permissionname = "Báo cáo quản lý tổng thể khoa";
                DASHBOARD_01.permissiontype = 3;
                DASHBOARD_01.tabMenuId = 6;
                DASHBOARD_01.permissionnote = "Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện";
                lstresult.Add(DASHBOARD_01);

                ClassCommon.classPermission DASHBOARD_02 = new ClassCommon.classPermission();
                DASHBOARD_02.permissioncheck = false;
                DASHBOARD_02.permissioncode = "DASHBOARD_02";
                DASHBOARD_02.permissionname = "Báo cáo bệnh nhân nội trú";
                DASHBOARD_02.permissiontype = 3;
                DASHBOARD_02.tabMenuId = 6;
                DASHBOARD_02.permissionnote = "Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện";
                lstresult.Add(DASHBOARD_02);

                ClassCommon.classPermission DASHBOARD_03 = new ClassCommon.classPermission();
                DASHBOARD_03.permissioncheck = false;
                DASHBOARD_03.permissioncode = "DASHBOARD_03";
                DASHBOARD_03.permissionname = "Báo cáo bệnh nhân ngoại trú";
                DASHBOARD_03.permissiontype = 3;
                DASHBOARD_03.tabMenuId = 6;
                DASHBOARD_03.permissionnote = "Lấy theo tiêu chí thời gian bệnh nhân đến khám; doanh thu chia theo khoa/phòng chỉ định";
                lstresult.Add(DASHBOARD_03);

                ClassCommon.classPermission DASHBOARD_04 = new ClassCommon.classPermission();
                DASHBOARD_04.permissioncheck = false;
                DASHBOARD_04.permissioncode = "DASHBOARD_04";
                DASHBOARD_04.permissionname = "Báo cáo tổng hợp toàn viện";
                DASHBOARD_04.permissiontype = 3;
                DASHBOARD_04.tabMenuId = 6;
                DASHBOARD_04.permissionnote = "Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng bệnh nhân ra viện; BN Viện phí ngoại trú chỉ lấy dịch vụ đã thu tiền";
                lstresult.Add(DASHBOARD_04);

                ClassCommon.classPermission DASHBOARD_05 = new ClassCommon.classPermission();
                DASHBOARD_05.permissioncheck = false;
                DASHBOARD_05.permissioncode = "DASHBOARD_05";
                DASHBOARD_05.permissionname = "Báo cáo doanh thu cận lâm sàng";
                DASHBOARD_05.permissiontype = 3;
                DASHBOARD_05.tabMenuId = 6;
                DASHBOARD_05.permissionnote = "Lấy theo tiêu chí thời gian duyệt viện phí; doanh thu chia theo khoa/phòng chỉ định";
                lstresult.Add(DASHBOARD_05);

                ClassCommon.classPermission DASHBOARD_06 = new ClassCommon.classPermission();
                DASHBOARD_06.permissioncheck = false;
                DASHBOARD_06.permissioncode = "DASHBOARD_06";
                DASHBOARD_06.permissionname = "Báo cáo xuất nhập tồn tủ trực";
                DASHBOARD_06.permissiontype = 3;
                DASHBOARD_06.tabMenuId = 6;
                DASHBOARD_06.permissionnote = "Báo cáo xuất nhập tồn tủ trực";
                lstresult.Add(DASHBOARD_06);

                ClassCommon.classPermission DASHBOARD_07 = new ClassCommon.classPermission();
                DASHBOARD_07.permissioncheck = false;
                DASHBOARD_07.permissioncode = "DASHBOARD_07";
                DASHBOARD_07.permissionname = "Báo cáo bệnh nhân sử dụng thuốc/vật tư tại khoa";
                DASHBOARD_07.permissiontype = 3;
                DASHBOARD_07.tabMenuId = 6;
                DASHBOARD_07.permissionnote = "Báo cáo bệnh nhân sử dụng thuốc/vật tư tại khoa";
                lstresult.Add(DASHBOARD_07);

                ClassCommon.classPermission DASHBOARD_10 = new ClassCommon.classPermission();
                DASHBOARD_10.permissioncheck = false;
                DASHBOARD_10.permissioncode = "DASHBOARD_10";
                DASHBOARD_10.permissionname = "Báo cáo tổng hợp doanh thu khoa - toàn viện";
                DASHBOARD_10.permissiontype = 3;
                DASHBOARD_10.tabMenuId = 6;
                DASHBOARD_10.permissionnote = "Doanh thu chia theo khoa/phòng chỉ định; BN Viện phí ngoại trú chỉ lấy dịch vụ đã thu tiền";
                lstresult.Add(DASHBOARD_10);

                ClassCommon.classPermission DASHBOARD_11 = new ClassCommon.classPermission();
                DASHBOARD_11.permissioncheck = false;
                DASHBOARD_11.permissioncode = "DASHBOARD_11";
                DASHBOARD_11.permissionname = "Báo cáo BN sử dụng thuốc theo nhóm \"Hạn chế sử dụng\" - Theo khoa";
                DASHBOARD_11.permissiontype = 3;
                DASHBOARD_11.tabMenuId = 6;
                DASHBOARD_11.permissionnote = "Báo cáo BN sử dụng thuốc theo nhóm \"Hạn chế sử dụng\" - Theo khoa";
                lstresult.Add(DASHBOARD_11);

                ClassCommon.classPermission DASHBOARD_12 = new ClassCommon.classPermission();
                DASHBOARD_12.permissioncheck = false;
                DASHBOARD_12.permissioncode = "DASHBOARD_12";
                DASHBOARD_12.permissionname = "Báo cáo BN sử dụng thuốc theo nhóm \"Hạn chế sử dụng\" - Tổng hợp";
                DASHBOARD_12.permissiontype = 3;
                DASHBOARD_12.tabMenuId = 6;
                DASHBOARD_12.permissionnote = "Báo cáo BN sử dụng thuốc theo nhóm \"Hạn chế sử dụng\" - Tổng hợp";
                lstresult.Add(DASHBOARD_12);

                ClassCommon.classPermission DASHBOARD_13 = new ClassCommon.classPermission();
                DASHBOARD_13.permissioncheck = false;
                DASHBOARD_13.permissioncode = "DASHBOARD_13";
                DASHBOARD_13.permissionname = "Cập nhật và điều động nhân lực";
                DASHBOARD_13.permissiontype = 3;
                DASHBOARD_13.tabMenuId = 6;
                DASHBOARD_13.permissionnote = "Cập nhật và điều động nhân lực điều dưỡng/kỹ thuật viên hàng ngày";
                lstresult.Add(DASHBOARD_13);



                //Báo cáo QL tài chính=3; tabMenuId = 6;
                ClassCommon.classPermission REPORT_101 = new ClassCommon.classPermission();
                REPORT_101.permissioncheck = false;
                REPORT_101.permissioncode = "REPORT_101";
                REPORT_101.permissionname = "Báo cáo thống kê tiền khám yêu cầu thứ 7, chủ nhật";
                REPORT_101.permissiontype = 3;
                REPORT_101.tabMenuId = 4;
                REPORT_101.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_101_DV";
                lstresult.Add(REPORT_101);

                ClassCommon.classPermission REPORT_102 = new ClassCommon.classPermission();
                REPORT_102.permissioncheck = false;
                REPORT_102.permissioncode = "REPORT_102";
                REPORT_102.permissionname = "Báo cáo trích thưởng cho chuyên gia khám bệnh theo yêu cầu";
                REPORT_102.permissiontype = 3;
                REPORT_102.tabMenuId = 4;
                REPORT_102.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_102_DV";
                lstresult.Add(REPORT_102);

                ClassCommon.classPermission REPORT_103 = new ClassCommon.classPermission();
                REPORT_103.permissioncheck = false;
                REPORT_103.permissioncode = "REPORT_103";
                REPORT_103.permissionname = "Báo cáo chi thưởng dịch vụ khám yêu cầu viện phí";
                REPORT_103.permissiontype = 3;
                REPORT_103.tabMenuId = 4;
                REPORT_103.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_103_DV_KB;REPORT_103_DV_SADT;REPORT_103_DV_XN";
                lstresult.Add(REPORT_103);

                ClassCommon.classPermission REPORT_104 = new ClassCommon.classPermission();
                REPORT_104.permissioncheck = false;
                REPORT_104.permissioncode = "REPORT_104";
                REPORT_104.permissionname = "Báo cáo trích thưởng dịch vụ đo mật độ loãng xương, đo độ sơ hóa của gan";
                REPORT_104.permissiontype = 3;
                REPORT_104.tabMenuId = 4;
                REPORT_104.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_104_DV";
                lstresult.Add(REPORT_104);

                ClassCommon.classPermission REPORT_105 = new ClassCommon.classPermission();
                REPORT_105.permissioncheck = false;
                REPORT_105.permissioncode = "REPORT_105";
                REPORT_105.permissionname = "Báo cáo trích thưởng dịch vụ nước sôi";
                REPORT_105.permissiontype = 3;
                REPORT_105.tabMenuId = 4;
                REPORT_105.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_105_DV";
                lstresult.Add(REPORT_105);

                ClassCommon.classPermission REPORT_106 = new ClassCommon.classPermission();
                REPORT_106.permissioncheck = false;
                REPORT_106.permissioncode = "REPORT_106";
                REPORT_106.permissionname = "Báo cáo trích thưởng dịch vụ phẫu thuật theo yêu cầu (sử dụng kính hiển vi)";
                REPORT_106.permissiontype = 3;
                REPORT_106.tabMenuId = 4;
                REPORT_106.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_106_DV";
                lstresult.Add(REPORT_106);

                ClassCommon.classPermission REPORT_107 = new ClassCommon.classPermission();
                REPORT_107.permissioncheck = false;
                REPORT_107.permissioncode = "REPORT_107";
                REPORT_107.permissionname = "Báo cáo chỉ định dịch vụ khám bệnh chi tiết";
                REPORT_107.permissiontype = 3;
                REPORT_107.tabMenuId = 4;
                REPORT_107.permissionnote = "";
                lstresult.Add(REPORT_107);

                ClassCommon.classPermission REPORT_108 = new ClassCommon.classPermission();
                REPORT_108.permissioncheck = false;
                REPORT_108.permissioncode = "REPORT_108";
                REPORT_108.permissionname = "Báo cáo chi thưởng dịch vụ viện phí";
                REPORT_108.permissiontype = 3;
                REPORT_108.tabMenuId = 4;
                REPORT_108.permissionnote = "";
                lstresult.Add(REPORT_108);

                ClassCommon.classPermission REPORT_109 = new ClassCommon.classPermission();
                REPORT_109.permissioncheck = false;
                REPORT_109.permissioncode = "REPORT_109";
                REPORT_109.permissionname = "Danh sách các khoa được hưởng K3";
                REPORT_109.permissiontype = 3;
                REPORT_109.tabMenuId = 4;
                REPORT_109.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_109_DV; REPORT_109_KHOA";
                lstresult.Add(REPORT_109);

                ClassCommon.classPermission REPORT_110 = new ClassCommon.classPermission();
                REPORT_110.permissioncheck = false;
                REPORT_110.permissioncode = "REPORT_110";
                REPORT_110.permissionname = "Trích tiền thưởng dịch vụ giường yêu cầu chuyển khoản";
                REPORT_110.permissiontype = 3;
                REPORT_110.tabMenuId = 4;
                REPORT_110.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_110_DV; REPORT_110_KHOA";
                lstresult.Add(REPORT_110);

                ClassCommon.classPermission REPORT_111 = new ClassCommon.classPermission();
                REPORT_111.permissioncheck = false;
                REPORT_111.permissioncode = "REPORT_111";
                REPORT_111.permissionname = "Chi thưởng dịch vụ giường ngoại kiều viện phí";
                REPORT_111.permissiontype = 3;
                REPORT_111.tabMenuId = 4;
                REPORT_111.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_111_DV";
                lstresult.Add(REPORT_111);

                ClassCommon.classPermission REPORT_112 = new ClassCommon.classPermission();
                REPORT_112.permissioncheck = false;
                REPORT_112.permissioncode = "REPORT_112";
                REPORT_112.permissionname = "Chi thưởng giường yêu cầu cho các khoa phòng chuyển khoản";
                REPORT_112.permissiontype = 3;
                REPORT_112.tabMenuId = 4;
                REPORT_112.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_110_DV; REPORT_110_KHOA; REPORT_111_DV";
                lstresult.Add(REPORT_112);

                ClassCommon.classPermission REPORT_113 = new ClassCommon.classPermission();
                REPORT_113.permissioncheck = false;
                REPORT_113.permissioncode = "REPORT_113";
                REPORT_113.permissionname = "Danh sách Khoa hưởng tiền dịch vụ yêu cầu chất lượng cao";
                REPORT_113.permissiontype = 3;
                REPORT_113.tabMenuId = 4;
                REPORT_113.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_113_DV";
                lstresult.Add(REPORT_113);

                ClassCommon.classPermission REPORT_114 = new ClassCommon.classPermission();
                REPORT_114.permissioncheck = false;
                REPORT_114.permissioncode = "REPORT_114";
                REPORT_114.permissionname = "Báo cáo hậu cần và quản lý mổ yêu cầu";
                REPORT_114.permissiontype = 3;
                REPORT_114.tabMenuId = 4;
                REPORT_114.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_114_DV";
                lstresult.Add(REPORT_114);

                ClassCommon.classPermission REPORT_115 = new ClassCommon.classPermission();
                REPORT_115.permissioncheck = false;
                REPORT_115.permissioncode = "REPORT_115";
                REPORT_115.permissionname = "Báo cáo khoa chuẩn bị bệnh nhân";
                REPORT_115.permissiontype = 3;
                REPORT_115.tabMenuId = 4;
                REPORT_115.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_115_DV";
                lstresult.Add(REPORT_115);

                ClassCommon.classPermission REPORT_116 = new ClassCommon.classPermission();
                REPORT_116.permissioncheck = false;
                REPORT_116.permissioncode = "REPORT_116";
                REPORT_116.permissionname = "Nhập thực hiện phẫu thuật thủ thuật";
                REPORT_116.permissiontype = 3;
                REPORT_116.tabMenuId = 4;
                REPORT_116.permissionnote = "Sử dụng dữ liệu cho các báo cáo: REPORT_117; REPORT_118; REPORT_119";
                lstresult.Add(REPORT_116);

                ClassCommon.classPermission REPORT_117 = new ClassCommon.classPermission();
                REPORT_117.permissioncheck = false;
                REPORT_117.permissioncode = "REPORT_117";
                REPORT_117.permissionname = "Danh sách mổ Yêu cầu bằng phương pháp kính hiển vi";
                REPORT_117.permissiontype = 3;
                REPORT_117.tabMenuId = 4;
                REPORT_117.permissionnote = "";
                lstresult.Add(REPORT_117);

                ClassCommon.classPermission REPORT_118 = new ClassCommon.classPermission();
                REPORT_118.permissioncheck = false;
                REPORT_118.permissioncode = "REPORT_118";
                REPORT_118.permissionname = "Danh sách hưởng tiền dịch vụ yêu cầu chất lượng cao";
                REPORT_118.permissiontype = 3;
                REPORT_118.tabMenuId = 4;
                REPORT_118.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_118_DV";
                lstresult.Add(REPORT_118);

                ClassCommon.classPermission REPORT_119 = new ClassCommon.classPermission();
                REPORT_119.permissioncheck = false;
                REPORT_119.permissioncode = "REPORT_119";
                REPORT_119.permissionname = "Danh sách hưởng tiền dịch vụ mổ yêu cầu";
                REPORT_119.permissiontype = 3;
                REPORT_119.tabMenuId = 4;
                REPORT_119.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_119_DV";
                lstresult.Add(REPORT_119);

                ClassCommon.classPermission REPORT_120 = new ClassCommon.classPermission();
                REPORT_120.permissioncheck = false;
                REPORT_120.permissioncode = "REPORT_120";
                REPORT_120.permissionname = "BC thủ thuật thực hiện trên các dịch vụ kỹ thuật cao, chất lượng cao, yêu cầu";
                REPORT_120.permissiontype = 3;
                REPORT_120.tabMenuId = 4;
                REPORT_120.permissionnote = "Cấu hình trong DM dùng chung mã: REPORT_120_DV";
                lstresult.Add(REPORT_120);







                //Dashboard=3 ; permissiontype = 5;
                ClassCommon.classPermission DASHBOARD_08 = new ClassCommon.classPermission();
                DASHBOARD_08.permissioncheck = false;
                DASHBOARD_08.permissioncode = "DASHBOARD_08";
                DASHBOARD_08.permissionname = "Biểu đồ doanh thu khoa";
                DASHBOARD_08.permissiontype = 5;
                DASHBOARD_08.tabMenuId = 3;
                DASHBOARD_08.permissionnote = "Biểu đồ doanh thu khoa. Lấy theo tiêu chí khoa chỉ định dịch vụ";
                lstresult.Add(DASHBOARD_08);

                ClassCommon.classPermission DASHBOARD_09 = new ClassCommon.classPermission();
                DASHBOARD_09.permissioncheck = false;
                DASHBOARD_09.permissioncode = "DASHBOARD_09";
                DASHBOARD_09.permissionname = "Biểu đồ doanh thu theo khoa";
                DASHBOARD_09.permissiontype = 5;
                DASHBOARD_09.tabMenuId = 3;
                DASHBOARD_09.permissionnote = "Biểu đồ doanh thu theo khoa";
                lstresult.Add(DASHBOARD_09);




                //Bao cao in ra: permissiontype=10; tabMenuId=2
                ClassCommon.classPermission BAOCAO_001 = new ClassCommon.classPermission();
                BAOCAO_001.permissioncheck = false;
                BAOCAO_001.permissioncode = "BAOCAO_001";
                BAOCAO_001.permissionname = "Báo cáo Phẫu thuật - Khoa Gây mê hồi tỉnh";
                BAOCAO_001.permissiontype = 10;
                BAOCAO_001.tabMenuId = 2;
                BAOCAO_001.permissionnote = "Báo cáo Phẫu thuật - Khoa Gây mê hồi tỉnh";
                lstresult.Add(BAOCAO_001);

                ClassCommon.classPermission BAOCAO_002 = new ClassCommon.classPermission();
                BAOCAO_002.permissioncheck = false;
                BAOCAO_002.permissioncode = "BAOCAO_002";
                BAOCAO_002.permissionname = "Báo cáo Phẫu thuật - Khoa Tai mũi họng";
                BAOCAO_002.permissiontype = 10;
                BAOCAO_002.tabMenuId = 2;
                BAOCAO_002.permissionnote = "Báo cáo Phẫu thuật - Khoa Tai mũi họng";
                lstresult.Add(BAOCAO_002);

                ClassCommon.classPermission BAOCAO_003 = new ClassCommon.classPermission();
                BAOCAO_003.permissioncheck = false;
                BAOCAO_003.permissioncode = "BAOCAO_003";
                BAOCAO_003.permissionname = "Báo cáo Phẫu thuật - Khoa Răng hàm mặt";
                BAOCAO_003.permissiontype = 10;
                BAOCAO_003.tabMenuId = 2;
                BAOCAO_003.permissionnote = "Báo cáo Phẫu thuật - Khoa Răng hàm mặt";
                lstresult.Add(BAOCAO_003);

                ClassCommon.classPermission BAOCAO_004 = new ClassCommon.classPermission();
                BAOCAO_004.permissioncheck = false;
                BAOCAO_004.permissioncode = "BAOCAO_004";
                BAOCAO_004.permissionname = "Báo cáo Phẫu thuật - Khoa Mắt";
                BAOCAO_004.permissiontype = 10;
                BAOCAO_004.tabMenuId = 2;
                BAOCAO_004.permissionnote = "Báo cáo Phẫu thuật - Khoa Mắt";
                lstresult.Add(BAOCAO_004);

                ClassCommon.classPermission BAOCAO_005 = new ClassCommon.classPermission();
                BAOCAO_005.permissioncheck = false;
                BAOCAO_005.permissioncode = "BAOCAO_005";
                BAOCAO_005.permissionname = "Báo cáo Phẫu thuật - Chung";
                BAOCAO_005.permissiontype = 10;
                BAOCAO_005.tabMenuId = 2;
                BAOCAO_005.permissionnote = "Báo cáo Phẫu thuật - Chung";
                lstresult.Add(BAOCAO_005);

                ClassCommon.classPermission BAOCAO_006 = new ClassCommon.classPermission();
                BAOCAO_006.permissioncheck = false;
                BAOCAO_006.permissioncode = "BAOCAO_006";
                BAOCAO_006.permissionname = "Báo cáo Thủ thuật - Khoa Mắt";
                BAOCAO_006.permissiontype = 10;
                BAOCAO_006.tabMenuId = 2;
                BAOCAO_006.permissionnote = "Báo cáo Thủ thuật - Khoa Mắt";
                lstresult.Add(BAOCAO_006);

                ClassCommon.classPermission BAOCAO_007 = new ClassCommon.classPermission();
                BAOCAO_007.permissioncheck = false;
                BAOCAO_007.permissioncode = "BAOCAO_007";
                BAOCAO_007.permissionname = "Báo cáo Thủ thuật - Các khoa khác (trừ khoa mắt & PK mắt)";
                BAOCAO_007.permissiontype = 10;
                BAOCAO_007.tabMenuId = 2;
                BAOCAO_007.permissionnote = "Báo cáo Thủ thuật - Các khoa khác (trừ khoa mắt & PK mắt)";
                lstresult.Add(BAOCAO_007);

                ClassCommon.classPermission BAOCAO_008 = new ClassCommon.classPermission();
                BAOCAO_008.permissioncheck = false;
                BAOCAO_008.permissioncode = "BAOCAO_008";
                BAOCAO_008.permissionname = "Báo cáo Thủ thuật - Chung";
                BAOCAO_008.permissiontype = 10;
                BAOCAO_008.tabMenuId = 2;
                BAOCAO_008.permissionnote = "Báo cáo Thủ thuật - Chung";
                lstresult.Add(BAOCAO_008);

                ClassCommon.classPermission BAOCAO_009 = new ClassCommon.classPermission();
                BAOCAO_009.permissioncheck = false;
                BAOCAO_009.permissioncode = "BAOCAO_009";
                BAOCAO_009.permissionname = "Báo cáo Thủ thuật Nội soi dạ dày";
                BAOCAO_009.permissiontype = 10;
                BAOCAO_009.tabMenuId = 2;
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
