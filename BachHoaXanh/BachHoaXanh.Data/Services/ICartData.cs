using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Data.Services
{
    public interface ICartData
    {
        //SqlCartData GetCart(Controller controller);
        //SqlCartData GetCart(HttpContextBase context);
        Cart GetCartItem(string productid);
        bool AddToCart(Product product);
        int RemoveAmountOfCartItem(string productid);
        void RemoveCartItem(string productid);
        void EmptyCart();
        List<Cart> GetCartItems();
        int GetCount();
        decimal GetTotal();
        void SaveDetailsOfBill(Bill bill);
        string GetCartId(HttpContextBase context);
        void MigrateCart(string userName);
    }

}
