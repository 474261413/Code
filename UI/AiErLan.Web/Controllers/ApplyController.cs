using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AiErLan.Web.Controllers
{
    /// <summary>
    /// 预约报名
    /// </summary>
    public class ApplyController : Controller
    {
        // GET: Apply
        public ActionResult Index()
        {
            string userAgent = Request.UserAgent.ToLower();
            //判断是否是微信访问
            if (userAgent.Contains("micromessenger"))
            {
            }
            return View();
        }

        /// <summary>
        /// 添加报名预约
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public JsonResult EidtApply(Data.Client client)
        {
            bool b = true;
            string msg = "";
            client.Status = 1;
            AiErLan.Handle.Client clientbll = new Handle.Client();
            long id = 0;
            b = clientbll.Edit(client, out id);
            return Json(new { success = b, msg = msg }, JsonRequestBehavior.AllowGet);
        }
    }
}