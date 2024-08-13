using Routing4.CustomRouteConstraints;

var builder = WebApplication.CreateBuilder(args);

// Custom Route Constraints
// Register custom route constraints class
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraints)); // can give any name to your custom defined constraints
});

var app = builder.Build();



app.UseRouting();
app.UseEndpoints(endpoints =>
{
   //endpoints.Map("sales-report/{year:int:min(1900)}/{month:regex(^(apr|jul|oct|jan)$)}", async context => or
    endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}", async context => // here months is custom route constraints
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        if(month == "apr" || month == "jul" || month == "oct" || month == "jan")
        {
            await context.Response.WriteAsync($"Sales report: {year} - {month}");
        }
    });
});


// for all other routes
app.Run(async context =>
{
    await context.Response.WriteAsync($"No route match at {context.Request.Path}");
});

app.Run();
