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
        private SqlBillData dbBill = new SqlBillData();
 
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
                        
                        cartItem.Amount += 1;               
                        cartItem.Total = p.Price * cartItem.Amount * (100 - p.Discount) / 100;
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
 
        public void RemoveCartItem(string pid, string cid)
        {
            var cartItem = db.Carts.FirstOrDefault(cart => cart.CustomerId == cid && cart.ProductId == pid);
            db.Carts.Remove(cartItem);
            db.SaveChanges();
        }
        public List<ItemCartView> RemoveCartItemWithoutLogin(string pid, List<ItemCartView> cart)
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
            decimal? total = (from cartItems in db.Carts
                              where cartItems.CustomerId == cid && cartItems.ProductId == pid
                              select (decimal?)cartItems.Total).Sum();
          
            return total ?? decimal.Zero;
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
        public void SaveDetailsOfBill(Bill bill, string cid)
        {
            DetailsOfBill detail = new DetailsOfBill();
              var cartItem = GetCartItems(cid);
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItem)
            {
                var orderDetail = new DetailsOfBill()
                {
                    BillId = bill.Id,
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Image = item.Image,
                    Total = item.Total,
                };
                db.DetailsOfBills.Add(orderDetail);
            }
            db.SaveChanges();
        }
        public void CreatBill(string cid)
        {
            var customer = db.Customers.FirstOrDefault(x => x.Id == cid);
            // var cartItem = GetCartItems(cid);
            // Iterate over the items in the cart, 
            // adding the order details for each
            Bill bill = new Bill();
            bill.Id = (dbBill.GetBillId() + 1).ToString();
            bill.CustomerId = customer.Id;
            bill.CustomerName = customer.Name;
            bill.Address = customer.Address;
            bill.City = customer.City;
            bill.Datetime = DateTime.Now;
            bill.Payment = false;
            bill.Points = (int)(GetTotal(cid) * 5 / 100);
            bill.State = customer.State;
            bill.Total = GetTotal(cid);
            bill.Status = "Confirm";
            //  bill.ServiceCharge=          
            db.SaveChanges();

            SaveDetailsOfBill(bill, cid);

        }


        public List<ItemCartView> AddToCartCurrent(string pid, List<ItemCartView> cart, ref string result)
        {
            Product p = dbProduct.Get(pid);
            if (dbProduct.CheckAmountOfProduct(pid))
            {
                if (cart == null)
                {
                    ItemCartView item = new ItemCartView();
                    item.ProductId = pid;
                    item.ProductName = p.Name;
                    item.Price = p.Price;
                    item.Total = p.Price * (100 - p.Discount) / 100;
                    item.Image = p.Image1;
                    item.Amount = 1;
                    item.Status = 1;
                    cart = new List<ItemCartView>();
                    cart.Add(item);
                    result = "1";
                    return cart;//themm thanh cong
                    
                }
                else
                {
                    ItemCartView i = cart.Find(x => x.ProductId == pid);
                    if (i == null)
                    {
                        ItemCartView item = new ItemCartView();
                        item.ProductId = pid;
                        item.ProductName = p.Name;
                        item.Price = p.Price;
                        item.Total = p.Price * (100 - p.Discount) / 100;
                        item.Image = p.Image1;
                        item.Amount = 1;
                        item.Status = 1;
                        cart.Add(item);
                        result = "1";
                        return cart;
                    }
                    if (dbProduct.GetAmountOfProductCurrent(pid) > i.Amount)
                    {
                        cart.Remove(i);
                        i.Amount += 1;
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
            }
         
            result = "0";
            return cart;
        }
        public List<ItemCartView> RemoveAmountOfCartItemCurrent(string pid, List<ItemCartView> cart, int amount)
        {
            ItemCartView item = cart.Find(x => x.ProductId == pid);
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
            return cart;
        }
        public decimal GetTotalItemCurrent(string pid, List<ItemCartView> cart)
        {
            if(cart==null)
            {
                return 0;
            }
            ItemCartView item= cart.Find(x=> x.ProductId==pid);
            if(item==null)
            {
                return 0;
            }
            return item.Total;
        }
  
        public int GetCountCurrent(List<ItemCartView> list)
        {
            if(list==null)
            { return 0; }
            int count = 0;
            foreach(ItemCartView item in list)
            {
                count += item.Amount;
            }
            return count;
        }
        public decimal GetTotalCurrent(List<ItemCartView> list)
        {
            if (list == null)
            { return 0; }
            decimal total = 0;
            foreach (ItemCartView item in list)
            {
                total =total+ item.Total;
            }
            return total; ;
        }
        public void SaveDetailsOfBillCurrent(Bill bill, List<ItemCartView> list)
        {
            DetailsOfBill detail = new DetailsOfBill();
            foreach (var item in list)
            {
                var orderDetail = new DetailsOfBill()
                {
                    BillId = bill.Id,
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Image = item.Image,
                    Total = item.Total,
                };
                db.DetailsOfBills.Add(orderDetail);
            }
            db.SaveChanges();
        }
    }

}
