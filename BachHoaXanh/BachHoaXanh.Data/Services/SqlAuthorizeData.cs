using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlAuthorizeData: IAuthorizeData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlAuthorizeData(BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(Authorize authorize)
        {
            db.Authorizes.Add(authorize);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var authorize = db.Authorizes.Find(id);
            db.Authorizes.Remove(authorize);
            db.SaveChanges();
        }

        public Authorize Get(string id)
        {
            return db.Authorizes.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Authorize> GetAll()
        {
            return from r in db.Authorizes
                   orderby r.Id
                   select r;
        }

        public void Update(Authorize authorize)
        { 
            var entry = db.Entry(authorize);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
