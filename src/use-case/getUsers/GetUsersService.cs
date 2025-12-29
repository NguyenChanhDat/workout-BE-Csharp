using FirstNETWebApp.UseCase.GetUsers.Dtos;

namespace FirstNETWebApp.UseCase.GetUsers;

public class GetUsersService(IUserRepository _userRepository) : IGetUsersService
{
    public async Task<List<UserSummary>> HandleAsync(GetUsersRequest request)
    {
        var users = await _userRepository.GetAll();
        return [.. users.Select(u => new UserSummary(u.Id, u.Username, u.Email, u.MembershipTier))];
    }
}
