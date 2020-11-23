using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Web.Controllers
{
    public class ProductsController : Controller
    {
        //GET: Products
        private readonly IProductData db;
       
        public ProductsController(IProductData db)
        {
            this.db = db;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Details(string id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }
        [HttpGet]   // use to edit, create restaurant
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]  //use to post, write restaurant
        [ValidateAntiForgeryToken] //thuoc tinh ngan chan mot yeu cau gia mao
        public ActionResult Create(Product product)
        {
            if (String.IsNullOrEmpty(product.Id))
            {
                ModelState.AddModelError(nameof(product.Id), "Product's ID is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError(nameof(product.Name), "The name is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(product.Price.ToString()))
            {
                ModelState.AddModelError(nameof(product.Price), "Product's price is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(product.Amount.ToString()))
            {
                ModelState.AddModelError(nameof(product.Amount), "Amount is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(product.BranchId))
            {
                ModelState.AddModelError(nameof(product.BranchId), "Branch's ID is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(product.ClassifyId))
            {
                ModelState.AddModelError(nameof(product.ClassifyId), "Classify's ID is required");              
                //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(product.ProviderId))
            {
                ModelState.AddModelError(nameof(product.ProviderId), "Provider's ID is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(product.BranchId))
            {
                ModelState.AddModelError(nameof(product.BranchId), "Branch's ID is required");      //thong bao loi khi Name co gia tri null/ rong
            }
          
            if (ModelState.IsValid)     //Name hop le
            {
                db.Add(product);
                return RedirectToAction("Details", new { id = product.Id });
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product products)
        {
            if (ModelState.IsValid)
            {
                db.Update(products);
                TempData["Message"] = "You have saved the product!";
                return RedirectToAction("Details", new { id = products.Id });
            }
            return View(products);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, FormCollection form)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Drinks()
        {
           
            return View();
        }
        [HttpGet]
        public ActionResult Vegetables_Fruits()
        {

            return View();
        }
    }
}