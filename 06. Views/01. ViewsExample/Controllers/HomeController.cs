using Microsoft.AspNetCore.Mvc;
using ViewsExample.Models;

namespace ViewsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            // return View();
            return new ViewResult() { ViewName = "Index" };
        }


        #region 
        [Route("view")]
        public IActionResult Data()
        {
            ViewData["appTitle"] = "Asp.Net Core Demo App";
            List<PersonModel> people = new List<PersonModel>()
            {
                new PersonModel() { Name="Shivam", DateOfBirth=Convert.ToDateTime("16-04-2000"), PersonGender=Gender.Male },
                new PersonModel() { Name="Shivani", DateOfBirth=Convert.ToDateTime("16-04-2002"), PersonGender=Gender.Female },
                new PersonModel() { Name="Raj", DateOfBirth=Convert.ToDateTime("16-04-1998"), PersonGender=Gender.Male }
            };

            ViewData["people"] = people;
            return View();
        }
        #endregion


    }
}
