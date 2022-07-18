using K_haku.Core.Application.Interface.Service;
using K_haku.Core.Application.ViewModels;
using K_haku.Core.Application.ViewModels.Cuevana;
using K_haku.Core.Domain.Entities.Cuevana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Interface.Services.Cuevana
{
    public interface ICuevanaMoviesService : IGenericService<MovieViewModel, CuevanaInfoViewModel, CuevanaMovies>
    {
        Task<List<CuevanaInfoViewModel>> GetAllViewModelWhitInclude();
    }
}
