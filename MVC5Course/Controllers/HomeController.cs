using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        [HandleError(ExceptionType = typeof(ArgumentException), View = "ErrorArgument")]
        [HandleError(ExceptionType = typeof(SqlException), View = "ErrorSql")]
        public ActionResult ErrorTest(string errorType)
        {
            if (errorType == "1")
            {
                throw new Exception("Error 1");
            }
            if (errorType == "2")
            {
                throw new ArgumentException("Error 2");
            }
            return Content("Error 3");
        }
        public ActionResult RazorTest()
        {
            int[] data = new int[] { 1, 2, 3, 4, 5 };
            return PartialView(data);
        }
    }
}