
class CreateUserUseCase : IUseCase<CreateUserRequest, CreateUserResponse>
{
    private readonly ICreateUserService _createUserService;
    public CreateUserUseCase(ICreateUserService createUserService)
    {
        _createUserService = createUserService;
    }

    public Task<CreateUserResponse> ExecuteAsync(CreateUserRequest request)
    {
        return _createUserService.CreateUserAsync(request);
    }
}