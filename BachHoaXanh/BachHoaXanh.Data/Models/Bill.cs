using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class Bill
    {
        public DateTime Datetime { get; set; }
        [Required]      //requiments for Name
                        //...
        [MaxLength(255)]
        [Key]
        public string Id { get; set; }
        [MaxLength(255)]
        public string CustomerId { get; set; }
        public virtual Customer Customers { get; set; }
        [MaxLength(255)]
        //  [ForeignKey("Employee")]
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Total { get; set; }
        public int Points { get; set; }
        public string Status { get; set; }
        // public virtual List<DetailsOfBill> DetailsOfBill { get; set; }
        public virtual ICollection<DetailsOfBill> DetailsOfBill { get; set; }
    }
}
