using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string? Name { get; set; }
        public ICollection<Products>? Products { get; set; }

    }
}
