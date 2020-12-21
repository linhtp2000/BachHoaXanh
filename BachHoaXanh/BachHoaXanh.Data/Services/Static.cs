using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Services
{
    public class Static
    {
        BachHoaXanhDbContext db = new BachHoaXanhDbContext();

        //Doanh thu theo thang
        public List<decimal> GetSalesStaticByMonth(int year)
        {
            List<decimal> sales = new List<decimal>();

            var months = new List<int>();
            months.Add(1); months.Add(2); months.Add(3); months.Add(4); months.Add(5); months.Add(6);
            months.Add(7); months.Add(8); months.Add(9); months.Add(10); months.Add(11); months.Add(12);

            foreach (var item in months)
            {
                var temp = (from x in db.Bills where x.Datetime.Month == item && x.Datetime.Year == year select x.Total).ToList();
                if (temp == null)
                {
                    sales.Add(0);
                }
                else
                {
                    sales.Add(temp.Sum());
                }
                //decimal salary = (from x in db.Employees select x.Salary).Sum();
                // decimal invoices = (from x in db.Invoices where x.Datetime.Month == item &&x.Datetime.Year==year select x.Total).Sum();
                //sales = sales - salary - invoices;

            }
            return sales;
        }

        //Doanh thu theo năm
        public decimal GetSalesStaticByYear(int year)
        {
            var sales = (from x in db.Bills where x.Datetime.Year == year select x.Total).ToList();
            //decimal salary = (from x in db.Employees select x.Salary).Sum();
            //decimal invoices = (from x in db.Invoices select x.Total).Sum();
            //sales = sales - salary*12 - invoices;
            if (sales == null)
            {
                return 0;
            }
            return sales.Sum(); ;
        }

        //So luong don hang theo thang
        public List<int> GetBillStaticByMonth(int year)
        {
            List<int> sales = new List<int>();

            var months = new List<int>();
            months.Add(1); months.Add(2); months.Add(3); months.Add(4); months.Add(5); months.Add(6);
            months.Add(7); months.Add(8); months.Add(9); months.Add(10); months.Add(11); months.Add(12);

            foreach (var item in months)
            {
                var temp = (from x in db.Bills where x.Datetime.Month == item && x.Datetime.Year == year select x.Id).ToList();
                if (temp == null)
                {
                    sales.Add(0);
                }
                else
                {
                    sales.Add(temp.Count());
                }
            }
            return sales;
        }

        //So luong don hang theo nam
        public int GetBillStaticByYear(int year)
        {

            var bills = (from x in db.Bills where x.Datetime.Year == year select x.Id).ToList();
            if (bills == null)
            {
                return 0;
            }

            return bills.Count();
        }

    }
}
