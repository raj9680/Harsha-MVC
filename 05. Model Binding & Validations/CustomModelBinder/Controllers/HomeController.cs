using CustomModelBinder.CustomModelBinders;
using CustomModelBinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomModelBinder.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("This is my home page", "text/plain");
        }


        [Route("data")]
        public IActionResult Index1([FromBody] [ModelBinder(BinderType = typeof(PersonModelBinder))] PersonModel person)
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


        [Route("binder-provider")]
        public IActionResult Index2(PersonModel person)
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
