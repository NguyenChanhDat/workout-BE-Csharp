using FirstNETWebApp.Core.Repository;
using FirstNETWebApp.UseCase.CreateUser.Dtos;

namespace FirstNETWebApp.Services;

public class CreateUserService(IUserRepository _userRepository, IHashService _hashService) : ICreateUserService
{
    public async Task<User> HandleAsync(CreateUserRequest request)
    {
        // This runs inside a transaction (decorator will ensure that)
        var passwordHashed = await _hashService.HashPassword(request.Password);
        var user = await _userRepository.Create(new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = passwordHashed,
            MembershipTier = request.MembershipTier ?? MembershipTierEnum.Basic
        }) ?? throw new Exception("User creation failed");
        return user;
    }
}