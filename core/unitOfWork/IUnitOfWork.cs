public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> operation);
}