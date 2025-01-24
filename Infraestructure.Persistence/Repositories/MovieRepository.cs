using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Movie;
using Core.Domain.Entities.Relations;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly KhakuContext _dbContext;

        public MovieRepository(KhakuContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Movie>> Exist(List<Movie> movieList)
        {
            List<Movie> allMovies = [];
            foreach (var movie in movieList)
            {
                var exists = await _dbContext.Set<Movie>()
                    .AnyAsync(x => x.TMDBID == movie.TMDBID);
                if (!exists)
                {
                    allMovies.Add(movie);
                }
            }
            return allMovies;

        }

        public async Task<List<MovieMovieWeb>> GetId(List<MovieMovieWeb> movieList)
        {
            List<MovieMovieWeb> allMovies = [];
            foreach (var movie in movieList)
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

        public async Task<int> GetIdByTmdbId(int TmdbId)
        {
            return (await _dbContext.Set<Movie>().FirstAsync(x => x.TMDBID == TmdbId)).ID;
        }

        public async Task<Movie> GetMovieInfo(int MovieId)
        {
            var response = await _dbContext.Set<Movie>().FindAsync(MovieId);
            await _dbContext.Entry(response).Collection(x => x.MovieMovieWeb).LoadAsync();
            await _dbContext.Entry(response).Collection(x => x.GenreMovie).LoadAsync();
            return response;
        }

        public async Task<List<Movie>> SearchMovies(string Title)
        {
            var searchKeywords = Title.ToLower().Split(' ');
            var responseMovies = new List<Movie>();
            foreach (var keyword in searchKeywords)
            {
                var moviesMatchingKeyword = await _dbContext.Set<Movie>()
                    .Where(x => x.Title.ToLower().Contains(keyword)).Include(x => x.GenreMovie)
                    .ToListAsync();
                responseMovies.AddRange(moviesMatchingKeyword);
            }
            responseMovies = responseMovies.Distinct().ToList();

            return responseMovies;
        }
    }
}
