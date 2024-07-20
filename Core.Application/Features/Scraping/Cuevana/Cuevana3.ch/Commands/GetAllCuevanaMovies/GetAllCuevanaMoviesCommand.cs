using AutoMapper;
using Core.Application.DTOs.Relations;
using Core.Application.Helpers.TMDB;
using Core.Application.Interface.Repositories;
using Core.Domain.Entities.GeneralMovie;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.WebScraping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Scraping.Cuevana.Cuevana3.ch.Commands.GetAllCuevanaMovies
{
    public class GetAllCuevanaMoviesCommand : IRequest<bool>
    {

    }

    public class GetAllCuevanaMoviesCommandHandler : IRequestHandler<GetAllCuevanaMoviesCommand, bool>
    {
        private readonly IMovieWebRepository _movieWebRepository;
        private readonly IMovie_MovieWebRepository _movie_MovieWebRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly GetTMDBData _getTMDBData;
        private readonly IMapper _mapper;

        public GetAllCuevanaMoviesCommandHandler(IMovieWebRepository movieWebRepository, IMovie_MovieWebRepository movie_MovieWebRepository, IMovieRepository movieRepository, GetTMDBData getTMDBData, IMapper mapper)
        {
            _movieWebRepository = movieWebRepository;
            _movie_MovieWebRepository = movie_MovieWebRepository;
            _movieRepository = movieRepository;
            _getTMDBData = getTMDBData;
            _mapper = mapper;
        }


        public async Task<bool> Handle(GetAllCuevanaMoviesCommand request, CancellationToken cancellationToken)
        {
            var _cuevanaService = new Services.WebScrapers.MovieESWebsites.Cuevana.Cuevana3.ch.Cuevana3CHServices(1, "https://cuevana3.ch");

            var pagination = await _cuevanaService.GetCuevana3Pagination();
            while(pagination > 0)
            {
                Console.WriteLine($"Paginacion {pagination}");
                List<Movie_MovieWebDTO> relations = new List<Movie_MovieWebDTO>();


                var movies = await _cuevanaService.GetCuevana3(pagination);
                if(movies != null)
                {
                    var data = await _getTMDBData.GetTMDBId(movies);
                    List<Movie> uniqueMovies = data.Movies.GroupBy(m => m.TMDBID).Select(g => g.First()).ToList();
                    await _movieRepository.AddAllAsync(await _movieRepository.Exist(uniqueMovies)); //Movie Added if not exist
                    var movieWeb = await _movieWebRepository.Exist(data.MovieWebDTO);

                    foreach (var movie in movieWeb)
                    {
                        var movieWebAdd = await _movieWebRepository.AddAsync(_mapper.Map<MovieWeb>(movie)); //MovieWeb Added if not exist
                        relations.Add(new Movie_MovieWebDTO
                        {
                            MovieID = movie.TMDBTempID,
                            MovieWebID = movieWebAdd.ID,
                            Verified = false
                        });
                    }

                    var dataMovieEndRelations = await _movieRepository.GetId(_mapper.Map<List<Movie_MovieWeb>>(relations));
                    foreach (var endRelations in dataMovieEndRelations)
                    {
                        try
                        {
                            if (endRelations.MovieID != 0 && endRelations.MovieID != null)
                            {
                                await _movie_MovieWebRepository.AddAsync(endRelations); //Movie_MovieWeb Added
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                pagination--;
            }

            return true;
        }
    }
}
