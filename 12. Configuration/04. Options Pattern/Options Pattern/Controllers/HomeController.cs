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

            // Options Pattern to load Configuration - using Get 
            //WeatherApiOptionsPattern weatherApi = _configuration.GetSection("weatherapi").Get<WeatherApiOptionsPattern>();

            // OR uncomment either

            // Options Pattern to load Configuration - using Bind
            WeatherApiOptionsPattern weatherApi = new WeatherApiOptionsPattern();
            _configuration.GetSection("WeatherApi").Bind(weatherApi);


            // Difference between Bind & Get
            // Get Loads conf. values into new Options object.
            // Bind Loads conf. values into existing Options object.

            ViewBag.ClientID = weatherApi.ClientID;
            ViewBag.ClientSecret = weatherApi.ClientSecret;

            return View(_cities);
        }
    }
}
