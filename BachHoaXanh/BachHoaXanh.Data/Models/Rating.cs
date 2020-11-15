using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class Rating
    {
        [Key]
        [Column(Order = 1)]
        // [ForeignKey("Product")]
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Key]
        [Column(Order = 2)]
        //  [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int Rate { get; set; }
        [Required]
        [MaxLength(255)]
        public string Comments { get; set; }
        public int Points { get; set; }
       
        //public string BiillId { get; set; }
        //public virtual Bill Bill { get; set; }
    }
}
