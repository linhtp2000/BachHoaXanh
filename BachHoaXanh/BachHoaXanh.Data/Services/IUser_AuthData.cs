using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface IUser_AuthData
    {
      //  IEnumerable<> GetAll();
       User_Authorize Get(string user, string auth);
        void Add(User_Authorize user_auth);
        void Update(User_Authorize user_auth);
        void Delete(string user, string auth);
    }
}
