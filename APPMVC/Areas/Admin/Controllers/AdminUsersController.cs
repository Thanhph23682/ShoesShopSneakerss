using APPDATA.DB;
using APPDATA.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using FPolyShopSneakers.Helpper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        private readonly ShopDbContext _context;
        public INotyfService _NotyfService { get; }

        public AdminUsersController(ShopDbContext context, INotyfService notyfService)
        {
            _context = context;
            _NotyfService = notyfService;
        }

        // GET: Admin/AdminUsers
        public async Task<IActionResult> Index()
        {
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "RoleName");
            var shopDbContext = _context.Users.Include(u => u.Role);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: Admin/AdminUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/AdminUsers/Create
        public IActionResult Create()
        {
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "RoleName");
            return View();
        }

        // POST: Admin/AdminUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RoleID,UserName,FullName,Password,PhoneNumber,Image,Status")] User user, IFormFile? ImageFile)
        {
            if (!ModelState.IsValid)
            {
                user.UserName = Utilities.ToTitleCase(user.UserName);
                if (ImageFile != null)
                {
                    string extension = Path.GetExtension(ImageFile.FileName);
                    string images = Utilities.SEOUrl(user.UserName) + extension;
                    user.Image = await Utilities.UploadFile(ImageFile, @"Users", images.ToLower());
                }
                if (string.IsNullOrEmpty(user.Image)) user.Image = "default.jpg";

                _context.Add(user);
                await _context.SaveChangesAsync();
                _NotyfService.Success("Tạo mới tài khoản thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "RoleName", user.RoleID);
            ViewData["Roles"] = new SelectList(_context.Roles, "ID", "RoleName", user.Status);



            return View(user);
        }

        // GET: Admin/AdminUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "RoleName", user.RoleID);
            return View(user);
        }

        // POST: Admin/AdminUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RoleID,UserName,FullName,Password,PhoneNumber,Image,Status")] User user, IFormFile ImageFile)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    user.UserName = Utilities.ToTitleCase(user.UserName);
                    if (ImageFile != null)
                    {
                        string extension = Path.GetExtension(ImageFile.FileName);
                        string images = Utilities.SEOUrl(user.UserName) + extension;
                        user.Image = await Utilities.UploadFile(ImageFile, @"User", images.ToLower());
                    }
                    if (string.IsNullOrEmpty(user.Image)) user.Image = "default.jpg";

                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    _NotyfService.Success("Cập nhật tài khoản thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        _NotyfService.Error("Có lỗi xảy ra");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "RoleName", user.RoleID);
            return View(user);
        }

        // GET: Admin/AdminUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/AdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ShopDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            _NotyfService.Success("Xóa tài khoản thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
