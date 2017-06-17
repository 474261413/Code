 using AiErLan.Config.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AiErLan.Web.Areas.Admin.Controllers
{
  
    public class BaseController : Controller
    {
      //  protected Data.DataOperate DbHelp = new Data.DataOperate();
        protected readonly AelAuthentication _adminAuthentication;
        public BaseController()
        {
            _adminAuthentication = new AelAuthentication();
        }
        /// <summary>
        /// 当前登录的账号
        /// </summary>
        protected Data.Admin Admin
        {
            get
            {
                if (_adminAuthentication.IsAuthenticated)
                    return _adminAuthentication.GetCurrentLoginUser<Data.Admin>();
                return null;
            }
        }

        public void RedirectUrl(string url)
        {
            this.Response.Clear();//这里是关键，清除在返回前已经设置好的标头信息，这样后面的跳转才不会报错
            this.Response.BufferOutput = true;//设置输出缓冲
            if (!this.Response.IsRequestBeingRedirected)//在跳转之前做判断,防止重复
            {
                this.Response.Redirect(url, true);
            }
        }
    }
}