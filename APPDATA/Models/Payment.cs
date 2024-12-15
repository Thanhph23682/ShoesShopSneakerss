using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Payment
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentID { get; set; }
        //public int UserID { get; set; }
       /// <summary>
       /// public int OrderId { get; set; }
       /// </summary>
        public string PaymentMethod { get; set; } = null!;
        public string? PaymentDescription { get; set; }

        //public string? PaymentImage { get; set; }
        public string? PaymentImage { get; set; } = string.Empty;
        public virtual Order? Order { get; set; }


    }
}
