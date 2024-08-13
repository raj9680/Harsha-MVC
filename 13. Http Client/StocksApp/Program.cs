using Dependency_Injection.Modals;
using Dependency_Injection.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// HttpClient
builder.Services.AddHttpClient();

builder.Services.AddScoped<FinHubService, FinHubService>();

// Configure Class & Map json conf. for options pattern
builder.Services.Configure<StocksEntity>(builder.Configuration.GetSection("TradingOptions"));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();


app.Run();
