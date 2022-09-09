using K_haku.Core.Application.Interface.Repositories;
using K_haku.Infraestructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Infraestructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly K_hakuContext _dbcontext;

        public GenericRepository(K_hakuContext dbcontext)
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
            if(useSkip == true)
            {
                return await _dbcontext.Set<Entity>().Skip(skip).Take(30).ToListAsync();
            }
            return await _dbcontext.Set<Entity>().ToListAsync();
        }



        public virtual async Task<List<Entity>> GetAllWhitIncludes(List<string> properties)
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
    }
}
