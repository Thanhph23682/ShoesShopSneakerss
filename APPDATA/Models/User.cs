using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public  class User
    {
     
        [Key]

        public int ID { get; set; }
        public int RoleID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }

        public virtual Role Role { get; set; } = null;
        public ICollection<Order>? Orders { get; set; }


    }
}
