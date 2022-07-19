
using AutoMapper;
using K_haku.Core.Application.Interface.Repositories;
using K_haku.Core.Application.Interface.Services.Cuevana;
using K_haku.Core.Application.ViewModels;
using K_haku.Core.Application.ViewModels.ScrapPages;
using K_haku.Core.Domain.Entities;
using K_haku.Core.Domain.Entities.Cuevana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Services.Cuevana
{
    public class ScrapPagesMoviesService : GenericService<ScrapPagesViewModel, ScrapPagesInfoViewModel, ScrapPages>, IScrapPagesService
    {
        private readonly IScrapPagesRepository _scrapPagesRepository;
        private readonly IMapper _mapper;

        public ScrapPagesMoviesService(IScrapPagesRepository scrapPagesRepository, IMapper mapper) : base(scrapPagesRepository, mapper)
        {
            _scrapPagesRepository = scrapPagesRepository;
            _mapper = mapper;
        }

        public async Task<List<ScrapPagesInfoViewModel>> GetAllViewModelWhitInclude() //GetAllAsync
        {
            var cuevanaMovieList = await _scrapPagesRepository.GetAllWhitIncludes(new List<string> { });
            return cuevanaMovieList.Select(x => new ScrapPagesInfoViewModel
            {
                Img = x.Img,
                Info = x.Info,
                isOn = x.isOn,
                LastScrap = x.LastScrap,
                PageUrl = x.PageUrl,
                Title = x.Title
            }).ToList();
        }
    }
}
