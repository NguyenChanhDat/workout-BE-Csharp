public static class InfrastructureProvider
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        // For register background tasks like SQS, ...
        // builder.Services.AddHostedService<>();


        // For register EF repository
        builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        builder.Services.AddScoped<IExerciseRepository, EfExerciseRepository>();
        builder.Services.AddScoped<IBodyTrackRepository, EfBodyTrackRepository>();
        builder.Services.AddScoped<ISessionRepository, EfSessionRepository>();
        builder.Services.AddScoped<ISetRepository, EfSetRepository>();
        builder.Services.AddScoped<IUserRepository, EfUserRepository>();

        // For register other providers like AWS, ...
        builder.Services.AddSingleton<IHashService, ASPHashService>();
    }
}