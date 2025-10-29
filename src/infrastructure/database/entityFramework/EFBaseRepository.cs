using Microsoft.EntityFrameworkCore;

public class EFBaseRepository<Entity>(DatabaseContext context) : IBaseRepository<Entity> where Entity : class
{
    protected readonly DbSet<Entity> _dbSet = context.Set<Entity>();

    public async Task<Entity> Create(Entity entity)
    {
        var entityAdded = await _dbSet.AddAsync(entity);
        return entityAdded.Entity;
    }

    public async Task<Entity?> Delete(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
        return entity;
    }

    public async Task<List<Entity>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Entity?> GetOneById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task Update(Entity entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }
}
