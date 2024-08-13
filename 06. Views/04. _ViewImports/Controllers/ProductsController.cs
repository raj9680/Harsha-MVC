using Microsoft.AspNetCore.Mvc;

namespace _ViewImports.Controllers
{
    public class ProductsController : Controller
    {
        [Route("products/all")]
        public IActionResult All()
        {
            return View();
        }
    }
}
