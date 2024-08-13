using Microsoft.AspNetCore.Mvc;

namespace ViewDataInLayoutViews.Controllers
{
    public class ProductsController : Controller
    {
        [Route("products")]
        public IActionResult All()
        {
            return View();
        }
    }
}
