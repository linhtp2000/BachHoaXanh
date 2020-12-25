using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.ModelView;
using BachHoaXanh.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Web.Controllers
{
    public class CheckoutController : Controller
    {
        //  const string PromoCode = "FREE";
        private ICartData dbCart;
        private IBillData dbBill;
        BachHoaXanhDbContext db = new BachHoaXanhDbContext();
        // GET: Checkout
        //public ActionResult AddressAndPayment()
        //{
        //    var carts = SqlCartData.GetCart(this.HttpContext);

        //    return View();
        //}
        [HttpGet]
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddressAndPayment(AddressForOrder modeladdress)
        {
            //   TryUpdateModel(order);
            if (String.IsNullOrEmpty(modeladdress.Address))
            {
                ModelState.AddModelError(nameof(modeladdress.Address), "Address is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(modeladdress.City))
            {
                ModelState.AddModelError(nameof(modeladdress.City), "City is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(modeladdress.Name))
            {
                ModelState.AddModelError(nameof(modeladdress.Name), "Full Name is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(modeladdress.State))
            {
                ModelState.AddModelError(nameof(modeladdress.State), "State is required");      //thong bao loi khi Name co gia tri null/ rong
            }
            if (String.IsNullOrEmpty(modeladdress.Phone))
            {
                ModelState.AddModelError(nameof(modeladdress.Phone), "Your Phone is required");      //thong bao loi khi Name co gia tri null/ rong
            }

            if (ModelState.IsValid)
            {
                Bill newbill = new Bill();
              //  var cart = SqlCartData.GetCart(this.HttpContext);
                newbill.CustomerId = User.Identity.Name;
                newbill.Datetime = DateTime.Now;
                newbill.Address = modeladdress.Address;
                newbill.City = modeladdress.City;
                newbill.CustomerName = modeladdress.Name;
                newbill.Id = (dbBill.GetBillId() + 1).ToString();
              //  newbill.Total = cart.GetTotal();
                newbill.Points = (int)(newbill.Total * 3 / 100);
                //    newbill.ServiceCharge
                newbill.Status = "DatHang";
                //Save Order
                dbBill.Add(newbill);

                //Process the order
                //cart.SaveDetailsOfBill(newbill);

                return RedirectToAction("Complete", new { id = newbill.Id });
            }

            return RedirectToAction("~/Cart_Checkout/Cart");
        }
        //
        // GET: /Checkout/Complete
        public ActionResult Complete(string id)
        {
            // Validate customer owns this order
            bool isValid = db.Bills.Any(b => b.Id == id && b.CustomerId == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}