using BachHoaXanh.Data.Models;
using BachHoaXanh.Data.ModelView;
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
        private BachHoaXanhDbContext db2 = new BachHoaXanhDbContext();
        private SqlProductData dbProduct = new SqlProductData();

        //public SqlProductData dbProduct = new SqlProductData();
        //public static SqlCartData GetCart(string cid)
        //{
        //    var cart = new SqlCartData();
        //    //  cart.ShoppingCartId = cart.GetCartId(context);
        //    return cart;
        //}

        //public Cart GetCartItem(string productid)
        //{
        //    var cartItem = db.Carts.SingleOrDefault(
        //       c => c.CustomerId == ShoppingCartId
        //       && c.ProductId == productid);
        //    return cartItem;
        //}

        public List<ItemCartViewWithoutLogin> AddToCartWithoutLogin(string pid, List<ItemCartViewWithoutLogin> cart, ref string result)
        {
            Product p = dbProduct.Get(pid);
            if (dbProduct.CheckAmountOfProduct(pid))
            {
                if (cart == null)
                {
                    ItemCartViewWithoutLogin item = new ItemCartViewWithoutLogin();
                    item.ProductId = pid;
                    item.ProductName = p.Name;
                    item.Price = p.Price;
                    item.Total = p.Price *(100 - p.Discount) / 100;
                    item.Image = p.Image1;
                    item.Amount = 1;
                    item.Status = 1;
                    cart = new List<ItemCartViewWithoutLogin>();
                    cart.Add(item);
                    result = "1";
                }
            }
            else
            {
                ItemCartViewWithoutLogin i = cart.Find(x => x.ProductId == pid);
                if (i == null)
                {
                    ItemCartViewWithoutLogin item = new ItemCartViewWithoutLogin();
                    item.ProductId = pid;
                    item.ProductName = p.Name;
                    item.Price = p.Price;
                    item.Total = p.Price * (100 - p.Discount) / 100;
                    item.Image = p.Image1;
                    item.Amount = 1;
                    item.Status = 1;
                    cart.Add(item);
                    result = "1";
                }
                if (dbProduct.GetAmountOfProductCurrent(pid) > i.Amount)
                {
                    cart.Remove(i);
                    i.Amount +=1;
                    i.Total = p.Price * i.Amount * (100 - p.Discount) / 100;
                    i.Status = 1;
                    cart.Add(i);
                    result = "1";
                }
                else
                {
                    result = "2";
                }
            }
            result = "0";
            return cart;//themm thanh cong
        }        
        //public string AddToCart(string pid, string cid)
        //{
                       
        //    var cartItem = db.Carts.SingleOrDefault(c => c.CustomerId == cid && c.ProductId == pid);
        //    Product p = dbProduct.Get(pid);
        //    if (dbProduct.CheckAmountOfProduct(pid))
        //    {
        //        if (cartItem == null)
        //        {
        //            Cart item = new Cart();
        //            item.ProductId = pid;
        //            item.CustomerId = cid;
        //            item.ProductName = p.Name;
        //            item.Price = p.Price;
        //            item.Total = p.Price * (100 - p.Discount) / 100;
        //            item.Image = p.Image1;
        //            item.Amount = 1;
        //            item.Status = 1;
        //            db2.Carts.Add(item);
        //            db2.SaveChanges();
        //            return "1";//themm thanh cong
        //        }
        //        else
        //        {
        //            if (dbProduct.GetAmountOfProductCurrent(pid) > cartItem.Amount)
        //            {
        //                cartItem.Amount += 1;
        //                cartItem.Total = p.Price * cartItem.Amount * (100 - p.Discount) / 100;
        //                db.SaveChanges();
        //                return "1";
        //            }
        //            else
        //            {
        //                return "2";//da dat so luong san pham toi da
        //            }
        //        }
        //    }
        //    return "0";//het hang
        //}
        public string AddToCart(string pid, string cid)
        {

            Cart cartItem = db.Carts.FirstOrDefault(c => c.CustomerId == cid && c.ProductId == pid);
            Product p = dbProduct.Get(pid);
            if (dbProduct.CheckAmountOfProduct(pid))
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
                    if (dbProduct.GetAmountOfProductCurrent(pid) > cartItem.Amount)
                    {
                        db.Carts.Remove(cartItem);
                        db.SaveChanges();
                        //Cart item = new Cart();
                        //item.ProductId = pid;
                        //item.CustomerId = cid;
                        //item.ProductName = cartItem.ProductName;
                        //item.Amount +=1;
                        cartItem.Amount += 1;
                        //item.Price = p.Price;
                        cartItem.Total = p.Price * cartItem.Amount * (100 - p.Discount) / 100;
                        //item.Image = p.Image1;
                        cartItem.Status = 1;

                        db2.Carts.Add(cartItem);
                        db2.SaveChanges();
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

       
        public void RemoveAmountOfCartItem(string pid, string cid, int amount)
        {
            // Get the cart
            var cartItem = db.Carts.FirstOrDefault(cart => cart.CustomerId == cid && cart.ProductId == pid);
            Product p = dbProduct.Get(pid);
            // int itemCount = 0;

            if (cartItem != null)
            {
                if (amount >= 1)
                {
                    db.Carts.Remove(cartItem);
                    db.SaveChanges();
                    Cart item = new Cart();
                    item.ProductId = pid;
                    item.CustomerId = cid;
                    item.ProductName = cartItem.ProductName;
                    item.Amount =amount;
                    item.Price = p.Price;
                    item.Total = p.Price * amount * (100 - p.Discount) / 100;
                    item.Image = p.Image1;
                    item.Status = 1;

                    db2.Carts.Add(item);
                    db2.SaveChanges();

                }
            }
            //  return itemCount;
        }

        public List<ItemCartViewWithoutLogin> RemoveAmountOfCartItemWithoutLogin(string pid, List<ItemCartViewWithoutLogin> cart, int amount)
        {
            ItemCartViewWithoutLogin item = cart.Find(x => x.ProductId == pid);
            Product p = dbProduct.Get(pid);
            if (item.Amount >= 2)
            { 
                    cart.Remove(item);
                    item.Amount -= 1; 
                    item.Total = p.Price * item.Amount * (100 - p.Discount) / 100;
                    item.Status = 1;
                    cart.Add(item);
                //result = "1";//xoa thanh cong
            }
           // result = "0";   //error
            return cart;
        }
        public void RemoveCartItem(string pid, string cid)
        {
            var cartItem = db.Carts.FirstOrDefault(cart => cart.CustomerId == cid && cart.ProductId == pid);
            db.Carts.Remove(cartItem);
            db.SaveChanges();
        }
        public List<ItemCartViewWithoutLogin> RemoveCartItemWithoutLogin(string pid, List<ItemCartViewWithoutLogin> cart)
        {
            var item = cart.Find(c => c.ProductId == pid);
            cart.Remove(item);
;          return cart;
        }
        public void EmptyCart(string cid)
        {
            var cartItems = db.Carts.Where(
                cart => cart.CustomerId == cid);

            foreach (var cartItem in cartItems)
            {
                db2.Carts.Remove(cartItem);
            }
            // Save changes
            db2.SaveChanges();
        }
        public List<Cart> GetCartItems(string cid)
        {
            //List<Cart> cartitems = new List<Cart>();
            var carts = db.Carts.Where(cart => cart.CustomerId == cid).ToList();
            if (carts != null)
            {
                foreach (var item in carts)
                {
                    db.Carts.Remove(item);
                    db.SaveChanges();                 
                    if (dbProduct.CheckAmountOfProduct(item.ProductId))
                    {
                        item.Status = 1;
                        int count = dbProduct.GetAmountOfProductCurrent(item.ProductId);
                        if (item.Amount > count)
                        {
                            item.Amount = count;
                        }                     
                    }
                    else
                    {
                        item.Status = 0;
                    }
                    db2.Carts.Add(item);
                    db2.SaveChanges();
                }
               // cartitems = carts;
              //  db.SaveChanges();
            }
            return db.Carts.Where(cart => cart.CustomerId == cid).ToList();
        }
        public int GetCount(string cid)
        {           
            // Get the count of each item in the cart and sum them up
            int? amount = (from cartItems in db.Carts
                          where cartItems.CustomerId == cid
                          select (int?) cartItems.Amount).Sum();

            return amount ?? 0;
        }
        public decimal GetTotalItem(string pid, string cid)
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CustomerId == cid && cartItems.ProductId == pid
                              select (decimal?)cartItems.Total).Sum();
          
            return total ?? decimal.Zero;
        }
        public decimal GetTotalItemWithoutLogin(string pid, List<ItemCartViewWithoutLogin> cart)
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            if(cart==null)
            {
                return 0;
            }
            ItemCartViewWithoutLogin item= cart.Find(x=> x.ProductId==pid);
            if(item==null)
            {
                return 0;
            }
            return item.Total;
        }
        public decimal GetTotal(string cid)
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CustomerId == cid
                              select (decimal?)cartItems.Total).Sum();

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
          //  EmptyCart();

        }

        public int GetCountWithoutLogin(List<ItemCartViewWithoutLogin> list)
        {
            if(list==null)
            { return 0; }
            int count = 0;
            foreach(ItemCartViewWithoutLogin item in list)
            {
                count += item.Amount;
            }
            return count;
        }
        public decimal GetTotalWithoutLogin(List<ItemCartViewWithoutLogin> list)
        {
            if (list == null)
            { return 0; }
            decimal total = 0;
            foreach (ItemCartViewWithoutLogin item in list)
            {
                total += item.Total;
            }
            return total; ;
        }
        // We're using HttpContextBase to allow access to cookies.
        //public string GetCartId(HttpContextBase context)
        //{
        //    if (context.Session[CartSessionKey] == null)
        //    {
        //        if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
        //        {
        //            context.Session[CartSessionKey] = context.User.Identity.Name;
        //        }
        //        else
        //        {
        //            // Generate a new random GUID using System.Guid class
        //            Guid tempCartId = Guid.NewGuid();
        //            // Send tempCartId back to client as a cookie
        //            context.Session[CartSessionKey] = tempCartId.ToString();
        //        }
        //    }
        //    return context.Session[CartSessionKey].ToString();
        //}
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        //public void MigrateCart(string userName)
        //{
        //    var shoppingCart = db.Carts.Where(
        //        c => c.CustomerId == ShoppingCartId);

        //    foreach (Cart item in shoppingCart)
        //    {
        //        item.CustomerId = userName;
        //    }
        //    db.SaveChanges();
        //}
    }

}
