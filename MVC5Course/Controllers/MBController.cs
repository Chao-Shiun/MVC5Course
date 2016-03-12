using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : Controller
    {
        // GET: MB
        public ActionResult Index()
        {
            return View();
        }

        //方法一
        [HttpPost]
        public ActionResult Index(string Name, DateTime DateOfBirth)
        {
            return Content(Name + "   " + DateOfBirth);
        }

        //方法二
        /*[HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            //fc["Name"]沒出來，fc的陣列參數要跟View裡面欄位的Name同名
            return Content(fc["FirstName"] +"  "+fc["DateOfBirth"]);
        }*/

        //不建議的寫法
        //[HttpPost]
        //public ActionResult Index(int a)
        //{
        //    return Content(Request.Form["Name"] + " " + Request.Form["Birthday"]);
        //}
    }
}