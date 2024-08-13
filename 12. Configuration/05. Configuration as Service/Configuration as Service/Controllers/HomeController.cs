using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        // Step 2, step 1 in program.cs

        //Instead of injecting IConfiguration, we can use IOptions
        //private readonly IConfiguration _configuration;

        private readonly WeatherApiOptionsPattern _options;

        public HomeController( //IConfiguration configuration,
        IOptions<WeatherApiOptionsPattern> weatherApiOptionsPattern)
        {
            _options = weatherApiOptionsPattern.Value;
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

            ViewBag.ClientID = _options.ClientID;
            ViewBag.ClientSecret = _options.ClientSecret;

            return View(_cities);
        }
    }
}
