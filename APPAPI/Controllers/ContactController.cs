using APPAPI.Services;
using APPDATA.DB;
using APPDATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace APPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        ShopDbContext _context = new ShopDbContext();
        private readonly CRUDapi<Contact> _crud;

        public ContactController()
        {

            _crud = new CRUDapi<Contact>(_context, _context.Contacts);
        }
        [Route("GetById")]
        [HttpGet]
        public ActionResult<Contact> GetById(int id)
        {
            var contact = _crud.GetAllItems().FirstOrDefault(p => p.ContactID == id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }



        [HttpGet]
        public IEnumerable<Contact> GetAll()
        {
            return _crud.GetAllItems().ToList();
        }
        [Route("Create")]
        [HttpPost]
        public bool Create(Contact obj)
        {

            return _crud.CreateItem(obj);
        }
        [Route("Update")]
        [HttpPut]
        public bool Update(Contact obj)
        {

            Contact item = _crud.GetAllItems().FirstOrDefault(p => p.ContactID == obj.ContactID);



            if (item != null)
            {
                item.ContactName = obj.ContactName;
                item.ContactDescription = obj.ContactDescription;
                item.ContactImageURL = obj.ContactImageURL;

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
            Contact item = _crud.GetAllItems().FirstOrDefault(p => p.ContactID == id);

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


