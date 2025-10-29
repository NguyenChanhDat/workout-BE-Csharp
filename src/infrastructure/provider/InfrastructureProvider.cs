public static class InfrastructureProvider
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        // For register background tasks like SQS, ...
        // builder.Services.AddHostedService<>();


        // For register EF repository
        builder.Services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
        builder.Services.AddSingleton<IExerciseRepository, EfExerciseRepository>();
        builder.Services.AddSingleton<IBodyTrackRepository, EfBodyTrackRepository>();
        builder.Services.AddSingleton<ISessionRepository, EfSessionRepository>();
        builder.Services.AddSingleton<ISetRepository, EfSetRepository>();
        builder.Services.AddSingleton<IUserRepository, EfUserRepository>();

        // For register other providers like AWS, ...
        builder.Services.AddSingleton<IHashService, ASPHashService>();
    }
}