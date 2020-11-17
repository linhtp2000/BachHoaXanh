using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlCategoryData: ICategoryData
    {
        private readonly BachHoaXanhDbContext  db;
        public SqlCategoryData (BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var catagory = db.Categories.Find(id);
            db.Categories.Remove(catagory);
            db.SaveChanges();
        }

        public Category Get(string id)
        {
            return db.Categories.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return from r in db.Categories
                   orderby r.Name
                   select r;
        }

        public void Update(Category category)
        {
            var entry = db.Entry(category);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
