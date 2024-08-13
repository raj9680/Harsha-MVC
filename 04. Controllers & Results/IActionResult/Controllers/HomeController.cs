using Microsoft.AspNetCore.Mvc;

namespace IActionResults.Controllers
{
    public class HomeController : Controller
    {
        [Route("book")]
        public IActionResult Index()
        {
            // bookid should be applied
            if (!Request.Query.ContainsKey("bookid"))
            {
                Response.StatusCode = 400;
                return Content("Book id not suppplied");
            }


            // bookid cannot be emptied
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                Response.StatusCode = 400;
                return Content("Book id cannot be emptied");
            }    


            // bookid id should be between 1 to 1000
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                Response.StatusCode = 400;
                return Content("Book id cannot be emptied");
            }


            var bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookid"]);
            if (bookId <= 0)
                return Content("Book id can't be less than or equal 0");
            if (bookId > 1000)
                return Content("Book id can't be greater than 1000");

            // is loggedIn should be true
            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                Response.StatusCode = 401;
                return Content("LoggedIn should be true");
            }

            return File("/LINQ.pdf", "application/pdf");
        }
    }
}
