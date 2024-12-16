using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public class Customer
    {
        //public Customer()
        //{
        //    Carts = new HashSet<Cart>();
        //    CreateDate = DateTime.Now; // Đặt ngày tạo mặc định là thời điểm hiện tại
        //}
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; } = null;
        public string FullName { get; set; } = null;
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; } = null;
        public string? Image { get; set; }
        public DateTime? BirthDay { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Cart>? Carts { get; set; }
        public virtual ICollection<Voucher>? Vouchers { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual Role? Role { get; set; } = null;
    }
}
