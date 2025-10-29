
class CreateUserUseCase(ICreateUserService _createUserService) : IUseCase<CreateUserRequest, CreateUserResponse>
{
    public Task<CreateUserResponse> ExecuteAsync(CreateUserRequest request)
    {
        return _createUserService.CreateUserAsync(request);
    }
}