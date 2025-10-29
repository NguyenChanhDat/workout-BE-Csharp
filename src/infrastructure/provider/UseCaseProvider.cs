public static class UseCaseProvider
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IUseCase<CreateUserRequest, CreateUserResponse>, CreateUserUseCase>();
    }
}