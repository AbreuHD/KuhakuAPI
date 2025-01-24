using Core.Domain.Entities.Relations;

namespace Core.Application.Interface.Repositories
{
    public interface IMovieList_MovieRepository : IGenericRepository<MovieListMovie>
    {
        public Task<bool> ItemExist(int MovieId, int ShareListID);
    }
}
