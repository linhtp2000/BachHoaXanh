using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace BachHoaXanh.Data.Models
{
    public class Area
    {
        [Required]      //requiments for Name
                        //...
        [MaxLength(255)]
        [Key]
        public string Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        //public virtual List<Branch> Branches { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
    }
}
