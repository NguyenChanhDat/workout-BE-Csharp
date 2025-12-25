using FirstNETWebApp.UseCase.Base.Interfaces;
namespace FirstNETWebApp.UseCase;


class CreateUserValidator : IValidator<CreateUserRequest>
{
    public async Task CheaplyValidateAsync(CreateUserRequest request)
    {
        // lightweight validation outside transaction
        if (string.IsNullOrWhiteSpace(request.Username)) throw new ArgumentException("Username is required");
        if (string.IsNullOrWhiteSpace(request.Password)) throw new ArgumentException("Password is required");
        if (string.IsNullOrWhiteSpace(request.Email)) throw new ArgumentException("Email is required");
        await Task.CompletedTask;
    }
}