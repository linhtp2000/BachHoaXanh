using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachHoaXanh.Data.Models
{
    public class User_Authorize
    {
        [Required]      //requiments for Name                        //...
        [MaxLength(255)]
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [MaxLength(255)]
        [Key]
        [Column(Order = 2)]
        public string AuthId { get; set; }
        public virtual Authorize Authorize { get; set; }
        [MaxLength(255)]
        public string Note { get; set; }        
    }
}
