using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Movie;
using Core.Domain.Entities.Relations;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class MovieRepository(KhakuContext dbContext) : GenericRepository<Movie>(dbContext), IMovieRepository
    {
        private readonly KhakuContext _dbContext = dbContext;

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
            var movie = await _dbContext.Set<Movie>().FirstOrDefaultAsync(x => x.TMDBID == TmdbId);
            return movie?.ID ?? 0;
        }

        public async Task<Movie> GetMovieInfo(int MovieId)
        {
            try
            {
                var response = await _dbContext.Set<Movie>().FindAsync(MovieId);
                if (response != null)
                {
                    await _dbContext.Entry(response).Collection(x => x.MovieMovieWeb!).LoadAsync();
                    await _dbContext.Entry(response).Collection(x => x.GenreMovie!).LoadAsync();
                }
                return response!;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("An error occurred while retrieving movie information.", e);
            }
        }

        public async Task<List<Movie>> SearchMovies(string title, int pageNumber, int pageSize)
        {
            IQueryable<Movie> query = _dbContext.Set<Movie>()
                .Include(x => x.GenreMovie)
                .OrderByDescending(x => x.Release_date);

            if (!string.IsNullOrWhiteSpace(title))
            {
                var searchKeywords = title.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var keyword in searchKeywords)
                {
                    query = query.Where(x => EF.Functions.Like(x.Title.ToLower(), $"%{keyword}%"));
                }
            }

            var movies = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return movies;
        }
    }
}
