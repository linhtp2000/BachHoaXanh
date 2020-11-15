using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface IAreaData
    {
        IEnumerable<Area> GetAll();
       Area Get(string id);
        void Add(Area area);
        void Update(Area area);
        void Delete(string id);
    }
}
