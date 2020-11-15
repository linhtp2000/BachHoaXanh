using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlWorkAtData: IWorkAtData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlWorkAtData(BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(WorkAt workat)
        {
            db.WorkAts.Add(workat);
            db.SaveChanges();

        }
        public void Delete(string employee, string branch)
        {
            var workat = db.WorkAts.SingleOrDefault(r=>(r.EmployeeId==employee&&r.BranchId==branch));
            db.WorkAts.Remove(workat);
            db.SaveChanges();
        }
        public IEnumerable<WorkAt> GetAll()
        {
            return from r in db.WorkAts
                   orderby r.BranchId
                   select r;
        }

        public WorkAt GetBranch(string id)
        {
            return db.WorkAts.FirstOrDefault(r => r.BranchId == id);
        }

        public WorkAt GetWorkAt(string employee, string branch)
        {
            return db.WorkAts.SingleOrDefault(r => (r.EmployeeId == employee && r.BranchId == branch));
        }

        public void Update(WorkAt workat)
        {
            var entry = db.Entry(workat);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
