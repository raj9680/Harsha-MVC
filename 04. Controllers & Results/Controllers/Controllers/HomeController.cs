using Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public ContentResult Index()
        {
            //return new ContentResult() 
            //{ 
            //    Content = "Hello from Index", 
            //    ContentType = "text/plain", 
            //    StatusCode = 200
            //};

            //return Content("Hello from Index", "text/plain"); // to use this Content() method, base class Controller is must to inherit

            return Content("<h1>Welcome </h1> <h2>Hello From Index</h2>", "text/html");
        }


        [Route("person")]
        public JsonResult Person()
        {
            Person person = new Person() { Id = Guid.NewGuid(), FirstName = "Raj", LastName = "Kumar", Age = 24 };
            return new JsonResult(person);
            // OR return Json(person);
        }


        [Route("contact/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact(long mobile)
        {
            return $"Hello World from Contact {mobile}";
        }

        
        [Route("file-download")]
        public VirtualFileResult FileDownload() // if file is under wwwroot folder
        {
            return new VirtualFileResult("/linq.pdf", "application/pdf");
            // OR 
            // return File(path, "application/pdf");
        }


        [Route("file-download-2")]
        public PhysicalFileResult FileDownload2() // if file is outside wwwroot folder
        {
            var path = "C:\\Users\\raj.kumar\\Desktop\\HARSHA-MVC\\Controllers & Results\\Controllers\\OutsideWWWROOT\\linq.pdf";

            return new PhysicalFileResult(path, "application/pdf");
            // OR 
            // return File(path, "application/pdf");
        }


        [Route("file-download-3")]
        public FileContentResult FileDownload3() // if file is in form bytearray i.e from DB
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\\Users\\raj.kumar\\Desktop\\HARSHA-MVC\\Controllers & Results\\Controllers\\OutsideWWWROOT\\linq.pdf");

            return new FileContentResult(bytes, "application/pdf");
            // OR 
            // return File(bytes, "application/pdf");
        }


        [Route("file-download-4")]
        public FileContentResult FileDownload4() // For all three
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\\Users\\raj.kumar\\Desktop\\HARSHA-MVC\\Controllers & Results\\Controllers\\OutsideWWWROOT\\linq.pdf");

            return File(bytes, "application/pdf");
            // OR 
            // return File(bytes, "application/pdf");
        }

    }
}
