using Core.Application.Interface.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity>(KhakuContext dbcontext) : IGenericRepository<Entity> where Entity : class
    {
        private readonly KhakuContext _dbcontext = dbcontext;

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
            Entity etry = await _dbcontext.Set<Entity>().FindAsync(ID)
                ?? throw new KeyNotFoundException($"{typeof(Entity).Name} not found");
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
            if (useSkip)
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
            return await _dbcontext.Set<Entity>().FindAsync(Id) 
                ?? throw new KeyNotFoundException($"{typeof(Entity).Name} not found");
        }
        public virtual async Task<Entity> GetByStringIdAsync(string Id)
        {
            return await _dbcontext.Set<Entity>().FindAsync(Id)
                ?? throw new KeyNotFoundException($"{typeof(Entity).Name} not found");
        }
    }
}
