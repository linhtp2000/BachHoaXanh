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
        public ActionResult Confirm()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Confirm"
                        select bill;
            return View(model);          
        }
        public ActionResult AllOfBills()
        {
            var model = from bill in bhx.Bills                       
                        select bill;
            return View(model);
        }
        public ActionResult Prepare()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Prepare"
                        select bill;
            return View(model);
        }
        public ActionResult Delivering()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Delivering"
                        select bill;
            return View(model);
        }
        public ActionResult Deliveried()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Deliveried"
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
                        where bill.Status == "Declined"
                        select bill;
            return View(model);
        }

        public ActionResult Payment_Refund()
        {
            //var model = from bill in bhx.Bills
            //            where bill.Status == "Payment" || bill.Status == "Refund"
            //            select bill;

            var model = from bill in bhx.Bills
                        where bill.Status.Contains("Refund")
                        select bill;
            
            return View(model);
        }
        public ActionResult Payment_Refund_ChuaXuLy()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Refund_Chua"
                        select bill;
            return View(model);
        }

        public ActionResult Payment_Refund_DaXuLy()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Refund_Roi"
                        select bill;
            return View(model);
        }
        public ActionResult AgreeBackMoney(string id)
        {
            var model = (from bill in bhx.Bills
                         where bill.Id == id
                         select bill).FirstOrDefault();

            model.Status = "Refund_Roi";
            bhx.SaveChanges();            
            return RedirectToAction("Payment_Refund_ChuaXuLy");
        }
    }
}