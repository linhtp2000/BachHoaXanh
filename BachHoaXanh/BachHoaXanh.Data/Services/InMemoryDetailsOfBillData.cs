using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryDetailsOfBillData:IDetailsOfBillData
    {
        List<DetailsOfBill> detailsOfBills ;
        public InMemoryDetailsOfBillData()
        {
            detailsOfBills = new List<DetailsOfBill>()
            {
                new DetailsOfBill{ProductId="SP01", ProductName="Sua", BillId="HD01",Amount=6 }
            };
        }
        public void Add(DetailsOfBill detailsOfBill)
        {
           detailsOfBills.Add(detailsOfBill);
        }
        public void Update(DetailsOfBill detailsOfBill)
        {
            var existing = GetBillId(detailsOfBill.BillId);
            if (existing != null)
            {
                existing.ProductId = detailsOfBill.ProductId;
                existing.ProductName = detailsOfBill.ProductName;
                existing.Amount = detailsOfBill.Amount;
            }
        }

        public DetailsOfBill GetBillId(string id)
        {
            return detailsOfBills.FirstOrDefault(r => r.BillId == id);
        }
        public DetailsOfBill GetProductId(string id)
        {
            return detailsOfBills.FirstOrDefault(r => r.ProductId == id);
        }

        public IEnumerable<DetailsOfBill> GetAll()
        {
            return detailsOfBills.OrderBy(r => r.BillId);
        }

        public void Delete(string id)
        {
            var detailsOfBill = GetBillId(id);
            if (detailsOfBill != null)
            {
                detailsOfBills.Remove(detailsOfBill);
            }

        }
    }
}
