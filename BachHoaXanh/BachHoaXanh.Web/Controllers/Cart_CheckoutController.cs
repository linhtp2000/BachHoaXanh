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
        private readonly ICartData dbcart;
        private IBillData dbBill;
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

        public ActionResult Cart()
        {
            ////var carts = dbcart.GetCart(this.HttpContext);

            // Set up ViewModel
            //var viewModel = new ShoppingCartView()
            //{
            //    CartItems = carts.GetCartItems(),
            //    CartTotal = carts.GetTotal()
            //};
            // Return the view

          var  carts= SqlCartData.GetCart(this.HttpContext);            
         return View(carts.GetCartItems());
        }
        [HttpPost]
        public ActionResult AddToCart(string id)
        {
            // Retrieve the album from the database
            var product = db.Products.Single(p => p.Id == id);

            // Add item to the cart
            var cart = SqlCartData.GetCart(this.HttpContext);
            cart.AddToCart(product);

            // Go back to the main store page for more shopping
            return RedirectToAction("~/Product/Index");
        }

        // Remove the item from the cart
        [HttpPost]
        public ActionResult RemoveAmountOfCartItem(string id)
        {           
            var cart = SqlCartData.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string productname = cart.GetCartItem(id).ProductName;

            // Remove amount of item
            int itemCount = cart.RemoveAmountOfCartItem(id);

            // Display the confirmation message
            //var results = new ShoppingCartRemoveView
            //{
            //    Message = Server.HtmlEncode(productname) +
            //        " has been removed from your shopping cart.",
            //    CartTotal = cart.GetTotal(),
            //    CartCount = cart.GetCount(),
            //    ItemCount = itemCount,
            //    DeleteId = id
            //};
            return RedirectToAction("Cart");
        }
        [HttpPost]
        public ActionResult RemoveCartItem(string id)
        {
            var cart = SqlCartData.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string productname = dbcart.GetCartItem(id).ProductName;

            // Remove from cart
           dbcart.RemoveAmountOfCartItem(id);

            return RedirectToAction("Cart");
        }
        ////
        //// GET: /ShoppingCart/CartSummary
        //[ChildActionOnly]
        //public ActionResult CartSummary()
        //{
        //    var cart = dbcart.GetCart(this.HttpContext);

        //    ViewData["CartCount"] = cart.GetCount();
        //    return PartialView("CartSummary");
        //}

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
                var cart = SqlCartData.GetCart(this.HttpContext);
                newbill.CustomerId = User.Identity.Name;
                newbill.Datetime = DateTime.Now;
                newbill.Address = modeladdress.Address;
                newbill.City = modeladdress.City;
                newbill.CustomerName = modeladdress.Name;
                newbill.Id = (dbBill.GetBillId() + 1).ToString();
                newbill.Total = cart.GetTotal();
                newbill.Points = (int)(newbill.Total * 3 / 100);
                //    newbill.ServiceCharge
                newbill.Status = "DatHang";
                //Save Order
                dbBill.Add(newbill);

                //Process the order
                cart.SaveDetailsOfBill(newbill);
                return Content("/Order/Confirm");
            }

            return View("AddressAndPayment");
        }
        //
        // GET: /Checkout/Complete
       

    }
}