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

        public int Id { get; set; }
        public int idCustomer { get; set; }
        public int idUser { get; set; }
        public DateTime CreatDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string? Conditions { get; set; }
        public string? PercentDiscount { get; set; }
        public int status { get; set; }
        public virtual Customer? Customer { get; set; }
        public ICollection<VoucherDetail>? voucherDetails { get; set; }

    }
}
