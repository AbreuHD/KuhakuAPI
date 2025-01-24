using Core.Domain.Entities.Movie;
using Core.Domain.Entities.Relations;

namespace Core.Application.Interface.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<List<Movie>> Exist(List<Movie> movieList);
        Task<List<MovieMovieWeb>> GetId(List<MovieMovieWeb> movieList);
        Task<int> GetIdByTmdbId(int TmdbId);
        Task<List<Movie>> SearchMovies(string Title);
        Task<Movie> GetMovieInfo(int MovieId);
    }
}
