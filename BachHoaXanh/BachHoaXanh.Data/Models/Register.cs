﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class Register
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string RepeatPassWord { get; set; }
    }
}
