var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();  // Registering Controllers Services
var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoint =>
{
    endpoint.Map("/", async context =>
    {
        await context.Response.WriteAsync("From Routing Middleware");
    });
});
app.MapControllers();


app.Run();
