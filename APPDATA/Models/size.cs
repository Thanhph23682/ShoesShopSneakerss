using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Size
    {
        [Key]
        public int SizeID { get; set; }
        public int NameSize { get; set; }
        public ICollection<ProductVariant>? ProductVariants { get; set; }

    }
}
