using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Color
    {
        [Key]
        public int colorID { get; set; }
        public string? colorName { get; set; }
        public string? colorValue { get; set; }
        public ICollection<ProductVariant>? ProductVariants { get; set; }

    }
}
