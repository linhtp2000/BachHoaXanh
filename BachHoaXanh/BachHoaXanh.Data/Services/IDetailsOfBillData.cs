using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface IDetailsOfBillData
    {
        IEnumerable<DetailsOfBill> GetAll();
        DetailsOfBill GetBillId(string id);
        DetailsOfBill GetProductId(string id);
        void Add(DetailsOfBill detailsOfBill);
        void Update(DetailsOfBill detailsOfBill);
        void Delete(string id);
    }
}
