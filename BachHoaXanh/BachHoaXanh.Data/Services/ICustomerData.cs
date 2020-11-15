using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface ICustomerData
    {
        IEnumerable<Customer> GetAll();
        Customer Get(string id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(string id);
    }
}
