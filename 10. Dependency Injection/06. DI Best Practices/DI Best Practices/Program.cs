using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


#region RegisteringService 

//builder.Services.Add(new ServiceDescriptor(
//    typeof(ICitiesService),
//    typeof(CitiesService),
//    ServiceLifetime.Scoped
//));

// OR

builder.Services.AddScoped<ICitiesService, CitiesService>();
// builder.Services.AddTransient<ICitiesService, CitiesService>();
// builder.Services.AddSingleton<ICitiesService, CitiesService>();

#endregion


#region BestPracticesOfDependencyInjection

/*
 * Watch Video 128 Udemy , Asp.Net Core 8 (.NET 8) | True Ultimate Guide
 */

#endregion


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
