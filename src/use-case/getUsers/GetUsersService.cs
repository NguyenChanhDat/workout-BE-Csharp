using FirstNETWebApp.Core.Repository;

namespace FirstNETWebApp.UseCase.GetUsers;

public class GetUsersService(IUserRepository _userRepository) : IGetUsersService
{
    public async Task<List<UserSummary>> HandleAsync(GetUsersRequest request)
    {
        var users = await _userRepository.GetAll(u => new UserSummary(
            u.Id,
            u.Username,
            u.Email,
            u.MembershipTier
        ));
        return users;
    }
}
