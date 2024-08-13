using Microsoft.AspNetCore.Mvc;

namespace Model_Binding___Validations.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("This is my home page", "text/plain");
        }


        [Route("bookstore")]
        // [Route("bookstore")] - query params
        // Url: /bookstore?id=10

        // [Route("bookstore/{id?}")] - route data
        // RouteUrl: /bookstore/20/

        // Url: /bookstore/20/?id=10  - route prioroty vs query [route has more]
        public IActionResult Index2(int? id)   // nullable int
        {
            if (id.HasValue == false)
            {
                return BadRequest("Book id is not supplied or empty");
            }

            if(id <= 0)
            {
                return BadRequest("Book id cannot not less than or equal to 0");
            }

            return Content($"We found a book with id: {id}", "text/plain");
        }


        /* 
 
           [FromQuery] & [FromRoute] to override the default priority or route behaviour
           
           1). [FromQuery] - gets the value from query string only
           
           public IActionResult ActionMethodName([FromQuery] type parameter){
            
           }

           2). [FromRoute] - gets the value from route parameters only
           
           public IActionResult ActionMethodName([FromRoute] type parameter){
            
           }

        */
    }
}
