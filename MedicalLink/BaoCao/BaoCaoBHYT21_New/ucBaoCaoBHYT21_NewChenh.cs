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
    public partial class ucBaoCaoBHYT21_NewChenh : UserControl
    {
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        //internal List<MedicalLink.ClassCommon.classBCBHYT21ChenhNew> lstdmdvktcombobox { get; set; }
        internal static List<MedicalLink.ClassCommon.classDMDVKTBHYTChenhNew> lstDVKTBHYTChenh { get; set; }
        private string madungtuyen = "";
        public ucBaoCaoBHYT21_NewChenh()
        {
            InitializeComponent();
        }
        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
            lblThongBao_CaiDat.Visible = false;
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
                    Base.ThongBaoLable.HienThiThongBao(timerThongBao,lblThongBao,Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
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
                        LayMaDangKyDungTuyen();
                        ChayLayBaoCao(); //
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
                                Base.ThongBaoLable.HienThiThongBao(timerThongBao, lblThongBao, Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
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
                    Base.ThongBaoLable.HienThiThongBao(timerThongBao, lblThongBao, Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }





    }
}
