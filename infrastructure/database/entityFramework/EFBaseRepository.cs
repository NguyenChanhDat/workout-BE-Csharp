using Microsoft.EntityFrameworkCore;

public class EFBaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
{
    private readonly DatabaseContext _context;
    private readonly DbSet<Entity> _dbSet;

    public EFBaseRepository(DatabaseContext context)
    {
        _context = context;
        _dbSet = context.Set<Entity>();
    }

    public async Task Create(Entity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
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
        await _context.SaveChangesAsync();
    }
}
