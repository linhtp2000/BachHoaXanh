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
        private readonly BachHoaXanhDbContext db = new BachHoaXanhDbContext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CustomerId";
        public SqlProductData dbProduct = new SqlProductData();
        public static SqlCartData GetCart(HttpContextBase context)
        {
            var cart = new SqlCartData();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        public static SqlCartData GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public Cart GetCartItem(string productid)
        {
            var cartItem = db.Carts.SingleOrDefault(
               c => c.CustomerId == ShoppingCartId
               && c.ProductId == productid);
            return cartItem;
        }
        public bool AddToCart(Product product)
        {
            // Get the matching cart and product instances
            var cartItem = db.Carts.SingleOrDefault(
                c => c.CustomerId == ShoppingCartId
                && c.ProductId == product.Id);
            if (dbProduct.CheckAmountOfProduct(product.Id))
            {
                if (cartItem == null)
                {
                    // Create a new cart item if no cart item exists
                    cartItem = new Cart
                    {
                        Product = product,
                        ProductId = product.Id,
                        CustomerId = ShoppingCartId,
                        ProductName = product.Name,
                        Price = product.Price,
                        Total = product.Price * product.Amount * (100 - product.Discount) / 100,
                        Image = product.Image1,
                        Amount = 1,
                        Status = 1
                    };
                    db.Carts.Add(cartItem);
                }
                else
                {
                    // If the item does exist in the cart, 
                    // then add one to the quantity
                    cartItem.Amount++;
                    cartItem.Total = product.Price * cartItem.Amount * (100 - product.Discount) / 100;
                }
                // Save changes
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public int RemoveAmountOfCartItem(string productid)
        {
            // Get the cart
            var cartItem = db.Carts.Single(
                cart => cart.CustomerId == ShoppingCartId
                && cart.ProductId == productid);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Amount > 1)
                {
                    cartItem.Amount--;
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
            return itemCount;
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
        public List<Cart> GetCartItems()
        {
            var carts = db.Carts.Where(
                cart => cart.CustomerId == ShoppingCartId).ToList();
            foreach (Cart item in carts)
            {
                if (dbProduct.CheckAmountOfProduct(item.ProductId))
                {
                    item.Status = 1;
                }
                else
                {
                    item.Status = 0;
                }
            }
            db.SaveChanges();
            return db.Carts.Where(
                cart => cart.CustomerId == ShoppingCartId).ToList();
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
        public void SaveDetailsOfBill(Bill bill)
        {
            DetailsOfBill detail = new DetailsOfBill();
            var cartItems = GetCartItems();
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
