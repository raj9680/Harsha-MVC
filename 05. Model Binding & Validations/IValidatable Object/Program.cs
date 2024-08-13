var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// For Xml
builder.Services.AddControllers().AddXmlSerializerFormatters().AddXmlDataContractSerializerFormatters();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseStaticFiles();

app.Run();