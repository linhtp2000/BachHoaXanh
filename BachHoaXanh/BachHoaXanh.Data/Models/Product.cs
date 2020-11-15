using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class Product
    {
        [Required]      //requiments for Name
                        //...
        [MaxLength(255)]
        [Key]
        public string Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public int Amount { get; set; }
        [MaxLength(255)]
        public string Details { get; set; }
        [MaxLength(255)]
        public string Image { get; set; }
        [MaxLength(255)]
       // [ForeignKey("Classify")]
        public string ClassifyId { get; set; }
        public virtual Classify Classify { get; set; }
        [MaxLength(255)]
      //  [ForeignKey("Branch")]
        public string BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        [MaxLength(255)]
      //  [ForeignKey("Provider")]
        public string ProviderId { get; set; }
        public virtual Provider Provider { get; set; }

        //public virtual List<Cart> Carts { get; set; }
        //public virtual List<DetailsOfBill> DetailsOfBills { get; set; }
        //public virtual List<DetailsOfInvoice> DetailsOfInvoices { get; set; }
        //public virtual List<Rating> Ratings { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<DetailsOfBill> DetailsOfBills { get; set; }
        public virtual ICollection<DetailsOfInvoice> DetailsOfInvoices { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
