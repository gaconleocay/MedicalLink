using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedicalLink.Base
{
    public static class classCheckInputString
    {
        /// <summary>
        /// Kiem tra 1 chuoi nhap vao la so??
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public static bool CheckISNumber(string candidate)
        {
            try
            {
                double num;
                if (double.TryParse(candidate, out num))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiem tra 1 chuoi nhap vao la dang Datetime: yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static bool CheckFormatDatetime(string datetime)
        {
            try
            {
                //string[] partNumbers = { "1298-673-4192", "A08Z-931-468A", 
                //              "_A90-123-129X", "12345-KKA-1230", 
                //              "0919-2893-1256" };
                //Regex rgx = new Regex(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");
                //foreach (string partNumber in partNumbers)
                //    Console.WriteLine("{0} {1} a valid part number.",
                //                      partNumber,
                //                      rgx.IsMatch(partNumber) ? "is" : "is not");
                Regex regex = new Regex(@"^\d{4}(-\d{2})(-\d{2})( \d{2})(:\d{2})(:\d{2})$");
                if (regex.IsMatch(datetime))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
