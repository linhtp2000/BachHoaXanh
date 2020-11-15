using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
   public interface IInvoiceData
    {
        IEnumerable<Invoice> GetAll();
        Invoice Get(string id);
        void Add(Invoice invoice);
        void Update(Invoice invoice);
        void Delete(string id);
    }
}
