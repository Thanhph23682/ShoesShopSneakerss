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
        public int Id { get; set; }
        public int orderId { get; set; }
        public int quantity { get; set; }
        public decimal amount_Price { get; set; }
        public virtual Order? order { get; set; }
        public ICollection<ProductVariant>? ProductVariants { get; set; }

    }
}
