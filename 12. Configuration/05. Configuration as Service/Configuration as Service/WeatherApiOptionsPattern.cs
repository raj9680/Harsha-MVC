namespace Dependency_Injection
{
    public class WeatherApiOptionsPattern
    {
        // Prop name should ne same as key in appsettings.json
        public string? ClientID { get; set; }
        public string? ClientSecret { get; set; }
    }
}
