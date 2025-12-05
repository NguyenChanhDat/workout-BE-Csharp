namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

public class EfUserRepository(DatabaseContext context) : EFBaseRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet.FindAsync(username);
    }
}