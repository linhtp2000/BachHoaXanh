using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryUser_AuthData:IUser_AuthData
    {
        List<User_Authorize> users_authorizes;
        public InMemoryUser_AuthData()
        {
            users_authorizes = new List<User_Authorize>()
            {
                new User_Authorize{UserId="NV01", AuthId="1",Note="" }
            };
        }
        public void Add(User_Authorize user_authorize)
        {
            users_authorizes.Add(user_authorize);
        }
        public void Update(User_Authorize user_authorize)
        {
            var existing = Get(user_authorize.UserId, user_authorize.AuthId);
            if (existing != null)
            {
                existing.Note = user_authorize.Note;
            }

        }

        public User_Authorize Get(string user,string auth)
        {
            // users_authorizes.FirstOrDefault(r => ( r.UserId == user));
            //users_authorizes.FirstOrDefault(r => (r.AuthId == auth));
            //return Get(user, auth);
            return users_authorizes.SingleOrDefault(r => (r.UserId == user && r.AuthId == auth));
        }

        //public IEnumerable<Area> GetAll()
        //{
        //    return areas.OrderBy(r => r.Name);
        //}

        public void Delete(string user, string auth)
        {
            var user_authorize = Get(user,auth);
            if (user_authorize != null)
            {
                users_authorizes.Remove(user_authorize);
            }

        }
    }
}
