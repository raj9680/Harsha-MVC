using CustomModelBinder.CustomBinderProvider;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddControllers();

// For registering custom model binder provider
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new PersonBinderProvider());
});

builder.Services.AddControllers().AddXmlSerializerFormatters();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseStaticFiles();

app.Run();