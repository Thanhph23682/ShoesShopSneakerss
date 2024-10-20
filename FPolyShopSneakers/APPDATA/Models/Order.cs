using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CustommerId { get; set; }
        public int payment_Status { get; set; }
        public int order_Status { get; set; }
        public DateTime order_Date { get; set; }
        public DateTime update_Date { get; set; }
        public decimal total_Amount { get; set; }
        public string? order_Desc { get; set; }
        public string order_Phone { get; set; }
        public string order_Name { get; set; }
        public string order_Address { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        //public User? User { get; set; }
        //public Customer? Customer { get; set; }
    }
}
