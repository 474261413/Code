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
    public class NewsController : Controller
    {
        AiErLan.Handle.News newsbll = new Handle.News();

        // GET: News
        public ActionResult Index(NewsReq rep)
        {
            int total = 0;
            rep.PageIndex = rep.PageIndex ?? 1;
            rep.PageSize = rep.PageSize ?? 5;
            rep.Type = (int)Data.Enums.NewsType.XW;
            var list = newsbll.GetNewsList(rep, out total);
            ViewBag.Pager = PagerHelper.CreatePagerByAjax(rep.PageIndex.Value, rep.PageSize.Value, total, "AiErLan.News");
            if (Request.IsAjaxRequest())
            {
                return View("NewsList", list);
            }
            return View(list);
        }

        [OutputCache(Duration = 18000, Location = System.Web.UI.OutputCacheLocation.Any, VaryByParam = "id")]
        public ActionResult Detail(long id = 0)
        { 
            var model = newsbll.GetNewsById(id, (int)NewsType.XW);
            return View(model);
        }
    }
}