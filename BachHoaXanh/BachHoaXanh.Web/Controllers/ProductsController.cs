using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.ModelView;
using BachHoaXanh.Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Web.Controllers
{
    public class ProductsController : Controller
    {
        //GET: Products
        public BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        private readonly IProductData db;
       
        public ProductsController(IProductData db)
        {
            this.db = db;
        }
        //   private readonly IClassifyData cdb;

        // GET: Classify
        [HttpGet]
        public ActionResult Category()
        {
            var model = from category in bhx.Categories
                        select category;
            //ViewBag.danhmuc = model;
            return View("_Category",model);
        }
        [HttpGet]
        public ActionResult Classify(string CategoryID)
        {
            var model = from classify in bhx.Classifys
                        where classify.CategoryId == CategoryID
                        select classify;
            ViewBag.classify = model;
            if (model != null)
                return View("DanhMuc_NhomHang", model);
            else
                return View("DanhMuc_NhomHang", model);
        }
        //public ActionResult GetAbout(string id)
        //{
        //    var model = db.Get(id);
        //    return View("Index");
        //}

        [HttpGet]
        public ActionResult Index(string id)
        {
            var model = from sp in bhx.Products
                        where sp.ClassifyId == id
                        select sp;
            var tinhtien = from sp in bhx.Products
                           where sp.ClassifyId == id
                           select sp.Discount;

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
        public ActionResult Create(ItemProduct item)
        {
            if (String.IsNullOrEmpty(item.Id))
            {
                ModelState.AddModelError(nameof(item.Id), "Product's ID is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(item.Name))
            {
                ModelState.AddModelError(nameof(item.Name), "The name is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(item.Price.ToString()))
            {
                ModelState.AddModelError(nameof(item.Price), "Product's price is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(item.Amount.ToString()))
            {
                ModelState.AddModelError(nameof(item.Amount), "Amount is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(item.BranchId))
            {
                ModelState.AddModelError(nameof(item.BranchId), "Branch's ID is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(item.ClassifyId))
            {
                ModelState.AddModelError(nameof(item.ClassifyId), "Classify's ID is required");              
                //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(item.ProviderId))
            {
                ModelState.AddModelError(nameof(item.ProviderId), "Provider's ID is required");      //thong bao loi khi Name co gia tri null/ rong
            }            


            if (ModelState.IsValid)     //Name hop le
            {
                Product p = new Product();
                p.Id = item.Id;
                p.Name = item.Name;
                p.BranchId = item.BranchId;
                p.Amount = item.Amount;
                p.ClassifyId = item.ClassifyId;
                p.ProviderId = item.ProviderId;
                p.Details = item.Details;
                p.Discount = item.Discount;
                p.Price = item.Price;

                string image1 = Guid.NewGuid() + Path.GetExtension(item.Image1.FileName);
                item.Image1.SaveAs(filename: Server.MapPath("~/Images" + image1));
                p.Image1 = item.Image1.ToString();
                string image2 = Guid.NewGuid() + Path.GetExtension(item.Image2.FileName);
                item.Image2.SaveAs(filename: Server.MapPath("~/Images" + image1));
                p.Image2 = item.Image2.ToString();
                string image3 = Guid.NewGuid() + Path.GetExtension(item.Image3.FileName);
                item.Image3.SaveAs(filename: Server.MapPath("~/Images" + image1));
                p.Image3 = item.Image3.ToString();
                db.Add(p);
                return RedirectToAction("Details", new { id = p.Id });
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
        
    }
}