public class CreateUserService(IUnitOfWork _unitOfWork, IUserRepository _userRepository, IHashService _hashService) : ICreateUserService
{
    public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var passwordHashed = await _hashService.HashPassword(request.Password);
        User? user = null;
        await _unitOfWork.ExecuteAsync(async () =>
         {
             user = await _userRepository.Create(new User
             {
                 Username = request.Username,
                 Email = request.Email,
                 Password = passwordHashed,
                 MembershipTier = request.MembershipTier ?? MembershipTierEnum.Basic
             });
         });
        if (user is null) throw new Exception("User creation failed");

        return new CreateUserResponse(user.Id, user.Username, user.Email, user.MembershipTier);
    }
}