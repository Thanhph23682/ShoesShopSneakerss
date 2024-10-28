using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        public string? ContactName { get; set; }
        public string? ContactDescription { get; set; }
        public string? ContactImageURL { get; set; }
    }
}
