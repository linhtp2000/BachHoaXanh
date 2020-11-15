using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlCustomerData: ICustomerData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlCustomerData (BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
        }

        public Customer Get(string id)
        {
            return db.Customers.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return from r in db.Customers
                   orderby r.Name
                   select r;
        }

        public void Update(Customer customer)
        {
            var entry = db.Entry(customer);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
