using Microsoft.AspNetCore.Mvc;

namespace PartialViews.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["ListTitle"] = "Cities Heading from Controller";
            ViewData["ListItems"] = new List<string>() { "New York", "Rome", "paris", "London", "Tokyo" };
            return View();
        }


        [Route("about")]
        public IActionResult About()
        {
            return View();
        }
    }
}
