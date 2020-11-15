using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface ICategoryData
    {
        IEnumerable<Category> GetAll();
        Category Get(string id);
        void Add(Category category);
        void Update(Category category);
        void Delete(string id);
    }
}
