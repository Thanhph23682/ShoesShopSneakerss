using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class ProductVariant
    {
        [Key]
        public int ProductVariantID { get; set; }
        public int productID { get; set; }
        public int colorID { get; set; }
        public int sizeID { get; set; }
        public int Quantity { get; set; }
        public DateTime update_date { get; set; }
        public string update_User { get; set; }
    }
}
