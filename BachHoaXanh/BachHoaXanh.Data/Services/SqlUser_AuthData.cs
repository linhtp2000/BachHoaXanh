using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class SqlUser_AuthData: IUser_AuthData
    {
        private readonly BachHoaXanhDbContext db;
        public SqlUser_AuthData(BachHoaXanhDbContext db)
        {
            this.db = db;
        }
        public void Add(User_Authorize user_authorize)
        {
            db.User_Authorizes.Add(user_authorize);
            db.SaveChanges();

        }
        public void Delete(string user, string auth)
        {
            var user_auth = db.User_Authorizes.SingleOrDefault(r => (r.UserId == user && r.AuthorizeId == auth));
            db.User_Authorizes.Remove(user_auth);
            db.SaveChanges();
        }

        public User_Authorize Get(string user, string auth)
        {
            return  db.User_Authorizes.SingleOrDefault(r => (r.UserId == user && r.AuthorizeId== auth));
        }

        //public IEnumerable<Invoice> GetAll()
        //{
        //    return from r in db.Invoices
        //           orderby r.Id
        //           select r;
        //}

        public void Update(User_Authorize user_authorize)
        {
            var entry = db.Entry(user_authorize);   //provides information, ability to perform actions on the entity
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
