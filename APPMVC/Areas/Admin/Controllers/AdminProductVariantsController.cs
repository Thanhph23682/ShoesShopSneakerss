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
    public class AdminProductVariantsController : Controller
    {
        private readonly ShopDbContext _context;

        public AdminProductVariantsController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminProductVariants
        public async Task<IActionResult> Index()
        {
            var shopDbContext = _context.ProductVariants.Include(p => p.Color).Include(p => p.OrderDetail).Include(p => p.Products).Include(p => p.Size);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: Admin/AdminProductVariants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductVariants == null)
            {
                return NotFound();
            }

            var productVariant = await _context.ProductVariants
                .Include(p => p.Color)
                .Include(p => p.OrderDetail)
                .Include(p => p.Products)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (productVariant == null)
            {
                return NotFound();
            }

            return View(productVariant);
        }

        // GET: Admin/AdminProductVariants/Create
        public IActionResult Create()
        {
            ViewData["ColorID"] = new SelectList(_context.Colors, "ColorID", "ColorID");
            ViewData["OrderDetailID"] = new SelectList(_context.OrderDetails, "ID", "ID");
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID");
            ViewData["SizeID"] = new SelectList(_context.Sizes, "SizeID", "SizeID");
            return View();
        }

        // POST: Admin/AdminProductVariants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OrderDetailID,ProductID,ColorID,SizeID,Quantity,UpdateDate,UpdateUser")] ProductVariant productVariant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productVariant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorID"] = new SelectList(_context.Colors, "ColorID", "ColorID", productVariant.ColorID);
            ViewData["OrderDetailID"] = new SelectList(_context.OrderDetails, "ID", "ID", productVariant.OrderDetailID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID", productVariant.ProductID);
            ViewData["SizeID"] = new SelectList(_context.Sizes, "SizeID", "SizeID", productVariant.SizeID);
            return View(productVariant);
        }

        // GET: Admin/AdminProductVariants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductVariants == null)
            {
                return NotFound();
            }

            var productVariant = await _context.ProductVariants.FindAsync(id);
            if (productVariant == null)
            {
                return NotFound();
            }
            ViewData["ColorID"] = new SelectList(_context.Colors, "ColorID", "ColorID", productVariant.ColorID);
            ViewData["OrderDetailID"] = new SelectList(_context.OrderDetails, "ID", "ID", productVariant.OrderDetailID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID", productVariant.ProductID);
            ViewData["SizeID"] = new SelectList(_context.Sizes, "SizeID", "SizeID", productVariant.SizeID);
            return View(productVariant);
        }

        // POST: Admin/AdminProductVariants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,OrderDetailID,ProductID,ColorID,SizeID,Quantity,UpdateDate,UpdateUser")] ProductVariant productVariant)
        {
            if (id != productVariant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productVariant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductVariantExists(productVariant.ID))
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
            ViewData["ColorID"] = new SelectList(_context.Colors, "ColorID", "ColorID", productVariant.ColorID);
            ViewData["OrderDetailID"] = new SelectList(_context.OrderDetails, "ID", "ID", productVariant.OrderDetailID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID", productVariant.ProductID);
            ViewData["SizeID"] = new SelectList(_context.Sizes, "SizeID", "SizeID", productVariant.SizeID);
            return View(productVariant);
        }

        // GET: Admin/AdminProductVariants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductVariants == null)
            {
                return NotFound();
            }

            var productVariant = await _context.ProductVariants
                .Include(p => p.Color)
                .Include(p => p.OrderDetail)
                .Include(p => p.Products)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (productVariant == null)
            {
                return NotFound();
            }

            return View(productVariant);
        }

        // POST: Admin/AdminProductVariants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductVariants == null)
            {
                return Problem("Entity set 'ShopDbContext.ProductVariants'  is null.");
            }
            var productVariant = await _context.ProductVariants.FindAsync(id);
            if (productVariant != null)
            {
                _context.ProductVariants.Remove(productVariant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductVariantExists(int id)
        {
          return (_context.ProductVariants?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
