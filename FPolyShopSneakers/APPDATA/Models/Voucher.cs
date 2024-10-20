using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public int idCustomer { get; set; }
        public int idUser { get; set; }
        public DateTime CreatDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string? Conditions { get; set; }
        public string PercentDiscount { get; set; }
        public int status { get; set; }
        public List<VoucherDetail> VoucherDetails { get; set; }
        //public Customer? Customer { get; set; }
    }
}
