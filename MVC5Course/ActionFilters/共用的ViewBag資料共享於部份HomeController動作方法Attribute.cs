using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class 共用的ViewBag資料共享於部份HomeController動作方法Attribute : ActionFilterAttribute
    {
        //OnActionExecuting執行之前
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "ActionFilter測試~~~~~~~~~~~~~~~~~";
            //if(!filterContext.HttpContext.Request.IsLocal)
            //filterContext.Result = new RedirectResult("/");
            base.OnActionExecuting(filterContext);
        }
    }
}