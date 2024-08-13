using FromHeader.Models;
using Microsoft.AspNetCore.Mvc;

namespace FromHeader.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("Hello World", "text/plain");
        }


        /*
        
        old apprach of reading headers:  'ControllerContext.HttpContext.Request.Headers["key"]'

        new approach of reading headers: 
          '[FromHeader(Name = "User-Agent")] string UserAgent' in action method param
         
         */
        [Route("data")]
        public IActionResult Index1(PersonModel person, [FromHeader(Name = "User-Agent")] string UserAgent)
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
            return Content($"{person}, {UserAgent}", "text/plain");
        }


        // Ecom Application
        [Route("order")]
        public IActionResult Order(OrderModel order)
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

            //generate a random order number between 1 and 99999
            Random random = new Random();
            int randomOrderNumber = random.Next(1, 99999);

            //return HTTP 200 response with order number
            return Json(new { orderNumber = randomOrderNumber });
        }
    }
}
