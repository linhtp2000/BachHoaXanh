using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.ModelView
{
    public class SalesModelView
    {
        public int year { get; set; }
        public List<SalesModelChildView> salesbymonth { get; set; }
    }
    public class SalesModelChildView
    {
        public int month { get; set; }
        public decimal total { get; set; }
    }
}
