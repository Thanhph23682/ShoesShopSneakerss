using APPDATA.DB;
using APPDATA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;


namespace APPMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ShopDbContext _context;
        public LoginController(ShopDbContext context)
        {
            _context = context;
        }


        // Kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string customerName, string password)
        {
            // Kiểm tra thông tin người dùng
            var customer = await _context.Customers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == customerName);

            if (customer == null || !BCrypt.Net.BCrypt.Verify(password, customer.Password))
            {
                // Thêm thông báo lỗi khi thông tin đăng nhập không đúng
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không chính xác.");
                return View();
            }

            // Lưu thông tin người dùng vào Session
            HttpContext.Session.SetInt32("CustomerID", customer.Id);
            HttpContext.Session.SetString("CustomerName", customer.Email);
            HttpContext.Session.SetString("FullName", customer.FullName);
            HttpContext.Session.SetInt32("RoleID", customer.RoleId);

            // Kiểm tra vai trò người dùng và chuyển hướng tương ứng
            return customer.RoleId switch
            {
                2 => RedirectToAction("Dashboard", "Admin"), // Admin
                1 => RedirectToAction("Index", "Admin"), // Nhân viên
                3 => RedirectToAction("Index", "Home"), // Customer hoặc vai trò khác
            };
        }
    }

}

