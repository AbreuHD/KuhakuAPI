
namespace Core.Application.Interface.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity, int ID);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllAsync(int skip = 0, bool useSkip = false);
        Task<Entity> GetByIdAsync(int Id);
        Task<Entity> GetByStringIdAsync(string Id);
        Task<List<Entity>> GetAllWithIncludes(List<String> properties);
        Task<List<Entity>> AddAllAsync(List<Entity> entity);
    }
}
