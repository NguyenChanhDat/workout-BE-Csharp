namespace FirstNETWebApp.Infrastructure.Database.EntityFramework;

public class EfUnitOfWork(DatabaseContext _context) : IUnitOfWork
{
    public async Task ExecuteAsync(Func<Task> operation)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await operation();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
