using APPDATA.DB;
using APPDATA.Models;

using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace APPMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("AdminPayments")]
    public class AdminPaymentsController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly INotyfService _NotyfService;
        private readonly IWebHostEnvironment _webHostEnvironment;





        public AdminPaymentsController(ShopDbContext context, INotyfService notyfService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _NotyfService = notyfService;
            _webHostEnvironment = webHostEnvironment;
        }

        

        public async Task<IActionResult> Index()
        {

            //var payments = _context.Payments
            //                          .Select(p => new Payment
            //                          {
            //                              PaymentID = p.PaymentID,
            //                              PaymentMethod = p.PaymentMethod,
            //                              PaymentDescription = p.PaymentDescription,
            //                              PaymentImage = p.PaymentImage // Đảm bảo đây là tên ảnh đã lưu
            //                          })
            //                          .ToList();

            //return View(payments); //
            var payments = await _context.Payments.ToListAsync();
            return View(payments);

        }
        [HttpPost]
        [Route("AddPaymentMethod")]
        public async Task<IActionResult> AddPaymentMethod([FromForm] string paymentMethod, [FromForm] string paymentDesc, [FromForm] IFormFile? paymentImg)
        {
            if (!string.IsNullOrEmpty(paymentMethod) && !string.IsNullOrEmpty(paymentDesc))
            {
                var payment = new Payment
                {
                    PaymentMethod = paymentMethod,
                    PaymentDescription = paymentDesc
                };

                if (paymentImg != null && paymentImg.Length > 0)
                {
                    // Tạo tên file duy nhất
                    string fileName = $"payment_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid()}{Path.GetExtension(paymentImg.FileName)}";

                    // Đường dẫn lưu file
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "payment");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    var filePath = Path.Combine(uploadPath, fileName);

                    // Lưu file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await paymentImg.CopyToAsync(stream);
                    }

                    // Lưu tên file vào database
                    payment.PaymentImage = fileName;
                }

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

              _NotyfService.Success("Thêm phương thức thanh toán thành công!");
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        //[HttpPost]
        //[Route("AddPaymentMethod")]
        //public async Task<IActionResult> AddPaymentMethod([FromForm] string paymentMethod, [FromForm] string paymentDesc, [FromForm] IFormFile? paymentImg)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(paymentMethod) || string.IsNullOrEmpty(paymentDesc))
        //        {
        //            _NotyfService.Error("Please provide all required information.");
        //            return Json(new { success = false, message = "Please provide all required information." });
        //        }

        //        var payment = new Payment
        //        {
        //            PaymentMethod = paymentMethod,
        //            PaymentDescription = paymentDesc
        //        };

        //        if (paymentImg != null && paymentImg.Length > 0)
        //        {
        //            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "payment");
        //            if (!Directory.Exists(uploadsFolder))
        //            {
        //                Directory.CreateDirectory(uploadsFolder);
        //            }

        //            var uniqueFileName = $"payment_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(paymentImg.FileName)}";
        //            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await paymentImg.CopyToAsync(fileStream);
        //            }

        //            payment.PaymentImage = $"/assets/img/payment/{uniqueFileName}";
        //        }

        //        _context.Payments.Add(payment);
        //        await _context.SaveChangesAsync();

        //        _NotyfService.Success("Payment method added successfully!");
        //        return Json(new { success = true, message = "Payment method added successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        _NotyfService.Error($"Error: {ex.Message}");
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("AddPaymentMethod")]
        //public async Task<IActionResult> AddPaymentMethod(IFormFile? paymentImg, string paymentMethod, string paymentDesc)
        //{
        //    //if (string.IsNullOrEmpty(paymentMethod) || string.IsNullOrEmpty(paymentDesc))
        //    //{
        //    //    return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin." });
        //    //}

        //    //string imagePath = null;

        //    //if (paymentImg != null)
        //    //{
        //    //    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        //    //    var fileExtension = Path.GetExtension(paymentImg.FileName).ToLower();

        //    //    if (!allowedExtensions.Contains(fileExtension))
        //    //    {
        //    //        return Json(new { success = false, message = "Chỉ cho phép ảnh định dạng jpg, jpeg hoặc png." });
        //    //    }

        //    //    if (paymentImg.Length > 5 * 1024 * 1024)
        //    //    {
        //    //        return Json(new { success = false, message = "Ảnh vượt quá 5MB." });
        //    //    }

        //    //    var fileName = $"payment_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid()}{fileExtension}";
        //    //    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "payment", fileName);

        //    //    var directoryPath = Path.GetDirectoryName(filePath);
        //    //    if (!Directory.Exists(directoryPath))
        //    //    {
        //    //        Directory.CreateDirectory(directoryPath);
        //    //    }
        //    //    //string DefaultImagePath = HttpContext.Current.Server.MapPath("~/NoImage.jpg");

        //    //    //byte[] imageArray = System.IO.File.ReadAllBytes(DefaultImagePath);
        //    //    //string base64ImageRepresentation = Convert.ToBase64String(imageArray);

        //    //    try
        //    //    {

        //    //        using (var stream = new FileStream(filePath, FileMode.Create))
        //    //        {
        //    //            await paymentImg.CopyToAsync(stream);
        //    //        }
        //    //        imagePath = $"/assets/img/payment/{fileName}";
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        return Json(new { success = false, message = $"Lỗi khi lưu ảnh: {ex.Message}" });
        //    //    }
        //    //}

        //    //var payment = new Payment
        //    //{
        //    //    PaymentMethod = paymentMethod,
        //    //    PaymentDescription = paymentDesc,
        //    //    PaymentImage = imagePath,
        //    //};

        //    //try
        //    //{
        //    //    _context.Payments.Add(payment);
        //    //    await _context.SaveChangesAsync();
        //    //    _NotyfService.Success("Thêm phương thức thanh toán thành công!");
        //    //    return Json(new { success = true });
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return Json(new { success = false, message = $"Lỗi khi lưu dữ liệu: {ex.Message}" });
        //    //}

        //}




        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>

        [Route("DeletePaymentMethod")]
        public IActionResult DeletePaymentMethod(int paymentId)
        {
            Payment payment = _context.Payments.FirstOrDefault(x => x.PaymentID == paymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
                
                // Hiển thị thông báo thành công
              _NotyfService.Success("Xóa phương thức thanh toán thành công!");

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}
