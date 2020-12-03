using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BachHoaXanh.Data.Services;
using BachHoaXanh.Data.Models;
namespace BachHoaXanh.Web.Controllers
{
    public class OrderController : Controller
    {
        BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        private readonly IBillData dbBill;
        private readonly IDetailsOfBillData dbDetail;
        public OrderController(IDetailsOfBillData db)
        {
            this.dbDetail = db;
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Deliveried()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Dagiao"
                       select bill;
            return View(model);
        }
        [HttpGet]
        public ActionResult Detail (string id)
        {
            //var model = from bill in bhx.Bills
            //            join detail in bhx.DetailsOfBills
            //             on bill.Id equals detail.BillId
            //            select new { IdBill = detail.BillId, Soluong = detail.Amount };
            var model1 = from billdetail in bhx.DetailsOfBills
                         where billdetail.BillId == id
                         select billdetail;
            //var model = from billdetail in bhx.DetailsOfBills
            //            join picture in bhx.Products
            //            on billdetail.ProductId equals picture.Id
            //            where billdetail.ProductId == id
            //            select new {BranchId = billdetail.ProductId, Id= billdetail.BillId, Name = billdetail.ProductName, Amount =  billdetail.Amount, Image1 = picture.Image1 };
            return View (model1); //model1
        }

        public ActionResult PictureProduct(string id)
        {
            var model = from picture in bhx.Products
                        where picture.Id == id
                        select picture;           
            return PartialView("PictureProduct", model);
        }
        public ActionResult Declined ()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Dahuy"
                        select bill;
            return View(model);
        }

        public ActionResult BackMoney()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Hoantien"
                        select bill;
            return View(model);
        }
    }
}