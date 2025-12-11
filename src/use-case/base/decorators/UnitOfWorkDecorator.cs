using FirstNETWebApp.UseCase.Base.Interfaces;

namespace FirstNETWebApp.UseCase.Decorators;

public class UnitOfWorkDecorator<TRequest, TResponse>(IMutationUseCase<TRequest, TResponse> _inner, IUnitOfWork _unitOfWork, IValidator<TRequest> _validator) : IMutationUseCase<TRequest, TResponse>
{
    public async Task<TResponse> ExecuteAsync(TRequest request)
    {
        TResponse result = default!;
        await _validator.ValidateCheapAsync(request);
        await _unitOfWork.ExecuteAsync(async () =>
        {
            result = await _inner.ExecuteAsync(request);
        });
        return result;
    }
}