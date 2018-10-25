using DevExpress.XtraTab;
using MedicalLink.FormCommon.TabCaiDat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.FormCommon
{
    internal static class TabControlProcess
    {
        #region Tabcontrol function
        //Dong tab
        internal static void CloseAllTabpage(XtraTabControl tabControlName)
        {
            try
            {
                int tab = 0;
                while (tabControlName.TabPages.Count > 0)
                {
                    if (tabControlName.TabPages[tab].Name != "xtraTabDSChucNang")
                    {
                        tabControlName.TabPages.Remove(tabControlName.TabPages[tab]);
                    }
                }
                System.GC.Collect();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        /// <summary>
        /// Tạo thêm tab mới
        /// </summary>
        /// <param name="tabControl">Tên TabControl để thêm tabpage mới vào</param>
        /// <param name="name">Tên tabpage mới</param>
        internal static void TabCreating(XtraTabControl tabControl, string name)
        {
            try
            {
                int index = KiemTraTabpageTonTai(tabControl, name);
                if (index >= 0)
                {
                    if (tabControl.TabPages[index].PageVisible == false)
                        tabControl.TabPages[index].PageVisible = true;
                }
                else
                {
                    index = 0;
                }
                tabControl.SelectedTabPage = tabControl.TabPages[index];
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        /// <summary>
        /// Tạo thêm tab mới
        /// </summary>
        /// <param name="tabControl">Tên TabControl để thêm tabpage mới vào</param>
        /// <param name="name">Tên tabpage mới</param>
        internal static void TabCreating(XtraTabControl tabControl, string name, string text, string tooltip, UserControl uc)
        {
            try
            {
                if (tabControl.Visible == false)
                {
                    tabControl.Visible = true;
                }
                int index = KiemTraTabpageTonTai(tabControl, name);
                if (index >= 0)
                {
                    if (tabControl.TabPages[index].PageVisible == false)
                        tabControl.TabPages[index].PageVisible = true;

                    tabControl.SelectedTabPage = tabControl.TabPages[index];
                }
                else
                {
                    KiemTraGioiHanSLTabpage(tabControl);
                    XtraTabPage tabpage = new XtraTabPage { Text = text, Name = name, Tooltip = tooltip };
                    tabControl.TabPages.Add(tabpage);
                    tabControl.SelectedTabPage = tabpage;

                    uc.Parent = tabpage;
                    uc.Show();
                    uc.Dock = DockStyle.Fill;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        /// <summary>
        /// Kiểm tra tabpage có tồn tại hay không
        /// </summary>
        /// <param name="tabControlName">Tên TabControl để kiểm tra</param>
        /// <param name="tabName">Tên tabpage cần kiểm tra</param>
        internal static int KiemTraTabpageTonTai(XtraTabControl tabControlName, string tabName)
        {
            int result = -1;
            try
            {
                for (int i = 0; i < tabControlName.TabPages.Count; i++)
                {
                    if (tabControlName.TabPages[i].Name == tabName)
                    {
                        result = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
            return result;
        }

        internal static void KiemTraGioiHanSLTabpage(XtraTabControl tabControlName)
        {
            try
            {
                if (tabControlName.TabPages.Count >= MedicalLink.Base.KeyTrongPhanMem.SoLuongTabPageChucNang)
                {
                    for (int i = 1; i < tabControlName.TabPages.Count - (MedicalLink.Base.KeyTrongPhanMem.SoLuongTabPageChucNang - 1); i++)
                    {
                        tabControlName.TabPages.Remove(tabControlName.TabPages[i]);
                        //tabControlName.SelectedTabPageIndex = tabControlName.TabPages.Count - 1;
                        System.GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        internal static UserControl SelectUCControlActive(string ucName)
        {
            UserControl ucResult = new UserControl();
            try
            {
                switch (ucName)
                {
                    case "SYS_02":
                        ucResult = new ucQuanLyNguoiDung();
                        break;
                    case "SYS_03":
                        ucResult = new ucDanhSachNhanVien();
                        break;
                    case "SYS_04":
                        ucResult = new ucCauHinhHeThong();
                        break;


                    //Chuc nang
                    case "TOOL_01":
                        ucResult = new ChucNang.ucSuaThoiGianRaVien();
                        break;
                    case "TOOL_02":
                        ucResult = new ChucNang.ucChuyenTienTamUng();
                        break;
                    case "TOOL_03":
                        ucResult = new ChucNang.ucMoBenhAn();
                        break;
                    case "TOOL_04":
                        ucResult = new ChucNang.ucSuaTGDuyetVP();
                        break;
                    case "TOOL_05":
                        ucResult = new ChucNang.ucXuLyBNBoKhoa();
                        break;
                    case "TOOL_06":
                        ucResult = new ChucNang.ucImportDMThuocVatTu();
                        break;
                    case "TOOL_07":
                        ucResult = new ChucNang.ucImportDMDichVu();
                        break;
                    case "TOOL_08":
                        ucResult = new ChucNang.ucSuaMaTenGiaDV();
                        break;
                    case "TOOL_09":
                        ucResult = new ChucNang.ucUpdateDataSerPrice();
                        break;
                    case "TOOL_10":
                        ucResult = new ChucNang.ucCapNhatKhaDungTonKho();
                        break;
                    case "TOOL_11":
                        ucResult = new ChucNang.ucSuaPhoiThanhToan();
                        break;
                    case "TOOL_12":
                        ucResult = new ChucNang.ucSuaPhieuCDDV();
                        break;
                    case "TOOL_13":
                        ucResult = new ChucNang.ucSuaThongTinBenhAn();
                        break;
                    case "TOOL_14":
                        ucResult = new ChucNang.ucKTHSBALoiTrangThai();
                        break;
                    case "TOOL_15":
                        ucResult = new ChucNang.ucKTTTDonThuocNoiTru();
                        break;
                    case "TOOL_16":
                        ucResult = new ChucNang.ucXuLyMaVPTrang();
                        break;
                    case "TOOL_17":
                        ucResult = new ChucNang.ucSuaDanhMucDichVu();
                        break;
                    case "TOOL_18":
                        ucResult = new ChucNang.ThongTinThucHienCLS.ucThongTinThucHienCanLamSang();
                        break;
                    case "TOOL_19":
                        ucResult = new ChucNang.DichVuThanhToanRieng.ucDichVuThanhToanRieng();
                        break;
                    case "TOOL_20":
                        ucResult = new ChucNang.CapNhatThangLuongCoBan.ucCapNhatThangLuongCoBan();
                        break;
                    case "TOOL_21":
                        ucResult = new ChucNang.ucSuaPhieuTamUng();
                        break;
                    case "TOOL_22":
                        ucResult = new ChucNang.ucGopBenhAn();
                        break;
                    case "TOOL_23":
                        ucResult = new ChucNang.ucThuocCapPhatMienPhi();
                        break;
                    case "TOOL_24":
                        ucResult = new ChucNang.ucTOOL24_CapNhatLoaiHTT();
                        break;
                    case "TOOL_25":
                        ucResult = new ChucNang.ucDongHoSoBenhAn();
                        break;
                    case "TOOL_26":
                        ucResult = new ChucNang.ucSuaHanSuDungThuocVatTu();
                        break;


                    //Bao cao
                    case "REPORT_01":
                        ucResult = new ChucNang.ucBC01_DSBNSDdv();
                        break;
                    case "REPORT_02":
                        ucResult = new ChucNang.ucBCThongKeTheoICD10();
                        break;
                    case "REPORT_03":
                        ucResult = new ChucNang.ucBCTimPhieuTHYL();
                        break;
                    case "REPORT_04":
                        ucResult = new ChucNang.ucBCChiDinhPTTT();
                        break;
                    case "REPORT_05":
                        ucResult = new ChucNang.ucBCBHYT21Chenh();
                        break;
                    case "REPORT_06":
                        ucResult = new ChucNang.ucBaoCaoBHYT21_NewChenh();
                        break;
                    case "REPORT_07":
                        ucResult = new ChucNang.ucTimThuocDVSaiMa();
                        break;
                    case "REPORT_08":
                        ucResult = new BaoCao.ucBCPhauThuatThuThuat();
                        break;
                    case "REPORT_09":
                        ucResult = new BaoCao.ucBCChiDinhPTTT_G304();
                        break;
                    case "REPORT_10":
                        ucResult = new BaoCao.ucBCXuatThuocNhaThuoc();
                        break;
                    case "REPORT_11":
                        ucResult = new BaoCao.ucBCThuocTheoNguoiKe();
                        break;
                    case "REPORT_12":
                        ucResult = new BaoCao.ucBCThucHienCLS();
                        break;
                    case "REPORT_13":
                        ucResult = new BaoCao.ucBCSoCDHA();
                        break;
                    case "REPORT_14":
                        ucResult = new BaoCao.ucBCBNSDDVKetHop();
                        break;
                    case "REPORT_15":
                        ucResult = new BaoCao.ucBCSoXetNghiem();
                        break;
                    case "REPORT_16":
                        ucResult = new BaoCao.ucBC16_DoanhThuTheoMayXN();
                        break;
                    case "REPORT_17":
                        ucResult = new BaoCao.BCPhauThuat_YeuCau();
                        break;
                    case "REPORT_18":
                        ucResult = new BaoCao.ucBCSoChiTietBenhNhan();
                        break;
                    case "REPORT_19":
                        ucResult = new BaoCao.ucBC19_SoThuThuatLamSang();
                        break;
                    case "REPORT_20":
                        ucResult = new BaoCao.BCPhauThuat_YeuCauQD1055();
                        break;
                    case "REPORT_21":
                        ucResult = new BaoCao.ucBCSuDungThuoc();
                        break;
                    case "REPORT_22":
                        ucResult = new BaoCao.ucBangKeTongHopHoaDon();
                        break;
                    case "REPORT_23":
                        ucResult = new BaoCao.ucBC23_DoanhThuTheoNhomDichVu();
                        break;
                    case "REPORT_24":
                        ucResult = new BaoCao.ucBNThieuTienTamUng();
                        break;
                    case "REPORT_25":
                        ucResult = new BaoCao.ucBCTienBNPhaiThanhToan();
                        break;
                    case "REPORT_26":
                        ucResult = new BaoCao.ucBCChenhLechNgayGiuong();
                        break;
                    case "REPORT_27":
                        ucResult = new BaoCao.ucBCTinhHinhRaVaoVien();
                        break;
                    case "REPORT_28":
                        ucResult = new BaoCao.ucBNSDDV_ThuChiKhac();
                        break;
                    case "REPORT_29":
                        ucResult = new BaoCao.ucBCThucHienCLS_UngBuou();
                        break;
                    case "REPORT_30":
                        ucResult = new BaoCao.ucBCVTTTRieng45TLCB();
                        break;
                    case "REPORT_31":
                        ucResult = new BaoCao.ucBCHoaDonThanhToanVP();
                        break;
                    case "REPORT_32":
                        ucResult = new BaoCao.BCPhauThuatThuThuat_TongHop();
                        break;
                    case "REPORT_33":
                        ucResult = new BaoCao.ucBCThucHienCLS_TongHop();
                        break;
                    case "REPORT_34":
                        ucResult = new BaoCao.ucTKSuDungDichVuTheoKhoa();
                        break;
                    case "REPORT_35":
                        ucResult = new BaoCao.ucBCHoaDonChungTuThuocSD();
                        break;
                    case "REPORT_36":
                        ucResult = new BaoCao.ucBCDoanhThuDichVuBC08();
                        break;
                    case "REPORT_37":
                        ucResult = new BaoCao.ucBCDoanhThuTheoLoaiHinhDV();
                        break;
                    case "REPORT_38":
                        ucResult = new BaoCao.ucTinhHinhKhamChuaBenhTongHop();
                        break;
                    case "REPORT_39":
                        ucResult = new BaoCao.BCChinhSuaHoaDon_TamUng();
                        break;
                    case "REPORT_40":
                        ucResult = new BaoCao.ucBCBHYT21Chenh2018();
                        break;
                    case "REPORT_41":
                        ucResult = new BaoCao.ucBC41_DVYeuCau();
                        break;
                    case "REPORT_42":
                        ucResult = new BaoCao.ucBC42_TaiNanThuongTich();
                        break;
                    case "REPORT_43":
                        ucResult = new BaoCao.ucBC43_SoThuThuatCLS();
                        break;
                    case "REPORT_44":
                        ucResult = new BaoCao.ucBC44_DVYeuCau_2();
                        break;
                    case "REPORT_45":
                        ucResult = new BaoCao.ucBC45_SoPhauThuatLamSang();
                        break;
                    case "REPORT_46":
                        ucResult = new BaoCao.ucBC46_TinhHinhThanhToan();
                        break;
                    case "REPORT_47":
                        ucResult = new BaoCao.ucBC47_BomTiemHaoPhi();
                        break;
                    case "REPORT_48":
                        ucResult = new BaoCao.ucBC48_PhauThuatThuThuatChiTiet();
                        break;
                    case "REPORT_49":
                        ucResult = new BaoCao.ucBC49_SuDungThuocToanVien();
                        break;
                    case "REPORT_50":
                        ucResult = new BaoCao.ucBC50_SuDungThuocTheoKhoaChiTiet();
                        break;
                    case "REPORT_52":
                        ucResult = new BaoCao.ucBC52_TGKhamBenhTrungBinh();
                        break;



                    //Bao cao QL Tai chinh
                    case "REPORT_101":
                        ucResult = new BCQLTaiChinh.BC101_TKTienKhamYCT7CN();
                        break;
                    case "REPORT_102":
                        ucResult = new BCQLTaiChinh.BC102_TrichThuongChuyenGiaYC();
                        break;
                    case "REPORT_103":
                        ucResult = new BCQLTaiChinh.BC103_ChiThuongDichVuVienPhi();
                        break;
                    case "REPORT_104":
                        ucResult = new BCQLTaiChinh.BC104_TrichThuongFIBRO();
                        break;
                    case "REPORT_105":
                        ucResult = new BCQLTaiChinh.BC105_TrichThuongDVNuocSoi();
                        break;
                    case "REPORT_106":
                        ucResult = new BCQLTaiChinh.BC106_TrichThuongKinhHienVi();
                        break;
                    case "REPORT_107":
                        ucResult = new BCQLTaiChinh.BC107_KhamBenhChiTiet();
                        break;
                    case "REPORT_108":
                        ucResult = new BCQLTaiChinh.BC108_ChiThuongDVThuVienPhi();
                        break;
                    case "REPORT_109":
                        ucResult = new BCQLTaiChinh.BC109_DSCacKhoaDuocHuongK3();
                        break;
                    case "REPORT_110":
                        ucResult = new BCQLTaiChinh.BC110_TrichThuongGiuongYCCK();
                        break;
                    case "REPORT_111":
                        ucResult = new BCQLTaiChinh.BC111_ChiThuongGiuongNgoaiKieuVP();
                        break;
                    case "REPORT_112":
                        ucResult = new BCQLTaiChinh.BC112_ChiTienGiuongYCChoCacKhoaCK();
                        break;
                    case "REPORT_113":
                        ucResult = new BCQLTaiChinh.BC113_DSKhoaHuongTienDVYCCLC();
                        break;
                    case "REPORT_114":
                        ucResult = new BCQLTaiChinh.BC114_HauCanVaQuanLyMoYeuCau();
                        break;
                    case "REPORT_115":
                        ucResult = new BCQLTaiChinh.BC115_KhoaChuanBiBNQD151();
                        break;
                    case "REPORT_116":
                        ucResult = new BCQLTaiChinh.NhapThongTinPTTT.ucNhapThucHienPTTT();
                        break;
                    case "REPORT_117":
                        ucResult = new BCQLTaiChinh.BC117_MoYCBangPPKinhHienVi();
                        break;
                    case "REPORT_118":
                        ucResult = new BCQLTaiChinh.BC118_DSHuongTienDVYCCLC();
                        break;
                    case "REPORT_119":
                        ucResult = new BCQLTaiChinh.BC119_DSHuongTienDVMoYC();
                        break;
                    case "REPORT_120":
                        ucResult = new BCQLTaiChinh.BC120_ThuThuatDVKTYC();
                        break;






                    //Dashboard
                    case "DASHBOARD_01":
                        ucResult = new Dashboard.ucBCQLTongTheKhoa();
                        break;
                    case "DASHBOARD_02":
                        ucResult = new Dashboard.ucBaoCaoBenhNhanNoiTru();
                        break;
                    case "DASHBOARD_03":
                        ucResult = new Dashboard.ucBaoCaoBenhNhanNgoaiTru();
                        break;
                    case "DASHBOARD_04":
                        ucResult = new Dashboard.ucBaoCaoTongHopToanVien();
                        break;
                    case "DASHBOARD_05":
                        ucResult = new Dashboard.ucBaoCaoCanLamSang();
                        break;
                    case "DASHBOARD_06":
                        ucResult = new Dashboard.ucBaoCaoXNTTuTruc();
                        break;
                    case "DASHBOARD_07":
                        ucResult = new Dashboard.ucBCBNSuDungThuocTaiKhoa();
                        break;
                    case "DASHBOARD_08":
                        ucResult = new Dashboard.ucDashboardDoanhThuTungKhoa();
                        break;
                    case "DASHBOARD_09":
                        ucResult = new Dashboard.ucDashboardBenhNhanNoiTru();
                        break;
                    case "DASHBOARD_10":
                        ucResult = new Dashboard.ucBCTongHopDoanhThuKhoa();
                        break;
                    case "DASHBOARD_11":
                        ucResult = new Dashboard.ucBCBNSuDungThuocTheoNhom();
                        break;
                    case "DASHBOARD_12":
                        ucResult = new Dashboard.ucBCBNSuDungThuocTheoNhom();
                        break;
                    case "DASHBOARD_13":
                        ucResult = new Dashboard.ucCapNhatVaDieuDongNhanSu();
                        break;




                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            return ucResult;
        }
    }
}
