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

        public int ID { get; set; }
        public int VoucherID { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public virtual Voucher? Voucher { get; set; }
    }
}
