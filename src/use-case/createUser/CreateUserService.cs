namespace FirstNETWebApp.Services;

public class CreateUserService(IUserRepository _userRepository, IHashService _hashService) : ICreateUserService
{
    public async Task ValidateAsync(CreateUserRequest request)
    {
        // lightweight validation outside transaction
        if (string.IsNullOrWhiteSpace(request.Username)) throw new ArgumentException("Username is required");
        if (string.IsNullOrWhiteSpace(request.Password)) throw new ArgumentException("Password is required");
        if (string.IsNullOrWhiteSpace(request.Email)) throw new ArgumentException("Email is required");
        await Task.CompletedTask;
    }

    public async Task<CreateUserResponse> HandleAsync(CreateUserRequest request)
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

        return new CreateUserResponse(user.Id, user.Username, user.Email, user.MembershipTier);
    }
}