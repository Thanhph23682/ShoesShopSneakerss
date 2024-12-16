using APPDATA.DB;
using Microsoft.AspNetCore.Mvc;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        private readonly ShopDbContext _context;

        public AdminLoginController(ShopDbContext context)
        {
            _context = context;
        }
        public IActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginAdmin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Tên đăng nhập và mật khẩu không được để trống.";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user != null)
            {
                // Lưu thông tin đăng nhập vào Session
                HttpContext.Session.SetInt32("UserID", user.ID);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetInt32("RoleID", user.RoleID);
                HttpContext.Session.SetString("FullName", user.FullName);

                if (user.RoleID == 2)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (user.RoleID == 1)
                {
                    return RedirectToAction("LoginAdmin", "EmployeeDashboard");
                }
                else
                {
                    ViewBag.Error = "Vai trò không hợp lệ.";
                    return View();
                }
            }
            else
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}