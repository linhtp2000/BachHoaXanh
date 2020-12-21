using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.ModelView
{
    public class LineChartVM
    {
        public List<int> labels { get; set; }
        public List<LineChartChildVM> datasets { get; set; }
        public LineChartVM()
        {
            labels = new List<int>();
            datasets = new List<LineChartChildVM>();
        }
    }
    public class LineChartChildVM
    {
        public LineChartChildVM()
        {
            //data = new List<object>();
        }
        public string label { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public int borderWidth { get; set; }
        public bool fill { get; set; }
        public Object data { get; set; }

    }
}

