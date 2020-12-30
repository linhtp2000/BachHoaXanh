using BachHoaXanh.Data.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BachHoaXanh.Data.Services;
using OfficeOpenXml;

namespace BachHoaXanh.Web.Controllers
{
    [Authorize(Roles = "QuanTri")]
    public class StaticController : Controller
    {
        // GET: Static
        public BachHoaXanhDbContext db = new BachHoaXanhDbContext();
        public SqlBillData dbBill = new SqlBillData();
        public Static st = new Static();
        public ActionResult BillStatic()
        {
            var billChart = new List<BarChartVM>();

            billChart.Add(GetBillStaticByMonth());
            billChart.Add(GetBillStaticByYear());

            return View(billChart);
        }
        private BarChartVM GetBillStaticByMonth()
        {
            var barChartData = new BarChartVM();
            //var labels = new List<string>();
            //labels.Add("Jan"); labels.Add("Feb"); labels.Add("Mar"); labels.Add("Apr"); labels.Add("May"); labels.Add("Jun");
            //labels.Add("Jul"); labels.Add("Aug"); labels.Add("Sep"); labels.Add("Oct"); labels.Add("Nov"); labels.Add("Dec");

            var datasets = new List<BarChartChildVM>();

            var months = new List<int>();
            months.Add(1); months.Add(2); months.Add(3); months.Add(4); months.Add(5); months.Add(6);
            months.Add(7); months.Add(8); months.Add(9); months.Add(10); months.Add(11); months.Add(12);

            var childModel = new BarChartChildVM();
            childModel.label = "Amount Of Bills by Month in 2020";
            childModel.backgroundColor = @"rgba(255,99,132,1)";
            childModel.borderColor = @"rgba(255,99,132,1)";
            childModel.borderWidth = 1;
            childModel.hoverBackgroundColor = @"rgba(255,99,132,0.4)";
            childModel.hoverBorderColor = @"rgba(255,99,132,1)";

            childModel.data = st.GetBillStaticByMonth(DateTime.Now.Year);
            datasets.Add(childModel);
            barChartData.datasets = datasets;
            barChartData.labels = months;
            return barChartData;
        }

        private BarChartVM GetBillStaticByYear()
        {
            var barChartData = new BarChartVM();
            var datasets = new List<BarChartChildVM>();
            List<int> bills = new List<int>();

            int year = DateTime.Now.Year;
            var years = new List<int>();
            years.Add(year - 4);
            years.Add(year - 3);
            years.Add(year - 2);
            years.Add(year - 1);
            years.Add(year);

            foreach (int item in years)
            {
                bills.Add(st.GetBillStaticByYear(item));
            }

            var childModel = new BarChartChildVM();
            childModel.label = "Amount Of Bills by Month in 2020";
            childModel.backgroundColor = @"rgba(255,99,132,1)";
            childModel.borderColor = @"rgba(255,99,132,1)";
            childModel.borderWidth = 1;
            childModel.hoverBackgroundColor = @"rgba(255,99,132,0.4)";
            childModel.hoverBorderColor = @"rgba(255,99,132,1)";

            childModel.data = bills;
            datasets.Add(childModel);
            barChartData.datasets = datasets;
            barChartData.labels = years;
            return barChartData;
        }

        public ActionResult SalesStatic()
        {
            var salesChart = new List<LineChartVM>();

            salesChart.Add(GetSaleStaticByMonth());
            salesChart.Add(GetSaleStaticByYear());

            return View(salesChart);

        }
        private LineChartVM GetSaleStaticByMonth()
        {
            var lineChartData = new LineChartVM();
            var datasets = new List<LineChartChildVM>();

            var months = new List<int>();
            months.Add(1); months.Add(2); months.Add(3); months.Add(4); months.Add(5); months.Add(6);
            months.Add(7); months.Add(8); months.Add(9); months.Add(10); months.Add(11); months.Add(12);

            var childModel = new LineChartChildVM();
            childModel.label = "Sales by Month in 2020";
            childModel.backgroundColor = @"rgba(255,99,132,1)";
            childModel.borderColor = @"rgba(255,99,132,1)";
            childModel.borderWidth = 2;

            childModel.data = st.GetSalesStaticByMonth(DateTime.Now.Year);
            datasets.Add(childModel);
            lineChartData.datasets = datasets;
            lineChartData.labels = months;
            return lineChartData;
        }

        private LineChartVM GetSaleStaticByYear()
        {
            var lineChartData = new LineChartVM();
            var datasets = new List<LineChartChildVM>();
            List<decimal> sales = new List<decimal>();

            int year = DateTime.Now.Year;
            var years = new List<int>();
            years.Add(year - 4);
            years.Add(year - 3);
            years.Add(year - 2);
            years.Add(year - 1);
            years.Add(year);

            foreach (int item in years)
            {
                sales.Add(st.GetBillStaticByYear(item));
            }

            var childModel = new LineChartChildVM();
            childModel.label = "Sales within five years";
            childModel.backgroundColor = @"rgba(255,99,132,1)";
            childModel.borderColor = @"rgba(255,99,132,1)";
            childModel.borderWidth = 2;

            childModel.data = sales;
            datasets.Add(childModel);
            lineChartData.datasets = datasets;
            lineChartData.labels = years;
            return lineChartData;
        }
        public void ExportToExcel()
        {
            List<SalesModelChildView> modelbyyear = new List<SalesModelChildView>();
            var year = (from x in db.Bills select x.Datetime.Year).Distinct();

            var months = new List<int>();
            months.Add(1); months.Add(2); months.Add(3); months.Add(4); months.Add(5); months.Add(6);
            months.Add(7); months.Add(8); months.Add(9); months.Add(10); months.Add(11); months.Add(12);

            SalesModelChildView modelbymonth = new SalesModelChildView();

            foreach (var item in months)
            {
                var totalsale = (from x in db.Bills where x.Datetime.Month == item select x.Total).ToList();
                if (totalsale == null)
                {
                    modelbymonth.total = 0;
                }
                else
                {
                    modelbymonth.total = totalsale.Sum();
                }
                modelbymonth.month = item;
                modelbyyear.Add(modelbymonth);
            }

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A2"].Value = "Jan";
            ws.Cells["B2"].Value = "Feb";
            ws.Cells["C2"].Value = "Mar";
            ws.Cells["D2"].Value = "Apr";
            ws.Cells["E2"].Value = "May";
            ws.Cells["F2"].Value = "Jun";
            ws.Cells["G2"].Value = "Jul";
            ws.Cells["H2"].Value = "Aug";
            ws.Cells["I2"].Value = "Sep";
            ws.Cells["J2"].Value = "Oct";
            ws.Cells["K2"].Value = "Nov";
            ws.Cells["L2"].Value = "Dec";

            int row = 3;
            foreach (var item in modelbyyear)
            {
                ws.Cells[string.Format("A{0}", row)].Value = item.total;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelStatic.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
        public ActionResult AmountOfBillForMonth()
        {

            return View();
        }
    }
}