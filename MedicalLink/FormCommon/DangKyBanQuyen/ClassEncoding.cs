using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace MedicalLink.FormCommon.DangKyBanQuyen
{
    public class ClassEncoding
    {
        internal static string MaHoaCoDinhDang(string sourceText, string typeCode)
        {
            string encodeText = "";
            try
            {
                switch (typeCode)
                {
                    case "UTF-16_Bytes":
                        // Ghi các byte được mã hóa theo UTF-16
                        // của chuỗi nguồn ra file.
                        byte[] utf16String = Encoding.Unicode.GetBytes(sourceText);
                        encodeText = BitConverter.ToString(utf16String);
                        break;
                    case "UTF-8_Bytes":
                        byte[] utf8String = Encoding.UTF8.GetBytes(sourceText);
                        encodeText = BitConverter.ToString(utf8String);
                        break;
                    case "ASCII_Bytes":
                        byte[] asciiString = Encoding.ASCII.GetBytes(sourceText);
                        encodeText = BitConverter.ToString(asciiString);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MedicalLink.Base.Logging.Warn("Ma hoa chuoi theo nhieu dinh dang " + ex.ToString());
            }
            return encodeText;
        }
    }
}
