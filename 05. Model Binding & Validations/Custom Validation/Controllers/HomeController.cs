using Custom_Validation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Custom_Validation.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("Hello World", "text/plain");
        }


        [Route("data")]
        public IActionResult Index1(PersonModel person)
        {
            if (!ModelState.IsValid)
            {
                List<string> errorsList = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errorsList.Add(error.ErrorMessage);
                    }
                }
                var errors = string.Join("\n", errorsList);
                return BadRequest(errors);
            }
            return Content($"{person}", "text/plain");
        }

    }
}
