using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class InMemoryUserData: IUserData
    {
        List<User> users;
        //public InMemoryUserData()
        //{
        //    users = new List<User>()
        //    {
        //        new User{Id="NV01",Email="linh@gmail.com", PassWord="123" }
        //    };
        //}
        public void Add(User user)
        {
            users.Add(user);
        }
        public void Update(User user)
        {
            var existing = Get(user.Id);
            if (existing != null)
            {
                existing.Name = user.Name;
            }

        }

        public User Get(string id)
        {
            return users.FirstOrDefault(r => r.Id == id);
        }

        //public IEnumerable<User> GetAll()
        //{
        //    return users.OrderBy(r => r.Name);
        //}

        public void Delete(string id)
        {
            var user = Get(id);
            if (user != null)
            {
                users.Remove(user);
            }

        }
    }
}
