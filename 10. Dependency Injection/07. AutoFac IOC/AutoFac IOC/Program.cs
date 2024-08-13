using Autofac;
using Autofac.Extensions.DependencyInjection;

using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);


#region Enabling_AutoFac

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

#endregion


builder.Services.AddControllersWithViews();


#region RegisteringServiceInAutoFac

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerDependency(); // Equavalent to AddTransient in regular container

    containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerLifetimeScope(); // Equavalent to AddScoped in regular container

    // containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().SingleInstance(); // Equavalent to Singleton in regular container
});

#endregion


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
