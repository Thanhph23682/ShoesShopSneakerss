﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public  class Brand
    {
        [Key]
        public int brandID { get; set; }
        public string name { get; set; }
    }
}
