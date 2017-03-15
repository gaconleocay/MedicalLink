using DevExpress.XtraCharts;
using DevExpress.XtraSplashScreen;
using MedicalLink.ClassCommon;
using MedicalLink.DatabaseProcess.FilterDTO;
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
    public partial class ucDashboardDoanhThuTungKhoa : UserControl
    {
        private DangDTRaVienChuaDaTTFilterDTO filter { get; set; }
        private void LayDuLieuBaoCao_ChayMoi()
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                filter = new DangDTRaVienChuaDaTTFilterDTO();
                thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                filter.loaiBaoCao = "REPORT_08";
                filter.dateTu = this.thoiGianTu;
                filter.dateDen = this.thoiGianDen;
                filter.dateKhoangDLTu = this.KhoangThoiGianLayDuLieu;
                filter.departmentgroupid = Convert.ToInt16(cboKhoa.EditValue);
                filter.loaivienphiid = 0;
                filter.chayTuDong = 0;

                //DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_DangDT_Tmp(filter);
                //DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienChuaTT_Tmp(filter);
                DatabaseProcess.DangDTRaVienChuaDaTT_Tmp_Process.SQLChay_RaVienDaTT_Tmp(filter);
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

                string sqlBaoCao_RaVienDaTT = "(SELECT departmentgroupid, raviendatt_slbn_bh, raviendatt_slbn_vp, raviendatt_slbn, raviendatt_tienkb, raviendatt_tienxn, raviendatt_tiencdhatdcn, raviendatt_tienpttt, raviendatt_tiendvktc, raviendatt_tiengiuongthuong,raviendatt_tiengiuongyeucau, raviendatt_tienkhac, raviendatt_tienvattu, raviendatt_tienmau, raviendatt_tienthuoc_bhyt, raviendatt_tienthuoc_vp, raviendatt_tienthuoc, raviendatt_tongtien_bhyt, raviendatt_tongtien_vp, raviendatt_tongtien, raviendatt_tamung FROM tools_raviendatt_tmp raviendatt WHERE raviendatt.departmentgroupid = '" + Convert.ToInt32(cboKhoa.EditValue) + "' and loaibaocao='REPORT_08' and chaytudong=0 ORDER BY raviendatt_date DESC LIMIT 1) Union (select 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 from tools_raviendatt_tmp  limit 1) order by departmentgroupid desc; ";

                DataView dataBCTongTheKhoa_RaVienDaTT = new DataView(condb.getDataTable(sqlBaoCao_RaVienDaTT));

                chartControlDTKhoa.Series[0].Points.Clear();
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Khám bệnh", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienkb"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Xét nghiệm", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienxn"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("CĐHA-TDCN", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tiencdhatdcn"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("PTTT", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienpttt"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("DC KTC", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tiendvktc"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Giường thường", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tiengiuongthuong"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Giường yêu cầu", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tiengiuongyeucau"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Thuốc", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienthuoc"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Vật tư", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienvattu"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Máu", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienmau"].ToString())));
                chartControlDTKhoa.Series[0].Points.Add(new SeriesPoint("Khác", Utilities.Util_TypeConvertParse.ToDouble(dataBCTongTheKhoa_RaVienDaTT[0]["raviendatt_tienkhac"].ToString())));
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

    }
}
