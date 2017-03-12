using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.DatabaseProcess
{
    internal static class TinhMuaHuongBHYT
    {
        /// <summary>
        /// Tính toán quyền lợi được hưởng theo số thẻ BHYT- chưa đủ để tính số tiền
        /// </summary>
        /// <returns></returns>
        internal static int TinhMucHuongTheoTheBHYT(string bhytcode, int bhyt_loaiid, int loaivienphiid, int du5nam6thangluongcoban, int bhyt_tuyenbenhvien)
        {
            /*
             * loaivienphiid=0 nội trú; =1 ngoại trú
             * bhyt_tuyenbenhvien=1: huyen; =2: tinh; ==3 TW
             */
            int result = 0;
            try
            {
                int maquyenloithe = 0;
                if (bhytcode != "" && bhytcode.Length == 15)
                {
                    maquyenloithe = Utilities.Util_TypeConvertParse.ToInt16(bhytcode.Substring(2, 1));
                }
                switch (maquyenloithe)
                {
                    case 1:
                    case 2:
                    case 5:
                        if (bhyt_loaiid == 1 || bhyt_loaiid == 2 || bhyt_loaiid == 3)
                        {
                            result = 100;
                        }
                        else if (bhyt_loaiid == 4) //trai tuyen
                        {
                            if (loaivienphiid == 0)
                            {
                                if (bhyt_tuyenbenhvien == 1)
                                {
                                    result = 100;
                                }
                                else if (bhyt_tuyenbenhvien == 2)
                                {
                                    result = 60;
                                }
                                else if (bhyt_tuyenbenhvien == 3)
                                {
                                    result = 40;
                                }
                            } //ngoai tru
                            else
                            {
                                if (bhyt_tuyenbenhvien == 1)
                                {
                                    result = 100;
                                }
                            }
                        }
                        break;
                    case 3:
                        if (bhyt_loaiid == 1 || bhyt_loaiid == 2 || bhyt_loaiid == 3)
                        {
                            if (du5nam6thangluongcoban == 1)
                            {
                                result = 100;
                            }
                            else
                            {
                                result = 95;
                            }
                        }
                        else if (bhyt_loaiid == 4) //trai tuyen
                        {
                            if (loaivienphiid == 0)
                            {
                                if (bhyt_tuyenbenhvien == 1)
                                {
                                    result = 95;
                                }
                                else if (bhyt_tuyenbenhvien == 2)
                                {
                                    result = 57;
                                }
                                else if (bhyt_tuyenbenhvien == 3)
                                {
                                    result = 38;
                                }
                            }
                            else
                            {
                                if (bhyt_tuyenbenhvien == 1)
                                {
                                    result = 95;
                                }
                            }
                        }
                        break;
                    case 4:
                        if (bhyt_loaiid == 1 || bhyt_loaiid == 2 || bhyt_loaiid == 3)
                        {
                            if (du5nam6thangluongcoban == 1)
                            {
                                result = 100;
                            }
                            else
                            {
                                result = 80;
                            }
                        }
                        else if (bhyt_loaiid == 4) //trai tuyen
                        {
                            if (loaivienphiid == 0)
                            {
                                if (bhyt_tuyenbenhvien == 1)
                                {
                                    result = 80;
                                }
                                else if (bhyt_tuyenbenhvien == 2)
                                {
                                    result = 48;
                                }
                                else if (bhyt_tuyenbenhvien == 3)
                                {
                                    result = 32;
                                }
                            }
                            else
                            {
                                if (bhyt_tuyenbenhvien == 1)
                                {
                                    result = 80;
                                }
                            }
                        }
                        break;
                    default:
                        break;
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
