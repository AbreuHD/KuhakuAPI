using AutoMapper;
using K_haku.Core.Application.Interface.Repositories.Cuevana;
using K_haku.Core.Application.WebsScrapers.GetAll.Cuevana;
using K_haku.Core.Domain.Entities.Cuevana;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using K_haku.Core.Application.Helpers;
using K_haku.Core.Domain.Entities;

namespace K_haku.Core.Application.Features.Cuevana.Commands.GetCuevanaMovies
{
    public class GetCuevanaMoviesCommand : IRequest<bool> 
    {
        public bool Start { get; set; }
    }
    public class GetCuevanaMoviesCommandHandler : IRequestHandler<GetCuevanaMoviesCommand, bool>
    {
        private readonly ICuevanaMoviesRepository _cuevanaMoviesRepository;
        private readonly IMovieListRepository _movieListRepository;
        private readonly IMapper _mapper;
        public readonly GetTMDBInfo _getTMDBInfo;

        public GetCuevanaMoviesCommandHandler(ICuevanaMoviesRepository cuevanaMoviesRepository, IMapper mapper, GetTMDBInfo getTMDBInfo, IMovieListRepository movieListRepository)
        {
            _cuevanaMoviesRepository = cuevanaMoviesRepository;
            _mapper = mapper;
            _getTMDBInfo = getTMDBInfo;
            _movieListRepository = movieListRepository;
        }
        public async Task<bool> Handle(GetCuevanaMoviesCommand request, CancellationToken cancellationToken)
        {
            CuevanaGetAllMovies _cuevanaGetAllMovies = new();
            var CuevanaMoviesList = await _cuevanaGetAllMovies.MovieList();
            //List<CuevanaMovies> NewMovies = new();
            //List<MovieList> NewTMDB = new();
            int i = 0;
            foreach (var Movie in CuevanaMoviesList)
            {
                var convert = _mapper.Map<CuevanaMovies>(Movie);
                if (await _cuevanaMoviesRepository.Exist(convert) == false)
                {
                    i++;
                    Console.WriteLine($"converting data #{i} from {convert.Title} : {convert.Link}");
                    var tmdb = await _getTMDBInfo.GetTMDBId(Movie.Title);
                    //convert.TMDBId = await _getTMDBInfo.GetTMDBId(Movie.Title);
                    //if(convert.TMDBId == "") { convert.TMDBId = null; }
                    convert.CreatedBy = "Kuhaku Scrapping";
                    convert.Created = DateTime.Now;
                    
                    if(tmdb != null && (await _movieListRepository.Exist(tmdb.ID) == false))
                    {
                        try
                        {
                            Console.WriteLine($"Getting data TMDB {convert.TMDBId}");
                            convert.TMDBId = tmdb.ID;
                            //var tmdb = await _getTMDBInfo.GetTMDBMovieInfo(convert.TMDBId);
                            tmdb.CreatedBy = "Kuhaku Scrapping";
                            tmdb.Created = DateTime.Now;
                            try
                            {
                                await _movieListRepository.AddAsync(tmdb);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error getting data TMDB {convert.TMDBId}");
                                Console.WriteLine(ex.Message);
                            }
                            //NewTMDB.Add(tmdb);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"Error getting data TMDB {convert.TMDBId}");
                            Console.WriteLine(ex.Message);
                        }
                    }
                    try
                    {
                        await _cuevanaMoviesRepository.AddAsync(convert);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error data db {convert.TMDBId}");
                        Console.WriteLine(ex.Message);
                    }
                    //NewMovies.Add(convert);
                }
            }
            /*try
            {
                await _movieListRepository.AddAllAsync(NewTMDB);
                await _cuevanaMoviesRepository.AddAllAsync(NewMovies);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                await _movieListRepository.AddAllAsync(NewTMDB);
                await _cuevanaMoviesRepository.AddAllAsync(NewMovies);
            }*/

            return true;
        }
    }
}
