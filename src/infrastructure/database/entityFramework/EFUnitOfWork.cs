namespace FirstNETWebApp.Infrastructure.Database.EntityFramework;

public class EfUnitOfWork(DatabaseContext _context) : IUnitOfWork
{
    public async Task<TResponse> ExecuteAsync<TResponse>(Func<Task<TResponse>> operation)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            TResponse result = await operation();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            Console.WriteLine("Transaction committed successfully with Result: " + result?.ToString());
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
