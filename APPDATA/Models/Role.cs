using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
            Customers = new HashSet<Customer>();
        }
        public int Id { get; set; }
        public string RoleName { get; set; } = null;

        public string? Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
