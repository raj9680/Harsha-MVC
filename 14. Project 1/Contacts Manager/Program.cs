using Contacts_Manager;
using Contacts_Manager.Filters.ActionFilters;
using Serilog;

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
    options.Filters.Add(new ResponseHeaderActionFilter(logger) { _keyy = "My_Key-From_Global", _valuee = "My-value-From-Global", Order = 2});

    // logger is not required bcz of AttributeFilter
    // options.Filters.Add(new ResponseHeaderActionFilter("My-Key-From-Global", "My-Value-From-Global", 2));
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

// passing configuration in param
builder.Services.ConfigureServices(builder.Configuration);
// builder.Services - first param
// builder.Configuration - second param

var app = builder.Build();



if(!builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error"); //path to redirect
    // app.UseExceptionHandlingMiddleware();
}

app.UseSerilogRequestLogging();

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
