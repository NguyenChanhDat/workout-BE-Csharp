using FirstNETWebApp.Services;
using FirstNETWebApp.UseCase.Base.Interfaces;
namespace FirstNETWebApp.UseCase;

public class CreateUserUseCase(ICreateUserService _createUserService) : IMutationUseCase<CreateUserRequest, User>
{
    // This method will be executed inside a transaction by UnitOfWorkDecorator
    public Task<User> ExecuteAsync(CreateUserRequest request)
    {
        return _createUserService.HandleAsync(request);
    }
}