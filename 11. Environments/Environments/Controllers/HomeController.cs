using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICitiesService _citiesService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ICitiesService citiesService, IWebHostEnvironment webHostEnvironment)
        {
            _citiesService = citiesService;
            _webHostEnvironment = webHostEnvironment;
        }
        

        [Route("/")]
        [Route("route-to-cause-error-intentionally")]
        public IActionResult Index()
        {
            // Get env details in controller
            ViewBag.Environment = _webHostEnvironment.EnvironmentName;
            List<string> cities = _citiesService.GetCities();
            return View(cities);
        }


        [Route("route-to-cause-error-intentionally")]
        public IActionResult Index2()
        {
            return View();
        }
    }
}
