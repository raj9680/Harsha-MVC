namespace Middleware4
{
    public class MyConventionalMiddleware
    {
        private readonly RequestDelegate _next;
        public MyConventionalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Request.Query.ContainsKey("firstname") && httpContext.Request.Query.ContainsKey("lastname"))
            {
                string fullName = httpContext.Request.Query["firstname"] + " " + httpContext.Request.Query["lastname"];
                await httpContext.Response.WriteAsync("From My Conventional Middleware"+ fullName+"\n");
            }

            await _next(httpContext);
            // after logic
        }
    }

    // Custom Extension Middleware Method
    public static class MyConventionalMiddlewareExtension
    {
        public static IApplicationBuilder UseMyConventionalMiddleware(this IApplicationBuilder app)
        {
            // any code
            return app.UseMiddleware<MyConventionalMiddleware>();
        }
    }
}
