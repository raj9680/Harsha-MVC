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

            ViewBag.ClientId = _configuration["WeatherApi:ClientId"];
            ViewBag.ClientSecret = _configuration.GetValue<string>("WeatherApi:ClientSecret", "Default Value");

            // Hierarchical value using get GetSection

            ViewBag.Section = _configuration.GetSection("weatherapi")["ClientId"];

            // OR Clean Approach
            IConfiguration weatherApi = _configuration.GetSection("weatherapi");
            ViewBag.Sectionn = weatherApi["ClientId"];

            return View(_cities);
        }
    }
}
