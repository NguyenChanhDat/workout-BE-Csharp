namespace FirstNETWebApp.Services;

public interface ICreateUserService
{
    Task<User> HandleAsync(CreateUserRequest request);
}