using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
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


namespace MedicalLink.BaoCao.BCXuatThuocNhaThuoc
{
    public partial class BCXuatThuocNhaThuocDetail : Form
    {
        #region Khai bao
        private long medicinestorebillid_cd { get; set; }
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        #endregion

        public BCXuatThuocNhaThuocDetail()
        {
            InitializeComponent();
        }
        public BCXuatThuocNhaThuocDetail(long medicinestorebillid)
        {
            try
            {
                InitializeComponent();
                this.medicinestorebillid_cd = medicinestorebillid;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #region Load
        private void BCXuatThuocNhaThuocDetail_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                if (this.medicinestorebillid_cd != 0)
                {
                    LoadDataToGrid();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void LoadDataToGrid()
        {
            try
            {
                string sqlGetData = "SELECT ROW_NUMBER () OVER (ORDER BY mef.medicinename) as stt, me.medicinestorebillcode, me.medicinestorebillid, mef.medicinecode, mef.medicinename, mef.solo, mef.sodangky, me.accept_money, me.approve_soluong as soluong_chidinh, me.accept_soluong as soluong_xuat, me.accept_soluong * me.accept_money as thanhtien FROM medicine me INNER JOIN medicine_ref mef on me.medicinerefid=mef.medicinerefid WHERE me.medicinestorebillid=" + this.medicinestorebillid_cd + ";";
                DataTable dataExport = condb.GetDataTable(sqlGetData);
                gridControlPhieuThuocDetail.DataSource = dataExport;
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }

        #endregion

        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewPhieuThuocDetail.RowCount > 0)
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
                                        gridViewPhieuThuocDetail.ExportToXls(exportFilePath);
                                        break;
                                    case ".xlsx":
                                        gridViewPhieuThuocDetail.ExportToXlsx(exportFilePath);
                                        break;
                                    case ".rtf":
                                        gridViewPhieuThuocDetail.ExportToRtf(exportFilePath);
                                        break;
                                    case ".pdf":
                                        gridViewPhieuThuocDetail.ExportToPdf(exportFilePath);
                                        break;
                                    case ".html":
                                        gridViewPhieuThuocDetail.ExportToHtml(exportFilePath);
                                        break;
                                    case ".mht":
                                        gridViewPhieuThuocDetail.ExportToMht(exportFilePath);
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
        private void gridViewBNDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }



    }
}
