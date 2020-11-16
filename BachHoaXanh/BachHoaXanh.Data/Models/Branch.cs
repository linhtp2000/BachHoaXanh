using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class Branch
    {
        [Required]     
        [MaxLength(255)]
        [Key]
        public string Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string Address { get; set; }
       // [MaxLength(255)]
       //// [ForeignKey("Employee")]
       // public string ManagerId { get; set; }
       // public virtual Employee Manager { get; set; }     //foreign key Manager' id
        [MaxLength(255)]
       // [ForeignKey("Area")]
        public string AreaId { get; set; }     
        public virtual Area Area { get; set; }

        //public virtual List<Invoice> Invoices { get; set; }
        //public virtual List<Product> Products { get; set; }
       // public virtual List<Employee> Employees { get; set; }// list Employees are working at Branch
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }// list Employees are working at Branch
        public virtual ICollection<WorkAt> WorkAts { get; set; }
    }
}

