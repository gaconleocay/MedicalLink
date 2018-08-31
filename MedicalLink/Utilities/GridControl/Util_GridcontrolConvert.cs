using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DevExpress.XtraGrid.Columns;

namespace MedicalLink.Utilities.GridControl
{
    public static class Util_GridcontrolConvert
    {
        public static DataTable ConvertGridControlToDataTable(DevExpress.XtraGrid.Views.Grid.GridView gridViewData)
        {
            DataTable result = new DataTable();
            try
            {
                foreach (GridColumn column in gridViewData.Columns)
                {
                    if (result.Columns.Contains(column.Caption) == false && column.Caption != null)
                    {
                        if (column.ColumnType.Name.Contains("Nullable"))
                        {
                            if (column.ColumnType.FullName.Contains("Decimal"))
                            {
                                result.Columns.Add(column.Caption, Type.GetType("System.Decimal"));
                            }
                            else
                            {
                                result.Columns.Add(column.Caption, Type.GetType("System.String"));
                            }
                        }
                        else
                        {
                            result.Columns.Add(column.Caption, column.ColumnType);
                        }
                    }
                }
                for (int i = 0; i < gridViewData.DataRowCount; i++)
                {
                    DataRow row = result.NewRow();
                    foreach (GridColumn column in gridViewData.Columns)
                    {
                        //Sap xep lai thu tu cot
                        if (column.Caption.ToLower() == "stt")
                        {
                            row[column.Caption] = i + 1;
                        }
                        else
                        {
                            if (column.ColumnType.FullName.Contains("Decimal"))
                            {
                                row[column.Caption] = gridViewData.GetRowCellValue(i, column) ?? 0;
                            }
                            else
                            {
                                row[column.Caption] = gridViewData.GetRowCellValue(i, column);
                            }
                        }
                    }
                    result.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
            return result;
        }




    }
}
