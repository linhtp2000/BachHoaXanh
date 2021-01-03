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
    public class ManageProductController : Controller
    {
        // GET: ManageProduct
        public BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        public BachHoaXanhDbContext bhx2 = new BachHoaXanhDbContext();
        SqlBranchData dbBranch = new SqlBranchData();
        SqlCategoryData dbCategory = new SqlCategoryData();
        SqlClassifyData dbClassify = new SqlClassifyData();
        SqlCartData dbCart = new SqlCartData();
        SqlProductData dbProduct = new SqlProductData();
        SqlProviderData dbProvider = new SqlProviderData();
        public ActionResult Index()
        {
            var model = dbProduct.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Details(string id)
        {
            //var model = db.Get(id);
            var model = (from sp in bhx.Products
                         where sp.Id == id
                         select sp).FirstOrDefault();
            
            return View(model);
        }
        [HttpGet]   // use to edit, create restaurant
        public ActionResult Create()
        {
            ViewBag.BranchName = new SelectList(bhx.Branchs, "Id", "Name", 1);
            ViewBag.ClassifyName = new SelectList(bhx.Classifys, "Id", "Name", 1);
            ViewBag.ProviderName = new SelectList(bhx.Providers, "Id", "Name", 1);

            //Session["BranchName"] = new SelectList(bhx.Branchs, "Id", "Name", 1);
            // Session["ClassifyName"] = new SelectList(bhx.Classifys, "Id", "Name", 1);
            // Session["ProviderName"] = new SelectList(bhx.Providers, "Id", "Name", 1);

            return View();
        }

        [HttpPost]  //use to post, write restaurant
        [ValidateInput(false)]
        public ActionResult Create(ItemProduct item, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3)
        {

            //IEnumerable<Branch> lBranchs = dbBranch.GetAll();
            //IEnumerable<Provider> lProviders = dbProvider.GetAll();
            //IEnumerable<Classify> lClassifys = dbClassify.GetAll();

            //ViewBag.Branch.Name = lBranchs;
            //ViewBag.Classify.Name = lClassifys;
            //ViewBag.ProviderId.Name = lProviders;

            //ViewBag.BranchName = new SelectList(bhx.Branchs, "Id", "Name", item.Branch.Id);
            //ViewBag.ClassifyName = new SelectList(bhx.Classifys, "Id", "Name", item.Classify.Id);
            //ViewBag.ProviderName = new SelectList(bhx.Providers, "Id", "Name", item.Provider.Id);

            ViewBag.BranchName = new SelectList(bhx.Branchs, "Id", "Name", 1);
            ViewBag.ClassifyName = new SelectList(bhx.Classifys, "Id", "Name", 1);
            ViewBag.ProviderName = new SelectList(bhx.Providers, "Id", "Name", 1);
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
            if (String.IsNullOrEmpty(item.BranchName))
            {
                ModelState.AddModelError(nameof(item.BranchName), "Branch is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(item.ClassifyName))
            {
                ModelState.AddModelError(nameof(item.ClassifyName), "Classify is required");
                //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(item.ProviderName))
            {
                ModelState.AddModelError(nameof(item.ProviderName), "Provider is required");      //thong bao loi khi Name co gia tri null/ rong
            }

            //List<Branch> lBranchs = dbBranch.GetAll() as List<Branch>;
            //List<Provider> lProviders = dbProvider.GetAll() as List<Provider>;
            //List<Classify> lClassifys = dbClassify.GetAll() as List<Classify>;

            //ViewBag.BranchName = lBranchs;
            //ViewBag.ClassifyName = lClassifys;
            //ViewBag.ProviderName = lProviders;

            //string branch = bhx.Branchs.Where(x => x.Name == item.BranchName).Select(x=> x.Id).First().ToString();
            //Classify classify = bhx.Classifys.Where(x => x.Name == item.ClassifyName).FirstOrDefault();
            //Provider provider = bhx.Providers.Where(x => x.Name == item.ProviderName).FirstOrDefault(); 
            if (ModelState.IsValid)
            {
                Product p = new Product();
                p.Id = dbProduct.GetIdMax().ToString();
                p.Name = item.Name;
                p.BranchId = item.BranchName;
                p.Amount = item.Amount;
                p.ClassifyId = item.ClassifyName;
                p.ProviderId = item.ProviderName;
                p.Details = item.Details;
                p.Discount = item.Discount;
                p.Price = item.Price;
                p.Date = item.Date;

                if (Image1.ContentLength > 0)
                {
                    //Lay ten hinh anh
                    string image1 = p.Id + ".1" + Path.GetExtension(item.Image1.FileName);  //GetExstension
                    //item.Image1.SaveAs(filename: ("E:/LTWeb/BachHoaXanh_Update/BachHoaXanh/BachHoaXanh.Web/Images/" + image1));                 
                    var path = Path.Combine(Server.MapPath("~/Images"), image1);
                    //Neu thu muc chua hinh anh do roi thi xuat ra thong bao
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.upload = " Image has existed!";
                    }
                    else
                    {
                        item.Image1.SaveAs(path);
                    }
                    string temp = "~/Images/" + image1;
                    p.Image1 = temp;
                }
                else
                {
                    p.Image1 = null;
                }

                if (Image2.ContentLength > 0)
                {
                    //Lay ten hinh anh
                    string image2 = p.Id + ".2" + Path.GetExtension(item.Image2.FileName);  //GetExstension
                    //item.Image1.SaveAs(filename: ("E:/LTWeb/BachHoaXanh_Update/BachHoaXanh/BachHoaXanh.Web/Images/" + image1));                 
                    var path = Path.Combine(Server.MapPath("~/Images"), image2);
                    //Neu thu muc chua hinh anh do roi thi xuat ra thong bao
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.upload = " Image has existed!";
                        // p.Image2 = path.ToString();
                    }
                    else
                    {
                        item.Image2.SaveAs(path);
                        // p.Image2 = path.ToString();
                    }
                    string temp = "~/Images/" + image2;
                    p.Image2 = temp;
                }
                else
                {
                    p.Image2 = null;
                }


                if (Image3.ContentLength > 0)
                {
                    //Lay ten hinh anh
                    string image3 = p.Id + ".3" + Path.GetExtension(item.Image3.FileName);  //GetExstension
                    //item.Image1.SaveAs(filename: ("E:/LTWeb/BachHoaXanh_Update/BachHoaXanh/BachHoaXanh.Web/Images/" + image1));                 
                    var path = Path.Combine(Server.MapPath("~/Images"), image3);
                    //Neu thu muc chua hinh anh do roi thi xuat ra thong bao
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.upload = " Image has existed!";
                    }
                    else
                    {
                        item.Image3.SaveAs(path);
                    }
                    string temp = "~/Images/" + image3;
                    p.Image3 = temp;
                }
                else
                {
                    p.Image3 = null;
                }

                bhx.Products.Add(p);
                bhx.SaveChanges();
                return RedirectToAction("ProductDetails", "Admin", new { id = p.Id });
            }
            return View("ProductCreate");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
           
            var item = dbProduct.Get(id);
            ViewBag.BranchName = new SelectList(bhx.Branchs, "Id", "Name", item.BranchId);
            ViewBag.ClassifyName = new SelectList(bhx.Classifys, "Id", "Name", item.ClassifyId);
            ViewBag.ProviderName = new SelectList(bhx.Providers, "Id", "Name", item.ProviderId);
            ItemProduct p = new ItemProduct();
           
            p.Id = dbProduct.GetIdMax().ToString();
            p.Name = item.Name;          
            p.Amount = item.Amount;          
            p.Details = item.Details;
            p.Discount = item.Discount;
           
            p.Price = item.Price;
            p.Date = item.Date;
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemProduct item, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3)
        {
            
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
            if (String.IsNullOrEmpty(item.BranchName))
            {
                ModelState.AddModelError(nameof(item.BranchName), "Branch is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(item.ClassifyName))
            {
                ModelState.AddModelError(nameof(item.ClassifyName), "Classify is required");
                //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(item.ProviderName))
            {
                ModelState.AddModelError(nameof(item.ProviderName), "Provider is required");      //thong bao loi khi Name co gia tri null/ rong
            }

            if (ModelState.IsValid)
            {
                Product p = new Product();
                p.Id = dbProduct.GetIdMax().ToString();
                p.Name = item.Name;
                p.BranchId = item.BranchName;
                p.Amount = item.Amount;
                p.ClassifyId = item.ClassifyName;
                p.ProviderId = item.ProviderName;
                p.Details = item.Details;
                p.Discount = item.Discount;
                p.Price = item.Price;
                p.Date = item.Date;

                if (Image1.ContentLength > 0)
                {
                    //Lay ten hinh anh
                    string image1 = p.Id + ".1" + Path.GetExtension(item.Image1.FileName);  //GetExstension
                    //item.Image1.SaveAs(filename: ("E:/LTWeb/BachHoaXanh_Update/BachHoaXanh/BachHoaXanh.Web/Images/" + image1));                 
                    var path = Path.Combine(Server.MapPath("~/Images"), image1);
                    //Neu thu muc chua hinh anh do roi thi xuat ra thong bao
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.upload = " Image has existed!";
                    }
                    else
                    {
                        item.Image1.SaveAs(path);
                    }
                  //  string temp = 
                    p.Image1 = "~/Images/" + image1;
                }
                else
                {
                    p.Image1 = null;
                }

                if (Image2.ContentLength > 0)
                {
                    //Lay ten hinh anh
                    string image2 = p.Id + ".2" + Path.GetExtension(item.Image2.FileName);  //GetExstension
                    //item.Image1.SaveAs(filename: ("E:/LTWeb/BachHoaXanh_Update/BachHoaXanh/BachHoaXanh.Web/Images/" + image1));                 
                    var path = Path.Combine(Server.MapPath("~/Images"), image2);
                    //Neu thu muc chua hinh anh do roi thi xuat ra thong bao
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.upload = " Image has existed!";
                        // p.Image2 = path.ToString();
                    }
                    else
                    {
                        item.Image2.SaveAs(path);
                        // p.Image2 = path.ToString();
                    }
                  //  string temp =
                    p.Image2 = "~/Images/" + image2;
                }
                else
                {
                    p.Image2 = null;
                }


                if (Image3.ContentLength > 0)
                {
                    //Lay ten hinh anh
                    string image3 = p.Id + ".3" + Path.GetExtension(item.Image3.FileName);  //GetExstension
                    //item.Image1.SaveAs(filename: ("E:/LTWeb/BachHoaXanh_Update/BachHoaXanh/BachHoaXanh.Web/Images/" + image1));                 
                    var path = Path.Combine(Server.MapPath("~/Images"), image3);
                    //Neu thu muc chua hinh anh do roi thi xuat ra thong bao
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.upload = " Image has existed!";
                    }
                    else
                    {
                        item.Image3.SaveAs(path);
                    }
                  //  string temp = 
                    p.Image3 = "~/Images/" + image3;
                }
                else
                {
                    p.Image3 = null;
                }

                
               var temp= bhx.Products.Find(item.Id);
                bhx.Products.Remove(temp);
                bhx.Products.Add(p);
                bhx.SaveChanges();
                return RedirectToAction("Details", new { id = p.Id });
            }
            return View("ProductCreate");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var model = dbProduct.Get(id);
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product model)
        {
            bhx.Products.Remove(model);
            bhx.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}