using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View("Index");
        }
        public ActionResult ContentTest()
        {
            return Content("<script>alert('Redriecting ...');</script>", "application/javascript", Encoding.UTF8);
        }
        public ActionResult FileTest()
        {
            return File(Server.MapPath("~/Content/alphago-logo.png"), "image/png", "GoGoGo.PNG");
        }

        private ActionResult File(string v)
        {
            throw new NotImplementedException();
        }

        public ActionResult JsonTest()
        {
            var db = new FabricsEntities();
            db.Configuration.LazyLoadingEnabled = false;//延遲載入
            var data = db.Product.Take(5);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RedirectTest()
        {
            return RedirectToAction("Edit", "Products", new { id = 1 });
        }
    }
}