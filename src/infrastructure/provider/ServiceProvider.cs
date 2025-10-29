public static class ServiceProvider
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ICreateUserService, CreateUserService>();
    }
}