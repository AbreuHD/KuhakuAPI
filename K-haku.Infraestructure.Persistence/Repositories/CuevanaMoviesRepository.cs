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
    public class CuevanaMoviesRepository : GenericRepository<CuevanaMovies>, ICuevanaMoviesRepository
    {
        private readonly K_hakuContext _dbContext;

        public CuevanaMoviesRepository(K_hakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Exist(CuevanaMovies movie)
        {
            var MoviesList = await _dbContext.Set<CuevanaMovies>().ToListAsync();
            var MovieCount = MoviesList.Where(x => x.Link == movie.Link).Count();
            if(MovieCount != 0) { return true; }
            return false;
        }
    }
}
