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
    public class Cart_CheckoutController : Controller
    {
        // GET: Cart
        //   private List<Cart> carts= new List<Cart>() ;
        private SqlCartData dbcart = new SqlCartData();
        private IBillData dbBill = new SqlBillData();
        BachHoaXanhDbContext db = new BachHoaXanhDbContext();
        //public CartController()
        //{
        //    db = new BachHoaXanhDbContext();
        //}
        //[HttpGet]
        //public ActionResult Index()
        //{

        //    //IEnumerable<Cart> carts = (from p in db.Carts
        //    //                           //join c in db.Customers on p.CustomerId equals c.Id
        //    //                           select 
        //    //                           {
        //    //                               ProductId = p.ProductId,
        //    //                               ProductName = p.ProductName,
        //    //                               Image = p.Image,
        //    //                               Price = p.Price
        //    //                           }).ToList();
        //    //Session["CartCounter"] = carts.Count;
        //    Session["Cart"] = carts;
        //    //var c = dbcart.GetAll();
        //    return View(carts);
        //}
        //[HttpPost]
        //public ActionResult Buy(string id)
        //{
        //    Product p = new Product();

        //    //If cart is empty
        //    if (Session["CartCounter"] == null)
        //    {
        //        carts = new List<Cart>();
        //        carts.Add(new Cart {Product=p, ProductId = p.Id,ProductName=p.Name, Amount = 1, Price=p.Price, Image=p.Image1,Total=p.Price*p.Amount});               
        //    }
        //    else
        //    {
        //        carts = Session["Cart"]as List<Cart>;
        //        int index = isExist(id);
        //        //This product has existed in cart
        //        if (index != -1)
        //        {
        //            carts[index].Amount++;
        //            carts[index].Total = carts[index].Price * carts[index].Amount;
        //        }
        //        else
        //        {
        //            carts.Add(new Cart { Product = p, ProductId = p.Id, ProductName = p.Name, Amount = 1, Price = p.Price, Image = p.Image1, Total = p.Price * p.Amount });
        //        }

        //    }
        //    Session["CartCounter"] = carts.Count;
        //    Session["Cart"] = carts;
        //    //Save item in cart to sql
        //    //foreach (Cart cart in carts)
        //    //{
        //    //    dbcart.Add(cart);
        //    //}

        //    return RedirectToAction("~/Product/Index");
        //}

        //public ActionResult Remove(string id)
        //{
        //    carts = (List<Cart>)Session["cart"];
        //    int index = isExist(id);
        //    carts.RemoveAt(index);
        //    Session["cart"] = carts;

        //    //foreach (Cart cart in carts)
        //    //{
        //    //    dbcart.Delete(pid, cid);
        //    //}
        //    return RedirectToAction("Index");
        //}

        //private int isExist(string id)
        //{
        //    List<Cart> cart = (List<Cart>)Session["cart"];
        //    for (int i = 0; i < cart.Count; i++)
        //        if (cart[i].ProductId==id)
        //            return i;
        //    return -1;
        //}

        public ActionResult CartPartial()
        {
            //string s = Session["ID"].ToString();
            if(Session["ID"]==null)
            //if (s==null)
            {
                List<ItemCartViewWithoutLogin> cart= Session["Cart"] as List < ItemCartViewWithoutLogin>;
                if (cart == null)
                {
                    ViewBag.CartTotal = 0;
                    ViewBag.CartCounter = 0;
                }
                else
                {
                    ViewBag.CartTotal = dbcart.GetTotalWithoutLogin(cart);
                    ViewBag.CartCounter = dbcart.GetCountWithoutLogin(cart);
                }
                return PartialView();
            }
            string s = Session["ID"].ToString();
            int temp= dbcart.GetCount(s);
            if (temp==0)
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
            List<ItemCartViewWithoutLogin> cart1 = Session["Cart"] as List<ItemCartViewWithoutLogin>;
            //Khong co dang nhap tai khoan
            if(Session["ID"] == null)
            {
                //if(cart1==null)
                //{
                //    return PartialView("CartParttial");
                //}/
                //else
                // {
                ViewBag.CartTotal = dbcart.GetTotalWithoutLogin(cart1);
                ViewBag.CartCounter = dbcart.GetCountWithoutLogin(cart1);
                return View(cart1);
               //}
            }
            List<ItemCartViewWithoutLogin> cart2 =new List<ItemCartViewWithoutLogin>();
            List<Cart> tempCart = dbcart.GetCartItems(Session["ID"].ToString());
            foreach(Cart item in tempCart)
            {
                ItemCartViewWithoutLogin itemview = new ItemCartViewWithoutLogin();
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
                List<ItemCartViewWithoutLogin> cart = Session["Cart"] as List<ItemCartViewWithoutLogin>;
                Session["Cart"] = dbcart.AddToCartWithoutLogin(id, cart, ref result);
                itemTotal = dbcart.GetTotalItemWithoutLogin(id,Session["Cart"] as List<ItemCartViewWithoutLogin>);
                cartTotal= dbcart.GetTotalWithoutLogin(Session["Cart"] as List<ItemCartViewWithoutLogin>);
                ViewBag.CartTotal = dbcart.GetTotalWithoutLogin(Session["Cart"] as List<ItemCartViewWithoutLogin>);
                ViewBag.CartCounter = dbcart.GetCountWithoutLogin(Session["Cart"] as List<ItemCartViewWithoutLogin>);
                ViewBag.ItemTotal = dbcart.GetTotalItemWithoutLogin(id,Session["Cart"] as List<ItemCartViewWithoutLogin>);

            }
            else 
            { 

                result = dbcart.AddToCart(id, Session["ID"].ToString());          
                itemTotal = dbcart.GetTotalItem(id, Session["ID"].ToString());
                cartTotal = dbcart.GetTotal(Session["ID"].ToString());
                ViewBag.CartTotal = dbcart.GetTotal(Session["ID"] .ToString());
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
                List<ItemCartViewWithoutLogin> cart = Session["Cart"] as List<ItemCartViewWithoutLogin>;
                Session["Cart"] = dbcart.RemoveAmountOfCartItemWithoutLogin(id, cart, amount);
                total = dbcart.GetTotalWithoutLogin(Session["Cart"] as List<ItemCartViewWithoutLogin>);
                ViewBag.CartTotal = dbcart.GetTotalWithoutLogin(Session["Cart"] as List<ItemCartViewWithoutLogin>);
                ViewBag.CartCounter = dbcart.GetCountWithoutLogin(Session["Cart"] as List<ItemCartViewWithoutLogin>);
                ViewBag.ItemTotal = dbcart.GetTotalItemWithoutLogin(id, Session["Cart"] as List<ItemCartViewWithoutLogin>);
            }
            else
            {
                string cid = Session["ID"].ToString();
                dbcart.RemoveAmountOfCartItem(id, cid,amount);
              
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
                List<ItemCartViewWithoutLogin> cart = Session["Cart"] as List<ItemCartViewWithoutLogin>;
                Session["Cart"] = dbcart.RemoveCartItemWithoutLogin(pid, cart);
                ViewBag.CartTotal = dbcart.GetTotalWithoutLogin(Session["Cart"] as List<ItemCartViewWithoutLogin>);
                ViewBag.CartCounter = dbcart.GetCountWithoutLogin(Session["Cart"] as List<ItemCartViewWithoutLogin>);
                ViewBag.ItemTotal = dbcart.GetTotalItemWithoutLogin(pid, Session["Cart"] as List<ItemCartViewWithoutLogin>);
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

        //Checkout
        [HttpGet]
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                // var cart = SqlCartData.GetCart(this.HttpContext);
                newbill.CustomerId = User.Identity.Name;
                newbill.Datetime = DateTime.Now;
                newbill.Address = modeladdress.Address;
                newbill.City = modeladdress.City;
                newbill.CustomerName = modeladdress.Name;
                newbill.Id = (dbBill.GetBillId() + 1).ToString();
                //  newbill.Total = cart.GetTotal();
                newbill.Points = (int)(newbill.Total * 3 / 100);
                //    newbill.ServiceCharge
                newbill.Status = "Confirm";
                //Save Order
                dbBill.Add(newbill);

                //Process the order
                //cart.SaveDetailsOfBill(newbill);
                return Content("/Order/Confirm");
            }

            return View("AddressAndPayment");
        }
        //
        // GET: /Checkout/Complete


    }
}