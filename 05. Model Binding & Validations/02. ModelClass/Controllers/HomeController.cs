using Microsoft.AspNetCore.Mvc;
using ModelClass.Models;

namespace ModelClass.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore")]
        public IActionResult Index(BookModel bookModel)
        {
            return Json(bookModel);
        }
    }
}
