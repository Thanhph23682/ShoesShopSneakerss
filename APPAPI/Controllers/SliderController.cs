using APPAPI.Services;
using APPDATA.DB;
using APPDATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APPAPI.Controllers
{
 [Route("api/[controller]")]
 [ApiController]
 public class SliderController : ControllerBase
 {
  ShopDbContext _context = new ShopDbContext();
  private readonly CRUDapi<Slider> _crud;

  public SliderController()
  {
   _crud = new CRUDapi<Slider>(_context, _context.Sliders);
  }
  [Route("GetById")]
  [HttpGet]
  public ActionResult<Slider> GetById(int id)
  {
   var slider = _crud.GetAllItems().FirstOrDefault(p => p.SliderID == id);
   if (slider == null)
   {
    return NotFound();
   }
   return Ok(slider);
  }
  [HttpGet]
  public IEnumerable<Slider> GetAll()
  {
   return _crud.GetAllItems().ToList();
  }
  [Route("Create")]
  [HttpPost]
  public bool Create(Slider obj)
  {
   return _crud.CreateItem(obj);
  }
  [Route("Update")]
  [HttpPut]
  public bool Update(Slider obj)
  {

   Slider item = _crud.GetAllItems().FirstOrDefault(p => p.SliderID == obj.SliderID);
   if (item != null)
   {
    item.SliderImage = obj.SliderImage;
    item.SliderStatus = obj.SliderStatus;
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
   Slider item = _crud.GetAllItems().FirstOrDefault(p => p.SliderID == id);

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
