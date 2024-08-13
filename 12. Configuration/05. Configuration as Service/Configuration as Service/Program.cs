using Dependency_Injection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Step 1
// Supply an object of WeatherApiOptionsPattern (with 'WeatherApi' section) as a service
builder.Services.Configure<WeatherApiOptionsPattern>(builder.Configuration.GetSection("WeatherApi"));


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();


app.Run();
