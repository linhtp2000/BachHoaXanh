using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BachHoaXanh.Data.Models
{
    public class Cart
    {
        [Required]
        [MaxLength(255)]
        [Key]
        [Column(Order = 1)]
        //  [ForeignKey("Product")]
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        [MaxLength(255)]
        [Key]
        [Column(Order = 2)]
        //  [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }

        public string Image { get; set; }
        public int Status { get; set; }

    }
}
