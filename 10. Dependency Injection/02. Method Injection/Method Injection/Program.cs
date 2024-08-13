using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registering Service to IOC/DI Container
builder.Services.Add(new ServiceDescriptor(
    typeof(ICitiesService),
    typeof(CitiesService),
    ServiceLifetime.Transient
));

/* Transient (per injection): New service object will be created everytime, when the service is injected. Ex: We have injected the same transient service in to three different controller or may be in three different services. So everytime when the request is received, a new object created for the service class
 * 
 * Scoped: For every scope a new service request will be created and remains re-used until the request is finished. New object will be created for new request and hence it also remians re-used until the proces completes.
 * 
 * Singleton: Object will remian same re-used throughout the application until the application is shutdown.
 */



var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
