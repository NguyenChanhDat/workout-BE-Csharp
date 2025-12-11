using System.Reflection;
using FirstNETWebApp.UseCase.Base.Interfaces;
using FirstNETWebApp.UseCase.Decorators;
using Scrutor;

namespace FirstNETWebApp.Infrastructure.Provider;

public static class UseCaseProvider
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        RegisterUseCaseScanning(builder);
        RegisterDecorators(builder);
    }

    // Scan and register mutation use-cases by namespace
    private static void RegisterUseCaseScanning(WebApplicationBuilder builder)
    {
        builder.Services.Scan(selector => selector
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(classes => classes.InNamespaces(
                "FirstNETWebApp.UseCase"
                ),
                publicOnly: false
            )
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    // Apply decorators to IMutationUseCase<TRequest,TResponse>
    private static void RegisterDecorators(WebApplicationBuilder builder)
    {
        builder.Services.Decorate(typeof(IMutationUseCase<,>), typeof(UnitOfWorkDecorator<,>));
    }
}