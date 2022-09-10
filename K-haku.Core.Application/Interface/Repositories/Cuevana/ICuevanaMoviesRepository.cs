using K_haku.Core.Application.Dtos.Movie;
using K_haku.Core.Domain.Entities;
using K_haku.Core.Domain.Entities.Cuevana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Interface.Repositories.Cuevana
{
    public interface ICuevanaMoviesRepository : IGenericRepository<CuevanaMovies>
    {
        Task<bool> Exist(CuevanaMovies movie);
        Task<MovieResponse> GetByTMDBIdAsync(string Id);

    }
}
