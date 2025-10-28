class CreateUserService : ICreateUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;

    public CreateUserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IHashService hashService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _hashService = hashService;
    }

    public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var passwordHashed = await _hashService.HashPassword(request.Password);
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = passwordHashed
        };
        await _unitOfWork.ExecuteAsync(async () =>
         {
             await _userRepository.Create(user);
         });

        return new CreateUserResponse(user.Id, user.Username, user.Email, user.MembershipTier);
    }
}