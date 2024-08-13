using IValidatable_Object.Models;
using Microsoft.AspNetCore.Mvc;

namespace IValidatable_Object.Controllers
{
    public class FromBodyController : Controller
    {
        [Route("frombody")]
        public IActionResult Index1([FromBody] PersonModel person)
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
