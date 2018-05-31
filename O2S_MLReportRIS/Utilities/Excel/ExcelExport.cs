using DevExpress.XtraGrid.Views.Grid;
using O2S_Common.DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MLReportRIS.Utilities.Excel
{
    public static class ExcelExport
    {
        public static void ExportExcelTemplate(string pv_sErr, string fileNameTemplate, List<reportExcelDTO> thongTinThem, DataTable dataTable)
        {
            try
            {
                //thongTinThem.AddRange(ThemThongTinTemplate.ThemThongTin());//them thong tin
                O2S_Common.Excel.ExcelExport.ExportExcelTemplate(pv_sErr, fileNameTemplate, thongTinThem, dataTable);
            }
            catch (Exception ex)
            {
                O2S_Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
