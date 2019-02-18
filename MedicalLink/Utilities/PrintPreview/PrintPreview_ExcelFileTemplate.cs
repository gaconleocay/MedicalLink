using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DevExpress.XtraSplashScreen;

namespace MedicalLink.Utilities.PrintPreview
{
    public static class PrintPreview_ExcelFileTemplate
    {
        public static void UsingExcelTemplate(string fileNameTemplate, List<ClassCommon.reportExcelDTO> thongTinThem, DataTable dataTable)
        {
            try
            {
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                MemoryStream streammemory = export.ExportExcelTemplate_ToStream("",fileNameTemplate, thongTinThem, dataTable);

                DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetControl = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                spreadsheetControl.AllowDrop = false;
                spreadsheetControl.LoadDocument(streammemory, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
                DevExpress.Spreadsheet.IWorkbook workbook = spreadsheetControl.Document;
                spreadsheetControl.ShowRibbonPrintPreview();
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        public static void UsingExcelTemplate(string fileNameTemplate, List<ClassCommon.reportExcelDTO> thongTinThem)
        {
            try
            {
                Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                MemoryStream streammemory = export.ExportExcelTemplate_ToStream("", fileNameTemplate, thongTinThem);

                DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetControl = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
                spreadsheetControl.AllowDrop = false;
                spreadsheetControl.LoadDocument(streammemory, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
                DevExpress.Spreadsheet.IWorkbook workbook = spreadsheetControl.Document;
                spreadsheetControl.ShowRibbonPrintPreview();
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
