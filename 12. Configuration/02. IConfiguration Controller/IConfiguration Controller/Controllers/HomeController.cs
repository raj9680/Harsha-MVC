using Microsoft.AspNetCore.Mvc;

namespace Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<string> _cities = new List<string>()
            {
                "London",
                "Paris",
                "New York",
                "Barbados"
            };

            ViewBag.MyKey = _configuration["MyKey"];
            ViewBag.Default = _configuration.GetValue<int>("KeyNotExist", 30);

            return View(_cities);
        }
    }
}
