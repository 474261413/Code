using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AiErLan.Config.Filter
{
    public class AelExceptionFilterAttribute : HandleErrorAttribute
    {
        //版本1：使用预置队列类型存储异常对象
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>();

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is System.Web.HttpRequestValidationException)
                return;
            if (!filterContext.Exception.Message.Contains("服务器无法在已发送 HTTP 标头之后设置状态。"))
                ExceptionQueue.Enqueue(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}
