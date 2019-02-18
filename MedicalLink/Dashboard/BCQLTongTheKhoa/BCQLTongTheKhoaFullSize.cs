using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalLink.Dashboard.BCQLTongTheKhoa
{
    public partial class BCTongTheKhoaFullSize : Form
    {
        public BCTongTheKhoaFullSize()
        {
            InitializeComponent();
        }
        public BCTongTheKhoaFullSize(List<BCDashboardQLTongTheKhoa> dataBCQLTongTheKhoaSDO, string tenkhoa)
        {
            InitializeComponent();
            gridControlDataQLTTKhoa.DataSource = dataBCQLTongTheKhoaSDO;
            lblTenThongTinChiTiet.Text = "BÁO CÁO QUẢN LÝ TỔNG THỂ - " + tenkhoa.ToUpper();

        }

        private void bandedGridViewDataBNNT_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void bandedGridViewDataBNNT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.DodgerBlue;
                    e.Appearance.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (bandedGridViewDataQLTTKhoa.RowCount > 0)
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
                                        bandedGridViewDataQLTTKhoa.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        bandedGridViewDataQLTTKhoa.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        bandedGridViewDataQLTTKhoa.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        bandedGridViewDataQLTTKhoa.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        bandedGridViewDataQLTTKhoa.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        bandedGridViewDataQLTTKhoa.ExportToMht(exportFilePath);
                                        break;
                                    default:
                                        break;
                                }
                                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
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
                    O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Warn(ex);
            }
        }


    }
}
