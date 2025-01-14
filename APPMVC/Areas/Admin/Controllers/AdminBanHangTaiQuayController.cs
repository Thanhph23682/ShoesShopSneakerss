
using Microsoft.AspNetCore.Mvc;
using APPDATA.DB;
using APPDATA.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using iTextSharp.text;

using iTextSharp.text.pdf;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Font = System.Drawing.Font;


using APPMVC.ModelsViews;
using PagedList.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Security.Claims;
using static System.Net.WebRequestMethods;



namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminBanHangTaiQuayController : Controller
    {
        private readonly ShopDbContext _context;






        public AdminBanHangTaiQuayController(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page, string searchTerm = null)
        {
            var pageNumber = page ?? 1;
            var pageSize = 10;

            var query = _context.Bill
                .Include(b => b.Customer)
                .Include(b => b.User)
                .Include(b => b.BillDetails)
                .ThenInclude(bd => bd.Product)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b =>
                    b.BillCode.Contains(searchTerm) ||
                    b.Customer.FullName.Contains(searchTerm));
            }

            query = query.OrderByDescending(b => b.CreateDate);

            var bills = await query.ToListAsync();
            var models = new PagedList<Bill>(bills.AsQueryable(), pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.totalProducts = _context.Products.Count();

            return View(models);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var bill = await _context.Bill
                .Include(b => b.Customer)
                .Include(b => b.User)
                .Include(b => b.BillDetails)
                .ThenInclude(bd => bd.Product)
                .FirstOrDefaultAsync(b => b.BillId == id);

            if (bill == null)
                return NotFound();

            var viewModel = new BillViewModel
            {
                BillID = bill.BillId,
                CustomerName = bill.Customer?.FullName ?? "Khách lẻ",
                CreateDate = bill.CreateDate,
                TotalAmount = bill.TotalAmount,
                PaymentMethod = bill.PaymentMethod,
                Status = bill.Status,
                Details = bill.BillDetails.Select(bd => new BillDetailViewModel
                {
                    ProductName = bd.Product.NameProduct,
                    Price = bd.Price,
                    Quantity = bd.Quantity,
                    SubTotal = bd.Total
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult GetCustomers()
        {

            var customers = _context.Customers
                .Select(c => new
                {
                    id = c.Id,
                    name = c.FullName,
                    phone = c.PhoneNumber,
                    address = c.Address,
                })
                .ToList();
            return Json(customers);
        }
        [HttpGet]
        public IActionResult GetStaff()
        {
            var staff = _context.Users
                .Where(u => u.Status == 1) // Chỉ lấy nhân viên đang hoạt động
                .Select(u => new { id = u.ID, name = u.FullName }) // Lựa chọn các trường cần thiết
                .ToList();

            return Json(staff); // Trả về dữ liệu JSON
        }

        //[HttpGet]
        //public IActionResult GetProducts()
        //{
        //    var products = _context.Products
        //        .Select(p => new
        //        {
        //            id = p.ProductID,
        //            name = p.NameProduct,
        //            price = p.Price,
        //            image = "/images/products/" + p.ImagePath

        //        })
        //        .ToList();
        //    return Json(products);
        //}

        [HttpGet]
        public IActionResult GetTotalProducts()
        {
            return Json(_context.Products.Count());
        }
        [HttpGet]
        public IActionResult GetProducts(int page = 1, int itemsPerPage = 8)
        {
            var products = _context.Products
                .Skip((page - 1) * itemsPerPage) // Skip items for previous pages
                .Take(itemsPerPage) // Take the required number of items for the current page
                .Select(p => new
                {
                    id = p.ProductID,
                    name = p.NameProduct,
                    price = p.Price,
                    image = "/images/products/" + p.ImagePath
                })
                .ToList();

            return Json(products);
        }



        //[HttpGet]
        //public IActionResult GetProducts()
        //{
        //    var products = _context.Products
        //        .Include(p => p.ProductVariants)
        //        .ThenInclude(pv => pv.Size)
        //        .Select(p => new
        //        {
        //            id = p.ProductID,
        //            name = p.NameProduct,
        //            price = p.Price,
        //            image = "/images/products/" + p.ImagePath,
        //            sizes = p.ProductVariants.Select(pv => new
        //            {
        //                id = pv.Size.SizeID,
        //                name = pv.Size.NameSize
        //            }).ToList()
        //        })
        //        .ToList();
        //    return Json(products);
        //}


        [HttpPost]
        public async Task<IActionResult> CompleteBill([FromBody] Bill model)
        {
            if (model == null || model.BillDetails == null || model.BillDetails.Count == 0)
            {
                return Json(new { success = false, message = "Chi tiết hóa đơn không hợp lệ." });
            }

            foreach (var item in model.BillDetails)
            {
                if (item.ProductId <= 0 || item.Quantity <= 0 || item.Price <= 0 || item.Total <= 0)
                {
                    return Json(new { success = false, message = "Chi tiết hóa đơn không hợp lệ. Vui lòng kiểm tra lại các thông tin sản phẩm." });
                }
            }

            if (model.PaidAmount < model.TotalAmount)
            {
                return Json(new { success = false, message = "Số tiền thanh toán không đủ." });
            }

            // Tìm khách hàng theo số điện thoại
            //var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == model.CustomerId);
            //if (customer == null)
            //{
            //    return Json(new { success = false, message = "Khách hàng không tồn tại." });
            //}

            using var transaction = await _context.Database.BeginTransactionAsync();
            /* int totalAmountInt = Convert.ToInt32(model.TotalAmount)*/
            ;


            try
            {
                // Nếu CustomerId = 0 thì mặc định là khách lẻ
                //int customerId = model.CustomerId;
                // Kiểm tra và xử lý CustomerId
                //int customerId = (int)(model.CustomerId > 0 ? model.CustomerId : 0);

                //if (customerId > 0)
                //{
                //    var customerExists = await _context.Customers.AnyAsync(c => c.Id == customerId);
                //    if (!customerExists)
                //    {
                //        return Json(new { success = false, message = "Khách hàng không tồn tại." });
                //    }
                //}

                
                //int? customerId = null;
                //var customerType = model.CustomerType; // Thêm trường này vào model

                //if (customerType == "registered")
                //{
                //    // Nếu là khách hàng thành viên, lấy ID từ model
                //    customerId = model.CustomerId;

                //    // Kiểm tra xem khách hàng có tồn tại không
                //    var customerExists = await _context.Customers.AnyAsync(c => c.Id == customerId);
                //    if (!customerExists)
                //    {
                //        return Json(new { success = false, message = "Khách hàng không tồn tại." });
                //    }
                //}


                var bill = new Bill
                {
                    BillCode = GenerateBillCode(),
                    CreateDate = DateTime.Now,
                    UserId = model.UserId,
                    //CustomerId = customerId,
                    //CustomerId = model.CustomerId > 0 ? model.CustomerId : null,

                    /* CustomerId = model.CustomerId,*/  // Lưu Id của khách hàng
                    TotalAmount = model.BillDetails.Sum(x => x.Price * x.Quantity), // Tính tổng tiền từ chi tiết // Tổng tiền chính xác
                    PaidAmount = model.PaidAmount,   // Số tiền thanh toán chính xác
                    ChangeAmount = model.PaidAmount - model.TotalAmount,
                    PaymentMethod = model.PaymentMethod,
                    Status = 1,
                    Items = model.BillDetails.Count
                };
            

                _context.Bill.Add(bill);
                await _context.SaveChangesAsync();


                foreach (var item in model.BillDetails)
                {

                    var billDetail = new BillDetails
                    {
                        BillId = bill.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Total = item.Quantity * item.Price

                    };

                    _context.BillDetail.Add(billDetail);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Json(new { success = true, billId = bill.Id });
            }

            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Json(new { success = false, message = ex.Message });
            }
        }
    









        [HttpGet]
        public async Task<IActionResult> PrintBill(int id)
        {

            // Lấy thông tin người dùng hiện tại

            var bill = await _context.Bill
                .Include(b => b.BillDetails)
                .ThenInclude(bd => bd.Product)
                .Include(b => b.Customer)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bill == null)
            {
                return Json(new { success = false, message = "Không tìm thấy thông tin hóa đơn." });
            }

            var memoryStream = new MemoryStream();
            var document = new iTextSharp.text.Document(PageSize.A4, 25, 25, 30, 30);
            var writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            try
            {
                // Đăng ký font chữ
                string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fonts", "OpenSans_SemiCondensed-Light.ttf");
                BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                // Tạo các font styles
                var normalFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.NORMAL);
                var boldFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);
                var italicFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.ITALIC);

                // Thêm tiêu đề
                var title = new Paragraph("HÓA ĐƠN BÁN HÀNG", boldFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);
                document.Add(new Paragraph("\n"));

                // Thông tin hóa đơn
                document.Add(new Paragraph($"Mã hóa đơn: {bill.BillCode}", normalFont));
                document.Add(new Paragraph($"Ngày: {bill.CreateDate:dd/MM/yyyy HH:mm}", normalFont));
                document.Add(new Paragraph($"Thu ngân: {bill.User?.FullName}", normalFont));

                // Thông tin khách hàng
                if (bill.Customer != null)
                {
                    document.Add(new Paragraph($"Khách hàng: {bill.Customer.FullName}", normalFont));
                    document.Add(new Paragraph($"SĐT: {bill.Customer.PhoneNumber}", normalFont));
                }

                document.Add(new Paragraph("----------------------------------------", normalFont));

                // Tạo bảng sản phẩm
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 2f, 1f, 2f, 2f });

                // Header của bảng
                AddTableCell(table, "Sản phẩm", boldFont);
                AddTableCell(table, "Số lượng", boldFont);
                AddTableCell(table, "Đơn giá", boldFont);
                AddTableCell(table, "Thành tiền", boldFont);

                // Thêm dữ liệu sản phẩm
                foreach (var item in bill.BillDetails)
                {
                    AddTableCell(table, item.Product.NameProduct, normalFont);
                    AddTableCell(table, item.Quantity.ToString(), normalFont);
                    AddTableCell(table, item.Price.ToString("N0"), normalFont);
                    AddTableCell(table, (item.Quantity * item.Price).ToString("N0"), normalFont);
                }

                document.Add(table);
                document.Add(new Paragraph("----------------------------------------", normalFont));

                // Thông tin thanh toán
                var paymentTable = new PdfPTable(2);
                paymentTable.WidthPercentage = 100;
                paymentTable.SetWidths(new float[] { 1f, 2f });

                AddPaymentRow(paymentTable, "Tổng tiền:", $"{bill.TotalAmount:N0} VNĐ", boldFont);
                AddPaymentRow(paymentTable, "Tiền khách trả:", $"{bill.PaidAmount:N0} VNĐ", normalFont);
                AddPaymentRow(paymentTable, "Tiền thối:", $"{(bill.PaidAmount - bill.TotalAmount):N0} VNĐ", normalFont);
                AddPaymentRow(paymentTable, "Phương thức thanh toán:", bill.PaymentMethod, normalFont);

                document.Add(paymentTable);



                // Số tiền bằng chữ
                document.Add(new Paragraph("\nSố tiền bằng chữ:", normalFont));
                document.Add(new Paragraph(ConvertNumberToWords(bill.TotalAmount), italicFont));

                // Chữ ký
                document.Add(new Paragraph("\n\n"));
                document.Add(new Paragraph("Ký tên (Người bán hàng): ____________________", normalFont));

                // Thêm văn bản đổi trả sản phẩm
                var returnPolicyText = new Paragraph("**Chính sách đổi trả sản phẩm**", boldFont);
                returnPolicyText.Alignment = Element.ALIGN_CENTER;
                returnPolicyText.SpacingBefore = 10; // Khoảng cách trước đoạn văn bản
                document.Add(returnPolicyText);

                var returnPolicyDetails = new Paragraph(
                    "1. Quý khách có thể đổi trả sản phẩm trong vòng 7 ngày kể từ ngày mua hàng.\n" +
                    "2. Sản phẩm chỉ được đổi trả khi còn nguyên tem, hộp và chưa qua sử dụng.\n" +
                    "3. Vui lòng mang hóa đơn và sản phẩm đến cửa hàng để được hỗ trợ.\n" +
                    "4. Một số sản phẩm nằm ngoài phạm vi đổi trả theo chính sách, vui lòng tham khảo tại quầy.",
                    normalFont
                );
                returnPolicyDetails.SpacingBefore = 5; // Khoảng cách trước đoạn văn bản chi tiết
                returnPolicyDetails.SpacingAfter = 10; // Khoảng cách sau đoạn văn bản chi tiết
                document.Add(returnPolicyDetails);


                var thankYouText = new Paragraph("CẢM ƠN QUÝ KHÁCH VÀ HẸN GẶP LẠI", boldFont);
                thankYouText.Alignment = Element.ALIGN_CENTER;
                thankYouText.SpacingBefore = 20; // Khoảng cách trước đoạn văn bản cảm ơn
                thankYouText.SpacingAfter = 20; // Khoảng cách sau đoạn văn bản cảm ơn
                document.Add(thankYouText);

                document.Close();
                return Json(new { success = true, pdf = Convert.ToBase64String(memoryStream.ToArray()) });
            }
            catch (Exception ex)
            {
                document.Close();
                return Json(new { success = false, message = $"Lỗi khi tạo PDF: {ex.Message}" });
            }
        }

        private void AddTableCell(PdfPTable table, string text, iTextSharp.text.Font font)
        {
            var cell = new PdfPCell(new Phrase(text, font));
            cell.Padding = 5;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(cell);
        }

        private void AddPaymentRow(PdfPTable table, string label, string value, iTextSharp.text.Font font)
        {
            table.AddCell(new PdfPCell(new Phrase(label, font)) { Border = 0, PaddingBottom = 5 });
            table.AddCell(new PdfPCell(new Phrase(value, font)) { Border = 0, PaddingBottom = 5 });
        }


        private string ConvertNumberToWords(decimal number)
        {
            // Các mảng lưu trữ các đơn vị
            string[] ones = { "", "Một", "Hai", "Ba", "Bốn", "Năm", "Sáu", "Bảy", "Tám", "Chín" };
            string[] tens = { "", "Mười", "Hai Mươi", "Ba Mươi", "Bốn Mươi", "Năm Mươi", "Sáu Mươi", "Bảy Mươi", "Tám Mươi", "Chín Mươi" };
            string[] hundreds = { "", "Một Trăm", "Hai Trăm", "Ba Trăm", "Bốn Trăm", "Năm Trăm", "Sáu Trăm", "Bảy Trăm", "Tám Trăm", "Chín Trăm" };

            string[] units = { "", "Nghìn", "Triệu", "Tỷ", "Nghìn Tỷ" };  // Các đơn vị lớn hơn

            long integerPart = (long)number;
            string result = "";

            if (integerPart == 0)
            {
                return "Không Đồng";
            }

            int unitIndex = 0;  // Chỉ số đơn vị
            while (integerPart > 0)
            {
                int part = (int)(integerPart % 1000);  // Lấy 3 chữ số cuối
                if (part > 0)
                {
                    result = ConvertPartToWords(part, ones, tens, hundreds) + " " + units[unitIndex] + " " + result;
                }
                integerPart /= 1000;  // Chia 1000 để xử lý phần tiếp theo
                unitIndex++;
            }

            result = result.Trim();

            // Thêm "đồng" ở cuối
            result += " Đồng";

            return result;
        }

        private string ConvertPartToWords(int part, string[] ones, string[] tens, string[] hundreds)
        {
            string result = "";

            int hundred = part / 100;
            int ten = (part % 100) / 10;
            int one = part % 10;

            if (hundred > 0)
            {
                result += hundreds[hundred] + " ";
            }
            if (ten > 0)
            {
                result += tens[ten] + " ";
            }
            if (one > 0)
            {
                result += ones[one];
            }

            return result.Trim();
        }






        private string GenerateBillCode()
        {
            // Đảm bảo phương thức này trả về một giá trị hợp lệ
            return Guid.NewGuid().ToString(); // Hoặc một cách thức tạo mã hóa đơn phù hợp
        }



    }


}




