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
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public double Price { get; set; }
        public int Points { get; set; }

       // public virtual List<DetailsOfBill> DetailsOfBill { get; set; }
        public virtual ICollection<DetailsOfBill> DetailsOfBill { get; set; }
    }
}
