using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Role
    {

        
        [Key]

        public int ID { get; set; }
        public string RoleName { get; set; } = null;

        public string? Description { get; set; }

        public virtual ICollection<User>? Users { get; set; }
        public virtual ICollection<Customer>? Customers { get; set; }
    }
}
