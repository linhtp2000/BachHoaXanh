using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class User
    {
        [Required]      //requiments for Name
                        //...
        [MaxLength(255)]
        [Key]
        [Column(Order = 1)]
        public string Id { get; set; }
        // public virtual Employee Employee { get; set; }
        public string Name { get; set; }
        //[MaxLength(255)]
        //[Key]
        //[Column(Order = 2)]
        // [DataType(DataType.EmailAddress)]
        //public string Email { get; set; }
        ////  public virtual Employee EmployeeEmail { get; set; }
        //[MaxLength(255)]
        //public string PassWord { get; set; }

        public virtual ICollection<User_Authorize> User_Authorizes { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
