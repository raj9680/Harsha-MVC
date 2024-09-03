using Service;
using ServiceContracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Contacts_Manager.Filters.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Adding controllers and Views support/ And Gobal filters
builder.Services.AddControllersWithViews(options=>
{
    // Getting service instance of ILogger/AnyService to use globally
    var logger = builder.Services.BuildServiceProvider()
    .GetRequiredService<ILogger<ResponseHeaderActionFilter>>();

    // Registering ActionFilter without Parameters
    // options.Filters.Add<ResponseHeaderActionFilter>();

    //options.Filters.Add<ResponseHeaderActionFilter>(2); - with order parameter as 2, this approach is good only Filter Class without parameter


    // Registering ActionFilter with Parameters
    options.Filters.Add(new ResponseHeaderActionFilter(logger, "My-Key-From-Global", "My-Value-From-Global", 2));
});

// Logging
//builder.Host.ConfigureLogging(loggingProviders =>
//{
//    loggingProviders.ClearProviders(); // all logging providers off
//    loggingProviders.AddConsole(); // only logging enables for console
//                                   // for enabling debug & eventLog
//    loggingProviders.AddDebug();
//    loggingProviders.AddEventLog();
//});

// Configuring Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration)// Reading conf. from appsettings.json
    .ReadFrom.Services(services); 
    // read out current app's and services & make them available to SeriLog.
});

// add Services to IOC container
// Singleton: Instantiate one time for all the application
builder.Services.AddScoped<ICountryService, CountryService>();
// 
builder.Services.AddScoped<IPersonService, PersonService>();


// DbContext Registeration
builder.Services.AddDbContext<PersonsDbContext>(options=>
{
    //options.UseSqlServer(builder.Configuration["ConnectionStrings:CMConnection"]);

    options.UseSqlServer(builder.Configuration.GetConnectionString("CMConnection"));
});

// For Logging Specific Fields
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties;
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
});



var app = builder.Build();

app.UseSerilogRequestLogging();

if(builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.Logger.LogDebug("debug-message");
//app.Logger.LogInformation("information-message");
//app.Logger.LogWarning("warning-message");
//app.Logger.LogError("error-message");
//app.Logger.LogCritical("Critical-message");

// Enable Logging
app.UseHttpLogging();

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "rotativa");
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
