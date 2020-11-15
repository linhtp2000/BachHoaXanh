using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class DetailsOfBill
    {
        [Required]
        [MaxLength(255)]
         [Key]
         [Column(Order=1)]
      //  [ForeignKey("Product")]
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        [MaxLength(255)]
        [Key]
        [Column(Order = 2)]
        //  [ForeignKey("Bill")]
        public string BillId { get; set; }
        public virtual Bill Bill { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
       
        public int Amount { get; set; }
    }
}
