using Dependency_Injection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.Configure<WeatherApiOptionsPattern>(builder.Configuration.GetSection("WeatherApi"));


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();


app.Run();
