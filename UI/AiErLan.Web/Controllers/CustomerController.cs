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
    public class CustomerController : Controller
    {
        AiErLan.Handle.News newsbll = new Handle.News();
      /// <summary>
      /// 经典案例
      /// </summary>
      /// <param name="rep"></param>
      /// <returns></returns>
        public ActionResult Index(NewsReq rep)
        {
            if (Request.IsAjaxRequest())
            {
                int total = 0;
                rep.PageIndex = rep.PageIndex ?? 1;
                rep.PageSize = rep.PageSize ?? 5;
                rep.Type = (int)Data.Enums.NewsType.JDAL;
                var list = newsbll.GetNewsList(rep, out total);
                ViewBag.Pager = PagerHelper.CreatePagerByAjax(rep.PageIndex.Value, rep.PageSize.Value, total, "AiErLan.Customer");
                return View("_index", list);
            }
            return View();
        }

        public ActionResult Detail(long id = 0)
        { 
            var model = newsbll.GetNewsById(id, (int)NewsType.JDAL);
            return View(model);
        }
    }
}