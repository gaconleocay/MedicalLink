using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.Utilities
{
    public class NumberConvert
    {
        public static string NumberToStringMoney(double money)
        {
            string result = "";
            try
            {
                result = money.ToString("#,##0.0000");
            }
            catch (Exception ex)
            {
                result = "";
            }

            return result;
        }

        public static string NumberToStringMoneyAfterRound(decimal number)
        {
            string result = "";
            try
            {
                result = Math.Round(number, MidpointRounding.AwayFromZero).ToString("#,##0.0000");
            }
            catch (Exception ex)
            {
                result = "";
            }

            return result;
        }

        public static string NumberToStringMoney(double money, string cultureInfo)
        {
            string result = "";
            try
            {
                cultureInfo = (String.IsNullOrWhiteSpace(cultureInfo) ? "vi-VN" : cultureInfo);
                result = money.ToString("#,##0.0000", System.Globalization.CultureInfo.CreateSpecificCulture(cultureInfo));
            }
            catch (Exception ex)
            {
                result = "";
            }
            return result;
        }

        public static string NumberToStringNumberHasSeperator(double money)
        {
            string result = "";
            try
            {
                result = money.ToString("#,##0");
            }
            catch (Exception ex)
            {
                result = "";
            }

            return result;
        }

        public static string NumberToString(decimal number, int numberDigit)
        {
            string result = "";
            string format = "";
            try
            {
                switch (numberDigit)
                {
                    case 0:
                        format = "#,##0";
                        break;
                    case 1:
                        format = "#,##0.0";
                        break;
                    case 2:
                        format = "#,##0.00";
                        break;
                    case 3:
                        format = "#,##0.000";
                        break;
                    case 4:
                        format = "#,##0.0000";
                        break;
                    default:
                        break;
                }
                result = Math.Round(number, MidpointRounding.AwayFromZero).ToString(format);
            }
            catch (Exception ex)
            {
                result = "";
            }

            return result;
        }

        public static string NumberToString(decimal number, int numberDigit, string cultureInfo)
        {
            string result = "";
            string format = "";
            try
            {
                cultureInfo = (String.IsNullOrWhiteSpace(cultureInfo) ? "vi-VN" : cultureInfo);
                switch (numberDigit)
                {
                    case 0:
                        format = "#,##0";
                        break;
                    case 1:
                        format = "#,##0.0";
                        break;
                    case 2:
                        format = "#,##0.00";
                        break;
                    case 3:
                        format = "#,##0.000";
                        break;
                    case 4:
                        format = "#,##0.0000";
                        break;
                    default:
                        break;
                }
                result = Math.Round(number, MidpointRounding.AwayFromZero).ToString(format, System.Globalization.CultureInfo.CreateSpecificCulture(cultureInfo));
            }
            catch (Exception ex)
            {
                result = "";
            }

            return result;
        }

        public static string NumberToStringRoundMax4(decimal number)
        {
            string result = "";
            try
            {
                result = NumberToStringRoundAuto(number, 4);
            }
            catch (Exception ex)
            {
                result = "";
            }

            return result;
        }

        public static string NumberToStringRoundAuto(decimal number, int numberDigit)
        {
            string result = "";
            string format = "";
            try
            {
                switch (numberDigit)
                {
                    case 0:
                        format = "#,##0";
                        break;
                    case 1:
                        format = "#,##0.#";
                        break;
                    case 2:
                        format = "#,##0.##";
                        break;
                    case 3:
                        format = "#,##0.###";
                        break;
                    case 4:
                        format = "#,##0.####";
                        break;
                    default:
                        break;
                }
                result = Math.Round(number, numberDigit, MidpointRounding.AwayFromZero).ToString(format);
            }
            catch (Exception ex)
            {
                result = "";
            }

            return result;
        }

        public static decimal NumberToNumberRoundMax4(decimal number)
        {
            decimal result = 0;
            try
            {
                result = NumberToNumberRoundAuto(number, 4);
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }

        public static decimal NumberToNumberRoundAuto(decimal number, int numberDigit)
        {
            decimal result = 0;
            try
            {
                result = Math.Round(number, numberDigit, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }
    }
}
