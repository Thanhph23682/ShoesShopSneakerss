using APPAPI.Services;
using APPDATA.DB;
using APPDATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        ShopDbContext _context = new ShopDbContext();
        private readonly CRUDapi<Order> _crud;
        public OrderController()
        {
            _crud = new CRUDapi<Order>(_context, _context.Orders);
        }
        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            return _crud.GetAllItems().ToList();
        }
        [Route("Create")]
        [HttpPost]
        public bool Create( Order obj)
        {
            return _crud.CreateItem(obj);
        }
        [Route("Update")]
        [HttpPut]
        public bool Update(Order obj)
        {

            Order item = _crud.GetAllItems().FirstOrDefault(p => p.Id == obj.Id);
            if (item != null)
            {
                item.UserId = obj.UserId;
                item.CustomerId = obj.CustomerId;
                item.payment_Status = obj.payment_Status;
                item.order_Status = obj.order_Status;
                item.order_Date = obj.order_Date;
                item.update_Date = DateTime.UtcNow; // Cập nhật thời gian hiện tại
                item.total_Amount = obj.total_Amount;
                item.order_Desc = obj.order_Desc;
                item.order_Phone = obj.order_Phone;
                item.order_Name = obj.order_Name;
                item.order_Address = obj.order_Address;



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
            Order item = _crud.GetAllItems().FirstOrDefault(p => p.Id == id);

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

