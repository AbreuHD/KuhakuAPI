using AutoMapper;
using K_haku.Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using K_haku.Core.Domain.Entities;

namespace K_haku.Core.Application.Features.ScrapPages.Commands.CreateScrapPages
{
    public class CreateScrapPagesCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public string Info { get; set; }
        public string Img { get; set; }
        public string PageUrl { get; set; }
    }

    public class CreateScrapPagesCommandHandler : IRequestHandler<CreateScrapPagesCommand, bool>
    {
        private readonly IScrapPagesRepository _scrapPagesRepository;
        private readonly IMapper _mapper;
        public CreateScrapPagesCommandHandler(IScrapPagesRepository scrapPagesRepository, IMapper mapper)
        {
            _mapper = mapper;
            _scrapPagesRepository = scrapPagesRepository;
        }

        public async Task<bool> Handle(CreateScrapPagesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ScrapPage scrapP = _mapper.Map<ScrapPage>(request);
                scrapP.Created = DateTime.Now;
                await _scrapPagesRepository.AddAsync(scrapP);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
