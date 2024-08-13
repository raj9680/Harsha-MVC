using Microsoft.AspNetCore.Mvc;

namespace RedirectResults.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore")]
        public IActionResult Index()
        {
            //return new RedirectToActionResult("Index2", "Home", new { }, true); - 302 Found
            return new RedirectToActionResult("Index2", "Home", new { }, permanent: true);   // 301 Moved permanently
            
        }

        [Route("store/books")]
        public IActionResult Index2()
        {
            return Content("<h1>Books</h1>", "text/html");
        }

        [Route("store/cars")]
        public IActionResult Index3()
        {
            return Content("<h1>cars</h1>", "text/html");
        }


        [Route("laptops/{id}")]
        public IActionResult Index4(int id)
        {
            return RedirectToActionPermanent("Index5", "Home", new { id = id });
            // return new LocalRedirectResult($"store/laptops/{id}"); // url of same application OR
            // return LocalRedirect("local_url");  // 302 - Found results for both

            // return new LocalRedirectResult($"store/laptops/{id}", true); // url of same application OR
            // return LocalRedirectPermanent("local_url");  // 301 - moved permanent

            // return Redirect($"store/books/{id}");
            // return RedirectPermanent($"store/books/{id}"); // 301 - moved permanent
        }


        [Route("store/laptops/{id}")]
        public IActionResult Index5()
        {
            var bookId = Convert.ToInt16(Request.RouteValues["id"]);
            return Content($"This is Laptop {bookId}");
        }
    }
}
