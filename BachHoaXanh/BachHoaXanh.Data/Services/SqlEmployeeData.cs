using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlEmployeeData: IEmployeesData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlEmployeeData(BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(Employee employee )
        {
            db.Employees.Add(employee);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
        }

        public Employee Get(string id)
        {
            return db.Employees.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return from r in db.Employees
                   orderby r.Name
                   select r;
        }

        public void Update(Employee employee)
        {
            var entry = db.Entry(employee);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
