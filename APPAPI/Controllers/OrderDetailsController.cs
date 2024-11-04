using APPAPI.Services;
using APPDATA.DB;
using APPDATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        ShopDbContext _context = new ShopDbContext();
        private readonly CRUDapi<OrderDetail> _crud;
        public OrderDetailsController()
        {
            _crud = new CRUDapi<OrderDetail>(_context, _context.OrderDetails);
        }
        [HttpGet]
        public IEnumerable<OrderDetail> GetAll()
        {
            return _crud.GetAllItems().ToList();
        }
        [Route("Create")]
        [HttpPost]
        public bool Create(OrderDetail obj)
        {
            return _crud.CreateItem(obj);
        }
        [Route("Update")]
        [HttpPut]
        public bool Update(OrderDetail obj)
        {

            OrderDetail item = _crud.GetAllItems().FirstOrDefault(p => p.Id == obj.Id);
            if (item != null)
            {
                item.orderId = obj.orderId;
                item.quantity = obj.quantity;
                item.amount_Price = obj.amount_Price;


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
            OrderDetail item = _crud.GetAllItems().FirstOrDefault(p => p.Id == id);

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

