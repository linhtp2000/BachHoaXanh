using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Data.Services
{
    public class SqlCartData : ICartData
    {
        private BachHoaXanhDbContext db = new BachHoaXanhDbContext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CustomerId";
        public SqlProductData dbProduct = new SqlProductData();
        public static SqlCartData GetCart(string cid)
        {
            var cart = new SqlCartData();
          //  cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
     
        public Cart GetCartItem(string productid)
        {
            var cartItem = db.Carts.SingleOrDefault(
               c => c.CustomerId == ShoppingCartId
               && c.ProductId == productid);
            return cartItem;
        }
        public string AddToCart(string pid, string cid)
        {
            //// Get the matching cart and product instances
            //var cartItem = db.Carts.SingleOrDefault(
            //    c => c.CustomerId == ID
            //    && c.ProductId == product.Id);
            //if (dbProduct.CheckAmountOfProduct(product.Id))
            //{
            //    try
            //    {
            //        if (cartItem == null)
            //        {
            //            // Create a new cart item if no cart item exists
            //            Cart item = new Cart
            //            {
            //                ProductId = product.Id,
            //                CustomerId = ID,
            //                ProductName = product.Name,
            //                Price = product.Price,
            //                Total = product.Price * product.Amount * (100 - product.Discount) / 100,
            //                Image = product.Image1,
            //                Amount = 1,
            //                Status = 1
            //            };
            //            db.Carts.Add(item);
            //        }
            //        else
            //        {
            //            // If the item does exist in the cart, 
            //            // then add one to the quantity
            //            cartItem.Amount++;
            //            cartItem.Total = product.Price * cartItem.Amount * (100 - product.Discount) / 100;
            //        }
            //        // Save changes
            //       return db.SaveChanges();

            //    }
            //    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            //    {
            //        Exception raise = dbEx;
            //        foreach (var validationErrors in dbEx.EntityValidationErrors)
            //        {
            //            foreach (var validationError in validationErrors.ValidationErrors)
            //            {
            //                string message = string.Format("{0}:{1}",
            //                    validationErrors.Entry.Entity.ToString(),
            //                    validationError.ErrorMessage);
            //                // raise a new exception nesting  
            //                // the current instance as InnerException  
            //                raise = new InvalidOperationException(message, raise);
            //            }
            //        }
            //        throw raise;
            //    }
            //}
            //return 0;

            //string temp = "0";

            var cartItem = db.Carts.FirstOrDefault(c => c.CustomerId == cid && c.ProductId == pid);
            Product p = dbProduct.Get(pid);
                if (p.Amount > 0)
                {
                    if (cartItem == null)
                    {
                        Cart item = new Cart();
                        item.ProductId = pid;
                        item.CustomerId = cid;
                        item.ProductName = p.Name;
                        item.Price = p.Price;
                        item.Total = p.Price * (100 - p.Discount) / 100;
                        item.Image = p.Image1;
                        item.Amount = 1;
                        item.Status = 1;
                        db.Carts.Add(item);
                        db.SaveChanges();
                        return "1";//themm thanh cong
                    }
                    else
                    {
                        if (p.Amount > cartItem.Amount)
                        {
                            cartItem.Amount += 1;
                            cartItem.Total = p.Price * cartItem.Amount * (100 - p.Discount) / 100;
                            db.SaveChanges();
                            return "1";
                        }
                        else
                        {
                            return "2";//da dat so luong san pham toi da
                        }
                    }
                }
                 return "0";//het hang
        }
        public void RemoveAmountOfCartItem(string pid,string cid)
        {
            // Get the cart
            var cartItem = db.Carts.Single(cart => cart.CustomerId == cid && cart.ProductId == pid);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Amount > 1)
                {
                    cartItem.Amount-=1;
                    cartItem.Total = cartItem.Price * cartItem.Amount * (100 - cartItem.Product.Discount) / 100;
                    itemCount = cartItem.Amount;
                }
                //else
                //{
                //    db.Carts.Remove(cartItem);
                //}
                // Save changes
                db.SaveChanges();
            }
          //  return itemCount;
        }
        public void RemoveCartItem(string productid)
        {
            var cartItem = db.Carts.Single(
                cart => cart.CustomerId == ShoppingCartId
                && cart.ProductId == productid);

            db.Carts.Remove(cartItem);
            db.SaveChanges();
        }
        public void EmptyCart()
        {
            var cartItems = db.Carts.Where(
                cart => cart.CustomerId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                db.Carts.Remove(cartItem);
            }
            // Save changes
            db.SaveChanges();
        }
        public List<Cart> GetCartItems(string cid)
        {
            List<Cart> cartitems = new List<Cart>();
            var carts = db.Carts.Where(cart => cart.CustomerId == cid).ToList();
            foreach (Cart item in carts)
            {
                if (dbProduct.CheckAmountOfProduct(item.ProductId))
                {
                    item.Status = 1;
                    int count = dbProduct.GetAmountOfProductCurrent(item.ProductId);
                    if (item.Amount>count)
                    {
                        item.Amount = count;
                    }
                }
                else
                {
                    item.Status = 0;
                }
                cartitems.Add(item);
            }
            db.SaveChanges();
            return cartitems;
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            var amount = (from cartItems in db.Carts
                          where cartItems.CustomerId == ShoppingCartId
                          select cartItems);
            if (amount == null)
            {
                return 0;
            }
            return amount.Count();
        }
        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CustomerId == ShoppingCartId
                              select (int?)cartItems.Total).Sum();

            return total ?? decimal.Zero;
        }
        public void SaveDetailsOfBill(Bill bill, string ID)
        {
            DetailsOfBill detail = new DetailsOfBill();
            var cartItems = GetCartItems(ID);
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new DetailsOfBill()
                {
                    Amount = item.Amount,
                    BillId = bill.Id,
                    ProductId = item.ProductId,
                    Product = item.Product,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Image = item.Image,
                    Total = item.Amount * item.Price * (100 - item.Product.Discount) / 100
                };
                db.DetailsOfBills.Add(orderDetail);

            }
            // Save the order
            db.SaveChanges();
            // Empty the shopping cart
            EmptyCart();

        }
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = db.Carts.Where(
                c => c.CustomerId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CustomerId = userName;
            }
            db.SaveChanges();
        }
    }

}
