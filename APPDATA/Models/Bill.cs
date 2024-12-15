// APPDATA/Models/Bill.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APPDATA.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public string BillCode { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }  // Staff/Cashier
        public int? CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal ChangeAmount { get; set; }
        public string PaymentMethod { get; set; }
        public int Status { get; set; }  // 1: Completed, 0: Pending, -1: Cancelled
        public int Items { get; set; }
        public virtual User? User { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<BillDetails>? BillDetails { get; set; }
        public int BillId { get; set; }
    }
}
