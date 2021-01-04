using BachHoaXanh.Data.ModelView;
using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Web.Controllers
{
    //[Authorize(Roles = "XemDonHang, XemSP,")]
    public class Cart_CheckoutController : Controller
    {
        // GET: Cart
        //   private List<Cart> carts= new List<Cart>() ;
        private SqlCartData dbcart = new SqlCartData();
        private IBillData dbBill = new SqlBillData();
        BachHoaXanhDbContext db = new BachHoaXanhDbContext();
       
        public ActionResult CartPartial()
        {
            //string s = Session["ID"].ToString();
            if (Session["ID"] == null)
            //if (s==null)
            {
                List<ItemCartView> cart = Session["Cart"] as List<ItemCartView>;
                if (cart == null)
                {
                    ViewBag.CartTotal = 0;
                    ViewBag.CartCounter = 0;
                }
                else
                {
                    ViewBag.CartTotal = dbcart.GetTotalCurrent(cart);
                    ViewBag.CartCounter = dbcart.GetCountCurrent(cart);
                }
                return PartialView();
            }
            string s = Session["ID"].ToString();
            int temp = dbcart.GetCount(s);
            if (temp == 0)
            {
                ViewBag.CartTotal = 0;
                ViewBag.CartCounter = 0;
                return PartialView();
            }
            ViewBag.CartTotal = dbcart.GetTotal(s);
            ViewBag.CartCounter = temp;
            return PartialView();
        }

        [HttpGet]
        public ActionResult Cart()
        {
            List<ItemCartView> cart1 = Session["Cart"] as List<ItemCartView>;
            //Khong co dang nhap tai khoan
            if (Session["ID"] == null)
            {

                ViewBag.CartTotal = dbcart.GetTotalCurrent(cart1);
                ViewBag.CartCounter = dbcart.GetCountCurrent(cart1);
                return View(cart1);
            }
            List<ItemCartView> cart2 = new List<ItemCartView>();
            List<Cart> tempCart = dbcart.GetCartItems(Session["ID"].ToString());
            foreach (Cart item in tempCart)
            {
                ItemCartView itemview = new ItemCartView();
                itemview.ProductId = item.ProductId;
                itemview.ProductName = item.ProductName;
                itemview.Price = item.Price;
                itemview.Total = item.Total;
                itemview.Image = item.Image;
                itemview.Amount = item.Amount;
                itemview.Status = item.Status;
                cart2.Add(itemview);
            }
            ViewBag.CartTotal = dbcart.GetTotal(Session["ID"].ToString());
            ViewBag.CartCounter = dbcart.GetCount(Session["ID"].ToString());
            Session["Cart"] = cart2;
            return View(cart2);
        }

        public ActionResult AddToCart(string id)
        {
            //khong login
            // string s = Session["ID"].ToString();
            string result = "0";
            decimal itemTotal = 0;
            decimal cartTotal = 0;
            if (Session["ID"] == null)
            {
                List<ItemCartView> cart = Session["Cart"] as List<ItemCartView>;
                Session["Cart"] = dbcart.AddToCartCurrent(id, cart, ref result);
                itemTotal = dbcart.GetTotalItemCurrent(id, Session["Cart"] as List<ItemCartView>);
                cartTotal = dbcart.GetTotalCurrent(Session["Cart"] as List<ItemCartView>);
                ViewBag.CartTotal = dbcart.GetTotalCurrent(Session["Cart"] as List<ItemCartView>);
                ViewBag.CartCounter = dbcart.GetCountCurrent(Session["Cart"] as List<ItemCartView>);
                ViewBag.ItemTotal = dbcart.GetTotalItemCurrent(id, Session["Cart"] as List<ItemCartView>);

            }
            else
            {

                result = dbcart.AddToCart(id, Session["ID"].ToString());
                itemTotal = dbcart.GetTotalItem(id, Session["ID"].ToString());
                cartTotal = dbcart.GetTotal(Session["ID"].ToString());
                ViewBag.CartTotal = dbcart.GetTotal(Session["ID"].ToString());
                ViewBag.CartCounter = dbcart.GetCount(Session["ID"].ToString());
                ViewBag.ItemTotal = dbcart.GetTotalItem(id, Session["ID"].ToString());
            }
            CartView results = new CartView
            {
                success = true,
                price = itemTotal,
                total = cartTotal
            };
            if (result == "1")
            {
                return Json(new { success = true, result = results }, JsonRequestBehavior.AllowGet);
            }
            results.success = false;
            return Json(new { success = false, result = results }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult UpdateCart(string id)
        {
            decimal itemTotal = dbcart.GetTotalItem(id, Session["ID"].ToString());
            decimal cartTotal = dbcart.GetTotal(Session["ID"].ToString());
            CartView results = new CartView
            {
                price = itemTotal,
                total = cartTotal
            };
            return Json(new { result = 3 }, JsonRequestBehavior.AllowGet);
        }
        // Remove the item from the cart
        [HttpPost]
        public ActionResult RemoveAmountOfCartItem(string id, int amount)
        {
            // string cid = Session["ID"].ToString();
            decimal total = 0;
            if (Session["ID"] == null)
            {
                List<ItemCartView> cart = Session["Cart"] as List<ItemCartView>;
                Session["Cart"] = dbcart.RemoveAmountOfCartItemCurrent(id, cart, amount);
                total = dbcart.GetTotalCurrent(Session["Cart"] as List<ItemCartView>);
                ViewBag.CartTotal = dbcart.GetTotalCurrent(Session["Cart"] as List<ItemCartView>);
                ViewBag.CartCounter = dbcart.GetCountCurrent(Session["Cart"] as List<ItemCartView>);
                ViewBag.ItemTotal = dbcart.GetTotalItemCurrent(id, Session["Cart"] as List<ItemCartView>);
            }
            else
            {
                string cid = Session["ID"].ToString();
                dbcart.RemoveAmountOfCartItem(id, cid, amount);

                total = dbcart.GetTotalItem(id, cid);
                ViewBag.CartTotal = dbcart.GetTotal(Session["ID"].ToString());
                ViewBag.CartCounter = dbcart.GetCount(Session["ID"].ToString());
                ViewBag.ItemTotal = dbcart.GetTotalItem(id, Session["ID"].ToString());
            }
            //return RedirectToAction("Cart",new {id= cid});
            //CartView results = new CartView
            //{
            //    success = true,
            //    price = itemTotal,
            //    total = cartTotal
            //};
            return Json(new { success = true, Total = total }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RemoveCartItem(string pid)
        {
            if (Session["ID"] == null)
            {
                List<ItemCartView> cart = Session["Cart"] as List<ItemCartView>;
                Session["Cart"] = null;
                Session["Cart"] = dbcart.RemoveCartItemWithoutLogin(pid, cart);
                ViewBag.CartTotal = dbcart.GetTotalCurrent(Session["Cart"] as List<ItemCartView>);
                ViewBag.CartCounter = dbcart.GetCountCurrent(Session["Cart"] as List<ItemCartView>);
                ViewBag.ItemTotal = dbcart.GetTotalItemCurrent(pid, Session["Cart"] as List<ItemCartView>);
            }
            else
            {
                dbcart.RemoveCartItem(pid, Session["ID"].ToString());
                ViewBag.CartTotal = dbcart.GetTotal(Session["ID"].ToString());
                ViewBag.CartCounter = dbcart.GetCount(Session["ID"].ToString());
                ViewBag.ItemTotal = dbcart.GetTotalItem(pid, Session["ID"].ToString());
            }
            return Json(new { success = true });
        }

        //===============ORDER=====================//

        public ActionResult OrderForMem()
        {

            var model = db.Customers.Where(r => r.Id == Session["ID"].ToString()).FirstOrDefault();
            AddressView a = new AddressView();
            a.Name = model.Name;
            a.Address = model.Address;
            a.City = model.City;
            a.Email = model.Email;
            a.Phone = model.PhoneNumber;
            a.State = model.State;

            return View(a);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderForMem(AddressView model)
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
                newbill.CustomerId = Session["ID"].ToString();
                newbill.Datetime = DateTime.Now;
                newbill.Address = model.Address;
                newbill.City = model.City;
                newbill.CustomerName = model.Name;
                //  newbill.Total = cart.GetTotal();

                //    newbill.ServiceCharge
                newbill.Status = "Confirm";
                newbill.Payment = false;
                newbill.State = model.State;
                newbill.Total = dbcart.GetTotalCurrent(Session["Cart"] as List<ItemCartView>);

                newbill.Points = (int)newbill.Total * 3 / 100;
                //Save Order
                db.Bills.Add(newbill);
                db.SaveChanges();
                //Save details of bill
                dbcart.SaveDetailsOfBillCurrent(newbill, Session["Cart"] as List<ItemCartView>);
                Session["Cart"] = null;
                return RedirectToAction("Confirm","Order");
            }
            return View();
        }

        [HttpGet]
        public ActionResult OrderForCur()
        {
            return View();

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
                db.Bills.Add(newbill);
                db.SaveChanges();
                //Save details of bill
                dbcart.SaveDetailsOfBillCurrent(newbill, Session["Cart"] as List<ItemCartView>);
                Session["Cart"] = null;
                return RedirectToAction("Cảm ơn bạn đã đặt hàng tại Bách Hóa xanh");
            }
            return View();
        }





    }
}