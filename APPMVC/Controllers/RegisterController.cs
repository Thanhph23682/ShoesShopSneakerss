using APPDATA.DB;
using APPDATA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BCrypt.Net;

namespace APPMVC.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ShopDbContext _context;

        public RegisterController(ShopDbContext context)
        {
            _context = context;
        }
        // Kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        // Kiểm tra định dạng số điện thoại
        private bool IsValidPhone(string phoneNumber)
        {
            var regex = new Regex(@"^(0|\+84)\d{9,10}$");
            return regex.IsMatch(phoneNumber);
        }

        // GET: Register
        //[Route("Register.html", Name = "Register")]
        public IActionResult Register()
        {
            return View("~/Views/Login/Register.cshtml");
        }


        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string password, string fullName, string email, string phoneNumber)
        {
            // Validate phía server
            if (string.IsNullOrWhiteSpace(fullName))
            {
                ModelState.AddModelError("FullName", "Họ và tên không được để trống.");
            }
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                ModelState.AddModelError("Email", "Email không hợp lệ.");
            }
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {   
                ModelState.AddModelError("Password", "Mật khẩu phải có ít nhất 6 ký tự.");
            }
            if (string.IsNullOrWhiteSpace(phoneNumber) || !IsValidPhone(phoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Số điện thoại không hợp lệ.");
            }

            if (!ModelState.IsValid)
            {
                return View(); // Trả về View với thông báo lỗi
            }

            // Kiểm tra nếu email hoặc số điện thoại đã tồn tại
            var existingUser = await _context.Customers
                .AnyAsync(u => u.Email == email || u.PhoneNumber == phoneNumber);

            if (existingUser)
            {
                ModelState.AddModelError("Email", "Email hoặc số điện thoại đã tồn tại.");
                return View();
            }

            // Thực hiện đăng ký người dùng
            var user = new Customer
            {
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(password) ,// Mã hóa mật khẩu
                RoleId = 3,
                CreateDate = DateTime.Now
            };

            _context.Customers.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Login");
        }
    }
}
