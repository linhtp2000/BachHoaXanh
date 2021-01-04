using BachHoaXanh.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BachHoaXanh.Data.ModelView
{
    public class ItemProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int Amount { get; set; }

        public string Details { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image1 { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image2 { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image3 { get; set; }

        // [ForeignKey("Classify")]
        public string ClassifyName { get; set; }

        //  [ForeignKey("Branch")]
        public string BranchName { get; set; }
        // public virtual Branch Branch { get; set; }

        //  [ForeignKey("Provider")]
        public string ProviderName { get; set; }
    }
}
