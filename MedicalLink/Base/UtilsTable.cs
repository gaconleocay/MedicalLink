using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.Base
{
    /*
     Mình viết một lớp Utils để lấy ra wrapper cho table phục vụ cho mục đích hiển thị.
Hàm này có thể tùy biến với các tham số đầu vào mà cho ra field hiển thị mong muốn.
     */
    internal class UtilsTable
    {
        /// <summary>
        /// Get wrapper table for displaying purpose!
        /// </summary>
        /// <param name="core">Original table</param>
        /// <param name="seperateString">A string for seperating members you want to display</param>
        /// <param name="diplayMember">A name for display member</param>
        /// <param name="args">Input members for creating display member</param>
        /// <returns>Display wrapper of input table</returns>
        public static DataTable getTableDisplayWrapper(DataTable core, string seperateString, string diplayMember, params string[] args)
        {
            DataTable wrapper = new DataTable();
            // Make new display column 
            wrapper.Columns.Add(new DataColumn(diplayMember, Type.GetType("System.String")));
            // Copy columns from original table
            foreach (DataColumn columnCore in core.Columns)
            {
                wrapper.Columns.Add(new DataColumn(columnCore.ColumnName, columnCore.DataType));
            }
            // Copy rows from original table with a new display cell for each row
            foreach (DataRow rowCore in core.Rows)
            {
                DataRow rowWrapper = wrapper.NewRow();
                string displayCell = "";
                // Create display cell
                foreach (string arg in args)
                {
                    try
                    {
                        displayCell += rowCore[arg] + seperateString;
                    }
                    catch (ArgumentException)
                    {
                        throw new Exception("Invalid data field");
                    }
                }
                displayCell = displayCell.Remove(displayCell.Length - seperateString.Length);
                rowWrapper[diplayMember] = displayCell;
                // Copy cells from original row
                foreach (DataColumn columnCore in core.Columns)
                {
                    // Avoid to make a shallow copy
                    rowWrapper[columnCore.ColumnName] = rowCore[columnCore].ToString();
                }
                wrapper.Rows.Add(rowWrapper);
            }
            return wrapper;
        }

    }
}
