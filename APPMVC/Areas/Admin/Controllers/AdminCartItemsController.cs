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
    public class AdminCartItemsController : Controller
    {
        private readonly ShopDbContext _context;

        public AdminCartItemsController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminCartItems
        public async Task<IActionResult> Index()
        {
            var shopDbContext = _context.CartItems.Include(c => c.Cart).Include(c => c.Products);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: Admin/AdminCartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItems = await _context.CartItems
                .Include(c => c.Cart)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItems == null)
            {
                return NotFound();
            }

            return View(cartItems);
        }

        // GET: Admin/AdminCartItems/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductID", "ProductID");
            return View();
        }

        // POST: Admin/AdminCartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CartId,ProductId,Quantity,PriceAmount")] CartItems cartItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", cartItems.CartId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductID", "ProductID", cartItems.ProductId);
            return View(cartItems);
        }

        // GET: Admin/AdminCartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItems = await _context.CartItems.FindAsync(id);
            if (cartItems == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", cartItems.CartId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductID", "ProductID", cartItems.ProductId);
            return View(cartItems);
        }

        // POST: Admin/AdminCartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CartId,ProductId,Quantity,PriceAmount")] CartItems cartItems)
        {
            if (id != cartItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemsExists(cartItems.Id))
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
            ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id", cartItems.CartId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductID", "ProductID", cartItems.ProductId);
            return View(cartItems);
        }

        // GET: Admin/AdminCartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItems = await _context.CartItems
                .Include(c => c.Cart)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItems == null)
            {
                return NotFound();
            }

            return View(cartItems);
        }

        // POST: Admin/AdminCartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CartItems == null)
            {
                return Problem("Entity set 'ShopDbContext.CartItems'  is null.");
            }
            var cartItems = await _context.CartItems.FindAsync(id);
            if (cartItems != null)
            {
                _context.CartItems.Remove(cartItems);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemsExists(int id)
        {
          return (_context.CartItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
