using Routing5.CustomRouteConstraints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraints));
});
var app = builder.Build();

/* Endpoint Selection Order
 * 
 * 1. URL template with more segments i.e. "a/b/c/d" is higher than "a/b/c".
 * 2. URL template with literal text has more precedence than a parameter segment i.e. "a/b" is higher than      "a/{parameter}".
 * 3. URL template that has a parameter segment with constraints has more precedence than a parameter segment    without constraints i.e. "a/b:int" is higher than "a/b".
 * 4. Catch-all parameters (**) i.e. "a/b" is higher than "a/**".
 *
 */

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    // sales-report/2024/jan
    endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}", async context => 
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        if (month == "apr" || month == "jul" || month == "oct" || month == "jan")
        {
            await context.Response.WriteAsync($"Sales report: {year} - {month}");
        }
        else
        {
            await context.Response.WriteAsync("Sales report is not available");
        }
    });

    // sales-report/2024/jan - more lateral text
    endpoints.Map("sales-report/2024/jan", async context =>
    {
        await context.Response.WriteAsync("Sales report exclusively fro 2024 - jan");
    });
});


app.Run(async context =>
{
    await context.Response.WriteAsync($"No route match at {context.Request.Path}");
});

app.Run();
