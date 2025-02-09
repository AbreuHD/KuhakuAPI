using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Relations;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class MovieList_MovieRepository : GenericRepository<MovieListMovie>, IMovieList_MovieRepository
    {
        private readonly KhakuContext _dbContext;

        public MovieList_MovieRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ItemExist(int MovieId, int ShareListID)
        {
            return await _dbContext.Set<MovieListMovie>().AnyAsync(x => x.MovieID == MovieId && x.ShareListID == ShareListID);
        }
    }
}
