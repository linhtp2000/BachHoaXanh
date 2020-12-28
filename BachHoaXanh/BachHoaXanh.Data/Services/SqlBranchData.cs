using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlBranchData: IBranchData
    {
        private BachHoaXanhDbContext db= new BachHoaXanhDbContext();
        public SqlBranchData (BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public SqlBranchData()
        {         
        }
        public void Add(Branch branch)
        {
            db.Branchs.Add(branch);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var branch = db.Branchs.Find(id);
            db.Branchs.Remove(branch);
            db.SaveChanges();
        }

        public Branch Get(string id)
        {
            return db.Branchs.First(r => r.Name == id);
        }

        public List<string> GetAll()
        {
            var list = db.Branchs.Select(x => x.Name).Distinct();
            return list.ToList();
        }

        public void Update(Branch branch)
        {
            var entry = db.Entry(branch);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
