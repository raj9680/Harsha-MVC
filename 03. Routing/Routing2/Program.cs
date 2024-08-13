var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

// creating endpoints
app.UseEndpoints(endpoints =>
{

    // routeParameter 1
    // endpoints.Map("employee/profile/{employeename}", async context =>   // dynamic route parameter
    // endpoints.Map("employee/profile/{employeename=Raj}", async context =>  // default route parameter
    endpoints.Map("employee/profile/{employeid?}", async context =>  // optional param returns null
    {
        if (context.Request.RouteValues.ContainsKey("employeid"))
        {
            int? employee_id = Convert.ToInt32(context.Request.RouteValues["employeid"]);
            await context.Response.WriteAsync($"In employee: {employee_id}");
        }
        else
        {
            await context.Response.WriteAsync("Employe Id is not supplied");
        }
    });
    

    // routeParameter 2
    endpoints.Map("files/{filename}.{extension}", async context =>
    {
        string? f_name = Convert.ToString(context.Request.RouteValues["filename"]);
        string? f_extension = Convert.ToString(context.Request.RouteValues["extension"]);

        await context.Response.WriteAsync($"In files: {f_name}-{f_extension}");
    });

});

app.Run();
