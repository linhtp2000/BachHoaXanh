using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlCartData: ICartData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlCartData (BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(Cart cart)
        {
            db.Carts.Add(cart);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();
        }

        public Cart Get(string id)
        {
            return db.Carts.FirstOrDefault(r => r.CustomerId == id);
        }

        public IEnumerable<Cart> GetAll()
        {
            return from r in db.Carts
                   orderby r.CustomerId
                   select r;
        }

        public void Update(Cart cart)
        {
            var entry = db.Entry(cart);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
