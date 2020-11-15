using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
   public class InMemoryEmployeeData: IEmployeesData
    {

        List<Employee> employees;
        public InMemoryEmployeeData()
        {
            employees = new List<Employee>()
            {
                new Employee{Id="NV01", Name="Linh", Password="123", ManagerId="NV01",AdminId="NV01",Salary=500000,Email="linh@gmailcom" }
            };
        }
        public void Add(Employee employee)
        {
            employees.Add(employee);
        }
        public void Update(Employee employee)
        {
            var existing = Get(employee.Id);
            if (existing != null)
            {
                existing.Name = employee.Name;
                existing.Password = employee.Password;
                existing.ManagerId = employee.ManagerId;
                existing.AdminId = employee.AdminId;
                existing.Salary = employee.Salary;
                existing.Email = employee.Email;
            }
        }

        public Employee Get(string id)
        {
            return employees.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return employees.OrderBy(r => r.Name);
        }

        public void Delete(string id)
        {
            var employee = Get(id);
            if (employee != null)
            {
                employees.Remove(employee);
            }

        }
    }
}
