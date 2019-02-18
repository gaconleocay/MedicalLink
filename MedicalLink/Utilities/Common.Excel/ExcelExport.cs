using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using System.IO;
using System.Windows.Forms;

namespace MedicalLink.Utilities.Common.Excel
{
    public class ExcelExport
    {
        #region Process_TMP
        private DataTable InsertOrders(List<ClassCommon.reportExcelDTO> thongTinThem)
        {
            DataTable orderTable = new DataTable("DATA");
            try
            {
                if (thongTinThem != null && thongTinThem.Count > 0)
                {
                    foreach (var item_name in thongTinThem)
                    {
                        orderTable.Columns.Add(item_name.name, typeof(string));
                    }

                    DataRow newRow = orderTable.NewRow();
                    foreach (var item_value in thongTinThem)
                    {
                        newRow[item_value.name] = item_value.value;
                    }

                    orderTable.Columns.Add("CURRENTDATETIME", typeof(string));
                    newRow["CURRENTDATETIME"] = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute + " ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;

                    orderTable.Columns.Add("CURRENTDATE", typeof(string));
                    newRow["CURRENTDATE"] = "Ngày " + System.DateTime.Now.Day + " tháng " + System.DateTime.Now.Month + " năm " + System.DateTime.Now.Year;

                    orderTable.Columns.Add("SOYTE", typeof(string));
                    newRow["SOYTE"] = GlobalStore.SoYTe_String;

                    orderTable.Columns.Add("TENBENHVIEN", typeof(string));
                    newRow["TENBENHVIEN"] = GlobalStore.TenBenhVien_String;

                    orderTable.Columns.Add("CURRENTUSER", typeof(string));
                    newRow["CURRENTUSER"] = Base.SessionLogin.SessionUsername;

                    orderTable.Rows.Add(newRow);
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
            return orderTable;
        }

        #endregion


        public void ExportExcelTemplate(string pv_sErr, string fileNameTemplate, List<ClassCommon.reportExcelDTO> thongTinThem, DataTable dataTable)
        {
            try
            {
                DataSet dataExportExcel = new DataSet();
                dataExportExcel.Tables.Add(InsertOrders(thongTinThem));

                DataTable dataTableCopy = dataTable.Copy();
                dataExportExcel.Tables.Add(dataTableCopy);

                string fileTemplatePath = Environment.CurrentDirectory + "\\Templates\\" + fileNameTemplate;
                WorkbookDesigner designer;
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel 2010 (.xlsx)|*.xlsx |Excel 2003 (.xls)|*.xls|Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string exportFilePath = saveDialog.FileName;
                        string fileExtenstion = new FileInfo(exportFilePath).Extension;

                        if (File.Exists(fileTemplatePath))
                        {
                            designer = new WorkbookDesigner();
                            string strRoot = Environment.CurrentDirectory + "\\Library\\";
                            Aspose.Cells.License l = new Aspose.Cells.License();
                            string strLicense = strRoot + "Aspose.Cells.lic";
                            l.SetLicense(strLicense);
                            designer.Open(fileTemplatePath);
                            if (dataExportExcel.Tables.Count > 0)
                            {
                                dataExportExcel.Tables[0].TableName = "DATA";
                                for (int i = 1; i < dataExportExcel.Tables.Count; i++)
                                {
                                    dataExportExcel.Tables[i].TableName = "DATA" + i;
                                }
                                designer.SetDataSource(dataExportExcel);
                                designer.Process();
                                designer.Workbook.CalculateFormula();
                                switch (fileExtenstion)
                                {
                                    case ".xls":
                                        designer.Workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Excel97To2003));
                                        break;
                                    case ".xlsx":
                                        designer.Workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Xlsx));
                                        break;
                                    case ".pdf":
                                        designer.Workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Pdf));
                                        break;
                                    case ".html":
                                        designer.Workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Html));
                                        break;
                                    default:
                                        designer.Workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Excel97To2003));
                                        break;
                                }
                                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                                frmthongbao.Show();
                            }
                        }
                        else
                        {
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_TEMPLATE_BAO_CAO);
                            frmthongbao.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
                MessageBox.Show("Export dữ liệu thất bại!", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcelTemplate(string pv_sErr, string fileNameTemplate, List<ClassCommon.reportExcelDTO> thongTinThem, List<DataTable> lstdataTable)
        {
            try
            {
                DataSet dataExportExcel = new DataSet();
                dataExportExcel.Tables.Add(InsertOrders(thongTinThem));

                foreach (var item in lstdataTable)
                {
                    DataTable dataTableCopy = item.Copy();
                    dataExportExcel.Tables.Add(dataTableCopy);
                }

                string fileTemplatePath = Environment.CurrentDirectory + "\\Templates\\" + fileNameTemplate;
                WorkbookDesigner designer;
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel 2010 (.xlsx)|*.xlsx |Excel 2003 (.xls)|*.xls|Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string exportFilePath = saveDialog.FileName;
                        string fileExtenstion = new FileInfo(exportFilePath).Extension;

                        if (File.Exists(fileTemplatePath))
                        {
                            designer = new WorkbookDesigner();
                            string strRoot = Environment.CurrentDirectory + "\\Library\\";
                            Aspose.Cells.License l = new Aspose.Cells.License();
                            string strLicense = strRoot + "Aspose.Cells.lic";
                            l.SetLicense(strLicense);
                            designer.Open(fileTemplatePath);
                            if (dataExportExcel.Tables.Count > 0)
                            {
                                dataExportExcel.Tables[0].TableName = "DATA";
                                for (int i = 1; i < dataExportExcel.Tables.Count; i++)
                                {
                                    dataExportExcel.Tables[i].TableName = "DATA" + i;
                                }
                                designer.SetDataSource(dataExportExcel);
                                designer.Process();
                                designer.Workbook.CalculateFormula();
                                switch (fileExtenstion)
                                {
                                    case ".xls":
                                        designer.Workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Excel97To2003));
                                        break;
                                    case ".xlsx":
                                        designer.Workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Xlsx));
                                        break;
                                    case ".pdf":
                                        designer.Workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Pdf));
                                        break;
                                    case ".html":
                                        designer.Workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Html));
                                        break;
                                    default:
                                        designer.Workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Excel97To2003));
                                        break;
                                }
                                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                                frmthongbao.Show();
                            }
                        }
                        else
                        {
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_TIM_THAY_TEMPLATE_BAO_CAO);
                            frmthongbao.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
                MessageBox.Show("Export dữ liệu thất bại!", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void ExportDataGridViewToFile(DevExpress.XtraGrid.GridControl gridControlData, DevExpress.XtraGrid.Views.Grid.GridView gridViewData)
        {
            if (gridViewData.RowCount > 0)
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
                                    gridControlData.ExportToXls(exportFilePath);
                                    break;
                                case ".xlsx":
                                    gridControlData.ExportToXlsx(exportFilePath);
                                    break;
                                case ".rtf":
                                    gridControlData.ExportToRtf(exportFilePath);
                                    break;
                                case ".pdf":
                                    gridControlData.ExportToPdf(exportFilePath);
                                    break;
                                case ".html":
                                    gridControlData.ExportToHtml(exportFilePath);
                                    break;
                                case ".mht":
                                    gridControlData.ExportToMht(exportFilePath);
                                    break;
                                default:
                                    break;
                            }
                            O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                            frmthongbao.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                     O2S_Common.Logging.LogSystem.Error(ex);
                    MessageBox.Show("Export dữ liệu thất bại!", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.KHONG_CO_DU_LIEU);
                frmthongbao.Show();
            }
        }

        public MemoryStream ExportExcelTemplate_ToStream(string pv_sErr, string fileNameTemplate, List<ClassCommon.reportExcelDTO> thongTinThem, DataTable dataTable)
        {
            MemoryStream result = new MemoryStream();
            try
            {
                DataSet dataExportExcel = new DataSet();
                dataExportExcel.Tables.Add(InsertOrders(thongTinThem));

                DataTable dataTableCopy = dataTable.Copy();
                dataExportExcel.Tables.Add(dataTableCopy);

                string fileTemplatePath = Environment.CurrentDirectory + "\\Templates\\" + fileNameTemplate;
                WorkbookDesigner designer;

                if (File.Exists(fileTemplatePath))
                {
                    designer = new WorkbookDesigner();
                    string strRoot = Environment.CurrentDirectory + "\\Library\\";
                    Aspose.Cells.License l = new Aspose.Cells.License();
                    string strLicense = strRoot + "Aspose.Cells.lic";
                    l.SetLicense(strLicense);
                    designer.Open(fileTemplatePath);
                    if (dataExportExcel.Tables.Count > 0)
                    {
                        dataExportExcel.Tables[0].TableName = "DATA";
                        for (int i = 1; i < dataExportExcel.Tables.Count; i++)
                        {
                            dataExportExcel.Tables[i].TableName = "DATA" + i;
                        }
                        designer.SetDataSource(dataExportExcel);
                        designer.Process();
                        designer.Workbook.CalculateFormula();
                        designer.Workbook.Save(result, new XlsSaveOptions(SaveFormat.Xlsx));
                    }
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public MemoryStream ExportExcelTemplate_ToStream(string pv_sErr, string fileNameTemplate, List<ClassCommon.reportExcelDTO> thongTinThem)
        {
            MemoryStream result = new MemoryStream();
            try
            {
                DataSet dataExportExcel = new DataSet();
                dataExportExcel.Tables.Add(InsertOrders(thongTinThem));

                string fileTemplatePath = Environment.CurrentDirectory + "\\Templates\\" + fileNameTemplate;
                WorkbookDesigner designer;

                if (File.Exists(fileTemplatePath))
                {
                    designer = new WorkbookDesigner();
                    string strRoot = Environment.CurrentDirectory + "\\Library\\";
                    Aspose.Cells.License l = new Aspose.Cells.License();
                    string strLicense = strRoot + "Aspose.Cells.lic";
                    l.SetLicense(strLicense);
                    designer.Open(fileTemplatePath);
                    if (dataExportExcel.Tables.Count > 0)
                    {
                        dataExportExcel.Tables[0].TableName = "DATA";
                        for (int i = 1; i < dataExportExcel.Tables.Count; i++)
                        {
                            dataExportExcel.Tables[i].TableName = "DATA" + i;
                        }
                        designer.SetDataSource(dataExportExcel);
                        designer.Process();
                        designer.Workbook.CalculateFormula();
                        designer.Workbook.Save(result, new XlsSaveOptions(SaveFormat.Xlsx));
                    }
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public void ExportExcelNotTemplate(string _tenBaoCao, DataTable dataTable)
        {
            try
            {
                int iRowStartImport = 2;

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel 2010 (.xlsx)|*.xlsx |Excel 2003 (.xls)|*.xls|Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string exportFilePath = saveDialog.FileName;
                        string fileExtenstion = new FileInfo(exportFilePath).Extension;

                        //if (File.Exists(fileTemplatePath))
                        //{
                        Workbook _workbook = new Workbook();
                        Worksheet _worksheet = _workbook.Worksheets[0];

                        //style cho tieu de
                        Style style_C1 = _worksheet.Cells["C1"].GetStyle();
                        style_C1.Font.IsBold = true;
                        style_C1.Font.Name = "Times New Roman";
                        style_C1.Font.Size = 15;

                        _worksheet.Cells["C1"].PutValue(_tenBaoCao);
                        _worksheet.Cells["C1"].SetStyle(style_C1);

                        _worksheet.Cells.ImportDataTable(dataTable, true, "A3");
                        _worksheet.AutoFitColumns();

                        Cells cells = _workbook.Worksheets[0].Cells;
                        Style style = default(Style);
                        for (int iCol = 0; iCol <= dataTable.Columns.Count - 1; iCol++)
                        {
                            var cell = cells[iRowStartImport, iCol];

                            ExportExcelNoTemplate_SetHeaderValue(cell.GetStyle(), iCol, iRowStartImport, dataTable.Columns[iCol].Caption, cells);

                            for (int iRow = 0; iRow <= dataTable.Rows.Count - 1; iRow++)
                            {
                                style = cells[(int)(iRowStartImport + iRow + 1), iCol].GetStyle();
                                cell = cells[(int)(iRowStartImport + iRow + 1), iCol];

                                //if (isGroup && dataTable.Columns[iCol].ColumnName.Contains("GROUP_"))
                                //{
                                //    ExportExcelNoTemplate_SetStyle(style, StyleType.Group);
                                //    cell.PutValue(dataTable.Rows[iRow][iCol]);
                                //    cell.SetStyle(style);
                                //}
                                //else
                                //{
                                if (dataTable.Columns[iCol].ColumnName.ToUpper() == "STT")
                                {
                                    double? value = null;
                                    ExportExcelNoTemplate_SetStyle(style, StyleType.Stt);
                                    if (dataTable.Rows[iRow][iCol] != DBNull.Value)
                                    {
                                        value = double.Parse(System.Convert.ToString(dataTable.Rows[iRow][iCol]));
                                    }
                                    cell.PutValue(value);
                                    cell.SetStyle(style);
                                }
                                else
                                {
                                    style.VerticalAlignment = TextAlignmentType.Center;
                                    ExportExcelNoTemplate_SetBodyValue(dataTable.Columns[iCol].DataType, dataTable.Rows[iRow][iCol],
                                        style, iCol, iRow, iRowStartImport, cells);

                                }
                            }
                        }
                        //
                        switch (fileExtenstion)
                        {
                            case ".xls":
                                _workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Excel97To2003));
                                break;
                            case ".xlsx":
                                _workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Xlsx));
                                break;
                            case ".pdf":
                                _workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Pdf));
                                break;
                            case ".html":
                                _workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Html));
                                break;
                            default:
                                _workbook.Save(exportFilePath, new XlsSaveOptions(SaveFormat.Excel97To2003));
                                break;
                        }
                        O2S_Common.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_Common.Utilities.ThongBao.frmThongBao(MedicalLink.Base.ThongBaoLable.EXPORT_DU_LIEU_THANH_CONG);
                        frmthongbao.Show();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
                MessageBox.Show("Export dữ liệu thất bại!", "Thông báo !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #region Process
        private static void ExportExcelNoTemplate_SetHeaderValue(Style style, decimal iCol, decimal iRowStartImport, string HeaderText, Cells cells)
        {
            try
            {
                ExportExcelNoTemplate_SetStyle(style, StyleType.Header);
                cells[(int)iRowStartImport, (int)iCol].PutValue(HeaderText);
                cells[(int)iRowStartImport, (int)iCol].SetStyle(style);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        private static Style ExportExcelNoTemplate_SetStyle(Style style, ExcelExport.StyleType type)
        {
            try
            {
                style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                switch (type)
                {
                    case StyleType.Header:
                        style.Font.IsBold = true;
                        style.Font.Size++;
                        style.BackgroundColor = System.Drawing.Color.BlanchedAlmond;
                        style.HorizontalAlignment = TextAlignmentType.Center;
                        break;
                    case StyleType.Group:
                        style.Font.IsBold = true;
                        style.HorizontalAlignment = TextAlignmentType.Left;
                        break;
                    case StyleType.Sum:
                        style.Font.IsBold = true;
                        style.HorizontalAlignment = TextAlignmentType.Center;
                        break;
                    case StyleType.Stt:
                        style.HorizontalAlignment = TextAlignmentType.Center;
                        break;
                    case StyleType.Body:
                        break;
                    case StyleType.None:
                        style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.None;
                        style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.None;
                        style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.None;
                        style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.None;
                        break;
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
            return default(Style);
        }
        private enum StyleType
        {
            None = 0,
            Header = 1,
            Group = 2,
            Sum = 3,
            Stt = 4,
            Body = 5
        }

        private static void ExportExcelNoTemplate_SetBodyValue(Type type, object value, Style style, int iCol, int iRow, int iRowStartImport, Cells cells)
        {
            try
            {
                ExportExcelNoTemplate_SetStyle(style, StyleType.Body);
                if (type == typeof(System.DateTime) || type == typeof(System.DateTime?))
                {
                    TimeSpan test = new TimeSpan(0, 0, 0);
                    if (value != DBNull.Value && System.Convert.ToDateTime(value).TimeOfDay != test)
                    {
                        //style.Custom = "HH:mm";
                        style.Custom = "hh:mm dd/mm/yyyy";
                    }
                    else
                    {
                        style.Custom = "hh:mm dd/mm/yyyy";
                    }

                    style.HorizontalAlignment = TextAlignmentType.Center;
                }
                else if (type == typeof(Int16) || type == typeof(Int16?) || type == typeof(Int32) || type == typeof(Int32?) || type == typeof(Int64) || type == typeof(Int64?) || type == typeof(long) || type == typeof(long?))
                {
                    style.Number = 0;
                    style.HorizontalAlignment = TextAlignmentType.Right;
                }
                else if (type == typeof(decimal) || type == typeof(decimal?) || type == typeof(double) || type == typeof(double?))
                {
                    style.Number = 43;
                    style.HorizontalAlignment = TextAlignmentType.Right;
                }
                else if ((type == typeof(bool)) || (type == typeof(bool?)))
                {
                    style.Number = 49;
                    style.HorizontalAlignment = TextAlignmentType.Center;
                    if (value != DBNull.Value && value != null)
                    {
                        value = "X";
                    }
                    else
                    {
                        value = "";
                    }
                }
                else
                {
                    style.Number = 49;
                    style.HorizontalAlignment = TextAlignmentType.Left;
                    style.IsTextWrapped = true;
                }

                cells[iRowStartImport + iRow + 1, iCol].PutValue(value);
                cells[iRowStartImport + iRow + 1, iCol].SetStyle(style);
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

        #endregion

    }
}
