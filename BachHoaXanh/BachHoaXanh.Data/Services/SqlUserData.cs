using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlUserData: IUserData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlUserData(BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();

        }
        public void Delete(string id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public User Get(string id)
        {
            return db.Users.FirstOrDefault(r => r.Id == id);
        }

        //public IEnumerable<Invoice> GetAll()
        //{
        //    return from r in db.Users
        //           orderby r.Id
        //           select r;
        //}

        public void Update(User user)
        {
            var entry = db.Entry(user);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
