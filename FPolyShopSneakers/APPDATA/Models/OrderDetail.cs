using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int orderId { get; set; }
        public int quantity { get; set; }
        public decimal amount_Price { get; set; }

    }
}
