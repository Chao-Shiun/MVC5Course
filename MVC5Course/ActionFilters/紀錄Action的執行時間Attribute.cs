using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class 紀錄Action的執行時間Attribute : ActionFilterAttribute
    {
        Stopwatch sw = new Stopwatch();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Action執行前
            sw = Stopwatch.StartNew();
            sw.Start();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Action執行後
            sw.Stop();
            TimeSpan el = sw.Elapsed;
            filterContext.Controller.ViewBag.Message = "載入到結束共花費" + sw.ElapsedMilliseconds/1000.0+"秒";
            base.OnActionExecuted(filterContext);
        }
    }
}