using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Globalization;
using System.IO;
using MedicalLink.Base;
using DevExpress.XtraGrid.Views.Grid;
using MedicalLink.ClassCommon;
using DevExpress.Utils.Menu;
using MedicalLink.Utilities.GridControl;
using MedicalLink.DatabaseProcess;
using DevExpress.XtraPrinting;

namespace MedicalLink.BaoCao
{
    public partial class ucBangKeTongHopHoaDon : UserControl
    {
        #region Declaration
        MedicalLink.Base.ConnectDatabase condb = new MedicalLink.Base.ConnectDatabase();
        private DataTable dataDanhSachSo { get; set; }
        #endregion

        #region Load
        public ucBangKeTongHopHoaDon()
        {
            InitializeComponent();
        }

        private void ucBangKeTongHopHoaDon_Load(object sender, EventArgs e)
        {
            dateTuNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dateDenNgay.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
        }

        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(MedicalLink.ThongBao.WaitForm1));
            try
            {
                gridControlDSHoaDon.DataSource = null;
                this.dataDanhSachSo = new DataTable();

                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

                int billgrouptype = 2;
                if (cboLoaiSo.Text == "Tổng hợp")
                {
                    billgrouptype = 0;
                }
                else if (cboLoaiSo.Text == "Tạm ứng")
                {
                    billgrouptype = 1;
                }

                string sql_getdata = "select ROW_NUMBER () OVER (ORDER BY big.billgroupdate) as stt, big.billgroupcode, big.billgroupdate, big.sophieusudung, (big.sophieufrom || '-' || big.sophieuto) sophieutu_den, string_agg(case when b.dahuyphieu=1 then b.billcode end, '; ') as billcode_huy, sum(case when b.dahuyphieu=0 then b.datra else 0 end) as tongtien_thu from (select billgroupcode,dahuyphieu,datra,billcode from bill where billdate between '" + tungay + "' and '" + denngay + "') b inner join billgroup big on big.billgroupcode=b.billgroupcode and billgrouptype=" + billgrouptype + " group by big.billgroupcode,big.sophieufrom,big.sophieuto,big.billgroupdate,big.sophieusudung;";

                this.dataDanhSachSo = condb.GetDataTable_HIS(sql_getdata);

                if (this.dataDanhSachSo != null && this.dataDanhSachSo.Rows.Count > 0)
                {
                    gridControlDSHoaDon.DataSource = this.dataDanhSachSo;
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
            SplashScreenManager.CloseForm();
        }

        #region Xuat bao cao
        private void tbnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDSHoaDon.RowCount > 0)
                {
                    string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                    string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                    string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                    List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                    ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                    reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                    reportitem.value = tungaydenngay;

                    thongTinThem.Add(reportitem);
                    string fileTemplatePath = "BC_BangKeTongHopHoaDon.xlsx";
                    Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                    export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, this.dataDanhSachSo);
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        #endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string tungay = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");
                string denngay = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("HH:mm dd/MM/yyyy");

                string tungaydenngay = "( Từ " + tungay + " - " + denngay + " )";

                List<ClassCommon.reportExcelDTO> thongTinThem = new List<ClassCommon.reportExcelDTO>();
                ClassCommon.reportExcelDTO reportitem = new ClassCommon.reportExcelDTO();
                reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                reportitem.value = tungaydenngay;

                thongTinThem.Add(reportitem);
                string fileTemplatePath = "BC_BangKeTongHopHoaDon.xlsx";
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                export.ExportExcelTemplate_ReportTemps("", fileTemplatePath, thongTinThem, this.dataDanhSachSo);

                DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetControl = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                spreadsheetControl.AllowDrop = false;
                string fullPath = Environment.CurrentDirectory + "\\ReportTemps\\" + fileTemplatePath;
                spreadsheetControl.LoadDocument(fullPath, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
                DevExpress.Spreadsheet.IWorkbook workbook = spreadsheetControl.Document;
                spreadsheetControl.ShowRibbonPrintPreview();
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Error(ex);
            }
        }

        private void gridViewDSHoaDon_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn(ex);
            }
        }
    }
}
