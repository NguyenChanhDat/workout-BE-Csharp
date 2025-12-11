namespace FirstNETWebApp.UseCase.Base.Interfaces;

public interface IValidator<TRequest>
{
    Task ValidateCheapAsync(TRequest request);
}