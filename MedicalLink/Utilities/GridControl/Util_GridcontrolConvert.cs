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
                    if (result.Columns.Contains(column.FieldName) == false && column.FieldName != null)
                    {
                        if (column.ColumnType.Name.Contains("Nullable"))
                        {
                            if (column.ColumnType.FullName.Contains("Decimal"))
                            {
                                result.Columns.Add(column.FieldName, Type.GetType("System.Decimal"));
                            }
                            else
                            {
                                result.Columns.Add(column.FieldName, Type.GetType("System.String"));
                            }
                        }
                        else
                        {
                            result.Columns.Add(column.FieldName, column.ColumnType);
                        }
                    }
                }
                for (int i = 0; i < gridViewData.DataRowCount; i++)
                {
                    DataRow row = result.NewRow();
                    foreach (GridColumn column in gridViewData.Columns)
                    {
                        row[column.FieldName] = gridViewData.GetRowCellValue(i, column);
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
