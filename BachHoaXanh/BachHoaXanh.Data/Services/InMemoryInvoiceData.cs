using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryInvoiceData: IInvoiceData
    {
        List<Invoice> invoices;
        public InMemoryInvoiceData()
        {
            invoices = new List<Invoice>()
            {
                new Invoice{Id="HD01", Datetime=DateTime.Now, ProviderId="NCC01",BranhId="CN01", Price=20000000}
            };
        }
        public void Add(Invoice invoice)
        {
            invoices.Add(invoice);
        }
        public void Update(Invoice invoice)
        {
            var existing = Get(invoice.Id);
            if (existing != null)
            {
                existing.Id = invoice.Id;
                existing.ProviderId= invoice.ProviderId;
                existing.Datetime = invoice.Datetime;
                existing.BranhId = invoice.BranhId;              
                existing.Price = invoice.Price;
            }

        }

        public Invoice Get(string id)
        {
            return invoices.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Invoice> GetAll()
        {
            return invoices.OrderBy(r => r.Id);
        }

        public void Delete(string id)
        {
            var invoice = Get(id);
            if (invoice != null)
            {
                invoices.Remove(invoice);
            }

        }
    }
}
