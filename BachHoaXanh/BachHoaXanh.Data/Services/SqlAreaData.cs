using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlAreaData: IAreaData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlAreaData (BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(Area area)
        {
            db.Areas.Add(area);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var area = db.Areas.Find(id);
            db.Areas.Remove(area);
            db.SaveChanges();
        }

        public Area Get(string id)
        {
            return db.Areas.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Area> GetAll()
        {
            return from r in db.Areas
                   orderby r.Name
                   select r;
        }

        public void Update(Area area)
        {
            var entry = db.Entry(area);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
