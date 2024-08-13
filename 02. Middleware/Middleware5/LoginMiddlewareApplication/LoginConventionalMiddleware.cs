namespace Middleware5.LoginMiddlewareApplication
{
    public class LoginConventionalMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginConventionalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            
            if(httpContext.Request.Method == "POST" && httpContext.Request.Query.ContainsKey("username") && httpContext.Request.Query.ContainsKey("password"))
            {
                var username = httpContext.Request.Query["username"][0];
                var password = httpContext.Request.Query["password"][0];
                if (username != null && password != null && username == "admin@admin.com" && password == "password1234")
                    httpContext.Response.WriteAsync("Login Successful \n \n \n");
                else
                    httpContext.Response.WriteAsync("Login Failed \n \n \n");

            }
                
            return _next(httpContext);
        }

        
    }
    
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoginConventionalMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginConventionalMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginConventionalMiddleware>();
        }
    }
}
