using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
   public class SqlRatingData:IRatingData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlRatingData(BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(Rating rating)
        {
            db.Ratings.Add(rating);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var rating = db.Ratings.Find(id);
            db.Ratings.Remove(rating);
            db.SaveChanges();
        }

        public Rating Get(string id)
        {
            return db.Ratings.FirstOrDefault(r => r.ProductId == id);
        }

        public IEnumerable<Rating> GetAll()
        {
            return from r in db.Ratings
                   orderby r.ProductId
                   select r;
        }

        public void Update(Rating rating)
        {
            var entry = db.Entry(rating);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
