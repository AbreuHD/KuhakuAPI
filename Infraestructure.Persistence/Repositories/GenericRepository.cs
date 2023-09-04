using Core.Application.Interface.Repositories;
using Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly KhakuContext _dbcontext;

        public GenericRepository(KhakuContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _dbcontext.Set<Entity>().AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<Entity>> AddAllAsync(List<Entity> entity)
        {
            _dbcontext.Set<Entity>().AddRange(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(Entity entity, int ID)
        {
            Entity etry = await _dbcontext.Set<Entity>().FindAsync(ID);
            _dbcontext.Entry(etry).CurrentValues.SetValues(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            _dbcontext.Set<Entity>().Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllAsync(int skip = 0, bool useSkip = false)
        {
            if (useSkip == true)
            {
                return await _dbcontext.Set<Entity>().Skip(skip).Take(30).ToListAsync();
            }
            return await _dbcontext.Set<Entity>().ToListAsync();
        }

        public virtual async Task<List<Entity>> GetAllWithIncludes(List<string> properties)
        {
            var query = _dbcontext.Set<Entity>().AsQueryable();
            foreach (var property in properties)
            {
                query = query.Include(property);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<Entity> GetByIdAsync(int Id)
        {
            return await _dbcontext.Set<Entity>().FindAsync(Id);
        }
        public virtual async Task<Entity> GetByStringIdAsync(string Id)
        {
            return await _dbcontext.Set<Entity>().FindAsync(Id);
        }
    }
}
