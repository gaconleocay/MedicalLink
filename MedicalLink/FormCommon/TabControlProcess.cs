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
                    //
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
                        ucResult = new ChucNang.ucImportDMThuoc();
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
                    //
                    case "REPORT_01":
                        ucResult = new ChucNang.ucBCDSBNSDdv();
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
                    //
                    case "REPORT_08":
                        ucResult = new Dashboard.ucBaoCaoTongTheKhoa();
                        break;
                    case "REPORT_09":
                        ucResult = new Dashboard.ucBaoCaoBenhNhanNoiTru();
                        break;
                    case "REPORT_10":
                        ucResult = new Dashboard.ucBaoCaoBenhNhanNgoaiTru();
                        break;
                    case "REPORT_11":
                        ucResult = new Dashboard.ucBaoCaoTongHopToanVien();
                        break;
                    case "REPORT_12":
                        ucResult = new Dashboard.ucBaoCaoCanLamSang();
                        break;


                    case "REPORT_14":
                        ucResult = new Dashboard.ucBaoCaoXNTTuTruc();
                        break;


                    //
                    case "DASHBOARD_01":
                        ucResult = new Dashboard.ucDashboardDoanhThuTungKhoa();
                        break;
                    case "DASHBOARD_02":
                        ucResult = new Dashboard.ucDashboardBenhNhanNoiTru();
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
