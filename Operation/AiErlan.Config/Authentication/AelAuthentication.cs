using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
namespace AiErLan.Config.Authentication
{
  public  class AelAuthentication
    {

        /// <summary>
        /// 保存票据信息
        /// </summary>
        /// <param name="name">用户标识符</param>
        /// <param name="userName">用户名称，显示名，后台显示的是真实名称，前台待定</param>
        /// <param name="createPersistentCookie"></param>
        public virtual void SignIn(string name, object obj, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();
            var expirationTimeSpan = FormsAuthentication.Timeout;
            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
              name,
                now,
                now.Add(expirationTimeSpan),
                createPersistentCookie,
                JsonConvert.SerializeObject(obj),
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Sign out
        /// </summary>
        public virtual void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public T GetCurrentLoginUser<T>()
        {
            var formsIdentity = (FormsIdentity)HttpContext.Current.User.Identity;
            var val = formsIdentity.Ticket.UserData;
            if (string.IsNullOrEmpty(val))
            {
                throw new Exception("登录过期");
            }
            return JsonConvert.DeserializeObject<T>(val);
        }

        public string GetUserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public bool IsAuthenticated
        {
            get { return HttpContext.Current.Request.IsAuthenticated; }
        }
        public string LoginName
        {
            get { return FormsAuthentication.LoginUrl; }
        }
    }
}
