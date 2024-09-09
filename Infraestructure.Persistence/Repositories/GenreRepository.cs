using Core.Application.Interface.Repositories;
using Core.Domain.Entities.GeneralMovie;
using Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
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
    }
}
