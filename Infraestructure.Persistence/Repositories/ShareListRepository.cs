using Core.Application.Interface.Repositories;
using Core.Domain.Entities.UserThings;
using Infraestructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class ShareListRepository(KhakuContext dbContext) : GenericRepository<ShareList>(dbContext), IShareListRepository
    {
        private readonly KhakuContext _dbContext = dbContext;

        public async Task<List<ShareList>> GetAllByUserId(string userId)
        {
            return await _dbContext.Set<ShareList>().Where(x => x.UserID == userId).ToListAsync();
        }
    }
}
