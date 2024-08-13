namespace Middleware4
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custom Middleware - Starts \n");
            await next(context);
            await context.Response.WriteAsync("Custom Middleware - Ends \n");
        }
    }


    // Custom Extension Middleware Method
    public static class UseMyCustomMiddleware
    {
        public static IApplicationBuilder UseMyCustomMiddlewar(this IApplicationBuilder app)
        {
            // any code
            return app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}