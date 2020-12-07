using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BachHoaXanh.Data.Services
{
    public interface IBillData
    {
        IEnumerable<Bill> GetAll();
        Bill Get(string id);
        int GetBillId();
        void Add(Bill bill);
        void Update(Bill bill);
        void Delete(string id);
        string GetUserId(HttpContextBase context);
    }
}
