using Core.Domain.Entities.Movie;
using Core.Domain.Entities.Relations;

namespace Core.Application.Interface.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<List<Movie>> Exist(List<Movie> movieList);
        Task<List<MovieMovieWeb>> GetId(List<MovieMovieWeb> movieList);
        Task<int> GetIdByTmdbId(int TmdbId);
        Task<List<Movie>> SearchMovies(string? title, List<int>? genreFilters, int pageNumber, int pageSize);
        Task<Movie> GetMovieInfo(int MovieId);
        Task<List<(Genre Genre, List<Movie> Movies)>> GetMoviesByGenresAsync(bool kidMode, int limitPerGenre = 6);
    }
}
