using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AiErLan.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //AiErLan.Handle.News newsbll = new Handle.News();
            //newsbll.Add(new Data.News
            //{
            //    CreateDateTime = DateTime.Now,
            //    Status = 1,
            //    Type = 1
            //});
         //   var model=   newsbll.GetNewsList();
            //   return View(model);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            AiErLan.Log.LogFactory.CreateLoger(this).Info("----");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}