using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<CityModel> _city;
        public HomeController()
        {
            this._city = 
            new List<CityModel>() 
            {
                new CityModel() { CityName = "London", CityUniqueCode = 102, DateAndTime = Convert.ToDateTime("2030-01-01 8:00"), TemperatureFahrenheit = "33" },
                new CityModel() { CityName = "New York", CityUniqueCode = 213, DateAndTime = Convert.ToDateTime("2030-01-01 3:00"), TemperatureFahrenheit = "60" },
                new CityModel() { CityName = "Paris", CityUniqueCode = 312, DateAndTime = Convert.ToDateTime("2030-01-01 9:00"), TemperatureFahrenheit = "82" }
            }; 
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View(_city);
        }


        [Route("weather/{cityCode}")]
        public IActionResult WeatherDetails(int cityCode)
        {
            CityModel? data = _city.Where(x => x.CityUniqueCode == cityCode).FirstOrDefault();
            return View(data);
        }


        [Route("weather-partial/{cityCode}")]
        public IActionResult WeatherDetailPartial(int cityCode)
        {
            CityModel? data = _city.Where(x => x.CityUniqueCode == cityCode).FirstOrDefault();

            return PartialView("_PartialDetail", data);
        }
    }
}
