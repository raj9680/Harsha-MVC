namespace WeatherApp.Models
{
    public class CityModel
    {
        public int CityUniqueCode { get; set; }
        public string? CityName { get; set; }
        public DateTime DateAndTime { get; set; }
        public string? TemperatureFahrenheit { get; set; }
    }
}
