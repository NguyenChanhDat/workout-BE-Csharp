namespace FirstNETWebApp.UseCase.Base.Interfaces;

public interface IValidator<TRequest>
{
    Task CheaplyValidateAsync(TRequest request);
}