public interface IUnitOfWork
{
    Task<T> ExecuteAsync<T>(Func<Task<T>> action);
}