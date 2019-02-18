using MedicalLink.ClassCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.DatabaseProcess
{
    internal static class TinhMucHuongBHYT
    {
        /// <summary>
        /// Tính toán quyền lợi được hưởng theo số thẻ BHYT- chưa đủ để tính số tiền
        /// </summary>
        /// <returns></returns>
        internal static int TinhMucHuongTheoTheBHYT(ClassCommon.TinhBHYTThanhToanDTO tinhBHYT)
        {
            /*bhyt_loaiid: =1 đúng tuyến; =2: đúng tuyến giới thiệu; =3 đúng tuyến cấp cứu; =4 trái tuyến
             * loaivienphiid=0 nội trú; =1 ngoại trú
             * bhyt_tuyenbenhvien=1: huyen; =2: tinh; ==3 TW
             */
            int result = 0;
            try
            {
                int maquyenloithe = 0;
                if (tinhBHYT.bhytcode != "" && tinhBHYT.bhytcode.Length == 15)
                {
                    maquyenloithe = Utilities.TypeConvertParse.ToInt16(tinhBHYT.bhytcode.Substring(2, 1));
                }
                switch (maquyenloithe)
                {
                    case 1:
                    case 2:
                    case 5:
                        if (tinhBHYT.bhyt_loaiid == 1 || tinhBHYT.bhyt_loaiid == 2 || tinhBHYT.bhyt_loaiid == 3)
                        {
                            result = 100;
                        }
                        else if (tinhBHYT.bhyt_loaiid == 4) //trai tuyen
                        {
                            if (tinhBHYT.loaivienphiid == 0)
                            {
                                if (tinhBHYT.bhyt_tuyenbenhvien == 1)
                                {
                                    result = 100;
                                }
                                else if (tinhBHYT.bhyt_tuyenbenhvien == 2)
                                {
                                    result = 60;
                                }
                                else if (tinhBHYT.bhyt_tuyenbenhvien == 3)
                                {
                                    result = 40;
                                }
                            } //ngoai tru
                            else
                            {
                                if (tinhBHYT.bhyt_tuyenbenhvien == 1)
                                {
                                    result = 100;
                                }
                            }
                        }
                        break;
                    case 3:
                        if (tinhBHYT.bhyt_loaiid == 1 || tinhBHYT.bhyt_loaiid == 2 || tinhBHYT.bhyt_loaiid == 3)
                        {
                            if (tinhBHYT.du5nam6thangluongcoban == 1)
                            {
                                result = 100;
                            }
                            else
                            {
                                result = 95;
                            }
                        }
                        else if (tinhBHYT.bhyt_loaiid == 4) //trai tuyen
                        {
                            if (tinhBHYT.loaivienphiid == 0)
                            {
                                if (tinhBHYT.bhyt_tuyenbenhvien == 1)
                                {
                                    result = 95;
                                }
                                else if (tinhBHYT.bhyt_tuyenbenhvien == 2)
                                {
                                    result = 57;
                                }
                                else if (tinhBHYT.bhyt_tuyenbenhvien == 3)
                                {
                                    result = 38;
                                }
                            }
                            else
                            {
                                if (tinhBHYT.bhyt_tuyenbenhvien == 1)
                                {
                                    result = 95;
                                }
                            }
                        }
                        break;
                    case 4:
                        if (tinhBHYT.bhyt_loaiid == 1 || tinhBHYT.bhyt_loaiid == 2 || tinhBHYT.bhyt_loaiid == 3)
                        {
                            if (tinhBHYT.du5nam6thangluongcoban == 1)
                            {
                                result = 100;
                            }
                            else
                            {
                                result = 80;
                            }
                        }
                        else if (tinhBHYT.bhyt_loaiid == 4) //trai tuyen
                        {
                            if (tinhBHYT.loaivienphiid == 0)
                            {
                                if (tinhBHYT.bhyt_tuyenbenhvien == 1)
                                {
                                    result = 80;
                                }
                                else if (tinhBHYT.bhyt_tuyenbenhvien == 2)
                                {
                                    result = 48;
                                }
                                else if (tinhBHYT.bhyt_tuyenbenhvien == 3)
                                {
                                    result = 32;
                                }
                            }
                            else
                            {
                                if (tinhBHYT.bhyt_tuyenbenhvien == 1)
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
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// Tính toán Số tiền BHYT thanh toán Chi phí trong phạm vi quyền lợi BHBYT
        /// ngay 25/7/2017
        /// </summary>
        /// <returns></returns>
        internal static decimal TinhSoTienBHYTThanhToan(ClassCommon.TinhBHYTThanhToanDTO thongtinthanhtoan)
        {
            /*bhyt_loaiid: =1 đúng tuyến; =2: đúng tuyến giới thiệu; =3 đúng tuyến cấp cứu; =4 trái tuyến
            * loaivienphiid=0 nội trú; =1 ngoại trú
            * bhyt_tuyenbenhvien=1: huyen; =2: tinh; ==3 TW
            */
            decimal result = 0;
            try
            {
                decimal money_bhyttra_dvktc = 0;
                decimal money_bhyttra_ngoaidvktc = 0;


                thongtinthanhtoan.chiphi_ngoaigoidvktc = thongtinthanhtoan.chiphi_trongpvql - thongtinthanhtoan.chiphi_goidvktc;
                thongtinthanhtoan._46thanhluongcoban = thongtinthanhtoan.bhyt_thangluongtoithieu * 46;
                thongtinthanhtoan._15thanhluongcoban = thongtinthanhtoan.bhyt_thangluongtoithieu * (decimal)0.15;

                if (thongtinthanhtoan.bhytcode != "" && thongtinthanhtoan.bhytcode.Length == 15)
                {
                    thongtinthanhtoan.maquyenloithe = Utilities.TypeConvertParse.ToInt16(thongtinthanhtoan.bhytcode.Substring(2, 1));
                }

                if (thongtinthanhtoan.chiphi_trongpvql <= thongtinthanhtoan._15thanhluongcoban)
                {
                    money_bhyttra_dvktc = thongtinthanhtoan.chiphi_trongpvql;
                    //money_bhyttra_ngoaidvktc = 0;
                }
                else
                {
                    switch (thongtinthanhtoan.maquyenloithe)
                    {
                        case 1:
                        case 2:
                        case 5:
                            if (thongtinthanhtoan.bhyt_loaiid == 1 || thongtinthanhtoan.bhyt_loaiid == 2 || thongtinthanhtoan.bhyt_loaiid == 3) //dung tuyen
                            {
                                if (thongtinthanhtoan.chiphi_goidvktc > thongtinthanhtoan._46thanhluongcoban)
                                {
                                    money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                }
                                else
                                {
                                    money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc;
                                }
                                money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc;
                            }
                            else if (thongtinthanhtoan.bhyt_loaiid == 4) //trai tuyen
                            {
                                if (thongtinthanhtoan.loaivienphiid == 0)
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if (thongtinthanhtoan.chiphi_goidvktc > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 2)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.6) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.6;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.6;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 3)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.4) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.4;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.4;
                                    }
                                }
                                else//ngoai tru
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if (thongtinthanhtoan.chiphi_goidvktc > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc;
                                    }
                                }
                            }
                            break;
                        case 3:
                            if (thongtinthanhtoan.bhyt_loaiid == 1 || thongtinthanhtoan.bhyt_loaiid == 2 || thongtinthanhtoan.bhyt_loaiid == 3)
                            {
                                if (thongtinthanhtoan.du5nam6thangluongcoban == 1)
                                {
                                    if (thongtinthanhtoan.chiphi_goidvktc > thongtinthanhtoan._46thanhluongcoban)
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                    }
                                    else
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc;
                                    }
                                    money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc;
                                }
                                else
                                {
                                    if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95) > thongtinthanhtoan._46thanhluongcoban)
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                    }
                                    else
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95;
                                    }
                                    money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.95;
                                }
                            }
                            else if (thongtinthanhtoan.bhyt_loaiid == 4) //trai tuyen
                            {
                                if (thongtinthanhtoan.loaivienphiid == 0)
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.95;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 2)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.57) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.59;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.57;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 3)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.38) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.38;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.38;
                                    }
                                }
                                else
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.95;
                                    }
                                }
                            }
                            break;
                        case 4:
                            if (thongtinthanhtoan.bhyt_loaiid == 1 || thongtinthanhtoan.bhyt_loaiid == 2 || thongtinthanhtoan.bhyt_loaiid == 3)
                            {
                                if (thongtinthanhtoan.du5nam6thangluongcoban == 1)
                                {
                                    if (thongtinthanhtoan.chiphi_goidvktc > thongtinthanhtoan._46thanhluongcoban)
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                    }
                                    else
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc;
                                    }
                                    money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc;
                                }
                                else
                                {
                                    if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8) > thongtinthanhtoan._46thanhluongcoban)
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                    }
                                    else
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8;
                                    }
                                    money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.8;
                                }
                            }
                            else if (thongtinthanhtoan.bhyt_loaiid == 4) //trai tuyen
                            {
                                if (thongtinthanhtoan.loaivienphiid == 0)
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.8;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 2)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.48) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.48;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.48;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 3)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.32) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.32;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.32;
                                    }
                                }
                                else
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.8;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                result = money_bhyttra_dvktc + money_bhyttra_ngoaidvktc;
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// Tính toán Số tiền BHYT thanh toán Chi phí trong phạm vi quyền lợi BHBYT - Quy ra Ty le Trung binh cua DV (tru dv KTC
        /// ngay 29/7/2017
        /// </summary>
        /// <returns></returns>
        internal static decimal TinhSoTienBHYTThanhToan_TyLeTrungBinh(ClassCommon.TinhBHYTThanhToanDTO thongtinthanhtoan)
        {
            /*bhyt_loaiid: =1 đúng tuyến; =2: đúng tuyến giới thiệu; =3 đúng tuyến cấp cứu; =4 trái tuyến
            * loaivienphiid=0 nội trú; =1 ngoại trú
            * bhyt_tuyenbenhvien=1: huyen; =2: tinh; ==3 TW
            */
            decimal result = 0;
            try
            {
                decimal money_bhyttra_dvktc = 0;
                decimal money_bhyttra_ngoaidvktc = 0;


                thongtinthanhtoan.chiphi_ngoaigoidvktc = thongtinthanhtoan.chiphi_trongpvql - thongtinthanhtoan.chiphi_goidvktc;
                thongtinthanhtoan._46thanhluongcoban = thongtinthanhtoan.bhyt_thangluongtoithieu * 46;
                thongtinthanhtoan._15thanhluongcoban = thongtinthanhtoan.bhyt_thangluongtoithieu * (decimal)0.15;

                if (thongtinthanhtoan.bhytcode != "" && thongtinthanhtoan.bhytcode.Length == 15)
                {
                    thongtinthanhtoan.maquyenloithe = Utilities.TypeConvertParse.ToInt16(thongtinthanhtoan.bhytcode.Substring(2, 1));
                }

                if (thongtinthanhtoan.chiphi_trongpvql <= thongtinthanhtoan._15thanhluongcoban)
                {
                    //money_bhyttra_dvktc = 0;
                    money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_trongpvql;
                }
                else
                {
                    switch (thongtinthanhtoan.maquyenloithe)
                    {
                        case 1:
                        case 2:
                        case 5:
                            if (thongtinthanhtoan.bhyt_loaiid == 1 || thongtinthanhtoan.bhyt_loaiid == 2 || thongtinthanhtoan.bhyt_loaiid == 3) //dung tuyen
                            {
                                if (thongtinthanhtoan.chiphi_goidvktc > thongtinthanhtoan._46thanhluongcoban)
                                {
                                    money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                }
                                else
                                {
                                    money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc;
                                }
                                money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc;
                            }
                            else if (thongtinthanhtoan.bhyt_loaiid == 4) //trai tuyen
                            {
                                if (thongtinthanhtoan.loaivienphiid == 0)
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if (thongtinthanhtoan.chiphi_goidvktc > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 2)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.6) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.6;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.6;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 3)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.4) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.4;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.4;
                                    }
                                }
                                else//ngoai tru
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if (thongtinthanhtoan.chiphi_goidvktc > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc;
                                    }
                                }
                            }
                            break;
                        case 3:
                            if (thongtinthanhtoan.bhyt_loaiid == 1 || thongtinthanhtoan.bhyt_loaiid == 2 || thongtinthanhtoan.bhyt_loaiid == 3)
                            {
                                if (thongtinthanhtoan.du5nam6thangluongcoban == 1)
                                {
                                    if (thongtinthanhtoan.chiphi_goidvktc > thongtinthanhtoan._46thanhluongcoban)
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                    }
                                    else
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc;
                                    }
                                    money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc;
                                }
                                else
                                {
                                    if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95) > thongtinthanhtoan._46thanhluongcoban)
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                    }
                                    else
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95;
                                    }
                                    money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.95;
                                }
                            }
                            else if (thongtinthanhtoan.bhyt_loaiid == 4) //trai tuyen
                            {
                                if (thongtinthanhtoan.loaivienphiid == 0)
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.95;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 2)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.57) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.59;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.57;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 3)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.38) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.38;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.38;
                                    }
                                }
                                else
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.95;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.95;
                                    }
                                }
                            }
                            break;
                        case 4:
                            if (thongtinthanhtoan.bhyt_loaiid == 1 || thongtinthanhtoan.bhyt_loaiid == 2 || thongtinthanhtoan.bhyt_loaiid == 3)
                            {
                                if (thongtinthanhtoan.du5nam6thangluongcoban == 1)
                                {
                                    if (thongtinthanhtoan.chiphi_goidvktc > thongtinthanhtoan._46thanhluongcoban)
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                    }
                                    else
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc;
                                    }
                                    money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc;
                                }
                                else
                                {
                                    if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8) > thongtinthanhtoan._46thanhluongcoban)
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                    }
                                    else
                                    {
                                        money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8;
                                    }
                                    money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.8;
                                }
                            }
                            else if (thongtinthanhtoan.bhyt_loaiid == 4) //trai tuyen
                            {
                                if (thongtinthanhtoan.loaivienphiid == 0)
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.8;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 2)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.48) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.48;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.48;
                                    }
                                    else if (thongtinthanhtoan.bhyt_tuyenbenhvien == 3)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.32) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.32;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.32;
                                    }
                                }
                                else
                                {
                                    if (thongtinthanhtoan.bhyt_tuyenbenhvien == 1)
                                    {
                                        if ((thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8) > thongtinthanhtoan._46thanhluongcoban)
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan._46thanhluongcoban;
                                        }
                                        else
                                        {
                                            money_bhyttra_dvktc = thongtinthanhtoan.chiphi_goidvktc * (decimal)0.8;
                                        }
                                        money_bhyttra_ngoaidvktc = thongtinthanhtoan.chiphi_ngoaigoidvktc * (decimal)0.8;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                if ((money_bhyttra_dvktc + money_bhyttra_ngoaidvktc) == thongtinthanhtoan.chiphi_trongpvql)
                {
                    result = 1;
                }
                else
                {
                    result = (long)(money_bhyttra_dvktc + money_bhyttra_ngoaidvktc) / thongtinthanhtoan.chiphi_trongpvql;
                }
            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        /// <summary>
        /// Tính số tiền BHYT thanh toán đối với gói dịch vụ TT theo tỷ lệ - bv Việt Tiệp
        /// ngay 11/10
        /// </summary>
        /// <param name="_filter"></param>
        /// <returns></returns>
        internal static decimal TinhSoTienBHYTThanhToan_VTYTTTRieng
            (TinhBHYTThanhToanTTRiengDTO _filter)
        {
            decimal result = 0;
            try
            {
                /*bhyt_loaiid: =1 đúng tuyến; =2: đúng tuyến giới thiệu; =3 đúng tuyến cấp cứu; =4 trái tuyến
                * loaivienphiid=0 nội trú; =1 ngoại trú
                * bhyt_tuyenbenhvien=1: huyen; =2: tinh; ==3 TW
                */
                if (_filter.bhytcode != "" && _filter.bhytcode.Length == 15)
                {
                    _filter.maquyenloithe = Utilities.TypeConvertParse.ToInt16(_filter.bhytcode.Substring(2, 1));
                }


            }
            catch (Exception ex)
            {
                 O2S_Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
