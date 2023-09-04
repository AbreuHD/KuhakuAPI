using Core.Domain.Entities.GeneralMovie;
using Core.Domain.Entities.Relations;

namespace Core.Application.Interface.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<List<Movie>> Exist(List<Movie> movie);
        Task<List<Movie_MovieWeb>> GetId(List<Movie_MovieWeb> movie);
    }
}
