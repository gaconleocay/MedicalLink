﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.Utilities
{
    public class Util_TypeConvertParse
    {
        /// <summary>
        /// Function to convert an string type to Boolean.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="bool"/> value, if fail return default value of Boolean class: false</returns>
        public static bool ToBoolean(string inputValue)
        {
            bool blnReturnValue = default(bool);
            if (!string.IsNullOrEmpty(inputValue))
            {
                Boolean.TryParse(inputValue, out blnReturnValue);
            }
            return blnReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Byte.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="byte"/> value, if fail return default value of Byte class: 0</returns>
        public static byte ToByte(string inputValue)
        {
            byte bytReturnValue = default(byte);
            if (!string.IsNullOrEmpty(inputValue))
            {
                byte.TryParse(inputValue, out bytReturnValue);
            }
            return bytReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Signed Byte.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="sbyte"/> value, if fail return default value of SByte class: 0</returns>
        public static sbyte ToSByte(string inputValue)
        {
            sbyte sbytReturnValue = default(sbyte);
            if (!string.IsNullOrEmpty(inputValue))
            {
                sbyte.TryParse(inputValue, out sbytReturnValue);
            }
            return sbytReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Integer.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="short"/> value, if fail return default value of Int16 class: 0</returns>
        public static short ToInt16(string inputValue)
        {
            short shrReturnValue = default(short);
            if (!string.IsNullOrEmpty(inputValue))
            {
                short.TryParse(inputValue, out shrReturnValue);
            }
            return shrReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Unsigned Integer.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="ushort"/> value, if fail return default value of UInt16 class: 0</returns>
        public static ushort ToUInt16(string inputValue)
        {
            ushort ushrReturnValue = default(ushort);
            if (!string.IsNullOrEmpty(inputValue))
            {
                ushort.TryParse(inputValue, out ushrReturnValue);
            }
            return ushrReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Integer.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="int"/> value, if fail return default value of Int32 class: 0</returns>
        public static int ToInt32(string inputValue)
        {
            int intReturnValue = default(int);
            if (!string.IsNullOrEmpty(inputValue))
            {
                int.TryParse(inputValue, out intReturnValue);
            }
            return intReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Unsigned Integer.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="uint"/> value, if fail return default value of UInt32 class: 0</returns>
        public static uint ToUInt32(string inputValue)
        {
            uint uintReturnValue = default(uint);
            if (!string.IsNullOrEmpty(inputValue))
            {
                uint.TryParse(inputValue, out uintReturnValue);
            }
            return uintReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Long Integer.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="long"/> value, if fail return default value of Int64 class: 0</returns>
        public static long ToInt64(string inputValue)
        {
            long lngReturnValue = default(long);
            if (!string.IsNullOrEmpty(inputValue))
            {
                long.TryParse(inputValue, out lngReturnValue);
            }
            return lngReturnValue;
        }


        /// <summary>
        /// Function to convert an string type to Unsigned Long Integer.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="ulong"/> value, if fail return default value of UInt64 class: 0</returns>
        public static ulong ToUInt64(string inputValue)
        {
            ulong ulngReturnValue = default(ulong);
            if (!string.IsNullOrEmpty(inputValue))
            {
                ulong.TryParse(inputValue, out ulngReturnValue);
            }
            return ulngReturnValue;
        }


        /// <summary>
        /// Function to convert an string type to Single-Precision Floating-Point.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="float"/> value, if fail return default value of Single class: 0.0f</returns>
        public static float ToFloat(string inputValue)
        {
            float fltReturnValue = default(float);
            if (!string.IsNullOrEmpty(inputValue))
            {
                float.TryParse(inputValue, out fltReturnValue);
            }
            return fltReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to Double-Precision Floating-Point.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="double"/> value, if fail return default value of Double class: 0.0d</returns>
        public static double ToDouble(string inputValue)
        {
            double dblReturnValue = default(double);
            if (!string.IsNullOrEmpty(inputValue))
            {
                double.TryParse(inputValue, out dblReturnValue);
            }
            return dblReturnValue;
        }


        /// <summary>
        /// Function to convert an string type to Decimal.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="decimal"/> value, if fail return default value of Decimal class: 0.0m<returns>
        public static decimal ToDecimal(string inputValue)
        {
            decimal decReturnValue = default(decimal);
            if (!string.IsNullOrEmpty(inputValue))
            {
                decimal.TryParse(inputValue, out decReturnValue);
            }
            return decReturnValue;
        }

        /// <summary>
        /// Function to convert an string type to DateTime.
        /// </summary>
        /// <param groupCode="inputValue">string type, indicate value to be parsed.</param>
        /// <returns>a <see cref="DateTime"/> value, if fail return MinValue of DateTime class</returns>
        public static DateTime ToDateTime(string inputValue)
        {
            DateTime dtmReturnValue = DateTime.Now;
            if (!string.IsNullOrEmpty(inputValue))
            {
                DateTime.TryParse(inputValue, out dtmReturnValue);
            }
            return dtmReturnValue;
        }
    }
}
