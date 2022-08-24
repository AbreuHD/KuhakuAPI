using AutoMapper;
using K_haku.Core.Application.Interface.Repositories;
using K_haku.Core.Application.ViewModels.ScrapPages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Features.ScrapPages.Queries.GetAll
{
    public class GetAllScrapPagesQuery : IRequest<List<ScrapPagesInfoViewModel>>
    {
        //bool Start { get; set; }
    }

    public class GetAllScrapPagesQueryHandler : IRequestHandler<GetAllScrapPagesQuery, List<ScrapPagesInfoViewModel>>
    {
        IScrapPagesRepository _scrapPagesRepository;
        IMapper _mapper;
        public GetAllScrapPagesQueryHandler(IScrapPagesRepository scrapPagesRepository, IMapper mapper)
        {
            _scrapPagesRepository = scrapPagesRepository;
            _mapper = mapper;
        }

        public async Task<List<ScrapPagesInfoViewModel>> Handle(GetAllScrapPagesQuery request, CancellationToken cancellationToken)
        {
            var Pages = await _scrapPagesRepository.GetAllAsync();
            return _mapper.Map<List<ScrapPagesInfoViewModel>>(Pages);
        }
    }
}
