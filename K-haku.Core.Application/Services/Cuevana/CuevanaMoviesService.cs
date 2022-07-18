
using AutoMapper;
using K_haku.Core.Application.Interface.Repositories.Cuevana;
using K_haku.Core.Application.Interface.Services.Cuevana;
using K_haku.Core.Application.ViewModels;
using K_haku.Core.Application.ViewModels.Cuevana;
using K_haku.Core.Domain.Entities.Cuevana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Services.Cuevana
{
    public class CuevanaMoviesService : GenericService<MovieViewModel, CuevanaInfoViewModel, CuevanaMovies>, ICuevanaMoviesService
    {
        private readonly ICuevanaMoviesRepository _cuevanaMoviesRepository;
        private readonly IMapper _mapper;

        public CuevanaMoviesService(ICuevanaMoviesRepository cuevanaMoviesRepository, IMapper mapper) : base(cuevanaMoviesRepository, mapper)
        {
            _cuevanaMoviesRepository = cuevanaMoviesRepository;
            _mapper = mapper;
        }

        public async Task<List<CuevanaInfoViewModel>> GetAllViewModelWhitInclude() //GetAllAsync
        {
            var cuevanaMovieList = await _cuevanaMoviesRepository.GetAllWhitIncludes(new List<string> { "Posts", "Register" });
            return cuevanaMovieList.Select(x => new CuevanaInfoViewModel
            {
                Age = x.Age,
                ID = x.ID,
                Link = x.Link,
                Photo = x.Photo,
                Title = x.Title,
                //Seen = x.se
            }).ToList();
        }
    }
}
