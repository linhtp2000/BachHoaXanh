using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlDetailsOfInvoiceData: IDetailsOfInvoiceData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlDetailsOfInvoiceData(BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(DetailsOfInvoice detailsOfInvoice )
        {
            db.DetailsOfInvoices.Add(detailsOfInvoice);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var detailsOfInvoice = db.DetailsOfInvoices.Find(id);
            db.DetailsOfInvoices.Remove(detailsOfInvoice);
            db.SaveChanges();
        }
        public IEnumerable<Cart> GetAll()
        {
            return from r in db.Carts
                   orderby r.CustomerId
                   select r;
        }

        public DetailsOfInvoice GetInvoiceId(string id)
        {
            return db.DetailsOfInvoices.FirstOrDefault(r => r.InvoiceId == id);
        }

        public DetailsOfInvoice GetProductId(string id)
        {
            return db.DetailsOfInvoices.FirstOrDefault(r => r.ProductId == id);
        }

        public void Update(DetailsOfInvoice detailsOfInvoice)
        {
            var entry = db.Entry(detailsOfInvoice);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }

        IEnumerable<DetailsOfInvoice> IDetailsOfInvoiceData.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
