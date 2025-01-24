using Core.Domain.Entities.Movie;

namespace Core.Application.Interface.Repositories
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<bool> Exist(int TmdbId);
        Task<int> GetIdByTmdbId(int TmdbId);
    }
}
