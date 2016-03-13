using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    //[Authorize]認證專用
    //ActionFilter寫在父類別，代表全部繼承這個Controller的都會套用ActionFilter
    public abstract class BaseController : Controller
    {
        protected ProductRepository repo = RepositoryHelper.GetProductRepository();

        /// <summary>
        /// 如果沒有找到Conntoller底下的Action會呼叫此方法
        /// </summary>
        /// <param name="actionName">Action的名稱</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Redirect("/").ExecuteResult(this.ControllerContext);
            //base.HandleUnknownAction(actionName);
            //this.View("Index").ExecuteResult(this.ControllerContext);
        }

        public ActionResult Debug()
        {
            return View("DEBUG");
        }
    }
}