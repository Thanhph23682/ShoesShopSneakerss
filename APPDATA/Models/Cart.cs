using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Cart
    {
       
        [Key]

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual Customer? Customers { get; set; }
        public virtual ICollection<CartItems>? CartItems { get; set; }

    }
}
