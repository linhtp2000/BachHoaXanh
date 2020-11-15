using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface ILoginData
    {

        Login GetAccount(Login account);
        Login GetEmail(string email);        
        void Verify();

    }
}
