var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // adds all the classes as services

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); 
});

// OR
// app.MapControllers();

app.Run();


