using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    //[共用的ViewBag資料共享於部份HomeController動作方法]這樣寫就是全部套用
    public class HomeController : BaseController
    {
        [共用的ViewBag資料共享於部份HomeController動作方法]
        public ActionResult Index()
        {
            return View();
        }
        
        [共用的ViewBag資料共享於部份HomeController動作方法]
        [紀錄Action的執行時間Attribute]
        public ActionResult About()
        {
            
            return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact()XXXXXXXXXXXXXXXXXX";

            return View();
        }
        
        private ActionResult Test()
        {
            return View();
        }
    }
}