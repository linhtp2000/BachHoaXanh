using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryDetailsOfInvoiceData:IDetailsOfInvoiceData
    {
        List<DetailsOfInvoice> detailsOfInvoices;
        public InMemoryDetailsOfInvoiceData()
        {
            detailsOfInvoices = new List<DetailsOfInvoice>()
            {
                new DetailsOfInvoice{ProductId="SP01", ProductName="Sua", InvoiceId="HD01", Price=10000, Amount=6}
            };
        }
        public void Add(DetailsOfInvoice detailsOfInvoice)
        {
            detailsOfInvoices.Add(detailsOfInvoice);
        }
        public void Update(DetailsOfInvoice detailsOfInvoice)
        {
            var existing = GetInvoiceId(detailsOfInvoice.InvoiceId);
            if (existing != null)
            {
                //existing.
                existing.ProductId= detailsOfInvoice.ProductId;
                existing.ProductName = detailsOfInvoice.ProductName;
                existing.InvoiceId = detailsOfInvoice.InvoiceId;
                existing.Price = detailsOfInvoice.Price;
                existing.Amount = detailsOfInvoice.Amount;
            }

        }

        public DetailsOfInvoice GetInvoiceId(string id)
        {
            return detailsOfInvoices.FirstOrDefault(r => r.InvoiceId == id);
        }
        public DetailsOfInvoice GetProductId(string id)
        {
            return detailsOfInvoices.FirstOrDefault(r => r.ProductId == id);
        }
        public IEnumerable<DetailsOfInvoice> GetAll()
        {
            return detailsOfInvoices.OrderBy(r => r.InvoiceId);
        }

        public void Delete(string id)
        {
            var detailsOfInvoice = GetInvoiceId(id);
            if (detailsOfInvoice != null)
            {
                detailsOfInvoices.Remove(detailsOfInvoice);
            }

        }
    }
}
