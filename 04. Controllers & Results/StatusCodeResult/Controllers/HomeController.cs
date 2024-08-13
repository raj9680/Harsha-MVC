using Microsoft.AspNetCore.Mvc;

namespace StatusCodeResult.Controllers
{
    public class HomeController : Controller
    {
        [Route("book")]
        public IActionResult Index()
        {
            // bookid should be applied
            if (!Request.Query.ContainsKey("bookid"))
            {
                //Response.StatusCode = 400;
                //return Content("Book id not suppplied");

                //return new BadRequestResult();  // OR
                return BadRequest("Book id is not supplied");
            }


            // bookid cannot be emptied
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                //Response.StatusCode = 400;
                //return Content("Book id cannot be emptied");

                //return new BadRequestResult();  // OR
                return BadRequest("Book id cannot be emptied");
            }


            // bookid id should be between 1 to 1000
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                //Response.StatusCode = 400;
                //return Content("Book id cannot be emptied");

                return BadRequest("Book id cannot be emptied");
            }


            var bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookid"]);
            if (bookId <= 0)
                // return Content("Book id can't be less than or equal 0");
                return NotFound("Book not found");

            if (bookId > 1000)
                // return Content("Book id can't be greater than 1000");
                return NotFound("Book not found");

            // is loggedIn should be true
            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                //Response.StatusCode = 401;
                //return Content("LoggedIn should be true");
                return Unauthorized("You are not authorized");
            }

            return File("/LINQ.pdf", "application/pdf");
        }
    }
}
