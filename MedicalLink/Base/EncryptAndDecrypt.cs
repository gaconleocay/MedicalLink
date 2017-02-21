using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.Base
{
    static internal class EncryptAndDecrypt
    {
        static string key = MedicalLink.Base.KeyTrongPhanMem.MaHoaVaGiaiMa_key;
        /// <summary>
        /// Mã hóa 1 chuỗi có pass
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns>chuỗi đã mã hóa</returns>
        internal static string Encrypt(string toEncrypt, bool useHashing)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                return "";
                MedicalLink.Base.Logging.Warn("Ham ma hoa " + ex.ToString());
            }
        }
        /// <summary>
        /// Giải mã 1 chuỗi
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns>trả về chuỗi được giải mã</returns>
        internal static string Decrypt(string cipherString, bool useHashing)
        {
            try
            {
                if (cipherString != "")
                {
                    byte[] keyArray;
                    byte[] toEncryptArray = Convert.FromBase64String(cipherString);
                    if (useHashing)
                    {
                        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                        hashmd5.Clear();
                    }
                    else
                        keyArray = UTF8Encoding.UTF8.GetBytes(key);
                    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                    tdes.Key = keyArray;
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;
                    ICryptoTransform cTransform = tdes.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    tdes.Clear();
                    return UTF8Encoding.UTF8.GetString(resultArray);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
                MedicalLink.Base.Logging.Warn("Ham giai ma " + ex.ToString());
            }
        }

    }
}
