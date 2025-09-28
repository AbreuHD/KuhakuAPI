using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Movie;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private readonly KhakuContext _dbContext;

        public GenreRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Exist(int TmdbId)
        {
            return await _dbContext.Set<Genre>().Where(x => x.GenreID == TmdbId).FirstOrDefaultAsync() != null;
        }

        public async Task<int> GetIdByTmdbId(int TmdbId)
        {
            return (await _dbContext.Set<Genre>().Where(x => x.GenreID == TmdbId).FirstOrDefaultAsync()).ID;
        }
        public async Task<List<Genre>> GetAllByIdsAsync(IEnumerable<int> ids)
        {
            return await _dbContext.Set<Genre>()
                .Where(g => ids.Contains(g.ID))
                .ToListAsync();
        }
    }
}
