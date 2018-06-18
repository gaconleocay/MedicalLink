using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.Utilities
{
  public static  class DateTimes
    {
        /// <summary>
        /// Lấy ra ngày đầu tiên trong tháng có chứa 
        /// 1 ngày bất kỳ được truyền vào
        /// </summary>
        /// <param name="dtDate">Ngày nhập vào</param>
        /// <returns>Ngày đầu tiên trong tháng</returns>
        public static DateTime GetFirstDayOfMonth(System.DateTime dtInput)
        {
            System.DateTime dtResult = dtInput;
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày đầu tiên trong tháng được truyền vào 
        /// là 1 số nguyên từ 1 đến 12
        /// </summary>
        /// <param name="iMonth">Thứ tự của tháng trong năm</param>
        /// <returns>Ngày đầu tiên trong tháng</returns>
        public static DateTime GetFirstDayOfMonth(int iMonth)
        {
            System.DateTime dtResult = new System.DateTime(System.DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }

        /// <summary>
        /// Lấy ra ngày cuối cùng trong tháng có chứa 
        /// 1 ngày bất kỳ được truyền vào
        /// </summary>
        /// <param name="dtInput">Ngày nhập vào</param>
        /// <returns>Ngày cuối cùng trong tháng</returns>
        public static DateTime GetLastDayOfMonth(System.DateTime dtInput)
        {
            System.DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        /// <summary>
        /// Lấy ra ngày cuối cùng trong tháng được truyền vào
        /// là 1 số nguyên từ 1 đến 12
        /// </summary>
        /// <param name="iMonth"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(int iMonth)
        {
            System.DateTime dtResult = new System.DateTime(System.DateTime.Now.Year, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
    }
}
