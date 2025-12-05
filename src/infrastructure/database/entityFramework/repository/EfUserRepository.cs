namespace FirstNETWebApp.Infrastructure.Database.EntityFramework.Repository;

public class EfUserRepository(DatabaseContext context) : EFBaseRepository<User>(context), IUserRepository
{
    /// <summary>
    /// Retrieves the user with the specified username.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>The User with the specified username, or null if no matching user exists.</returns>
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet.FindAsync(username);
    }
}