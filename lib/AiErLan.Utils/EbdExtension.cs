using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AiErLan.Utils
{
    /// <summary>
    /// 扩展工具
    /// </summary>
    public static class EbdExtension
    {
        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="len">指定的长度</param>
        /// <returns>截取后的字符串</returns>
        public static string Cut(this string str, int len)
        {
            string Temp = "";
            if (str.Length > len)
            {
                Temp = str.Substring(0, len) + "..";
            }
            else
            {
                Temp = str;
            }
            return Temp;
        }
        /// <summary>
        /// EXCEL导入时间转换
        /// </summary>
        /// <param name="value">字符型时间</param>
        /// <returns></returns>
        public static DateTime ConvertExcelDateTimeIntoCLRDateTime(object value)
        {
            if (value is DateTime)
            { return DateTime.Parse(value.ToString()); }
            else
            {
                string[] sdate = value.ToString().Split('/');
                value = sdate[2] + "-" + sdate[1].Substring(0, sdate[1].Length - 1) + "-" + sdate[0];
                return DateTime.Parse(value.ToString());
            }
        }
        /// <summary>
        /// 身份证正则验证
        /// </summary>
        /// <param name="str_idcard">身份证号码</param>
        /// <returns></returns>
        public static bool IsIDcard(string Id)
        {
            if (Id.Length == 18)
            {
                return CheckIDCard18(Id);
            }
            if (Id.Length == 15)
            {
                return CheckIDCard15(Id);
            }
            return false;
        }
        /// <summary>
        /// 18位身份证验证
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }
        /// <summary>
        /// 15位身份证验证
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 手机和座机号码验证
        /// </summary>
        /// <param name="mobileNum">电话号码</param>
        /// <returns></returns>
        public static bool isMobileNumber(string mobileNum)
        {
            Regex MOBILE = new Regex("^1(3[0-9]|4[57]|5[0-35-9]|7[01678]|8[0-9])\\d{8}$");
            Regex CM = new Regex("^1(3[4-9]|4[7]|5[0-27-9]|7[08]|8[2-478])\\d{8}$");
            Regex CU = new Regex("^1(3[0-2]|4[5]|5[256]|7[016]|8[56])\\d{8}$");
            Regex CT = new Regex("^1(3[34]|53|7[07]|8[019])\\d{8}$");
            Regex ZJ = new Regex(@"^(\d{3,4}-?)?\d{7,8}$");
            if (mobileNum.Length < 11)
            {
                return false;
            }
            if (MOBILE.IsMatch(mobileNum) || CM.IsMatch(mobileNum) || CU.IsMatch(mobileNum) || CT.IsMatch(mobileNum) || ZJ.IsMatch(mobileNum))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int GetAge(string Id)
        {
            int age = 0;
            string birthday = "";
            if (Id.Length == 18)
            {
                birthday = Id.Substring(6, 4);
            }
            else
            {
                birthday = "19" + Id.Substring(6, 2);
            }
            age = Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(birthday);
            return age;
        }

        public static DateTime CovertToDataTime(this long dateTime)
        {
            if (dateTime == 0)
                return new DateTime();
            return DateTime.ParseExact(dateTime.ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);//.ToString("yyyy-MM-dd HH:mm");
        }
    }
}
