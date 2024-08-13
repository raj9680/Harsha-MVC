using Microsoft.Extensions.FileProviders;



// configuring custom assets folder
var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = "myroot" // accepts only one folder name - myroot
});

var app = builder.Build();




// StaticFiles
app.UseStaticFiles(); // works with the webroot path




app.UseStaticFiles(new StaticFileOptions() // for multiple folders - mywebroot
{
    //FileProvider = new PhysicalFileProvider(builder.Environment.ContentRootPath + "\\mywebroot")  OR
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "mywebroot"))
});



#region Assignment


IDictionary<int, string> countries = new Dictionary<int, string>();
countries.Add(1, "USA");
countries.Add(2, "Canada");
countries.Add(3, "United Kingdom");
countries.Add(4, "India");
countries.Add(5, "Japan");


app.UseRouting();
app.UseEndpoints(endpoints =>
{
    // Route One
    endpoints.Map("/", async context =>
    {
        await context.Response.WriteAsync("Hello");
    });



    // Route Two
    endpoints.Map("/countries", async context =>
    {
        for (int i = 1; i <= 5; i++)
        {
            await context.Response.WriteAsync(countries[i]+" \n");
        }
    });



    // Route Three
    endpoints.Map("/countries/{id:int:range(1,100)}", async context =>
    {
        int country_id = Convert.ToInt32(context.Request.RouteValues["id"]);

        if (country_id > 5)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("[No Country]");
        }

        if (country_id <= 5)
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync($"{countries[country_id]}");
        }
    });


    // Route Four - When request path is "countries/{id}"
    endpoints.MapGet("/countries/{id:int:min(101)}", async context =>
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("The id should be between 1 and 100 - min");
    });

});

#endregion


app.Run();
