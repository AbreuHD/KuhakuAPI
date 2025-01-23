using Core.Domain.Entities.UserThings;

namespace Core.Application.Interface.Repositories
{
    public interface IShareListRepository : IGenericRepository<ShareList>
    {
        public Task<List<ShareList>> GetAllByUserId(string userId, bool isUserLoged);
        public Task<List<ShareList>> SearchShareList(string Name);
    }
}
