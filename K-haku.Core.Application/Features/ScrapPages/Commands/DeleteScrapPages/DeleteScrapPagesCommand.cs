using K_haku.Core.Application.Interface.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Features.ScrapPages.Commands.DeleteScrapPages
{
    public class DeleteScrapPagesCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteScrapPagesCommandHandler : IRequestHandler<DeleteScrapPagesCommand, bool>
    {
        private readonly IScrapPagesRepository _scrapPagesRepository;

        public DeleteScrapPagesCommandHandler(IScrapPagesRepository scrapPagesRepository)
        {
            _scrapPagesRepository = scrapPagesRepository;
        }
        
        public async Task<bool> Handle(DeleteScrapPagesCommand request, CancellationToken cancellationToken)
        {
            var Movie = await _scrapPagesRepository.GetByIdAsync(request.Id);
            await _scrapPagesRepository.DeleteAsync(Movie);
            return true;
        }
            
    }
}
