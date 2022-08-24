using K_haku.Core.Application.Interface.Repositories;
using K_haku.Core.Application.Interface.Repositories.Cuevana;
using K_haku.Core.Domain.Entities;
using K_haku.Core.Domain.Entities.Cuevana;
using K_haku.Infraestructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Infraestructure.Persistence.Repositories
{
    public class MovieListRepository : GenericRepository<MovieList>, IMovieListRepository
    {
        private readonly K_hakuContext _dbContext;

        public MovieListRepository(K_hakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Exist(string tmdbid)
        {
            var TMDB = await _dbContext.Set<MovieList>().ToListAsync();
            var TMDBCount = TMDB.Where(x => x.ID == tmdbid).Count();
            if(TMDBCount != 0) { return true; }
            return false;
        }
    }
}
