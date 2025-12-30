using System.Linq.Expressions;

public interface IBaseRepository<Entity>
{
    Task<Entity?> GetOneById(int id);
    Task<List<Entity>> GetAll();
    Task<List<Result>> GetAll<Result>(Expression<Func<Entity, Result>> selector);
    Task<Entity> Create(Entity entity);
    Task Update(Entity entity);
    Task<Entity?> Delete(int id);
}