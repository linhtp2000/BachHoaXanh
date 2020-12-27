﻿using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.ModelView;
using BachHoaXanh.Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace BachHoaXanh.Web.Controllers
{
    public class ProductsController : Controller
    {
        //GET: Products

        public BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        private IProductData db;
        SqlCartData dbCart = new SqlCartData();
        SqlProductData dbProduct = new SqlProductData();
        public ProductsController(IProductData db)
        {
            this.db = db;
        }
        //   private readonly IClassifyData cdb;

        // GET: Classify
        //[HttpGet]
        //public ActionResult Category()
        //{
        //    var model = from category in bhx.Categories
        //                select category;
        //    //ViewBag.danhmuc = model;
        //    return View("_Category",model);
        //}
        //[HttpGet]
        //public ActionResult Classify(string CategoryID)
        //{
        //    var model = from classify in bhx.Classifys
        //                where classify.CategoryId == CategoryID
        //                select classify;
        //    ViewBag.classify = model;
        //    if (model != null)
        //        return View("DanhMuc_NhomHang", model);
        //    else
        //        return View("DanhMuc_NhomHang", model);
        //}
        //public ActionResult GetAbout(string id)
        //{
        //    var model = db.Get(id);
        //    return View("Index");
        //}

        //[HttpGet]
        //public ActionResult Index(string id)
        //{
        //    var model = from sp in bhx.Products
        //                where sp.ClassifyId == id
        //                select sp;
        //    var tinhtien = from sp in bhx.Products
        //                   where sp.ClassifyId == id
        //                   select sp.Discount;

        //    return View(model);
        //}
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult Index(int? page, string id, string tieuchuanloc)
        {
            ViewBag.PriceSortParm = String.IsNullOrEmpty(tieuchuanloc) ? "giagiam" : "";
            IEnumerable<Product> model;
            if (id != null)
            {
                model = (from sp in bhx.Products
                         where sp.ClassifyId == id
                         select sp);//.OrderBy(s => s.Name);
                ViewBag.All = 0;
            }
            else
            {
                model = (from sp in bhx.Products
                             // where sp.ClassifyId == id
                         select sp);//.OrderBy(s => s.Name);
                ViewBag.All = 1;
            }
            if (model.FirstOrDefault() == null) return Content("Sản phẩm sẽ được thêm vào sớm, xin lỗi quý khách vì sự bất tiện này.");
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;
            ViewBag.page = page;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            // string tieuchuanloc = null ;
            //string tieuchuanloc = Request.Form["tieuchuanloc"];
            switch (tieuchuanloc)
            {
                case "giagiam":
                    ViewBag.tieuchuan = "giá giảm dần";
                    model = model.OrderByDescending(sp => sp.Price);
                    break;
                default:
                    model = model.OrderBy(s => s.Price);
                    ViewBag.tieuchuan = "giá tăng dần";
                    break;
            }

            var model1 = (from sp in bhx.Products
                          join nh in bhx.Classifys
                          on sp.ClassifyId equals nh.Id
                          where nh.Id == id
                          select nh.Name).FirstOrDefault();
            ViewBag.Classify = model1;//.SingleOrDefault().Classify.Name; 
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 4;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(model.ToPagedList(pageNumber, pageSize));
        }
        //[HttpGet]




        [HttpGet]
        public ActionResult Details(string id)
        {
            //var model = db.Get(id);
            var model = (from sp in bhx.Products
                         where sp.Id == id
                         select sp).FirstOrDefault();
            if (model == null)
            {
                return View("Error");
            }
            return View(model);
        }
        [HttpGet]   // use to edit, create restaurant
        public ActionResult Create()
        {
            Product sp = new Product();
            ViewBag.BranchName = new SelectList(bhx.Branchs, "Id", "Name", sp.BranchId);
            ViewBag.ClassifyName = new SelectList(bhx.Classifys, "Id", "Name", sp.ClassifyId);
            ViewBag.ProviderName = new SelectList(bhx.Providers, "Id", "Name", sp.ProviderId);
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
            //if (String.IsNullOrEmpty(item.BranchId))
            //{
            //    ModelState.AddModelError(nameof(item.BranchId), "Branch's ID is required");      //thong bao loi khi Name co gia tri null/ rong
            //}
            if (String.IsNullOrEmpty(item.ClassifyId))
            {
                ModelState.AddModelError(nameof(item.ClassifyId), "Classify's ID is required");
                //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(item.ProviderId))
            {
                ModelState.AddModelError(nameof(item.ProviderId), "Provider's ID is required");      //thong bao loi khi Name co gia tri null/ rong
            }

            ViewBag.BranchName = new SelectList(bhx.Branchs, "Id", "Name", item.BranchId);
            ViewBag.ClassifyName = new SelectList(bhx.Classifys, "Id", "Name", item.ClassifyId);
            ViewBag.ProviderName = new SelectList(bhx.Providers, "Id", "Name", item.ProviderId);
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

                string image1 = Guid.NewGuid() + Path.GetExtension(item.Image1.FileName);  //GetExstension
                item.Image1.SaveAs(filename: Server.MapPath("~/Images/" + image1));
                p.Image1 = item.Image1.ToString();
                string image2 = Guid.NewGuid() + Path.GetExtension(item.Image2.FileName);
                item.Image2.SaveAs(filename: Server.MapPath("~/Images/" + image1));
                p.Image2 = item.Image2.ToString();
                string image3 = Guid.NewGuid() + Path.GetExtension(item.Image3.FileName);
                item.Image3.SaveAs(filename: Server.MapPath("~/Images/" + image1));
                p.Image3 = item.Image3.ToString();


                //string image1 = Path.GetFileNameWithoutExtension(item.Image1.FileName);
                //string extension1 = Path.GetExtension(item.Image1.FileName);
                //image1 = image1 + extension1;


                //item.Image1.SaveAs(filename: Server.MapPath("~/Images/" + image1));
                //p.Image1 = item.Image1.ToString();
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
                return View("Error");
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

        //*********************PHƯƠNG: TỪ ĐÂY TRỞ XUỐNG **************************

        //public ActionResult Search() 
        //{
        //    return View("SearchOrSort");
        //}

        [HttpPost]
        public ActionResult Search(/*int? page,*/ string ProductName) ///TÌM KIẾM ĐƯỢC, NHƯNG ****KHÔNG***** PHÂN TRANG ĐƯỢC
        {
            var model = (from sp in bhx.Products
                         where sp.Name.Contains(ProductName)
                         select sp).OrderBy(x => x.Name);
            //if (page == null) page = 1;

            // Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.

            // Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            //int pageSize = 4;

            // Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            //int pageNumber = (page ?? 1);
            ProductName = Request.Form["Product"];
            ViewBag.TenSP = ProductName;
            // model = model.Where(x => x.Name.Contains(ProductName)).OrderBy(x=>x.Name);



            return View("SearchOrSort", model/*.ToPagedList(pageNumber, pageSize)*/);
        }
        [HttpGet]
        public ActionResult SortPriceDown()  //TẠM CHƯA DÙNG
        {
            var model = (from sp in bhx.Products

                         select sp).OrderByDescending(sp => sp.Price);
            return View("SearchOrSort", model.ToPagedList(1, 4));
        }

        [HttpGet]
        public ActionResult SortPriceUp()  //TẠM CHƯA DÙNG
        {
            var model = (from sp in bhx.Products
                         select sp).OrderBy(sp => sp.Price);
            return View("SearchOrSort", model.ToPagedList(1, 4));
        }
        [HttpPost]
        public ActionResult Sort(int? page, string id)   //TẠM CHƯA DÙNG
        {
            var model = (from sp in bhx.Products
                         where sp.ClassifyId == id
                         select sp).OrderBy(s => s.Name);
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            // string tieuchuanloc = null;
            string tieuchuanloc = Request.Form["tieuchuanloc"];
            switch (tieuchuanloc)
            {
                case "giagiam":
                    model = model.OrderByDescending(sp => sp.Price);
                    break;
                case "giatang":
                    model = model.OrderBy(sp => sp.Price);
                    break;
                default:
                    model = model.OrderBy(s => s.Name);
                    break;
            }

            var model1 = (from sp in bhx.Products
                          where sp.ClassifyId == id
                          select sp.Classify.Name).FirstOrDefault();
            ViewBag.Classify = model1;//.SingleOrDefault().Classify.Name; 
            var tinhtien = from sp in bhx.Products
                           where sp.ClassifyId == id
                           select sp.Discount;

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 4;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View("SearchOrSort", model.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThongKe()
        {
            var sanpham = from sp in bhx.Products
                          select sp;
            ViewBag.TotalProduct = sanpham.Sum(x => x.Amount);
            var khachhang = from kh in bhx.Customers
                            select kh;
            ViewBag.TotalCustomer = khachhang.Select(x => x.Id).Count();
            var doanhthu = from tongtien in bhx.Bills
                           select tongtien;
            ViewBag.ToTal = doanhthu.Select(x => x.Total).Sum();

            return View();
        }
        public ActionResult Top20Products()
        {
            var model = (from id in bhx.DetailsOfBills
                         join sp in bhx.Products on id.ProductId equals sp.Id
                         group id by new { id.ProductId, sp.Name, sp.Price, sp.Image1 } into t
                         select new { id = t.Key.ProductId, Name = t.Key.Name, Price = t.Key.Price, Image = t.Key.Image1, AmountSum = t.Sum(sp => sp.Amount) }).OrderByDescending(x => x.AmountSum);
            // select new Product { Id = sp.Id, Name = sp.Name, Price = sp.Price, Discount = sp.Discount, Amount = t.Key };
            //new { Id = id.ProductId, Amount = id.Amount }; //  ViewBag.m= model.GroupBy(x=>x.Id, x => x.Amount).Su)                     
            List<Top20Products> top20 = new List<Top20Products>();
            int i = 1;
            foreach (var sp in model)
            {

                Top20Products sanpham = new Top20Products();
                sanpham.Id = sp.id;
                sanpham.Name = sp.Name;
                sanpham.TotalAmount = sp.AmountSum;
                sanpham.Price = sp.Price;
                sanpham.No = i; //số thứ tự từ 1->20
                top20.Add(sanpham);
                if (i == 20) break;
                i++;
            }
            return View(top20);
        }

        public ActionResult BestSeller(string ClassifyId)
        {
            var model = (from id in bhx.DetailsOfBills
                         join sp in bhx.Products on id.ProductId equals sp.Id
                         where sp.ClassifyId == ClassifyId
                         group id by new { id.ProductId, sp.Name, sp.Price, sp.Image1, sp.Discount } into t
                         select new { id = t.Key.ProductId, Name = t.Key.Name, Price = t.Key.Price, Image = t.Key.Image1, Discount = t.Key.Discount, AmountSum = t.Sum(sp => sp.Amount) }).OrderByDescending(x => x.AmountSum);
            // select new Product { Id = sp.Id, Name = sp.Name, Price = sp.Price, Discount = sp.Discount, Amount = t.Key };
            //new { Id = id.ProductId, Amount = id.Amount }; //  ViewBag.m= model.GroupBy(x=>x.Id, x => x.Amount).Su)                     
            List<Product> top4 = new List<Product>();
            int i = 1;
            foreach (var sp in model)
            {
                Product sanpham = new Product();
                sanpham.Id = sp.id;
                sanpham.Name = sp.Name;
                sanpham.Discount = sp.Discount;
                sanpham.Price = sp.Price;
                sanpham.Image1 = sp.Image; //số thứ tự từ 1->20
                top4.Add(sanpham);
                if (i == 4) break;
                i++;
            }
            return View(top4);
        }
        [HttpPost]
        public ActionResult AddReview(string ProductId)
        {
            string email = null;
            if (Session["Email"] != null)
            {
                email = Session["Email"].ToString();
            }
            var Khid = //from rate in bhx.Ratings
            //            join
                        (from kh in bhx.Customers
                         where kh.Email == email
                         select kh.Id).FirstOrDefault();
            var tontai = bhx.Ratings.Where(x => x.CustomerId == Khid).FirstOrDefault();
            if (tontai == null) //Khách hàng này chưa nhận xét
            {
                Rating rate = new Rating();
                rate.CustomerId = Khid;
                rate.ProductId = ProductId;
                rate.Comments = Request.Form["Message"];
                bhx.Ratings.Add(rate);
                bhx.SaveChanges();
                return RedirectToAction("Details", new { id = ProductId });
            }
            else { return Content("Bạn đã nhận xét sản phẩm rồi"); }
            //return  RedirectToAction("Details",new {id=ProductId });
        }
        public ActionResult ShowReview(string ProductId)
        {
            var model1 = from rate in bhx.Ratings
                         join cus in bhx.Customers
                         on rate.CustomerId equals cus.Id
                         where rate.ProductId == ProductId
                         select new { CustomerName = cus.Name, Review = rate.Comments };
            var ListReview = new List<Rating>();
            foreach (var r in model1)
            {
                Rating rate = new Rating();
                rate.CustomerId = r.CustomerName; //gán tạm Tên khách hàng
                rate.Comments = r.Review;

                ListReview.Add(rate);
            }
            return View(ListReview);
        }
        [HttpPost]
        public ActionResult AddToCart(string id)
        {
            //    // Retrieve the album from the database
            //    //var product = db.Products.Single(p => p.Id == id);            
            //    // Add item to the cart

            //    var cart = SqlCartData.GetCart(this.HttpContext);
            //    //Customer cus = bhx.Customers.SingleOrDefault(
            //    //  c => c.PhoneNumber == cart.ShoppingCartId);
            //    if (!cart.AddToCart(p))
            //    {
            //        TempData["Message"] = "Product has been sold out!";
            //    }

            //    // Go back to the main store page for more shopping
            //    return RedirectToAction("Index");
            ////}
            //List<Cart> cart = new List<Cart>();

            //return Content(dbCart.AddToCart(p, Session["ID"].ToString()).ToString());
            //if (dbCart.AddToCart(p, Session["ID"].ToString()))
            //{
            //    TempData["Message"] = "Product has been sold out!";
            //}
            //cart = dbCart.GetCartItems(Session["ID"].ToString());
            //Session["Cart"] = cart;
            //return RedirectToAction("Index");
            string result = dbCart.AddToCart(id, Session["ID"].ToString());
            Session["CartCounter"] = dbCart.GetCount(Session["ID"].ToString());
            if (result == "1")
            {
                return RedirectToAction("Index");
            }
            if (result == "2")
            {
                TempData["Message"] = "You have reached to the maximum of quanlity!";
                return RedirectToAction("Index");
            }
            if (result == "0")
            {
                TempData["Message"] = "Product has been sold out!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Some problems have been!";
                return RedirectToAction("Index");
            }

        }

        //public ActionResult AddToCart(string id)
        //{

        //    string result = dbCart.AddToCart(id, Session["ID"].ToString());
        //    int counter = dbCart.GetCount(Session["ID"].ToString());
        //    Session["CartCounter"] = counter;
        //    if (result == "1")
        //    {
        //        return Json(new { result = 1 }, JsonRequestBehavior.AllowGet);
        //    }
        //    if (result == "2")
        //    {
        //        // TempData["Message"] = "You have reached to the maximum of quanlity!";
        //        return Json(new { result = 2 }, JsonRequestBehavior.AllowGet);
        //    }
        //    if (result == "0")
        //    {
        //        // TempData["Message"] = "Product has been sold out!";
        //        return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        //TempData["Message"] = "Some problems have been!";
        //        return Json(new { result = 3 }, JsonRequestBehavior.AllowGet);
        //    }

        //}
    }
}