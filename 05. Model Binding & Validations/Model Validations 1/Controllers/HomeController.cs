using Microsoft.AspNetCore.Mvc;
using Model_Validations_1.Models;

namespace Model_Validations_1.Controllers
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
                // OR
                // string errorsList = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage));
                // ModelState.Values.SelectMany(value => value.Errors).Select(err =>  err.ErrorMessage).ToList();

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
