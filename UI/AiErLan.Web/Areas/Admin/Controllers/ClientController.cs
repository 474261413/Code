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
    /// 客户管理
    /// </summary>
    [AELAuthorizeFilter]
    public class ClientController : Controller
    {
        AiErLan.Handle.Client clientbll = new Handle.Client();
        public ActionResult Index(ClientReq rep)
        {
            int total = 0;
            rep.PageIndex = rep.PageIndex ?? 1;
            rep.PageSize = rep.PageSize ?? 15;
            var list = clientbll.GetClientList(rep, out total);
            ViewBag.Pager = PagerHelper.CreatePagerByAjax(rep.PageIndex.Value, rep.PageSize.Value, total, "AiErLan.ClientPage");
            if (Request.IsAjaxRequest())
            {
                return View("_index", list);
            }
            return View(list);
        }

    }
}