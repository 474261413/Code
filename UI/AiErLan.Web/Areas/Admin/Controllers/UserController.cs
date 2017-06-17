using AiErLan.Config.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AiErLan.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [AELAuthorizeFilter]
    public class UserController : BaseController
    {
        Handle.Account account = new Handle.Account();

        // GET: Admin/User
        public ActionResult Index()
        {
            IQueryable<Data.Admin> list = account.GetAdminByFun(s => s.Status == 1);
            return View(list);
        }
        public JsonResult EidtPwd(string name, string pwd, string npwd)
        {
            bool b = account.EditPwd(name, pwd, npwd);
            return Json(new { result = b }, JsonRequestBehavior.AllowGet);
        }

    }
}