using AutoMapper;
using K_haku.Core.Application.Interface.Repositories;
using K_haku.Core.Domain.Entities;
using K_haku.Core.Domain.Entities.Cuevana;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Features.ScrapPages.Commands.UpdateScrapPages
{
    public class UpdateScrapPagesCommand : IRequest<bool>
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string Img { get; set; }
        public string PageUrl { get; set; }
        public char isOn { get; set; }
    }

    public class UpdateScrapPagesCommandHandler : IRequestHandler<UpdateScrapPagesCommand, bool>
    {
        private readonly IScrapPagesRepository _scrapPagesRepository;
        private readonly IMapper _mapper;
        
        public UpdateScrapPagesCommandHandler(IScrapPagesRepository scrapPagesRepository, IMapper mapper)
        {
            _scrapPagesRepository = scrapPagesRepository;
            _mapper = mapper;
        }
        
        public async Task<bool> Handle(UpdateScrapPagesCommand request, CancellationToken cancellationToken)
        {
            var ScrapPa = await _scrapPagesRepository.GetByIdAsync(request.ID);
            if (ScrapPa == null) { return false; }
            ScrapPa = _mapper.Map<ScrapPage>(request);
            await _scrapPagesRepository.UpdateAsync(ScrapPa, ScrapPa.ID);
            return true;
        }
    }
}
