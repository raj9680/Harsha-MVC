using Microsoft.AspNetCore.Mvc;
using ViewComponentsWithViewData.Models;

namespace ViewComponentsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("about")]
        public IActionResult About()
        {
            return View();
        }
        
        
        [Route("friends-list")]
        public IActionResult FriendsResult()
        {
            PersonGridModel personModel = new PersonGridModel()
            {
                GridTitle = "Persons List",
                Persons = new List<PersonModel>()
                {
                    new PersonModel { JobTitle = "QA", PersonName = "Adams" },
                    new PersonModel { JobTitle = "Analyst", PersonName = "Della" },
                    new PersonModel { JobTitle = "Developer", PersonName = "Bone" }
                }
            };
            return ViewComponent("Grid", new { gridParam = personModel });
        }
    }
}
