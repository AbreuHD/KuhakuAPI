using Core.Application.DTOs.Scraping;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.WebScraping;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class MovieWebRepository : GenericRepository<MovieWeb>, IMovieWebRepository
    {
        private readonly KhakuContext _dbContext;

        public MovieWebRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MovieWebDto>> Exist(List<MovieWebDto> movies)
        {
            List<MovieWebDto> newMovie = [];
            int i = 0;
            foreach (var movie in movies)
            {
                var exists = await _dbContext.Set<MovieWeb>()
                    .AnyAsync(x => x.Url == movie.Url);
                if (!exists)
                {
                    newMovie.Add(movies[i]);
                }
                i++;
            }

            return newMovie;
        }

    }
}
