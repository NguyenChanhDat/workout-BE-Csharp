namespace FirstNETWebApp.UseCase.Base.Interfaces;

public interface IMutationUseCase<TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request);
}

public interface IQueryUseCase<TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request);
}