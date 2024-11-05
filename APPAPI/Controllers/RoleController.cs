using APPAPI.Services;
using APPDATA.DB;
using APPDATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APPAPI.Controllers
{
 [Route("api/[controller]")]
 [ApiController]
 public class RoleController : ControllerBase
 {
  ShopDbContext _context = new ShopDbContext();
   private readonly CRUDapi<Role> _crud;
  

  

 
  public RoleController()
  {
   _crud = new CRUDapi<Role>(_context, _context.Roles);
  }

  [Route("GetById")]
  [HttpGet]
  public ActionResult<Role> GetById(int id)
  {
   var role = _crud.GetAllItems().FirstOrDefault(p => p.ID == id);
   if (role == null)
   {
    return NotFound();
   }
   return Ok(role);
  }
  [HttpGet]
  public IEnumerable<Role> GetAll() 
  {
   return _crud.GetAllItems().ToList();
  }
  [Route("Create")]
  [HttpPost] 
  public bool Create (Role obj)
  {
   return _crud.CreateItem(obj);
  }
  [Route("Update")]
  [HttpPut]
  public bool Update(Role obj)
  {
   Role item = _crud.GetAllItems().FirstOrDefault(p => p.ID == obj.ID);

   if (item != null)
   {
    
    item.RoleName = obj.RoleName;
    item.Description = obj.Description;

    
    return _crud.UpdateItem(item);
   }
   else
   {
   
    return false; 
   }
  }

  [Route("Delete")]
  [HttpDelete]
  public bool Delete(int id)
  {
   Role item = _crud.GetAllItems().FirstOrDefault(p => p.ID == id);

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
