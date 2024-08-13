var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/", async context =>
    {
        await context.Response.WriteAsync("Hello World");
    }); 
});
app.MapControllers();

app.Run();
