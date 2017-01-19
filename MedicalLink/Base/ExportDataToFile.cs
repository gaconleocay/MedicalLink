using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace MedicalLink.Base
{
    public static class ExportDataToFile
    {
        public static bool ExportDataTable_ToExcel(DataTable dt, string cTieuDe_BC)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel file (*.xlsx)|*.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                string strFileName = f.FileName;
                if (File.Exists(strFileName) == true)
                    if (MessageBox.Show("File đã tồn tại, bạn có muốn ghi đè lên không ?", "Export to Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        File.Delete(strFileName);
                    else
                        return false;
                //khoi tao cac doi tuong Com Excel de lam viec
                Excel.Application xlApp;
                Excel.Worksheet xlSheet;
                Excel.Workbook xlBook;
                //doi tuong Trống để thêm  vào xlApp sau đó lưu lại sau
                object missValue = System.Reflection.Missing.Value;
                //khoi tao doi tuong Com Excel moi
                xlApp = new Excel.Application();
                xlBook = xlApp.Workbooks.Add(missValue);
                //su dung Sheet dau tien de thao tac
                xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(1);
                //không cho hiện ứng dụng Excel lên để tránh gây đơ máy
                xlApp.ReferenceStyle = Excel.XlReferenceStyle.xlA1;
                xlApp.Visible = false;
                int socot = dt.Columns.Count;
                int sohang = dt.Rows.Count;
                int i, j;

                //Trung them để kiểm tra khi cột tới AA, nghĩa là trên 26(cột Z) cột
                string cToi_Cot = socot > 26 ? "A" + Convert.ToChar(socot - 26 - 1 + 65) + "" : Convert.ToChar(socot + 65) + "";

                //set thuoc tinh cho tieu de
                xlSheet.get_Range("A1", cToi_Cot + "1").Merge(false);
                Excel.Range caption = xlSheet.get_Range("A1", cToi_Cot + "1");
                caption.Select();
                caption.FormulaR1C1 = cTieuDe_BC;
                //căn lề cho tiêu đề
                caption.HorizontalAlignment = Excel.Constants.xlCenter;
                caption.Font.Bold = true;
                caption.VerticalAlignment = Excel.Constants.xlCenter;
                caption.Font.Size = 15;
                //màu nền cho tiêu đề
                caption.Interior.ColorIndex = 20;
                caption.RowHeight = 30;
                //set thuoc tinh cho cac header
                Excel.Range header = xlSheet.get_Range("A2", cToi_Cot + "2");
                header.Select();

                header.HorizontalAlignment = Excel.Constants.xlCenter;
                header.Font.Bold = true;
                header.Font.Size = 10;
                //điền tiêu đề cho các cột trong file excel
                for (i = 0; i < socot; i++)
                    xlSheet.Cells[2, i + 1] = dt.Columns[i].ColumnName;
                ////dien cot stt
                //xlSheet.Cells[2, 1] = "STT";
                //for (i = 0; i < sohang; i++)
                //    xlSheet.Cells[i + 3, 1] = i + 1;
                //dien du lieu vao sheet



                for (i = 0; i < sohang; i++)
                    for (j = 0; j < socot; j++)
                    {
                        if (dt.Rows[i][j].GetType().Name == "String") //Trung them de kiem tra số dạng chuỗi tránh mất số 0 đầu chuỗi
                            xlSheet.Cells[i + 3, j + 1] = "'" + dt.Rows[i][j];
                        else
                            xlSheet.Cells[i + 3, j + 1] = dt.Rows[i][j];
                    }
                //autofit độ rộng cho các cột
                //for (i = 0; i < sohang; i++)
                //    ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();

                xlSheet.Columns.AutoFit();
                //save file
                //xlBook.SaveAs(strFileName);
                xlBook.SaveAs(strFileName, Excel.XlFileFormat.xlWorkbookDefault, missValue, missValue, missValue, missValue, Excel.XlSaveAsAccessMode.xlExclusive, missValue, missValue, missValue, missValue, missValue);
                xlBook.Close(true);
                releaseObject(xlSheet);
                releaseObject(xlBook);
                xlApp.Quit();

                // release cac doi tuong COM                            
                releaseObject(xlApp);
                GC.Collect();
                MessageBox.Show("Đã xuất ra tập tin Excel tại : " + (char)13 + strFileName, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;

        }

        public static bool ExportDataTable_ToExcel(DataGridView grd, string cTieuDe_BC)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel file (*.xlsx)|*.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                string strFileName = f.FileName;
                //if (File.Exists(strFileName) == true)
                //    if (MessageBox.Show("File đã tồn tại, bạn có muốn ghi đè lên không ?", "Export to Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //        File.Delete(strFileName);
                //    else
                //        return false;                
                //khoi tao cac doi tuong Com Excel de lam viec
                Excel.Application xlApp;
                Excel.Worksheet xlSheet;
                Excel.Workbook xlBook;
                //doi tuong Trống để thêm  vào xlApp sau đó lưu lại sau
                object missValue = System.Reflection.Missing.Value;
                //khoi tao doi tuong Com Excel moi
                xlApp = new Excel.Application();
                xlBook = xlApp.Workbooks.Add(missValue);
                //su dung Sheet dau tien de thao tac
                xlSheet = (Excel.Worksheet)xlBook.Worksheets.get_Item(1);
                //không cho hiện ứng dụng Excel lên để tránh gây đơ máy
                xlApp.ReferenceStyle = Excel.XlReferenceStyle.xlA1;
                xlApp.Visible = false;
                int socot = grd.Columns.Count;
                int sohang = grd.Rows.Count;
                int i, j;

                //Trung them để kiểm tra khi cột tới AA, nghĩa là trên 26(cột Z) cột
                string cToi_Cot = socot > 26 ? "A" + Convert.ToChar(socot - 26 - 1 + 65) + "" : Convert.ToChar(socot + 65) + "";

                //set thuoc tinh cho tieu de
                xlSheet.get_Range("A1", cToi_Cot + "1").Merge(false);
                Excel.Range caption = xlSheet.get_Range("A1", cToi_Cot + "1");
                caption.Select();
                caption.FormulaR1C1 = cTieuDe_BC;
                //căn lề cho tiêu đề
                caption.HorizontalAlignment = Excel.Constants.xlCenter;
                caption.Font.Bold = true;
                caption.VerticalAlignment = Excel.Constants.xlCenter;
                caption.Font.Size = 15;
                //màu nền cho tiêu đề
                caption.Interior.ColorIndex = 20;
                caption.RowHeight = 30;
                //set thuoc tinh cho cac header
                Excel.Range header = xlSheet.get_Range("A2", cToi_Cot + "2");
                header.Select();

                header.HorizontalAlignment = Excel.Constants.xlCenter;
                header.Font.Bold = true;
                header.Font.Size = 10;
                //điền tiêu đề cho các cột trong file excel
                for (i = 0; i < socot; i++)
                    xlSheet.Cells[2, i + 1] = grd.Columns[i].HeaderText;
                ////dien cot stt
                //xlSheet.Cells[2, 1] = "STT";
                //for (i = 0; i < sohang; i++)
                //    xlSheet.Cells[i + 3, 1] = i + 1;
                //dien du lieu vao sheet


                for (i = 0; i < sohang; i++)
                    for (j = 0; j < socot; j++)
                    {
                        if (grd.Rows[i].Cells[j].Value.GetType().Name == "String") //Trung them de kiem tra số dạng chuỗi tránh mất số 0 đầu chuỗi
                            xlSheet.Cells[i + 3, j + 1] = "'" + grd.Rows[i].Cells[j].Value;
                        else
                            xlSheet.Cells[i + 3, j + 1] = grd.Rows[i].Cells[j].Value;

                    }
                //autofit độ rộng cho các cột
                //for (i = 0; i < sohang; i++)
                //    ((Excel.Range)xlSheet.Cells[1, i + 1]).EntireColumn.AutoFit();

                xlSheet.Columns.AutoFit();
                //save file
                //xlBook.SaveAs(strFileName);
                xlBook.SaveAs(strFileName, Excel.XlFileFormat.xlWorkbookDefault, missValue, missValue, missValue, missValue, Excel.XlSaveAsAccessMode.xlExclusive, missValue, missValue, missValue, missValue, missValue);
                xlBook.Close(true);
                releaseObject(xlSheet);
                releaseObject(xlBook);
                xlApp.Quit();

                // release cac doi tuong COM                            
                releaseObject(xlApp);
                GC.Collect();
                MessageBox.Show("Đã xuất ra tập tin Excel tại : " + (char)13 + strFileName, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;

        }
        public static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                throw new Exception("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Xuat du lieu tu GridView ra file Excel,pdf...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ExportDataGridViewToFile(DevExpress.XtraGrid.GridControl gridControlData, DevExpress.XtraGrid.Views.Grid.GridView gridViewData)
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
                            MessageBox.Show("Export dữ liệu thành công!", "Thông báo !");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra" + ex.ToString(), "Thông báo !");
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo !");
            }
        }

    }
}
