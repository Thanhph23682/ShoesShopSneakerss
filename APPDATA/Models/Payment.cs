using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentDescription { get; set; }
        public int PaymentImage { get; set; }
        public virtual Order? order { get; set; }

    }
}
