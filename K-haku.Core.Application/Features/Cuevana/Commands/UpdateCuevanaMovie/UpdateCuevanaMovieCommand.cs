using AutoMapper;
using K_haku.Core.Application.Interface.Repositories.Cuevana;
using K_haku.Core.Domain.Entities.Cuevana;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_haku.Core.Application.Features.Cuevana.Commands.UpdateCuevanaMovie
{
    public class UpdateCuevanaMovieCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Link { get; set; }
        public string Age { get; set; }
        public string TMDB { get; set; }
        public bool Confirmed { get; set; }
    }

    public class UpdateCuevanaMovieCommandHandler : IRequestHandler<UpdateCuevanaMovieCommand, bool>
    {
        private readonly ICuevanaMoviesRepository _cuevanaMoviesRepository;
        private readonly IMapper _mapper;
        
        public UpdateCuevanaMovieCommandHandler(ICuevanaMoviesRepository cuevanaMoviesRepository, IMapper mapper)
        {
            _cuevanaMoviesRepository = cuevanaMoviesRepository;
            _mapper = mapper;
        }
        
        public async Task<bool> Handle(UpdateCuevanaMovieCommand request, CancellationToken cancellationToken)
        {
            var CuevanaMovieGet = await _cuevanaMoviesRepository.GetByIdAsync(request.Id);
            if (CuevanaMovieGet == null) { return false; }
            CuevanaMovieGet = _mapper.Map<CuevanaMovies>(request);
            await _cuevanaMoviesRepository.UpdateAsync(CuevanaMovieGet, CuevanaMovieGet.ID);
            return true;
        }
    }
}
