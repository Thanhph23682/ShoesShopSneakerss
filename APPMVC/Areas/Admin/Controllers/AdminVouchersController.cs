using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APPDATA.DB;
using APPDATA.Models;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminVouchersController : Controller
    {
        private readonly ShopDbContext _context;

        public AdminVouchersController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminVouchers
        public async Task<IActionResult> Index()
        {
            var shopDbContext = _context.Vouchers.Include(v => v.Customer);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: Admin/AdminVouchers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vouchers == null)
            {
                return NotFound();
            }

            var voucher = await _context.Vouchers
                .Include(v => v.Customer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // GET: Admin/AdminVouchers/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "Id", "Email");
            return View();
        }

        // POST: Admin/AdminVouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CustomerID,UserID,CreatDate,StartDate,EndDate,Description,Amount,Conditions,PercentDiscount,Status")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voucher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CustomerID"] = new SelectList(_context.Customers, "Id", "Email", voucher.CustomerID);
            return View(voucher);
        }

        // GET: Admin/AdminVouchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vouchers == null)
            {
                return NotFound();
            }

            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "Id", "Email", voucher.CustomerID);
            return View(voucher);
        }

        // POST: Admin/AdminVouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CustomerID,UserID,CreatDate,StartDate,EndDate,Description,Amount,Conditions,PercentDiscount,Status")] Voucher voucher)
        {
            if (id != voucher.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voucher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoucherExists(voucher.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "Id", "Email", voucher.CustomerID);
            return View(voucher);
        }

        // GET: Admin/AdminVouchers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vouchers == null)
            {
                return NotFound();
            }

            var voucher = await _context.Vouchers
                .Include(v => v.Customer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // POST: Admin/AdminVouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vouchers == null)
            {
                return Problem("Entity set 'ShopDbContext.Vouchers'  is null.");
            }
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher != null)
            {
                _context.Vouchers.Remove(voucher);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoucherExists(int id)
        {
          return (_context.Vouchers?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
