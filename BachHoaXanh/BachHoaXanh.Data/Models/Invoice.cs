using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class Invoice
    {
        [Required]      //requiments for Name
        [Key]
        [MaxLength(255)]
        public string Id { get; set; }
        public DateTime Datetime { get; set; }       
                        //...        
        [MaxLength(255)]
       // [ForeignKey("Provider")]
        public string ProviderId { get; set; }
        public virtual Provider Provider { get; set; }
        [MaxLength(255)]
    //   [ForeignKey("Branch")]
        public string BranhId { get; set; }
        public virtual Branch Branch { get; set; }
        public double Price { get; set; }

        // public virtual List<DetailsOfInvoice> GetDetailsOfInvoices { get; set; }
        public virtual ICollection<DetailsOfInvoice> GetDetailsOfInvoices { get; set; }

    }
}
