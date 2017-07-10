using AiErLan.Data.Enums;
using AiErLan.Data.Model;
using AiErLan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AiErLan.Web.Controllers
{
    public class ProductController : Controller
    {
        AiErLan.Handle.News newsbll = new Handle.News();
        // GET: Product
        public ActionResult Index(NewsReq rep)
        {
            int total = 0;
            rep.PageIndex = rep.PageIndex ?? 1;
            rep.PageSize = rep.PageSize ?? 5;
            var list = newsbll.GetNewsList(rep, out total);
            ViewBag.Pager = PagerHelper.CreatePagerByAjax(rep.PageIndex.Value, rep.PageSize.Value, total, "AiErLan.NewsPage");
            if (Request.IsAjaxRequest())
            {
                return View("_index", list);
            }
            return View(list);
        }

        public ActionResult Detail(long id=0) {

         var model=   newsbll.GetNewsById(id, (int)NewsType.CP);
            return View(model);
        }
    }
}