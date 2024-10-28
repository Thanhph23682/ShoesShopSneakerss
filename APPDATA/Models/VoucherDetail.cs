using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class VoucherDetail
    {
        [Key]

        public int Id { get; set; }
        public int idVoucher { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public virtual Voucher? voucher { get; set; }
    }
}
