public interface IBaseRepository<Entity>
{
    Task<Entity?> GetOneById(int id);
    Task<List<Entity>> GetAll();
    Task Create(Entity entity);
    Task Update(Entity entity);
    Task Delete(int id);
}