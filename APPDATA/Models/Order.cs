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

        public int Id { get; set; }
        public int UserId { get; set; } 
        public int CustomerId { get; set; } 
        public int Payment_Status { get; set; }
        public int? Order_Status { get; set; } = null;
        public DateTime? Order_Date { get; set; } = null;
        public DateTime? Update_Date { get; set; } = null;
        public decimal? Total_Amount { get; set; } = null;
        public string? Order_Desc { get; set; } = null;
        public string? Order_Phone { get; set; } = null;
        public string? Order_Name { get; set; } = null;
        public string? Order_Address { get; set; } = null;
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public virtual User? User { get; set; }
        public virtual Customer? Customer { get; set; }

    }
}
