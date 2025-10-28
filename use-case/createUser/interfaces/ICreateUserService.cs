public interface ICreateUserService
{
    Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
}