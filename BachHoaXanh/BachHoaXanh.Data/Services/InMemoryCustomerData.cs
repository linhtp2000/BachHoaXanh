using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryCustomerData: ICustomerData
    {
        List<Customer> customers;
        public InMemoryCustomerData()
        {
            customers = new List<Customer>()
            {
                new Customer{Id="KH01", Name="Linh",Address="Quan 9",Password="123", PhoneNumber="0123456",Email="linh@gmail.com"}
            };
        }
        public void Add(Customer customer)
        {
            customers.Add(customer); ;
        }
        public void Update(Customer customer)
        {
            var existing = Get(customer.Id);
            if (existing != null)
            {
                existing.Name = customer.Name;
                existing.Address = customer.Address;
                existing.PhoneNumber = customer.PhoneNumber;
                existing.Password = customer.Password;
                existing.Email = customer.Email;
            }
        }

        public Customer Get(string id)
        {
            return customers.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return customers.OrderBy(r => r.Name);
        }

        public void Delete(string id)
        {
            var customer = Get(id);
            if (customer != null)
            {
                customers.Remove(customer);
            }

        }
    }
}
