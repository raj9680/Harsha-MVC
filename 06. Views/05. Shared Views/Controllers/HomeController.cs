using Microsoft.AspNetCore.Mvc;
using Shared_Views.Models;
using Shared_Views.Models.Wrappers;

namespace Shared_Views.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<PersonModel> people = new List<PersonModel>();
        private readonly List<Product> product = new List<Product>();
        public HomeController()
        {
            this.people = new List<PersonModel>() {
                new PersonModel() { Name = "Shivam", DateOfBirth = Convert.ToDateTime("16-04-2000"), PersonGender = Gender.Male },
                new PersonModel() { Name = "Shivani", DateOfBirth = Convert.ToDateTime("16-04-2002"), PersonGender = Gender.Female },
                new PersonModel() { Name = "Raj Kumar", DateOfBirth = Convert.ToDateTime("16-04-1998"), PersonGender = Gender.Male }
            };

            this.product = new List<Product>() {
                new Product() { ProductId = 1, ProductName="Product 1" },
                new Product() { ProductId = 1, ProductName="Product 2" },
                new Product() { ProductId = 1, ProductName="Product 3" },
            };
        }


        [Route("/")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "Asp.Net Core Demo App";

            return View(this.people);
        }


        [Route("person/{name}")]
        public IActionResult person(string name)
        {
            PersonModel? person = this.people.Where(people => people.Name == name).FirstOrDefault();

            return View(person);
        }


        [Route("person-with-product")]
        public IActionResult PersonWithProduct()
        {
            PersonAndProductWrapperModel personAndProductWrapperModel = new PersonAndProductWrapperModel() { PersonData = this.people, ProductData = this.product };
            return View(personAndProductWrapperModel);
        }
    }
}
