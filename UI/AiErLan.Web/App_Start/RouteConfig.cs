﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AiErLan.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //              name: "html",
            //              url: "{controller}/{action}/{id}.xxx",
            //              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //             namespaces: new string[] { "AiErLan.Web.Controllers" } 
            //          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "AiErLan.Web.Controllers" }

            );
        }
    }
}
