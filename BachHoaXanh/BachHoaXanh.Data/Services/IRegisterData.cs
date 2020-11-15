using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public interface IRegisterData
    {
        Register GetAccount(Register account);
        Register GetEmail(string email);
        void Add();
    }
}
