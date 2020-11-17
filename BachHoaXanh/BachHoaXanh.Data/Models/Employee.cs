using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class Employee
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
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
       // [MaxLength(255)]
       // [ForeignKey("Branch")]
        //public string BranchId { get; set; }
        //public virtual Branch WorkAtBranch { get; set; }
        [MaxLength(255)]
      // [ForeignKey("Employee")]
        public string ManagerId { get; set; }
        public virtual Employee Manager { get; set; }
        [MaxLength(255)]
      // [ForeignKey("Employee")]
        public string AdminId { get; set; }
        public virtual Employee Administrator { get; set; }

        [MaxLength(255)]
        // [ForeignKey("Employee")]
        public string UserdId { get; set; }
        public virtual User User { get; set; }

        public double Salary { get; set; }

        public virtual ICollection<Branch> ManageBranch { get; set; }
       // public virtual List<Bill> Bills { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Employee> Manage { get; set; }
        public virtual ICollection<Employee> Admin { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<WorkAt> WorkAts { get; set; }


    }
}
