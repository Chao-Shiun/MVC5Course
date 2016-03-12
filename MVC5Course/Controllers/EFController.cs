using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;
using System.Data.Entity;
namespace MVC5Course.Controllers
{
    public class EFController : BaseController
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

            var data = db.Product.OrderByDescending(p => p.ProductId).Take(50);

            //var data = db.Product.ToList();

            return View(data);
        }
        [HttpGet]
        public ActionResult Index(bool? isActive, string keyword)
        {
            var data = db.Product.OrderByDescending(p => p.ProductId).AsQueryable();
            if (isActive.HasValue)
            {
                data = data.Where(p => p.Active.HasValue ? p.Active.Value == isActive.Value : false);
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(p => p.ProductName.Contains(keyword));
            }

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
            //var item = db.Product.Find(id);
            //db.Product.Remove(item);
            var product = db.Product.Find(id);

            //刪除關聯資料的方法一
            /*foreach (var item in db.OrderLine.Where(p=>p.ProductId==id).ToList())
            {
                db.OrderLine.Remove(ol);
            }*/

            //刪除關聯資料的方法二
            /*foreach (var ol in product.OrderLine.ToList())
            {
                    db.OrderLine.Remove(ol);
            }*/

            //刪除關聯資料的方法三
            db.OrderLine.RemoveRange(product.OrderLine);
            db.Product.Remove(product);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult QueryPlan(int num=10)
        {
            var data = db.Product.Include(t => t.OrderLine).OrderBy(p => p.ProductId).AsQueryable();

            //var data = db.Database.SqlQuery<Product>(@"
            //            SELECT * 
            //            FROM dbo.Product p
            //            WHERE p.ProductId < @p0", num);
            
            return View(data);
        }
    }
}