using Core.Domain.Entities.UserThings;

namespace Core.Application.Interface.Repositories
{
    public interface IShareListRepository : IGenericRepository<ShareList>
    {
        public Task<List<ShareList>> GetAllByUserId(string userId, bool isUserLoged);
        public Task<List<ShareList>> SearchShareList(string? name, int pageNumber = 1, int pageSize = 10);
        public Task<ShareList> GetByIdWithMoviesAsync(int id);
    }
}
