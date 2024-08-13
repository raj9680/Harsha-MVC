﻿using Microsoft.AspNetCore.Mvc;
using StronglyTypedViews.Models;

namespace StronglyTypedViews.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<PersonModel> people = new List<PersonModel>();
        public HomeController()
        {
            this.people.Add(new PersonModel() { Name = "Shivam", DateOfBirth = Convert.ToDateTime("16-04-2000"), PersonGender = Gender.Male });
            this.people.Add(new PersonModel() { Name = "Shivani", DateOfBirth = Convert.ToDateTime("16-04-2002"), PersonGender = Gender.Female });
            this.people.Add(new PersonModel() { Name = "Raj Kumar", DateOfBirth = Convert.ToDateTime("16-04-1998"), PersonGender = Gender.Male });
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
    }
}
