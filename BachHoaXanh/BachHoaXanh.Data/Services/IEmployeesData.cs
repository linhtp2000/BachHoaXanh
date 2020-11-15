using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();
        Employee Get(string id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(string id);
    }
}
