using System.Reflection;
using FirstNETWebApp.Infrastructure.Database.EntityFramework;

namespace FirstNETWebApp.Infrastructure.Provider;

public static class InfrastructureProvider
{
    /// <summary>
    /// Register dependency-injection services required by the infrastructure layer (repositories, unit of work, and other providers).
    /// </summary>
    /// <param name="builder">The WebApplicationBuilder whose services will be configured for the infrastructure layer.</param>
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        // For register background tasks like SQS, ...
        // builder.Services.AddHostedService<>();

        // Register EF unit of work explicitly
        builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        // Scan and register concrete repository/service implementations under our infrastructure namespaces
        builder.Services.Scan(selector => selector
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classSelector => classSelector.InNamespaces(
                    "FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository",
                    "FirstNETWebApp.Infrastructure.Database.EntityFramework"
                ))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // For register other providers like AWS, ... (kept explicit registration for clarity)
        builder.Services.AddSingleton<IHashService, ASPHashService>();
    }
}