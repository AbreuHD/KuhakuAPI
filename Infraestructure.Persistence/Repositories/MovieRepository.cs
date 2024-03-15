using Core.Application.Interface.Repositories;
using Core.Domain.Entities.GeneralMovie;
using Core.Domain.Entities.Relations;
using Core.Domain.Entities.WebScraping;
using Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly KhakuContext _dbContext;

        public MovieRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Movie>> Exist(List<Movie> movies)
        {
            List<Movie> allMovies = new List<Movie>();
            foreach (var movie in movies)
            {
                var exists = await _dbContext.Set<Movie>()
                    .AnyAsync(x => x.TMDBID == movie.TMDBID);
                if (exists == false)
                {
                    allMovies.Add(movie);
                }
            }
            return allMovies;

        }

        public async Task<List<Movie_MovieWeb>> GetId(List<Movie_MovieWeb> movies)
        {
            List<Movie_MovieWeb> allMovies = new List<Movie_MovieWeb>();
            foreach (var movie in movies)
            {
                var movieId = await _dbContext.Set<Movie>()
                        .Where(m => m.TMDBID == movie.MovieID)
                        .Select(m => m.ID)
                        .FirstOrDefaultAsync();

                movie.MovieID = movieId;
                allMovies.Add(movie);
            }
            
            return allMovies;
        }

        public async Task<Movie> GetMovieWebPage(int MovieId)
        {
            var response = await _dbContext.Set<Movie>().Include(x => x.Movie_MovieWeb).FirstOrDefaultAsync(x => x.ID == MovieId);
            return response;
        }

        public async Task<(List<Movie>, List<Genre>)> SearchMovies(string Title, List<int> Value = null)
        {
            var searchKeywords = Title.ToLower().Split(' ');
            var responseGenres = new List<Genre>();
            //var responseMovies = await _dbContext.Set<Movie>().Where(x => x.Title.ToLower().Contains(Title.ToLower())).ToListAsync();
            var responseMovies = new List<Movie>();
            foreach (var keyword in searchKeywords)
            {
                var moviesMatchingKeyword = await _dbContext.Set<Movie>()
                    .Where(x => x.Title.ToLower().Contains(keyword))
                    .ToListAsync();

                responseMovies.AddRange(moviesMatchingKeyword);
            }
            responseMovies = responseMovies.Distinct().ToList();

            return (responseMovies, responseGenres);
        }
    }
}
