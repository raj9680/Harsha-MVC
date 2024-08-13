using Microsoft.AspNetCore.Mvc;
using StronglyTypedPartialViews.Models;

namespace PartialViews.Controllers
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

        [Route("programming-languages")]
        public IActionResult ProgrammingLanguage()
        {
            ListModel listModel = new ListModel()
            {
                ListTitle = "Programming Languages",
                ListItems = new List<string>()
                {
                    "Python",
                    "Php",
                    "C-Sharp",
                    "Java"
                }
            };
            return PartialView("_List", listModel);
            // OR
            //return new PartialViewResult() { ViewName = "_List", Model=listModel}
        }
    }
}
