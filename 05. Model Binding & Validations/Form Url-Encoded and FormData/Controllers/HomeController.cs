using Form_Url_Encoded_and_FormData.Models;
using Microsoft.AspNetCore.Mvc;

namespace Form_Url_Encoded_and_FormData.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("Hello World", "text/plain");
        }


        [Route("bookstore/{bookid?}")]  // pass value as route & route data i.e form data 
        /* 
         * Form url-encoded-data
         * Pass data as form-url-encoded in postman. It is mapped automatically to model.
         * Also the form data has more priority than route data
         */
        public IActionResult Index(int? bookid, BookModel bookModel)
        {
            return Content($"{bookModel}");
        }
    }
}
