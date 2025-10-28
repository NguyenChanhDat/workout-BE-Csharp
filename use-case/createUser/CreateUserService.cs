class CreateUserService : ICreateUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public CreateUserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password
        };
        await _unitOfWork.ExecuteAsync(async () =>
         {
             await _userRepository.Create(user);
         });

        return new CreateUserResponse(user.Id, user.Username, user.Email, user.MembershipTier);
    }
}