using AiErLan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AiErLan.Web.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly string _cookieKeyByPWD = "AiErLanPwd";
        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            string LoginName = string.Empty;
            string Pwd = string.Empty;
            if (System.Web.HttpContext.Current.Request.Cookies != null && System.Web.HttpContext.Current.Request.Cookies["LoginName"] != null && System.Web.HttpContext.Current.Request.Cookies["RememberPassword"] != null && System.Web.HttpContext.Current.Request.Cookies["RememberPassword"].Value != "0")
            {
                LoginName = System.Web.HttpContext.Current.Request.Cookies["LoginName"].Value;
                LoginName = System.Web.HttpContext.Current.Server.UrlDecode(LoginName);
                ViewBag.LoginName = LoginName;
                //Pwd = System.Web.HttpContext.Current.Request.Cookies["Pwd"].Value;
                Pwd = HttpCookieHelper.GetValueAndDecrypt(_cookieKeyByPWD);
                ViewBag.Pwd = Pwd;
            }
            if (_adminAuthentication.IsAuthenticated)
            {
                return Redirect("/home/index");
            }
            else
            {
                _adminAuthentication.SignOut();
            }

            return View();
        }



        /// <summary>
        /// 登录数据提交
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(Data.Model.LoginModel loginInfo)
        {
            var result = false;
            if (string.IsNullOrEmpty(loginInfo.LoginName) || string.IsNullOrEmpty(loginInfo.Pwd))
            {
                return Json(new { result }, JsonRequestBehavior.AllowGet);
            }
            Handle.Account account = new Handle.Account();
            Data.Admin admin = account.Login(loginInfo.LoginName, loginInfo.Pwd);
            if (admin != null)
            {
                HttpCookie cookieLoginName = new HttpCookie("LoginName");
                HttpCookie cookieRememberPassword = new HttpCookie("RememberPassword");
                if (loginInfo.RememberPassword)
                {
                    HttpCookie cookiePwd = new HttpCookie(_cookieKeyByPWD);
                    cookieLoginName.Value = loginInfo.LoginName;
                    cookieLoginName.Expires = DateTime.Now.AddMonths(1);
                    System.Web.HttpContext.Current.Response.Cookies.Set(cookieLoginName);
                    cookiePwd.Value = loginInfo.Pwd;
                    cookiePwd.Expires = DateTime.Now.AddMonths(1);
                     System.Web.HttpContext.Current.Response.Cookies.Set(cookiePwd);
                    HttpCookieHelper.SetAndEncrypt(cookiePwd);
                    cookieRememberPassword.Value = "1";
                    cookieRememberPassword.Expires = DateTime.Now.AddMonths(1);
                    System.Web.HttpContext.Current.Response.Cookies.Set(cookieRememberPassword);
                }
                else
                {
                    cookieRememberPassword.Value = "0";
                    cookieRememberPassword.Expires = DateTime.Now.AddMonths(1);
                    System.Web.HttpContext.Current.Response.Cookies.Set(cookieRememberPassword);
                }
                result = true; 
                _adminAuthentication.SignIn(admin.Name, admin, false);
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            _adminAuthentication.SignOut();
            return Redirect(_adminAuthentication.LoginName);
        }
        /// <summary>
        /// 判断用户是否登录
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckLogin()
        {
            if (!_adminAuthentication.IsAuthenticated)
                return Json(false, JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}