public class CreateUserService(IUnitOfWork _unitOfWork, IUserRepository _userRepository, IHashService _hashService) : ICreateUserService
{
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