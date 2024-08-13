var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Middleware1 // app.Use() shortcircuit execute or pass the control to nextt middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello");
    await next(context);
    await context.Response.WriteAsync("Reversed after middleware 2 execution"); // execute on return from m2
});

//Middleware 2 
// app.Run() is called non-terminating/short-circuting middleware that may / may not forward the request to the next middleware
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello");
});

app.Run();
