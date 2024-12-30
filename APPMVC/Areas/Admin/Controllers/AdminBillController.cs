using APPDATA.DB;
using APPDATA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBillController : Controller
    {

        private readonly ShopDbContext _context;

        public AdminBillController(ShopDbContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index(int? page)
        //{
        //    var pageNumber = page == null || page <= 0 ? 1 : page.Value;
        //    var pageSize = 10;

        //    // Truy vấn dữ liệu
        //    var query = _context.Bill
        //        .Include(b => b.Customer)
        //        .Include(b => b.BillDetails)
        //        .ThenInclude(bd => bd.Product)
        //        .OrderByDescending(b => b.CreateDate);

        //    // Tạo PagedList từ IQueryable
        //    var models = new PagedList<Bill>(
        //        query.AsNoTracking(), // Sử dụng AsNoTracking() để tối ưu hiệu suất
        //        pageNumber,
        //        pageSize
        //    );

        //    ViewBag.CurrentPage = pageNumber;

        //    return View(models);
        //}

        public async Task<IActionResult> Index(int? page, string searchTerm)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;

            // Truy vấn cơ sở dữ liệu
            var query = _context.Bill
                .Include(b => b.BillDetails)
                .ThenInclude(bd => bd.Product)
                .AsNoTracking();

            // Áp dụng tìm kiếm nếu có từ khóa
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(b =>
                    b.BillCode.ToLower().Contains(searchTerm) // Tìm theo mã hóa đơn
                );
            }

            // Sắp xếp và phân trang
            query = query.OrderByDescending(b => b.CreateDate);

            // Tính tổng số hóa đơn trong cơ sở dữ liệu, không phân trang
            var totalBillCount = await query.CountAsync();

            // Lưu tổng số hóa đơn vào ViewBag
            ViewBag.TotalBillCount = totalBillCount;

            // Phân trang dữ liệu
            var models = new PagedList<Bill>(query, pageNumber, pageSize);

            // Lưu thông tin từ khóa tìm kiếm và trang hiện tại
            ViewBag.CurrentPage = pageNumber;
            ViewBag.SearchTerm = searchTerm;

            return View(models);
        }





        public IActionResult Details(int id)
        {
            var bill = _context.Bill
                .Include(b => b.Customer)
                .Include(b => b.User)
                .Include(b => b.BillDetails)
                    .ThenInclude(bd => bd.Product)
                .AsNoTracking() // Thêm dòng này để tối ưu performance
                .FirstOrDefault(b => b.Id == id);

            if (bill == null)
            {
                return NotFound();
            }

            // Đảm bảo load đầy đủ thông tin sản phẩm
            foreach (var detail in bill.BillDetails)
            {
                if (detail.Product == null)
                {
                    // Load product riêng nếu chưa được load
                    detail.Product = _context.Products
                        .AsNoTracking()
                        .FirstOrDefault(p => p.ProductID == detail.ProductId);
                }
            }

            return View(bill);
        }



    }
}

