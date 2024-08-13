namespace Middleware3
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custom Middleware - Starts\n"); 
            // before logic

            await next(context);

            await context.Response.WriteAsync("Custom Middleware - Ends\n"); 
            // after logic
        }
    }



    // optional
    // Custom Extension Middleware Method
    public static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
            // any code, we are calling the middleware instead to call in program.cs file
            return app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
