public interface IBaseRepository<Entity>
{
    Task<Entity?> GetOneById(int id);
    Task<List<Entity>> GetAll();
    Task<Entity> Create(Entity entity);
    Task Update(Entity entity);
    Task<Entity?> Delete(int id);
}