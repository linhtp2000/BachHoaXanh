using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class Classify
    {
        [Required]      //requiments for Name
                        //...
        [MaxLength(255)]
        [Key]
        public string Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        //[ForeignKey("Category")]
        public string CategoryId { get; set; }
        public virtual Category Categories { get; set; }
        // public virtual List<Product> Products { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
