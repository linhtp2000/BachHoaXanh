using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface IUserData
    {
       // IEnumerable<User> GetAll();
        User Get(string id);
        void Add(User user);
        void Update(User user);
        void Delete(string id);
    }
}
