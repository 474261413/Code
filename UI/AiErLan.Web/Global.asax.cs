using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AiErLan.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
           AiErLan.Log.LogFactory.Initialize(isWeb: true);
            AiErLan.Config.MessageQueueConfig.RegisterExceptionLogQueue();
            AiErLan.Config. GlobalApp.Instance.Initialize();
        }
    }
}
