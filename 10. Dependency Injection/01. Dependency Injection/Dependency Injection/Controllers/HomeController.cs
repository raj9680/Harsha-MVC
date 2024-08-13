using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICitiesService _citiesService;
        public HomeController(ICitiesService citiesService)
        {
            // create object of ICitiesService/CitiesService
            _citiesService = citiesService;
        }


        [Route("/")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService.GetCities();
            return View(cities);
        }
    }
}
