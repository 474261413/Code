using AiErLan.Config.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AiErLan.Web.Areas.Admin.Controllers
{
    [AELAuthorizeFilter]
    public class HomeController : BaseController
    {
        // GET: Admin/Home

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main() {

            return View();
        }
    }
}