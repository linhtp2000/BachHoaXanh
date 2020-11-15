using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface IClassifyData
    {
        IEnumerable<Classify> GetAll();
        Classify Get(string id);
        void Add(Classify classify);
        void Update(Classify classify);
        void Delete(string id);
    }
}
