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
    public class AdminVoucherDetailsController : Controller
    {
        private readonly ShopDbContext _context;

        public AdminVoucherDetailsController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminVoucherDetails
        public async Task<IActionResult> Index()
        {
            var shopDbContext = _context.VoucherDetails.Include(v => v.Voucher);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: Admin/AdminVoucherDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VoucherDetails == null)
            {
                return NotFound();
            }

            var voucherDetail = await _context.VoucherDetails
                .Include(v => v.Voucher)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (voucherDetail == null)
            {
                return NotFound();
            }

            return View(voucherDetail);
        }

        // GET: Admin/AdminVoucherDetails/Create
        public IActionResult Create()
        {
            ViewData["VoucherID"] = new SelectList(_context.Vouchers, "ID", "ID");
            return View();
        }

        // POST: Admin/AdminVoucherDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,VoucherID,Quantity,Status")] VoucherDetail voucherDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voucherDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VoucherID"] = new SelectList(_context.Vouchers, "ID", "ID", voucherDetail.VoucherID);
            return View(voucherDetail);
        }

        // GET: Admin/AdminVoucherDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VoucherDetails == null)
            {
                return NotFound();
            }

            var voucherDetail = await _context.VoucherDetails.FindAsync(id);
            if (voucherDetail == null)
            {
                return NotFound();
            }
            ViewData["VoucherID"] = new SelectList(_context.Vouchers, "ID", "ID", voucherDetail.VoucherID);
            return View(voucherDetail);
        }

        // POST: Admin/AdminVoucherDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,VoucherID,Quantity,Status")] VoucherDetail voucherDetail)
        {
            if (id != voucherDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voucherDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoucherDetailExists(voucherDetail.ID))
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
            ViewData["VoucherID"] = new SelectList(_context.Vouchers, "ID", "ID", voucherDetail.VoucherID);
            return View(voucherDetail);
        }

        // GET: Admin/AdminVoucherDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VoucherDetails == null)
            {
                return NotFound();
            }

            var voucherDetail = await _context.VoucherDetails
                .Include(v => v.Voucher)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (voucherDetail == null)
            {
                return NotFound();
            }

            return View(voucherDetail);
        }

        // POST: Admin/AdminVoucherDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VoucherDetails == null)
            {
                return Problem("Entity set 'ShopDbContext.VoucherDetails'  is null.");
            }
            var voucherDetail = await _context.VoucherDetails.FindAsync(id);
            if (voucherDetail != null)
            {
                _context.VoucherDetails.Remove(voucherDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoucherDetailExists(int id)
        {
          return (_context.VoucherDetails?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
