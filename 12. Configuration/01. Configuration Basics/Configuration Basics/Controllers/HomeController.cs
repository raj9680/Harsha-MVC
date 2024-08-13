using Autofac;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ICitiesService _citiesService;

        public HomeController(ICitiesService citiesService1)
        {
            _citiesService = citiesService1;
        }
        

        [Route("/")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService.GetCities();
            return View(cities);
        }
    }
}
