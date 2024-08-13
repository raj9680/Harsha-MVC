using Autofac;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace Dependency_Injection.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ICitiesService _citiesService1;
        private readonly ICitiesService _citiesService2;
        private readonly ICitiesService _citiesService3;

        // - Below is the way to get ServiceFactoryScope of default DI/IOC container
        // private readonly IServiceScopeFactory _serviceScopeFactory; 

        // For AutoFac we use below method & inject the same in constructor and scoped using below as well
        private readonly ILifetimeScope _serviceScopeRepositoryFromAutoFac;


        public HomeController(ICitiesService citiesService1, ICitiesService citiesService2, ICitiesService citiesService3, ILifetimeScope serviceScopeRepositoryFromAutoFac)
        {
            _citiesService1 = citiesService1;
            _citiesService2 = citiesService2;
            _citiesService3 = citiesService3;
            _serviceScopeRepositoryFromAutoFac = serviceScopeRepositoryFromAutoFac;
        }
        

        [Route("/")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService1.GetCities();

            // We can use the autofac to call other services also
            ViewBag.InstanceId_CitiesService_1 = _citiesService1.ServiceInstanceId;
            ViewBag.InstanceId_CitiesService_2 = _citiesService2.ServiceInstanceId;
            ViewBag.InstanceId_CitiesService_3 = _citiesService3.ServiceInstanceId;

            // We can use the autofac to call other services also it wirks like default DI container but with more features
            using (ILifetimeScope scope = _serviceScopeRepositoryFromAutoFac.BeginLifetimeScope())
            {
                ICitiesService citiesService = scope.Resolve<ICitiesService>();

                // DB Work
                ViewBag.InstanceIdScope = citiesService.ServiceInstanceId;
            } // End of scope; it calls CitiesService.Dispose automatically
            
            return View(cities);
        }
    }
}
