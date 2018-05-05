using AiErLan.Data.Model;
using AiErLan.Data.Enums;

using AiErLan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AiErLan.Web.Controllers
{

    public class AiErLanAPIController : Controller
    {
        private ApiCallResult<object> mode = new ApiCallResult<object>();
        AiErLan.Handle.News newsbll = new Handle.News();
        AiErLan.Handle.Client clientbll = new Handle.Client();

        [HttpPost]
        [Route("GetNewsList")]
        public JsonResult GetNewsList(NewsReq rep)
        {
            int total = 0;
            rep.PageIndex = rep.PageIndex ?? 1;
            rep.PageSize = rep.PageSize ?? 5;
            var list = newsbll.GetNewsList(rep, out total);
            mode.data = list;
            mode.isSuccess = true;
            return Json(mode, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("GetAddClient")]
        public JsonResult GetAddClient(Data.Client client)
        {
            long id = 0;
            mode.isSuccess = clientbll.Edit(client, out id);
            mode.data = new { id = id };
            return Json(mode, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Route("test")]
        public JsonResult test()
        {

            return Json(mode, JsonRequestBehavior.AllowGet);
        }

    }
}
