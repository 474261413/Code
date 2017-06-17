using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using AiErLan.Log;
using log4net;

namespace AiErLan.Utils
{
    public class HttpCookieHelper
    {
        /// <summary>
        /// 设置Cookie值并将Value进行URL方式编码
        /// </summary>
        /// <param name="cookie">要设置的cookie</param>
        public static void SetAndEscape(HttpCookie cookie)
        {
            if (cookie == null)
            {
                throw new ArgumentNullException();
            }
            cookie.Value = Uri.EscapeDataString(cookie.Value);
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
        /// <summary>
        /// 得到Cookie中对应Name的值,并将值就行URL方式解码
        /// 返回值：存在返回Cookie中的value解码值,否则Null, 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetValueAndUnescape(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var cookie = HttpContext.Current.Request.Cookies.Get(name);
            if (cookie == null)
            {
                return null;
            }
            var value = Uri.UnescapeDataString(cookie.Value);
            return value;
        }
        /// <summary>
        /// 设置Cookie，并将Value进行加密
        /// </summary>
        /// <param name="cookie">要设置的cookie</param>
        public static void SetAndEncrypt(HttpCookie cookie)
        {
            if (cookie == null)
            {
                throw new ArgumentNullException();
            }
            cookie.Value = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(cookie.Value, false, 0));
            //cookie.Value = Uri.EscapeDataString(cookie.Value);
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
        /// <summary>
        /// 得到Cookie中对应Name的值,并将值就行解密处理
        /// 返回值：存在返回Cookie中的value解密值,否则Null, 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetValueAndDecrypt(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(name);
            if (cookie == null)
            {
                return null;
            }
            string value = null;
            try
            {   if (string.IsNullOrWhiteSpace(cookie.Value))
                    return null;
                var result = FormsAuthentication.Decrypt(cookie.Value);
                value = result.Name;
            }
            catch (Exception ex)
            {
                log4net.ILog loger = LogFactory.CreateLoger("HttpCookieHelper");
                loger.Info("cookie"+ cookie.Value, ex);
            }
            return value;
        }
    }
}
