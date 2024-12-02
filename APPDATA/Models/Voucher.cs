using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Voucher
    {
        [Key]

        public int ID { get; set; }
        public int? CustomerID { get; set; }
        public DateTime CreatDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string? Conditions { get; set; }
        public string? PercentDiscount { get; set; }
        public int Status { get; set; }
        public virtual Customer? Customer { get; set; }
        public ICollection<VoucherDetail>? VoucherDetails { get; set; }

    }
}
