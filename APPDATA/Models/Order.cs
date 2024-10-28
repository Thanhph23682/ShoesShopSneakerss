using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Order
    {
        [Key]

        public int? Id { get; set; } = null;
        public int? UserId { get; set; } = null;
        public int? CustomerId { get; set; } = null;
        public int? payment_Status { get; set; } = null;
        public int? order_Status { get; set; } = null;
        public DateTime? order_Date { get; set; } = null;
        public DateTime? update_Date { get; set; } = null;
        public decimal? total_Amount { get; set; } = null;
        public string? order_Desc { get; set; } = null;
        public string? order_Phone { get; set; } = null;
        public string? order_Name { get; set; } = null;
        public string? order_Address { get; set; } = null;
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<Payment>? payments { get; set; }
        public virtual User? user { get; set; }
        public virtual Customer? Customer { get; set; }

    }
}
