using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface IAuthorizeData
    {
      //  IEnumerable<Authorize> GetAll();
        Authorize Get(string id);
        void Add(Authorize authorize);
       // void Update(Authorize authorize);
        void Delete(string id);
    }
}
