using Middleware3;

var builder = WebApplication.CreateBuilder(args);

// registering custom middleware
builder.Services.AddTransient<MyCustomMiddleware>(); // 1

var app = builder.Build();

//Middleware1 // app.Use() shortcircuit execute or pass the control to nextt middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello 1\n");
    await next(context);
});


//Middleware2 // calling custom middleware, we call above as per required order
app.UseMiddleware<MyCustomMiddleware>(); // 2

// OR we can call middleware using extension method
app.UseMyCustomMiddleware();



//Middleware 3 // app.Use() shortcircuit execute or pass the control to nextt middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Hello 3\n");
    // await next(context); // optional i.e of find the response then stop going to next midldeware
});


//Middleware 4
// app.Run() is called non-terminating/short-circuting middleware that may / may not forward the request to the next middleware
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Hello 4\n");
});

app.Run();
