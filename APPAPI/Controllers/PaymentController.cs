using APPAPI.Services;
using APPDATA.DB;
using APPDATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        ShopDbContext _context = new ShopDbContext();
        private readonly CRUDapi<Payment> _crud;
        public PaymentController()
        {
            _crud = new CRUDapi<Payment>(_context, _context.Payments);
        }
        [HttpGet]
        public IEnumerable<Payment> GetAll()
        {
            return _crud.GetAllItems().ToList();
        }
        [Route("Create")]
        [HttpPost]
        public bool Create(Payment obj)
        {
            return _crud.CreateItem(obj);
        }
        [Route("Update")]
        [HttpPut]
        public bool Update(Payment obj)
        {

            Payment item = _crud.GetAllItems().FirstOrDefault(p => p.PaymentID == obj.PaymentID);
            if (item != null)
            {
                item.PaymentMethod = obj.PaymentMethod;
                item.PaymentDescription = obj.PaymentDescription;
                item.PaymentImage = obj.PaymentImage;

                return _crud.UpdateItem(item);
            }
            else
            {
                return false;
            }


        }
        [Route("Delete")]
        [HttpDelete]
        public bool Delete(int? id)
        {
            Payment item = _crud.GetAllItems().FirstOrDefault(p => p.PaymentID == id);

            if (item != null)
            {
                // Nếu tìm thấy, thực hiện xóa
                return _crud.DeleteItem(item);
            }
            else
            {
                // Nếu không tìm thấy, trả về false
                return false;
            }
        }
    }
}

