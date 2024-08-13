var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// enable Routing
app.UseRouting();


// create endpoints
app.UseEndpoints(endpoints =>
{
    // add your endpoints here
    endpoints.Map("map1", async (context) =>  // execute middleware for any request
    {
        await context.Response.WriteAsync("Inside Map One");
    });

    endpoints.MapPost("map2", async (context) => // execute middleware for POST request
    {
        await context.Response.WriteAsync("Inside Map Two");
   });
});


// for all other endpoints
app.Run(async context =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});


// Get Endpoint Info - should be placed/excuted after useEndpoint() middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    Microsoft.AspNetCore.Http.Endpoint? endPoint = context.GetEndpoint();
    if (endPoint != null)
        await context.Response.WriteAsync($"endpoint: {endPoint.DisplayName}");
    await next(context);
});


app.Run();
