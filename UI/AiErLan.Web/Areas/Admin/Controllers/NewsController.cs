using AiErLan.Config.Filter;
using AiErLan.Data.Model;
using AiErLan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AiErLan.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 文章管理
    /// </summary>
    [AELAuthorizeFilter]
    public class NewsController : BaseController
    {
        AiErLan.Handle.News newsbll = new Handle.News();
        // GET: Admin/News
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

        public ActionResult EidtNews(long? id)
        {
            Data.News model = null;
            if (id.HasValue)
            {
                model = newsbll.GetNewsById(id);
            }
            model = model ?? new Data.News();
            //model.Type = (int)Data.Enums.NewsType.XW;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EidtNews(Data.News news)
        {
            long id = 0;
            bool result = false;
            news.Contents = Request["editorValue"];
            //  news.Type = (int)Data.Enums.NewsType.HYXW;
            news.UserID = Admin.Id;
            result = newsbll.Edit(news, out id);

            return Json(new { success = result, id = id }, JsonRequestBehavior.AllowGet);
        }

    }
}