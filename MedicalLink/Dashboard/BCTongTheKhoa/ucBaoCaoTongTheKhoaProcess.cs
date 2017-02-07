using DevExpress.XtraSplashScreen;
using MedicalLink.TimerService;
using MedicalLink.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard
{
    public partial class ucBaoCaoTongTheKhoa : UserControl
    {
        internal void LayDuLieuBaoCao()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                // Lấy từ ngày, đến ngày
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                TimerServiceProcess.SQLKiemTraVaUpdate_DangDT_Tmp(0, thoiGianTu, thoiGianDen, KhoangThoiGianLayDuLieu, Convert.ToInt32(cboKhoa.EditValue), 0);
                TimerServiceProcess.SQLKiemTraVaUpdate_RaVienChuaTT_Tmp(0, thoiGianTu, thoiGianDen, KhoangThoiGianLayDuLieu, Convert.ToInt32(cboKhoa.EditValue), 0);
                TimerServiceProcess.SQLKiemTraVaUpdate_RaVienDaTT_Tmp(0, thoiGianTu, thoiGianDen, KhoangThoiGianLayDuLieu, Convert.ToInt32(cboKhoa.EditValue));
                SQLLayDuLieuBaoCao();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void SQLLayDuLieuBaoCao()
        {
            try
            {
                lblThoiGianLayBaoCao.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

                string sqlBaoCao_DangDT = "SELECT * FROM tools_dangdt_tmp WHERE departmentgroupid = '" + Convert.ToInt32(cboKhoa.EditValue) + "' and kieulaydulieu='0' and khoangdl_tu='" + KhoangThoiGianLayDuLieu + "' ORDER BY dangdt_date DESC LIMIT 1; ";
                string sqlBaoCao_RaVienChuaTT = "SELECT * FROM tools_ravienchuatt_tmp WHERE departmentgroupid = '" + Convert.ToInt32(cboKhoa.EditValue) + "' and kieulaydulieu='0' and khoangdl_tu='" + KhoangThoiGianLayDuLieu + "' ORDER BY ravienchuatt_date DESC LIMIT 1;";
                string sqlBaoCao_RaVienDaTT = "SELECT * FROM tools_raviendatt_tmp raviendatt WHERE raviendatt.departmentgroupid = '" + Convert.ToInt32(cboKhoa.EditValue) + "' ORDER BY raviendatt_date DESC LIMIT 1; ";

                DataView dataBCTongTheKhoa_DangDT = new DataView(condb.getDataTable(sqlBaoCao_DangDT));
                DataView dataBCTongTheKhoa_RaVienChuaTT = new DataView(condb.getDataTable(sqlBaoCao_RaVienChuaTT));
                DataView dataBCTongTheKhoa_RaVienDaTT = new DataView(condb.getDataTable(sqlBaoCao_RaVienDaTT));
                HienThiDuLieuBaoCao_DangDT(dataBCTongTheKhoa_DangDT);
                HienThiDuLieuBaoCao_RaVienChuaTT(dataBCTongTheKhoa_RaVienChuaTT);
                HienThiDuLieuBaoCao_RaVienDaTT(dataBCTongTheKhoa_RaVienDaTT);
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #region Hien thi du lieu
        private void HienThiDuLieuBaoCao_DangDT(DataView dataBCTongTheKhoa)
        {
            try
            {
                if (dataBCTongTheKhoa != null && dataBCTongTheKhoa.Count > 0)
                {
                    lblBNHienDien.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_slbn"]), 0);
                    lblBNChuyenDi.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["bn_chuyendi"]), 0);
                    lblBNChuyenDen.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["bn_chuyenden"]), 0);
                    lblBNRaVien.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravien_slbn"]), 0);

                    Decimal tongtien = Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tongtien"]);
                    lblDangDTSoTien.Text = Util_NumberConvert.NumberToString(tongtien, 0);
                    lblDangDTKhamBenh.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienkb"]), 0);
                    lblDangDTXetNghiem.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienxn"]), 0);
                    lblDangDTCDHA.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tiencdhatdcn"]), 0);
                    lblDangDTPTTT.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienpttt"]), 0);
                    lblDangDTDVKTC.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tiendvktc"]), 0);
                    lblDangDTGiuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tiengiuong"]), 0);
                    lblDangDTDVKhac.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienkhac"]), 0);
                    lblDangDTMau.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienmau"]), 0);
                    lblDangDTVatTu.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienvattu"]), 0);
                    lblDangDTThuoc.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienthuoc"]), 0);
                    if (tongtien != 0)
                    {
                        lblDangDTTyLeThuoc.Text = Convert.ToString(Math.Round((Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tienthuoc"]) * 100 / tongtien), 2));
                    }
                    else
                    {
                        lblDangDTTyLeThuoc.Text = "0";
                    }
                    lblDangDTTamUng.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["dangdt_tamung"]), 0);
                }
                else
                {
                    lblBNHienDien.Text = "0";
                    lblBNChuyenDi.Text = "0";
                    lblBNChuyenDen.Text = "0";
                    lblBNRaVien.Text = "0";
                    lblDangDTSoTien.Text = "0";
                    lblDangDTKhamBenh.Text = "0";
                    lblDangDTXetNghiem.Text = "0";
                    lblDangDTCDHA.Text = "0";
                    lblDangDTPTTT.Text = "0";
                    lblDangDTDVKTC.Text = "0";
                    lblDangDTGiuong.Text = "0";
                    lblDangDTDVKhac.Text = "0";
                    lblDangDTMau.Text = "0";
                    lblDangDTVatTu.Text = "0";
                    lblDangDTThuoc.Text = "0";
                    lblDangDTTyLeThuoc.Text = "0";
                    lblDangDTTamUng.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }
        private void HienThiDuLieuBaoCao_RaVienChuaTT(DataView dataBCTongTheKhoa)
        {
            try
            {
                if (dataBCTongTheKhoa != null && dataBCTongTheKhoa.Count > 0)
                {
                    lblDaRVSoLuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_slbn"]), 0);
                    Decimal tongtien = Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tongtien"]);

                    lblDaRVDoanhThu.Text = Util_NumberConvert.NumberToString(tongtien, 0);
                    lblDaRVKhamBenh.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienkb"]), 0);
                    lblDaRVXetNghiem.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienxn"]), 0);
                    lblDaRVCDHA.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tiencdhatdcn"]), 0);
                    lblDaRVPTTT.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienpttt"]), 0);
                    lblDaRVDVKTC.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tiendvktc"]), 0);
                    lblDaRVGiuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tiengiuong"]), 0);
                    lblDaRVDVKhac.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienkhac"]), 0);
                    lblDaRVVatTu.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienvattu"]), 0);
                    lblDaRVMau.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienmau"]), 0);
                    lblDaRVThuoc.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienthuoc"]), 0);
                    if (tongtien != 0)
                    {
                        lblDaRVTyLeThuoc.Text = Convert.ToString(Math.Round((Convert.ToDecimal(dataBCTongTheKhoa[0]["ravienchuatt_tienthuoc"]) * 100 / tongtien), 2));
                    }
                    else
                    {
                        lblDaRVTyLeThuoc.Text = "0";
                    }
                }
                else
                {
                    lblDaRVSoLuong.Text = "0";
                    lblDaRVDoanhThu.Text = "0";
                    lblDaRVKhamBenh.Text = "0";
                    lblDaRVXetNghiem.Text = "0";
                    lblDaRVCDHA.Text = "0";
                    lblDaRVPTTT.Text = "0";
                    lblDaRVDVKTC.Text = "0";
                    lblDaRVGiuong.Text = "0";
                    lblDaRVDVKhac.Text = "0";
                    lblDaRVMau.Text = "0";
                    lblDaRVVatTu.Text = "0";
                    lblDaRVThuoc.Text = "0";
                    lblDaRVTyLeThuoc.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void HienThiDuLieuBaoCao_RaVienDaTT(DataView dataBCTongTheKhoa)
        {
            try
            {
                //raviendatt_slbn_bh, raviendatt_slbn_vp, raviendatt_slbn, raviendatt_tienkb, raviendatt_tienxn, raviendatt_tiencdhatdcn, raviendatt_tienpttt, raviendatt_tiendvktc, raviendatt_tiengiuong, raviendatt_tienkhac, raviendatt_tienvattu, raviendatt_tienmau, raviendatt_tienthuoc_bhyt, raviendatt_tienthuoc_vp, raviendatt_tienthuoc, raviendatt_tongtien_bhyt, raviendatt_tongtien_vp, raviendatt_tongtien, raviendatt_tamung, raviendatt_date
                if (dataBCTongTheKhoa != null && dataBCTongTheKhoa.Count > 0)
                {
                    lblDaTTSoLuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_slbn_bh"]), 0);

                    Decimal tongtien = Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tongtien"]);
                    lblDaTTDoanhThu.Text = Util_NumberConvert.NumberToString(tongtien, 0);
                    lblDaTTKhamBenh.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienkb"]), 0);
                    lblDaTTXetNghiem.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienxn"]), 0);
                    lblDaTTCDHA.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tiencdhatdcn"]), 0);
                    lblDaTTPTTT.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienpttt"]), 0);
                    lblDaTTDVKTC.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tiendvktc"]), 0);
                    lblDaTTGiuong.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tiengiuong"]), 0);
                    lblDaTTDVKhac.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienkhac"]), 0);
                    lblDaTTVatTu.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienvattu"]), 0);
                    lblDaTTMau.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienmau"]), 0);
                    lblDaTTThuoc.Text = Util_NumberConvert.NumberToString(Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienthuoc"]), 0);
                    if (tongtien != 0)
                    {
                        lblDaTTTyLeThuoc.Text = Convert.ToString(Math.Round((Convert.ToDecimal(dataBCTongTheKhoa[0]["raviendatt_tienthuoc"]) * 100 / tongtien), 2));
                    }
                    else
                    {
                        lblDaTTTyLeThuoc.Text = "0";
                    }
                }
                else
                {
                    lblDaTTSoLuong.Text = "0";
                    lblDaTTDoanhThu.Text = "0";
                    lblDaTTKhamBenh.Text = "0";
                    lblDaTTXetNghiem.Text = "0";
                    lblDaTTCDHA.Text = "0";
                    lblDaTTPTTT.Text = "0";
                    lblDaTTDVKTC.Text = "0";
                    lblDaTTGiuong.Text = "0";
                    lblDaTTDVKhac.Text = "0";
                    lblDaTTMau.Text = "0";
                    lblDaTTVatTu.Text = "0";
                    lblDaTTThuoc.Text = "0";
                    lblDaTTTyLeThuoc.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion
    }
}
