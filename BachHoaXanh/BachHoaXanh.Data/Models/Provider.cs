using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class Provider
    {
        [Required]      //requiments for Name
                        //...
        [MaxLength(255)]
        [Key]
        public string Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [MaxLength(255)]
        public string PhoneNumber { get; set; }

        //public virtual List<Product> Products { get; set; }
        //public virtual List<Invoice> Invoices { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
