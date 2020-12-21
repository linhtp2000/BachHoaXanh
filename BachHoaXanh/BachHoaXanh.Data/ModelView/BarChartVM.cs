using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.ModelView
{

    public class BarChartVM
    {
        public List<int> labels { get; set; }
        public List<BarChartChildVM> datasets { get; set; }
        public BarChartVM()
        {
            labels = new List<int>();
            datasets = new List<BarChartChildVM>();
        }
    }
    public class BarChartChildVM
    {
        public BarChartChildVM()
        {
            //data = new List<object>();
        }
        public string label { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public int borderWidth { get; set; }
        public string hoverBackgroundColor { get; set; }
        public string hoverBorderColor { get; set; }
        public Object data { get; set; }

    }
}



