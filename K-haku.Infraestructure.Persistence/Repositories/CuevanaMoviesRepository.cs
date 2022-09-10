using AutoMapper;
using K_haku.Core.Application.Dtos.Movie;
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
        IMapper _mapper;
        public CuevanaMoviesRepository(K_hakuContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> Exist(CuevanaMovies movie)
        {
            var MoviesList = await _dbContext.Set<CuevanaMovies>().ToListAsync();
            var MovieCount = MoviesList.Where(x => x.Link == movie.Link).Count();
            if(MovieCount != 0) { return true; }
            return false;
        }

        public async Task<MovieResponse> GetByTMDBIdAsync(string Id)
        {
            var MovieInfo = await _dbContext.Set<CuevanaMovies>().Where(a => a.TMDBId == Id ).FirstOrDefaultAsync();
            var Movie = _mapper.Map<MovieResponse>(MovieInfo);
            return Movie;
        }
    }
}
