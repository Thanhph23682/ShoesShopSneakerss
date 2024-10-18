using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class size
    {
        [Key]
        public int sizeID { get; set; }
        public int nameSize { get; set; }
    }
}
