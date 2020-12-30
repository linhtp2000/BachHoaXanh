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
    //[Authorize(Roles = "QuanTri, QLDonHang")]
    public class OrderController : Controller
    {
        BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        private SqlBillData  dbBill= new SqlBillData();
        private SqlDetailsOfBillData dbDetail = new SqlDetailsOfBillData();
        private SqlCartData dbcart = new SqlCartData();


        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Confirm()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Confirm"
                        select bill;
            return View(model);
        }
        //[Authorize(Roles = "XemDonHang")]
        public ActionResult AllOfBills()
        {
            var model = from bill in bhx.Bills
                        select bill;
            return View(model);
        }
        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Prepare()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Prepare"
                        select bill;
            return View(model);
        }
        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Delivering()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Delivering"
                        select bill;
            return View(model);
        }
        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Deliveried()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Deliveried"
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
                         where billdetail.BillId == id
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
                        where bill.Status == "Declined"
                        select bill;
            return View(model);
        }

        //[Authorize(Roles = "XemDonHang")]
        public ActionResult Payment_Refund()
        {
            var model = from bill in bhx.Bills
                        where bill.Status == "Payment" || bill.Status == "Refund"
                        select bill;
            return View(model);
        }


        //============PHAN DANH CHO NHAN VIEN, QUAN TRỊ, QUAN LY===================
        //Checkout
        [HttpGet]
        public ActionResult OrderForMember()
        {
            if (Session["ID"] == null)
            {
                return View();
            }
            return View();

        }

        [HttpGet]
        public ActionResult OrderForCur()
        {
            return View("GoToOrder");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderForCur(AddressView model)
        {
            //   TryUpdateModel(order);
            if (String.IsNullOrEmpty(model.Address))
            {
                ModelState.AddModelError(nameof(model.Address), "Address is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(model.City))
            {
                ModelState.AddModelError(nameof(model.City), "City is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(model.Name))
            {
                ModelState.AddModelError(nameof(model.Name), "Full Name is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(model.State))
            {
                ModelState.AddModelError(nameof(model.State), "State is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Email is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(model.Phone))
            {
                ModelState.AddModelError(nameof(model.Phone), "Your Phone is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            //model.Point = 0;
            //model.OrderDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                Bill newbill = new Bill();
                newbill.Id = dbBill.GetBillId().ToString();
                newbill.CustomerId = null;
                newbill.Datetime = DateTime.Now;
                newbill.Address = model.Address;
                newbill.City = model.City;
                newbill.CustomerName = model.Name;
                //  newbill.Total = cart.GetTotal();
                newbill.Points = 0;
                //    newbill.ServiceCharge
                newbill.Status = "Confirm";
                newbill.Payment = false;
                newbill.State = model.State;
                newbill.Total = dbcart.GetTotalCurrent(Session["Cart"] as List<ItemCartView>);
                //Save Order
                bhx.Bills.Add(newbill);
                bhx.SaveChanges();
                //Save details of bill
                dbcart.SaveDetailsOfBillCurrent(newbill, Session["Cart"] as List<ItemCartView>);
                Session["Cart"] = null;
                return Content("/Order/Confirm");
            }
            return RedirectToAction("Confirm");
        }


    }
}