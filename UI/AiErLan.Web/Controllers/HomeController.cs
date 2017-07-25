using AiErLan.Data.Enums;
using AiErLan.Data.Model;
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
            AiErLan.Handle.News newsbll = new Handle.News();
            var productlist = newsbll.GetNewsList(s => s.Status == (int)NewsStatus.XS && s.Type == (int)NewsType.CP).OrderByDescending(s => s.CreateDateTime).Take(8);
            var newslist = newsbll.GetNewsList(s => s.Status == (int)NewsStatus.XS && s.Type == (int)NewsType.XW).OrderByDescending(s => s.CreateDateTime).Take(4);
            var customer = newsbll.GetNewsList(s => s.Status == (int)NewsStatus.XS && s.Type == (int)NewsType.JDAL).OrderByDescending(s => s.CreateDateTime).Take(8);
            IndexView model = new IndexView
            {
                News = newslist,
                Product = productlist,
                Customer = customer
            };
            return View(model);
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