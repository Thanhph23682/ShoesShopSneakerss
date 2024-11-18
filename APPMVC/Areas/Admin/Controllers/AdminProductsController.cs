using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APPDATA.DB;
using APPDATA.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using PagedList.Core;
using FPolyShopSneakers.Helpper;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly ShopDbContext _context;
        public INotyfService _NotyfService { get; }

        public AdminProductsController(ShopDbContext context, INotyfService notyfService)
        {
            _context = context;
            _NotyfService = notyfService;

        }

        // GET: Admin/AdminProducts
        public async Task<IActionResult> Index(int? page, string name)
        {
            var shopDbContext = _context.Products.Include(p => p.Brand).Include(p => p.Category);
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CategoryID", "Name");//viewbag
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name");//viewbag 

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var listProducts = _context.Products.AsNoTracking()
                .Include(p => p.Category )
                .Include(p => p.Brand )
                .OrderByDescending(p => p.ProductID).AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                listProducts = listProducts.Where(p => p.NameProduct.ToLower().Contains(name));
            }

            PagedList<Products> models = new PagedList<Products>(listProducts, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(models);
        }

        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name");
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CategoryID", "Name");
            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,CategoryID,BrandID,NameProduct,Price,Desciption,CreateDate,Thumbnail,ImagePath,Alias")] Products products, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                products.NameProduct = Utilities.ToTitleCase(products.NameProduct);
                if (ImageFile != null)
                {
                    string extension = Path.GetExtension(ImageFile.FileName);
                    string images = Utilities.SEOUrl(products.NameProduct) + extension;
                    products.ImagePath = await Utilities.UploadFile(ImageFile, @"Products", images.ToLower());
                }
                if (string.IsNullOrEmpty(products.ImagePath)) products.ImagePath = "default.jpg";
                products.Alias = Utilities.SEOUrl(products.NameProduct);
                products.CreateDate = DateTime.Now;






                // Thêm sản phẩm vào context và lưu vào cơ sở dữ liệu

                _context.Add(products);

                await _context.SaveChangesAsync();
                _NotyfService.Success("Tạo mới thành công");

                return RedirectToAction(nameof(Index));
            }
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name", products.BrandID);
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CategoryID", "Name", products.CategoryID);
            return View(products);
        }

        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name", products.BrandID);
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CategoryID", "Name", products.CategoryID);
            return View(products);
        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,CategoryID,BrandID,NameProduct,Price,Desciption,CreateDate,Thumbnail,ImagePath,Alias")] Products products, IFormFile? ImageFile)
        {
            if (id != products.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    products.NameProduct = Utilities.ToTitleCase(products.NameProduct);
                    if (ImageFile != null)
                    {
                        string extension = Path.GetExtension(ImageFile.FileName);
                        string images = Utilities.SEOUrl(products.NameProduct) + extension;
                        products.ImagePath = await Utilities.UploadFile(ImageFile, @"Products", images.ToLower());
                    }
                    if (string.IsNullOrEmpty(products.ImagePath)) products.ImagePath = "default.jpg";

                    _context.Update(products);
                    await _context.SaveChangesAsync();
                    _NotyfService.Success("Cập nhật thành công");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductID))
                    {
                        return NotFound();
                        _NotyfService.Error("Có lỗi xảy ra");

                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name", products.BrandID);
            ViewData["DanhMuc"] = new SelectList(_context.Categories, "CategoryID", "Name", products.CategoryID);
            return View(products);
        }

        // GET: Admin/AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ShopDbContext.Products'  is null.");
            }
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
                _NotyfService.Success("Xóa thành công");

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
