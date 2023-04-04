using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MasterLibrary.Utils
{
    public class Helper
    {
        /// <summary>
        /// Chuyển đổi số ra tiền kiểu Việt Nam
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string FormatVNMoney(decimal money)
        {
            if (money == 0)
            {
                return "0 ₫";
            }
            return String.Format(CultureInfo.InvariantCulture,
                                "{0:#,#} ₫", money);
        }

        /// <summary>
        /// Chuyển sang chuỗi sang Base64
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Chuyển Base64 sang string
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Mã hoá mật khẩu
        /// </summary>
        /// <param name="_password"></param>
        /// <returns></returns>
        public static string HashPassword(string _password)
        {
            return Base64Encode(_password);
        }

        /// <summary>
        /// Decode mã hoá mật khẩu
        /// </summary>
        /// <param name="_hashPassword"></param>
        /// <returns></returns>
        public static string DePassword(string _hashPassword)
        {
            return Base64Decode(_hashPassword);
        }
    }
}
