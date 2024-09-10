using Core.Domain.Entities.GeneralMovie;

namespace Core.Application.Interface.Repositories
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<bool> Exist(int TmdbId);
        Task<int> GetIdByTmdbId(int TmdbId);
    }
}
