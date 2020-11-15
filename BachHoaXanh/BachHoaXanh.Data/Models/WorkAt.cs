using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class WorkAt
    {
        [Required]      //requiments for Name                        //...
        [MaxLength(255)]
        [Key]
        [Column(Order = 1)]
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        [MaxLength(255)]
        [Key]
        [Column(Order = 2)]
        public string BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        [MaxLength(255)]
        public string Position { get; set; }

    }
}
