using FirstNETWebApp.UseCase.Base.Interfaces;

namespace FirstNETWebApp.UseCase.Decorators;

public class UnitOfWorkDecorator<TRequest, TResponse>(IMutationUseCase<TRequest, TResponse> _inner, IUnitOfWork _unitOfWork, IValidator<TRequest> _validator) : IMutationUseCase<TRequest, TResponse>
{
    public async Task<TResponse> ExecuteAsync(TRequest request)
    {
        await _validator.CheaplyValidateAsync(request);
        return await _unitOfWork.ExecuteAsync(
            () => _inner.ExecuteAsync(request)
        );
    }
}