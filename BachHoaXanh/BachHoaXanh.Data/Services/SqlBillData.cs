using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlBillData: IBillData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlBillData(BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(Bill bill)
        {
            db.Bills.Add(bill);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
            db.SaveChanges();
        }

        public Bill Get(string id)
        {
            return db.Bills.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Bill> GetAll()
        {
            return from r in db.Bills
                   orderby r.CustomerId
                   select r;
        }

        public void Update(Bill bill)
        {
            var entry = db.Entry(bill);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
