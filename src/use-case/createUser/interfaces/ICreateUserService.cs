namespace FirstNETWebApp.Services;

public interface ICreateUserService
{
    Task<CreateUserResponse> HandleAsync(CreateUserRequest request);
}