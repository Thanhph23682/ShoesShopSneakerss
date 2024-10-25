using Microsoft.AspNetCore.Mvc;

namespace APPMVC.Controllers
{
    public class ProductsController : Controller
    {
        [Route("shop.html", Name = "ShopProduct")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
