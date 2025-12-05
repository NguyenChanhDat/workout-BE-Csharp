using Microsoft.EntityFrameworkCore;

namespace FirstNETWebApp.Infrastructure.Database.EntityFramework;

public class EFBaseRepository<Entity>(DatabaseContext context) : IBaseRepository<Entity> where Entity : class
{
    protected readonly DbSet<Entity> _dbSet = context.Set<Entity>();

    public async Task<Entity> Create(Entity entity)
    {
        var entityAdded = await this._dbSet.AddAsync(entity);
        return entityAdded.Entity;
    }

    public async Task<Entity?> Delete(int id)
    {
        var entity = await this._dbSet.FindAsync(id);
        if (entity != null)
        {
            this._dbSet.Remove(entity);
        }
        return entity;
    }

    public async Task<List<Entity>> GetAll()
    {
        return await this._dbSet.ToListAsync();
    }

    public async Task<Entity?> GetOneById(int id)
    {
        return await this._dbSet.FindAsync(id);
    }

    public async Task Update(Entity entity)
    {
        this._dbSet.Update(entity);
        await Task.CompletedTask;
    }
}
