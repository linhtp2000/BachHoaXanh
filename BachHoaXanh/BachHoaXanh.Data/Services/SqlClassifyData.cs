using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlClassifyData: IClassifyData
    {
        private  BachHoaXanhDbContext db= new BachHoaXanhDbContext();
        public SqlClassifyData(BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public SqlClassifyData()
        {
        }
        public void Add(Classify classify )
        {
            db.Classifys.Add(classify);
            db.SaveChanges();
        }
        public void Delete(string id)
        {
            var classify = db.Classifys.Find(id);
            db.Classifys.Remove(classify);
            db.SaveChanges();
        }

        public Classify Get(string id)
        {
            return db.Classifys.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Classify> GetAll()
        {
            var list = db.Classifys.OrderBy(x => x.Name).Select(x => x).Distinct();
            return list.AsEnumerable<Classify>();
        }

        public void Update(Classify classify)
        {
            var entry = db.Entry(classify);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
