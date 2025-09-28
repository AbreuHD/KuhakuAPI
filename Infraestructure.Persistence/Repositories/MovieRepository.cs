using Core.Application.Interface.Repositories;
using Core.Domain.Entities.Movie;
using Core.Domain.Entities.Relations;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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

        public async Task<List<Movie>> SearchMovies(string? title, List<int>? genreFilters, int pageNumber, int pageSize)
        {
            var keywords = string.IsNullOrWhiteSpace(title)
                ? []
                : title!.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            IQueryable<Movie> baseQuery = _dbContext.Set<Movie>();

            if (keywords.Length > 0)
            {
                foreach (var k in keywords)
                {
                    var word = k.Trim().ToLower();
                    baseQuery = baseQuery.Where(m => EF.Functions.Like(m.Title.ToLower(), $"%{word}%"));
                }
            }

            if (genreFilters == null || genreFilters.Count == 0)
            {
                return await baseQuery
                    .OrderByDescending(m => m.Release_date)
                    .Include(m => m.GenreMovie)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            var filteredIdsQuery =
                from m in baseQuery
                join gm in _dbContext.Set<GenreMovie>() on m.ID equals gm.MovieID
                where genreFilters.Contains(gm.GenreID)
                group gm by new
                {
                    m.ID,
                    m.Release_date
                } into g
                where g.Select(x => x.GenreID).Distinct().Count() == genreFilters.Count
                orderby g.Key.Release_date descending
                select g.Key.ID;

            var pageIds = await filteredIdsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (pageIds.Count == 0)
                return [];

            var result = await _dbContext.Set<Movie>()
                .Where(m => pageIds.Contains(m.ID))
                .OrderByDescending(m => m.Release_date)
                .Include(m => m.GenreMovie)
                .ToListAsync();

            return result;
        }
        public async Task<List<(Genre Genre, List<Movie> Movies)>> GetMoviesByGenresAsync(bool kidMode, int limitPerGenre = 6)
        {
            var genres = await _dbContext.Set<Genre>()
                .Include(g => g.GenreMovie!)
                    .ThenInclude(gm => gm.Movie)
                .ToListAsync();

            var result = new List<(Genre Genre, List<Movie> Movies)>();

            foreach (var genre in genres)
            {
                if (genre.GenreMovie is null || genre.GenreMovie.Count == 0)
                    continue;

                var movies = genre.GenreMovie
                    .Select(gm => gm.Movie)
                    .Where(m => m != null &&
                                ((kidMode && m.Adult == false) || !kidMode))
                    .Take(limitPerGenre)
                    .ToList();

                if (movies.Count > 0)
                {
                    result.Add((genre, movies!));
                }
            }

            return result;
        }
    }
}
