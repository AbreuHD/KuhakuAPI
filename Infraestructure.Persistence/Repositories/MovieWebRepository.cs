using Core.Application.DTOs.Scraping;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.WebScraping;
using Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Repositories
{
    public class MovieWebRepository : GenericRepository<MovieWeb>, IMovieWebRepository
    {
        private readonly KhakuContext _dbContext;

        public MovieWebRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MovieWebDTO>> Exist(List<MovieWebDTO> movies)
        {
            List<MovieWebDTO> newMovie = new List<MovieWebDTO>();
            int i = 0;
            foreach (var movie in movies)
            {
                var exists = await _dbContext.Set<MovieWeb>()
                    .AnyAsync(x => x.Url == movie.Url);
                if(exists == false)
                {
                    newMovie.Add(movies[i]);
                }
                i++;
            }

            return newMovie;
        }

    }
}
