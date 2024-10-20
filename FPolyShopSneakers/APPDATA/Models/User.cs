﻿using System;
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
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Image { get; set; }
        public bool? Status { get; set; }

        public virtual Role Role { get; set; } = null;
    }
}