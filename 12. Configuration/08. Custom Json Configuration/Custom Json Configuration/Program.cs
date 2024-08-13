using Dependency_Injection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.Configure<WeatherApiOptionsPattern>(builder.Configuration.GetSection("WeatherApi"));



// Load MyCutomConfig.json
builder.Host.ConfigureAppConfiguration((hostingContent, config) =>
{
    config.AddJsonFile("MyCustomConfig.json", optional: true, reloadOnChange:true);
}); // enter full path, if file is at different location
// End



var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();


app.Run();
