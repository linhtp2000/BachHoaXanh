using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace BachHoaXanh.Data.Services
{
    public interface IProductData
    {

        IEnumerable<Product> GetAll();
        Product Get(string id);
        void Add(Product product);
        void Update(Product product);
        void Delete(string id);
    }
}
