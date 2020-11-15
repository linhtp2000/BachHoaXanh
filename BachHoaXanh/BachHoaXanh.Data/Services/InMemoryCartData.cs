using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryCartData: ICartData
    {
        List<Cart> carts;
        public InMemoryCartData()
        {
            carts = new List<Cart>()
            {
                new Cart{ProductId="SP01",ProductName="Sua", CustomerId="KH01",Price=6000, Amount=5}
            };
        }
        public void Add(Cart cart)
        {
            carts.Add(cart);
        }       
        public Cart  Get(string id)
        {
            return carts.FirstOrDefault(r => r.CustomerId == id);
        }    

        public IEnumerable<Cart> GetAll()
        {
            return carts.OrderBy(r => r.CustomerId);
        }
        public void Delete(string id)
        {
            var cart = Get(id);
            if (cart != null)
            {
                carts.Remove(cart);
            }

        }


    }
}
