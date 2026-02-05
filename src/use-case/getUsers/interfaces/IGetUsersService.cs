namespace FirstNETWebApp.UseCase.GetUsers;

public interface IGetUsersService
{
    Task<List<UserSummary>> HandleAsync(GetUsersRequest request);
}
