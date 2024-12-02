using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APPDATA.DB;
using APPDATA.Models;
using FPolyShopSneakers.Helpper;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCustomersController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly INotyfService _NotyfService; // Corrected to use INotyfService instead of object

        public AdminCustomersController(ShopDbContext context, INotyfService notyfService)
        {
            _context = context;
            _NotyfService = notyfService;
        }

        // GET: Admin/AdminCustomers
        public async Task<IActionResult> Index()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "ID", "RoleName");
            var shopDbContext = _context.Customers.Include(c => c.Role);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: Admin/AdminCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/AdminCustomers/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "ID", "RoleName");
            return View();
        }

        // POST: Admin/AdminCustomers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleId,Email,FullName,PhoneNumber,Address,Password,Image,BirthDay,CreateDate,Status")] Customer customer, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                customer.FullName = Utilities.ToTitleCase(customer.FullName);
                customer.BirthDay = customer.BirthDay ?? DateTime.Now;
                if (ImageFile != null)
                {
                    string extension = Path.GetExtension(ImageFile.FileName);
                    string images = Utilities.SEOUrl(customer.FullName) + extension; // Corrected from user.UserName to customer.UserName
                    customer.Image = await Utilities.UploadFile(ImageFile, @"Customer", images.ToLower());
                }
                if (string.IsNullOrEmpty(customer.Image)) customer.Image = "default.jpg";
               
                
                _context.Add(customer);
                await _context.SaveChangesAsync();
                _NotyfService.Success("Tạo mới tài khoản thành công");
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "ID", "RoleName", customer.RoleId);
            

            return View(customer);
        }

        // GET: Admin/AdminCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "ID", "RoleName", customer.RoleId);
            return View(customer);
        }

        // POST: Admin/AdminCustomers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,Email,FullName,PhoneNumber,Address,Password,Image,BirthDay,CreateDate,Status")] Customer customer, IFormFile ImageFile)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    customer.FullName = Utilities.ToTitleCase(customer.FullName);
                    if (ImageFile != null)
                    {
                        string extension = Path.GetExtension(ImageFile.FileName);
                        string images = Utilities.SEOUrl(customer.FullName) + extension;
                        customer.Image = await Utilities.UploadFile(ImageFile, @"Customer", images.ToLower());
                    }
                    if (string.IsNullOrEmpty(customer.Image)) customer.Image = "default.jpg";

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                    _NotyfService.Success("Cập nhật tài khoản thành công");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))  // Corrected user.ID to customer.Id
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "ID", "RoleName", customer.RoleId);
            return View(customer);
        }

        // GET: Admin/AdminCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admin/AdminCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'ShopDbContext.Customers' is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            _NotyfService.Success("Xóa tài khoản thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
