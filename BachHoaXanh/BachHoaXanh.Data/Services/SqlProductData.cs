using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlProductData : IProductData
    {
        private BachHoaXanhDbContext db = new BachHoaXanhDbContext();

        public bool CheckAmountOfProduct(string id)
        {
            bool flag = false;
            var p = (from u in db.Products
                     where u.Id == id && u.Amount > 0
                     select u).ToList();
            if (p != null)
            {
                flag = true;
            }
            return flag;
        }
        public int GetAmountOfProductCurrent(string id)
        {
            var p = db.Products.SingleOrDefault(x => x.Id == id);

            return p.Amount;
        }
        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();

        }
        public void Delete(string id)
        {

            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
        }

        public Product Get(string id)
        {
            return db.Products.SingleOrDefault(r => r.Id == id);
        }
        public int GetIdMax()
        {
            var list= db.Products.Select(r => r.Id).ToList();
            if(list==null)
            {
                return 1;
            }
            //Doi BillId kieu string sang int
            List<int> intlist = list.Select(s => int.Parse(s)).ToList();
            return intlist.Max()+1;
        }

        public IEnumerable<Product> GetAll()
        {
            return from r in db.Products
                   orderby r.Name
                   select r;
        }

        public void Update(Product product)
        {
            var entry = db.Entry(product);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
       
    }
}
