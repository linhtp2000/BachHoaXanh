using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryBillData: IBillData
    {
        List<Bill> bills;
        public InMemoryBillData()
        {
            bills = new List<Bill>()
            {
                new Bill{Id="KV01", Datetime=DateTime.Now, CustomerId="KH01",EmployeeId="NV01", Price=200000, Points=2}
            };
        }
        public void Add(Bill bill)
        {
            bills.Add(bill);
        }
        public void Update(Bill bill)
        {
            var existing = Get(bill.Id);
            if (existing != null)
            {
                existing.Id = bill.Id;
                existing.CustomerId = bill.CustomerId;
                existing.Datetime = bill.Datetime;
                existing.EmployeeId = bill.EmployeeId;
                existing.Points = bill.Points;
                existing.Price = bill.Price;
            }

        }

        public Bill Get(string id)
        {
            return bills.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Bill> GetAll()
        {
            return bills.OrderBy(r => r.Id);
        }

        public void Delete(string id)
        {
            var bill = Get(id);
            if (bill != null)
            {
                bills.Remove(bill);
            }

        }
    }
}
