using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index()
        {
            var db = new FabricsEntities();
            List<string> allErrorList = new List<string>();
            Product product = new Product()
            {
                ProductName = "BENZ",
                Price = 9,
                Stock = 1,
                Active = true
            };
            db.Product.Add(product);

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    string EntityName = item.Entry.Entity.GetType().Name;
                    foreach (var err in item.ValidationErrors)
                    {
                        allErrorList.Add(err.ErrorMessage);
                        //throw new Exception(err.ErrorMessage);
                        //return ViewBag(err.ErrorMessage);

                    }
                }
                //if (allErrorList.Count != 0)
                //    return View(allErrorList.ToList());

            }
            var pkey = product.ProductId;

            //var data = db.Product.Where(p => p.ProductId == pkey).ToList();

            var data = db.Product.OrderByDescending(p => p.ProductId).Take(5);

            //var data = db.Product.ToList();

            return View(data);
        }

        public ActionResult Detail(int id)
        {
            //var data= db.Product.Find(id);
            //var data = db.Product.Where(p => p.ProductId == id).FirstOrDefault();
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var item = db.Product.Find(id);
            db.Product.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}