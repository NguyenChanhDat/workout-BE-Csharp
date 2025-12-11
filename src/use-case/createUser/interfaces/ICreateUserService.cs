namespace FirstNETWebApp.Services;

public interface ICreateUserService
{
    Task ValidateAsync(CreateUserRequest request);
    Task<CreateUserResponse> HandleAsync(CreateUserRequest request);
}