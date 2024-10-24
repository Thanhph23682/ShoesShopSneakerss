﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPDATA.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Carts = new HashSet<Cart>();
            CreateDate = DateTime.Now; // Đặt ngày tạo mặc định là thời điểm hiện tại
            Status = true; // Đặt trạng thái mặc định là hoạt động
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; } = null;
        public string FullName { get; set; } = null;
        public int PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; } = null;
        public string? Image { get; set; }
        public DateTime? BirthDay { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual Role Role { get; set; } = null;
    }
}
