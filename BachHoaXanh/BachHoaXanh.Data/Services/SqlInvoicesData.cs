using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlInvoicesData:IInvoiceData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlInvoicesData(BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(Invoice invoice)
        {
            db.Invoices.Add(invoice);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
        }

        public Invoice Get(string id)
        {
            return db.Invoices.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Invoice> GetAll()
        {
            return from r in db.Invoices
                   orderby r.Id
                   select r;
        }

        public void Update(Invoice invoice)
        {
            var entry = db.Entry(invoice);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
