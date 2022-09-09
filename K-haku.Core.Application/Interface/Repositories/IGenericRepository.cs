using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Interface.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity, int ID);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllAsync(int skip = 0, bool useSkip = false);
        Task<Entity> GetByIdAsync(int Id);
        Task<List<Entity>> GetAllWhitIncludes(List<String> properties);
        Task<List<Entity>> AddAllAsync(List<Entity> entity);
    }
}
