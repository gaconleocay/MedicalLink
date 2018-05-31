using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MLReportRIS.Utilities.Prints
{
    public static class PrintPreview
    {
        public static void ShowPrintPreview_UsingExcelTemplate(string fileNameTemplate, List<O2S_Common.DataObjects.reportExcelDTO> thongTinThem, DataTable dataTable)
        {
            try
            {
               // thongTinThem.AddRange(ThemThongTinTemplate.ThemThongTin());//them thong tin
                O2S_Common.PrintPreview.ExcelFileTemplate.ShowPrintPreview_UsingExcelTemplate(fileNameTemplate, thongTinThem, dataTable);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }
    }

}
