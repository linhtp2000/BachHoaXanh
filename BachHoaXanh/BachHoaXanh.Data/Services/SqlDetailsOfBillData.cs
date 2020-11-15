using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlDetailsOfBillData: IDetailsOfBillData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlDetailsOfBillData (BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(DetailsOfBill detailsOfBill)
        {
            db.DetailsOfBills.Add(detailsOfBill);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var detailOfBill = db.DetailsOfBills.Find(id);
            db.DetailsOfBills.Remove(detailOfBill);
            db.SaveChanges();
        }

        public IEnumerable<DetailsOfBill> GetAll()
        {
            return from r in db.DetailsOfBills
                   orderby r.BillId
                   select r;
        }

        public DetailsOfBill GetBillId(string id)
        {
            return db.DetailsOfBills.FirstOrDefault(r => r.BillId == id);
        }

        public DetailsOfBill GetProductId(string id)
        {
            return db.DetailsOfBills.FirstOrDefault(r => r.ProductId == id);
        }

        public void Update(DetailsOfBill detailsOfBill)
        {
            var entry = db.Entry(detailsOfBill);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
