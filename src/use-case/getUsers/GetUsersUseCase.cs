using FirstNETWebApp.UseCase.Base.Interfaces;

namespace FirstNETWebApp.UseCase.GetUsers;

public class GetUsersUseCase(IGetUsersService _service) : IQueryUseCase<GetUsersRequest, List<UserSummary>>
{
    public async Task<List<UserSummary>> ExecuteAsync(GetUsersRequest request)
    {
        return await _service.HandleAsync(request);
    }
}
