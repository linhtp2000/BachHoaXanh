using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Web.Controllers
{
    //[Authorize(Roles = "QuanTri, QLDonHang, QLSanPham")]
    public class ManageOrdersController : Controller
    {
        // GET: ManageOrders
        BachHoaXanhDbContext bhx = new BachHoaXanhDbContext();
        private SqlBillData dbBill = new SqlBillData();
        private SqlDetailsOfBillData dbDetail = new SqlDetailsOfBillData();
        private SqlCartData dbcart = new SqlCartData();


        //[Authorize(Roles = "XemDonHang")]
        public ActionResult ChoThanhToan()
        {
            var model = from bill in bhx.Bills
                        where bill.Payment==false
                        select bill;
            return View(model);
        }
        //[Authorize(Roles = "XemDonHang")]
        public ActionResult DaThanhToanVaChuaGiao()
        {
            var model = from bill in bhx.Bills
                        where bill.Payment == true && bill.Status != "Deliveried"
                        select bill;
            return View(model);
        }
     
        public ActionResult DaThanhGiaoVaThanhToan()
        {
            var model = from bill in bhx.Bills
                        where bill.Payment == true && bill.Status == "Deliveried"
                        select bill;
            return View(model);
        }
        //[Authorize(Roles = "XemDonHang")]
        [HttpGet]
        public ActionResult Details(string id)
        {
           var model = bhx.Bills.FirstOrDefault(r => r.Id == id);          
            return View(model);
        }

        [HttpPost]
        public ActionResult Details(Bill bill)
        {
            Bill model = bhx.Bills.Single(r => r.Id == bill.Id);
            model.Payment = bill.Payment;
            model.State = bill.State;
            bhx.SaveChanges();
            return View(model);
        }
      
    }
}