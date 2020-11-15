using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
   public interface IProvidersData
    {
        IEnumerable<Provider> GetAll();
        Provider Get(string id);
        void Add(Provider provider);
        void Update(Provider provider);
        void Delete(string id);
    }
}
