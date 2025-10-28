public class EfUnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;

    public EfUnitOfWork(DatabaseContext context)
    {
        _context = context;
    }

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
