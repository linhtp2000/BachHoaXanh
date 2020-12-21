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
        [MaxLength(255)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int Amount { get; set; }
        [MaxLength(255)]
        public string Details { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image1 { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image2 { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image3 { get; set; }
        [MaxLength(255)]
        // [ForeignKey("Classify")]
        public string ClassifyId { get; set; }
        [MaxLength(255)]
        //  [ForeignKey("Branch")]
        public string BranchId { get; set; }
        // public virtual Branch Branch { get; set; }
        [MaxLength(255)]
        //  [ForeignKey("Provider")]
        public string ProviderId { get; set; }
    }
}
