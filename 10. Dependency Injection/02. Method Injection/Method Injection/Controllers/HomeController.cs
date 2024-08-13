using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        /*
        private readonly ICitiesService _citiesService;
        public HomeController(ICitiesService citiesService)
        {
            // create object of ICitiesService/CitiesService
            _citiesService = citiesService;
        }
        */

        // Object Creation of Service class using Method Injection
        // Service object will be created only for this method instead of whole class
        [Route("/")]
        public IActionResult Index([FromServices] ICitiesService _citiesService)
        {
            List<string> cities = _citiesService.GetCities();
            return View(cities);
        }
    }
}
