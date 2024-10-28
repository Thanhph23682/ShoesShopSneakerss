using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class OrderDetail
    {
        [Key]
        public int ID { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal AmountPrice { get; set; }
        public virtual Order? Order { get; set; }
        public ICollection<ProductVariant>? ProductVariants { get; set; }

    }
}
