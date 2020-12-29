using BachHoaXanh.Data.ModelView;
using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BachHoaXanh.Data.Services
{
    public class SqlBillData : IBillData
    {
        BachHoaXanhDbContext db = new BachHoaXanhDbContext();
      
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
        public int GetBillId()
        {
            var stringlist = (from bill in db.Bills
                              select bill.Id).ToList();
            //Doi BillId kieu string sang int
            List<int> intlist = stringlist.Select(s => int.Parse(s)).ToList();
            return intlist.Max();
        }

        public Bill Get(string id)
        {
            return db.Bills.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Models.Bill> GetAll()
        {
            return from r in db.Bills
                   orderby r.CustomerId
                   select r;
        }
        public IEnumerable<Models.Bill> GetAllBill()
        {
            var bills = (from r in db.Bills
                         select r);
            return bills.ToList();
        }

        public void Update(Bill bill)
        {
            var entry = db.Entry(bill);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
       
 
    }
}
