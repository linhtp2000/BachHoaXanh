using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface ICartData
    {
        IEnumerable<Cart> GetAll();
        Cart Get(string id);
        void Add(Cart cart);
       // void Update(Cart cart);
        void Delete(string id);
    }

}
