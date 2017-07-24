using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MedicalLink.Utilities.Common.String
{
    public class Convert
    {
        public static string HexToUTF8(string hexInput)
        {
            string result;
            try
            {
                int numberChars = hexInput.Length;
                byte[] bytes = new byte[numberChars / 2];
                for (int i = 0; i < numberChars; i += 2)
                {
                    bytes[i / 2] = System.Convert.ToByte(hexInput.Substring(i, 2), 16);
                }
                result = System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Bo dau chuoi ky tu tieng Viet.
        /// Neu co exception thi tra lai chuoi ban dau.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UnSignVNese(string text)
        {
            string result = text;
            try
            {
                const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
                const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
                int index = -1;
                char[] arrChar = FindText.ToCharArray();
                while ((index = result.IndexOfAny(arrChar)) != -1)
                {
                    int index2 = FindText.IndexOf(result[index]);
                    result = result.Replace(result[index], ReplText[index2]);
                }
            }
            catch (Exception)
            {
                result = text;
            }
            return result;
        }

        /// <summary>
        /// Bo dau chuoi ky tu tieng Viet.
        /// Neu co exception thi tra lai chuoi ban dau.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UnSignVNese2(string text)
        {
            string result = text;
            try
            {
                if (!System.String.IsNullOrWhiteSpace(result))
                {
                    for (int i = 33; i < 48; i++)
                    {
                        result = result.Replace(((char)i).ToString(), "");
                    }
                    for (int i = 58; i < 65; i++)
                    {
                        result = result.Replace(((char)i).ToString(), "");
                    }
                    for (int i = 91; i < 97; i++)
                    {
                        result = result.Replace(((char)i).ToString(), "");
                    }
                    for (int i = 123; i < 127; i++)
                    {
                        result = result.Replace(((char)i).ToString(), "");
                    }
                    Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
                    string strFormD = result.Normalize(System.Text.NormalizationForm.FormD);
                    result = regex.Replace(strFormD, System.String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                }
            }
            catch (Exception)
            {
                result = text;
            }
            return result;
        }

        /// <summary>
        /// Chuyen so tien tu kieu so sang viet bang chu (tieng Viet).
        /// Neu co exception thi tra lai xau rong.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string CurrencyToVneseString(string number)
        {
            string result;
            try
            {
                string[] strTachPhanSauDauPhay;
                if (number.Contains('.') || number.Contains(','))
                {
                    strTachPhanSauDauPhay = number.Split('.', ',');
                    return (CurrencyToVneseString(strTachPhanSauDauPhay[0]) + "phẩy " + CurrencyToVneseString(strTachPhanSauDauPhay[1]));
                }

                string[] dv = { "", "mươi", "trăm", "nghìn", "triệu", "tỉ" };
                string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

                int i, j, k, n, len, found, ddv, rd;

                len = number.Length;
                number += "ss";
                result = "";
                found = 0;
                ddv = 0;
                rd = 0;

                i = 0;
                while (i < len)
                {
                    //So chu so o hang dang duyet
                    n = (len - i + 2) % 3 + 1;

                    //Kiem tra so 0
                    found = 0;
                    for (j = 0; j < n; j++)
                    {
                        if (number[i + j] != '0')
                        {
                            found = 1;
                            break;
                        }
                    }

                    //Duyet n chu so
                    if (found == 1)
                    {
                        rd = 1;
                        for (j = 0; j < n; j++)
                        {
                            ddv = 1;
                            switch (number[i + j])
                            {
                                case '0':
                                    if (n - j == 3) result += cs[0] + " ";
                                    if (n - j == 2)
                                    {
                                        if (number[i + j + 1] != '0') result += "linh ";
                                        ddv = 0;
                                    }
                                    break;
                                case '1':
                                    if (n - j == 3) result += cs[1] + " ";
                                    if (n - j == 2)
                                    {
                                        result += "mười ";
                                        ddv = 0;
                                    }
                                    if (n - j == 1)
                                    {
                                        if (i + j == 0) k = 0;
                                        else k = i + j - 1;

                                        if (number[k] != '1' && number[k] != '0')
                                            result += "mốt ";
                                        else
                                            result += cs[1] + " ";
                                    }
                                    break;
                                case '5':
                                    if ((i + j == len - 1) || (i + j + 3 == len - 1))
                                        result += "năm ";
                                    else
                                        result += cs[5] + " ";
                                    break;
                                default:
                                    result += cs[(int)number[i + j] - 48] + " ";
                                    break;
                            }

                            //Doc don vi nho
                            if (ddv == 1)
                            {
                                result += ((n - j) != 1) ? dv[n - j - 1] + " " : dv[n - j - 1];
                            }
                        }
                    }


                    //Doc don vi lon
                    if (len - i - n > 0)
                    {
                        if ((len - i - n) % 9 == 0)
                        {
                            if (rd == 1)
                                for (k = 0; k < (len - i - n) / 9; k++)
                                    result += "tỉ ";
                            rd = 0;
                        }
                        else
                            if (found != 0) result += dv[((len - i - n + 1) % 9) / 3 + 2] + " ";
                    }

                    i += n;
                }

                if (len == 1)
                    if (number[0] == '0' || number[0] == '5') return cs[(int)number[0] - 48];

                result = result + "đồng.";
                result = FirstCharToUpper(result);
            }
            catch (Exception)
            {
                result = "";
            }
            return result;
        }


        public static string FirstCharToUpper(string input)
        {
            try
            {
                if (!System.String.IsNullOrEmpty(input))
                    return (input.First().ToString().ToUpper() + System.String.Join("", input.Skip(1)));
            }
            catch (Exception)
            {
                input = "";
            }
            return input;
        }

        public string HexToAscii(string hexString)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hexString.Length; i += 2)
                {
                    string hs = hexString.Substring(i, i + 2);
                    System.Convert.ToChar(System.Convert.ToUInt32(hexString.Substring(0, 2), 16)).ToString();
                }
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
