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
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        // [MaxLength(255)]
        // [ForeignKey("Branch")]
        //public string BranchId { get; set; }
        //public virtual Branch WorkAtBranch { get; set; }
        [MaxLength(255)]
    //  [ForeignKey("ManageId")]
        public string ManagerId { get; set; }
        public virtual Employee Manage { get; set; }
        [MaxLength(255)]
      // [ForeignKey("AdminId")]
        public string AdminId { get; set; }
        public virtual Employee Admin { get; set; }

        [MaxLength(255)]
        // [ForeignKey("Employee")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public double Salary { get; set; }

        public virtual ICollection<Branch> Branch { get; set; }
       // public virtual List<Bill> Bills { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
      //  [ForeignKey("ManagerId")]
        //[ForeignKey("AdminId")]
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Employee> Employee1s { get; set; }
        //  public virtual ICollection<Employee> Employees{ get; set; }

        // public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<WorkAt> WorkAts { get; set; }


    }
}
