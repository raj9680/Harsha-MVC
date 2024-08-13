var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    //endpoints.Map("employee/profile/{employeid:int?}", async context =>  // constraint param accepts only int
    endpoints.Map("employee/profile/{reportdate:datetime}", async context => // constraint param accepts only datetime
    {
        if (context.Request.RouteValues.ContainsKey("reportdate"))
        {
            DateTime d_date = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
            await context.Response.WriteAsync($"In employee: {d_date.ToShortDateString()}");
        }
        else
        {
            await context.Response.WriteAsync("Datetime is not supplied");
        }
    });


    // Eg:  cities/cityid - with GUID
    endpoints.Map("cities/{cityid:guid}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("cityid"))
        {
            Guid city_id = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])!); // ! means value cannot be null
            await context.Response.WriteAsync($"City Id is: {city_id}");
        }
        else
        {
            await context.Response.WriteAsync("City Id is not GUID");
        }
    });


    // Eg:  cities/cityid - minlenght(value), maxlength(value), length(min,max)
    // endpoints.Map("employee/profile/{employeename:minlength(3):maxlength(7)=Rajesh}", async context =>
    endpoints.Map("employee/profile/{employeename:length(3,7)=Rajesh}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("employeename"))
        {
            DateTime d_date = Convert.ToDateTime(context.Request.RouteValues["employeename"]);
            await context.Response.WriteAsync($"In employee length: {d_date.ToShortDateString()}");
        }
        else
        {
            await context.Response.WriteAsync("Length is not supplied");
        }
    });
    // min(), max() for numbers, length(1,4) fro text length
});

// for all other endpoints
app.Run(async context =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();
