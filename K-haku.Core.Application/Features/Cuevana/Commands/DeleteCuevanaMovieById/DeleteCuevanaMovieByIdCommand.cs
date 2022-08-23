using K_haku.Core.Application.Interface.Repositories.Cuevana;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Features.Cuevana.Commands.DeleteCuevanaMovieById
{
    public class DeleteCuevanaMovieByIdCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteCuevanaMovieByIdCommandHandler : IRequestHandler<DeleteCuevanaMovieByIdCommand, bool>
    {
        private readonly ICuevanaMoviesRepository _cuevanaMoviesRepository;

        public DeleteCuevanaMovieByIdCommandHandler(ICuevanaMoviesRepository cuevanaMoviesRepository)
        {
            _cuevanaMoviesRepository = cuevanaMoviesRepository;
        }
        
        public async Task<bool> Handle(DeleteCuevanaMovieByIdCommand request, CancellationToken cancellationToken)
        {
            var Movie = await _cuevanaMoviesRepository.GetByIdAsync(request.Id);
            await _cuevanaMoviesRepository.DeleteAsync(Movie);
            return true;
        }
            
    }
}
