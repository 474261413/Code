using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
namespace AiErLan.Config.Filter
{
  public  class AELAuthorizeFilterAttribute: AuthorizeAttribute
    {

        /// <summary>
        /// 处理未能授权的 HTTP 请求。
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult(499);
                filterContext.HttpContext.Response.Write(FormsAuthentication.LoginUrl);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
