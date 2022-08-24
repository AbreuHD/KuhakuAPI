using K_haku.Core.Domain.Entities;
using K_haku.Core.Domain.Entities.Cuevana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Interface.Repositories.Cuevana
{
    public interface IMovieListRepository : IGenericRepository<MovieList>
    {
        Task<bool> Exist(string tmdbid);

    }
}
