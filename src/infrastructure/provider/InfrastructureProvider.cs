using System.Reflection;
using FirstNETWebApp.Infrastructure.Database.EntityFramework;

namespace FirstNETWebApp.Infrastructure.Provider;

public static class InfrastructureProvider
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        // For register background tasks like SQS, ...
        // builder.Services.AddHostedService<>();

        // Scan and register concrete repository/service implementations under our infrastructure namespaces
        builder.Services.Scan(selector => selector
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classSelector => classSelector
                .InNamespaces(
                    "FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository",
                    "FirstNETWebApp.Infrastructure.Database.EntityFramework"
                ),
                publicOnly: false
            )
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // For register other providers like AWS, ... (kept explicit registration for clarity)
        builder.Services.AddSingleton<IHashService, ASPHashService>();
    }
}