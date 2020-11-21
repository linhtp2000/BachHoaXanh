using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class Customer
    {
        [Required]      //requiments for Name
                        //...
        [MaxLength(255)]
        [Key]
        public string Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }
        public int Points { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }

        //public virtual List<Bill> Bills { get; set; }
        //public virtual List<Rating> Ratings { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
