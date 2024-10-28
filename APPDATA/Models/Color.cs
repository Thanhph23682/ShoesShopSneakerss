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
        public int ColorID { get; set; }
        public string? ColorName { get; set; }
        public string? ColorValue { get; set; }
        public ICollection<ProductVariant>? ProductVariants { get; set; }

    }
}
