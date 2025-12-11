using System.Reflection;
using Scrutor;

namespace FirstNETWebApp.Infrastructure.Provider;

public static class ServiceProvider
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        // builder.Services.AddSingleton<ICreateUserService, CreateUserService>();
        // Scan and register mutation use-cases by namespace
        builder.Services.Scan(selector => selector
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.InNamespaces(
                "FirstNETWebApp.Services"
                ),
                publicOnly: false
            )
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}