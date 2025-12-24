using FirstNETWebApp.Services;
using FirstNETWebApp.UseCase.Base.Interfaces;
namespace FirstNETWebApp.UseCase;


class CreateUserValidator(ICreateUserService _createUserService) : IValidator<CreateUserRequest>
{
    public async Task CheaplyValidateAsync(CreateUserRequest request)
    {
        await _createUserService.CheaplyValidateAsync(request);
    }
}