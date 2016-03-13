using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;
using System.Data.SqlClient;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index()
        {
            var data = repo.All().Take(5);
            //var data = repo.Get超級複雜的資料集();
            //var repoOL = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);
            //ViewData.Model = data;//強型別

            return View(data);
            //return View(db.Product.Where(p => !p.IsDeleted));
        }
        [HttpPost]
        //public ActionResult Index(IList<Product> products)//這是使用強型別的model binder，也可這樣寫Product[]
        public ActionResult Index(IList<Products批次更新VIewModel> data)
        {                                                 //參數名稱要注意要跟View的變數一樣名稱
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var product = repo.Find(item.ProductId);
                    product.Stock = item.Stock;
                    product.Price = item.Price;
                }
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(repo.All().Take(5));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewData["OrderLines"] = product.OrderLine.ToList();
            ViewBag.OrderLines = product.OrderLine.ToList();
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                TempData["ProductsEditDoneMsg"] = "商品編輯成功";
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        public ActionResult Edit(int id, FormCollection form)//後面FormCollection純粹為了overload辨識
        {
            Product product = repo.Find(id);

            //if (ModelState.IsValid)
            
            if (TryUpdateModel<Product>(product, new string[] {
                "ProductId","ProductName","Price","Active","Stock"
            }))
            {
                /*var db = (FabricsEntities)repo.UnitOfWork.Context;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();*/
                try
                {
                    //TryUpdateModel一樣會做model驗證，失敗就回傳false，實做過程中，Commit()有Exception可能是資料庫來的
                    repo.UnitOfWork.Commit();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                    {
                        string EntityName = item.Entry.Entity.GetType().Name;
                        foreach (var err in item.ValidationErrors)
                        {
                            throw new Exception(err.ErrorMessage);
                            //throw new Exception(err.ErrorMessage);
                            //return ViewBag(err.ErrorMessage);

                        }
                    }
                    //if (allErrorList.Count != 0)
                    //    return View(allErrorList.ToList());

                }catch(SqlException SE)
                {
                    throw SE;
                }catch(Exception EX)
                {
                    throw EX;
                }
                TempData["ProductsEditDoneMsg"] = "商品編輯成功";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repo.Find(id);
            product.IsDeleted = true;
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = (FabricsEntities)repo.UnitOfWork.Context;
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
