using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface IBranchData
    {
        IEnumerable<Branch> GetAll();
        Branch Get(string id);
        void Add(Branch branch);
        void Update(Branch branch);
        void Delete(string id);
    }
}
