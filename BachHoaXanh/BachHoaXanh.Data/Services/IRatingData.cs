using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface IRatingData
    {
        IEnumerable<Rating> GetAll();
        Rating Get(string id);
        void Add(Rating rating);
        void Update(Rating rating);
        void Delete(string id);
    }
}
