using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using DevExpress.XtraSplashScreen;

namespace MedicalLink.ChucNang
{
    public partial class ucBCBHYT21Chenh : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        internal List<MedicalLink.ClassCommon.classDMDVKTBHYTChenh> lstdmdvktcombobox { get; set; }
        public static List<MedicalLink.ClassCommon.classDMDVKTBHYTChenh> lstDVKTBHYTChenh = new List<ClassCommon.classDMDVKTBHYTChenh>();
        public ucBCBHYT21Chenh()
        {
            InitializeComponent();
        }
        private void ucBCBHYT21Chenh_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTime denNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                dateTuNgay.Value = tuNgay;
                dateDenNgay.Value = denNgay;
                chkcomboListDSKhoa.Enabled = false;

                LoadDanhMucDichVu();
                lblThongTinDVKT_TT37.Text = "";
                lblThongTinDVKT_Cu.Text = "";

                LoadDanhMucDichVuGanMaTuongDuong();
                LoadDanhSachKhoa();
                CheckDSKhoa();
            }
            catch (Exception)
            {
            }
        }
        private void cbbLoaiBA_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbLoaiBA.Text == "Lọc theo khoa")
                {
                    chkcomboListDSKhoa.Enabled = true;
                }
                else
                {
                    chkcomboListDSKhoa.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbbLoaiBA.Text.Trim() == "" || cbbTieuChi.Text.Trim() == "")
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
                else
                {
                    SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
                    try
                    {
                        LayThoiGianLayBaoCao();
                        LayTieuChiBaoCao();
                        LayLoaiBenhAn();
                        LayDanhSachLocTheoKhoa();
                        ChayLayBaoCao();
                    }
                    catch (Exception)
                    {
                    }
                    SplashScreenManager.CloseForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewBHYT21Chenh.RowCount > 0)
                {
                    try
                    {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.Filter = "Excel 2003 (.xls)|*.xls|Excel 2010 (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                            if (saveDialog.ShowDialog() != DialogResult.Cancel)
                            {
                                string exportFilePath = saveDialog.FileName;
                                string fileExtenstion = new FileInfo(exportFilePath).Extension;

                                switch (fileExtenstion)
                                {
                                    case ".xls":
                                        gridControlBHYT21Chenh.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        gridControlBHYT21Chenh.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        gridControlBHYT21Chenh.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        gridControlBHYT21Chenh.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        gridControlBHYT21Chenh.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        gridControlBHYT21Chenh.ExportToMht(exportFilePath);
                                        break;
                                    default:
                                        break;
                                }
                                ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                                frmthongbao.Show();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Có lỗi xảy ra", "Thông báo !");
                    }
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void gridViewBHYT21Chenh_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            try
            {
                //if (e.IsGetData)
                //{
                //    DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

                //    string dv_dieuchinhgia = view.GetRowCellValue(e.ListSourceRowIndex, "DVKTMa_DCG").ToString();
                //    var dv_timkiemdcg = lstDVKTBHYTChenh.FirstOrDefault(o => o.MaDVKT_Cu == dv_dieuchinhgia);

                //    if (e.Column.FieldName == "dv_nhom_ten")
                //    {
                //        string dv_nhom_ma = view.GetRowCellValue(e.ListSourceRowIndex, "dv_nhom_ma").ToString();
                //        if (dv_nhom_ma == "03XN")
                //            e.Value = "Xét nghiệm";
                //        else if (dv_nhom_ma == "04CDHA")
                //            e.Value = "Chẩn đoán hình ảnh";
                //        else if (dv_nhom_ma == "06PTTT")
                //            e.Value = "Chuyên khoa";
                //        else if (dv_nhom_ma == "12NG")
                //            e.Value = "Ngày giường";
                //        else if (dv_nhom_ma == "01KB")
                //            e.Value = "Khám bệnh";
                //        else if (dv_nhom_ma == "07KTC")
                //            e.Value = "Dịch vụ kỹ thuật cao";
                //    }
                //    else if (e.Column.FieldName == "MaDDVKT_CODE")
                //    {
                //        if (dv_timkiemdcg != null)
                //        {
                //            e.Value = dv_timkiemdcg.MaDVKT_Code;
                //        }
                //    }
                //    else if (e.Column.FieldName == "TenDVKT_Cu")
                //    {
                //        if (dv_timkiemdcg != null)
                //        {
                //            e.Value = dv_timkiemdcg.TenDVKT_Cu;
                //        }
                //    }
                //    else if (e.Column.FieldName == "MaDVKT_TuongDuong")
                //    {
                //        if (dv_timkiemdcg != null)
                //        {
                //            e.Value = dv_timkiemdcg.MaDVKT_TuongDuong;
                //        }
                //    }
                //    else if (e.Column.FieldName == "DonGia_Moi")
                //    {
                //        if (dv_timkiemdcg != null)
                //        {
                //            e.Value = dv_timkiemdcg.DonGia_Moi;
                //        }
                //    }
                //    else if (e.Column.FieldName == "DonGia_Chenh")
                //    {
                //        decimal dv_dongia_cu = 0;
                //        decimal dv_dongia_moi = 0;
                //        try
                //        {
                //            dv_dongia_cu = Convert.ToDecimal((view.GetRowCellValue(e.ListSourceRowIndex, "DonGia_DCG") ?? 0).ToString());
                //            dv_dongia_moi = Convert.ToDecimal((view.GetRowCellValue(e.ListSourceRowIndex, "DonGia_Moi") ?? 0).ToString());
                //        }
                //        catch (Exception ex)
                //        {
                //            MedicalLink.Base.Logging.Warn(ex);
                //        }
                //        e.Value = (dv_dongia_cu - dv_dongia_moi).ToString();
                //    }
                //    else if (e.Column.FieldName == "ChiPhiTangThem")
                //    {
                //        decimal dongiachenh = 0;
                //        decimal soluong = 0;
                //        try
                //        {
                //            dongiachenh = Convert.ToDecimal((view.GetRowCellValue(e.ListSourceRowIndex, "DonGia_Chenh") ?? 0).ToString());
                //            soluong = Convert.ToDecimal((view.GetRowCellValue(e.ListSourceRowIndex, "SoLuong_DCG") ?? 0).ToString());
                //        }
                //        catch (Exception ex)
                //        {
                //            MedicalLink.Base.Logging.Warn(ex);
                //        }
                //        e.Value = (dongiachenh * soluong).ToString();
                //    }

                //}
            }
            catch (Exception)
            {
            }
        }
        private void gridViewBHYT21Chenh_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                //if (e.Column == stt)
                //{
                //    e.DisplayText = Convert.ToString(e.RowHandle + 1);
                //}
            }
            catch (Exception)
            {

            }
        }


    }
}
