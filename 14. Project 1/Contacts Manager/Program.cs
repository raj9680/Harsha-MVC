using Service;
using ServiceContracts;
using Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// add Services to IOC container
// Singleton: Instantiate one time for all the application
builder.Services.AddScoped<ICountryService, CountryService>();
// 
builder.Services.AddScoped<IPersonService, PersonService>();


// DbContext Registeration
builder.Services.AddDbContext<PersonsDbContext>(options=>
{
    //options.UseSqlServer(builder.Configuration["ConnectionStrings:CMConnection"]);

    options.UseSqlServer(builder.Configuration.GetConnectionString("CMConnection"));
});



var app = builder.Build(); 

if(builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "rotativa");
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
