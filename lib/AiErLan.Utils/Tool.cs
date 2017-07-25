using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography;
using System.Globalization;
using System.Web;

namespace AiErLan.Utils
{
    public static class Tool
    {
        /// <summary>
        /// 随机数生成
        /// </summary>
        /// <param name="Length">随机数长度</param>
        /// <returns></returns>
        public static string GetRandom(int Length = 4)
        {
            char[] constant = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(10)]);
            }
            return newRandom.ToString();
        }

        /// <summary>
        /// 移除HTML标签
        /// </summary>
        /// <param name="HTMLStr"></param>
        /// <returns></returns>
        public static string ParseTags(string HTMLStr)
        {
            return System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");
        }

        /// <summary>
        /// 查找字符串出现的次数
        /// </summary>
        /// <param name="kestr">字符串</param>
        /// <param name="cont">内容</param>
        /// <returns></returns>
        public static int FindKeyStrCount(string kestr, string cont)
        {
            int Star = 0;
            int Count = 0;

            while (Star != -1)
            {
                Star = cont.IndexOf(kestr, Star);
                if (Star != -1)
                {
                    Count++;
                    Star++;
                }
            }

            return Count;
        }

        /// <summary>
        /// 替换试题选项里的占位符
        /// </summary>
        /// <param name="cont">被替换的内容</param>
        /// <param name="newTag">替换的标签</param>
        /// <returns></returns>
        public static string deleteTag(string cont, string newTag, bool b = true)
        {
            cont = cont.Replace("{u", "<span style=\"text-decoration: underline; text-align: center; \">").Replace("u}", "</span>");
            cont = cont.Replace("｛u", "<span style=\"text-decoration: underline; text-align: center; \">").Replace("u｝", "</span>");
            if (b)
            {
                int num = FindKeyStrCount("$option", cont);
                for (int i = 0; i < num; i++)
                {
                    if (num > 1)
                    {
                        cont = cont.Replace("$option:" + (i + 1) + "$", "<span  style=\"text-decoration: underline; text-align: center; \"> &nbsp; &nbsp;  &nbsp; &nbsp; " + (i + 1) + " &nbsp; &nbsp;  &nbsp; &nbsp; </span>");
                        cont = cont.Replace("option:" + (i + 1) + "$", "<span  style=\"text-decoration: underline; text-align: center; \"> &nbsp; &nbsp;  &nbsp; &nbsp; " + (i + 1) + " &nbsp; &nbsp;  &nbsp; &nbsp; </span>");
                    }
                    else
                    {
                        cont = cont.Replace("$option:" + (i + 1) + "$", newTag).Replace("option:" + (i + 1) + "$", newTag);
                    }
                }
            }
            return cont;
        }

        public static string replaceTag(string cont, string newTag, bool b = true)
        {
            if (b)
            {
                int num = FindKeyStrCount("$option", cont);
                for (int i = 0; i < num; i++)
                {
                    if (num > 1)
                    {
                        cont = cont.Replace("$option:" + (i + 1) + "$", newTag + (i + 1) + newTag);
                    }
                    else
                    {
                        cont = cont.Replace("$option:" + (i + 1) + "$", newTag).Replace("option:" + (i + 1) + "$", newTag);
                    }
                }
            }
            return cont;
        }

        /// <summary>
        /// 移除HMTL
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return content;
            }
            return Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(content, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase).Trim(), "<(.[^>]*)>", "", RegexOptions.IgnoreCase).Trim(), @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase).Trim(), "-->", "", RegexOptions.IgnoreCase).Trim(), "<!--.*", "", RegexOptions.IgnoreCase).Trim(), "&(quot|#34);", "\"", RegexOptions.IgnoreCase).Trim(), "&(amp|#38);", "&", RegexOptions.IgnoreCase).Trim(), "&(lt|#60);", "<", RegexOptions.IgnoreCase).Trim(), "&(gt|#62);", ">", RegexOptions.IgnoreCase).Trim(), "&(nbsp|#160);", " ", RegexOptions.IgnoreCase).Trim(), "&(iexcl|#161);", "\x00a1", RegexOptions.IgnoreCase).Trim(), "&(cent|#162);", "\x00a2", RegexOptions.IgnoreCase).Trim(), "&(pound|#163);", "\x00a3", RegexOptions.IgnoreCase).Trim(), "&(copy|#169);", "\x00a9", RegexOptions.IgnoreCase).Trim(), @"&#(\d+);", "", RegexOptions.IgnoreCase).Trim(), "<span (.[^>]*)>", "", RegexOptions.IgnoreCase).Trim(), "<a (.[^>]*)>", "", RegexOptions.IgnoreCase).Trim().Replace("<p>", "").Trim().Replace("<span>", "").Trim().Replace("</span>", "").Trim().Replace("<strong>", "").Trim().Replace("</strong>", "").Trim().Replace("&#160;", "").Trim().Replace("&nbsp;", "").Trim().Replace("</p>", "").Trim().Replace("<", "").Trim().Replace(">", "").Trim().Replace("\r\n", "").Trim().Replace("br /", "").Trim().Replace(@"\", "").Trim();
        }

        public static string removeWhiteSpace(string str)
        {
            string str2 = string.Empty;
            CharEnumerator enumerator = str.GetEnumerator();
            while (enumerator.MoveNext())
            {
                byte[] buffer = new byte[1];
                int num = Encoding.ASCII.GetBytes(enumerator.Current.ToString())[0];
                string str3 = enumerator.Current.ToString();
                if (!((num == 0x20) || string.IsNullOrWhiteSpace(str3)))
                {
                    str2 = str2 + enumerator.Current.ToString();
                }
            }
            return str2;
        }

        public static string AddWords(string SourceContent, string targetKey, string splitKey)
        {
            if (!string.IsNullOrWhiteSpace(SourceContent))
                SourceContent += splitKey;
            return SourceContent += targetKey;
        }

        /// <summary>
        /// 将 ｛u u｝替换为下划线
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceUTag(string str)
        {
            str = str.Replace("{u", "<span style=\"text-decoration: underline; text-align: center; \">").Replace("u}", "</span>");
            str = str.Replace("｛u", "<span style=\"text-decoration: underline; text-align: center; \">").Replace("u｝", "</span>");
            return str;
        }

        /// <summary>
        /// 阿拉伯数组转罗马数字
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string toRoman(this int? n)
        {
            int[] arabic = new int[13];
            string[] roman = new string[13];
            int i = 0;
            string o = "";
            arabic[0] = 1000;
            arabic[1] = 900;
            arabic[2] = 500;
            arabic[3] = 400;
            arabic[4] = 100;
            arabic[5] = 90;
            arabic[6] = 50;
            arabic[7] = 40;
            arabic[8] = 10;
            arabic[9] = 9;
            arabic[10] = 5;
            arabic[11] = 4;
            arabic[12] = 1;

            roman[0] = "M";
            roman[1] = "CM";
            roman[2] = "D";
            roman[3] = "CD";
            roman[4] = "C";
            roman[5] = "XC";
            roman[6] = "L";
            roman[7] = "XL";
            roman[8] = "X";
            roman[9] = "IX";
            roman[10] = "V";
            roman[11] = "IV";
            roman[12] = "I";

            if (n != null)
            {
                while (n > 0)
                {
                    while (n >= arabic[i])
                    {
                        n = n - arabic[i];
                        o = o + roman[i];
                    }
                    i++;
                }
            }
            return o;
        }

        /// <summary>
        /// 如果字符串为null 返回 "";
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToStringOrNull(this string str)
        {
            if (str == null)
            {
                return "";
            }
            return str.Trim();
        }

        /// <summary>
        /// 将long？转字符串如果为Null 转为""
        /// </summary>
        /// <param name="long_str"></param>
        /// <returns></returns>
        public static string ToStringOrNull(this long? long_str)
        {
            if (long_str == null)
            {
                return "";
            }
            return long_str.ToString();
        }
        /// <summary>
        /// 将int？转字符串如果为Null 转为""
        /// </summary>
        /// <param name="int_str"></param>
        /// <returns></returns>
        public static string ToStringOrNull(this int? int_str)
        {
            if (int_str == null)
            {
                return "";
            }
            return int_str.ToString();
        }

        /// <summary>
        /// 将bool?转出 字符串
        /// </summary>
        /// <param name="bool_str"></param>
        /// <returns></returns>
        public static string ToStringOrNull(this bool? bool_str)
        {
            if (bool_str == null)
            {
                return "";
            }

            return bool_str.ToString();
        }

        /// <summary>
        ///  将DateTime?转出 字符串
        /// </summary>
        /// <param name="dateTime_str"></param>
        /// <param name="format">格式化</param>
        /// <returns></returns>
        public static string ToStringOrNull(this DateTime? dateTime_str, string format = null)
        {
            if (dateTime_str == null)
            {
                return "";
            }
            if (!string.IsNullOrEmpty(format))
            {
                return dateTime_str.Value.ToString(format);
            }

            return dateTime_str.Value.ToString();
        }


        /// <summary>
        /// 将字符串类型转换成可为空的Decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal? StringToNullDecimal(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                decimal d = 0;
                if (decimal.TryParse(str, out d))
                {
                    return d;
                }
                else
                {
                    return null;
                }
            }
            return null;

        }
        /// <summary>
        /// 将字符串类型转换成可为空的Int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? StringToNullInt(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                int d = 0;
                if (int.TryParse(str, out d))
                {
                    return d;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        /// <summary>
        /// 替换中文标点符号
        /// </summary>
        /// <param name="str">传入字符串</param>
        /// <returns></returns>
        public static string ReplaceMark(string str)
        {
            str = System.Web.HttpUtility.HtmlDecode(str);
            str = Regex.Replace(str, @"\b\s+\b", " ");
            str = Regex.Replace(str, @"\s*/\s*", "/");
            str = str.Replace('’', '\'');
            str = str.Replace("、", ",");
            str = str.Replace("。", ".");
            str = str.Replace("？", "?");
            str = str.Replace("（", "(");
            str = str.Replace("）", ")");
            str = str.Replace("，", ",");
            str = str.Replace("“", "\"");
            str = str.Replace("”", "\"");
            str = str.Replace("﹣", "-");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&gt;", ">");
            str = str.Replace("；", ";");
            str = str.Replace("：", ":");
            str = str.Replace("．", ".");
            str = str.Replace('\\', '/');
            str = str.Trim();
            return str;

        }

        /// <summary>
        /// 正则表达式取括号内的内容
        /// </summary>
        /// <param name="cont">传入内容</param>
        /// <param name="bracketsCont">括号内的内容</param>
        /// <returns>是否包含括号</returns>
        public static bool GetBrackets(string cont, out string bracketsCont)
        {
            bracketsCont = "";
            var res = System.Text.RegularExpressions.Regex.IsMatch(cont, @"[\(（][\s\S]*[\)）]");
            if (res == true)
            {
                Match match = Regex.Match(cont, @"[\(（][\s\S]*[\)）]");
                for (int i = 0; i < match.Groups.Count; i++)
                {
                    bracketsCont = match.Groups[i].Value;
                    if (bracketsCont.Contains("分") == false && bracketsCont.Contains("题") == false)
                    {
                        res = false;
                        bracketsCont = "";
                    }
                }
            }
            return res;
        }


        /// <summary>
        ///   转半角的函数(DBC case)  
        ///   任意字符串
        /// 半角字符串 ///
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248 ///
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32; continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// /// 转全角的函数(SBC case) /// 
        ///        任意字符串 /// 全角字符串 ///
        ///全角空格为12288,半角空格为32 
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248 ///
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        { //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288; continue;
                }
                if (c[i] < 127) c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 去除string 集合的首位空格
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<string> ListStringTrim(List<string> list)
        {
            List<string> T = new List<string>();
            foreach (string item in list)
            {
                T.Add(ReplaceMark(item.Trim()));
            }
            return T;
        }

        /// <summary>
        /// 替换并移除P标签
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="ImageUrl"></param>
        /// <param name="ImgDomainTag"></param>
        /// <returns></returns>
        public static string RemovePAndReplaceImageTag(string Content, string ReplaceContent, string ReplaceTag)
        {
            if (string.IsNullOrWhiteSpace(Content))
                return "";
            return Content.Replace("<p>", "").Replace("</p>", "").Replace(ReplaceContent, ReplaceTag);
        }

        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="ptime"></param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime? ptime)
        {
            if (!ptime.HasValue)
            {
                return "";
            }
            DateTime time = DateTime.Parse(ptime.Value.ToString("yyyy-MM-dd"));
            DateTime now = DateTime.Now;
            TimeSpan span = now.Subtract(time);
            int days = span.Days;
            int hours = span.Hours;
            int minutes = span.Minutes;
            string str = string.Empty;
            if (days > 0)
            {
                if (days == 1)
                {
                    str = "昨天 " + ptime.Value.ToString("H:m");
                }
                else
                {
                    if (days == 2)
                    {
                        return ("前天 " + ptime.Value.ToString("H:m"));
                    }
                    if (now.Year == ptime.Value.Year)
                    {
                        str = string.Concat(new object[] { ptime.Value.Month, "月", ptime.Value.Day, "日 ", ptime.Value.ToString("H:m") });
                    }
                    else
                    {
                        str = ptime.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
                    }
                }
                return str;
            }
            if (hours > 0)
            {
                if (hours <= 5)
                {
                    str = hours.ToString() + " 小时前";
                }
                else if (now.Day == ptime.Value.Day)
                {
                    str = "今天 " + ptime.Value.ToString("H:m");
                }
                else
                {
                    str = "昨天 " + ptime.Value.ToString("H:m");
                }
                return str;
            }
            if (minutes > 0)
            {
                str = minutes.ToString() + "分钟前";
            }
            else
            {
                str = "刚刚";
            }
            return str;
        }

        struct Timeclass
        {
            public int Hour;
            public int Minute;
            public int Second;
        }
        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="second">秒</param>
        public static string FormatTime(int second)
        {
            Timeclass i;
            i.Second = second % 60;
            i.Minute = ((second - i.Second) / 60) % 60;
            i.Hour = (second - i.Second) / 3600;
            string result = string.Empty;
            if (i.Hour > 0)
            {
                result = string.Format("{0} 小时 ", i.Hour);
            }
            result += string.Format("{0} 分 {1} 秒", i.Minute, i.Second);
            return result;
        }



        /// <summary>
        /// 将0和1 转换成F和T
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToRight(string str)
        {
            string s = str.Trim();
            if (s == "1" || s == "对" || s == "正确" || s.ToLower() == "t")
            {
                return "正确";
            }
            else if (s == "0" || s == "错" || s == "错误" || s.ToLower() == "f")
            {
                return "错误";
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 替换转义字符
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string ReplaceEscapeTag(this string Content)
        {
            if (string.IsNullOrWhiteSpace(Content))
                return "";
            return Content.Replace("转义字符", @"\");
        }

        /// <summary>
        /// SHA1 Hash加密
        /// </summary>
        /// <param name="str_sha1_in"></param>
        /// <returns></returns>
        public static string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }


        public static string ReplaceAAndSpan(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return "";
            }
            return Regex.Replace(Regex.Replace(content, "<a[^>]*>|</a>$", "", RegexOptions.IgnoreCase), "<span[^>]*>|</span>$", "", RegexOptions.IgnoreCase).Replace("</a>", "").Replace("</span>", "");
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">源字符</param>
        /// <param name="startIndex">开始位置</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length *= -1;
                    if ((startIndex - length) < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex -= length;
                    }
                }
                if (startIndex > str.Length)
                {
                    return "";
                }
            }
            else if ((length >= 0) && ((length + startIndex) > 0))
            {
                length += startIndex;
                startIndex = 0;
            }
            else
            {
                return "";
            }
            if ((str.Length - startIndex) < length)
            {
                length = str.Length - startIndex;
                return str.Substring(startIndex, length);

            }
            else
            {
                return str.Substring(startIndex, length) + "......";
            }
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="longTime">yyyyMMddHHmmss字符串</param>
        /// <returns></returns>
        public static DateTime FormartLongDateTime(string longTime)
        {
            return DateTime.ParseExact(longTime, "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.None);
        }

        /// <summary>
        /// 去除首位空格 为Null 返回null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimNull(this string str)
        {
            if (str == null)
            {
                return null;
            }
            else
            {
                return str.Trim();
            }
        }

        /// <summary>
        /// 获取用户真实IP
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public static string GetIP(HttpContextBase hc)
        {
            string str = hc.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrWhiteSpace(str))
            {
                str = hc.Request.ServerVariables["REMOTE_ADDR"];
            }
            return str;
        }

        /// <summary>
        /// 截取出身年月日
        /// </summary>
        /// <param name="IDCard">身份证号</param>
        /// <returns></returns>
        public static DateTime? GetDate(string IDCard)
        {
            string BirthDay = " ";
            string strYear;
            string strMonth;
            string strDay;
            if (IDCard.Length == 15)
            {
                strYear = IDCard.Substring(6, 2);
                strMonth = IDCard.Substring(8, 2);
                strDay = IDCard.Substring(10, 2);
                BirthDay = "19" + strYear + "- " + strMonth + "- " + strDay;
            }
            if (IDCard.Length == 18)
            {
                strYear = IDCard.Substring(6, 4);
                strMonth = IDCard.Substring(10, 2);
                strDay = IDCard.Substring(12, 2);
                BirthDay = strYear + "- " + strMonth + "- " + strDay;
            }
            if (string.IsNullOrWhiteSpace(BirthDay))
                return null;
            try
            {
                return DateTime.Parse(BirthDay);
            }
            catch (Exception ex)
            {
                AiErLan.Log.LogFactory.CreateLoger("转换出错").Warn("时间转换错误：" + BirthDay);
                return null;
            }
        }
    }
}
