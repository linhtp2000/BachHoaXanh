using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BachHoaXanh.Data.Services;
using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.ModelView;

namespace BachHoaXanh.Web.Controllers
{
    [Authorize(Roles = "XemDonHang")]
    public class OrderController : Controller
    {
        BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        private SqlBillData dbBill = new SqlBillData();
        private SqlDetailsOfBillData dbDetail = new SqlDetailsOfBillData();
        private SqlCartData dbcart = new SqlCartData();


        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Confirm()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Confirm" && bill.CustomerId==Session["ID"].ToString()
                        select bill;
            return View(model);
        }
        //[Authorize(Roles = "XemDonHang")]
        public ActionResult AllOfBills()
        {
            var model = from bill in bhx.Bills
                        where bill.CustomerId == Session["ID"].ToString()
                        select bill;
            return View(model);
        }
        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Prepare()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Prepare" && bill.CustomerId == Session["ID"].ToString()
                        select bill;
            return View(model);
        }
        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Delivering()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Delivering" && bill.CustomerId == Session["ID"].ToString()
                        select bill;
            return View(model);
        }
        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Deliveried()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Deliveried" && bill.CustomerId == Session["ID"].ToString()
                        select bill;
            return View(model);
        }
        [HttpGet]
        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Detail(string id)
        {
            //var model = from bill in bhx.Bills
            //            join detail in bhx.DetailsOfBills
            //             on bill.Id equals detail.BillId
            //            select new { IdBill = detail.BillId, Soluong = detail.Amount };
            var model1 = from billdetail in bhx.DetailsOfBills
                         where billdetail.BillId == id && billdetail.BillId == Session["ID"].ToString()
                         select billdetail;
            //var model = from billdetail in bhx.DetailsOfBills
            //            join picture in bhx.Products
            //            on billdetail.ProductId equals picture.Id
            //            where billdetail.ProductId == id
            //            select new {BranchId = billdetail.ProductId, Id= billdetail.BillId, Name = billdetail.ProductName, Amount =  billdetail.Amount, Image1 = picture.Image1 };
            return View(model1); //model1
        }

        public ActionResult PictureProduct(string id)
        {
            var model = from picture in bhx.Products
                        where picture.Id == id
                        select picture;
            return PartialView("PictureProduct", model);
        }
        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Declined()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Declined" && bill.CustomerId == Session["ID"].ToString()
                        select bill;
            return View(model);
        }

        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Payment_Refund()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Payment" || bill.Status == "Refund" && bill.CustomerId == Session["ID"].ToString()
                        select bill;
            return View(model);
        }
        public ActionResult AgreeBackMoney(string id)
        {
            var model = (from bill in bhx.Bills
                         where bill.Id == id && bill.CustomerId == Session["ID"].ToString()
                         select bill).FirstOrDefault();

            model.Status = "Refund_Roi";
            bhx.SaveChanges();
            return RedirectToAction("Payment_Refund_ChuaXuLy");
        }

        //============PHAN DANH CHO NHAN VIEN, QUAN TRỊ, QUAN LY===================
        //Checkout

    }

}