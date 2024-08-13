var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Home");
    });
});

app.UseStaticFiles();


app.Run();
