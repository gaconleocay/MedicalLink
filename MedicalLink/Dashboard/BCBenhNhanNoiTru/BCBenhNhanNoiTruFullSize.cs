using DevExpress.XtraGrid.Views.Grid;
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

namespace MedicalLink.Dashboard.BCBenhNhanNoiTru
{
    public partial class BCBenhNhanNoiTruFullSize : Form
    {
        public BCBenhNhanNoiTruFullSize()
        {
            InitializeComponent();
        }
        public BCBenhNhanNoiTruFullSize(DataView dataBCTongTheKhoa)
        {
            InitializeComponent();
            gridControlDataBNNT.DataSource = dataBCTongTheKhoa;
        }

        private void BCBenhNhanNoiTruFullSize_Load(object sender, EventArgs e)
        {
            try
            {
               

            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
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
                    e.Appearance.BackColor = Color.LightPink;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (bandedGridViewDataBNNT.RowCount > 0)
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
                                        bandedGridViewDataBNNT.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        bandedGridViewDataBNNT.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        bandedGridViewDataBNNT.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        bandedGridViewDataBNNT.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        bandedGridViewDataBNNT.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        bandedGridViewDataBNNT.ExportToMht(exportFilePath);
                                        break;
                                    default:
                                        break;
                                }
                                timerThongBao.Start();
                                lblThongBao.Visible = true;
                                lblThongBao.Text = "Export dữ liệu thành công!";
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
                    timerThongBao.Start();
                    lblThongBao.Visible = true;
                    lblThongBao.Text = "Không có dữ liệu!";
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
        private void timerThongBao_Tick(object sender, EventArgs e)
        {
            timerThongBao.Stop();
            lblThongBao.Visible = false;
        }

    }
}
