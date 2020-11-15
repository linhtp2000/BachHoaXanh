using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface IDetailsOfInvoiceData
    {
        IEnumerable<DetailsOfInvoice> GetAll();
        DetailsOfInvoice GetProductId(string id);
        DetailsOfInvoice GetInvoiceId(string id);
        void Add(DetailsOfInvoice details);
        void Update(DetailsOfInvoice details);
        void Delete(string id);
    }
}
