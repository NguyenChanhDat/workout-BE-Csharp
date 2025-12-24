namespace FirstNETWebApp.Services;

public interface ICreateUserService
{
    Task CheaplyValidateAsync(CreateUserRequest request);
    Task<CreateUserResponse> HandleAsync(CreateUserRequest request);
}