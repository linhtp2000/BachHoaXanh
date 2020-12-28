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
        //Cart GetCartItem(string productid);
        string AddToCart(string pid, string cid);
        void RemoveAmountOfCartItem(string pid, string cid, int amount);
        void RemoveCartItem(string pid, string cid);
        void EmptyCart(string cid);
        List<Cart> GetCartItems(string id);
        int GetCount(string ID);
        decimal GetTotal(string cid);
        decimal GetTotalItem(string cid, string pid);
        void SaveDetailsOfBill(Bill bill, string id);
        //string GetCartId(HttpContextBase context);
        //void MigrateCart(string userName);
    }

}
