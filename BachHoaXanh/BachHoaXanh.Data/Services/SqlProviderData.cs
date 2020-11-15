using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlProviderData: IProvidersData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlProviderData (BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(Provider provider)
        {
            db.Providers.Add(provider);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var provider = db.Providers.Find(id);
            db.Providers.Remove(provider);
            db.SaveChanges();
        }

        public Provider Get(string id)
        {
            return db.Providers.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Provider> GetAll()
        {
            return from r in db.Providers
                   orderby r.Name
                   select r;
        }

        public void Update(Provider provider)
        {
            var entry = db.Entry(provider);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
