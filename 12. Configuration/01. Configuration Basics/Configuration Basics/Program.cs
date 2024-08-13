using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

#region Registering Services
builder.Services.AddScoped<ICitiesService, CitiesService>();
#endregion

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.Map("/", async context =>
    {
        await context.Response.WriteAsync(app.Configuration["MyKey"]+"\n");

        // OR

        await context.Response.WriteAsync(app.Configuration.GetValue<string>("MyKey")+"\n");

        // OR with default value

        await context.Response.WriteAsync(app.Configuration.GetValue<string>("InvalidKey", "Key is not found, so this is default value") + "\n");
    });
});
//app.MapControllers();


app.Run();
